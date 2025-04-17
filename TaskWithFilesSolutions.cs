using System.Xml.Serialization;
using System;

namespace ProjectFileTasks
{
    /// <summary>
    /// Класс для методов решения задач 1 - 5
    /// </summary>
    class TaskWithFilesSolutions
    {
        public static double arithmeticMeanElements(string fileName)
        {
            double sum = 0;
            int count = 0;

            StreamReader reader = new StreamReader(fileName);
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] numbers = line.Split(' ');

                for (int i = 0; i < numbers.Length; i++)
                {
                    if (double.TryParse(numbers[i], out double result))
                    {
                        sum += result;
                        count++;
                    }
                }
            }
            reader.Close();

            if (count > 0)
            {
                return sum / count;
            }
            return 0;
        }

        public static double productOddElements(string fileName)
        {
            double mul = 1;
            bool hasOddNumber = false;  //флаг для проверки наличия нечетных чисел в файле

            StreamReader reader = new StreamReader(fileName);
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] numbers = line.Split(' ');

                for (int i = 0; i < numbers.Length; i++)
                {
                    if (long.TryParse(numbers[i], out long result) && result % 2 != 0)
                    {
                        mul *= result;
                        hasOddNumber = true;
                    }
                }
            }
            reader.Close();
            if (hasOddNumber)
            {
                return mul;
            }
            else return 0;
        }

        public static void linesWithoutLetters(string inputFile, string outputFile)
        {
            StreamReader reader = new StreamReader(inputFile);
            StreamWriter writer = new StreamWriter(outputFile);
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                bool hasLetters = false;    //флаг для отслеживания наличия букв в строке

                foreach (char c in line)
                {
                    if (char.IsLetter(c))   //если символ это буква
                    {
                        hasLetters = true;
                        break;
                    }
                }

                if (!hasLetters)
                {
                    writer.WriteLine(line);
                }
            }
            reader.Close();
            writer.Close();
        }

        /// <summary>
        /// Находит максимальное значение по модулю нечетного элемента в бинарном файле.
        /// </summary>
        /// <param name="filePath"> Путь к бинарному файлу, содержащему целые числа. </param>
        /// <returns> Возвращает максимальное значение модуля нечетного элемента, найденного на нечетных позициях </returns>
        public static int findMaxOddComponent(string filePath)
        {
            int maxOddValue = int.MinValue;
            BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open));
            int index = 1;

            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                int number = reader.ReadInt32();

                if (index % 2 != 0)
                {
                    int absValue = Math.Abs(number);
                    if (absValue > maxOddValue)
                    {
                        maxOddValue = number;
                    }
                }
                index++;
            }
            reader.Close();

            return maxOddValue;
        }

        public static void findToysForSpecificYears(string filePath)
        {
            Toy[] toyList;

            //десериализация игрушек
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Toy[]));
                toyList = (Toy[])serializer.Deserialize(fileStream);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при десериализации: {ex.Message}");
                return;
            }
            fileStream.Close();

            Console.WriteLine("\nИгрушки для детей 4-5 лет:");

            foreach (var toy in toyList)
            {
                if (toy.MinAge <= 4 && toy.MaxAge >= 5)
                {
                    Console.Write(toy.Name + " ");
                }
            }

            Console.WriteLine();
            Console.WriteLine(new string('-', 80));
        }
    }
}