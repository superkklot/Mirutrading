using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mirutrading.Infrastructure.Tests
{
	[TestClass]
	public class SecurityTest
	{
		[TestMethod]
		public void TestMd5()
		{
			string source = "admin_admin123";
			var target = Security.ComputeMd5String(source);

			Assert.AreEqual("3598f66fbc93c0d5abd2dabab9de74cc", target);
		}
	}
}
