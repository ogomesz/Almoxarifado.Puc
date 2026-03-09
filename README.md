# 📦 Sistema de Gestão de Almoxarifado - Parte 1

Este projeto nasceu de uma observação prática no meu dia a dia de trabalho na **PUC Minas (Campus Betim)**. Percebi que o controle de estoque ainda seguia processos manuais (papel e planilhas isoladas), o que gerava lentidão e riscos de erro. Como estudante de Sistemas de Informação, decidi desenvolver esta aplicação em **C#** para automatizar e profissionalizar esse gerenciamento.

## 🚀 Status do Projeto: Parte 1 Concluída
Nesta primeira etapa, o foco foi a construção de uma base sólida utilizando os pilares da **Programação Orientada a Objetos (POO)** e uma arquitetura organizada e protegida.

---

## 🏗️ Arquitetura e Organização
O código foi dividido em camadas para facilitar a manutenção e futuras expansões (como a implementação de uma interface Web):

* **`Models/`**: Contém as entidades principais (`Produto` e `Movimentacao`). Utiliza modificadores de acesso `internal` e propriedades com `private set` para garantir o encapsulamento.
* **`Services/`**: Camada de lógica de negócio (`Estoque`), responsável por gerenciar as listas dinâmicas, realizar buscas e processar entradas/saídas.
* **`UI/`**: Interface de linha de comando (CLI) interativa, utilizando uma estrutura de loop `do-while` e `switch case`.

## 🛠️ Funcionalidades Implementadas
- [x] Cadastro de produtos com ID único.
- [x] Registro de entrada de mercadorias.
- [x] Registro de saída com validação de saldo insuficiente.
- [x] Histórico detalhado de movimentações (com data e hora).
- [x] Listagem geral de estoque.
- [x] Busca rápida de produtos por código.

## 📈 Roadmap de Evolução
- [ ] **Parte 2:** Implementação de tratamento de exceções (`try-catch` / `TryParse`) e validações de dados.
- [ ] **Parte 3:** Persistência de dados com Banco de Dados SQL.
- [ ] **Parte 4:** Desenvolvimento de interface Web (HTML, CSS e JavaScript) e Deploy.

---

## 💻 Tecnologias Utilizadas
* Linguagem: **C#**
* Plataforma: **.NET**
* Ambiente: **VS Code / Visual Studio**

---
**Desenvolvido por Eric Gomes Cordeiro** *Estudante de Sistemas de Informação - PUC Minas*
