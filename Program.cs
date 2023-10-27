/*“Реалізувати клас Point (властивості: координати X,Y),
 * метод ToString для вивода координат точки, 
 * конструктор Реалізувати клас Triangle (властивості: масив Point[3]),
 * конструктор Клас Triangle повинен реалізувати інтерфейс IEnumerator 
 * В Main протестіть роботу foreach для об'єкта типу Triangle”*/
using System;
using System.Collections;
using System.Collections.Generic;

namespace My_Triangle
{
    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }

    class Triangle : IEnumerable<Point>
    {
        private Point[] points = new Point[3];

        public Point this[int index]
        {
            get { return points[index]; }
            set { points[index] = value; }
        }

        public Triangle(Point p1, Point p2, Point p3)
        {
            points[0] = p1;
            points[1] = p2;
            points[2] = p3;
        }

        public IEnumerator<Point> GetEnumerator()
        {
            foreach (Point point in points)
            {
                yield return point;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static bool IsTriangle(Point p1, Point p2, Point p3)
        {
            // Проверяем, является ли треугольником по критерию существования
            double a = CalculateDistance(p1, p2);
            double b = CalculateDistance(p2, p3);
            double c = CalculateDistance(p3, p1);

            return a + b > c && a + c > b && b + c > a;
        }

        private static double CalculateDistance(Point p1, Point p2)
        {
            int dx = p1.X - p2.X;
            int dy = p1.Y - p2.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }

    class Program
    {
        static void Main()
        {
            Point p1, p2, p3;

            Console.WriteLine("Введите координаты вершин треугольника через пробел:");

            Console.Write("Вершина 1 (X Y): ");
            p1 = ReadPoint();

            Console.Write("Вершина 2 (X Y): ");
            p2 = ReadPoint();

            Console.Write("Вершина 3 (X Y): ");
            p3 = ReadPoint();

            if (Triangle.IsTriangle(p1, p2, p3))
            {
                Triangle triangle = new Triangle(p1, p2, p3);

                Console.WriteLine("Координаты вершин треугольника:");

                foreach (Point point in triangle)
                {
                    Console.WriteLine(point);
                }
            }
            else
            {
                Console.WriteLine("Треугольник с такими координатами невозможен.");
            }
            Console.ReadKey();
        }

        static Point ReadPoint()
        {
            string[] input = Console.ReadLine().Split();
            if (input.Length == 2 && int.TryParse(input[0], out int x) && int.TryParse(input[1], out int y))
            {
                return new Point(x, y);
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Повторите ввод (X Y):");
                return ReadPoint();
            }
        }
    }
}
