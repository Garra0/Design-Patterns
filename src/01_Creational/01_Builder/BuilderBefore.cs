using System;

namespace DesignPatterns.ConsoleApp.Patterns._02_Builder;

// ====================================================================================
// ⚠️ Before Builder Pattern
// الكود قبل التحسين
// ====================================================================================

public class ComputerBefore
{
    public string CPU { get; }
    public string RAM { get; }
    public string Storage { get; }
    public string? GPU { get; }
    public bool HasWifi { get; }
    public bool HasBluetooth { get; }

    // Telescoping Constructor: (المشكلة هنا: مشيد ضخم يحتوي على كل المعاملات حتى لو كانت اختيارية)
    public ComputerBefore(string cpu, string ram, string storage, string? gpu, bool hasWifi, bool hasBluetooth)
    {
        CPU = cpu;
        RAM = ram;
        Storage = storage;
        GPU = gpu;
        HasWifi = hasWifi;
        HasBluetooth = hasBluetooth;
    }
}

public static class BuilderBeforeDemo
{
    public static void Run()
    {
        Console.WriteLine("--- Running BEFORE Builder Pattern ---");

        // Null Parameters: (تضطر لتمرير قيم فارغة أو افتراضية للمعاملات التي لا تحتاجها مثل كرت الشاشة والواي فاي)
        var myComputer = new ComputerBefore("Intel i5", "8GB", "256GB SSD", null, false, true);

        Console.WriteLine($"Computer details: CPU={myComputer.CPU}, RAM={myComputer.RAM}, GPU={myComputer.GPU ?? "None"}");
    }
}
