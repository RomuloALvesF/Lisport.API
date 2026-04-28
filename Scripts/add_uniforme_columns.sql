-- Adiciona colunas de uniforme na tabela Alunos (execute se a migration não foi aplicada)
-- O banco fica em: Lisport.API/lisport.db (pasta do projeto da API)
-- Na pasta Lisport.API: sqlite3 lisport.db ".read Scripts/add_uniforme_columns.sql"
-- Ou use um cliente SQLite (DB Browser, etc.) e execute os comandos abaixo.

ALTER TABLE Alunos ADD COLUMN TemUniforme INTEGER NOT NULL DEFAULT 0;
ALTER TABLE Alunos ADD COLUMN RecebeuUniforme INTEGER NULL;
