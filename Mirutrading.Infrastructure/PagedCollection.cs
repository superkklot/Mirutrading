using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirutrading.Infrastructure
{
	public class PagedCollection<T>
		where T: class
	{
		private List<T> _list;
		private int _pageindex;
		private int _pagesize;
		private int _pagecount;
		private int _itemcount;

		public int PageIndex { get { return _pageindex; } }
		public int PageSize { get { return _pagesize; } }
		public int PageCount { get { return _pagecount; } }
		public int ItemCount { get { return _itemcount; } }
		public List<T> Items { get { return _list; } }

		public PagedCollection(List<T> list,
			int pageindex, int pagesize, int itemcount)
		{
			_list = list;
			_pageindex = pageindex;
			_pagesize = pagesize;
			_itemcount = itemcount;
			_pagecount = (int)Math.Ceiling((double)_itemcount / (double)_pagesize);
		}
	}
}
