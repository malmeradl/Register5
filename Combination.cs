class Combination : Element
{
    public unsafe bool* InputValues { get; private set; }//значения входов
    private readonly Func<bool, bool, bool> Comparator;

    public unsafe Combination(string name, int inputCount, Func<bool, bool, bool> comparator) : base(name, inputCount, outputCount: 1)
    {
        bool[] arr = new bool[InputCount];
        Comparator = comparator;

        fixed (bool* pointer = arr)
        {
            InputValues = pointer;
        }
        Console.WriteLine($"Combination: {name} ({InputCount})\n");
    }

    public unsafe Combination(Combination other) : base(other)//копирование
    {
        bool[] arr = new bool[other.InputCount];
        Comparator = other.Comparator;

        fixed (bool* pointer = arr)
        {
            InputValues = pointer;
            for (int i = 0; i < other.InputCount; i++)
            {
                InputValues[i] = other.InputValues[i];
            }
        }

        Console.WriteLine($"Combination {Name} has been copied.\n");
    }

    ~Combination()
    {
        Console.WriteLine($"Combination {Name} was destructed.\n");
    }

    public unsafe void SetInputValues(params bool[] inputValues)
    {
        if (inputValues.Length != InputCount)
        {
            throw new ArgumentException($"Invalid input array length. Expected length: {InputCount}");
        }

        for (int i = 0; i < InputCount; i++)
        {
            InputValues[i] = inputValues[i];
        }
    }


    public unsafe bool GetInputValue(int index)
    {
        if (index < 0 || index >= InputCount)
        {
            throw new ArgumentOutOfRangeException(nameof(index), $"Invalid Input index: {index}");
        }

        Console.WriteLine($"Input {index} value: {InputValues[index]}\n");
        return InputValues[index];
    }

    public unsafe bool ComputeOutput()
    {
        bool result = InputValues[0];
        if (InputCount == 1)
        {
            result = Comparator(result, false);
        }
        else
        {

            for (int i = 1; i < InputCount; i++)
            {
                result = Comparator(result, InputValues[i]);
            }
        }
        Console.WriteLine($"Output: {result}\n");
        return result;
    }

    // private unsafe bool IsInputValuesEnough()
    // {
    //     if (InputCount < 2)
    //     {
    //         throw new InvalidOperationException("These values are not enough to calculate the output value.\n");
    //     }
    //     return true;
    // }
}
