//  -Помимо общего набора стандартных свойств (ID, ширина, высота, глубина, вес), паллета может содержать в себе коробки.
//    - У коробки должен быть указан срок годности или дата производства. Если указана дата производства, то срок
//          годности вычисляется из даты производства плюс 100 дней. (+)

//    - Срок годности и дата производства - это конкретная дата без времени (например, 01.01.2023). (+)

//    - Срок годности паллеты вычисляется из наименьшего срока годности коробки, вложенной в паллету.
//          Вес паллеты вычисляется из суммы веса вложенных коробок + 30кг. (+)

//    - Объем коробки вычисляется как произведение ширины, высоты и глубины. (+)

//    - Объем паллеты вычисляется как сумма объема всех находящихся на ней коробок и произведения ширины,
//          высоты и глубины паллеты. (+)

//    - Каждая коробка не должна превышать по размерам паллету (по ширине и глубине). (+)

using MonopolyTest;
using System;

internal class Program
{
    // - Вес паллеты вычисляется из суммы веса вложенных коробок + 30кг. (+)
    public static double CountPalletWeight(Pallet pallet)
    {
        if (pallet == null || pallet.Boxes.Count == 0) { return 0; }

        return pallet.Boxes.Sum(x => x.Weight) + 30;
    }

    // - Срок годности паллеты вычисляется из наименьшего срока годности коробки, вложенной в паллету. (+)
    public static DateTime CountPalletBestBeforeDate(Pallet pallet)
    {
        if (pallet == null || pallet.Boxes.Count == 0) { new Exception("Empty pallet"); }

        return pallet.Boxes.Min(x => x.BestBeforeDate);
    }

    // - Объем паллеты вычисляется как сумма объема всех находящихся на ней коробок и произведения ширины,
    //      высоты и глубины паллеты. (+)
    public static double CountPalletVolume(Pallet pallet)
    {
        if (pallet == null) { return 0; }

        var palletVolume = pallet.Height * pallet.Width * pallet.Depth;

        if (pallet.Boxes.Count == 0) { return palletVolume; }
        else { return palletVolume + pallet.Boxes.Sum(x => x.Volume); }
    }

    public static DateTime RandomDate(Random gen)
    {
        DateTime start = new DateTime(1995, 1, 1);
        int range = (DateTime.Today - start).Days;
        return start.AddDays(gen.Next(range));
    }

    public static Random random = new Random();

    public static List<Box> GenerateBoxes(int boxesQuantity)
    {
        List<Box> boxes = new List<Box>();

        for (int i = 0; i < boxesQuantity; i++)
        {
            Box box1 = new Box(RandomDate(random), random.Next(1, 20), random.Next(1, 20), random.Next(1, 20), random.Next(1, 20));
            Box box2 = new Box(RandomDate(random), RandomDate(random), random.Next(1, 20), random.Next(1, 20), random.Next(1, 20), random.Next(1, 20));
            boxes.Add(box1);
            boxes.Add(box2);
        }

        return boxes;
    }

    //    -Сгруппировать все паллеты по сроку годности, отсортировать по возрастанию срока годности,
    //          каждой группе отсортировать паллеты по весу.
    //    - 3 паллеты, которые содержат коробки с наибольшим сроком годности, отсортированные по возрастанию объема.
    //    - (Опционально) Покрыть функционал unit-тестами.
    public static void Main(string[] args)
    {
        List<Box> boxes1 = GenerateBoxes(2);
        List<Box> boxes2 = GenerateBoxes(4);
        List<Box> boxes3 = GenerateBoxes(3);

        // -Сгруппировать все паллеты по сроку годности, отсортировать по возрастанию срока годности,
        //      каждой группе отсортировать паллеты по весу.

        Pallet pallet1 = new Pallet(15, 10, 5, 20, boxes1);
        Pallet pallet2 = new Pallet(5, 20, 15, 10, boxes2);
        Pallet pallet3 = new Pallet(5, 20, 15, 10, boxes3);

        List<Pallet> pallets = new List<Pallet>();
        pallets.Add(pallet1);
        pallets.Add(pallet2);
        pallets.Add(pallet3);

        var sortedPallets = pallets.OrderBy(x => x.BestBeforeDate).ThenBy(x => x.Weight).ToList();

        foreach (var pallet in sortedPallets)
        {
            Console.WriteLine($"Weight - {pallet.Weight} - {pallet.BestBeforeDate}");
        }

        Console.WriteLine();

        // - 3 паллеты, которые содержат коробки с наибольшим сроком годности, отсортированные по возрастанию объема.

        var newSortedPallets = pallets.OrderBy(x => x.BestBeforeDate).Take(3).ToList();
        newSortedPallets = newSortedPallets.OrderBy(x => x.Volume).ToList();

        foreach (var pallet in newSortedPallets)
        {
            Console.WriteLine($"Volume: {pallet.Volume} - {pallet.BestBeforeDate}");
        }
    }
}