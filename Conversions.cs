using System;

namespace RomanNumbers
{
    public static class Conversions
    {
        public static string ToRoman(this int arabic)
        {
            return arabic switch
            {
                var temp when temp > 0 && temp < 4000 => ToRomanInternal(arabic),
                var temp when temp < 0 => throw new ArgumentException($"\u001b[31mThe number \u001b[94m{arabic}\u001b[31m is too small to convert.\u001b[0m", "\u001b[32marabic\u001b[0m"),
                var temp when temp >= 4000 => throw new ArgumentException($"\u001b[31mThe number \u001b[94m{arabic}\u001b[31m is too large to convert.\u001b[0m", "\u001b[32marabic\u001b[0m"),
                0 => throw new ArgumentException("\u001b[31mThe Roman zero symbol does not exist. There are records that in some cases the letter \"\u001b[92mN\u001b[31m\" was used.\u001b[0m", "\u001b[32marabic\u001b[0m"),
                _ => throw new ArgumentException($"\u001b[31mThe Arabic number (\u001b[94m{arabic}\u001b[31m) is not valid.\u001b[0m", "\u001b[32marabic\u001b[0m")
            };
        }

        public static bool TryToRoman(this int arabic, out string roman, bool printError = false)
        {
            try
            {
                roman = ToRoman(arabic);
            }
            catch (Exception e)
            {
                if (printError)
                {
                    Console.WriteLine(e.Message);
                }

                roman = "";
            }

            return (arabic > 0 && arabic < 4000);
        }

        private static string ToRomanInternal(int arabic)
        {
            return arabic switch
            {
                var temp when temp >= 1000 => "M" + ToRomanInternal(temp - 1000),
                var temp when temp >= 900 => "CM" + ToRomanInternal(temp - 900),
                var temp when temp >= 500 => "D" + ToRomanInternal(temp - 500),
                var temp when temp >= 400 => "CD" + ToRomanInternal(temp - 400),
                var temp when temp >= 100 => "C" + ToRomanInternal(temp - 100),
                var temp when temp >= 90 => "XC" + ToRomanInternal(temp - 90),
                var temp when temp >= 50 => "L" + ToRomanInternal(temp - 50),
                var temp when temp >= 40 => "XL" + ToRomanInternal(temp - 40),
                var temp when temp >= 10 => "X" + ToRomanInternal(temp - 10),
                var temp when temp >= 9 => "IX" + ToRomanInternal(temp - 9),
                var temp when temp >= 5 => "V" + ToRomanInternal(temp - 5),
                var temp when temp >= 4 => "IV" + ToRomanInternal(temp - 4),
                var temp when temp >= 1 => "I" + ToRomanInternal(temp - 1),
                0 => "",
                _ => throw new ArgumentException("\u001b[31mThe number contains digits that cannot be converted...\u001b[0m", "\u001b[32marabic\u001b[0m")
            };
        }

        public static int ToArabic(this string roman)
        {
            int temp;

            try
            {
                temp = ToArabicInternal(roman);
            }
            catch
            {
                throw;
            }

            return temp switch
            {
                var t when t > 0 && t < 4000 => temp,
                var t when t < 0 => throw new ArgumentException($"\u001b[31mThe number \u001b[94m{roman}\u001b[31m is too small to convert.\u001b[0m", "\u001b[32mroman\u001b[0m"),
                var t when t >= 4000 => throw new ArgumentException($"\u001b[31mThe number \u001b[94m{roman}\u001b[31m is too large to convert.\u001b[0m", "\u001b[32mroman\u001b[0m"),
                0 => throw new ArgumentException("\u001b[31mThe Roman zero symbol does not exist. There are records that in some cases the letter \"\u001b[92mN\u001b[31m\" was used.\u001b[0m", "\u001b[32mroman\u001b[0m"),
                _ => throw new ArgumentException($"\u001b[31mThe Roman number (\u001b[94m{roman}\u001b[31m) is not valid.\u001b[0m", "\u001b[32mroman\u001b[0m")
            };
        }
        
        public static bool TryToArabic(this string roman, out int arabic, bool printError = false)
        {
            try
            {
                arabic = ToArabic(roman);
            }
            catch (Exception e)
            {
                if (printError)
                {
                    Console.WriteLine(e.Message);
                }

                arabic = 0;
            }

            return arabic > 0 && arabic < 4000;
        }

        public static int ToArabicInternal(string roman, int last = 1000)
        {
            return roman switch
            {
                "" => 0,
                var temp when (temp.Length >= 4 && (temp[0..4] == "MMMM" || temp[0..4] == "CCCC" || temp[0..4] == "XXXX" || temp[0..4] == "IIII")) => throw new ArgumentException("\u001b[31mThe number contains letters that cannot be converted...\u001b[0m", "\u001b[32mroman\u001b[0m"),
                var temp when (temp.Length >= 2 && temp[0..2] == "CM" && last == 1000) => 900 + ToArabicInternal(temp[2..], 900),
                var temp when (temp[0] == 'M' && last == 1000) => 1000 + ToArabicInternal(temp[1..], 1000),
                var temp when (temp.Length >= 2 && temp[0..2] == "CD" && last == 1000) => 400 + ToArabicInternal(temp[2..], 400),
                var temp when (temp[0] == 'D' && last == 1000) => 500 + ToArabicInternal(temp[1..], 500),
                var temp when (temp.Length >= 2 && temp[0..2] == "XC" && last >= 100) => 90 + ToArabicInternal(temp[2..], 90),
                var temp when (temp[0] == 'C' && last >= 100 && last != 400 && last != 900) => 100 + ToArabicInternal(temp[1..], 100),
                var temp when (temp.Length >= 2 && temp[0..2] == "XL" && last >= 100) => 40 + ToArabicInternal(temp[2..], 40),
                var temp when (temp[0] == 'L' && last >= 100) => 50 + ToArabicInternal(temp[1..], 50),
                var temp when (temp.Length >= 2 && temp[0..2] == "IX" && last >= 10) => 9 + ToArabicInternal(temp[2..], 9),
                var temp when (temp[0] == 'X' && last >= 10 && last != 40 && last != 90) => 10 + ToArabicInternal(temp[1..], 10),
                var temp when (temp.Length >= 2 && temp[0..2] == "IV" && last >= 10) => 4 + ToArabicInternal(temp[2..], 4),
                var temp when (temp[0] == 'V' && last >= 10) => 5 + ToArabicInternal(temp[1..], 5),
                var temp when (temp[0] == 'I' && last >= 1 && last != 4 && last != 9) => 1 + ToArabicInternal(temp[1..], 1),
                "N" => 0,
                _ => throw new ArgumentException("\u001b[31mThe number contains letters that cannot be converted...\u001b[0m", "\u001b[32mroman\u001b[0m")
            };
        }
    }
}
