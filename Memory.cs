class Memory : Element
{
    public bool[] InputsValues { get; private set; } // тк все триггеры синхронные 0- элемент Сlk вход
    public bool Q { get; private set; } // состояние на прямом выходе триггера
    public bool QInv { get; private set; } // состояние на инверсном выходе триггера
    private readonly TriggerType triggerType;

    public Memory(TriggerType type) : base(name: type.ToString(), inputCount: GetTriggerInputCount(type), outputCount: 2)
    {
        InputsValues = new bool[InputCount];
        QInv = !Q;
        triggerType = type;
    }

    public Memory(Memory other) : base(other)
    {
        InputsValues = other.InputsValues.Clone() as bool[];
        Q = other.Q;
        QInv = other.QInv;
        triggerType = other.triggerType;
        Console.WriteLine($"Trigger {Name} has been copied.\n");
    }

    ~Memory()
    {
        Console.WriteLine($"Trigger {Name} was destructed.\n");
    }

    public bool GetInputValue(int index)
    {
        ValidateInputIndex(index);
        return InputsValues[index];
    }

    public void SetInputValueByIndex(int index, bool var)
    {
        ValidateInputIndex(index);
        InputsValues[index] = var;
    }

    private void ValidateInputIndex(int index)
    {
        if (index < 0 || index >= InputCount)
        {
            throw new IndexOutOfRangeException("Invalid input index.");
        }
    }

    public bool SetInputValues(params bool[] values)
    {
        if (values.Length != InputCount)
        {
            throw new ArgumentException("Invalid number of input values.");
        }

        Array.Copy(values, InputsValues, values.Length);
        Console.WriteLine($"Input values for Trigger {Name} have been set.\n");
        return true;
    }

    public static int GetTriggerInputCount(TriggerType type)
    {
        switch (type)
        {
            case TriggerType.D:
            case TriggerType.T:
                return 2;
            case TriggerType.JK:
            case TriggerType.RS:
                return 3;
            default:
                throw new ArgumentException($"Unknown trigger type: {type}");
        }
    }

    public void ComputeState()
    {
        switch (triggerType)
        {
            case TriggerType.D:
                DTriggerComputeOut();
                break;
            case TriggerType.T:
                TTriggerComputeOut();
                break;
            case TriggerType.JK:
                JKTriggerComputeOut();
                break;
            case TriggerType.RS:
                RSTriggerComputeOut();
                break;
            default:
                throw new ArgumentException("Unknown trigger type");
        }
    }

    private void DTriggerComputeOut()
    {
        bool inputC = InputsValues[0];
        bool inputD = InputsValues[1];

        if (inputC && inputD)
        {
            Set();
        }
        if (inputC && !inputD)
        {
            Reset();
        }
        QInv = !Q;
    }
    private void TTriggerComputeOut()
    {
        bool inputC = InputsValues[0];
        bool inputT = InputsValues[1];

        if (inputC && inputT)
        {
            InvertQ();
        }
        QInv = !Q;
    }
    private void JKTriggerComputeOut()
    {
        bool inputC = InputsValues[0];
        bool inputJ = InputsValues[1];
        bool inputK = InputsValues[2];

        if (inputC && !inputJ && inputK)
        {
            Reset();
        }
        if (inputC && inputJ && !inputK)
        {
            Set();
        }
        if (inputC && inputJ && inputK)
        {
            InvertQ();
        }
        QInv = !Q;
    }
    private void RSTriggerComputeOut()
    {
        bool inputC = InputsValues[0];
        bool inputR = InputsValues[1];
        bool inputS = InputsValues[2];

        if (inputC && !inputR && inputS)
        {
            Set();
        }
        if (inputC && inputR && !inputS)
        {
            Reset();
        }
        if (inputC && inputR && inputS)
        {
            throw new InvalidOperationException("Forbidden combination for RS trigger.");
        }
        QInv = !Q;
    }
    public void Reset()
    {
        Q = false;
    }

    public void Set()
    {
        Q = true;
    }
    private void InvertQ()
    {
        Q = !Q;
    }

    public bool Equals(Memory other)
    {
        return triggerType.Equals(other.triggerType);
    }

    public void DisplayState()
    {
        Console.WriteLine($"\nTrigger state for {Name}");
        Console.WriteLine($"Direct output (Q): {Q}");
        Console.WriteLine($"Inverted output (QInv): {QInv}\n");
    }
}
