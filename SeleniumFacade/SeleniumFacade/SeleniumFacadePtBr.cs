using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using Keys = OpenQA.Selenium.Keys;

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
        //log //todo
        throw;
      }
      return driver;
    }

    public static void navegarPara(IWebDriver driver, string v)
    {
      throw new NotImplementedException();
    }

    public static void fechaDriver(IWebDriver driver)
    {
        driver.Close();
        driver.Quit();
    }

    public static void enviarEnterSemElemento(IWebDriver driver)
    {
      Actions builder = new Actions(driver);
      builder.KeyDown(Keys.Return).Build().Perform();
      builder.KeyUp(Keys.Return).Build().Perform();
    }
    public static object retornarTextoPorId(IWebDriver driver, string v)
    {
      try
      {
        throw new NotImplementedException();
      }
      catch (Exception)
      {
        throw;
      }
      
    }
  }
}
