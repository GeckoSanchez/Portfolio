namespace ZData00.Bases
{
	using System.Diagnostics;
	using Actions;
	using Attributes;
	using Exceptions;
	using Newtonsoft.Json;
	using Values;

	[JsonObject(MemberSerialization.OptIn)]
	public class BaseName : Base<string>
	{
		[JsonProperty]
		public new string Value { get => base.Value; protected set => base.Value = value; }

		[JsonConstructor, PrimaryConstructor]
		public BaseName(string value) : base(Def.Name)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			Range<int> range = new(8, 64, 1);

			try
			{
				if (value.Length < range.End)
					throw new Exception($"This name's {Format<string>.ExcValue(value)} length {Format<int>.ExcValue(value.Length)} does not fit in the standard range for a name {Format<Range<int>>.ExcValue(range)}");
				else
					Value = value;
			}
			catch (Exception ex)
			{
				throw new NameException(ex, sf);
			}
		}
	}
}
