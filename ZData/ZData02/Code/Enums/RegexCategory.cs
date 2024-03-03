namespace ZData02.Enums
{
	[Flags]
	public enum RegexCategory : ulong
	{
		None,

		UppercaseLetter = 1 << 0x1,
		LowercaseLetter = 1 << 0x2,
		TitleCaseLetter = 1 << 0x3,
		ModifierLetter = 1 << 0x4,
		OtherLetter = 1 << 0x5,
		Letter = UppercaseLetter | LowercaseLetter | TitleCaseLetter | ModifierLetter | OtherLetter,

		NonSpacingMark = 1 << 0x6,
		SpacingCombiningMark = 1 << 0x7,
		EnclosingMark = 1 << 0x8,
		Mark = NonSpacingMark | SpacingCombiningMark | EnclosingMark,

		DigitNumber = 1 << 0x9,
		LetterNumber = 1 << 0xA,
		OtherNumber = 1 << 0xB,
		Number = DigitNumber | LetterNumber | OtherNumber,

		ConnectorPunctuation = 1 << 0xC,
		DashPunctuation = 1 << 0xD,
		OpenPunctuation = 1 << 0xE,
		ClosePunctuation = 1 << 0xF,
		InitQuotePunctuation = 1 << 0x10,
		FinalQuotePunctuation = 1 << 0x11,
		OtherPunctuation = 1 << 0x12,
		Punctuation = ConnectorPunctuation | DashPunctuation | OpenPunctuation | ClosePunctuation | InitQuotePunctuation | FinalQuotePunctuation | OtherPunctuation,

		MathSymbol = 1 << 0x13,
		CurrencySymbol = 1 << 0x14,
		ModifierSymbol = 1 << 0x15,
		OtherSymbol = 1 << 0x16,
		Symbol = MathSymbol | CurrencySymbol | ModifierLetter | OtherSymbol,

		SpaceSeparator = 1 << 0x17,
		LineSeparator = 1 << 0x18,
		ParagraphSeparator = 1 << 0x19,
		Separator = SpaceSeparator | LineSeparator | ParagraphSeparator,

		ControlOther = 1 << 0x1A,
		FormatOther = 1 << 0x1B,
		SurrogateOther = 1 << 0x1C,
		PrivateUseOther = 1 << 0x1D,
		NotAssignedOther = 1 << 0x1E,
		Other = ControlOther | FormatOther | SurrogateOther | PrivateUseOther | NotAssignedOther,

		All = Letter | Number | Punctuation | Symbol | Separator | Other,
		Unknown = ulong.MaxValue ^ All,
	}
}
