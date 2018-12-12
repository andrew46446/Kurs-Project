using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ude;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Kurs_Project_Final
{
    public class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Show_Menu();
                string answer = Console.ReadLine();

                if (answer.Equals("1"))
                {
                    Console.Clear();
                    Get_Text_To_Decode(out string text, out int length);
                    Decode(length, text, out string decode_text);
                    Console.WriteLine("Хотите ли вы Сохранить указанный файл? 1-да, любая другая клавиша - нет");
                    answer = Console.ReadLine();
                    if (answer.Equals("1"))
                    {
                        Save(decode_text);
                    }
                    answer = "";
                    Console.ReadKey();
                }
                if (answer.Equals("2"))
                {
                    Console.Clear();
                    Text_and_Check_Code_Direction(out int q, out string direction, out string text);
                    Сode(q, direction, text, out string code_text);
                    Console.WriteLine("Хотите ли вы Сохранить данный файл? 1-да, любая другая клавиша - нет");
                    answer = Console.ReadLine();
                    if (answer.Equals("1"))
                    {
                        Save(code_text);
                    }
                    answer = "";
                    Console.ReadKey();
                }
                if (answer.Equals("3")) { break; }
            }
        }

        static void Show_Menu()
        {
            Console.Clear();
            Console.WriteLine("Добрый день ! Это-дешифратор ver 1.0. Выберете необходимое действие");
            Console.WriteLine("1. Дешифровать пользовательский файл.");
            Console.WriteLine("2. Зашифровать пользовательский текст.");
            Console.WriteLine("3. Выход");
        }

        static void Get_Text_To_Decode(out string text, out int length)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Укажите путь к файлу");
                    string path = Console.ReadLine();
                    length = File.ReadAllText(path).Length;
                    text = GetCode(path);
                    Encoding encoding = Encoding.GetEncoding(text == "0" ? 65001 : 1251);
                    text = File.ReadAllText(path, encoding);
                    break;
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine("Упс, возникла ошибка, попробуйте еще раз!\n {0}\n", ex.Message);
                }
            }
        }

        public static void Decode(int length, string text, out string decode_text)
        {
            Console.Clear();
            int q = 31;
            decode_text = "";
            Console.WriteLine("Первоначальный текст: {0}", text);
            for (int i = 0; i < length; i++)
            {
                decode_text = decode_text + Algorithm(q, text[i], "+");
            }
            Console.WriteLine("Смещение: {0}, Текст: {1}", q, decode_text);
        }

        public static char Algorithm(int q, char letter, string direction)
        {
            int j;
            string alfabet_Low = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            string alfabet_Upp = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
            string alfabet;

            if (alfabet_Low.Contains(letter) || alfabet_Upp.Contains(letter))
            {
                alfabet = Char.IsLower(letter) ? alfabet_Low : alfabet_Upp;

                if (direction.Equals("+"))
                {
                    j = alfabet.IndexOf(letter) + q <= 32 ? alfabet.IndexOf(letter) + q : alfabet.IndexOf(letter) + q - 33;
                    return alfabet[j];
                }
                else
                {
                    j = alfabet.IndexOf(letter) - q >= 0 ? alfabet.IndexOf(letter) - q : 33 + alfabet.IndexOf(letter) - q;
                    return alfabet[j];
                }
            }
            else return letter;
        }

        public static void Сode(int q, string direction, string text, out string code_text)
        {
            code_text = "";
            int length = text.Length;

            for (int i = 0; i < length; i++)
            {
                code_text = code_text + Algorithm(q, text[i], direction);
            }
            Console.WriteLine("Итог: " + code_text);

        }

        static void Text_and_Check_Code_Direction(out int q, out string direction, out string text)
        {
            string regex = @"(\+|\-)[0-3][0-9]\b|((\+|\-)[0-9])\b";
            Console.WriteLine("Введите текст, который необходимо зашифровать");
            text = Console.ReadLine();

            Console.WriteLine("Укажите необходимое смещение и его напраление. Пример: -5 ");
            while (true)
            {
                string answer = Console.ReadLine();
                if (Regex.IsMatch(answer, regex))

                {
                    direction = answer[0].ToString();
                    if (answer.Length == 2)
                    {
                        q = Int32.Parse(answer[1].ToString());
                        break;
                    }
                    else
                    {
                        q = Int32.Parse(answer[1].ToString()) * 10 + Int32.Parse(answer[2].ToString());
                        if (q <= 33)
                        {
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Некорректный ввод, попробуйте еще раз. Не забывайте, что можно задавать смещение только от 0 до 33.");
                        }
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Некорректный ввод, попробуйте еще раз. Не забывайте, что можно задавать смещение только от 0 до 33.");
                }
            }
        }

        public static void Save(string text)
        {
            Console.Clear();
            while (true)
            {
                try
                {
                    Console.WriteLine("Укажите путь к файлу и его имя, например: D:\\example\\text.txt");
                    string path = Console.ReadLine();
                    if (Path.GetExtension(path) == ".txt")
                    {
                        File.WriteAllText(path, text);
                        Console.WriteLine("Файл успешно сохранен по адресу: {0}\nДля продолжения нажмите любую клавишу", path);
                        break;

                    }
                    else
                    {
                        if (path.Length == 0)
                        {
                            Console.WriteLine("Пустое имя файла! Попробуйте еще раз!");
                        }
                        else
                        {
                            path = path + ".txt";
                            File.WriteAllText(path, text);
                            Console.WriteLine("Файл успешно сохранен по адресу: {0}\nДля продолжения нажмите любую клавишу", path);
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine("Упс, возникла ошибка, попробуйте еще раз!\n {0}", ex.Message);
                }
            }

        }

        static string GetCode(string path)
        {
            using (FileStream fs = File.OpenRead(path))
            {
                Ude.CharsetDetector cdet = new Ude.CharsetDetector();
                cdet.Feed(fs);
                cdet.DataEnd();
                string ans;
                if (cdet.Charset.Contains("UTF"))
                {
                    ans = "0";
                    return (ans);
                }
                else if (cdet.Charset.Contains("windows"))
                {
                    ans = "1";
                    return (ans);
                }
                return ("0");
            }
        }
    }
}

