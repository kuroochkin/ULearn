using NUnit.Framework;

namespace Table;

public class Table<TRows, TColumns, TValues>
{
	public TValues[,] Values { get; set; }

	public Dictionary<TRows, int> Rows { get; set; }

	public Dictionary<TColumns, int> Columns { get; set; }

	private int _rowsCounter;
	private int _columnsCounter;

	private Counts _rowsCount;
	private Counts _columnsCount;
	public Counts _rows => _rowsCount;
	public Counts _columns => _columnsCount;

	public Table()
	{
		Rows = new Dictionary<TRows, int>(); 
		Columns = new Dictionary<TColumns, int>();
		Values = new TValues[100,100];
	}

	public struct Counts
	{
		public int count;
		public int Count()
		{
			return count;
		}
	}

	private bool BoolAddRow(TRows rowItem)
	{
		if (!Rows.ContainsKey(rowItem))
		{
			Rows[rowItem] = _rowsCounter;
			++_rowsCounter;
			return true;
		}

		return false;
	}

	public void AddRow(TRows rowItem)
	{

		if (add(rowItem))

			++_rowsCount.count;

	}
}
