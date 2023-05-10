# Tabela para administração de funcionários e projetos
Aplicação WPF com fins didáticos para administração de banco de dados de funcionários e projetos de uma empresa. É possível inserir novos dados, atualizar e remover. 

### Pré-requisitos

Antes de começar, você vai precisar ter instalado em sua máquina o banco de dados PostgresSQL e atualizar a string de conexão com os dados da sua máquina. Criar as seguintes tabelas: 
```
CREATE TABLE Projeto
(
    nome VARCHAR(200) PRIMARY KEY,
    dataInicio TIMESTAMP NOT NULL,
    dataFim TIMESTAMP NOT NULL,
    observacao VARCHAR(200)
);

CREATE TABLE Funcionario
(
    cpf VARCHAR(14) PRIMARY KEY,
    nomefunc VARCHAR(200) NOT NULL,
    salario decimal NOT NULL,
    departamento VARCHAR(200) NOT NULL,
    projeto1 VARCHAR(200),
    projeto2 VARCHAR(200),
    projeto1_nome VARCHAR(200) REFERENCES Projeto(nome) ON DELETE SET NULL,
    projeto2_nome VARCHAR(200) REFERENCES Projeto(nome) ON DELETE SET NULL,
    CONSTRAINT cpf_length CHECK (char_length(cpf) = 14)
);

```
Dentro do Visual Studio é necessário instalar os paccotes NuGets e apertar F5.

### Visualização


![todos](https://user-images.githubusercontent.com/63231569/236639173-0a935fab-74b2-43e4-9330-432f77614e52.PNG)

![funcionario](https://user-images.githubusercontent.com/63231569/236639179-52e05dff-a235-486b-acab-5c3d2a5d1998.PNG)

![adcfunc](https://user-images.githubusercontent.com/63231569/236639224-191a29bf-da6e-49d7-b654-d8f46531a518.PNG)

![editarfunc](https://user-images.githubusercontent.com/63231569/236639191-4af974a5-e17f-45a7-8245-d846585506a2.PNG)
