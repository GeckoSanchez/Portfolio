namespace ZData00.Bases
{
	using System.Diagnostics;
	using System.Runtime.CompilerServices;
	using Actions;
	using Attributes;
	using Enums;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class BaseException : Exception
	{
		[JsonProperty]
		public override string Message => base.Message;
		[JsonProperty("Block type")]
		public BlockType BType { get; set; }
		[JsonProperty("Exception type")]
		public ExType ExType { get; set; }
		[JsonProperty]
		public string Name { get; set; }
		[JsonProperty]
		public string Path { get; set; }
		[JsonProperty]
		public int Line { get; set; }

		/// <summary>
		/// Primary constructor for the <see cref="BaseException"/> class
		/// </summary>
		/// <param name="ex">The given <see cref="Exception"/></param>
		/// <param name="btype">The given block type</param>
		/// <param name="extype">The given exception type</param>
		/// <param name="name">The given name of the code-block</param>
		/// <param name="path">The file path of the code-block</param>
		/// <param name="line">The line number of the code-block</param>
		[JsonConstructor, PrimaryConstructor]
		public BaseException(Exception? ex, BlockType? btype, ExType extype = ExType.Base, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = 0) : base((ex ?? new()).Message, ex)
		{
			Log.Event(new StackFrame(true));
			BType = btype ?? Def.BType;
			ExType = extype;
			Name = name;
			Path = path;
			Line = line;
		}

		/// <summary>
		/// Constructor for the <see cref="BaseException"/> class
		/// </summary>
		/// <param name="sf">The given <see cref="StackFrame"/> object</param>
		/// <inheritdoc cref="BaseException(Exception?, BlockType?, ExType, string, string, int)"/>
		public BaseException(Exception? ex, StackFrame sf, ExType extype = ExType.Base, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = 0) : this(ex, Get.BlockTypeOrDefault(sf, Def.BType), extype, sf.GetMethod()!.Name ?? name, sf.GetFileName() ?? path, sf.GetFileLineNumber() != 0 ? sf.GetFileLineNumber() : line) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="BaseException(Exception?, StackFrame, ExType, string, string, int)"/>
		/// <param name="message">The given message</param>
		public BaseException(string? message, StackFrame sf, ExType extype = ExType.Base, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = 0) : this(new Exception(message), Get.BlockTypeOrDefault(sf, Def.BType), extype, sf.GetMethod()!.Name ?? name, sf.GetFileName() ?? path, sf.GetFileLineNumber() != 0 ? sf.GetFileLineNumber() : line) => Log.Event(new StackFrame(true));

		public override Exception GetBaseException()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			Exception? Out = null;

			try
			{
				Out = base.GetBaseException();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
			finally
			{
				Out ??= new();
			}

			return Out;
		}

		public override string ToString()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);
			return Message;
		}
	}
}
