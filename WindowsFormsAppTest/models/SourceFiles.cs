using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using SystemLocalStore.models;
using Util;
using WindowsFormsAppTest.errors;

namespace WindowsFormsAppTest.models
{
    public class SourceFiles : INotifyPropertyChanged
    {
        private readonly string[] DataKeys = { "additional", "clinical", "consultation", "immunisation", "patient", "practice", "referral", "staff", "test", "therapy" };
        public bool isValidated = false;
        public bool isSaved = false;
        public Action changeFunc;

        #region Directories

        //Root Dir for auto load

        string rootDir;
        public string RootDir { get { return rootDir; } set { rootDir = value; OnChangeEvent("RootDir"); } }
        //Data Dir Root Dir
        string dataDir;
        public string DataDir
        {
            get { return dataDir ?? (!string.IsNullOrEmpty(RootDir) ? Path.Combine(RootDir, "data") : String.Empty); }
            set { dataDir = value; OnChangeEvent("DataDir"); }
        }
        //Data Files Dirs
        string additionalDir;
        public string AdditionalDir
        {
            get { return additionalDir ?? (!string.IsNullOrEmpty(DataDir) ? Path.Combine(DataDir, "Additional") : String.Empty); }
            set { additionalDir = value; OnChangeEvent("AdditionalDir"); }
        }
        string clinicalDir;
        public string ClinicalDir
        {
            get { return clinicalDir ?? (!string.IsNullOrEmpty(DataDir) ? Path.Combine(DataDir, "Clinical") : String.Empty); }
            set { clinicalDir = value; OnChangeEvent("ClinicalDir"); }
        }
        string consultationDir;
        public string ConsultationDir
        {
            get { return consultationDir ?? (!string.IsNullOrEmpty(DataDir) ? Path.Combine(DataDir, "Consultation") : String.Empty); }
            set { consultationDir = value; OnChangeEvent("ConsultationDir"); }
        }
        string immunisationDir;
        public string ImmunisationDir
        {
            get { return immunisationDir ?? (!string.IsNullOrEmpty(DataDir) ? Path.Combine(DataDir, "Immunisation") : String.Empty); }
            set { immunisationDir = value; OnChangeEvent("ImmunisationDir"); }
        }
        string patientDir;
        public string PatientDir
        {
            get { return patientDir ?? (!string.IsNullOrEmpty(DataDir) ? Path.Combine(DataDir, "Patient") : String.Empty); }
            set { patientDir = value; OnChangeEvent("PatientDir"); }
        }
        string practiceDir;
        public string PracticeDir
        {
            get { return practiceDir ?? (!string.IsNullOrEmpty(DataDir) ? Path.Combine(DataDir, "Practice") : String.Empty); }
            set { practiceDir = value; OnChangeEvent("PracticeDir"); }
        }
        string referralDir;
        public string ReferralDir
        {
            get { return referralDir ?? (!string.IsNullOrEmpty(DataDir) ? Path.Combine(DataDir, "Referral") : String.Empty); }
            set { referralDir = value; OnChangeEvent("ReferralDir"); }
        }
        string staffDir;
        public string StaffDir
        {
            get { return staffDir ?? (!string.IsNullOrEmpty(DataDir) ? Path.Combine(DataDir, "Staff") : String.Empty); }
            set { staffDir = value; OnChangeEvent("StaffDir"); }
        }
        string testDir;
        public string TestDir
        {
            get { return testDir ?? (!string.IsNullOrEmpty(DataDir) ? Path.Combine(DataDir, "Test") : String.Empty); }
            set { testDir = value; OnChangeEvent("TestDir"); }
        }
        string therapyDir;
        public string TherapyDir
        {
            get { return therapyDir ?? (!string.IsNullOrEmpty(DataDir) ? Path.Combine(DataDir, "Therapy") : String.Empty); }
            set { therapyDir = value; OnChangeEvent("TherapyDir"); }
        }
        //Lookup Dirs
        string lookupsDir;
        public string LookupsDir
        {
            get { return lookupsDir ?? (!string.IsNullOrEmpty(RootDir) ? Path.Combine(RootDir, "lookups") : String.Empty); }
            set { lookupsDir = value; OnChangeEvent("LookupsDir"); }
        }
        string lookupTypesDir;
        public string LookuptypeDir
        {
            get { return lookupTypesDir ?? (!string.IsNullOrEmpty(LookupsDir) ? Path.Combine(LookupsDir, "TXTFiles") : String.Empty); }
            set { lookupTypesDir = value; OnChangeEvent("LookuptypeDir"); }
        }
        //Vocabulary Dir
        string vocabularyDir;
        public string VocabularyDir
        {
            get { return vocabularyDir ?? (!string.IsNullOrEmpty(RootDir) ? Path.Combine(RootDir, "vocabulary") : String.Empty); }
            set { vocabularyDir = value; OnChangeEvent("VocabularyDir"); }
        }

        #endregion

        #region Files

        //File Collections Processed
        List<string> additionalFiles = new List<string>();
        List<string> clinicalFiles = new List<string>();
        List<string> consultationFiles = new List<string>();
        List<string> immunisationFiles = new List<string>();
        List<string> patientFiles = new List<string>();
        List<string> practiceFiles = new List<string>();
        List<string> referralFiles = new List<string>();
        List<string> staffFiles = new List<string>();
        List<string> testFiles = new List<string>();
        List<string> therapyFiles = new List<string>();
        List<string> lookuptypeFiles = new List<string>();

        //Lookup Files
        string commonDosagesFile;
        public string CommondosagesFile
        {
            get { return commonDosagesFile ?? (!string.IsNullOrEmpty(LookupsDir) ? Path.Combine(LookupsDir, "processed", "common_dosages.txt") : String.Empty); }
            set { commonDosagesFile = value; OnChangeEvent("CommondosagesFile"); }
        }
        string entityFile;
        public string EntityFile
        {
            get { return entityFile ?? (!string.IsNullOrEmpty(LookupsDir) ? Path.Combine(LookupsDir, "processed", "entity.txt") : String.Empty); }
            set { entityFile = value; OnChangeEvent("EntityFile"); }
        }
        string medicalFile;
        public string MedicalFile
        {
            get { return medicalFile ?? (!string.IsNullOrEmpty(LookupsDir) ? Path.Combine(LookupsDir, "processed", "medical.txt") : String.Empty); }
            set { medicalFile = value; OnChangeEvent("MedicalFile"); }
        }
        string productFile;
        public string ProductFile
        {
            get { return productFile ?? (!string.IsNullOrEmpty(LookupsDir) ? Path.Combine(LookupsDir, "processed", "product.txt") : String.Empty); }
            set { productFile = value; OnChangeEvent("ProductFile"); }
        }
        string scoreMethodFile;
        public string ScoremethodFile
        {
            get { return scoreMethodFile ?? (!string.IsNullOrEmpty(LookupsDir) ? Path.Combine(LookupsDir, "processed", "scoremethod.txt") : String.Empty); }
            set { scoreMethodFile = value; OnChangeEvent("ScoremethodFile"); }
        }

        //Vocabulary Files
        string conceptFile;
        public string ConceptFile
        {
            get { return conceptFile ?? (!string.IsNullOrEmpty(VocabularyDir) ? Path.Combine(VocabularyDir, "concept.csv") : String.Empty); }
            set { conceptFile = value; OnChangeEvent("ConceptFile"); }
        }
        string conceptAncestorFile;
        public string ConceptAncestorFile
        {
            get { return conceptAncestorFile ?? (!string.IsNullOrEmpty(VocabularyDir) ? Path.Combine(VocabularyDir, "concept_ancestor.csv") : String.Empty); }
            set { conceptAncestorFile = value; OnChangeEvent("ConceptAncestorFile"); }
        }
        string conceptClassFile;
        public string ConceptClassFile
        {
            get { return conceptClassFile ?? (!string.IsNullOrEmpty(VocabularyDir) ? Path.Combine(VocabularyDir, "concept_class.csv") : String.Empty); }
            set { conceptClassFile = value; OnChangeEvent("ConceptClassFile"); }
        }
        string conceptRelationshipFile;
        public string ConceptRelationshipFile
        {
            get { return conceptRelationshipFile ?? (!string.IsNullOrEmpty(VocabularyDir) ? Path.Combine(VocabularyDir, "concept_relationship.csv") : String.Empty); }
            set { conceptRelationshipFile = value; OnChangeEvent("ConceptRelationshipFile"); }
        }
        string conceptSynonymFile;
        public string ConceptSynonymFile
        {
            get { return conceptSynonymFile ?? (!string.IsNullOrEmpty(VocabularyDir) ? Path.Combine(VocabularyDir, "concept_synonym.csv") : String.Empty); }
            set { conceptSynonymFile = value; OnChangeEvent("ConceptSynonymFile"); }
        }
        string domainFile;
        public string DomainFile
        {
            get { return domainFile ?? (!string.IsNullOrEmpty(VocabularyDir) ? Path.Combine(VocabularyDir, "domain.csv") : String.Empty); }
            set { domainFile = value; OnChangeEvent("DomainFile"); }
        }
        string drugStrengthFile;
        public string DrugStrengthFile
        {
            get { return drugStrengthFile ?? (!string.IsNullOrEmpty(VocabularyDir) ? Path.Combine(VocabularyDir, "drug_strength.csv") : String.Empty); }
            set { drugStrengthFile = value; OnChangeEvent("DrugStrengthFile"); }
        }
        string relationshipFile;
        public string RelationshipFile
        {
            get { return relationshipFile ?? (!string.IsNullOrEmpty(VocabularyDir) ? Path.Combine(VocabularyDir, "relationship.csv") : String.Empty); }
            set { relationshipFile = value; OnChangeEvent("RelationshipFile"); }
        }
        string vocabularyFile;
        public string VocabularyFile
        {
            get { return vocabularyFile ?? (!string.IsNullOrEmpty(VocabularyDir) ? Path.Combine(VocabularyDir, "vocabulary.csv") : String.Empty); }
            set { vocabularyFile = value; OnChangeEvent("VocabularyFile"); }
        }

        private PropertyInfo[] holderProperties
        {
            get { return this.GetType().GetProperties().Where(pi => Regex.Match(pi.Name, @"(File|Dir)$").Success && char.IsUpper(pi.Name[0])).ToArray(); }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        #region File Processors

        public int? processDataFiles(string propertyKey)
        {
            var dirProp = $"{propertyKey.FirstCharToUpper()}Dir";
            PropertyInfo propDir = this.GetType().GetProperty(dirProp);
            var path = (string)propDir.GetValue(this);
            if (!Directory.Exists(path)) return null;
            string[] files = Directory.GetFiles(path, "*.txt", SearchOption.AllDirectories);
            if (files.Length == 0) return 0;
            PropertyInfo propFiles = this.GetType().GetProperty($"{propertyKey}Files");
            if (null != propFiles) propFiles.SetValue(this, files.Cast<string>().ToList());

            return files.Length;
        }

        public SourceFiles populatePaths(List<SourceFile> sources)
        {
            foreach (var source in sources)
            {
                var prop = source.Code + (source.IsFile ? "File" : "Dir");
                PropertyInfo propInfo = this.GetType().GetProperty(prop);
                if (null == propInfo) continue;
                propInfo.SetValue(this, source.FilePath);
            }
            return this;
        }

        public IEnumerable<Exception> validationErrors()
        {
            foreach (var property in holderProperties)
            {
                var path = (string)property.GetValue(this);
                var hash = Regex.Replace(property.Name, @"(File|Dir)$", "");
                if (Regex.Match(property.Name, @"Dir$").Success)
                {
                    if (!Directory.Exists(path)) yield return new InvalidDirectoryException($"{path}#{hash}");
                    if (DataKeys.Contains(hash.ToLower()))
                    {
                        var f = processDataFiles(hash);
                        if (null != f && f <= 0) yield return new GeneralException($"'{path}#{hash}' doesn't have any file for processing!");
                    }
                }
                else if (Regex.Match(property.Name, @"File").Success && !File.Exists(path)) yield return new InvalidFileException($"{path}#{hash}");
            }
        }

        public IEnumerable<KeyValuePair<string, string>> analysisDetails()
        {
            foreach (var property in holderProperties)
            {
                var path = (string)property.GetValue(this);
                var hash = Regex.Replace(property.Name, @"(File|Dir)$", "");
                if (Directory.Exists(path))
                {
                    var cnt = Directory.GetFiles(path, "*.txt", SearchOption.AllDirectories).Length;
                    yield return new KeyValuePair<string, string>($"{path}#{hash}", $"{cnt} Files");
                }
                else yield return new KeyValuePair<string, string>($"{path}#{hash}", "OK");
            }
        }

        public IEnumerable<SourceFile> processedFiles(Int64 workLoadId)
        {
            string[] ignoredDirs = { "rootdir", "vocabularydir","datadir", "lookupsdir" };
            foreach (var property in holderProperties.Where(pi=>!ignoredDirs.Contains(pi.Name.ToLower())))
            {
                var path = (string)property.GetValue(this);
                var hash = Regex.Replace(property.Name, @"(File|Dir)$", "");
                var IsFile = File.Exists(path);
                yield return new SourceFile
                {
                    WorkLoadId = workLoadId,
                    TableName = hash.ToSnakeCase(),
                    FileName = IsFile ? Path.GetFileNameWithoutExtension(path) : (new DirectoryInfo(path)).Name,
                    FilePath = path,
                    FileHash = "[NO HASHING]", // path.ToFileMD5Hash(), // We will relook this later in future. Fails now
                    Code = hash,
                    IsFile = IsFile,
                    Processed = false
                };
            }
        }

        #endregion

        private void OnChangeEvent(string property_name) { OnPropertyChanged(new PropertyChangedEventArgs(property_name)); }

        public void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, args);
            isValidated = false;
            if (null != changeFunc) changeFunc();
        }
    }
}
