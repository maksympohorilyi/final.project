using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Final1
{
    class Program
    {
        static Dictionary<char, int> ParseWords(string str) // розбір рядка на букви без символів
        {
            Dictionary<char, int> openWith = new Dictionary<char, int>();

            for (int i = 0; i < str.Length; i++)
            {
                if (Char.IsLetter(str, i))
                {
                    int value = 0;
                    if (openWith.TryGetValue(str[i], out value))
                    {
                        openWith[str[i]] = openWith[str[i]] + 1;
                    }
                    else
                    {
                        openWith.Add(str[i], 1);
                    }
                }
            }
            return openWith;
        }

        static List<string> Vocabulary() // зчитування слів із файлу словника
        {
            List<string> items = new List<string>();

            foreach (string line in System.IO.File.ReadLines(@"C:\Users\ADM\OneDrive\Документи\Project\Final1\Final1\vocabulary.txt"))
            {
                items.Add(line);
            }
            return items;
        }
        
        static void Main(string[] args)
        {
            Console.WriteLine("Перед продовженням впевнiться, що словник заповнений!");
            Console.WriteLine("Введiть перевiрочний рядок: ");

            List<string> vocabulary = Vocabulary(); // ініциалізація словника

            string inputText = Convert.ToString(Console.ReadLine());
            List<string> result = new List<string>();

            Dictionary<char, int> openWith = ParseWords(inputText); // розбір вхідного рядка на символи
            Dictionary<char, int> parsedWord = new Dictionary<char,int>(); // розібрані букви зі словника

            foreach (string word in vocabulary) // перебір слів зі словника
            {
                parsedWord = ParseWords(word); // розбір слова на букви та їх кількість
                bool check = false;
                

                for (int index = 0; index < parsedWord.Count; index++)
                {
                    var item = parsedWord.ElementAt(index); // зберігання букви та кількості

                    if (!openWith.ContainsKey(item.Key)) // перевірка слова на вміст букви з рядка
                    {
                        check = false;
                        break;
                    }
                    if (item.Value > openWith[item.Key]) // перевірка кількості букв
                    {
                        check = false;                        
                        break;
                    }
                    check = true;
                }
                if (check)
                {
                    result.Add(word);
                }
            }

            Console.WriteLine("Слова, якi вдалось створити: ");
            foreach (string word in result)
            {
                
                Console.WriteLine(word);
            }
            
            Console.WriteLine("Кiлькiсть створених слiв: {0}", result.Count);

        }
    }
}
