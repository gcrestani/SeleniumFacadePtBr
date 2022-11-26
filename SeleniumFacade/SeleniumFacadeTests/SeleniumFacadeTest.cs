
using OpenQA.Selenium;
using SeleniumFacade;

namespace SeleniumFacadeTests
{
  public class SeleniumFacadeTest : IDisposable
  {
    public IWebDriver driver;
    public string siteTeste;

    public SeleniumFacadeTest()
    {
      var solutionDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
      siteTeste = $"{solutionDir}\\Web\\seleniumTests.html";
    }

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
      SeleniumFacadePtBr.navegarPara(driver, siteTeste);

      //Act
      SeleniumFacadePtBr.enviarEnterSemElemento(driver);

      //Assert
      var textoDoElemento = SeleniumFacadePtBr.retornarTextoPorId(driver, "enterDetectado");
      Assert.Equal("Botão acionado", textoDoElemento);
    }


    [Fact(DisplayName = "retornarTextoPorId deve retornar o conteudo em texto de um elemento")]
    public void retornarTextoPorId()
    {
      //Arrange
      driver = SeleniumFacadePtBr.criaDriver();
      SeleniumFacadePtBr.navegarPara(driver, siteTeste);

      //Act
      var texto = SeleniumFacadePtBr.retornarTextoPorId(driver, "divComTexto");

      //Assert
      Assert.Equal("Texto dentro de uma div.", texto);
    }

    [Fact(DisplayName = "retornarTextoPorId deve retornar vazio para um elemento inexistente")]
    public void retornarTextoPorIdInexistente()
    {
      //Arrange
      driver = SeleniumFacadePtBr.criaDriver();
      SeleniumFacadePtBr.navegarPara(driver, siteTeste);

      //Act
      var texto = SeleniumFacadePtBr.retornarTextoPorId(driver, "idInexistente");

      //Assert
      Assert.Equal("", texto);
    }


    [Fact(DisplayName = "navegarPara deve acessar o site passado por parametro")]
    public void navegarPara()
    {
      //Arrange
      driver = SeleniumFacadePtBr.criaDriver();

      //Act
      SeleniumFacadePtBr.navegarPara(driver, siteTeste);

      //Assert
      var urlFormatada = driver.Url.Replace("file:///", "").Replace("/", "\\");
      Assert.Equal(siteTeste, urlFormatada);
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