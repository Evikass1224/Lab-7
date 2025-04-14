using System;

namespace ProjectFileTasks
{
    /// <summary>
    /// Класс для методов решения задач 6 - 10
    /// </summary>
    class OtherTaskSolutions
    {
        public static List<string> getUniqueElements(List<string> L1, List<string> L2)
        {
            List<string> L = new List<string>();

            foreach (var item in L1)
            {
                if (!L2.Contains(item) && !L.Contains(item))
                {
                    L.Add(item);
                }
            }
            return L;
        }

        /// <summary>
        /// Проверяет, является ли раздел списка символов симметричным.
        /// </summary>
        /// <param name="list">Связанный список символов, который необходимо проверить.</param>
        /// <param name="i">Начальный индекс (1-базовый) секции для проверки симметрии.</param>
        /// <param name="j">Конечный индекс (1-базовый) секции для проверки симметрии</param>
        /// <returns>Возвращает true, если секция симметрична, в противном случае false</returns>
        public static bool isSymmetricSection(LinkedList<char> list, int i, int j)
        {
            LinkedListNode<char> startNode = list.First;  //узел на позиции i

            for (int index = 0; index < i - 1; index++)
            {
                if (startNode == null) return false;
                startNode = startNode.Next;
            }

            LinkedListNode<char> endNode = startNode;   //узел на позиции j

            for (int index = i - 1; index < j - 1; index++)
            {
                if (endNode == null) return false;
                endNode = endNode.Next;
            }

            for (int index = 0; index < (j - i + 1) / 2; index++)
            {
                if (startNode == null || endNode == null) return false;
                if (startNode.Value != endNode.Value)
                {
                    return false;
                }

                startNode = startNode.Next;
                endNode = endNode.Previous;
                
            }
            return true;
        }

        public static void processChocolatePreferences(Dictionary<string, HashSet<int>> chocolatePreferences, HashSet<string> allChocolates, int numberSweetTooth)
        {
            HashSet<string> likedByAll = new HashSet<string>();
            HashSet<string> likedBySome = new HashSet<string>();
            HashSet<string> likedByNone = new HashSet<string>();

            //названия шоколадок 
            var chocolateKeys = new List<string>(chocolatePreferences.Keys);

            for (int i = 0; i < chocolateKeys.Count; i++)
            {
                string chocolateName = chocolateKeys[i];

                //список сладкоежек, кому понравилась шоколадка
                HashSet<int> likedBy = chocolatePreferences[chocolateName];

                if (likedBy.Count == numberSweetTooth)
                {
                    likedByAll.Add(chocolateName);
                }
                else if (likedBy.Count > 0)
                {
                    likedBySome.Add(chocolateName);
                }
            }

            //все доступные шоколадки
            var availableChocolate = new List<string>(allChocolates);

            for (int j = 0; j < availableChocolate.Count; j++)
            {
                if (!chocolatePreferences.ContainsKey(availableChocolate[j]))
                {
                    likedByNone.Add(availableChocolate[j]);
                }
            }

            Console.WriteLine("\nШоколад, который нравится всем:");
            for (int k = 0; k < likedByAll.Count; k++)
            {
                Console.WriteLine(likedByAll.ElementAt(k));
            }

            Console.WriteLine("\nШоколад, который нравится некоторым:");
            for (int l = 0; l < likedBySome.Count; l++)
            {
                Console.WriteLine(likedBySome.ElementAt(l));
            }

            Console.WriteLine("\nШоколад, который не нравится никому:");
            for (int m = 0; m < likedByNone.Count; m++)
            {
                Console.WriteLine(likedByNone.ElementAt(m));
            }
        }

        public static int missingRussianLetters(string filePath)
        {
            HashSet<char> russianAlphabet = new HashSet<char>();

            for (char c = 'а'; c <= 'я'; c++)
            {
                russianAlphabet.Add(c);
            }
            russianAlphabet.Add('ё');

            HashSet<char> lettersInText = new HashSet<char>();

            try
            {
                string text = File.ReadAllText(filePath);
                foreach (char c in text.ToLower())
                {
                    if (russianAlphabet.Contains(c))
                    {
                        lettersInText.Add(c);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return -1;
            }

            int missingCount = russianAlphabet.Count - lettersInText.Count;
            return missingCount;
        }

        public static List<string> findPassengersToFree(string filePath)
        {
            //список для хранения фамилий пассажиров, которых можно освободить
            List<string> result = new List<string>();

            //отсортированная коллекция для хранения времени освобождения пассажиров и список их фамилий
            SortedList<TimeSpan, List<string>> passengerData = new SortedList<TimeSpan, List<string>>();

            try
            {
                string[] lines = File.ReadAllLines(filePath);
                TimeSpan currentTime = timeSpan(lines[0]);  //текущее время
                int passengerCount = int.Parse(lines[1]);  //количество пассажиров

                for (int i = 2; i < 2 + passengerCount; i++)
                {
                    string[] parts = lines[i].Split(' ');

                    string surname = parts[0];
                    TimeSpan releaseTime = timeSpan(parts[1]);

                    if (!passengerData.ContainsKey(releaseTime))
                    {
                        passengerData[releaseTime] = new List<string>();
                    }
                    passengerData[releaseTime].Add(surname);
                }

                //время, через 2 часа от текущего времени
                TimeSpan twoHoursLater = currentTime.Add(TimeSpan.FromHours(2));

                //список подходящих пассажиров
                List<KeyValuePair<TimeSpan, List<string>>> validPassengers = new List<KeyValuePair<TimeSpan, List<string>>>();

                //все записи в словаре
                foreach (var pair in passengerData)
                {
                    if (pair.Key > currentTime && pair.Key <= twoHoursLater)
                    {
                        validPassengers.Add(new KeyValuePair<TimeSpan, List<string>>(pair.Key, pair.Value));
                    }
                    else if (pair.Key < currentTime && (pair.Key + TimeSpan.FromDays(1)) <= twoHoursLater)
                    {
                        validPassengers.Add(new KeyValuePair<TimeSpan, List<string>>(pair.Key + TimeSpan.FromDays(1), pair.Value));
                    }
                }

                foreach (var pair in validPassengers)
                {
                    result.AddRange(pair.Value);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обработке данных: {ex.Message}");
            }
            return result;
        }

        private static TimeSpan timeSpan(string time)
        {
            string[] parts = time.Split(':');
            return new TimeSpan(int.Parse(parts[0]), int.Parse(parts[1]), 0);
        }
    }
}