ALTER TABLE {sc}.{tb} SET UNLOGGED;
COPY {sc}.{tb} {cls} FROM '{fp}' WITH QUOTE E'\b' HEADER {dl};
ALTER TABLE {sc}.{tb} SET LOGGED;