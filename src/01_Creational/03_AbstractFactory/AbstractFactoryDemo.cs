using System;

namespace DesignPatterns.ConsoleApp.Patterns._04_AbstractFactory;

// ====================================================================================
// ✅ After Abstract Factory Pattern
// الكود بعد التحسين
// ====================================================================================

// Note: (ملاحظة: بفصل عائلات الكائنات في مصانع مخصصة، استفدنا من تطبيق مبادئ تصميم هامة مثل)
// 1. Single Responsibility Principle: (كود إنشاء عائلة الأزرار والنصوص بأكمله معزول داخل كلاس المصنع الخاص بالثيم)
// 2. Open/Closed Principle: (يمكننا إضافة ثيم جديد بالكامل بإنشاء كلاس مصنع وكلاسات عناصر جديدة دون لمس كود الـ Application)

// Abstract Product A: (الواجهة المشتركة للمنتج الأول)
public interface IButton
{
    void Render();
}

// Concrete Product A1: (زر الثيم الداكن)
public class DarkButton : IButton
{
    public void Render() => Console.WriteLine("Rendering Dark Button");
}

// Concrete Product A2: (زر الثيم المضيء)
public class LightButton : IButton
{
    public void Render() => Console.WriteLine("Rendering Light Button");
}

// Abstract Product B: (الواجهة المشتركة للمنتج الثاني)
public interface ITextBox
{
    void Show();
}

// Concrete Product B1: (صندوق نصوص الثيم الداكن)
public class DarkTextBox : ITextBox
{
    public void Show() => Console.WriteLine("Showing Dark TextBox");
}

// Concrete Product B2: (صندوق نصوص الثيم المضيء)
public class LightTextBox : ITextBox
{
    public void Show() => Console.WriteLine("Showing Light TextBox");
}

// Abstract Factory: (المصنع المجرد الذي يحدد ميثودز إنشاء المنتجات المتوافقة)
public interface IUIFactory
{
    IButton CreateButton();
    ITextBox CreateTextBox();
}

// Concrete Factory 1: (مصنع الثيم الداكن المسؤول عن تجميع عائلة العناصر الداكنة)
public class DarkThemeFactory : IUIFactory
{
    public IButton CreateButton() => new DarkButton();
    public ITextBox CreateTextBox() => new DarkTextBox();
}

// Concrete Factory 2: (مصنع الثيم المضيء المسؤول عن تجميع عائلة العناصر المضيئة)
public class LightThemeFactory : IUIFactory
{
    public IButton CreateButton() => new LightButton();
    public ITextBox CreateTextBox() => new LightTextBox();
}

// Client: (كلاس العميل الذي يستخدم المصنع لإنشاء المنتجات دون معرفة كلاساتها الدقيقة)
public class Application
{
    private readonly IButton _button;
    private readonly ITextBox _textBox;

    // Dependency Injection: (حقن المصنع المجرد المناسب للعميل)
    public Application(IUIFactory factory)
    {
        // (المصنع يضمن برمجياً عدم خلط كتل برمجية من ثيمات مختلفة)
        _button = factory.CreateButton();
        _textBox = factory.CreateTextBox();
    }

    public void RunUI()
    {
        _button.Render();
        _textBox.Show();
    }
}

// Demo Runner: (مشغل الديمو بعد تطبيق الباترين)
public static class AbstractFactoryAfterDemo
{
    public static void Run()
    {
        Console.WriteLine("\n--- Running AFTER Abstract Factory ---");

        // UI Consistency: (بتمرير مصنع الثيم الداكن، يضمن النظام إنشاء كافة العناصر متوافقة تماماً)
        IUIFactory darkFactory = new DarkThemeFactory();
        var app = new Application(darkFactory);
        app.RunUI();

        // (التبديل إلى الثيم المضيء بسهولة عن طريق تغيير المصنع الممرر فقط)
        IUIFactory lightFactory = new LightThemeFactory();
        app = new Application(lightFactory);
        app.RunUI();
    }
}
