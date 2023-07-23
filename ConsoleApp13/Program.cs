using System.Transactions;

namespace OpenPartsBook
{
    [Serializable()]
    public class TranslatableString
    {
        [JsonProperty]
        private string Text;
        public List<Translation> Translations { get; set; }

        public TranslatableString()
        {
            Translations = new List<Translation>();
        }
        public TranslatableString(string value)
        {
            Translations = new List<Translation>();
            Text = value;
        }


        public string ToTranslatedString(string LanguageCode)
        {
            string temp = String.Empty;

            int i = LanguageCode.Length;

            if (this.Translations.Any(t => t.LanguageCode.ToLower().Equals(LanguageCode.ToLower())))
            {
                temp = this.Translations.First(t => t.LanguageCode.ToLower().Equals(LanguageCode.ToLower())).Text;
            }
            else
            {

                while ((i = LanguageCode.LastIndexOf("-", i)) != -1)
                {
                    var code = LanguageCode.Substring(0, i).ToLower();

                    if (this.Translations.Any(t => t.LanguageCode.ToLower().Equals(code.ToLower())))
                    {
                        temp = this.Translations.First(t => t.LanguageCode.ToLower().Equals(code.ToLower())).Text;
                        break;
                    }

                    --i;
                }
            }

            if (String.IsNullOrEmpty(temp)) temp = Text;

            return temp;
        }

        public static implicit operator TranslatableString(string s)
        {
            return s == null ? null : new TranslatableString(s);
        }

        public static implicit operator string(TranslatableString s) { return s.ToString(); }

        public override string ToString() { return Text; }

    }
}