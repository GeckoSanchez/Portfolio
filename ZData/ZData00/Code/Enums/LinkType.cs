namespace ZData00.Enums
{
	public enum LinkType : byte
	{
		Device = 1 << 0,
		Object = 1 << 1,
		All = Device | Object,
		None = byte.MaxValue ^ All,
	}
}
