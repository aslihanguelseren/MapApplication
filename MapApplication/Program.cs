using System;
using System.Collections.Generic;
using System.IO;

class Map
{
    public int width { get; set; }
    public int height { get; set; }

    public Map(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

}
abstract class LifeForm
{
    protected Map map;
    protected List<int[]> coordinates = new List<int[]>();

    protected LifeForm(Map map)
    {
        this.map = map;
    }

    public void Move(int dx, int dy)
    {
        int x = 0;
        int y = 0;
        if (coordinates.Count != 0) { 
        x = GetActualCoordinate()[0];
        y = GetActualCoordinate()[1];
    }
        x = (x + dx);
        y = (y + dy);

        if (x > map.width) x = 0;
        else if (x < 0) x = map.width;

        if (y > map.height) y = 0;
        else if (y < 0) y = map.height;
        coordinates.Add(new int[] { x, y });
    }

    public abstract List<int[]> ReportPath();

    public abstract int[] ReportActualCoordinate();
    public int[] GetActualCoordinate()
    {
        return coordinates[coordinates.Count - 1];
    }
}
class Human : LifeForm
{
    public Human(Map map) : base(map) { }

    public override List<int[]> ReportPath()
    {
        Console.WriteLine("Report Path");
        foreach (var coord in coordinates)
        {
            Console.WriteLine("[" + coord[0] + ", " + coord[1] + "]");
        }
        return coordinates; //new List<int[]>(coordinates)
    }
    public override int[] ReportActualCoordinate()
      {
        Console.WriteLine("Report Actual Coordinate");
        Console.WriteLine("[" + coordinates[coordinates.Count - 1][0] + ", " + coordinates[coordinates.Count - 1][1] + "]");
        return coordinates[coordinates.Count - 1];
    }
}

class Alien : LifeForm
{
    public Alien(Map map) : base(map) { }

    public override List<int[]> ReportPath()
    {
        List<int[]> reversed = coordinates.ConvertAll(coordinates => new int[2] { coordinates[1], coordinates[0] });
        Console.WriteLine("Report Path");
        foreach (var coord in reversed)
        {
            Console.WriteLine("[" + coord[0] + ", " + coord[1] + "]");
        }
        return reversed;
    }
    public override int[] ReportActualCoordinate()
    {
        Console.WriteLine("Report Actual Coordinate");
        Console.WriteLine("[" + coordinates[coordinates.Count - 1][1] + ", " + coordinates[coordinates.Count - 1][0] + "]");
        return coordinates[coordinates.Count - 1];
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Map dimensions w, h:");
        string[] mapSize = Console.ReadLine().Split(',');
        int width = int.Parse(mapSize[0]);
        int height = int.Parse(mapSize[1]);
        if (!(height > -height && width > -width))
        {
            Console.WriteLine("Invalid map dimensions");
            return;
        }
        var map = new Map(width, height);
        Console.WriteLine("Movement coordinates:");
        string[] movements = Console.ReadLine().Split(',');
        List<int[]> coordinates = new List<int[]>();
        Console.WriteLine("Life form (1 for Human, 2 for Alien):");
        int lifeFormType = int.Parse(Console.ReadLine());
        LifeForm lifeForm;

        switch (lifeFormType)
        {
            case 1:
                lifeForm = new Human(map);
                break;
            case 2:
                lifeForm = new Alien(map);
                break;
            default:
                lifeForm = new Human(map);
                Console.WriteLine("Invalid life form");
                break;
        }
        for (int i = 0; i < movements.Length; i += 2)
        {
            int dx = int.Parse(movements[i]);
            int dy = int.Parse(movements[i + 1]);
            lifeForm.Move(dx, dy);
        }
        lifeForm.ReportPath();
        lifeForm.ReportActualCoordinate();
        Console.ReadLine();
    }
}