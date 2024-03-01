namespace ZData00.Exceptions
{
	using System.Diagnostics;
	using System.Runtime.CompilerServices;
	using Actions;
	using Attributes;
	using Bases;
	using Enums;
	using Newtonsoft.Json;

	public class IDException : BaseException
	{
				[JsonConstructor, Unlogged, MainConstructor]
		public IDException(Exception? ex, BlockType? btype, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = 0) : base(ex, btype, ExType.ID, name, path, line) => Log.Event(new StackFrame(true));
		public IDException(Exception? ex, StackFrame sf, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = 0) : this(ex, Get.BlockTypeOrDefault(sf), name, path, line) => Log.Event(new StackFrame(true));
		public IDException(string? message, StackFrame sf, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = 0) : this(message != null ? new(message) : null, Get.BlockTypeOrDefault(sf), name, path, line) => Log.Event(new StackFrame(true));
	}
}
