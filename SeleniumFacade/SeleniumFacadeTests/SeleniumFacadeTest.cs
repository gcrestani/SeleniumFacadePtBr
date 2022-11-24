
using OpenQA.Selenium;
using SeleniumFacade;

namespace SeleniumFacadeTests
{
  public class SeleniumFacadeTest : IDisposable
  {
    public IWebDriver driver;
    [Fact (DisplayName ="criaDriver deve retornar um driver criado")]
    public void criaDriver()
    {
      driver = SeleniumFacadePtBr.criaDriver();
      Assert.NotNull(driver);
    }

    public void Dispose()
    {
      SeleniumFacadePtBr.fechaDriver(driver);
    }
  }
}