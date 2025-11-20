namespace EcommerceBackend.Enums
{

    public class Choice
    {
        public string Label { get; set; } = "";
        public decimal Price { get; set; } = 0;
    }
    public class OptionEnum
    {
        public string Label { get; set; } = "";
        public List<Choice> Choices { get; set; } = new List<Choice>();
    }
}
