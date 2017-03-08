using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Contacts.UITests
{
	[TestFixture(Platform.Android)]
	[TestFixture(Platform.iOS)]
	public class Tests
	{
		IApp app;
		Platform platform; 

		public Tests(Platform platform)
		{
			this.platform = platform;
		}

		[SetUp]
		public void BeforeEachTest()
		{
			app = AppInitializer.StartApp(platform);
		}

		[Test]
		public void AppLaunches()
		{
			app.Screenshot("First screen.");
			//app.Repl();
		}

		[Test]
		public void LoginWillSuccess()
		{
			app.EnterText(x => x.Marked("entUserName"), "Humberto Jaimes"); 
			app.EnterText(x => x.Marked("entEmail"), "Humberto@humbertojaimes.net"); 
			app.EnterText(x => x.Marked("entPassword"), "password");
			app.Tap(x => x.Marked("btnLogin"));

			app.WaitForNoElement(x => x.Marked("indIsBusy")); 
			app.WaitForElement(x => x.Marked("Perfil"));

			var profileElement = app.Query(x => x.Marked("Perfil")); 
			Assert.IsTrue(profileElement.Any());
		}
	}
}
