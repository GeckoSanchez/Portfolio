namespace ZData02.Enums
{
	public enum LinkKind : byte
	{
		Device = 1 << 0,
		Object = 1 << 1,
		Platform = 1 << 2,
		All = Device | Object | Platform,
		None = byte.MaxValue ^ All,
	}
}
