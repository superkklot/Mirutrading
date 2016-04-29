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

namespace Mirutrading.Application.Core.SearchEngine
{
    public class SearchLucene : ISearch
    {
        private IProductRepository _productRepository;
        private IImageRepository _imageRepository;

        public SearchLucene()
        {
            _productRepository = new ProductRepository();
            _imageRepository = new ImageRepository();
        }

        public void CreateIndexForProducts(string path)
        {
            FSDirectory directory = FSDirectory.Open(
                new DirectoryInfo(path), new NativeFSLockFactory());
            if (IndexWriter.IsLocked(directory)) return;
            bool isExist = IndexReader.IndexExists(directory);
            IndexWriter writer = new IndexWriter(directory, new PanGuAnalyzer(), !isExist, IndexWriter.MaxFieldLength.UNLIMITED);
            var products = _productRepository.FindAll();
            if(products.HasValue())
            {
                foreach (var product in products)
                {
                    Document document = new Document();
                    document.Add(new Field("id", product._id, Field.Store.YES, Field.Index.NOT_ANALYZED));
                    document.Add(new Field("type", product.Type.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                    document.Add(new Field("linkurl", product.LinkUrl, Field.Store.YES, Field.Index.NOT_ANALYZED));
                    document.Add(new Field("name", product.Name, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS));
                    var imgs = _imageRepository.GetByProductId(product._id);
                    if(imgs.HasValue())
                    {
                        var imgSize = ImageHandler.GetImgSizes()[1];
                        var img = imgs.FirstOrDefault(m => m.Width == imgSize.Width && m.Height == imgSize.Height);
                        if(img != null)
                        {
                            document.Add(new Field("imgurl", img.Url, Field.Store.YES, Field.Index.NOT_ANALYZED));
                        }
                    }
                    writer.AddDocument(document);
                }
            }
            writer.Close();
            directory.Dispose();
        }

        public PagedCollection<IndexProduct> SearchProducts(string term)
        {
            throw new NotImplementedException();
        }
    }
}
