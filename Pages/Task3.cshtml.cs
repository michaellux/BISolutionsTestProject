using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BISolutionsTestProject.Pages
{
    public class Task3Model : PageModel
    {
        public string Word { get; set; }
        public string IsPalindrome { get; set; }

        public void OnGet()
        {
            IsPalindrome = "Пока неизвестно";
        }

        public void OnPostAsync()
        {
            string text = Request.Form["text"];
            Word = text;

            IsPalindrome = DefinePalindrome(text) ? "Да" : "Нет";
        }

        private bool DefinePalindrome(string text)
        {
            text = text.Replace(" ", "");

            if (text.Length <= 1)
            {
                return true;
            }

            char firstSymbol = text[0];
            char lastSymbol = text[text.Length - 1];

            if (char.ToLower(firstSymbol) != char.ToLower(lastSymbol))
            {
                return false;
            }

            string stringWithoutFirstAndLastSymbols = text.Substring(1, text.Length - 2);
            return DefinePalindrome(stringWithoutFirstAndLastSymbols);
        }
    }
}
