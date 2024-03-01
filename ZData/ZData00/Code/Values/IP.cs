namespace ZData00.Values
{
	using System.Diagnostics;
	using System.Net;
	using Actions;
	using Attributes;
	using Bases;
	using Exceptions;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class IP : Base<byte[]>
	{
		[JsonProperty]
		public byte[] Values => Value;

		public enum IPConversion : byte
		{
			None,
			ToIPv4,
			ToIPv6,
		}

		/// <summary>
		/// Primary constructor for the <see cref="IP"/> class
		/// </summary>
		/// <param name="values">The byte array for the IP itself</param>
		[JsonConstructor, MainConstructor]
		protected IP(byte[] values) : base(values)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				if (values.Length != 6 || values.Length != 8)
					throw new Exception($"The number of parts of the given IP address is neither 6 or 8, which is invalid");
				else
					Value = values;
			}
			catch (Exception ex)
			{
				throw new ValueException(ex, sf);
			}
		}

		public IP(IPAddress value, IPConversion conversion = IPConversion.None) : this(Def.IPArray)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				switch (conversion)
				{
					case IPConversion.ToIPv4:
						value = value.MapToIPv4();
						break;
					case IPConversion.ToIPv6:
						value = value.MapToIPv6();
						break;
					case IPConversion.None:
					default:
						break;
				}

				Value = value.GetAddressBytes();
			}
			catch (Exception ex)
			{
				throw new ValueException(ex, sf);
			}
		}

		[EmptyConstructor]
		public IP() : this(Def.IPArray)
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

		public IP(string ip) : this(Def.IPArray)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				Value = IPAddress.Parse(ip).GetAddressBytes();
			}
			catch (Exception ex)
			{
				throw new ValueException(ex, sf);
			}
		}

		public static IP Generate(bool isIPv4 = true)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			IP? Out = null;

			try
			{
				byte[] bytes = isIPv4 ? new byte[6] : new byte[8];
				Random.Shared.NextBytes(bytes);
				Out = new(bytes);
			}
			catch (Exception ex)
			{
				throw new ValueException(ex, sf);
			}
			finally
			{
				Out ??= Def.IPArray;
			}

			return Out;
		}

		public static implicit operator IP(byte[] v) => new(v);

		public override string ToString()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			string? Out = null;

			try
			{
				Out = $"{new IPAddress(Values)}";
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
