DELETE FROM {sc}.drug_era WHERE PERSON_ID IN (SELECT patient_id FROM  {sc}._chunk WHERE ordinal = {ch});
		