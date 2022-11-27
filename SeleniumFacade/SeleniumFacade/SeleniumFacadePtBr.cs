using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
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
    public static object retornarTextoPorId(IWebDriver driver, string id)
    {
      try
      {
        return driver.FindElement(By.Id(id)).Text;
      }
      catch (System.Exception)
      {
        return "";
      }
    }
    public static bool navegarPara(IWebDriver driver, string url)
    {
      driver.Navigate().GoToUrl(url);
      new WebDriverWait(driver, new TimeSpan(0, 0, 3)).Until(
      d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
      return true;
    }
    public static void preencherTextPorId(IWebDriver driver, string texto, string id)
    {
      driver.FindElement(By.Id(id)).SendKeys(texto);
    }

    public static string retornarValuePorId(IWebDriver driver, string id)
    {
      var elemento = driver.FindElement(By.Id(id));
      return elemento.GetAttribute("value");
    }
    public static void limpaTextPorId(IWebDriver driver, string id)
    {
      driver.FindElement(By.Id(id)).Clear();
    }
  }
}

/*

namespace NixPDC
{
  class SeleniumMetodos
  {

    public static void clickTextoPorLink(IWebDriver driver, string link)
    {
      driver.FindElement(By.LinkText(link)).Click(); //PartialLinkText
    }

    public static string retornarTextoPorTag(IWebDriver driver, string tag)
    {
      return driver.FindElement(By.TagName(tag)).Text;
    }
    public static List<IWebElement> retornarTodosTextoPorTag(IWebDriver driver, string tag)
    {
      return driver.FindElements(By.TagName(tag)).ToList();
    }

    public static List<IWebElement> retornarTodosTextoPorClasse(IWebDriver driver, string classe)
    {
      return driver.FindElements(By.ClassName(classe)).ToList();
    }
    public static string retornarTextoPorClasse(IWebDriver driver, string classe)
    {
      return driver.FindElement(By.ClassName(classe)).Text;
    }
    public static string retornarTextoPorId(IWebDriver driver, string id)
    {
      return driver.FindElement(By.Id(id)).Text;
    }

    public static string retornarValuePorName(IWebDriver driver, string tag)
    {
      var elemento = driver.FindElement(By.TagName(tag));
      return elemento.GetAttribute("value");
    }

    public static void preencherTextPorId(IWebDriver driver, string texto, string id)
    {
      driver.FindElement(By.Id(id)).SendKeys(texto);
    }

    public static void preencherTextPorName(IWebDriver driver, string texto, string name)
    {
      driver.FindElement(By.Name(name)).SendKeys(texto);
    }
    public static void preencherTextPorClasse(IWebDriver driver, string texto, string classe)
    {
      driver.FindElement(By.ClassName(classe)).SendKeys(texto);
    }
    public static void clickPorId(IWebDriver driver, string id)
    {
      driver.FindElement(By.Id(id)).Click();
    }

    public static void clickPorName(IWebDriver driver, string name)
    {
      driver.FindElement(By.Name(name)).Click();
    }

    public static void clickPorClasse(IWebDriver driver, string classe)
    {
      driver.FindElement(By.ClassName(classe)).Click();
    }
    public static void clickPorTexto(IWebDriver driver, string texto, int numEle = 0)
    {
      var obj = driver.FindElements(By.XPath("//*[text()='" + texto + "']"))[numEle];
      obj.Click();
    }
    public static void clickPorXPATH(IWebDriver driver, string xpath, int numEle = 0)
    {
      var obj = driver.FindElement(By.XPath(xpath));
      obj.Click();
    }

    public static void rClickPorXPATH(IWebDriver driver, string xpath, int numEle = 0)
    {
      Actions actions = new Actions(driver);
      var obj = driver.FindElement(By.XPath(xpath));
      actions.MoveToElement(driver.FindElement(By.XPath(xpath)))
          .ContextClick()
          .Build()
          .Perform();
    }

    public static void preencherTextPorXPATH(IWebDriver driver, string xpath, string texto, int numEle = 0, int pausarAnterDoEnterMS = 500, bool enviarEnter = false)
    {
      driver.FindElements(By.XPath(xpath))[numEle].SendKeys(texto);
      if (enviarEnter)
      {
        FuncoesUteis.pausa(pausarAnterDoEnterMS);
        driver.FindElements(By.XPath(xpath))[numEle].SendKeys(Keys.Return);
      }
    }

    public static void back(IWebDriver driver)
    {
      driver.Navigate().Back();
    }
    public static void clickSubmit(IWebDriver driver)
    {
      driver.FindElement(By.CssSelector("input[type='submit']")).Click();
      new WebDriverWait(driver, new TimeSpan(0, 0, 03)).Until(
      d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
    }

    internal static void fecharDriver(IWebDriver driver)
    {
      driver.Close();
      driver.Quit();
    }

    internal static void selecionarSelectPorIdEValue(IWebDriver driver, string id, string value)
    {
      var select = driver.FindElement(By.Id(id));
      var selectElement = new SelectElement(select);
      selectElement.SelectByValue(value);
    }
    internal static void executaJS(IWebDriver driver, string codigoJS)
    {
      IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
      js.ExecuteScript(codigoJS);
    }

    internal static void executaScrollXpath(IWebDriver driver, string XPATH)
    {
      var select = driver.FindElement(By.XPath(XPATH));
      IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
      js.ExecuteScript("arguments[0].scrollBy(0,-350)", "XPATH");
    }

    internal static void alterarValueInputPorId(IWebDriver driver, string id, string value)
    {
      IWebElement element = driver.FindElement(By.Id(id));
      IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
      jse.ExecuteScript("arguments[0].value='" + value + "';", element);
    }

    internal static void alterarValueInputPorClasse(IWebDriver driver, string classe, string value)
    {
      IWebElement element = driver.FindElement(By.ClassName(classe));
      IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
      jse.ExecuteScript("arguments[0].value='" + value + "';", element);
    }

    internal static void alterarValueInputPorName(IWebDriver driver, string name, string value)
    {
      IWebElement element = driver.FindElement(By.Name(name));
      IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
      jse.ExecuteScript("arguments[0].value='" + value + "';", element);
    }

    internal static void limparCampoPorIdTeclado(IWebDriver driver, string id)
    {
      driver.FindElement(By.Id(id)).SendKeys(Keys.Control + "a");
      driver.FindElement(By.Id(id)).SendKeys(Keys.Delete);
    }

    internal static void limparCampoPorIdClear(IWebDriver driver, string id)
    {
      driver.FindElement(By.Id(id)).Clear();
    }

    internal static void limparCampoPorName(IWebDriver driver, string name)
    {
      driver.FindElement(By.Name(name)).Clear();
      driver.FindElement(By.Name(name)).SendKeys(Keys.Control + "a");
      driver.FindElement(By.Name(name)).SendKeys(Keys.Delete);
    }

    public static bool verificarSeExistePorClasse(IWebDriver driver, string classe)
    {
      try
      {
        driver.FindElement(By.ClassName(classe));
        return true;
      }
      catch (System.Exception e)
      {
        return false;
      }
    }

    public static bool verificarSeExistePorId(IWebDriver driver, string id)
    {
      bool existe = driver.FindElement(By.Id(id)).Displayed;
      return existe;
    }

    internal static void aceitaAlert(IWebDriver driver, int timeOutSeg = 20)
    {
      try
      {
        driver.SwitchTo().Alert().Accept();
      }
      catch (System.Exception e)
      {
        bool alertEncontrado = false;
        int tempoPassado = 0;
        while (!alertEncontrado)
        {
          FuncoesUteis.pausa(1000);
          tempoPassado++;
          if (tempoPassado <= timeOutSeg)
          {
            try
            {
              driver.SwitchTo().Alert().Accept();
              alertEncontrado = true;
            }
            catch (System.Exception f)
            { }
          }
          else
          {
            break;
          }
        }
      }
    }

    internal static void enviarEnterPorId(IWebDriver driver, string id)
    {
      driver.FindElement(By.Id(id)).SendKeys(Keys.Return);
    }
    internal static void enviarSetaPorId(IWebDriver driver, string id, int idSeta)
    {
      switch (idSeta)
      {
        case 1:
          driver.FindElement(By.Id(id)).SendKeys(Keys.ArrowUp);
          break;
        case 2:
          driver.FindElement(By.Id(id)).SendKeys(Keys.ArrowRight);
          break;
        case 3:
          driver.FindElement(By.Id(id)).SendKeys(Keys.ArrowDown);
          break;
        case 4:
          driver.FindElement(By.Id(id)).SendKeys(Keys.ArrowLeft);
          break;
        default:
          break;
      }
    }

    public static string retornarTextoElementoClasseNaoExclusiva(IWebDriver driver, string classe, int numeroDoElemento)
    {
      string texto = driver.FindElements(By.ClassName(classe))[numeroDoElemento].Text;
      return texto;
    }

    public static string retornarTextoElementoClasseNaoExclusivaPorTextContext(IWebDriver driver, string classe, int numeroDoElemento)
    {
      string texto = driver.FindElements(By.ClassName(classe))[numeroDoElemento].GetAttribute("textContent");
      return texto;
    }

    public static string retornaURL(IWebDriver driver)
    {
      return driver.Url;
    }

    public static string retornarTextoPorXPATH(IWebDriver driver, string xpath)
    {
      return driver.FindElement(By.XPath(xpath)).Text;
    }

    public static void moverAteElementoPorClasse(IWebDriver driver, string classe)
    {
      var element = driver.FindElement(By.ClassName(classe));
      Actions actions = new Actions(driver);
      actions.MoveToElement(element);
      actions.Perform();
    }
    public static void moverAteElementoPorXpath(IWebDriver driver, string xpath)
    {
      var element = driver.FindElement(By.XPath(xpath));
      Actions actions = new Actions(driver);
      actions.MoveToElement(element);
      actions.Perform();
    }

    public static string retornarHTMLPorClasse(IWebDriver driver, string classe)
    {
      return driver.FindElement(By.ClassName(classe)).GetAttribute("innerHTML");
    }

    public static string retornarHTMLPorCSSSelector(IWebDriver driver, string cssSelector)
    {
      return driver.FindElement(By.CssSelector(cssSelector)).GetAttribute("innerHTML");
    }

    internal static void clickRadioPorNamePorIndex(IWebDriver driver, string nome, int index)
    {
      IList<IWebElement> elements = driver.FindElements(By.Name(nome));
      elements[index].Click();
    }

    public static void mudarIframePorId(IWebDriver driver, string id)
    {
      driver.SwitchTo().Frame(id);
    }

    internal static int retornarQuantidadeDeElementosPorName(IWebDriver driver, string name)
    {
      IList<IWebElement> elements = driver.FindElements(By.Name(name));
      return elements.Count();
    }

    public static void mudarDeJanela(IWebDriver driver, int idJanela)
    {
      ReadOnlyCollection<string> windowHandles = driver.WindowHandles;
      driver.SwitchTo().Window(windowHandles[idJanela]);
    }

    public static void printDeJanela(IWebDriver driver, string url, string nomeDoArquivo)
    {
      driver.Url = url;
      ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(nomeDoArquivo + ".png", ScreenshotImageFormat.Png);
    }

    public static string retornarTextPorDataMd(IWebDriver driver, string dataMd)
    {
      return driver.FindElement(By.CssSelector($"[data-md='{dataMd}'")).GetAttribute("textContent");

    }
  }
} 
*/
