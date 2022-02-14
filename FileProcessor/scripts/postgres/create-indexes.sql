CREATE INDEX additional_adid_idx ON {sc}.additional USING btree (adid);

CREATE INDEX additional_data1_idx ON {sc}.additional USING btree (data1);

CREATE INDEX additional_data2_idx ON {sc}.additional USING btree (data2);

CREATE INDEX additional_data3_idx ON {sc}.additional USING btree (data3);

CREATE INDEX additional_data4_idx ON {sc}.additional USING btree (data4);

CREATE INDEX additional_data5_idx ON {sc}.additional USING btree (data5);

CREATE INDEX additional_data6_idx ON {sc}.additional USING btree (data6);

CREATE INDEX additional_data7_idx ON {sc}.additional USING btree (data7);

CREATE INDEX additional_patid_idx ON {sc}.additional USING btree (patid);

CREATE UNIQUE INDEX additional_idx_pkey ON {sc}.additional USING btree (id);

CREATE INDEX idx_add_enttype ON {sc}.additional USING btree (enttype);

CREATE INDEX clinical_adid_idx ON {sc}.clinical USING btree (adid);

CREATE INDEX clinical_consid_idx ON {sc}.clinical USING btree (consid);

CREATE INDEX clinical_constype_idx ON {sc}.clinical USING btree (constype);

CREATE INDEX clinical_patid_adid_idx ON {sc}.clinical USING btree (patid, adid);

CREATE INDEX clinical_patid_idx ON {sc}.clinical USING btree (patid);

CREATE UNIQUE INDEX clinical_idx_pkey ON {sc}.clinical USING btree (id);

CREATE INDEX clinical_staffid_idx ON {sc}.clinical USING btree (staffid);

CREATE INDEX idx_clin_medcode ON {sc}.clinical USING btree (medcode);

CREATE UNIQUE INDEX commondosages_idx_pkey ON {sc}.commondosages USING btree (dosageid);

CREATE INDEX idx_cdosage ON {sc}.commondosages USING btree (dosageid, daily_dose);

CREATE INDEX consultation_consid_constype_idx ON {sc}.consultation USING btree (consid, constype);

CREATE INDEX consultation_consid_idx ON {sc}.consultation USING btree (consid);

CREATE INDEX consultation_constype_idx ON {sc}.consultation USING btree (constype);

CREATE INDEX consultation_patid_consid_idx ON {sc}.consultation USING btree (patid, consid);

CREATE INDEX consultation_patid_idx ON {sc}.consultation USING btree (patid);

CREATE UNIQUE INDEX consultation_idx_pkey ON {sc}.consultation USING btree (id);

CREATE INDEX consultation_staffid_idx ON {sc}.consultation USING btree (staffid);

CREATE INDEX idx_cons_consid ON {sc}.consultation USING btree (patid, consid, constype);

CREATE INDEX daysupply_decodes_prodcode_idx ON {sc}.daysupply_decodes USING btree (prodcode);

CREATE INDEX daysupply_modes_prodcode_idx ON {sc}.daysupply_modes USING btree (prodcode);

CREATE INDEX entity_data10_lkup_idx ON {sc}.entity USING btree (data10_lkup);

CREATE INDEX entity_data11_lkup_idx ON {sc}.entity USING btree (data11_lkup);

CREATE INDEX entity_data12_lkup_idx ON {sc}.entity USING btree (data12_lkup);

CREATE INDEX entity_data1_lkup_idx ON {sc}.entity USING btree (data1_lkup);

CREATE INDEX entity_data2_lkup_idx ON {sc}.entity USING btree (data2_lkup);

CREATE INDEX entity_data3_lkup_idx ON {sc}.entity USING btree (data3_lkup);

CREATE INDEX entity_data4_lkup_idx ON {sc}.entity USING btree (data4_lkup);

CREATE INDEX entity_data5_lkup_idx ON {sc}.entity USING btree (data5_lkup);

CREATE INDEX entity_data6_lkup_idx ON {sc}.entity USING btree (data6_lkup);

CREATE INDEX entity_data7_lkup_idx ON {sc}.entity USING btree (data7_lkup);

CREATE INDEX entity_data8_lkup_idx ON {sc}.entity USING btree (data8_lkup);

CREATE INDEX entity_data9_lkup_idx ON {sc}.entity USING btree (data9_lkup);

CREATE INDEX entity_enttype_idx ON {sc}.entity USING btree (enttype);

CREATE UNIQUE INDEX entity_idx_pkey ON {sc}.entity USING btree (enttype, filetype, data_fields);

CREATE INDEX idx_imm_medcode ON {sc}.immunisation USING btree (medcode);

CREATE INDEX immunisation_consid_idx ON {sc}.immunisation USING btree (consid);

CREATE INDEX immunisation_patid_idx ON {sc}.immunisation USING btree (patid);

CREATE UNIQUE INDEX immunisation_idx_pkey ON {sc}.immunisation USING btree (id);

CREATE INDEX immunisation_staffid_idx ON {sc}.immunisation USING btree (staffid);

CREATE INDEX idx_lookup ON {sc}.lookup USING btree (lookup_type_id);

CREATE INDEX lookup_code_idx ON {sc}.lookup USING btree (code);

CREATE UNIQUE INDEX lookup_lookup_id_idx ON {sc}.lookup USING btree (lookup_id);

CREATE UNIQUE INDEX lookup_idx_pkey ON {sc}.lookup USING btree (lookup_id, lookup_type_id, code);

CREATE INDEX idx_med_medcode ON {sc}.medical USING btree (medcode, readcode);

CREATE UNIQUE INDEX medical_medcode_idx ON {sc}.medical USING btree (medcode);

CREATE UNIQUE INDEX medical_idx_pkey ON {sc}.medical USING btree (medcode);

CREATE UNIQUE INDEX medical_read_code_idx ON {sc}.medical USING btree (readcode);

CREATE INDEX idx_pat_frd ON {sc}.patient USING btree (patid, frd);

CREATE UNIQUE INDEX patient_patid_idx ON {sc}.patient USING btree (patid);

CREATE UNIQUE INDEX patient_idx_pkey ON {sc}.patient USING btree (patid, gender, yob);

CREATE INDEX patient_pracid_idx ON {sc}.patient USING btree (pracid);

CREATE INDEX idx_ref_medcode ON {sc}.referral USING btree (medcode);

CREATE INDEX referral_consid_idx ON {sc}.referral USING btree (consid);

CREATE INDEX referral_patid_idx ON {sc}.referral USING btree (patid);

CREATE UNIQUE INDEX referral_idx_pkey ON {sc}.referral USING btree (id);

CREATE INDEX referral_staffid_idx ON {sc}.referral USING btree (staffid);

CREATE INDEX idx_test_medcode ON {sc}.test USING btree (medcode);

CREATE INDEX test_consid_idx ON {sc}.test USING btree (consid);

CREATE INDEX test_data1_idx ON {sc}.test USING btree (data1);

CREATE INDEX test_data2_idx ON {sc}.test USING btree (data2);

CREATE INDEX test_data3_idx ON {sc}.test USING btree (data3);

CREATE INDEX test_data4_idx ON {sc}.test USING btree (data4);

CREATE INDEX test_data5_idx ON {sc}.test USING btree (data5);

CREATE INDEX test_data6_idx ON {sc}.test USING btree (data6);

CREATE INDEX test_data7_idx ON {sc}.test USING btree (data7);

CREATE INDEX test_data8_idx ON {sc}.test USING btree (data8);

CREATE INDEX test_enttype_idx ON {sc}.test USING btree (enttype);

CREATE INDEX test_patid_idx ON {sc}.test USING btree (patid);

CREATE UNIQUE INDEX test_idx_pkey ON {sc}.test USING btree (id);

CREATE INDEX test_staffid_idx ON {sc}.test USING btree (staffid);

CREATE INDEX idx_thpy_prodcode ON {sc}.therapy USING btree (prodcode, qty, numpacks);

CREATE INDEX therapy_consid_idx ON {sc}.therapy USING btree (consid);

CREATE INDEX therapy_dosageid_idx ON {sc}.therapy USING btree (dosageid);

CREATE INDEX therapy_packtype_idx ON {sc}.therapy USING btree (packtype);

CREATE INDEX therapy_patid_idx ON {sc}.therapy USING btree (patid);

CREATE UNIQUE INDEX therapy_idx_pkey ON {sc}.therapy USING btree (id);

CREATE INDEX therapy_prodcode_idx ON {sc}.therapy USING btree (prodcode);

CREATE INDEX therapy_staffid_idx ON {sc}.therapy USING btree (staffid);

CREATE UNIQUE INDEX lookuptype_lookup_type_id_idx ON {sc}.lookuptype USING btree (lookup_type_id);

CREATE INDEX lookuptype_name_idx ON {sc}.lookuptype USING btree (name);

CREATE UNIQUE INDEX lookuptype_idx_pkey ON {sc}.lookuptype USING btree (lookup_type_id, name);

CREATE UNIQUE INDEX practice_idx_pkey ON {sc}.practice USING btree (pracid, region);

CREATE INDEX practice_pracid_idx ON {sc}.practice USING btree (pracid);

CREATE INDEX product_bnfcode_idx ON {sc}.product USING btree (bnfcode);

CREATE INDEX product_dmdcode_idx ON {sc}.product USING btree (dmdcode);

CREATE INDEX product_gemscriptcode_idx ON {sc}.product USING btree (gemscriptcode);

CREATE UNIQUE INDEX product_idx_pkey ON {sc}.product USING btree (prodcode);

CREATE UNIQUE INDEX scoringmethod_idx_pkey ON {sc}.scoremethod USING btree (code);

CREATE UNIQUE INDEX staff_idx_pkey ON {sc}.staff USING btree (staffid, role);

CREATE INDEX staff_staffid_idx ON {sc}.staff USING btree (staffid);
