class Element
{
    public string Name { get; private set; }
    public int InputCount { get; private set; }
    public int ElementOutputs { get; private set; }


    public Element(string name, int inputCount, int outputCount)
    {
        Name = name;
        InputCount = inputCount;
        ElementOutputs = outputCount;

        //  Console.WriteLine($"Element: {Name} ({InputCount}; {ElementOutputs})\n");
    }

    public Element(Element other)
    {
        Name = other.Name;
        InputCount = other.InputCount;
        ElementOutputs = other.ElementOutputs;

        Console.WriteLine($"Element {Name} has been copied.\n");
    }

    ~Element()
    {
        Console.WriteLine($"Element {Name} was destructed.\n");
    }

    public void SetName(string newName)
    {
        Name = newName;
        Console.WriteLine($"Element name has been changed to: {newName}\n");
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Element name: {Name}");
        Console.WriteLine($"Inputs: {InputCount}");
        Console.WriteLine($"Outputs: {ElementOutputs}\n");
    }
}