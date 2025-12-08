
using adventofcode2025;
using Spectre.Console;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Reflection;


Main();


static void Main()
{
    var greetingsOutput = new Panel(new Markup($":christmas_tree: {xmasPrinter("Advent of Code 2025")} :christmas_tree: "))
        .BorderColor(Color.Red);
    AnsiConsole.Write(greetingsOutput);


    var a = Assembly.GetExecutingAssembly();
    var classes = a.GetTypes().Where(t => typeof(IDay).IsAssignableFrom(t) && t.IsClass);
    string[] implementedDays = new string[classes.Count()];

    foreach (var x in classes.Select((value, index) => new { value, index }))
    {
        string dayClassname = x.value.FullName;
        dayClassname = dayClassname.Replace(x.value.Namespace + ".", "");

        implementedDays[x.index] = dayClassname;
    }

    var selectedDay = implementedDays[implementedDays.Length-1];
    //selectedDay = "Day01";

    if (Environment.GetEnvironmentVariable("DOTNET_MODIFIABLE_ASSEMBLIES") != "debug")
    {
        selectedDay = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Which Day do you wanna run?")
                .PageSize(10)
                .AddChoices(implementedDays));
    }

    if (selectedDay != "")
    {
        string className = selectedDay;
        Type classType = Type.GetType("adventofcode2025." + className);

        if (classType != null)
        {
            Console.WriteLine("Results of " + selectedDay);
            Console.WriteLine("");
            object instance = Activator.CreateInstance(classType);
            MethodInfo methodPart1 = classType.GetMethod("SolvePart1");
            methodPart1.Invoke(instance, null);
            MethodInfo methodPart2 = classType.GetMethod("SolvePart2");
            methodPart2.Invoke(instance, null);
        }
    }
}

static string xmasPrinter(string input)
{
    string inColor = "";
    for (int i = 0; i < input.Length; i++)
    {
        if (i % 2 == 0)
        {
            inColor += $"[red on white]{input[i]}[/]";
        }
        else
        {
            inColor += $"[green on white]{input[i]}[/]";
        }
    }
    return inColor;
}