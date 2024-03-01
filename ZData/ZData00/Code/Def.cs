namespace ZData00
{
	using System.Text;
	using Enums;
	using ZData00.IDs;

	public static class Def
	{
		public const string Name = "[DefaultName]";
		public static readonly string Value = string.Empty;
		public const string AppName = "APPLICATION_NAME";

		public static string MainDir => $"Storage/{AppName}";
		public static string ObjectsDir => $"{MainDir}/Objects";
		public static string DevicesDir => $"{MainDir}/Devices";
		public static string LogsDir => $"{MainDir}/Logs";
		public static string HistoryDir => $"{MainDir}/History";

		public static string BackupsDir => $"{MainDir}/Backups";
		public static string BackupLogsDir => $"{BackupsDir}/Logs";
		public static string BackupObjectsDir => $"{BackupsDir}/Objects";

		public static string CurrentDir => $"{MainDir}/Current";
		public static string CurrentLogsDir => $"{CurrentDir}/Logs";
		public static string CurrentObjectsDir => $"{CurrentDir}/Objects";

		public static BlockType BType => BlockType.Constructor;
		public static DeviceType DevType => DeviceType.None;
		public static LogType LType => LogType.Event;
		public static ExType ExType => ExType.Base;
		public static ObjectType OType => ObjectType.Object;

		public static readonly Encoding Encoding = Encoding.Default;

		internal static readonly string ExceptionMessage = new Exception().Message;
		internal static readonly byte[] MsgPack = [];
		
		internal const string JSON = "{}";

		public static Dictionary<Type, byte> TopIDs => new()
		{
			{ typeof(BlockType), 1 },
			{ typeof(DeviceType), 2 },
			{ typeof(ExType), 3 },
			{ typeof(LinkType), 4 },
			{ typeof(LogType), 5 },
			{ typeof(Months), 6 },
			{ typeof(ObjectType), 7 },
			{ typeof(PageType), 8 },
			{ typeof(PlatformType), 9 },
			{ typeof(RegexCategory), 10 },
			{ typeof(UserType), 11 },
		};

		public static readonly byte[] MACAdress = [0, 0, 0, 0, 0, 0];
		public static readonly byte[] IPArray = System.Net.IPAddress.IPv6Any.GetAddressBytes();
	}

	public static class Def<T> where T : notnull
	{
		public static readonly T Value = default!;
		public static readonly T? NValue = default;
	}
}
