DELETE FROM {sc}.visit_occurrence WHERE person_id IN (SELECT patient_id FROM  {sc}._chunk WHERE ordinal = {ch});
