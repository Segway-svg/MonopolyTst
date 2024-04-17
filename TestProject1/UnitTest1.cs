using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace TestProject1;
using MonopolyTest;

public class Tests
{
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

    [Test]
    public void Test1()
    {
        // Arrange
        List<Box> boxes1 = GenerateBoxes(2);
        List<Box> boxes2 = GenerateBoxes(4);
        List<Box> boxes3 = GenerateBoxes(3);

        Pallet pallet1 = new Pallet(15, 10, 5, 20, boxes1);
        Pallet pallet2 = new Pallet(5, 20, 15, 10, boxes2);
        Pallet pallet3 = new Pallet(5, 20, 15, 10, boxes3);

        List<Pallet> pallets = new List<Pallet>();
        pallets.Add(pallet1);
        pallets.Add(pallet2);
        pallets.Add(pallet3);
        
        // Act
        var sortedPallets = pallets.OrderBy(x => x.BestBeforeDate).ThenBy(x => x.Weight).ToList();

        // Assert
        for (int i = 1; i < sortedPallets.Count(); i++)
        {
            if (sortedPallets[i - 1].BestBeforeDate > sortedPallets[i].BestBeforeDate)
                Assert.Fail();
        }
        Assert.Pass();
    }
}