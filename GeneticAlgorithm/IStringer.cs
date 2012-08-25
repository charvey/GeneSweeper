namespace GeneticAlgorithm
{
    public interface IStringer<T>
    {
        string ValueToString(T v);
        T StringToValue(string s);
    }
}
