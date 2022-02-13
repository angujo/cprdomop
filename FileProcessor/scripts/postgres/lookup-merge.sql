ALTER TABLE {sc}.lookup SET UNLOGGED;
UPDATE {sc}.lookup SET lookup.lookup_type_id = lt.lookup_type_id FROM {sc}.lookuptype lt WHERE lt.name = lookup.temp_file_name;
ALTER TABLE {sc}.lookup SET LOGGED;