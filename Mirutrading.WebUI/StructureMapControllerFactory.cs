using Mirutrading.Infrastructure.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Mirutrading.WebUI
{
	public class StructureMapControllerFactory : DefaultControllerFactory
	{
		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			return MirutradingContainer.Instance.GetInstance(controllerType) as IController;
		}
	}
}