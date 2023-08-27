using System.Linq;
using System.Reflection;

namespace Reflection;

public class Specifier<T> : ISpecifier
{
	public string? GetApiDescription() => typeof(T).GetCustomAttributes()
				.OfType<ApiDescriptionAttribute>().FirstOrDefault()?.Description;

	public string[] GetApiMethodNames()
	{
		return typeof(T).GetMethods().
			Where(x => x.GetCustomAttributes().OfType<ApiMethodAttribute>().Any()).
			Select(x => x.Name)
			.ToArray();
	}

	public string GetApiMethodDescription(string methodName)
	{
		var method = typeof(T).GetMethods().Where(x => x.GetCustomAttributes().OfType<ApiMethodAttribute>().Any())
			.Where(x => x.GetCustomAttributes().OfType<ApiDescriptionAttribute>().Any())
			.Where(x => x.Name == methodName)
			.FirstOrDefault();

		if (method is null)
			return null;
		
		return method.GetCustomAttributes().OfType<ApiDescriptionAttribute>().First().Description;
	}

	public string[] GetApiMethodParamNames(string methodName)
	{
		return typeof(T).GetMethods().Where(x => x.GetCustomAttributes().OfType<ApiMethodAttribute>().Any())
			.Where(x => x.Name == methodName)
			.FirstOrDefault()
			.GetParameters()
			.Select(x => x.Name.ToString())
			.ToArray();

	}

	public string GetApiMethodParamDescription(string methodName, string paramName)
	{
		var parametr = typeof(T).GetMethods().Where(x => x.GetCustomAttributes().OfType<ApiMethodAttribute>().Any())
			.Where(x => x.Name == methodName)
			.FirstOrDefault()
			.GetParameters()
			.Where(x => x.Name == paramName)
			.FirstOrDefault();
			
		return parametr.GetCustomAttributes().OfType<ApiDescriptionAttribute>().First().Description;
	}

	public ApiParamDescription GetApiMethodParamFullDescription(string methodName, string paramName)
	{
		throw new NotImplementedException();
	}

	public ApiMethodDescription GetApiMethodFullDescription(string methodName)
	{
		throw new NotImplementedException();
	}
}

