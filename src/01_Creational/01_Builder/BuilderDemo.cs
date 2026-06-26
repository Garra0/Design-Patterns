using System;

namespace DesignPatterns.ConsoleApp.Patterns._02_Builder;

// ====================================================================================
// ✅ After Builder Pattern
// الكود بعد التحسين
// ====================================================================================

// Note: (ملاحظة: بفصل منطق البناء عن كلاس البيانات، حققنا مبادئ تصميم هامة مثل)
// 1. Single Responsibility Principle: (كلاس الكمبيوتر مسؤول فقط عن تخزين البيانات، وكلاس البناء مسؤول عن كيفية تجميع المواصفات)
// 2. Open/Closed Principle: (يمكننا إضافة مواصفات جديدة بالبناء دون تعديل المشيد أو كسر الكود القديم للعملاء)

// Product: (الكائن المراد بناؤه)
public class Computer
{
    // (قيم افتراضية لتفادي أخطاء null)
    public string CPU { get; set; } = "Intel i3";
    public string RAM { get; set; } = "4GB";
    public string Storage { get; set; } = "128GB SSD";
    public bool HasGPU { get; set; }
    public bool HasWiFi { get; set; }
}

// Builder: (كلاس البناء المخصص لتجميع الخصائص خطوة بخطوة)
public class ComputerBuilder
{
    private readonly Computer _computer = new Computer();

    // Fluent Interface: (ميثودز ترجع الكائن نفسه لتسمح بربط العمليات متتالية خلف بعضها)

    // AddCPU: (تقوم بتجهيز المعالج ثم ترجع نفس البناء)
    public ComputerBuilder AddCPU(string cpu)
    {
        _computer.CPU = cpu;
        return this;
    }

    // AddRAM: (تقوم بتجهيز الرام ثم ترجع نفس البناء)
    public ComputerBuilder AddRAM(string ram)
    {
        _computer.RAM = ram;
        return this;
    }

    // AddStorage: (تقوم بتجهيز الهارد ثم ترجع نفس البناء)
    public ComputerBuilder AddStorage(string storage)
    {
        _computer.Storage = storage;
        return this;
    }

    // WithGPU: (تقوم بتجهيز كرت الشاشة ثم ترجع نفس البناء)
    public ComputerBuilder WithGPU()
    {
        _computer.HasGPU = true;
        return this;
    }

    // WithWiFi: (تقوم بتجهيز الواي فاي ثم ترجع نفس البناء)
    public ComputerBuilder WithWiFi()
    {
        _computer.HasWiFi = true;
        return this;
    }

    // Build: (الدالة النهائية التي ترجع الكائن النهائي المكتمل)
    public Computer Build()
    {
        return _computer;
    }
}

// Demo Runner: (مشغل الديمو بعد تطبيق الباترين)
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
