using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListLongWords
{
    class Program
    {
        //Т.к. в тексте могут быть знаки препинания, а мы разделили его по пробелам, нам нужно проверить, если ли у нашего слова знаки препинания
        static string ProcessingWords(string word)
        {
            var result = new StringBuilder();
            //Бежим по строке. Если данный символ буква, добавляем его к новой строке. Потом просто возвращаем нашу новую строку без лишних символов
            for (int i = 0; i< word.Length; i++)
                if (char.IsLetter(word[i]))
                    result.Append(word[i]);
            return result.ToString();
        }

        static void Main()
        {
            //Вводим путь до файла, из которого хотим считать данные.
            Console.WriteLine("Введите имя файла:");
            var path = Console.ReadLine();
            Console.WriteLine("Введите минимальную длину слова:");
            var lengthWord = int.Parse(Console.ReadLine());
            string text = " ";
            //Считываем данные из файла.
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    text = sr.ReadToEnd();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Файл не найден");
                Console.WriteLine(e.Message);
            }

            var words = text.Split(' ');
            var list = new List<string>();
            //Бежим по массиву и убираем лишнии знаки в словах
            //После проверяем, подходит нам это слово или нет, если да, то добавляем его в наш список
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = ProcessingWords(words[i]);
                if (words[i].Length >= lengthWord)
                    list.Add(words[i]);
            }
            var countWords = list.Count();

            try
            {
                //Создаем или перезаписываем файл. Т.к. мы просто записали имя и расширение пары, то файл будет создан в папке Debug  
                using (StreamWriter sr = new StreamWriter("result.txt", false))
                {
                    //Записываем инфу в наш файл. Одна строка - одно слово
                    foreach (var element in list)
                        sr.WriteLine(element);
                }
            }
            catch { }

            //Выводим количество полученных слов
            Console.WriteLine("Всего значений:" + countWords);
        }
    }
}
