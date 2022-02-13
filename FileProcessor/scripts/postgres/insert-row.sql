ALTER TABLE {sc}.{tb} SET UNLOGGED;
INSERT INTO {sc}.{tb} ({clms}) VALUES ({vals});
ALTER TABLE {sc}.{tb} SET LOGGED;