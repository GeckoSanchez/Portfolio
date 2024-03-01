namespace ZData00.Values
{
	using System.Diagnostics;
	using Actions;
	using Attributes;
	using Bases;
	using Exceptions;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class PhoneDigits : Base<string>
	{
		[JsonConstructor, MainConstructor]
		public PhoneDigits(string value) : base(value) => Log.Event(new StackFrame(true));

		public PhoneDigits(params char[] values) : this("")
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				if (!values.All(char.IsAsciiDigit))
					throw new Exception($"");
			}
			catch (Exception ex)
			{
				throw new ValueException(ex, sf);
			}
		}
	}
}
