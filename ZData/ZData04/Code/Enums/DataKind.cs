namespace ZData04.Enums
{
	[Flags]
	public enum DataKind : int
	{
		None = -1,
		/// <summary>
		/// A single Unicode character/code-point
		/// </summary>
		Char = 1 << 0x0,
		/// <summary>
		/// Any country name
		/// </summary>
		Country = 1 << 0x1,
		/// <summary>
		/// An amount of money with a currency symbol
		/// </summary>
		Currency = 1 << 0x2,
		/// <summary>
		/// A creadit card number (Format: NNNN NNNN NNNN NNNN)
		/// </summary>
		CreditCard = 1 << 0x3,
		/// <summary>
		/// A date (Format: YYYY-MM-DD)
		/// </summary>
		Date = Year | Month | Day,
		/// <summary>
		/// A day between 1-31 (depending on the <see cref="Month"/> and the <see cref="Year"/>)
		/// </summary>
		Day = 1 << 0x4,
		/// <summary>
		/// Digits with possibly one letter
		/// </summary>
		DoorNumber = 1 << 0x5,
		/// <summary>
		/// A floating-point number
		/// </summary>
		Float = 1 << 0x6,
		/// <summary>
		/// An hour between 0 and 23
		/// </summary>
		Hour = 1 << 0x7,
		/// <summary>
		/// A signed integer
		/// </summary>
		Int = 1 << 0x8,
		/// <summary>
		/// An IP address version 4 (Format: NNN.NNN.NNN.NNN)
		/// </summary>
		IPv4 = 1 << 0x9,
		/// <summary>
		/// An IP address version 6 (Format: XXXX:XXXX:XXXX:XXXX:XXXX:XXXX)
		/// </summary>
		IPv6 = 1 << 0xA,
		/// <summary>
		/// A millisecond between 0 and 999
		/// </summary>
		Millisecond = 1 << 0xB,
		/// <summary>
		/// A minute between 0 and 59
		/// </summary>
		Minute = 1 << 0xC,
		/// <summary>
		/// A combination with the <see cref="Date"/> <![CDATA[&]]> <see cref="Time"/>
		/// </summary>
		Moment = Date | Time,
		/// <summary>
		/// A month between 1 and 12 (or a <see cref="Months"/>)
		/// </summary>
		Month = 1 << 0xD,
		/// <summary>
		/// A hidden input string
		/// </summary>
		Password = 1 << 0xE,
		/// <summary>
		/// A floating-point number with a % at the end
		/// </summary>
		Percentage = 1 << 0xF,
		/// <summary>
		/// A postal/ZIP code (depending on the <see cref="Country"/>)
		/// </summary>
		PostalCode = 1 << 0x10,
		/// <summary>
		/// Any province name
		/// </summary>
		Province = 1 << 0x11,
		/// <summary>
		/// A <see cref="Int"/> range between <see cref="int.MinValue"/> <![CDATA[&]]> <see cref="int.MaxValue"/>
		/// </summary>
		Range = 1 << 0x12 | Int,
		/// <summary>
		/// A second between 0 and 59
		/// </summary>
		Second = 1 << 0x13,
		/// <summary>
		/// Any state name
		/// </summary>
		State = 1 << 0x14,
		/// <summary>
		/// A conbination of a <see cref="DoorNumber"/>, a <see cref="StreetName"/>, [a <see cref="Province"/>/a <see cref="State"/>], a <see cref="Country"/>, a <see cref="PostalCode"/>
		/// </summary>
		StreetAddress = DoorNumber | StreetName | Country | PostalCode,
		/// <summary>
		/// The street name for the <see cref="StreetAddress"/>
		/// </summary>
		StreetName = 1 << 0x15,
		/// <summary>
		/// A single-line text of Unicode characters
		/// </summary>
		String = 1 << 0x16,
		/// <summary>
		/// A multi-line text of Unicode characters
		/// </summary>
		Text = 1 << 0x17,
		/// <summary>
		/// A combination of an <see cref="Hour"/>:<see cref="Minute"/>:<see cref="Second"/>.<see cref="Millisecond"/>
		/// </summary>
		Time = Hour | Minute | Second | Millisecond,
		/// <summary>
		/// An unsigned integer
		/// </summary>
		UInt = 1 << 0x18,
		/// <summary>
		/// An uploaded file
		/// </summary>
		Upload = 1 << 0x19,
		/// <summary>
		/// A URL/Web address
		/// </summary>
		URL = 1 << 0x1A,
		/// <summary>
		/// A <see cref="UInt"/> range between <see cref="uint.MinValue"/> <![CDATA[&]]> <see cref="uint.MaxValue"/>
		/// </summary>
		URange = 1 << 0x1B | UInt,
		/// <summary>
		/// A year between -9999 to 9999
		/// </summary>
		Year = 1 << 0x1C,
	}
}
