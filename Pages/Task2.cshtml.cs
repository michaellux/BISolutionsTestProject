using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace BISolutionsTestProject.Pages
{
    public class Task2Model : PageModel
    {
        public string AllNumbers { get; set; }
        public string Input1 { get; set; }
        public string Input2 { get; set; }
        public string Output { get; set; }

        public void OnGet()
        {
        }

        public void OnPostAsync()
        {
            string firstNumber = Request.Form["first"];
            string secondNumber = Request.Form["second"];

            int first = (int)Convert.ToInt64(firstNumber);
            int second = (int)Convert.ToInt64(secondNumber);
            AllNumbers = $"{firstNumber},{secondNumber}";

            List<int> firstDigitsList = GetIntArray(first);
            List<int> secondDigitsList = GetIntArray(second);

            firstDigitsList.Reverse();
            secondDigitsList.Reverse();

            LinkedList<int> firstLinkedList = new LinkedList<int>(firstDigitsList);
            LinkedList<int> secondLinkedList = new LinkedList<int>(secondDigitsList);

            Input1 = CreateViewLinkedList(Input1, firstLinkedList);
            Input2 = CreateViewLinkedList(Input2, secondLinkedList);

            Output = "0";

            var currentFirstListNode = firstLinkedList.First;
            var currentSecondListNode = secondLinkedList.First;

            LinkedList<int> resultList = SumLinkedList(currentFirstListNode, currentSecondListNode, 0, null, null);

            Output = CreateViewLinkedList(Output, resultList);
        }

        private List<int> GetIntArray(int num)
        {
            List<int> listOfInts = new List<int>();
            while (num > 0)
            {
                listOfInts.Add(num % 10);
                num = num / 10;
            }
            listOfInts.Reverse();
            return listOfInts;
        }

        private string CreateViewLinkedList(string io, LinkedList<int> list)
        {
            io = "";
            foreach (var digit in list)
            {
                io = string.Concat(io, $"{digit}");
                if (digit != list.Last.Value)
                {
                    io = string.Concat(io, "=>");
                }
            }

            return io;
        }

        public LinkedList<int>? SumLinkedList(LinkedListNode<int> firstlistNode, LinkedListNode<int> secondlistNode, int carry, LinkedList<int>? resultList, LinkedListNode<int>? lastAddedNode)
        {
            if (firstlistNode == null && secondlistNode == null && carry == 0)
            {
                return resultList;
            }

            int sum = carry;

            if (firstlistNode != null)
            {
                sum += firstlistNode.Value;
            }

            if (secondlistNode != null)
            {
                sum += secondlistNode.Value;
            }

            LinkedList<int> currentResultList;
            LinkedListNode<int> lastNode;
            if (resultList == null)
            {
                currentResultList = new LinkedList<int>();
                lastNode = currentResultList.AddFirst(sum % 10);
            }
            else
            {
                currentResultList = resultList;
                lastNode = currentResultList.AddAfter(lastAddedNode, sum % 10);
            }

            if (firstlistNode != null || secondlistNode != null)
            {                
                return SumLinkedList(
                firstlistNode == null ? null : firstlistNode.Next,
                secondlistNode == null ? null : secondlistNode.Next,
                sum >= 10 ? 1 : 0,
                currentResultList,
                lastNode);
            }

            return resultList;
        }
    }
}
