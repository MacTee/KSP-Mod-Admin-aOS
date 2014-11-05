namespace KSPModAdmin.Translation.Plugin
{
    public class LanguageSelectInfo
    {
        public string Name { get; set; }
        public string Path { get; set; }


        public override string ToString()
        {
            return (string.IsNullOrEmpty(Name) ? base.ToString() : Name);
        }
    }
}