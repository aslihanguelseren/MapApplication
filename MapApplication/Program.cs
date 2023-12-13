using System;
using System.Collections.Generic;

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

        Console.WriteLine("Movement coordinates:");
        string[] movements = Console.ReadLine().Split(',');
        List<int[]> coordinates = new List<int[]>();
        Console.WriteLine("Life form (1 for Human, 2 for Alien):");
        int lifeFormType = int.Parse(Console.ReadLine());

        int x = 0, y = 0;
        for (int i = 0; i < movements.Length; i += 2)
        {
            int dx = int.Parse(movements[i]);
            int dy = int.Parse(movements[i + 1]);

            x = (x + dx);
            y = (y + dy);

            if (x > width) x = 0;
            else if (x < 0) x = width;

            if (y > height) y = 0;
            else if (y < 0) y = height;
            coordinates.Add(new int[] { x, y });
        }

        switch (lifeFormType)
        {
            case 1:
                ReportPath(coordinates);
                ReportActualCoordinate(coordinates[coordinates.Count - 1]);
                break;
            case 2:
                List<int[]> alienCoordinates = new List<int[]>();
                foreach (var coord in coordinates)
                {
                    alienCoordinates.Add(new int[] { coord[1], coord[0] });
                }

                ReportPath(alienCoordinates);
                ReportActualCoordinate(alienCoordinates[alienCoordinates.Count - 1]);
                break;
            default: 
                Console.WriteLine("Invalid life form");
                break;
        }
        Console.ReadLine();
    }

    static void ReportPath(List<int[]> coordinates)
    {
        Console.WriteLine("Report Path");
        foreach (var coord in coordinates)
        {
            Console.WriteLine("[" + coord[0] + ", "+coord[1] + "]");
        }
    }

    static void ReportActualCoordinate(int[] coordinate)
    {
        Console.WriteLine("Report Actual Coordinate");
        Console.WriteLine("[" + coordinate[0] + ", " + coordinate[1] + "]");
    }
}