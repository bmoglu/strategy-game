﻿
public struct Point
{
   public int X { get; set; }

   public int Y { get; set; }

   public Point(int x, int y)
   {
       this.X = x;
       this.Y = y;
   }

   //For == control
   public static bool operator == (Point first, Point second)
   {
       return first.X == second.X && first.Y == second.Y;
   }

   //For != control
   public static bool operator != (Point first, Point second)
   {
       return first.X != second.X || first.Y != second.Y;
   }

   //For - control
   public static Point operator -(Point x, Point y)
   {
       return  new Point(x.X - y.X, x.Y - y.Y);
   }
}