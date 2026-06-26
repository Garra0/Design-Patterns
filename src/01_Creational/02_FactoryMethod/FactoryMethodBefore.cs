using System;

namespace DesignPatterns.ConsoleApp.Patterns._03_FactoryMethod;

// ====================================================================================
// ⚠️ Before Factory Method Pattern
// الكود قبل التحسين
// ====================================================================================

public class EmailNotificationBefore
{
    public void Send(string message)
    {
        Console.WriteLine($"Sending Email: {message}");
    }
}

public class SMSNotificationBefore
{
    public void Send(string message)
    {
        Console.WriteLine($"Sending SMS: {message}");
    }
}

public class NotificationServiceBefore
{
    // Switch-case: (المشكلة هنا: نستخدم جمل شرطية لإنشاء الكائنات يدوياً بكلمة new)
    // Open/Closed Principle: (المشكلة هي أن إضافة وسيلة إرسال جديدة تجبرنا على التعديل هنا وهذا ينتهك مبدأ)
    public void SendNotification(string message, string type)
    {
        if (type == "Email")
        {
            var email = new EmailNotificationBefore();
            email.Send(message);
        }
        else if (type == "SMS")
        {
            var sms = new SMSNotificationBefore();
            sms.Send(message);
        }
    }
}

public static class FactoryMethodBeforeDemo
{
    public static void Run()
    {
        Console.WriteLine("--- Running BEFORE Factory Method ---");

        var service = new NotificationServiceBefore();
        service.SendNotification("Hello World!", "Email");
    }
}
