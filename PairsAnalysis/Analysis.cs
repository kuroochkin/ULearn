namespace PairsAnalysis;

public static class Analysis
{
	public static int FindMaxPeriodIndex(params DateTime[] data)
	{
		if (data.Count() == 0)
			throw new InvalidOperationException();
		return data
			.Pairs()
			.Select((pair) => (pair.Item2 - pair.Item1).TotalSeconds)
			.MaxIndex();
	}

	public static double FindAverageRelativeDifference(params double[] data)
	{
		if (data.Count() <= 1)
			throw new InvalidOperationException();
		var relativeDifferences = data
			.Pairs()
			.Select((pair) => (pair.Item2 - pair.Item1) / pair.Item1);
		return relativeDifferences.Sum() / relativeDifferences.Count();
	}
}

public static class ExtentionsAnalysis
{
	public static IEnumerable<Tuple<T, T>> Pairs<T>(this IEnumerable<T> dates)
	{
		var flag = true;
		var previous = default(T);
		foreach (var item in dates)
		{
			if (flag)
			{
				previous = item;
				flag = false;
				continue;
			}
			yield return Tuple.Create(previous, item);
			previous = item;
		}
	}

	public static int MaxIndex<T>(this IEnumerable<T> data)
	where T : IComparable<T>
	{
		return data.Select((x, y) => Tuple.Create(x, y))
			.OrderByDescending(x => x.Item1)
			.First()
			.Item2;
	}
}



