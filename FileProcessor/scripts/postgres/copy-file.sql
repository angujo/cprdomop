ALTER TABLE {sc}.{tb} SET UNLOGGED;
COPY {sc}.{tb} {cls} FROM '{fp}' WITH HEADER {dl};
ALTER TABLE {sc}.{tb} SET LOGGED;