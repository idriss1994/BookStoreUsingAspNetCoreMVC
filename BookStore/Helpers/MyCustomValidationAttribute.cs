using System.ComponentModel.DataAnnotations;

namespace BookStore.Helpers
{
    public class MyCustomValidationAttribute : ValidationAttribute
    {
        string _text;
        public MyCustomValidationAttribute(string text)
        {
            _text = text;
        }
        public string Text { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value != null)
            {
                string bookName = value as string;
                if (bookName.Contains(_text))
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult(ErrorMessage ?? $"The name of the book does not contain  the value {_text}!");
        }
    }
}
