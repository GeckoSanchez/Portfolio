namespace ZData02.Enums
{
	/// <summary>
	/// Kinds of save files
	/// </summary>
	[Flags]
	public enum SaveFileKind : byte
	{
		JSON = 1 << 0,
		MessagePack = 1 << 1,
		All = JSON | MessagePack,
		None = byte.MaxValue ^ All,
	}
}
