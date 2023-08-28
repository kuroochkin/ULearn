using System.Text;

namespace ReportGenerator;

public static class ReportMaker
{
	public delegate string Mark(string caption, string temperature, string humidity);
	public static string MakeReport(
		string caption,
		Func<IEnumerable<double>, string> calc,
		Mark mark,
		IEnumerable<Measurement> data)
	{
		var temperature = calc(data.Select(x => x.Temperature)).ToString();
		var humidity = calc(data.Select(x => x.Humidity)).ToString();
		return mark(caption, temperature, humidity);
	}
}


public static class ReportCalculator
{
	public static string MeanAndStdCalculate(IEnumerable<double> _data)
	{
		var data = _data.ToList();
		var mean = data.Average();
		var std = Math.Sqrt(data.Select(z => Math.Pow(z - mean, 2)).Sum() / (data.Count - 1));
		var result = new MeanAndStd { Mean = mean, Std = std };

		return result.ToString();
	}

	public static string MedianCalculate(IEnumerable<double> data)
	{
		var list = data.OrderBy(z => z).ToList();
		if (list.Count % 2 == 0)
			return ((list[list.Count / 2] + list[list.Count / 2 - 1]) / 2).ToString();

		var result = list[list.Count / 2];
		return result.ToString();
	}
}


public static class ReportForm
{
	public const string MeanAndStd = "Mean and Std";
	public const string Median = "Median";

	public static string ToHtml(string caption, string temperature, string humidity)
	{
		var str = new StringBuilder();
		str.Append($"<h1>{caption}</h1>");
		str.Append($"<ul><li><b>Temperature</b>: {temperature}<li><b>Humidity</b>: {humidity}</ul>");
		return str.ToString();
	}

	public static string ToMarkdown(string caption, string temperature, string humidity)
	{
		var str = new StringBuilder();
		str.Append($"## {caption}\n\n*");
		str.Append($"* **Temperature**: {temperature}\n\n");
		str.Append($"* **Humidity**: {humidity}\n\n");
		return str.ToString();
	}
}

public static class ReportMakerHelper
{
	public static string MeanAndStdMarkdownReport(IEnumerable<Measurement> data)
	{
		return ReportMaker.MakeReport(
			ReportForm.MeanAndStd,
			ReportCalculator.MeanAndStdCalculate,
			ReportForm.ToMarkdown,
			data
			);
	}

	public static string MedianMarkdownReport(IEnumerable<Measurement> data)
	{
		return ReportMaker.MakeReport(
			ReportForm.Median,
			ReportCalculator.MedianCalculate,
			ReportForm.ToMarkdown,
			data
			);
	}

	public static string MeanAndStdHtmlReport(IEnumerable<Measurement> data)
	{
		return ReportMaker.MakeReport(
			ReportForm.MeanAndStd,
			ReportCalculator.MeanAndStdCalculate,
			ReportForm.ToHtml,
			data
			);
	}

	public static string MedianHtmlReport(IEnumerable<Measurement> data)
	{
		return ReportMaker.MakeReport(
			ReportForm.Median,
			ReportCalculator.MedianCalculate,
			ReportForm.ToHtml,
			data
			);
	}
}

