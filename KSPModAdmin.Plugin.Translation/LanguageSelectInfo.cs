namespace KSPModAdmin.Plugin.Translation
{
    /// <summary>
    /// Class to hold the informations of the selected language.
    /// </summary>
    public class LanguageSelectInfo
    {
        /// <summary>
        /// Name of the language.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Path to the language file.
        /// </summary>
        public string Path { get; set; }


        /// <summary>
        /// Creates a string from the class informations.
        /// </summary>
        /// <returns>Returns the value of the "Name" property.</returns>
        public override string ToString()
        {
            return (string.IsNullOrEmpty(Name) ? base.ToString() : Name);
        }
    }
}