public class BooleanComparators
{
    public static bool LogicalAnd(bool a, bool b)
    {
        return a && b;
    }

    public static bool LogicalOr(bool a, bool b)
    {
        return a || b;
    }

    public static bool LogicalXor(bool a, bool b)
    {
        return a ^ b;
    }

    public static bool LogicalXnor(bool a, bool b)
    {
        return (a && b) || (!a && !b);
    }

    public static bool LogicalNo(bool a)
    {
        return !a;
    }
}