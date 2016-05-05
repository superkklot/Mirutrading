using Mirutrading.Application.ViewModel.Home;
using Mirutrading.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirutrading.Application.Core.SearchEngine
{
    public interface ISearch
    {
        void CreateIndexForProducts(string path);
        IList<IndexProduct> SearchProducts(string path, string term);
    }
}
