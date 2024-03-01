namespace ZData01.Enums
{
	[Flags]
	public enum PageType : uint
	{
		Home = 1 << 0x0,
		Exit = 1 << 0x1,

		Auth = 1 << 0x2,
		Login = 1 << 0x10 | Auth,
		Logout = 1 << 0x11 | Auth,
		Register = 1 << 0x12 | Auth,
		Account = 1 << 0x13 | Auth,

		Index = 1 << 0x3,
		Create = 1 << 0x4,
		Update = 1 << 0x5,
		Details = 1 << 0x6,
		Delete = 1 << 0x7,

		Admin = 1 << 0x8,
		Exception = 1 << 0x9,

		Unknown = 1 << 0xA,

		NotFound = uint.MaxValue ^ Home ^ Exit ^ Auth ^ Login ^ Logout ^ Register ^ Index ^ Create ^ Update ^ Details ^ Delete ^ Admin ^ Exception,
	}
}
