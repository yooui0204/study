using NetLib;

public class Cmp : IComparer<Tuple<int,int>>
{
    int IComparer<Tuple<int, int>>.Compare(Tuple<int, int>? x, Tuple<int, int>? y)
    {
        if(x.Item2 == y.Item2)
            return x.Item1 - y.Item1;
        else
            return x.Item2 - y.Item2;
    }
}
