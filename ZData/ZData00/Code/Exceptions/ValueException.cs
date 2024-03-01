namespace ZData00.Exceptions
{
	using System.Diagnostics;
	using System.Runtime.CompilerServices;
	using Actions;
	using Attributes;
	using Bases;
	using Enums;
	using Newtonsoft.Json;

	public class ValueException : BaseException
	{
		[JsonConstructor, MainConstructor]
		public ValueException(Exception? ex, BlockType? btype, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = 0) : base(ex, btype, ExType.Value, name, path, line) => Log.Event(new StackFrame(true));
		public ValueException(Exception? ex, StackFrame sf, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = 0) : this(ex, Get.BlockTypeOrDefault(sf), name, path, line) => Log.Event(new StackFrame(true));
		public ValueException(string? message, StackFrame sf, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = 0) : this(message != null ? new(message) : null, Get.BlockTypeOrDefault(sf), name, path, line) => Log.Event(new StackFrame(true));
	}
}
