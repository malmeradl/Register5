class Register
{
    private Memory[] Triggers;

    public int BitWidth { get; private set; }
    public bool ResetValue { get; private set; }
    public bool SetValue { get; private set; }
    public bool Clk { get; private set; }
    public bool[] RegisterInputsValues { get; private set; }
    public TriggerType triggerType { get; private set; }

    public Register(int bitWidth, TriggerType type)
    {
        triggerType = type;
        BitWidth = bitWidth;
        Triggers = new Memory[bitWidth];

        for (int i = 0; i < bitWidth; i++)
        {
            Triggers[i] = new Memory(type);
        }
        int size = BitWidth * (Memory.GetTriggerInputCount(type) - 1); // -clk
        Console.WriteLine(size);
        RegisterInputsValues = new bool[size];
    }

    public void SetSetValue(bool value)
    {
        SetValue = value;
    }

    public void SetResetValue(bool value)
    {
        ResetValue = value;
    }

    public void SetCLK(bool value)
    {
        Clk = value;
        foreach (Memory trigger in Triggers)
        {
            trigger.SetInputValueByIndex(0, value);
        }
    }

    public bool SetRegisterInputs(params bool[] inputs)
    {
        if (RegisterInputsValues.Length != inputs.Length)
        {
            throw new ArgumentException("Invalid number of input values.");
        }

        Array.Copy(inputs, RegisterInputsValues, inputs.Length);

        InputsValuesToTriggers();

        return true;
    }
    private void InputsValuesToTriggers()
    {
        int j = 0;
        foreach (Memory trigger in Triggers)
        {
            for (int i = 1; i < Memory.GetTriggerInputCount(triggerType); i++)
            {
                trigger.SetInputValueByIndex(i, RegisterInputsValues[j]);
                j++;
            }

        }
    }

    public bool GetTriggerOutputByIndex(int index)
    {
        if (index < 0 || index >= Triggers.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(index), $"Invalid output index: {index}");
        }

        return Triggers[index].Q;
    }

    public void ComputeState()
    {
        if (ResetValue && SetValue)
        {
            throw new InvalidOperationException("Set and reset cannot be active at the same time.");
        }

        foreach (Memory trigger in Triggers)
        {
            if (SetValue)
            {
                trigger.Set();
                SetSetValue(false);
            }
            else if (ResetValue)
            {
                trigger.Reset();
                SetResetValue(false);
            }
            else
            {
                trigger.ComputeState();
            }
        }
        SetCLK(false);
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"\n\nRegister Information:");
        Console.WriteLine($"Bit Width: {BitWidth}");
        Console.WriteLine($"Reset Value: {ResetValue}");
        Console.WriteLine($"Set Value: {SetValue}");
        Console.WriteLine($"Clock (Clk): {Clk}");

        Console.WriteLine("Register Inputs:");
        for (int i = 0; i < RegisterInputsValues.Length; i++)
        {
            Console.WriteLine($"Input {i}: {RegisterInputsValues[i]}");
        }
        Console.WriteLine("\n");
        for (int i = 0; i < Triggers.Length; i++)
        {
            Console.WriteLine($"Trigger {i}:");
            Triggers[i].DisplayState();
        }
    }

}
