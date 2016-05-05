using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.Documents;
using Mirutrading.Application.ViewModel.Home;
using Mirutrading.Infrastructure;
using Mirutrading.Repository;
using Mirutrading.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mirutrading.Application.Core.Images;
using Lucene.Net.Search;
using Mirutrading.Application.ViewModel.Admin;
using PanGu.HighLight;
using PanGu;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Tokenattributes;

namespace Mirutrading.Application.Core.SearchEngine
{
    public class SearchLucene : ISearch
    {
        private IProductRepository _productRepository;
        private IImageRepository _imageRepository;

        private const string _id = "id";
        private const string _type = "type";
        private const string _linkurl = "linkurl";
        private const string _name = "name";
        private const string _imgurl = "imgurl";

        public SearchLucene()
        {
            _productRepository = new ProductRepository();
            _imageRepository = new ImageRepository();
        }

        public void CreateIndexForProducts(string path)
        {
            FSDirectory directory = FSDirectory.Open(
                new DirectoryInfo(path), new NativeFSLockFactory());
            using (directory)
            {
                if (IndexWriter.IsLocked(directory)) return;
                //bool isExist = IndexReader.IndexExists(directory);
                IndexWriter writer = new IndexWriter(directory, new PanGuAnalyzer(), true, IndexWriter.MaxFieldLength.UNLIMITED);
                using (writer)
                {
                    var products = _productRepository.FindAll();
                    if (products.HasValue())
                    {
                        foreach (var product in products)
                        {
                            Document document = new Document();
                            document.Add(new Field(_id, product._id, Field.Store.YES, Field.Index.NOT_ANALYZED));
                            document.Add(new Field(_type, product.Type.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                            document.Add(new Field(_linkurl, product.LinkUrl, Field.Store.YES, Field.Index.NOT_ANALYZED));
                            document.Add(new Field(_name, product.Name, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS));
                            var imgs = _imageRepository.GetByProductId(product._id);
                            if (imgs.HasValue())
                            {
                                var imgSize = ImageHandler.GetImgSizes()[1];
                                var img = imgs.FirstOrDefault(m => m.Width == imgSize.Width && m.Height == imgSize.Height);
                                if (img != null)
                                {
                                    document.Add(new Field(_imgurl, img.Url, Field.Store.YES, Field.Index.NOT_ANALYZED));
                                }
                            }
                            writer.AddDocument(document);
                        }
                    }
                }
            }
        }

        public IList<IndexProduct> SearchProducts(string path, string term)
        {
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(path), new NoLockFactory());
            using (directory)
            {
                IndexReader reader = IndexReader.Open(directory, true);
                using (reader)
                {
                    IndexSearcher searcher = new IndexSearcher(reader);
                    PhraseQuery query = new PhraseQuery();
                    var termSplits = SplitWords(term);
                    foreach (var t in termSplits)
                    {
                        query.Add(new Term(_name, t));
                    }
                    query.Slop = 0;
                    TopScoreDocCollector collector = TopScoreDocCollector.Create(1000, true);
                    searcher.Search(query, collector);
                    ScoreDoc[] docs = collector.TopDocs(0, collector.TotalHits).ScoreDocs;
                    IList<IndexProduct> results = new List<IndexProduct>();
                    for (int i = 0; i < docs.Length; i++)
                    {
                        int docId = docs[i].Doc;
                        Document doc = searcher.Doc(docId);
                        IndexProduct idxPrd = new IndexProduct();
                        idxPrd.Product = new ProductRequest();
                        idxPrd.Product._id = doc.Get(_id) ?? "";
                        idxPrd.Product.Type = doc.Get(_type).ToInt();
                        idxPrd.Product.LinkUrl = doc.Get(_linkurl) ?? "";
                        idxPrd.Product.Name = HighLight(term, doc.Get(_name) ?? "");
                        idxPrd.Image = new ImageRequest();
                        idxPrd.Image.Url = doc.Get(_imgurl) ?? "";
                        results.Add(idxPrd);
                    }
                    return results;
                }
            }
        }

        private static string[] SplitWords(string content)
        {
            List<string> strList = new List<string>();
            PanGuAnalyzer analyzer = new PanGuAnalyzer();
            TokenStream tokenStream = analyzer.TokenStream("", new StringReader(content));
            while(tokenStream.IncrementToken())
            {
                var ita = tokenStream.GetAttribute<ITermAttribute>();
                strList.Add(ita.Term);
            }
            return strList.ToArray();
        }

        private string HighLight(string keyword, string content)
        {
            SimpleHTMLFormatter simpleHtmlFormatter =
                new SimpleHTMLFormatter("<font style=\"color:#cc0000;\"><b>", "</b></font>");
            Highlighter highlighter = new Highlighter(simpleHtmlFormatter, new Segment());
            highlighter.FragmentSize = 1000;
            return highlighter.GetBestFragment(keyword, content);
        }
    }
}
