using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic;

public class Program
{
    public static void Main()
    {
        List<dynamic> L = create_list();
        task_1(L);
        Console.WriteLine("\ntask_1 completed");
        Console.WriteLine("task_2: ");
        List<dynamic> L_2 = create_list();
        task_2(L_2);
        Console.WriteLine("");
        task_3();
        Console.WriteLine("task_4:\n");
        task_4();
    }

    // Метод удаления элементов из списка.
    public static void RemoveElements<T>(List<T> list, T element)
    {
        // Перебираем список в обратном порядке.
        for (int i = list.Count - 1; i >= 0; i--)
        {
            // Если текущий элемент равен E, удаляем его.
            if (list[i].Equals(element))
            {
                list.RemoveAt(i);
            }
        }
    }

    // Метод для преобразования строки в соответствующий тип
    private static dynamic GetElementByType(string elementToRemove)
    {
        if (int.TryParse(elementToRemove, out int num))
        {
            return num;
        }
        else if (bool.TryParse(elementToRemove, out bool boolean))
        {
            return boolean;
        }
        else
        {
            return elementToRemove;
        }
    }
    public static void PrintLinkedListReversed<T>(LinkedList<T> list)
    {
        // 
        LinkedListNode<T> current = list.Last;
        Console.Write("список в обратном порядке: ");
        // Проходим по списку в обратном порядке.
        while (current != null)
        {
            // Выводим текущий элемент.
            Console.Write(current.Value + " ");

            // Переходим к следующему элементу.
            current = current.Previous;
        }

        Console.WriteLine(); // Добавляем пустую строку в конце
    }
    private static List<dynamic> create_list()
    {
        Console.WriteLine("Введите элементы массива, разделенные пробелами:");
        string input = Console.ReadLine();
        string[]
    elements = input.Split(' ');

        // массив
        List<dynamic> L = new List<dynamic>();
        foreach (string element in elements)
        {
            // Проверяем тип элемента и добавляем его в список
            if (int.TryParse(element, out int num))
            {
                L.Add(num);
            }
            else if (bool.TryParse(element, out bool boolean))
            {
                L.Add(boolean);
            }
            else
            {
                L.Add(element);
            }
        }
        return L;
    }
    private static void task_1(List<dynamic> L)
    {
        Console.WriteLine("Введите элемент для удаления:");
        string elementToRemove = Console.ReadLine();

        //приводим вывод пользователя к типу(для тестов ,сам метод может удалять просто сщности равные заданному объекту)
        dynamic E = GetElementByType(elementToRemove);
        RemoveElements(L, E);

        // Выводим список после удаления.
        Console.WriteLine("Список после удаления элементов E:");
        foreach (var item in L)
        {
            Console.Write(item + " ");
        }
    }
    private static void task_2(List<dynamic> L)
    {
        LinkedList<dynamic> myLinkedList = new LinkedList<dynamic>();
        foreach (dynamic item in L)
        {
            myLinkedList.AddLast(item);
        }
        PrintLinkedListReversed(myLinkedList);
    }

    public static void task_3()
    {
        // Ввод данных и выввод входнных данных в консоль
        Console.WriteLine("входные данные для задания 3: \n");
        int n = 5;
        string[] schoolNames = { "Школа №1", "Гимназия №2", "Лицей №3", "Школа №4", "Гимназия №5" };
        string[] firmNames = { "Фирма А", "Фирма Б", "Фирма В", "Фирма Г", "Фирма Д", "успешные ребятки" };
        Console.WriteLine("школы: " + string.Join(", ", schoolNames));
        Console.WriteLine("фирмы: " + string.Join(", ", firmNames) + "\n");


        // Создаем словарь, где ключом является название учебного заведения,
        // а значением - множество фирм, у которых оно делало закупки.
        Dictionary<string, HashSet<string>> schoolFirms = new Dictionary<string, HashSet<string>>();
        for (int i = 0; i < n; i++)
        {
            schoolFirms[schoolNames[i]] = new HashSet<string>();
        }

        // Заполнение данных о закупках (пример):
        schoolFirms["Школа №1"].Add("Фирма Б");
        schoolFirms["Школа №1"].Add("Фирма Б");
        schoolFirms["Гимназия №2"].Add("Фирма В");
        schoolFirms["Лицей №3"].Add("Фирма В");
        schoolFirms["Лицей №3"].Add("Фирма Г");
        schoolFirms["Школа №4"].Add("Фирма Б");
        schoolFirms["Школа №4"].Add("Фирма Д");
        foreach (string name in schoolNames)
        {
            schoolFirms[name].Add(firmNames.Last());
        }
        Console.WriteLine("информация о закупках:");
        foreach (string school in schoolNames)
        {
            Console.WriteLine($"{school}: {string.Join(", ", schoolFirms[school])}");
        }
        Console.Write("\n");

        // 1) Фирмы, где закупались все учебные заведения
        HashSet<string> firmsAllSchools = new HashSet<string>(firmNames);
        foreach (string school in schoolNames)
        {
            firmsAllSchools.IntersectWith(schoolFirms[school]); // Пересечение множеств
        }
        Console.Write("\nПУНКТ 1. Фирмы, где закупались все учебные заведения:\nответ: ");
        Console.Write(string.Join(", ", firmsAllSchools));



        // 2) В каких фирмах закупка производилась хотя бы одним из заведений?
        Console.WriteLine("\nпункт 2: В каких фирмах закупка производилась хотя бы одним из заведений?");
        HashSet<string> firmsWithPurchases = new HashSet<string>();
        foreach (string school in schoolNames)
        {
            foreach (string firm in schoolFirms[school])
            {
                firmsWithPurchases.Add(firm);
            }
        }
        Console.WriteLine(string.Join(", ", firmsWithPurchases));


        // 3) В каких фирмах ни одно из заведений не закупало компьютеры?
        HashSet<string> firmsWithoutPurchases = new HashSet<string>(firmNames);
        firmsWithoutPurchases.ExceptWith(firmsWithPurchases);
        Console.Write("\nПункт 3: Фирмы, где ни одно из заведений не закупало компьютеры: ");
        Console.Write(string.Join(", ", firmsWithoutPurchases));
        Console.WriteLine();
    }


    public static void task_4()
    {
        string filePath = "input.txt";
        try
        {
            string text = File.ReadAllText(filePath);
            HashSet<char> characters = new HashSet<char>(text.ToLower());

            // Определение звонких согласных
            string voicedConsonants = "бвгджзлмнр";
            HashSet<char> voicedConsonantsSet = new HashSet<char>(voicedConsonants);

            // Находим пересечение множеств (звонкие согласные, которые есть в тексте)
            var sortedConsonants = characters.Where(c => voicedConsonantsSet.Contains(c)) // Получаем отсортированное множество
                                          .OrderBy(c => c) // Сортируем (избыточно, так как HashSet уже упорядочен)
                                          .ToList();

            Console.WriteLine($"Исходный текст в файле: {text}");
            Console.WriteLine("Звонкие согласные в тексте в алфавитном порядке:");
            Console.WriteLine(string.Join(", ", sortedConsonants));

        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Файл {filePath} не найден.");
        }
    }
}

