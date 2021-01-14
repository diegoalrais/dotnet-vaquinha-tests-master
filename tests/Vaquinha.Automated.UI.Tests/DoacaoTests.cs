using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Vaquinha.Tests.Common.Fixtures;
using Xunit;

namespace Vaquinha.AutomatedUITests
{
	public class DoacaoTests : IDisposable, IClassFixture<DoacaoFixture>, 
                                               IClassFixture<EnderecoFixture>, 
                                               IClassFixture<CartaoCreditoFixture>
	{
		private DriverFactory _driverFactory = new DriverFactory();
		private IWebDriver _driver;

		private readonly DoacaoFixture _doacaoFixture;
		private readonly EnderecoFixture _enderecoFixture;
		private readonly CartaoCreditoFixture _cartaoCreditoFixture;

		public DoacaoTests(DoacaoFixture doacaoFixture, EnderecoFixture enderecoFixture, CartaoCreditoFixture cartaoCreditoFixture)
        {
            _doacaoFixture = doacaoFixture;
            _enderecoFixture = enderecoFixture;
            _cartaoCreditoFixture = cartaoCreditoFixture;
        }
		public void Dispose()
		{
			_driverFactory.Close();
		}

		[Fact]
		public void DoacaoUI_AcessoTelaHome()
		{
			// Arrange
			_driverFactory.NavigateToUrl("https://vaquinha.azurewebsites.net/");
			_driver = _driverFactory.GetWebDriver();

			// Act
			IWebElement webElement = null;
			webElement = _driver.FindElement(By.ClassName("yellow"));
			webElement.Click();
			
			IWebElement campoNome = _driver.FindElement(By.Id("DadosPessoais_Nome"));
			campoNome.SendKeys(doacao.DadosPessoais.Nome);

			IWebElement campoEmail = _driver.FindElement(By.Id("DadosPessoais_Email"));
			campoEmail.SendKeys(doacao.DadosPessoais.Email);

			IWebElement campoMensagem = _driver.FindElement(By.Id("DadosPessoais_Mensagem"));
			campoMensagem.SendKeys(doacao.DadosPessoais.Mensagem);

			IWebElement campoEndereco = _driver.FindElement(By.Id("Endereco_TextoEndereco"));
			campoTextoEndereco.SendKeys(doacao.Endereco.TextoEndereco);

			IWebElement campoNumero = _driver.FindElement(By.Id("Endereco_Numero"));
			campoNumero.SendKeys(doacao.Endereco.Numero);

			IWebElement campoComplemento = _driver.FindElement(By.Id("Endereco_Complemento"));
			campoComplemento.SendKeys(doacao.Endereco.Complemento);
			
			IWebElement campoCidade= _driver.FindElement(By.Id("Endereco_Cidade));
			campoCidade.SendKeys(doacao.Endereco.Cidade);

			IWebElement campoEstado = _driver.FindElement(By.Id("Endereco_Estado"));
			campoEstado.SendKeys(doacao.Endereco.Estado);

			IWebElement campoTelefone = _driver.FindElement(By.Id("Endereco_Telefone"));
			campoTelefone.SendKeys(doacao.Endereco.Telefone);

			IWebElement campoNomeTitular = _driver.FindElement(By.Id("CartaoCredito_NomeTitular"));
			campoNomeTitular.SendKeys(doacao.Endereco.NomeTitular);
			
			IWebElement campoNumeroCartaoCredito = _driver.FindElement(By.Id("CartaoCredito_NumeroCartaoCredito"));
			campoNumeroCartaoCredito.SendKeys(doacao.Endereco.NumeroCartaoCredito);
			
			IWebElement campoValidade = _driver.FindElement(By.Id("CartaoCredito_Validade"));
			campoValidade.SendKeys(doacao.Endereco.Validade);

			IWebElement campoCVV = _driver.FindElement(By.Id("CartaoCredito_CVV"));
			campoCVV.SendKeys(doacao.Endereco.CVV);
			

			// Assert
			webElement.Displayed.Should().BeTrue(because:"logo exibido");
		}
		[Fact]
		public void DoacaoUI_CriacaoDoacao()
		{
			//Arrange
			var doacao = _doacaoFixture.DoacaoValida();
            doacao.AdicionarEnderecoCobranca(_enderecoFixture.EnderecoValido());
            doacao.AdicionarFormaPagamento(_cartaoCreditoFixture.CartaoCreditoValido());
			_driverFactory.NavigateToUrl("https://vaquinha.azurewebsites.net/");
			_driver = _driverFactory.GetWebDriver();

			//Act
			IWebElement webElement = null;
			webElement = _driver.FindElement(By.ClassName("btn-yellow"));
			webElement.Click();

			//Assert
			_driver.Url.Should().Contain("/Home/Index");
		}
	}
}