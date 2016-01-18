using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirutrading.Infrastructure.IOC
{
	class MirutradingRegistry : Registry
	{
		public MirutradingRegistry()
		{
			Scan(_ =>
			{
				_.Assembly("Mirutrading.WebUI");
				_.Assembly("Mirutrading.Application");
				_.Assembly("Mirutrading.Repository");
				_.WithDefaultConventions();
			});
		}
	}
}
