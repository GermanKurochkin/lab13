using System;
using System.Collections;
using System.Collections.Generic;

namespace lab13
{
    class Program
    {
        static MyCollection mc1 = new MyCollection();

        static void Add()
        {
            Console.WriteLine("Введите количество добавляемых элементов");
            int num = InputNum(20);
            for (int i = 0; i < num; i++)
                mc1.Add();
        }
        
        static void Delete()
        {
            Console.WriteLine("Введите место удаления элементов");
            int place = InputNum(mc1.Count + 1);
            mc1.Delete(place);
        }
        static int InputMenu(int maxNum)
        {
            int menu;
            string input;
            bool ok;
            do
            {
                input = Console.ReadLine();
                ok = int.TryParse(input, out menu);
                if (!ok) Console.WriteLine("Некорректный ввод");
                else if (menu < 0 || menu > maxNum) Console.WriteLine($"Некорректный ввод.Выберите вариант от 0 до {maxNum} из меню");
            } while (!ok || menu < 0 || menu > maxNum);

            return menu;
        }

        static int InputNum(int maxNum)
        {
            int num;
            string input;
            bool ok;
            do
            {
                input = Console.ReadLine();
                ok = int.TryParse(input, out num);
                if (!ok) Console.WriteLine("Некорректный ввод");
                else if (num < 0 || num > maxNum) Console.WriteLine($"Некорректный ввод. Введите число не больше {maxNum}");
            } while (!ok || num < 0 || num > maxNum);

            return num;

        }
        static void Main(string[] args)
        {
            Random rand = new Random();
            int menu = 10;


            while (menu != 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("1. Демонстрация коллекции");
                Console.WriteLine("2. Часть №2");
                Console.WriteLine("0.Выход");
                Console.ResetColor();
                menu = InputMenu(2);
                mc1 = new MyCollection(10);


                if (menu == 0) break;
                else
                {
                    int menuNext = 10;

                    switch (menu)
                    {
                        #region task1
                        case 1:

                            while (menuNext != 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("1.Показать коллекцию");
                                Console.WriteLine("2.Добавить элементы");
                                Console.WriteLine("3.Удалить элементы");
                                Console.WriteLine("4.Сортировка");
                                Console.WriteLine("5.Удалить коллекцию");
                                Console.WriteLine("6.Длина");
                                Console.WriteLine("0.Назад");
                                Console.ResetColor();
                                menuNext = InputMenu(6);
                             
                                if (menuNext == 0) break;
                                else
                                {
                                    switch (menuNext)
                                    {
                                        case 1:
                                            mc1.Show();
                                            break;
                                        case 2:
                                            Add();
                                            Console.WriteLine("Элементы добавлены");
                                            break;
                                        case 3:
                                            Delete();
                                            Console.WriteLine("Элементы удалены");
                                            break;
                                        case 4:
                                            mc1.Sort();
                                            mc1.Show();
                                            break;
                                        case 5:
                                            mc1.Clear();
                                            mc1.Show();
                                            break;
                                        case 6:
                                            Console.WriteLine($"Количество:{mc1.Count }");
                                            break;
                                    }
                                }

                            }
                            break;
                        #endregion task1

                        #region task2
                        case 2:
                            MyNewCollection list1 = new MyNewCollection();
                            MyNewCollection list2 = new MyNewCollection();
                            list1.Name = "First";
                            list2.Name = "Second";

                            Journal jour1 = new Journal();
                            Journal jour2 = new Journal();

                            list1.CollectionCountChanged += new CollectionHandler(jour1.CollectionCountChanged);
                            list1.CollectionReferenceChanged += new CollectionHandler(jour1.CollectionReferenceChanged);

                            list2.CollectionCountChanged += new CollectionHandler(jour2.CollectionCountChanged);
                            list2.CollectionReferenceChanged += new CollectionHandler(jour2.CollectionReferenceChanged);

                            list1.AddRandom();
                            list1.AddRandom();
                            list1.AddRandom();
                            list1.AddRandom();

                            list2.AddRandom();
                            list2.AddRandom();
                            list2.AddRandom();
                            list2.AddRandom();

                            list1.Remove(1);
                            list2.Remove(2);

                            list1.ChangeValue(2);
                            list2.ChangeValue(1);

                            list1.Add(new Engine( "ADD", 1111));
                            Console.WriteLine("В каждую коллекцию добавлено по 4 элемента");
                            Console.WriteLine("Из первой удален 1, из второй 2");
                            Console.WriteLine("В первой изменен 2(3 до удаления), во второй 1");
                            Console.WriteLine("В первую добавлен элемент ADD");
                            Console.WriteLine();

                            list1.Show();
                            Console.WriteLine();
                            list2.Show();

                            Console.WriteLine();
                            Console.WriteLine("Journal 1:");
                            Console.WriteLine();
                            jour1.ToString();
                            Console.WriteLine();
                            Console.WriteLine("Journal 2:");
                            Console.WriteLine();
                            jour2.ToString();



                            Console.ReadKey();

                            break;
                        #endregion task2

                    }
                }
            }

        }
    }
    public class JournalEntry
    {
        public string Name { get; set; }
        public string ChangeType { get; set; }
        public string ObjectData { get; set; }

        public JournalEntry(string name, string change, string data)
        {
            Name = name;
            ChangeType = change;
            ObjectData = data;
        }

        public override string ToString()
        {
            return "Имя коллекции: " + Name + "\nОбъект: " + ObjectData + " - " + ChangeType;
        }
    }
    public class Journal
    {
        private List<JournalEntry> journal;

        public Journal()
        {
            journal = new List<JournalEntry>();
        }

        public void CollectionCountChanged(object source, CollectionHandlerEventArgs arg)
        {
            JournalEntry jur = new JournalEntry(arg.NameCollection, arg.ChangeType, arg.ObjectData.ToString());
            journal.Add(jur);
        }
        public void CollectionReferenceChanged(object source, CollectionHandlerEventArgs arg)
        {
            JournalEntry jur = new JournalEntry(arg.NameCollection, arg.ChangeType, arg.ObjectData.ToString());
            journal.Add(jur);
        }
        public override string ToString()
        {
            string str ="";
            foreach (var item in journal)
            {
                Console.WriteLine(item.ToString());
                str += item.ToString();
                str += "\n";
              
            }
            return str;
        }
    }

  
}
