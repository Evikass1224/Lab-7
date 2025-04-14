using System;

namespace ProjectFileTasks
{
    class Program
    {
        static void Main()
        {
            bool exitFlag;
            TaskSelection taskSelection = new TaskSelection();
            taskSelection.inputNumberTask(out exitFlag);

            if (exitFlag)
            {
                Console.WriteLine("Выход из программы.");
            }
        }
    }
}
