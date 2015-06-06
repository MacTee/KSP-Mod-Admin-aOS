using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Xml;

namespace KSPModAdmin.Core.Utils.Localization
{
    /// <summary>
    /// Class to load language files and provide language Key/Value pairs for translation.
    /// </summary>
    public class Localizer
    {
        #region Constants

        // All used strings in this class.

        /// <summary>
        /// Default language short name.
        /// </summary>
        public const string DEFAULT_LANGUAGE = "eng";

        /// <summary>
        /// The string "LanguageName".
        /// </summary>
        public const string LANGUAGE_NAME = "LanguageName";

        /// <summary>
        /// The language file extension.
        /// </summary>
        public const string LANG_FILE_EXTENSION = "*.lang";
        private const string NEWLINE_REPLACE_CHAR = "^";
        private const string LANGUAGE = "Language";
        private const string CONTROL = "Control";
        private const string STRING = "String";
        private const string VALUE = "Value";
        private const string NAME = "Name";
        private const string LONGNAME = "LongName";
        private const string EQUAL_SIGN = "=";
        private const string MSG_DUPLICATE_KEY_0_1_2_3 = "Duplicate key in language \"{0}\": key = \"{1}\" value1 = \"{2}\" value2 = \"{3}\"";
        private const string MSG_ERROR_DURING_LOADING_LANGUAGE_0 = "Error during loading language \"{0}\"!";
        private const string MSG_DEFAULT_LANGUAGE_0_FILE_FOUND = "Default language {0} file not found!";
        private const string MSG_NO_LANGUAGE_FILE_FOUND = "No language file found!";
        private const string MSG_KEY_0_NOT_FOUND_FOR_LANGUAGE_1 = "Key {0} is not defined for language {1}!";
        private const string MSG_KEY_0_NOT_DEFINED_FOR_LANGUAGE_1 = "Key {0} is not defined for language {1}!";

        #endregion

        #region Members

        private static Localizer mInstance = null;

        private LanguagesDictionary mLanguageDictionary = new LanguagesDictionary();

        private List<Language> mLanguages = new List<Language>();

        #endregion

        #region Properties

        /// <summary>
        /// The global instance of a LanguageDictionaryManager.
        /// </summary>
        public static Localizer GlobalInstance
        {
            get
            {
                if (mInstance == null)
                    mInstance = new Localizer();

                return mInstance;
            }
        }


        /// <summary>
        /// Gets or sets the default language.
        /// </summary>
        public string DefaultLanguage { get; set; }

        /// <summary>
        /// Gets or sets The current selected language.
        /// </summary>
        public string CurrentLanguage { get; set; }

        /// <summary>
        /// Gets all available languages (short).
        /// </summary>
        public List<Language> AvailableLanguages { get { return mLanguages; } }

        /// <summary>
        /// Gets or sets the value for the specified key for the CurrentLanguage.
        /// </summary>
        /// <param name="key">The key to get the value from.</param>
        /// <returns>The value for the specified key for the CurrentLanguage.</returns>
        public string this[string key]
        {
            get
            {
                if (mLanguageDictionary.ContainsKey(CurrentLanguage) && mLanguageDictionary[CurrentLanguage].ContainsKey(key))
                    return mLanguageDictionary[CurrentLanguage][key];
                else if (mLanguageDictionary.ContainsKey(DEFAULT_LANGUAGE) && mLanguageDictionary[DEFAULT_LANGUAGE].ContainsKey(key))
                {
                    Messenger.AddError(string.Format(MSG_KEY_0_NOT_FOUND_FOR_LANGUAGE_1, key, CurrentLanguage));
                    return mLanguageDictionary[DEFAULT_LANGUAGE][key];
                }
                else
                {
                    Messenger.AddError(string.Format(MSG_KEY_0_NOT_FOUND_FOR_LANGUAGE_1, key, CurrentLanguage));
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets or sets the value for the specified language and key.
        /// </summary>
        /// <param name="language">The language of the key value.</param>
        /// <param name="key">The key to get the value from.</param>
        /// <returns>The value for the specified language and key..</returns>
        public string this[string language, string key]
        {
            get
            {
                // Does wanted language exists?
                string lang = language;
                if (!mLanguageDictionary.ContainsKey(language))
                {
                    // no, fall back to default language.
                    if (!mLanguageDictionary.ContainsKey(DefaultLanguage))
                        throw new ArgumentOutOfRangeException(string.Format(MSG_KEY_0_NOT_DEFINED_FOR_LANGUAGE_1, key, lang));

                    lang = DefaultLanguage;
                }

                // Does wanted value exists?
                if (mLanguageDictionary[lang].ContainsKey(key))
                    return mLanguageDictionary[lang, key];

                    // no, try default language.
                else if (lang != DefaultLanguage && mLanguageDictionary.ContainsKey(DefaultLanguage))
                    return mLanguageDictionary[DefaultLanguage, key];

                    // key not found!
                else
                {
                    Messenger.AddError(string.Format(MSG_KEY_0_NOT_FOUND_FOR_LANGUAGE_1, key, lang));
                    return string.Empty;
                }
            }
            set
            {
                if (!mLanguageDictionary.ContainsKey(language))
                    mLanguageDictionary.Add(language, new Dictionary<string, string>());

                if (ContainsKey(language, key))
                    mLanguageDictionary[language, key] = value;
                else
                    mLanguageDictionary[language].Add(key, value);
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of the Localizer class.
        /// </summary>
        public Localizer()
        {
            DefaultLanguage = DEFAULT_LANGUAGE;
            CurrentLanguage = DefaultLanguage;
        }

        /// <summary>
        /// Creates a new instance of the Localizer class.
        /// </summary>
        public Localizer(string filename, string defaultLanguage = DEFAULT_LANGUAGE)
        {
            DefaultLanguage = defaultLanguage;
            CurrentLanguage = DefaultLanguage;
            LoadLanguage(filename);
        }

        #endregion


        /// <summary>
        /// Loads all available languages from the passed directory.
        /// </summary>
        /// <param name="languageFolderPaths">Paths to the language files.</param>
        /// <returns>True, if load was without any errors.</returns>
        public bool LoadLanguages(string[] languageFolderPaths, bool defaultLanguageRequired = false, bool xml = true)
        {
            List<string> allLangFiles = new List<string>();
            foreach (var path in languageFolderPaths)
            {
                if (!Directory.Exists(path))
                    continue;

                string[] langFiles = Directory.GetFiles(path, LANG_FILE_EXTENSION);
                allLangFiles.AddRange(langFiles);
            }

            if (allLangFiles.Count == 0)
                return false;

            return LoadLanguageFiles(allLangFiles.ToArray(), defaultLanguageRequired, xml);
        }

        /// <summary>
        /// Loads all available languages from the passed directory.
        /// </summary>
        /// <param name="languageFolderPath">Path to the language files.</param>
        /// <returns>True, if load was without any errors.</returns>
        public bool LoadLanguages(string languageFolderPath, bool defaultLanguageRequired = false, bool xml = true)
        {
            if (!Directory.Exists(languageFolderPath))
                return false;

            string[] langFiles = Directory.GetFiles(languageFolderPath, LANG_FILE_EXTENSION);
            return LoadLanguageFiles(langFiles, defaultLanguageRequired, xml);
        }

        /// <summary>
        /// Loads all language files of passed filename array.
        /// </summary>
        /// <param name="langFiles">Array of paths to the language files.</param>
        /// <returns>True, if load was without any errors.</returns>
        public bool LoadLanguageFiles(string[] langFiles, bool defaultLanguageRequired = false, bool xml = true)
        {
            bool result = false;
            if (langFiles.Length <= 0)
                return false;

            string currentFile = string.Empty;

            try
            {
                foreach (var langFile in langFiles)
                {
                    currentFile = langFile;
                    if (xml)
                    {
                        if (LoadLanguageFromXml(langFile))
                            result = true;
                    }
                    else
                    {
                        if (LoadLanguage(langFile))
                            result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Messenger.AddError(string.Format(MSG_ERROR_DURING_LOADING_LANGUAGE_0, currentFile), ex);
                return false;
            }

            if (defaultLanguageRequired && !mLanguageDictionary.ContainsKey(DefaultLanguage))
            {
                Messenger.AddError(string.Format(MSG_DEFAULT_LANGUAGE_0_FILE_FOUND, DefaultLanguage));
                return false;
            }

            if (!result)
            {
                Messenger.AddError(MSG_NO_LANGUAGE_FILE_FOUND);
                return false;
            }

            return result;
        }


        /// <summary>
        /// Loads a language file to memory.
        /// </summary>
        /// <param name="filename">path to the language file.</param>
        /// <returns>True, if at least one key value pair could be loaded from the passed file.</returns>
        public bool LoadLanguage(string filename)
        {
            if (!File.Exists(filename))
                return false;

            string language = string.Empty;
            string[] fileContent = File.ReadAllLines(filename);
            if (fileContent.Length <= 0)
                return false;

            foreach (var line in fileContent)
            {
                if (line.Contains(LANGUAGE))
                {
                    language = line.Substring(line.IndexOf(EQUAL_SIGN) + EQUAL_SIGN.Length).Trim();
                    if (!mLanguageDictionary.ContainsKey(language))
                        mLanguageDictionary.Add(language, new Dictionary<string, string>());
                }

                if (line.Contains(LONGNAME))
                {
                    string longName = line.Substring(line.IndexOf(EQUAL_SIGN) + EQUAL_SIGN.Length).Trim();
                    if (!ContainsLanguageByLongName(longName))
                        mLanguages.Add(new Language(language, longName));
                }

                else if (!string.IsNullOrEmpty(language))
                {
                    string[] values = line.Split('=');
                    if (values.Length == 2 && !string.IsNullOrEmpty(language))
                    { 
                        values[0] = values[0].Trim();
                        values[1] = values[1].Trim().Replace(NEWLINE_REPLACE_CHAR, Environment.NewLine);
                        if (mLanguageDictionary[language].ContainsKey(values[0]))
                            mLanguageDictionary[language][values[0]] = values[1];
                        else
                            mLanguageDictionary[language].Add(values[0], values[1]);
                    }
                }
            }

            return (!string.IsNullOrEmpty(language) && mLanguageDictionary[language].Count > 0);
        }

        /// <summary>
        /// Loads a xml language file to memory.
        /// </summary>
        /// <param name="filename">path to the language file.</param>
        /// <returns>True, if at least one key value pair could be loaded from the passed file.</returns>
        public bool LoadLanguageFromXml(string filename)
        {
            if (!File.Exists(filename))
                return false;

            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            string language = string.Empty;
            XmlNodeList nodes = doc.GetElementsByTagName(LANGUAGE);
            if (nodes.Count > 0 && nodes[0].Attributes != null && nodes[0].Attributes[NAME] != null)
            {
                language = nodes[0].Attributes[NAME].Value.Trim();
                if (nodes[0].Attributes[LONGNAME] != null && !ContainsLanguageByName(language))
                    mLanguages.Add(new Language(language, nodes[0].Attributes[LONGNAME].Value.Trim()));
            }

            if (string.IsNullOrEmpty(language))
                return false;
            
            if (!mLanguageDictionary.ContainsKey(language))
                mLanguageDictionary.Add(language, new Dictionary<string, string>());

            nodes = doc.GetElementsByTagName(CONTROL);
            if (nodes.Count > 0)
                ReadNodes(nodes, language);

            nodes = doc.GetElementsByTagName(STRING);
            if (nodes.Count > 0)
                ReadNodes(nodes, language);

            return (!string.IsNullOrEmpty(language) && mLanguageDictionary[language].Count > 0);
        }


        /// <summary>
        /// Checks if the language is loaded by the Localizer.
        /// </summary>
        /// <param name="language">The language to check.</param>
        /// <returns>True if the language is loaded.</returns>
        public bool ContainsLaguage(string language)
        {
            return mLanguageDictionary.ContainsLaguage(language);
        }

        /// <summary>
        /// Checks if the key exists for the current language.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>True if the key exists for the current language.</returns>
        public bool ContainsKey(string key)
        {
            return mLanguageDictionary.ContainsKey(CurrentLanguage, key);
        }

        /// <summary>
        /// Checks if the key for a certain language is available.
        /// </summary>
        /// <param name="language">The language that the key entry should have.</param>
        /// <param name="key">The key to search for.</param>
        /// <returns>True, if the key for a certain language is available .</returns>
        public bool ContainsKey(string language, string key)
        {
            return mLanguageDictionary.ContainsKey(language, key);
        }

        
        /// <summary>
        /// Returns the LongName of the passed language.
        /// </summary>
        /// <param name="name">The language to get the LongName from.</param>
        /// <returns>The LongName of the passed language.</returns>
        public string GetLanguageLongName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return string.Empty;

            foreach (Language language in mLanguages)
            {
                if (language.Name == name)
                    return language.LongName;
            }

            return string.Empty;
        }

        /// <summary>
        /// Returns the language name of the passed LongName.
        /// </summary>
        /// <param name="longName">The LongName of the language to get.</param>
        /// <returns>The language name of the passed LongName.</returns>
        public string GetLanguageNameByLongName(string longName)
        {
            if (string.IsNullOrEmpty(longName))
                return string.Empty;

            foreach (Language language in mLanguages)
            {
                if (language.LongName == longName)
                    return language.Name;
            }

            return string.Empty;
        }

        /// <summary>
        /// Returns the key/value Dictionary of the language.
        /// </summary>
        /// <returns>The key/value Dictionary of the language.</returns>
        public Dictionary<string, string> GetDictionaryOfLanguage(string language)
        {
            return mLanguageDictionary[language];
        }


        /// <summary>
        /// Adds a key value pair for a certain language.
        /// </summary>
        /// <param name="language">The language.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Add(string language, string key, string value)
        {
            mLanguageDictionary.Add(language, key, value);
        }

        /// <summary>
        /// Adds a key value pair to the CurrentLanguage.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Add(string key, string value)
        {
            mLanguageDictionary.Add(CurrentLanguage, key, value);
        }


        /// <summary>
        /// Adds a key value pair for a certain language.
        /// </summary>
        /// <param name="language">The language to remove.</param>
        public void RemoveLanguage(string language)
        {
            mLanguageDictionary.RemoveLanguage(language);
        }

        /// <summary>
        /// Adds a key value pair for a certain language.
        /// </summary>
        /// <param name="language">The language.</param>
        /// <param name="key">The key to remove.</param>
        public void RemoveKeyFromLanguage(string language, string key)
        {
            mLanguageDictionary.RemoveKeyFromLanguage(language, key);
        }


        /// <summary>
        /// Clears the LanguageDictionary.
        /// </summary>
        public void Clear()
        {
            mLanguageDictionary.Clear();
            mLanguages.Clear();
        }


        /// <summary>
        /// Reads the LanguageDictionary entry from an XmlNode.
        /// </summary>
        /// <param name="nodes">The XmlNode to read the LanguageDictionary entry from.</param>
        /// <param name="language">The Language of the new LanguageDictionary entry.</param>
        private void ReadNodes(XmlNodeList nodes, string language)
        {
            foreach (XmlNode node in nodes)
            {
                if (node.Attributes != null && node.Attributes[NAME] != null && node.Attributes[VALUE] != null)
                {
                    string key = node.Attributes[NAME].Value.Trim();
                    string myValue = ControlTranslator.GetXmlUnescapedString(node.Attributes[VALUE].Value.Trim());
                    if (mLanguageDictionary[language].ContainsKey(key))
                    {
                        Messenger.AddError(string.Format(MSG_DUPLICATE_KEY_0_1_2_3, language, key, mLanguageDictionary[language][key], myValue));
                        mLanguageDictionary[language][key] = myValue;
                    }
                    else
                        mLanguageDictionary[language].Add(key, myValue);
                }

                if (node.HasChildNodes)
                    ReadNodes(node.ChildNodes, language);
            }
        }

        private bool ContainsLanguageByLongName(string longName)
        {
            foreach (Language language in mLanguages)
            {
                if (language.LongName == longName)
                    return true;
            }

            return false;
        }

        private bool ContainsLanguageByName(string name)
        {
            foreach (Language language in mLanguages)
            {
                if (language.Name == name)
                    return true;
            }

            return false;
        }
    }


    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public class Language
    {
        public string Name { get; set; }
        public string LongName { get; set; }

        public Language(string name, string longName)
        {
            Name = name;
            LongName = longName;
        }

        public override string ToString()
        {
            return LongName;
        }
    }
}
