using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace ReportGenerator;

public static class ReportMaker
{
	public static string MakeReport(
		string caption,
		Func<IEnumerable<double>, object> calc,
		Func<string, string, string, string> mark,
		IEnumerable<Measurement> data)
	{
		var temperature = calc(data.Select(x => x.Temperature)).ToString();
		var humidity = calc(data.Select(x => x.Humidity)).ToString();
		return mark(caption, temperature, humidity);
	}
}


public static class ReportData
{
	public const string MeanAndStd = "Mean and Std";
	public const string Median = "Median";

	public static object MeanAndStdCalculate(IEnumerable<double> _data)
	{
		var data = _data.ToList();
		var mean = data.Average();
		var std = Math.Sqrt(data.Select(z => Math.Pow(z - mean, 2)).Sum() / (data.Count - 1));
		return new MeanAndStd { Mean = mean, Std = std };
	}

	public static object MedianCalculate(IEnumerable<double> data)
	{
		var list = data.OrderBy(z => z).ToList();
		if (list.Count % 2 == 0)
			return (list[list.Count / 2] + list[list.Count / 2 - 1]) / 2;

		return list[list.Count / 2];
	}

	public static string ToHtml(string caption, string temperature, string humidity)
	{
		var str = new StringBuilder();
		str.Append($"<h1>{caption}<h1>");
		str.Append($"<ul><li><b>Temperature</b>: {temperature}<li><b>Humidity</b>: {humidity}</ul>");
		return str.ToString();
	}

	public static string ToMarkdown(string caption, string temperature, string humidity)
	{
		var str = new StringBuilder();
		str.Append($"## {caption}\n\n");
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
			ReportData.MeanAndStd,
			ReportData.MeanAndStdCalculate,
			ReportData.ToMarkdown,
			data
			);
	}

	public static string MedianMarkdownReport(IEnumerable<Measurement> data)
	{
		return ReportMaker.MakeReport(
			ReportData.Median,
			ReportData.MedianCalculate,
			ReportData.ToMarkdown,
			data
			);
	}

	public static string MeanAndStdHtmlReport(IEnumerable<Measurement> data)
	{
		return ReportMaker.MakeReport(
			ReportData.MeanAndStd,
			ReportData.MeanAndStdCalculate,
			ReportData.ToHtml,
			data
			);
	}

	public static string MedianHtmlReport(IEnumerable<Measurement> data)
	{
		return ReportMaker.MakeReport(
			ReportData.Median,
			ReportData.MedianCalculate,
			ReportData.ToHtml,
			data
			);
	}
}
