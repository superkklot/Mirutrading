using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirutrading.Application.Core.SearchEngine
{
    public class SearchProvider
    {
        public static ISearch GetSearch()
        {
            return new SearchLucene();
        }
    }
}
