using System;

namespace DesignPatterns.ConsoleApp.Patterns._04_AbstractFactory;

// ====================================================================================
// ⚠️ Before Abstract Factory Pattern
// الكود قبل التحسين
// ====================================================================================

public class DarkButtonBefore
{
    public void Render() => Console.WriteLine("Rendering Dark Button");
}

public class LightButtonBefore
{
    public void Render() => Console.WriteLine("Rendering Light Button");
}

public class DarkTextBoxBefore
{
    public void Show() => Console.WriteLine("Showing Dark TextBox");
}

public class LightTextBoxBefore
{
    public void Show() => Console.WriteLine("Showing Light TextBox");
}

public class ApplicationBefore
{
    // Switch-case: (المشكلة هنا: نتحكم بالإنشاء يدوياً ونكتب شروطاً في كل مكان لإنشاء عناصر الثيم)
    // Open/Closed Principle: (المشكلة هي أن إضافة ثيم جديد يجبرنا على إضافة شروط جديدة وتعديل كود البناء الفعلي)
    public void AssembleUI(string theme)
    {
        if (theme == "Dark")
        {
            var button = new DarkButtonBefore();
            var textBox = new DarkTextBoxBefore();
            button.Render();
            textBox.Show();
        }
        else if (theme == "Light")
        {
            // Mismatch: (خطر خلط كائنات من ثيمات مختلفة بالخطأ إذا لم ننتبه للشرط)
            var button = new LightButtonBefore();
            var textBox = new LightTextBoxBefore();
            button.Render();
            textBox.Show();
        }
    }
}

public static class AbstractFactoryBeforeDemo
{
    public static void Run()
    {
        Console.WriteLine("--- Running BEFORE Abstract Factory ---");

        var app = new ApplicationBefore();
        app.AssembleUI("Dark");
    }
}
