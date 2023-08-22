using NUnit.Framework;

namespace Table;

public class Table<TRows, TColumns, TValues>
{
	public TValues[,] Values { get; set; }

	public Dictionary<TRows, int> Rows { get; set; }

	public Dictionary<TColumns, int> Columns { get; set; }

	public ClassOpen<TRows, TColumns, TValues> Open
	{
		get { return new ClassOpen<TRows, TColumns, TValues>(this); }
	}

	public ClassExisted<TRows, TColumns, TValues> Existed
	{
		get { return new ClassExisted<TRows, TColumns, TValues>(this); }
	}

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
		if (BoolAddRow(rowItem))
			++_rowsCount.count;
	}

	private bool BoolAddColumn(TColumns columnItem)
	{
		if (!Columns.ContainsKey(columnItem))
		{
			Columns[columnItem] = _columnsCounter;
			++_columnsCounter;
			return true;
		}

		return false;
	}
	public void AddColumn(TColumns columnItem)
	{
		if (BoolAddColumn(columnItem))
			++_columnsCount.count;
	}
}

public class ClassOpen<TRows, TColumns, TValues>
{
	public TValues[,] Values { get; set; }
	public ClassOpen(Table<TRows, TColumns, TValues> table)
	{
		Values = table.Values;
	}
}

public class ClassExisted<TRows, TColumns, TValues>
{
	public TValues[,] Values { get; set; }
	public ClassExisted(Table<TRows, TColumns, TValues> table)
	{
		Values = table.Values;
	}
}
