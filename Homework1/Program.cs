using System;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Write("Введите первую строку (exit для выхода): ");
            string str1Param = Console.ReadLine();

            if (str1Param == "exit")
                break;

            Console.Write("Введите вторую строку: ");
            string str2Param = Console.ReadLine();

            // ======= Начало «Distance», но прямо внутри Main =======

            if (str1Param == null || str2Param == null)
            {
                Console.WriteLine("Строка не должна быть null");
                Console.WriteLine();
                continue;
            }

            int str1Len = str1Param.Length;
            int str2Len = str2Param.Length;

            if (str1Len == 0 && str2Len == 0)
            {
                Console.WriteLine("Расстояние Дамерау-Левенштейна: 0");
                Console.WriteLine();
                continue;
            }

            if (str1Len == 0)
            {
                Console.WriteLine("Расстояние Дамерау-Левенштейна: " + str2Len);
                Console.WriteLine();
                continue;
            }

            if (str2Len == 0)
            {
                Console.WriteLine("Расстояние Дамерау-Левенштейна: " + str1Len);
                Console.WriteLine();
                continue;
            }

            int[,] matrix = new int[str1Len + 1, str2Len + 1];

            for (int i = 0; i <= str1Len; i++)
                matrix[i, 0] = i;

            for (int j = 0; j <= str2Len; j++)
                matrix[0, j] = j;

            for (int i = 1; i <= str1Len; i++)
            {
                for (int j = 1; j <= str2Len; j++)
                {
                    int cost = (str1Param[i - 1] == str2Param[j - 1]) ? 0 : 1;

                    int deletion = matrix[i - 1, j] + 1;
                    int insertion = matrix[i, j - 1] + 1;
                    int substitution = matrix[i - 1, j - 1] + cost;

                    matrix[i, j] = Math.Min(Math.Min(deletion, insertion), substitution);

                    if (i > 1 && j > 1 &&
                        str1Param[i - 1] == str2Param[j - 2] &&
                        str1Param[i - 2] == str2Param[j - 1])
                    {
                        matrix[i, j] = Math.Min(matrix[i, j], matrix[i - 2, j - 2] + 1);
                    }
                }
            }

            int d = matrix[str1Len, str2Len];

            // ======= Конец «Distance» =======

            Console.WriteLine("Расстояние Дамерау-Левенштейна: " + d);
            Console.WriteLine();
        }
    }
}
