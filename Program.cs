class Program
{
    static void Main()
    {
        Combination comb = new Combination("XNOR", 6, BooleanComparators.LogicalXnor);
        comb.SetInputValues(true, false, false, true, false, false);
        comb.ComputeOutput();
        Console.WriteLine("/////////////////////////////////////////////////////////////");

        Memory t = new Memory(TriggerType.T);
        t.DisplayState();
        t.SetInputValues(true, true);
        t.ComputeState();
        t.DisplayState();
        t.SetInputValues(true, false);
        t.ComputeState();
        t.DisplayState();
        Console.WriteLine("/////////////////////////////////////////////////////////////");

        Register register = new Register(10, TriggerType.T);
        register.SetCLK(true);
        register.SetRegisterInputs(true, false, true, false, false, false, true, true, false, true);
        register.ComputeState();
        register.DisplayInfo();

        Console.WriteLine("/////////////////////////////////////////////////////////////");

        Register register2 = new Register(10, TriggerType.D);
        register2.SetCLK(true);
        register2.SetRegisterInputs(true, false, true, false, false, false, true, true, false, true);
        register2.ComputeState();
        register2.DisplayInfo();


    }
}
