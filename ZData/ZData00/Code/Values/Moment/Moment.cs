namespace ZData00.Values
{
	using Attributes;
	using Bases;
	using Newtonsoft.Json;

	[method: JsonConstructor, MainConstructor]
	public class Moment(DateTime value) : Base<DateTime>(value)
	{
	}
}
