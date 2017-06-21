using System;
using TechTalk.SpecFlow;
using Xamarin.UITest;
using Should;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest.Queries;

namespace TodoSampleMobile.UITest
{
	[Binding]
	public class CommonSteps
	{
		readonly IHomeScreen homeScreen;
		readonly ILoginScreen loginScreen;
		readonly IApp app;

		public CommonSteps ()
		{
			//app = FeatureContext.Current.Get<IApp>("App");
		    app = AppInitializer
		        .StartApp(Platform.iOS, "", false);
			homeScreen = FeatureContext.Current.Get<IHomeScreen> (ScreenNames.Home);
			loginScreen = FeatureContext.Current.Get<ILoginScreen> (ScreenNames.Login);
		}

		[Given (@"I am on the Login screen")]
		public void GivenIAmOnTheLoginScreen ()
		{
			app.Screenshot ("Given I am on the Login screen");
		}

		//[Given(@"I have at least one existing task named ""(.*)""")]
		//public void GivenIHaveAtLeastOneExistingTask(string taskName)
		//{
		//	app.WaitForElement (homeScreen.addButton);
		//	var count = app.Query (c => c.Marked (taskName)).Count ();
		//	if (count ==    0) {
		//		app.Screenshot ("Give I have at least one existing task named '" + taskName + "'");
		//		app.Tap (homeScreen.addButton);

		//		app.WaitForElement (addTaskScreen.nameEntry);
		//		app.EnterText (addTaskScreen.nameEntry, taskName);
		//		app.Tap (`Screen.saveButton);
		//		app.WaitForElement (c => c.Marked (taskName));
		//	}
		//	app.Screenshot ("Give I have at least one existing task named '" + taskName + "'");

		//	app.Query(c => c.Marked(taskName)).Count().ShouldBeGreaterThanOrEqualTo(1);
		//}

		//[When (@"I add a new task called ""(.*)""")]
		//public void WhenIAddANewTaskCalled (string taskName)
		//{
		//	app.WaitForElement (homeScreen.addButton);
		//	app.Tap (homeScreen.addButton);
		//	app.Screenshot ("When I add a new task called '" + taskName + "'");
		//	app.WaitForElement (addTaskScreen.nameEntry);
		//	app.EnterText (addTaskScreen.nameEntry, taskName);
		//	app.Screenshot ("When I add a new task called '" + taskName + "'");
		//}

		//[When (@"I save the task")]
		//public void WhenISaveTheTask ()
		//{
		//	app.Tap (addTaskScreen.saveButton);
		//	app.Screenshot ("When I save the task");
		//}

		[Then (@"I should see the ""(.*)"" task in the list")]
		public void ThenIShouldSeeTheTaskInTheList (string taskName)
		{
			app.WaitForElement (c => c.Marked (taskName));
			app.Query (c => c.Marked (taskName)).Length.ShouldBeGreaterThan (0);
			app.Screenshot ("Then I should see the '" + taskName + "' task in the list");
		}

        [Then(@"I should see a button named ""(.*)""")]
        public void ThenIShouldSeeaButtonNamed(string buttonName)
        {
            app.WaitForElement(c => c.Marked(buttonName));
            app.Query(c => c.Marked(buttonName)).Length.ShouldBeGreaterThan(0);
            app.Screenshot("Then I should see the '" + buttonName + "' task in the list");
        }

        [Then(@"The button named ""(.*)"" should have the color Hex code ""(.*)""")]
        public void ThenIShouldSeeAButtonWithTheColor(string buttonName,string colorHex)
        {
            app.WaitForElement(c => c.Marked(buttonName));
            app.Query(c => c.Marked(buttonName)).Length.ShouldBeGreaterThan(0);
            app.WaitForElement(loginScreen.loginButton);
            var backgroundColor = app.Query(c => c.Button(buttonName).Invoke("getBackground").Invoke("getColor"));
            Assert.AreEqual(backgroundColor,colorHex);
            app.Screenshot("Then The button named " + buttonName + " should have the color Hex code "+colorHex);
        }

        [When(@"I select the task named ""(.*)""")]
		public void WhenISelectTheTaskNamed(string taskName)
		{
			app.Tap(c => c.Marked(taskName));
			app.Screenshot ("When I select the task named '" + taskName + "'");
		}
	}

}