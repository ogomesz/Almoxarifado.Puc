-- 1. CRIAÇÃO DO BANCO DE DADOS
DROP DATABASE IF EXISTS almoxarifado_db;
CREATE DATABASE almoxarifado_db;
USE almoxarifado_db;

-- 2. CRIAÇÃO DAS TABELAS (A estrutura física)

CREATE TABLE Categoria (
    id_categoria INT AUTO_INCREMENT PRIMARY KEY,
    nome_categoria VARCHAR(100) NOT NULL
);

CREATE TABLE Setor (
    id_setor INT AUTO_INCREMENT PRIMARY KEY,
    nome_setor VARCHAR(100) NOT NULL
);

CREATE TABLE Usuario (
    id_usuario INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    login VARCHAR(50) NOT NULL UNIQUE,
    senha VARCHAR(50) NOT NULL
);

CREATE TABLE Produto (
    codigo_id INT PRIMARY KEY,
    nome_produto VARCHAR(150) NOT NULL,
    id_categoria INT,
    quantidade_estoque INT DEFAULT 0,
    FOREIGN KEY (id_categoria) REFERENCES Categoria(id_categoria)
);

CREATE TABLE Movimentacao (
    id_movimentacao INT AUTO_INCREMENT PRIMARY KEY,
    codigo_id INT NOT NULL,
    id_usuario INT NOT NULL,
    quantidade INT NOT NULL,
    tipo ENUM('ENTRADA', 'SAIDA') NOT NULL,
    data_hora DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (codigo_id) REFERENCES Produto(codigo_id),
    FOREIGN KEY (id_usuario) REFERENCES Usuario(id_usuario)
);

-- =======================================================
-- 3. MASSA DE DADOS PARA TESTES (Rode para testar as consultas)
-- =======================================================

-- Inserindo Categorias Padrão
INSERT INTO Categoria (nome_categoria) VALUES 
('Informática'),
('Papelaria'),
('Limpeza'),
('Mobiliário');

-- Inserindo Setores
INSERT INTO Setor (nome_setor) VALUES 
('TI - Suporte'),
('Administrativo'),
('Recursos Humanos');

-- Inserindo Usuários Padrão (Senha sem criptografia conforme C#)
INSERT INTO Usuario (nome, login, senha) VALUES 
('Administrador', 'admin', '123'),
('João Silva', 'joao.silva', 'senha123'),
('Maria Souza', 'maria.souza', 'senha123');

-- Inserindo Produtos Fictícios
INSERT INTO Produto (codigo_id, nome_produto, id_categoria, quantidade_estoque) VALUES 
(101, 'Mouse sem fio', 1, 150),
(102, 'Teclado Mecânico', 1, 80),
(201, 'Caderno Universitário', 2, 200),
(202, 'Caixa de Caneta Azul', 2, 60),
(301, 'Desinfetante 5L', 3, 30);

-- Inserindo Movimentações Fictícias no Histórico
-- Entradas
INSERT INTO Movimentacao (codigo_id, id_usuario, quantidade, tipo, data_hora) VALUES 
(101, 1, 150, 'ENTRADA', '2026-05-10 09:00:00'),
(102, 1, 80, 'ENTRADA', '2026-05-10 09:15:00'),
(201, 2, 200, 'ENTRADA', '2026-05-11 10:30:00'),
(202, 2, 60, 'ENTRADA', '2026-05-11 10:45:00'),
(301, 3, 30, 'ENTRADA', '2026-05-12 08:20:00');

-- Saídas (Consumo)
INSERT INTO Movimentacao (codigo_id, id_usuario, quantidade, tipo, data_hora) VALUES 
(101, 2, 5, 'SAIDA', '2026-05-15 14:00:00'),
(201, 3, 10, 'SAIDA', '2026-05-16 11:10:00'),
(202, 2, 2, 'SAIDA', '2026-05-18 16:40:00'),
(101, 1, 2, 'SAIDA', '2026-05-20 09:30:00');