﻿namespace NetSchool.Lecture_5.Example
{
    static class Program
    {
        static void Main(string[] args)
        {
            var mtrx = new Matrix(3, 3);
            mtrx.Fill(1);
            mtrx.Print();
        }
    }
}
