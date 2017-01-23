##### INSTRUÇÕES DE INSTALAÇÃO / CONFIGURAÇÃO #####

1. Baixar o projeto para sua máquina
2. Abrir a solution AvaliacaoWorkingHub no Visual Studio 2015
3. Editar a connection string presente no arquivo web.config, localizado na raiz do projeto AvaliacaoWorkingHub.Web, de acordo com seu ambiente
	a. Deve ser editada a linha 64
	b. Não alterar o parâmetro "initial catalog" da connection string
	c. Não alterar o atributo "name"
4. Rodar o script "CreateDatabase.sql" em sua instância do SQL SERVER. O arquivo pode ser localizado na raiz do projeto AvaliacaoWorkingHub.Data