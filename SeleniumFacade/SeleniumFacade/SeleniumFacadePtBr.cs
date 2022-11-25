using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumFacade
{
  public class SeleniumFacadePtBr
  {
    public static IWebDriver criaDriver(bool invisivel = false, bool permitirDownload = false, string pastaDownload = "")
    {
      IWebDriver driver;
      try
      {
        var chromeOptions = new ChromeOptions();
        chromeOptions.AddArguments("--start-maximized");
        chromeOptions.AddUserProfilePreference("download.default_directory", Application.StartupPath.ToString());
        ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();

        if (invisivel)
        {
          chromeOptions.AddArguments("headless");
          chromeDriverService.HideCommandPromptWindow = true;
        }
        driver = new ChromeDriver(chromeOptions);


        if (permitirDownload)
        {
          var enableDownloadCommandParameters = new Dictionary<string, object>
                {
                    { "behavior", "allow" },
                    { "downloadPath", Application.StartupPath.ToString() + pastaDownload}
                };
          var result = ((OpenQA.Selenium.Chrome.ChromeDriver)driver).ExecuteCdpCommand("Page.setDownloadBehavior", enableDownloadCommandParameters);
        }
      }
      catch (Exception)
      {
        //log
        throw;
      }
      return driver;
    }

    public static void fechaDriver(IWebDriver driver)
    {
        driver.Close();
        driver.Quit();
    }
  }
}
