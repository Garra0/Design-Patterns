using System;

namespace DesignPatterns.ConsoleApp.Patterns._02_Builder;

// ====================================================================================
// ✅ After Builder Pattern
// الكود بعد التحسين
// ====================================================================================

// 1. Computer: (الكائن المراد بناؤه)
public class Computer
{
    // (قيم افتراضية لتفادي أخطاء null)
    public string CPU { get; set; } = "Intel i3";
    public string RAM { get; set; } = "4GB";
    public string Storage { get; set; } = "128GB SSD";
    public bool HasGPU { get; set; }
    public bool HasWiFi { get; set; }
}

// 2. ComputerBuilder: (كلاس البناء)
public class ComputerBuilder
{
    private readonly Computer _computer = new Computer();

    // AddCPU: (كل دالة تقوم بتجهيز جزء، ثم ترجع نفس كلاس البناء)
    public ComputerBuilder AddCPU(string cpu)
    {
        _computer.CPU = cpu;
        return this;
    }

    public ComputerBuilder AddRAM(string ram)
    {
        _computer.RAM = ram;
        return this;
    }

    public ComputerBuilder AddStorage(string storage)
    {
        _computer.Storage = storage;
        return this;
    }

    public ComputerBuilder WithGPU()
    {
        _computer.HasGPU = true;
        return this;
    }

    public ComputerBuilder WithWiFi()
    {
        _computer.HasWiFi = true;
        return this;
    }

    // Build: (دالة التقفيل التي ترجع الكائن النهائي)
    public Computer Build()
    {
        return _computer;
    }
}

// 3. BuilderAfterDemo: (الاستدعاء)
public static class BuilderAfterDemo
{
    public static void Run()
    {
        Console.WriteLine("\n--- Running AFTER Builder Pattern ---");

        // Method Chaining: (الكود أصبح يقرأ كقصة ويمكننا تجاهل الخصائص غير المطلوبة)
        var myPc = new ComputerBuilder()
            .AddCPU("Intel i7")
            .AddRAM("16GB")
            .AddStorage("1TB")
            .WithGPU()
            // (تجاهلنا الـ WiFi لأنه غير مطلوب)
            .Build(); 

        Console.WriteLine($"Computer details: CPU={myPc.CPU}, RAM={myPc.RAM}, GPU={myPc.HasGPU}");
    }
}
