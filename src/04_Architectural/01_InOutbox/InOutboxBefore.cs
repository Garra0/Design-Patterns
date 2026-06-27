using System;

namespace DesignPatterns.ConsoleApp.Patterns._05_InOutbox;

// ====================================================================================
// ⚠️ Before Outbox Pattern
// الكود قبل التحسين
// ====================================================================================

public class OrderBefore
{
    public int Id { get; set; }
    public string Product { get; set; } = "";
}

public class MockDatabaseBefore
{
    public void SaveOrder(OrderBefore order)
    {
        Console.WriteLine($"Database: Order {order.Id} saved successfully.");
    }
}

public class MockMessageBrokerBefore
{
    public void Publish(string message)
    {
        // (محاكاة انقطاع الاتصال بالمسج بروكر بشكل عشوائي)
        throw new Exception("Message Broker is offline! Connection refused.");
    }
}

public class OrderServiceBefore
{
    private readonly MockDatabaseBefore _db = new MockDatabaseBefore();
    private readonly MockMessageBrokerBefore _broker = new MockMessageBrokerBefore();

    // Dual-write: (المشكلة هنا: نكتب في مكانين مختلفين بدون معاملة موحدة تضمن وصول المسج)
    public void CreateOrder(int id, string product)
    {
        var order = new OrderBefore { Id = id, Product = product };
        
        // 1. (الحفظ بالداتابيز ينجح بنجاح)
        _db.SaveOrder(order);

        // 2. (محاولة إرسال مسج الحدث تفشل وتضيع البيانات كلياً ولا تكتمل العملية)
        try
        {
            _broker.Publish($"OrderCreated: {order.Id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Broker Error: {ex.Message} (Event lost forever!)");
        }
    }
}

public static class InOutboxBeforeDemo
{
    public static void Run()
    {
        Console.WriteLine("--- Running BEFORE Outbox Pattern ---");

        var service = new OrderServiceBefore();
        service.CreateOrder(1, "Laptop");
    }
}
