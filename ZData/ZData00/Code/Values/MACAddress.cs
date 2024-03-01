namespace ZData00.Values
{
	using System.Diagnostics;
	using Actions;
	using Attributes;
	using Bases;
	using Exceptions;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class MACAddress : Base<byte[]>
	{
		[JsonProperty]
		public byte[] Values => Value;

		[JsonConstructor, MainConstructor]
		public MACAddress(byte[] value) : base(Def.MACAdress)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				if (value.Length != 6)
					throw new Exception($"The number of parts of the given MAC address is not 6, which is invalid");
				else
					Value = value;
			}
			catch (Exception ex)
			{
				throw new ValueException(ex, sf);
			}
		}

		public MACAddress() : this(Def.MACAdress)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				Value = new byte[6];
				Random.Shared.NextBytes(Value);
			}
			catch (Exception ex)
			{
				throw new ValueException(ex, sf);
			}
		}

		public override string ToString()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			string? Out = null;

			try
			{
				foreach (var i in Values)
					Out += $"{i:X2}";
			}
			catch (Exception ex)
			{
				throw new ValueException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= base.ToString();
				}
				catch (BaseException)
				{
					Out = "";
				}
			}

			return Out;
		}
	}
}
