namespace ZData04.Exceptions
{
	using System.Diagnostics;
	using System.Runtime.CompilerServices;
	using Actions;
	using Attributes;
	using Bases;
	using Enums;
	using Newtonsoft.Json;

	public class DeviceException : BaseException
	{
				[JsonConstructor, Unlogged, MainConstructor]
		public DeviceException(Exception? ex, BlockKind? btype, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = 0) : base(ex, btype, ExceptKind.Link, name, path, line) => Log.Event(new StackFrame(true));
		public DeviceException(Exception? ex, StackFrame sf, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = 0) : this(ex, Get.BlockKindOrDefault(sf), name, path, line) => Log.Event(new StackFrame(true));
		public DeviceException(string? message, StackFrame sf, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = 0) : this(message != null ? new(message) : null, Get.BlockKindOrDefault(sf), name, path, line) => Log.Event(new StackFrame(true));
	}
}
