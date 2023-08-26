using ReportGenerator;

List<Measurement> data = new List<Measurement>
		{
			new Measurement
			{
				Humidity=1,
				Temperature=-10,
			},
			new  Measurement
			{
				Humidity=2,
				Temperature=2,
			},
			new Measurement
			{
				Humidity=3,
				Temperature=14
			},
			new Measurement
			{
				Humidity=2,
				Temperature=30
			}
		};

var actual = ReportMakerHelper.MeanAndStdHtmlReport(data);
Console.WriteLine(actual);
