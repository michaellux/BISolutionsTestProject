using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace BISolutionsTestProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public string AllNumbers { get; set; }
        public string OddNumbers { get; set; }
        public string OddEverySecond { get; set; }
        public string Sum { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public void OnPostAsync()
        {
            string inpNumbers = Request.Form["numbers"];
            List<int> numbers = new();

            var strNumbers = inpNumbers.Split(',');

            foreach (string num in strNumbers)
            {
                numbers.Add((int)Convert.ToInt64(num));
            }

            AllNumbers = string.Join(",", numbers);

            List<int> oddNumbers = numbers.Where((num) => num % 2 != 0).ToList();
            OddNumbers = string.Join(",", oddNumbers);

            List<int> everySecond = oddNumbers.Where((x, i) => (i + 1) % 2 == 0).ToList();
            OddEverySecond = string.Join(",", everySecond);

            int sum = Math.Abs(everySecond.Sum());
            Sum = $"{sum}";
        }
    }
}