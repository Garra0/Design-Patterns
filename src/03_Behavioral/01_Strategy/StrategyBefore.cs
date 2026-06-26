using System;

namespace DesignPatterns.ConsoleApp.Patterns._01_Strategy;

// ====================================================================================
// ⚠️ Before Strategy Pattern
// الكود قبل التحسين
// ====================================================================================

public class OrderServiceBefore
{
    // المشكلة: إضافة خصم جديد (مثل خصم المعلمين) تجبرنا على تعديل الكود وإضافة حالة جديدة داخل السويتش.
    // Open/Closed Principle: (الكود يجب أن يكون مفتوحاً للتوسيع ومغلقاً للتعديل)
    public decimal CalculateFinalPrice(decimal price, string discountType)
    {
        switch (discountType)
        {
            case "Student":
                return price * 0.80m; // Student Discount: 20% (خصم الطلاب)
            case "VIP":
                return price * 0.90m; // VIP Discount: 10% (خصم كبار العملاء)
            default:
                return price; // No Discount (بدون خصم)
        }
    }
}

public static class StrategyBeforeDemo
{
    public static void Run()
    {
        Console.WriteLine("--- Running BEFORE Strategy Pattern ---");
        
        var orderService = new OrderServiceBefore();
        
        // String: (حساب السعر بنوع خصم نمرره كـ)
        decimal finalPrice = orderService.CalculateFinalPrice(100, "Student");
        
        Console.WriteLine($"Price before pattern: {finalPrice}");
    }
}
