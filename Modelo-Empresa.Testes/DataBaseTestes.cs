using Modelo_Empresa.DataBase;
using Modelo_Empresa.Models;
using Moq;

namespace Modelo_Empresa.Testes
{
    public class DataBaseTestes
    {
        
        [Fact]
        public void TestListarFuncionarios()
        {
            // Arrange
            var mockDataBase = new Mock<IDataBase>();
            var funcionariosEsperados = new List<FuncionarioModel>
        {
            new FuncionarioModel { Cpf = "123456789", Nome = "Marcos",
                Salario = 1000, Departamento = "Departamento 1",
                Projeto1 = "Projeto 1", Projeto2 = "Projeto 2" },
            new FuncionarioModel { Cpf = "987654321", Nome = "Paula",
                Salario = 2000, Departamento = "Departamento 2",
                Projeto1 = "Projeto 3", Projeto2 = "Projeto 4" }
        };
            mockDataBase.Setup(db => db.ListarFuncionarios()).Returns(funcionariosEsperados);
            var dataBase = mockDataBase.Object;

            // Act
            var funcionariosObtidos = dataBase.ListarFuncionarios();

            // Assert
            Assert.Equal(funcionariosEsperados, funcionariosObtidos);
        }

        [Fact]
        public void TestAdicionarFuncionario()
        {
            // Arrange
            var novoFuncionario = new FuncionarioModel("12345678901", "João", 2000, "RH", "Projeto 1", "Projeto 2");

            var mockDataBase = new Mock<IDataBase>();
            mockDataBase.Setup(db => db.AdicionarFuncionario(novoFuncionario)).Returns(true);
            var dataBase = mockDataBase.Object;

            // Act
            var resultado = dataBase.AdicionarFuncionario(novoFuncionario);

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public void TestAtualizarFuncionario()
        {
            // Arrange
            var funcionarioExistente = new FuncionarioModel("12345678901", "João", 2000, "RH", "Projeto A", "Projeto B");
            var funcionarioAtualizado = new FuncionarioModel("12345678901", "João Silva", 2500, "TI", "Projeto B", "Projeto C");

            var mockDataBase = new Mock<IDataBase>();
            mockDataBase.Setup(db => db.AtualizarFuncionario(funcionarioAtualizado)).Returns(true);
            var dataBase = mockDataBase.Object;

            // Act
            var resultado = dataBase.AtualizarFuncionario(funcionarioAtualizado);

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public void TestRemoverFuncionario()
        {
            // Arrange
            var funcionarioExistente = new FuncionarioModel("12345678901", "João", 2000, "RH", "Projeto A", "Projeto B");

            var mockDataBase = new Mock<IDataBase>();
            mockDataBase.Setup(db => db.RemoverFuncionario(funcionarioExistente)).Returns(true);
            var dataBase = mockDataBase.Object;

            // Act
            var resultado = dataBase.RemoverFuncionario(funcionarioExistente);

            // Assert
            Assert.True(resultado);
        }
        [Fact]
        public void TestAdicionarProjeto()
        {
            // Arrange
            var novoProjeto = new ProjetoModel("Projeto A", DateTime.Now, DateTime.Now.AddDays(30), "Observação do projeto A");

            var mockDataBase = new Mock<IDataBase>();
            mockDataBase.Setup(db => db.AdicionarProjeto(novoProjeto)).Returns(true);
            var dataBase = mockDataBase.Object;

            // Act
            var resultado = dataBase.AdicionarProjeto(novoProjeto);

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public void TestAtualizarProjeto()
        {
            // Arrange
            var projetoExistente = new ProjetoModel("Projeto A", DateTime.Now, DateTime.Now.AddDays(30), "Observação do projeto A");
            var projetoAtualizado = new ProjetoModel("Projeto A", DateTime.Now, DateTime.Now.AddDays(60), "Observação atualizada do projeto A");

            var mockDataBase = new Mock<IDataBase>();
            mockDataBase.Setup(db => db.AtualizarProjeto(projetoAtualizado)).Returns(true);
            var dataBase = mockDataBase.Object;

            // Act
            var resultado = dataBase.AtualizarProjeto(projetoAtualizado);

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public void TestRemoverProjeto()
        {
            // Arrange
            var projetoExistente = new ProjetoModel("Projeto A", DateTime.Now, DateTime.Now.AddDays(30), "Observação do projeto A");

            var mockDataBase = new Mock<IDataBase>();
            mockDataBase.Setup(db => db.RemoverProjeto(projetoExistente)).Returns(true);
            var dataBase = mockDataBase.Object;

            // Act
            var resultado = dataBase.RemoverProjeto(projetoExistente);

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public void TestListarProjetos()
        {
            // Arrange
            var listaProjetos = new List<ProjetoModel>()
            {
                new ProjetoModel("Projeto A", DateTime.Now, DateTime.Now.AddDays(30), "Observação do projeto A"),
                new ProjetoModel("Projeto B", DateTime.Now, DateTime.Now.AddDays(60), "Observação do projeto B"),
                new ProjetoModel("Projeto C", DateTime.Now, DateTime.Now.AddDays(90), "Observação do projeto C")
            };

            var mockDataBase = new Mock<IDataBase>();
            mockDataBase.Setup(db => db.ListarProjetos()).Returns(listaProjetos);
            var dataBase = mockDataBase.Object;

            // Act
            var resultado = dataBase.ListarProjetos();

            // Assert
            Assert.Equal(listaProjetos, resultado);
        }




    }
}