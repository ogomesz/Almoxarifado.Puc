-- 1. CRIAÇÃO DO BANCO DE DADOS
DROP DATABASE IF EXISTS almoxarifado_db;
CREATE DATABASE almoxarifado_db;
USE almoxarifado_db;

-- 2. CRIAÇÃO DAS TABELAS

CREATE TABLE Categoria (
    id_categoria INT AUTO_INCREMENT PRIMARY KEY,
    nome_categoria VARCHAR(100) NOT NULL
);

CREATE TABLE Fornecedor (
    id_fornecedor INT AUTO_INCREMENT PRIMARY KEY,
    nome_fantasia VARCHAR(150) NOT NULL,
    cnpj VARCHAR(18) UNIQUE NOT NULL
);

CREATE TABLE Usuario (
    id_usuario INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    login VARCHAR(50) UNIQUE NOT NULL,
    senha VARCHAR(50) NOT NULL
);

CREATE TABLE Produto (
    codigo_id INT PRIMARY KEY,
    nome_produto VARCHAR(150) NOT NULL,
    descricao VARCHAR(255), -- NOVA COLUNA AQUI!
    id_categoria INT,
    id_fornecedor INT,
    quantidade_estoque INT DEFAULT 0,
    FOREIGN KEY (id_categoria) REFERENCES Categoria(id_categoria),
    FOREIGN KEY (id_fornecedor) REFERENCES Fornecedor(id_fornecedor)
);

CREATE TABLE Movimentacao (
    id_movimentacao INT AUTO_INCREMENT PRIMARY KEY,
    quantidade INT NOT NULL,
    tipo ENUM('ENTRADA', 'SAIDA') NOT NULL,
    data_hora DATETIME DEFAULT CURRENT_TIMESTAMP,
    codigo_id INT NOT NULL,
    id_usuario INT NOT NULL,
    FOREIGN KEY (codigo_id) REFERENCES Produto(codigo_id),
    FOREIGN KEY (id_usuario) REFERENCES Usuario(id_usuario)
);

-- =======================================================
-- 3. MASSA DE DADOS PARA TESTES
-- =======================================================

-- Inserindo Categorias Padrão
INSERT INTO Categoria (nome_categoria) VALUES 
('Informática'),
('Papelaria'),
('Limpeza'),
('Operações'),
('Jardinagem');

-- Inserindo Fornecedores Padrão
INSERT INTO Fornecedor (nome_fantasia, cnpj) VALUES 
('Port', '11.111.111/0001-11'),
('Dell', '22.222.222/0001-22'),
('BrasPrint', '33.333.333/0001-33'),
('Minas Ferramentas', '44.444.444/0001-44'),
('Mercado Livre', '55.555.555/0001-55'),
('Climpo', '66.666.666/0001-66');

-- Inserindo Usuários Padrão
INSERT INTO Usuario (nome, login, senha) VALUES 
('Administrador', 'admin', '123'),
('João Silva', 'joao.silva', 'senha123');

-- Inserindo Produtos Iniciais
INSERT INTO Produto (codigo_id, nome_produto, id_categoria, id_fornecedor, quantidade_estoque) VALUES 
(101, 'Mouse sem fio', 1, 2, 150),
(102, 'Teclado Mecânico', 1, 2, 80),
(201, 'Caderno Universitário', 2, 1, 200),
(202, 'Caixa de Caneta Azul', 2, 3, 60),
(301, 'Desinfetante 5L', 3, 6, 30);

-- Inserindo Histórico Inicial
INSERT INTO Movimentacao (codigo_id, id_usuario, quantidade, tipo, data_hora) VALUES 
(101, 1, 150, 'ENTRADA', '2026-05-10 09:00:00'),
(102, 1, 80, 'ENTRADA', '2026-05-10 09:15:00'),
(201, 2, 200, 'ENTRADA', '2026-05-11 10:30:00'),
(101, 2, 5, 'SAIDA', '2026-05-15 14:00:00');
