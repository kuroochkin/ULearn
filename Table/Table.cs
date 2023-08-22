using NUnit.Framework;

namespace Table;

public class Table<TRow, TColumn, TValue>
{

	private bool IsExist(TRow iRow, TColumn iCol)
		=> Rows.Contains(iRow) && Columns.Contains(iCol);

	public TValue this[TRow iRow, TColumn iCol]
	{
		get
		{
			TValue value;
			table.TryGetValue(new Tuple<TRow, TColumn>(iRow, iCol), out value);
			if (!IsExist(iRow, iCol) && !IsOpen)
				throw new ArgumentException();
			return value;
		}
		set
		{
			if (!IsOpen && !IsExist(iRow, iCol))
				throw new ArgumentException();

			if (!Rows.Contains(iRow)) Rows.Add(iRow);
			if (!Columns.Contains(iCol)) Columns.Add(iCol);
			table[new Tuple<TRow, TColumn>(iRow, iCol)] = value;
		}
	}

	public void AddRow(TRow row)
	{
		if (!Rows.Contains(row)) Rows.Add(row);
	}

	public void AddColumn(TColumn col)
	{
		if (!Columns.Contains(col)) Columns.Add(col);
	}
	public Table<TRow, TColumn, TValue> Open
	{
		get
		{
			IsOpen = true;
			return this;
		}
	}
	public Table<TRow, TColumn, TValue> Existed
	{
		get
		{
			IsOpen = false;
			return this;
		}
	}

	private bool IsOpen = true;
	public List<TColumn> Columns { get; private set; } = new List<TColumn>();
	public List<TRow> Rows { get; private set; } = new List<TRow>();

	private Dictionary<Tuple<TRow, TColumn>, TValue> table
		= new Dictionary<Tuple<TRow, TColumn>, TValue>();
}