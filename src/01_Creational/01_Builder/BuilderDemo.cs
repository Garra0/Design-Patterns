using System;

namespace DesignPatterns.ConsoleApp.Patterns._02_Builder;

// ====================================================================================
// ✅ After Builder Pattern
// الكود بعد التحسين
// ====================================================================================

// Note: (ملاحظة: بفصل منطق البناء عن كلاس البيانات، حققنا مبادئ تصميم هامة مثل)
// 1. Single Responsibility Principle: (كلاس الكمبيوتر مسؤول فقط عن تخزين البيانات، وكلاس البناء مسؤول عن كيفية تجميع المواصفات)
// 2. Open/Closed Principle: (يمكننا إضافة مواصفات جديدة بالبناء دون تعديل المشيد أو كسر الكود القديم للعملاء)

// Product: (المنتج النهائي الذي نريد بناؤه ويكون غير قابل للتعديل بعد الإنشاء)
public class Computer
{
    public string CPU { get; }
    public string RAM { get; }
    public string Storage { get; }
    public string? GPU { get; }
    public bool HasWifi { get; }
    public bool HasBluetooth { get; }

    // (المشيد يستقبل كلاس البناء لنسخ القيم وإنشاء كائن كامل وصحيح)
    public Computer(ComputerBuilder builder)
    {
        CPU = builder.CPU;
        RAM = builder.RAM;
        Storage = builder.Storage;
        GPU = builder.GPU;
        HasWifi = builder.HasWifi;
        HasBluetooth = builder.HasBluetooth;
    }
}

// Builder: (كلاس البناء المخصص لتجميع الخصائص خطوة بخطوة)
public class ComputerBuilder
{
    // (خصائص مؤقتة لتجميع قيمها تدريجياً مع قيم افتراضية)
    public string CPU { get; private set; } = "Intel i3"; 
    public string RAM { get; private set; } = "4GB";
    public string Storage { get; private set; } = "128GB SSD";
    public string? GPU { get; private set; } = null;
    public bool HasWifi { get; private set; } = false;
    public bool HasBluetooth { get; private set; } = false;

    // Fluent Interface: (ميثودز ترجع الكائن نفسه لتسمح بربط العمليات متتالية خلف بعضها)
    
    public ComputerBuilder SetCPU(string cpu)
    {
        CPU = cpu;
        return this; // (نعيد الكائن الحالي لربط العمليات)
    }

    public ComputerBuilder SetRAM(string ram)
    {
        RAM = ram;
        return this;
    }

    public ComputerBuilder SetStorage(string storage)
    {
        Storage = storage;
        return this;
    }

    public ComputerBuilder SetGPU(string? gpu)
    {
        GPU = gpu;
        return this;
    }

    public ComputerBuilder SetWifi(bool hasWifi)
    {
        HasWifi = hasWifi;
        return this;
    }

    public ComputerBuilder SetBluetooth(bool hasBluetooth)
    {
        HasBluetooth = hasBluetooth;
        return this;
    }

    // Build: (الميثود النهائية التي تنشئ المنتج الفعلي بعد اكتمال البناء)
    public Computer Build()
    {
        return new Computer(this);
    }
}

// Demo Runner: (مشغل الديمو بعد تطبيق الباترين)
public static class BuilderAfterDemo
{
    public static void Run()
    {
        Console.WriteLine("\n--- Running AFTER Builder Pattern ---");

        // Method Chaining: (بناء جهاز كمبيوتر بالمواصفات المطلوبة فقط دون الحاجة لتمرير قيم فارغة)
        var builder = new ComputerBuilder();
        
        Computer gamingComputer = builder.SetCPU("Intel i9")
                                         .SetRAM("32GB")
                                         .SetStorage("2TB SSD")
                                         .SetGPU("RTX 4090")
                                         .SetWifi(true)
                                         .Build();

        Console.WriteLine($"Gaming Computer: CPU={gamingComputer.CPU}, RAM={gamingComputer.RAM}, GPU={gamingComputer.GPU}");
        Console.WriteLine($"Has Wifi: {gamingComputer.HasWifi}, Has Bluetooth: {gamingComputer.HasBluetooth}");
    }
}
