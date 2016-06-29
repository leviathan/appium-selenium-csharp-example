using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using NUnit.Framework;

namespace AppiumTests
{
	[TestFixture]
	public class iOSLoginTest
	{
		private AppiumDriver<IOSElement> driver;

		[SetUp]
		public void beforeAll()
		{
			// Create the required capabilities for connecting with the Appium Selenium test runner
			DesiredCapabilities capabilities = new DesiredCapabilities();

			// Extensive list of Appium capability flags can be found here: https://github.com/appium/appium/blob/master/docs/en/writing-running-appium/caps.md

			// Specify the Appium Version, which you are requesting from the Selenium runner
			capabilities.SetCapability("appium-version", "1.5.3");

			// iOS Specific settings for iOS Simulator --------------------------------------------------------------------

			// The name of the browser, which is used to configure the appium runner with the Selenium grid
			capabilities.SetCapability("browserName", "iPad Air");

			// Set platform to "Mac" in order to 
			Platform macPlatform = new Platform(PlatformType.Mac);
			capabilities.SetCapability("platform", macPlatform.ToString());

			// Which mobile OS platform to use, e.g. "iOS"
			capabilities.SetCapability("platformName", "iOS");

			// Mobile OS version (The requested version must be registered with the Selenium Grid)
			capabilities.SetCapability("platformVersion", "9.3");

			// The kind of mobile device or emulator to use (The device name must be registered with the Selenium Grid)
			capabilities.SetCapability("deviceName", "iPad Air");

			// (Simulator-only) Whether to keep keychains (Library/Keychains) when appium session is started/finished
			capabilities.SetCapability("keepKeyChains", true);

			// Don't reset app state before this session. (default: false)
			capabilities.SetCapability("noReset", true);

			// Amount of time in ms to wait for instruments before assuming it hung and failing the session
			capabilities.SetCapability("launchTimeout", 120000); // 2min launch time out

			// ------------------------------------------------------------------------------------------------------------

			// Dynamic environment identifier, which is used by the Xcode build agent 
			// Possible values: "development", "production"
			String tfsServerEnvironment = "development";

			// Possbible values: "Debug", "Release"
			String appConfiguration = "Debug";

			// Tell the Appium runner where to find the IPA, which should be loaded into the iOS simulator when the tests are run.
			capabilities.SetCapability("app", "/Users/macbuildserver/Public/001_xcode_share/" + tfsServerEnvironment + "/EMS_share/" + appConfiguration + "/EMS.ipa");

			// Selenium grid url
			Uri serverUri = new Uri("http://10.127.141.71:4433/wd/hub");

			Console.WriteLine("Server Uri: " + serverUri.ToString());

			try
			{
				driver = new IOSDriver<IOSElement>(serverUri, capabilities);
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception: " + e.ToString());
			}
		}

		[TearDown]
		public void afterAll()
		{
			Console.WriteLine("After All");

			if (driver != null)
			{
				driver.Quit();
			}
		}

		[Test]
		public void LoginTest()
		{
			Console.WriteLine("iOS Login Test");

			driver.FindElement(By.XPath("//UIAApplication[1]/UIAWindow[1]/UIAScrollView[1]/UIATextField[1]")).SetImmediateValue("xxxx-user-xxxx");
			driver.FindElement(By.XPath("//UIAApplication[1]/UIAWindow[1]/UIAScrollView[1]/UIASecureTextField[1]")).SetImmediateValue("xxxx-pass-xxxx");
			driver.FindElement(By.XPath("//UIAApplication[1]/UIAWindow[1]/UIAScrollView[1]/UIAButton[1]")).Click();
		}
	}

	public class MainTest
	{
		static void Main(string[] args)
		{
			Console.Write("Main...");
		}
	}
}
