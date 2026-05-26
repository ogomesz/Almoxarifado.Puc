# 📦 Sistema de Gestão de Almoxarifado

Este projeto nasceu de uma observação prática no cotidiano de trabalho na PUC Minas (Campus Betim). A percepção de que o controle de estoque seguia processos manuais (papel e planilhas isoladas), gerando lentidão e riscos de inconsistência, motivou o desenvolvimento desta aplicação em C# como estudante de Sistemas de Informação, com o objetivo de automatizar, proteger e profissionalizar esse gerenciamento.

---

## 🚀 Status do Projeto: Partes 1, 2 e 3 Concluídas!

O software evoluiu de um protótipo em memória para uma aplicação de console robusta, com barreira de segurança, tratamento rigoroso de dados e persistência real utilizando banco de dados relacional.

---

## 🏗️ Arquitetura e Organização do Sistema

O código segue os princípios de separação de responsabilidades e forte encapsulamento da Programação Orientada a Objetos (POO), garantindo manutenibilidade e preparando o terreno para futuras expansões (como uma transição para Web API):

- **`Models/` (`Produto.cs`, `Movimentacao.cs`)**: Entidades principais do sistema. Utilizam modificadores de acesso `internal` e propriedades com `private set` para assegurar o encapsulamento estrito.
- **`Persistence/` (`ConexaoBD.cs`)**: Centraliza o ciclo de vida da conexão com o motor MySQL através do driver nativo (`MySql.Data`).
- **`Services/` (`Estoque.cs`)**: Camada de persistência de dados e regras de negócio. Substituiu o armazenamento em listas dinâmicas por execuções de comandos SQL nativos (CRUD, controle de transações lógicas de entrada/saída e histórico).
- **`UI/` (`Program.cs`)**: Interface de Linha de Comando (CLI) interativa. Gerencia as rotas do sistema via estruturas de repetição e barreiras de controle de fluxo.

---

## 🛠️ Funcionalidades Implementadas

### 🔒 Segurança e Acesso
- **Autenticação de Usuários**: Fluxo completo de login e criação de novas contas direto no terminal.
- **Sessão Protegida**: Vinculação automática do ID do usuário logado a cada movimentação de estoque para fins de auditoria.

### 📦 Gerenciamento de Estoque (CRUD no Banco de Dados)
- **Cadastro de Produtos**: Inserção com validação de ID único e integridade referencial com a tabela de categorias.
- **Busca Avançada**: Consulta rápida de itens por código identificador, realizando junções (`JOIN`) para trazer o nome textual da categoria.
- **Listagem Geral**: Relatório instantâneo de todos os produtos do almoxarifado direto do banco de dados.
- **Exclusão Física**: Remoção de registros do estoque com tratamento no banco.

### 📊 Fluxo de Movimentações
- **Registro de Entrada**: Incremento de saldo com geração automática de log histórico.
- **Registro de Saída com Validação**: Bloqueio de retiradas caso o saldo em estoque seja insuficiente, prevenindo inconsistências.
- **Histórico Auditável**: Relatório cronológico de todas as operações (`ENTRADA` / `SAIDA`), explicitando quantidade, produto e data/hora do evento.

### 🛡️ Robustez e Resiliência (Parte 2)
- **Tratamento de Exceções**: Uso de `try-catch` para capturar falhas críticas de infraestrutura (como queda de conexão com o banco).
- **Validação de Inputs**: Substituição de leituras inseguras por `int.TryParse`, blindando o sistema contra quebras caso o usuário digite letras em campos numéricos.

---

## 💾 Estrutura do Banco de Dados

A base de dados foi mapeada para o modelo físico relacional e conta com 5 tabelas interligadas:
1. `Categoria`: Organização de grupos de produtos.
2. `Setor`: Mapeamento dos setores da instituição.
3. `Usuario`: Armazenamento de credenciais e nomes dos operadores.
4. `Produto`: Registro de itens e saldos.
5. `Movimentacao`: Histórico detalhado de fluxo do almoxarifado.

---

## 🔧 Como Executar o Projeto

### Pré-requisitos
- .NET SDK (versão 6.0 ou superior)
- MySQL Server (rodando localmente na porta 3306)
- IDE de sua preferência (VS Code ou Visual Studio)

### Passos para Configuração
1. **Configurar o Banco de Dados**:
   - Abra o seu gerenciador MySQL (ex: MySQL Workbench).
   - Execute o script contido no arquivo `script_almoxarifado.sql` para criar a estrutura das tabelas e popular a massa de testes.

2. **Ajustar a Conexão (Se necessário)**:
   - Verifique os parâmetros de servidor, usuário e senha dentro da classe `ConexaoBD.cs`.

3. **Rodar a Aplicação**:
   - No terminal da pasta raiz do projeto, execute o comando:
     ```bash
     dotnet run
     ```
   - Para testar o primeiro acesso, utilize o login padrão:

     - **Login**: `Adm`
     - **Senha**: `123`

---

## 📈 Roadmap de Evolução

- [x] **Parte 1**: Estrutura orientada a objetos e fluxo CLI em memória.
- [x] **Parte 2**: Tratamento de exceções e validação de dados de entrada.
- [x] **Parte 3**: Persistência física com Banco de Dados SQL (MySQL).
- [ ] **Parte 4**: Consultas de conjuntos avançadas e refinamento da documentação.
- [ ] **Parte 5**: Transição da arquitetura para Web API (ASP.NET Core) e interface gráfica.
