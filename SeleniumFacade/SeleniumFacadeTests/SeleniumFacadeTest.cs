
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
      //Arrange
      //Act
      driver = SeleniumFacadePtBr.criaDriver();

      //Assert
      Assert.NotNull(driver);
    }

    [Fact(DisplayName = "fechaDriver deve fechar o driver")]
    public void fechaDriver()
    {
      //Arrange
      driver = SeleniumFacadePtBr.criaDriver();

      //Act
      SeleniumFacadePtBr.fechaDriver(driver);

      //Assert
      Assert.Throws<ObjectDisposedException>(() => SeleniumFacadePtBr.fechaDriver(driver));
    }


    [Fact(DisplayName = "enviarEnterSemElemento deve pressionar a tecla enter sem selecionar nenhum elemento")]
    public void enviarEnterSemElemento()
    {
      //Arrange
      driver = SeleniumFacadePtBr.criaDriver();
      var solutionDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
      SeleniumFacadePtBr.navegarPara(driver, @"{solutionDir}/Web/enviarEnterSemElemento.html");

      //Act
      SeleniumFacadePtBr.enviarEnterSemElemento(driver);

      //Assert
      var textoDoElemento = SeleniumFacadePtBr.retornarTextoPorId(driver, "enterDetectado");
      Assert.Equal("Botão acionado", textoDoElemento);
    }

    

    public void Dispose()
    {
      try
      {
        SeleniumFacadePtBr.fechaDriver(driver);
      }
      catch (Exception)
      {
        //Ja fechado
      }
    }
  }
}