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
			// create the required capabilities
			DesiredCapabilities capabilities = new DesiredCapabilities();
			capabilities.SetCapability("appium-version", "1.5.3");

			capabilities.SetCapability("browserName", "iPad Air");
			Platform macPlatform = new Platform(PlatformType.Mac);
			capabilities.SetCapability("platform", macPlatform.ToString());
			capabilities.SetCapability("platformName", "iOS");
			capabilities.SetCapability("platformVersion", "9.3");
			capabilities.SetCapability("deviceName", "iPad Air");

			capabilities.SetCapability("keepKeyChains", true);
			capabilities.SetCapability("noReset", true);
			capabilities.SetCapability("launchTimeout", 120000); // 2min launch time out

			capabilities.SetCapability("app", "/Users/macbuildserver/Public/001_xcode_share/development/EMS_share/Debug/EMS.ipa");

			// selenium grid url
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
			//if (!Env.isSauce())
			//{
			//	AppiumServers.StopLocalService();
			//}
		}

		[Test]
		public void LoginTest()
		{
			Console.WriteLine("iOS Login Test");

			driver.FindElement(By.XPath("//UIAApplication[1]/UIAWindow[1]/UIAScrollView[1]/UIATextField[1]")).SetImmediateValue("peliu3p1");
			driver.FindElement(By.XPath("//UIAApplication[1]/UIAWindow[1]/UIAScrollView[1]/UIASecureTextField[1]")).SetImmediateValue("hSoRz3wo");
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
