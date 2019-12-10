namespace finalWork.Models
{
    public class Text
    {
        public string TextValue { get; set; }
        public string KeyText { get; set; }
        public string TextName { get; set; }
        public Text (string textValue, string key, string name)
        {
            TextValue = textValue;
            KeyText = key;
            TextName = name;
        }
        public Text() {}
    }
}
