/** ChunkClean #{ch} */
DELETE FROM {sc}.death JOIN {sc}._chunk ch ON patient_id = person_id and ch.ordinal = {ch};