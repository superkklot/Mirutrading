using Mirutrading.Application.Core.Models.Images;
using Mirutrading.Application.ViewModel.Home;
using Mirutrading.Infrastructure;
using Mirutrading.Repository.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirutrading.Application.Interface
{
    public interface IHomeService
    {
        PagedCollection<IndexProduct> GetBraProducts(int pageindex, int pagesize, ImageSize imgSize);
        PagedCollection<IndexProduct> GetBriefsProducts(int pageindex, int pagesize, ImageSize imgSize);
        PagedCollection<IndexProduct> SearchProducts(int pageindex, int pagesize, string path, string term);
    }
}
