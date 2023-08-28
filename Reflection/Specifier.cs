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
		var attributeParametrs = typeof(T).GetMethods().Where(x => x.GetCustomAttributes().OfType<ApiMethodAttribute>().Any())
			.Where(x => x.Name == methodName)
			.FirstOrDefault()
			.GetParameters()
			.Where(x => x.Name == paramName)
			.FirstOrDefault();

		var result = new ApiParamDescription
		{
			ParamDescription = new CommonDescription(paramName, attributeParametrs.GetCustomAttributes().OfType<ApiDescriptionAttribute>().First().Description),
			Required = attributeParametrs.GetCustomAttributes().OfType<ApiRequiredAttribute>().First().Required,
			MinValue = attributeParametrs.GetCustomAttributes().OfType<ApiIntValidationAttribute>().First().MinValue,
			MaxValue = attributeParametrs.GetCustomAttributes().OfType<ApiIntValidationAttribute>().First().MaxValue,
		};

		return result;
	}
		
	public ApiMethodDescription GetApiMethodFullDescription(string methodName)
	{
		var method = typeof(T).GetMethods().Where(x => x.GetCustomAttributes().OfType<ApiMethodAttribute>().Any())
			.Where(x => x.GetCustomAttributes().OfType<ApiDescriptionAttribute>().Any())
			.Where(x => x.Name == methodName)
			.FirstOrDefault();

		if (method.GetParameters() == null)
			return null;

		var paramDescriptions = method.GetParameters().Select(item => new ApiParamDescription
		{
			ParamDescription = new CommonDescription(item.Name, item.GetCustomAttributes().OfType<ApiDescriptionAttribute>().First().Description),
			Required = item.GetCustomAttributes().OfType<ApiRequiredAttribute>().First().Required,
			MinValue = item.GetCustomAttributes().OfType<ApiIntValidationAttribute>().First().MinValue,
			MaxValue = item.GetCustomAttributes().OfType<ApiIntValidationAttribute>().First().MaxValue,
		})
		.ToArray();

		var result = new ApiMethodDescription
		{
			MethodDescription = new CommonDescription(
				methodName, 
				method.GetCustomAttributes().OfType<ApiDescriptionAttribute>().First().Description
				),
			ParamDescriptions = paramDescriptions,
			ReturnDescription = new ApiParamDescription
			{
				ParamDescription = new CommonDescription(),
				Required = method.GetCustomAttributes().OfType<ApiRequiredAttribute>().First().Required,
				MinValue = method.GetCustomAttributes().OfType<ApiIntValidationAttribute>().First().MinValue,
				MaxValue = method.GetCustomAttributes().OfType<ApiIntValidationAttribute>().First().MaxValue,
			}
		};

		return result;
	}
}


