using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirutrading.Infrastructure.IOC
{
	class ContainerFactory
	{
		public static IContainer GetContainer()
		{
			var register = new Registry();
			register.IncludeRegistry<MirutradingRegistry>();
			IContainer container = new Container(register);
			return container;
		}
	}
}
