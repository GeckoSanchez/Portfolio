namespace ZData01.Exceptions
{
	using System.Diagnostics;
	using System.Runtime.CompilerServices;
	using Actions;
	using Attributes;
	using Bases;
	using Enums;
	using Newtonsoft.Json;

	public class ClassException : BaseException
	{
		[JsonConstructor, Unlogged, MainConstructor]
		public ClassException(Exception? ex, BlockType? btype, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = 0) : base(ex, btype, ExType.Class, name, path, line) => Log.Event(new StackFrame(true));
		public ClassException(Exception? ex, StackFrame sf, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = 0) : this(ex, Get.BlockTypeOrDefault(sf), name, path, line) => Log.Event(new StackFrame(true));
		public ClassException(string? message, StackFrame sf, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = 0) : this(message != null ? new(message) : null, Get.BlockTypeOrDefault(sf), name, path, line) => Log.Event(new StackFrame(true));
	}
}
