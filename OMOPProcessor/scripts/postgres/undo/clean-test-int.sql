DELETE FROM {sc}.test_int WHERE patid IN (SELECT patient_id FROM  {sc}._chunk WHERE ordinal = {ch});