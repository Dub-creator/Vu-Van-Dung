
using System;
using System.Collections.Generic;

int a;
int b;
int c;

Console.Write("Nhap a: ");
a = Convert.ToInt32(Console.ReadLine());

while (true)
{
    Console.Write("Nhap b: ");
    try
    {
        b = int.Parse(Console.ReadLine());
        break;
    }
    catch
    {
        Console.WriteLine("Nhap sai, vui long nhap lai");
    }
}

bool k;
while (true)
{
    Console.Write("Nhap c: ");
    k = int.TryParse(Console.ReadLine(), out c);
    if (k)
        break;
    else
        Console.WriteLine("Nhap sai, vui long nhap lai.");
}

Console.WriteLine($"{a}+{b}+{c} = {a + b + c}");
