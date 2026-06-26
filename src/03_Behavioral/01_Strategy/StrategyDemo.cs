using System;

namespace DesignPatterns.ConsoleApp.Patterns._01_Strategy;

// ====================================================================================
// ✅ After Strategy Pattern
// الكود بعد التحسين
// ====================================================================================

// Note: (ملاحظة: بمجرد فصل الأكواد في كلاسات منفصلة، استفدنا أيضاً من تطبيق مبادئ تصميم جديدة مثل)
// 1. Single Responsibility Principle: (كل كلاس أصبح له مسؤولية واحدة فقط وهي طريقة خصم واحدة)
// 2. Open/Closed Principle: (إمكانية إضافة خصم جديد بإنشاء كلاس جديد دون لمس الكود القديم)
// 3. Dependency Inversion Principle: (كلاس الخدمة يعتمد على الواجهة وليس على الكلاسات المحددة مباشرة)

// Interface: (نحدد العقد المشترك لجميع طرق الخصم)
public interface IDiscountStrategy
{
    // ApplyDiscount: (كل استراتيجية يجب أن تطبق هذه العملية وتستقبل السعر الأصلي)
    decimal ApplyDiscount(decimal price);
}

// Concrete Strategies: (كل طريقة خصم في كلاس منفصل ومستقل)
// Single Responsibility Principle: (بمجرد تقسيم كل نوع خصم في كلاس مستقل، حققنا مبدأ)

// StudentDiscount: (استراتيجية خصم الطلاب وتخصم 20%)
public class StudentDiscount : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal price) => price * 0.80m;
}

// VIPDiscount: (استراتيجية خصم كبار العملاء وتخصم 10%)
public class VIPDiscount : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal price) => price * 0.90m;
}

// NoDiscount: (استراتيجية بدون خصم كحالة افتراضية لتفادي أخطاء القيم الفارغة)
public class NoDiscount : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal price) => price;
}

// Context: (الخدمة التي تستخدم الاستراتيجيات)
public class OrderServiceAfter
{
    // Dependency Inversion Principle: (نحتفظ بمرجع للواجهة وليس لكلاس محدد لتطبيق مبدأ)
    private readonly IDiscountStrategy _discountStrategy;

    // Constructor Injection: (نقوم بحقن الاستراتيجية المطلوبة عبر المشيد)
    public OrderServiceAfter(IDiscountStrategy discountStrategy)
    {
        _discountStrategy = discountStrategy;
    }

    // Delegation: (نحسب السعر النهائي بتفويض العملية للاستراتيجية المحقونة)
    public decimal CalculateFinalPrice(decimal price)
    {
        // (السياق لا يعرف تفاصيل الخصم، فقط يستدعي الميثود المشترك)
        return _discountStrategy.ApplyDiscount(price);
    }
}

// Demo Runner: (مشغل الديمو بعد تطبيق الباترين)
public static class StrategyAfterDemo
{
    public static void Run()
    {
        Console.WriteLine("\n--- Running AFTER Strategy Pattern ---");
        
        // (حساب خصم الطلاب عبر حقن الاستراتيجية المناسبة بالخدمة)
        var studentStrategy = new StudentDiscount();
        var orderService = new OrderServiceAfter(studentStrategy);
        decimal finalPrice = orderService.CalculateFinalPrice(100);
        Console.WriteLine($"Price after pattern (Student): {finalPrice}");

        // (حساب خصم كبار العملاء عبر حقن استراتيجيتهم بالخدمة)
        var vipStrategy = new VIPDiscount();
        var vipOrder = new OrderServiceAfter(vipStrategy);
        decimal vipPrice = vipOrder.CalculateFinalPrice(100);
        Console.WriteLine($"Price after pattern (VIP): {vipPrice}");
    }
}
