using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatterns.ConsoleApp.Patterns._05_InOutbox;

// ====================================================================================
// ✅ After Outbox Pattern
// الكود بعد التحسين
// ====================================================================================

public class Order
{
    public int Id { get; set; }
    public string Product { get; set; } = "";
}

// OutboxMessage: (كلاس يمثل جدول الصادر بالداتابيز لتخزين الأحداث)
public class OutboxMessage
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string EventType { get; set; } = "";
    public string Payload { get; set; } = "";
    public bool IsProcessed { get; set; } = false;
}

// MockDatabase: (محاكاة قاعدة البيانات التي تدعم العمليات الموحدة)
public class MockDatabase
{
    public List<Order> Orders { get; } = new();
    public List<OutboxMessage> Outbox { get; } = new();

    // Transaction: (محاكاة بدء معاملة محلية لضمان حفظ كل شيء أو إلغائه)
    public void SaveInTransaction(Order order, OutboxMessage message)
    {
        Orders.Add(order);
        Outbox.Add(message);
        Console.WriteLine($"Database: Order {order.Id} and Outbox Event saved inside Transaction.");
    }
}

// MockMessageBroker: (محاكاة وسيط الرسائل والذي قد يتعطل أحياناً)
public class MockMessageBroker
{
    public bool IsOnline { get; set; } = false; // (مغلق بالبداية لمحاكاة العطل)

    public void Publish(string message)
    {
        if (!IsOnline)
        {
            throw new Exception("Message Broker is offline!");
        }
        Console.WriteLine($"Broker: Event published successfully -> {message}");
    }
}

// OrderServiceAfter: (كلاس الخدمة بعد إعادة الهيكلة)
public class OrderServiceAfter
{
    private readonly MockDatabase _db;

    public OrderServiceAfter(MockDatabase db)
    {
        _db = db;
    }

    public void CreateOrder(int id, string product)
    {
        var order = new Order { Id = id, Product = product };
        
        var outboxMessage = new OutboxMessage
        {
            EventType = "OrderCreated",
            Payload = $"OrderCreatedEvent: Id={id}"
        };

        // Atomicity: (حفظ بيانات الطلب وحفظ الحدث في الصادر معاً في نفس المعاملة المحلية)
        _db.SaveInTransaction(order, outboxMessage);
    }
}

// OutboxPublisher: (المعالج الخلفي لقراءة الصادر وإرساله للمستلم)
public class OutboxPublisher
{
    private readonly MockDatabase _db;
    private readonly MockMessageBroker _broker;

    public OutboxPublisher(MockDatabase db, MockMessageBroker broker)
    {
        _db = db;
        _broker = broker;
    }

    // Polling Publisher: (ميثود تقوم بالاستعلام وإرسال الرسائل غير المعالجة)
    public void PublishPendingEvents()
    {
        var pending = _db.Outbox.Where(m => !m.IsProcessed).ToList();
        
        if (!pending.Any()) return;

        Console.WriteLine($"\nPublisher: Found {pending.Count} pending event(s) in Outbox...");

        foreach (var message in pending)
        {
            try
            {
                // (محاولة الإرسال للبروكر)
                _broker.Publish(message.Payload);
                
                // (على فرض النجاح، نقوم بتعليم المسج كـ مرسلة)
                message.IsProcessed = true;
                Console.WriteLine($"Publisher: Message {message.Id} marked as Processed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Publisher: Failed to publish message {message.Id} -> {ex.Message} (Retrying later...)");
            }
        }
    }
}

// Demo Runner: (مشغل الديمو بعد تطبيق الباترين)
public static class InOutboxAfterDemo
{
    public static void Run()
    {
        Console.WriteLine("\n--- Running AFTER Outbox Pattern ---");

        var db = new MockDatabase();
        var broker = new MockMessageBroker();
        var orderService = new OrderServiceAfter(db);
        var publisher = new OutboxPublisher(db, broker);

        // 1. (المستخدم يقوم بإنشاء طلب بينما البروكر معطل بالكامل)
        broker.IsOnline = false;
        orderService.CreateOrder(10, "Laptop");

        // 2. (المعالج الخلفي يحاول الإرسال ويفشل ولكن البيانات تظل بأمان في الصادر)
        publisher.PublishPendingEvents();

        // 3. (يعود البروكر للعمل من جديد)
        Console.WriteLine("\n[Message Broker is back online!]");
        broker.IsOnline = true;

        // 4. (الدورة القادمة للمعالج الخلفي تنجح في إرسال المسجات المعلقة)
        publisher.PublishPendingEvents();
    }
}
