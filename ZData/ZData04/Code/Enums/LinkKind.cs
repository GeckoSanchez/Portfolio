namespace ZData04.Enums
{
	public enum LinkKind : byte
	{
		Link = 1 << 0,
		Object = 1 << 1,
		Platform = 1 << 2,
		All = Link | Object | Platform,
		None = byte.MaxValue ^ All,
	}
}
