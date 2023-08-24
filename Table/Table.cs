namespace Generics.Tables;

public class Table<TRow, TColumn, TVal>
{
	public List<TRow> Rows = new List<TRow>();
	public List<TColumn> Columns = new List<TColumn>();
	private Dictionary<string, TVal> vals = new Dictionary<string, TVal>();
	private OpenIndexer openIdx;
	private ExistedIndexer existIdx;

	public Table()
	{
		openIdx = new OpenIndexer(this);
		existIdx = new ExistedIndexer(this);
	}

	public void AddRow(TRow r)
	{
		if (!Rows.Contains(r)) Rows.Add(r);
	}

	public void AddColumn(TColumn c)
	{
		if (!Columns.Contains(c)) Columns.Add(c);
	}

	public OpenIndexer Open => openIdx;

	public ExistedIndexer Existed => existIdx;
	public class ExistedIndexer
	{
		private Table<TRow, TColumn, TVal> table;

		internal ExistedIndexer(Table<TRow, TColumn, TVal> table)
		{
			this.table = table;
		}

		public TVal this[TRow r, TColumn c]
		{
			get
			{
				var addr = r + ":" + c;
				if (!table.Rows.Contains(r) || !table.Columns.Contains(c)) throw new ArgumentException();
				if (!table.vals.ContainsKey(addr)) return default(TVal);
				return table.vals[addr];
			}
			set
			{
				var addr = r + ":" + c;
				if (!table.Rows.Contains(r) || !table.Columns.Contains(c)) throw new ArgumentException();
				if (!table.vals.ContainsKey(addr))
					table.vals.Add(addr, value);
				else table.vals[addr] = value;
			}
		}
	}

	public class OpenIndexer
	{
		private Table<TRow, TColumn, TVal> table;

		internal OpenIndexer(Table<TRow, TColumn, TVal> table)
		{
			this.table = table;
		}

		public TVal this[TRow r, TColumn c]
		{
			get
			{
				var addr = r + ":" + c;
				if (!table.vals.ContainsKey(addr)) return default(TVal);
				return table.vals[addr];
			}
			set
			{
				var addr = r + ":" + c;
				if (!table.Rows.Contains(r)) table.Rows.Add(r);
				if (!table.Columns.Contains(c)) table.Columns.Add(c);
				if (!table.vals.ContainsKey(addr))
					table.vals.Add(addr, value);
				else table.vals[addr] = value;
			}
		}
	}
}