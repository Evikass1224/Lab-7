using System.Xml.Serialization;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

namespace ProjectFileTasks
{
    /// <summary>
    /// Класс для выбора и обработки заданий, ввода и проверки данных.
    /// </summary>
    class TaskSelection
    {
        public void inputNumberTask(out bool exitFlag)
        {
            exitFlag = false;

            while (true)
            {
                Console.WriteLine("\nВыберите задание (1-5) или введите 0 для выхода:");
                for (int i = 1; i <= 10; i++)
                {
                    Console.WriteLine($"Задание {i}");
                }

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            dataEntryTask1();
                            break;
                        case 2:
                            dataEntryTask2();
                            break;
                        case 3:
                            dataEntryTask3();
                            break;
                        case 4:
                            dataEntryTask4();
                            break;
                        case 5:
                            dataEntryTask5();
                            break;
                        case 6:
                            dataEntryTask6();
                            break;
                        case 7:
                            dataEntryTask7();
                            break;
                        case 8:
                            dataEntryTask8();
                            break;
                        case 9:
                            dataEntryTask9();
                            break;
                        case 10:
                            dataEntryTask10();
                            break;
                        case 0:
                            exitFlag = true;
                            return;
                        default:
                            Console.WriteLine("\nНеверный выбор. Пожалуйста, попробуйте еще раз.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\nПожалуйста, введите корректное число.");
                }
            }
        }

        private string InputFileName()
        {
            string fileName = string.Empty;
            bool isInputValid = false; //флаг для отслеживания корректности ввода

            while (!isInputValid)
            {
                try
                {
                    fileName = Console.ReadLine();
                    StreamWriter writer = new StreamWriter(fileName);
                    isInputValid = true;
                    writer.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"\nОшибка: {e.Message}. Попробуйте снова.");
                }
            }

            return fileName;
        }

        private int getPositiveInteger(string prompt)
        {
            int number;
            while (true)
            {
                Console.WriteLine(prompt);
                if (int.TryParse(Console.ReadLine(), out number) && number > 0)
                {
                    return number;
                }
                Console.WriteLine("\nОшибка! Пожалуйста, введите положительное целое число.");
            }
        }

        private LinkedList<char> inputLinkedList(string prompt)
        {
            LinkedList<char> linkedList = new LinkedList<char>();
            bool validInput = false;

            while (!validInput)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("\nОшибка! Данные не были введены. Пожалуйста, попробуйте еще раз.");
                    continue;
                }

                string[] items = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                validInput = true;

                foreach (string item in items)
                {
                    linkedList.AddLast(item[0]);
                }
            }
            return linkedList;
        }

        private void validateIndicesLinkedList(LinkedList<char> list, out int i, out int j)
        {
            i = -1;
            j = -1;
            bool isValidInput = false;

            while (!isValidInput)
            {
                isValidInput = true;

                Console.WriteLine("\nВведите индекс i (начальный):");
                if (!int.TryParse(Console.ReadLine(), out i) || i <= 0 || i > list.Count)
                {
                    Console.WriteLine("Недопустимый индекс i. Попробуйте еще раз.");
                    isValidInput = false;
                }

                if (isValidInput)
                {
                    Console.WriteLine("\nВведите индекс j (конечный):");
                    if (!int.TryParse(Console.ReadLine(), out j) || j <= 0 || j > list.Count || j < i)
                    {
                        Console.WriteLine("Некорректный индекс j. Попробуйте еще раз.");
                        isValidInput = false;
                    }
                }
            }
        }

        private List<string> inputListValidation(string prompt)
        {
            List<string> list = new List<string>();

            Console.WriteLine(prompt);
            string input = Console.ReadLine();
            list.Clear();
            string[] inputs = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string item in inputs)
            {
                if (!list.Contains(item))
                {
                    list.Add(item);
                }
            }
            return list;
        }

        private HashSet<string> inputAllChocolates()
        {
            HashSet<string> chocolates = new HashSet<string>();
            bool inputIsValid = false;  //флаг для отслеживания корректности ввода

            while (!inputIsValid)
            {
                Console.WriteLine("\nВведите путь к файлу с доступными шоколадками:");
                string filePath = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(filePath))
                {
                    Console.WriteLine("Пожалуйста, введите корректный путь к файлу.");
                    continue;
                }

                StreamReader reader = null;

                try
                {
                    reader = new StreamReader(filePath);
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        int startIndex = 0;
                        int commaIndex;       //индекс запятой

                        //добавление шоколадки в набор
                        while ((commaIndex = line.IndexOf(',', startIndex)) != -1)
                        {
                            string chocolate = line.Substring(startIndex, commaIndex - startIndex).Trim();

                            if (!string.IsNullOrWhiteSpace(chocolate))
                            {
                                chocolates.Add(chocolate);
                            }

                            startIndex = commaIndex + 1;
                        }

                        string lastChocolate = line.Substring(startIndex).Trim();
                        if (!string.IsNullOrWhiteSpace(lastChocolate))
                        {
                            chocolates.Add(lastChocolate);
                        }
                    }
                    inputIsValid = true;
                    reader.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"\nОшибка: {e.Message}. Пожалуйста, попробуйте снова.");
                }
            }
            return chocolates;
        }


        /// <summary>
        /// Метод для ввода предпочтений шоколадок у конкретных сладкоежек.
        /// </summary>
        /// <param name="numberSweetTooth">Количество сладкоежек.</param>
        /// <param name="allChocolates">Набор всех шоколадок.</param>
        /// <returns>Словарь, в котором ключи - названия шоколадок, а значения - наборы индексов сладкоежек, предпочитающих эти шоколадки.</returns>
        private Dictionary<string, HashSet<int>> inputChocolatePreferences(int numberSweetTooth, HashSet<string> allChocolates)
        {
            //словарь для хранения предпочтений
            Dictionary<string, HashSet<int>> chocolatePreferences = new Dictionary<string, HashSet<int>>();
            List<string> chocolateList = allChocolates.ToList();

            Console.WriteLine("\nДоступные шоколадки:");
            int chocolateIndex = 1;

            foreach (var chocolate in chocolateList)
            {
                Console.WriteLine($"{chocolateIndex}) {chocolate}");
                chocolateIndex++;
            }

            for (int i = 0; i < numberSweetTooth; i++)
            {
                bool validInput = false;

                while (!validInput)
                {
                    Console.WriteLine($"\nВвод предпочтений для сладкоежки {i + 1} (введите номера шоколадок через пробел):");
                    string input = Console.ReadLine();
                    string[] inputNumbers = input.Split(' ');

                    validInput = true;      //флаг для отслеживания валидности ввода

                    foreach (var number in inputNumbers)
                    {
                        if (int.TryParse(number.Trim(), out int index) && index > 0 && index <= chocolateList.Count)
                        {
                            string chosenChocolate = chocolateList[index - 1]; //название шоколадки по индексу

                            if (!chocolatePreferences.ContainsKey(chosenChocolate))
                            {
                                chocolatePreferences[chosenChocolate] = new HashSet<int>();
                            }

                            chocolatePreferences[chosenChocolate].Add(i);
                        }
                        else
                        {
                            Console.WriteLine($"\n'{number}' не является корректным номером или находится вне диапазона. \nПопробуйте снова.");
                            validInput = false;
                            break;
                        }
                    }
                }
            }
            return chocolatePreferences;
        }

        private void randomFillBinaryFile(string filePath, int count)
        {
            Random random = new Random();
            BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create));

            for (int i = 0; i < count; i++)
            {
                int number = random.Next(-1000, 1000);
                writer.Write(number);
            }
            writer.Close();
        }

        private void dataEntryTask1()
        {
            Console.WriteLine("\n--- Среднее арифметическое элементов в файле ---");
            Console.WriteLine("\nВведите имя файла:");
            string fileName = InputFileName();

            int number = getPositiveInteger("\nВведите количество случайных целых чисел для заполнения файла: ");
            Random random = new Random();
            StreamWriter writer = null;

            try
            {
                writer = new StreamWriter(fileName);

                for (int i = 0; i < number; i++)
                {
                    int randomNumber = random.Next(-100, 100);
                    writer.WriteLine(randomNumber);
                }
                writer.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"\nОшибка: {e.Message}.");
            }

            double average = TaskWithFilesSolutions.arithmeticMeanElements(fileName);
            Console.WriteLine($"\nСреднее арифметическое: {average}");
        }

        private void dataEntryTask2()
        {
            Console.WriteLine("\n--- Произведение нечётных элементов в файле ---");
            Console.WriteLine("\nВведите имя файла:");
            string fileName = InputFileName();
            int number = getPositiveInteger("\nВведите количество случайных целых чисел для заполнения файла:");
            Random random = new Random();
            StreamWriter writer = null;

            try
            {
                writer = new StreamWriter(fileName);

                for (int i = 0; i < number; i++)
                {
                    int randomNumber = random.Next(-100, 100);
                    writer.Write(randomNumber);

                    if (i < number - 1)
                    {
                        writer.Write(" ");
                    }
                }
                writer.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"\nОшибка: {e.Message}.");
            }

            double mul = TaskWithFilesSolutions.productOddElements(fileName);
            Console.WriteLine($"\nПроизведение нечётных элементов: {mul}");
        }

        private void dataEntryTask3()
        {
            Console.WriteLine("\n--- Строки, в которых нет букв из исходного в новый ---");

            //ввод первого и второго файла и заполнение первого
            Console.WriteLine("\nВведите имя исходного текстового файла: ");
            string inputFile = InputFileName();

            Console.WriteLine("\nВведите имя текстового файла, в который запишутся строки без букв: ");
            string outputFile = InputFileName();

            bool isInputValid = false; //флаг для отслеживания корректности ввода

            while (!isInputValid)
            {
                StreamWriter writer = null;

                try
                {
                    writer = new StreamWriter(inputFile, false);
                    Console.WriteLine("\nВведите текст для записи в файл (для завершения ввода оставьте строку пустой и нажмите Enter):");
                    string text;
                    while (!string.IsNullOrEmpty(text = Console.ReadLine()))
                    {
                        writer.WriteLine(text);
                    }
                    isInputValid = true;
                    writer.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"\nОшибка: {e.Message}. Попробуйте снова.");
                }
            }

            //
            TaskWithFilesSolutions.linesWithoutLetters(inputFile, outputFile);
            Console.WriteLine($"\nСтроки без букв записаны в файл: {outputFile}");
        }

        private void dataEntryTask4()
        {
            Console.WriteLine("\n--- Поиск наибольшего из значений модулей компонент с нечетными номерами. ---");
            Console.Write("\nВведите имя бинарного файла: ");
            string fileName = InputFileName();
            int count;
            bool isInputValid = false; //флаг для отслеживания корректности ввода

            while (!isInputValid)
            {
                try
                {
                    Console.WriteLine("\nВыберите способ заполнения бинарного файла:");
                    Console.WriteLine("1. Заполнить случайными числами");
                    Console.WriteLine("2. Ввести числа с клавиатуры");
                    int choice = 0;
                    bool isChoiceValid = false; //флаг для выбора способа заполнения

                    while (!isChoiceValid)
                    {
                        Console.Write("\nВведите номер варианта (1 или 2): ");
                        if (int.TryParse(Console.ReadLine(), out choice) && (choice == 1 || choice == 2))
                        {
                            isChoiceValid = true;
                        }
                        else
                        {
                            Console.WriteLine("\nОшибка! Введите 1 или 2.");
                        }
                    }

                    if (choice == 1)
                    {
                        //количество чисел
                        count = getPositiveInteger("\nВведите количество случайных чисел для записи: ");

                        //генерация случайных чисел в бинарный файл
                        randomFillBinaryFile(fileName, count);
                    }
                    else
                    {
                        count = getPositiveInteger("\nВведите количество чисел для записи: ");

                        BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create));

                        for (int i = 0; i < count; i++)
                        {
                            Console.Write($"\nВведите число {i + 1}: ");
                            int number;

                            while (!int.TryParse(Console.ReadLine(), out number))
                            {
                                Console.WriteLine("Ошибка! Введите корректное целое число.");
                                Console.Write($"\nВведите число {i + 1}: ");
                            }
                            writer.Write(number);
                        }
                        writer.Close();
                    }

                    //нахождение максимального по модулю значение с нечетным номером
                    int maxOddComponent = TaskWithFilesSolutions.findMaxOddComponent(fileName);
                    Console.WriteLine($"\nНаибольшее значение из модулей компонент с нечетными номерами: {maxOddComponent}");
                    isInputValid = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"\nОшибка: {e.Message}. Попробуйте снова.");
                }
            }
        }

        private void dataEntryTask5()
        {
            Console.WriteLine("\n--- Поиск названий игрушек для детей четырех-пяти лет. ---");
            Console.WriteLine("\nВведите имя файла для сохранения данных об игрушках (без расширения):");
            string fileName = InputFileName();

            string binPath = Path.Combine(Environment.CurrentDirectory, fileName + ".bin");
            string xmlPath = Path.Combine(Environment.CurrentDirectory, fileName + ".xml");

            //массив игрушек для записи в бинарный файл
            Toy[] toys = createToys();

            //запись игрушек в бинарный файл
            BinaryWriter writer = new BinaryWriter(File.Open(binPath, FileMode.Create));
            try
            {
                foreach (var toy in toys)
                {
                    writer.Write(toy.Name);
                    writer.Write(toy.Price);
                    writer.Write(toy.MinAge);
                    writer.Write(toy.MaxAge);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при записи в бинарный файл: {ex.Message}");
            }
            writer.Close();

            //массив игрушек для чтение из бинарного файла
            Toy[] toysFromBinary = readToysFromBinaryFile(binPath);

            //сериализация массива игрушек
            FileStream xmlFileStream = new FileStream(xmlPath, FileMode.Create);
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Toy[]));
                serializer.Serialize(xmlFileStream, toysFromBinary);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сериализации в XML файл: {ex.Message}");
            }
            xmlFileStream.Close();

            Console.WriteLine($"\nДанные об игрушках сохранены в бинарный файл {binPath} и в XML файл {xmlPath}");
            TaskWithFilesSolutions.findToysForSpecificYears(xmlPath);
        }

        private Toy[] createToys()
        {
            Random rnd = new Random();
            string[] toyNames =
            {
            "Doll", "Dominoes", "Puzzles", "Tablet", "The Dollhouse"
            };

            Toy[] toys = new Toy[toyNames.Length];
            for (int i = 0; i < toyNames.Length; ++i)
            {
                Toy toy = new Toy
                {
                    Name = toyNames[i],
                    Price = rnd.Next(200, 2000)
                };

                Console.WriteLine($"\nНазвание игрушки: {toy.Name}");
                Console.WriteLine($"Цена игрушки: {toy.Price}Р");

                toy.MinAge = getPositiveInteger($"\nВведите минимальный возраст для игрушки {toy.Name}:");
                toy.MaxAge = getPositiveInteger($"\nВведите максимальный возраст для игрушки {toy.Name}:");

                while (toy.MaxAge < toy.MinAge)
                {
                    Console.WriteLine("Максимальный возраст должен быть больше или равен минимальному.");
                    toy.MaxAge = getPositiveInteger($"\nВведите максимальный возраст для игрушки {toy.Name}:");
                }

                Console.WriteLine($"\nИгрушка для детей от {toy.MinAge} до {toy.MaxAge} лет");
                toys[i] = toy;
            }
            return toys;
        }

        private Toy[] readToysFromBinaryFile(string binPath)
        {
            List < Toy > toys = new List<Toy>();
            BinaryReader reader = null;

            try
            {
                reader = new BinaryReader(File.Open(binPath, FileMode.Open));

                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    Toy toy = new Toy
                    {
                        Name = reader.ReadString(),
                        Price = reader.ReadInt32(),
                        MinAge = reader.ReadInt32(),
                        MaxAge = reader.ReadInt32()
                    };

                    toys.Add(toy);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении из бинарного файла: {ex.Message}");
            }
            if (reader != null)
            {
                reader.Close();
            }

            return toys.ToArray();
        }

        private void dataEntryTask6()
        {
            Console.WriteLine("\n--- Формирование списка L, который включает элементы, которые входят в список L1, но не входят в список L2 ---");

            List<string> L1 = inputListValidation("\nВведите элементы списка L1 через пробел:");
            List<string> L2 = inputListValidation("Введите элементы списка L2 через пробел:");

            List<string> L = OtherTaskSolutions.getUniqueElements(L1, L2);

            Console.WriteLine("\nЭлементы из L1, не входящие в L2:");
            Console.WriteLine(string.Join(", ", L));
        }

        private void dataEntryTask7()
        {
            Console.WriteLine("\n--- Симметричность участка списка с i-го по j-й элемент (i < j).  ---");

            LinkedList<char> list = inputLinkedList("Введите элементы LinkedList через пробел:");
            
            //ввод промежутка
            validateIndicesLinkedList(list, out int i, out int j);

            //проверка на симметричность
            bool isSymmetric = OtherTaskSolutions.isSymmetricSection(list, i, j);
            if (isSymmetric)
            {
                Console.WriteLine($"\nУчасток списка с индексов {i} по {j} симметричен.");
            }
            else
            {
                Console.WriteLine($"\nУчасток списка с индексов {i} по {j} не симметричен.");
            }
        }

        private void dataEntryTask8()
        {
            Console.WriteLine("\n--- Шоколадки  ---");

            //все шоколадки
            HashSet<string> allChocolates = inputAllChocolates();

            int numberSweetTooth = getPositiveInteger("\nВведите количество сладкоежек: ");

            //предпочтения в шоколаде для каждого сладкоежки
            Dictionary<string, HashSet<int>> chocolatePreferences = inputChocolatePreferences(numberSweetTooth, allChocolates);

            OtherTaskSolutions.processChocolatePreferences(chocolatePreferences, allChocolates, numberSweetTooth);
        }

        private void dataEntryTask9()
        {
            Console.WriteLine("\n--- Количество русских букв, которые не встречаются в тексте  ---");

            Console.WriteLine("\nВведите имя файла:");
            string filePath = InputFileName();

            Console.WriteLine("\nВведите текст для записи в файл:");
            string inputText = Console.ReadLine();

            File.WriteAllText(filePath, inputText);
            Console.WriteLine("Текст успешно записан в файл.");

            int missingLettersCount = OtherTaskSolutions.missingRussianLetters(filePath);
            Console.WriteLine($"\nКоличество букв русского алфавита, которые не встречаются в тексте: {missingLettersCount}");
        }

        private void dataEntryTask10()
        {
            Console.WriteLine("\n--- Пассажиры, которые должны освободить ячейки в ближайшие 2 часа. ---");
            
            try
            {
                Console.WriteLine("\nВведите имя файла с данными пассажиров: ");
                string filePath = Console.ReadLine();

                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Файл не найден. Пожалуйста, убедитесь, что путь к файлу верный.");
                }
                List<string> passengersToFree = OtherTaskSolutions.findPassengersToFree(filePath);

                if (passengersToFree.Count > 0)
                {
                    Console.WriteLine("\nПассажиры, которые должны освободить ячейки:");
                    foreach (var passenger in passengersToFree)
                    {
                        Console.WriteLine(passenger);
                    }
                }
                else
                {
                    Console.WriteLine("\nНет пассажиров, которые должны освободить ячейки в ближайшие 2 часа.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка: {ex.Message}");
            }
        }
    }
}
