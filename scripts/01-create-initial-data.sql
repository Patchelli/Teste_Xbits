INSERT INTO categorias (nome, descricao, created_at) VALUES
('Eletrônicos', 'Produtos eletrônicos e tecnológicos', CURRENT_TIMESTAMP),
('Roupas', 'Vestuário e acessórios', CURRENT_TIMESTAMP),
('Casa e Jardim', 'Produtos para casa e jardim', CURRENT_TIMESTAMP),
('Livros', 'Livros e materiais educativos', CURRENT_TIMESTAMP),
('Esportes', 'Equipamentos e acessórios esportivos', CURRENT_TIMESTAMP);

INSERT INTO produtos (nome, descricao, preco, categoria_id, created_at) VALUES
('Smartphone Samsung Galaxy', 'Smartphone com 128GB de armazenamento', 1299.99, 1, CURRENT_TIMESTAMP),
('Notebook Dell Inspiron', 'Notebook para uso profissional', 2499.90, 1, CURRENT_TIMESTAMP),
('Fone de Ouvido Bluetooth', 'Fone sem fio com cancelamento de ruído', 299.99, 1, CURRENT_TIMESTAMP),

('Camiseta Básica', 'Camiseta 100% algodão', 39.90, 2, CURRENT_TIMESTAMP),
('Calça Jeans', 'Calça jeans masculina', 89.90, 2, CURRENT_TIMESTAMP),
('Tênis Esportivo', 'Tênis para corrida e caminhada', 199.99, 2, CURRENT_TIMESTAMP),

('Aspirador de Pó', 'Aspirador com filtro HEPA', 299.90, 3, CURRENT_TIMESTAMP),
('Conjunto de Panelas', 'Conjunto com 5 panelas antiaderentes', 189.90, 3, CURRENT_TIMESTAMP),

('Clean Code', 'Livro sobre boas práticas de programação', 79.90, 4, CURRENT_TIMESTAMP),
('Design Patterns', 'Padrões de projeto em programação', 89.90, 4, CURRENT_TIMESTAMP),

('Bola de Futebol', 'Bola oficial para futebol', 49.90, 5, CURRENT_TIMESTAMP),
('Raquete de Tênis', 'Raquete profissional de tênis', 299.90, 5, CURRENT_TIMESTAMP);
