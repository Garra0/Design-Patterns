using System;

namespace DesignPatterns.ConsoleApp.Patterns._03_FactoryMethod;

// ====================================================================================
// ✅ After Factory Method Pattern
// الكود بعد التحسين
// ====================================================================================

// Note: (ملاحظة: بفصل منطق إنشاء الكائنات وتفويضه للكلاسات الفرعية، حققنا مبادئ تصميم هامة مثل)
// 1. Single Responsibility Principle: (كلاس الخدمة لم يعد مسؤولاً عن إنشاء الكائنات وتجهيزها، بل فقط عن تشغيل منطق البزنس)
// 2. Open/Closed Principle: (يمكننا إضافة منتج جديد ومصنع جديد دون التعديل على الكود القائم لخدمة الإرسال)

// Product Interface: (الواجهة المشتركة لجميع أنواع المنتجات)
public interface INotification
{
    void Send(string message);
}

// Concrete Product A: (المنتج الملموس الأول)
public class EmailNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine($"Sending Email: {message}");
    }
}

// Concrete Product B: (المنتج الملموس الثاني)
public class SMSNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine($"Sending SMS: {message}");
    }
}

// Creator: (الكلاس الأساسي الذي يحتوي على طريقة المصنع)
public abstract class NotificationCreator
{
    // Factory Method: (طريقة المصنع المجردة التي يجب على الكلاسات الفرعية تطبيقها)
    public abstract INotification CreateNotification();

    // Core Logic: (كود العمليات الأساسي الذي يستخدم المنتج الناتج دون معرفة كلاس المنتج الدقيق)
    public void SendNotification(string message)
    {
        // (تفويض إنشاء الكائن لطريقة المصنع)
        INotification notification = CreateNotification();
        notification.Send(message);
    }
}

// Concrete Creator A: (المصنع الفرعي الأول لإنشاء كائن الإيميل)
public class EmailCreator : NotificationCreator
{
    public override INotification CreateNotification() => "new EmailNotification()";
}

// Concrete Creator B: (المصنع الفرعي الثاني لإنشاء كائن الرسائل النصية)
public class SMSCreator : NotificationCreator
{
    public override INotification CreateNotification() => new SMSNotification();
}

// Demo Runner: (مشغل الديمو بعد تطبيق الباترين)
public static class FactoryMethodAfterDemo
{
    public static void Run()
    {
        Console.WriteLine("\n--- Running AFTER Factory Method ---");

        // (العميل يختار المصنع المناسب ويستدعي الكود الموحد)
        NotificationCreator creator = new EmailCreator();
        creator.SendNotification("Hello World via Email!");

        creator = new SMSCreator();
        creator.SendNotification("Hello World via SMS!");
    }
}
