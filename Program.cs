using System;

namespace RomanNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "test")
            {
                Console.WriteLine(Test.First() ? "\u001b[92mArabic to Roman: Pass...\u001b[0m" : "\u001b[91mArabic to Roman: Fail...\u001b[0m");
                Console.WriteLine(Test.Second() ? "\u001b[92mRoman to Arabic: Pass...\u001b[0m" : "\u001b[91mRoman to Arabic: Fail...\u001b[0m");
            }
            else
            {
                Console.WriteLine("\u001b[93mUsage:\n\t\u001b[31mR \u001b[93m- \u001b[33mRoman to Arabic Numerals,\n\t\u001b[31mA \u001b[93m- \u001b[33mArabic to Roman Numerals,\n\t\u001b[31mQ \u001b[93m- \u001b[33mQuit.\u001b[0m");

                bool exit = false;

                while (!exit)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.R:
                            Console.WriteLine("\u001b[93mEnter Roman Numeral to convert.\u001b[0m");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            var roman = Console.ReadLine().ToUpper();
                            Console.ResetColor();

                            if (roman.TryToArabic(out int arabic, true))
                            {
                                Console.WriteLine($"\u001b[93mRoman: \u001b[33m{roman} : \u001b[93mArabic: \u001b[92m{arabic}\u001b[0m");
                            }
                            break;
                        case ConsoleKey.A:
                            Console.WriteLine("\u001b[93mEnter Arabic Numeral to convert.\u001b[0m");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            var line = Console.ReadLine();
                            Console.ResetColor();

                            if (int.TryParse(line, out int arabic2))
                            {
                                if (arabic2.TryToRoman(out string roman2, true))
                                {
                                    Console.WriteLine($"\u001b[93mArabic: \u001b[33m{arabic2} : \u001b[93mRoman: \u001b[92m{roman2}\u001b[0m");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"\u001b[31mError! Invalid Arabic numeral (\u001b[94m{line}\u001b[31m).\u001b[0m");
                            }
                            break;
                        case ConsoleKey.Q:
                            exit = true;
                            break;
                    }
                }
            }
        }
    }
}
