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
			app.Repl();
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

		[Test]
		public void LoginWillFail()
		{
			app.EnterText(x => x.Marked("entPassword"), "Password67");
			app.Screenshot("Se introdujo un password: Password67");
			app.Tap(x => x.Marked("btnLogin"));
			app.Screenshot("Se presiono el inicio");
			app.WaitForNoElement("indIsBusy", "No aparecio error IsBusy");
			var errorDialog = app.Query("Error");
			Assert.IsTrue(errorDialog.Any(), "No se mostro el error");
		}

		[Test]
		public void CellWillBePressed()
		{
			LoginWillSuccess();
			app.Tap(x => x.Marked("entBirthday"));
			app.Screenshot("Tapped on view with class: EntryEditText marked: entBirthday");
			app.EnterText(x => x.Marked("entBirthday"), "12/12/1990");
			app.Tap(x => x.Marked("btnSaveProfile"));
			app.Screenshot("Se guardo el perfil");
			app.WaitForNoElement("indIsBusy", "No aparecio error IsBusy");
			if (platform == Platform.Android)
				app.TouchAndHold(x => x.Index(4));
			else
				app.SwipeRightToLeft(x => x.Index(4));
			app.Screenshot("Se hizo un long press en una celda");
			var deleteOption = app.Query("Eliminar");
			Assert.GreaterOrEqual(deleteOption.Count(), 1, "No hay opcion de eliminar");
		}
	}
}
