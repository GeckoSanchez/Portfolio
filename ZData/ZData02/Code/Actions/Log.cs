namespace ZData02.Actions
{
	using System.Diagnostics;
	using System.Reflection;
	using System.Runtime.CompilerServices;
	using Attributes;
	using Bases;
	using Enums;
	using Newtonsoft.Json;

	/// <summary>
	/// The <see cref="Log"/> class
	/// </summary>
	/// <param name="message">The message to add to the current entry</param>
	/// <param name="ltype">The log type of the current entry</param>
	/// <param name="btype">The block type of the current entry</param>
	/// <param name="etype">The exception type of the current entry</param>
	/// <param name="logMoment">The moment type of the current entry</param>
	/// <param name="name">The name of the code-block for which the current entry was created</param>
	/// <param name="path">
	/// The file path of where the code-block for which the current entry was created
	/// </param>
	/// <param name="line">
	/// The line number of the code-block for which the current entry was created
	/// </param>
	[JsonObject(MemberSerialization.OptIn)]
	[method: JsonConstructor, MainConstructor]
	public class Log(string? message, LogKind ltype, BlockKind btype, ExceptKind etype, DateTime logMoment, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) : IDisposable
	{
		[JsonProperty("Log message")]
		public string? Message { get; set; } = message;

		[JsonProperty("Block name")]
		private string Name { get; set; } = name;

		[JsonProperty("Block path")]
		private string Path { get; set; } = path;

		[JsonProperty("Block line")]
		private int Line { get; set; } = line;

		[JsonProperty("Block type")]
		private BlockKind BType { get; set; } = btype;

		[JsonProperty("Exception type")]
		private ExceptKind ExcType { get; set; } = etype;

		[JsonProperty("Log type")]
		public LogKind LogType { get; private set; } = ltype;

		[JsonProperty("Log moment")]
		public DateTime LogMoment { get; private set; } = logMoment;

		public Log([CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) : this(null, Def.LogKind, Def.BlockKind, Def.ExceptKind, DateTime.Now, name, path, line) { Write(); }

		public Log(BlockKind btype, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) : this(null, Def.LogKind, btype, Def.ExceptKind, DateTime.Now, name, path, line) { Write(); }

		public Log(ExceptKind etype, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) : this(null, Def.LogKind, Def.BlockKind, etype, DateTime.Now, name, path, line) { Write(); }

		public Log(Exception ex, BlockKind btype, ExceptKind etype, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) : this($"{ex.Message}", LogKind.Exception, btype, etype, DateTime.Now, name, path, line) { Write(); }

		/// <summary>
		/// Function to record a new event log
		/// </summary>
		/// <param name="sf">A stackframe captured at the top of the function</param>
		/// <param name="path">...</param>
		public static void Event(StackFrame sf, [CallerFilePath] string path = "")
		{
			try
			{
				MethodBase? method = sf.GetMethod();
				var unlogged = method?.GetCustomAttribute<UnloggedAttribute>();
				var primconst = method?.GetCustomAttribute<MainConstructorAttribute>();

				if (method?.Name.ToUpper() == "EQUALS")
					return;

				if (unlogged == null)
				{
					if (primconst == null)
						new Log(null, LogKind.Event, Get.BlockType(sf), Def.ExceptKind, DateTime.Now, sf.GetMethod()!.Name, sf.GetFileName() ?? path, sf.GetFileLineNumber()).Write();
					else
						new Log(null, LogKind.Event, BlockKind.Primary_Constructor, Def.ExceptKind, DateTime.Now, sf.GetMethod()!.Name, sf.GetFileName() ?? path, sf.GetFileLineNumber()).Write();
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($">>>> {ex.Message} <<<<");
			}
		}

		/// <inheritdoc cref="Event(StackFrame, string)"/>
		/// <param name="message">A custom message for the log</param>
		/// <param name="bkind">The kind of code-block in which the event occurred</param>
		/// <param name="exkind">The kind of exception in which the event occurred</param>
		/// <param name="name">...</param>
		/// <param name="path">...</param>
		/// <param name="line">...</param>
		public static void Event(string message, BlockKind bkind, ExceptKind exkind = ExceptKind.Base, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) => new Log(message, LogKind.Event, bkind, exkind, DateTime.Now, name, path, line).Write();

		/// <summary>
		/// Function to record a new error log
		/// </summary>
		/// <param name="bkind">The kind of code-block in which the event occurred</param>
		/// <param name="name">...</param>
		/// <param name="path">...</param>
		/// <param name="line">...</param>
		public static void Error(BlockKind btype, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) => new Log("", LogKind.Error, btype, Def.ExceptKind, DateTime.Now, name, path, line).Write();

		/// <inheritdoc cref="Error(BlockKind, string, string, int)"/>
		/// <param name="message">A custom message for the log</param>
		public static void Error(string message, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) => new Log(message, LogKind.Error, Def.BlockKind, Def.ExceptKind, DateTime.Now, name, path, line).Write();

		/// <inheritdoc cref="Error(BlockKind, string, string, int)"/>
		/// <param name="message">A custom message for the log</param>
		public static void Error(string message, BlockKind btype, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) => new Log(message, LogKind.Error, btype, Def.ExceptKind, DateTime.Now, name, path, line).Write();

		/// <inheritdoc cref="Error(BlockKind, string, string, int)"/>
		/// <param name="ex">The <see cref="System.Exception"/> used to provide the message for the log</param>
		public static void Error(Exception ex, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) => new Log(ex.Message, LogKind.Error, Def.BlockKind, Def.ExceptKind, DateTime.Now, name, path, line).Write();

		/// <inheritdoc cref="Error(System.Exception, string, string, int)"/>
		public static void Error(BaseException ex) => new Log(ex.Message, LogKind.Error, ex.BlockType, ex.ExceptionType, DateTime.Now, ex.Name, ex.Path, ex.Line).Write();

		/// <inheritdoc cref="Error(System.Exception, string, string, int)"/>
		/// <param name="sf">A stackframe captured at the top of the function</param>
		public static void Error(Exception ex, StackFrame sf, [CallerFilePath] string path = "")
		{
			try
			{
				MethodBase? method = sf.GetMethod();
				var unlogged = method?.GetCustomAttribute<UnloggedAttribute>();
				var primconst = method?.GetCustomAttribute<MainConstructorAttribute>();

				if (method?.Name.ToUpper() == "EQUALS")
					return;

				if (unlogged == null)
				{
					if (primconst == null)
						new Log(ex.Message, LogKind.Error, Get.BlockType(sf), Def.ExceptKind, DateTime.Now, sf.GetMethod()!.Name, sf.GetFileName() ?? path, sf.GetFileLineNumber()).Write();
					else
						new Log(ex.Message, LogKind.Error, BlockKind.Primary_Constructor, Def.ExceptKind, DateTime.Now, sf.GetMethod()!.Name, sf.GetFileName() ?? path, sf.GetFileLineNumber()).Write();
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine($">>>> {e.Message} <<<<");
			}
		}

		/// <inheritdoc cref="Error(Exception, StackFrame, string)"/>
		/// <param name="method">The information that the code can provide about a given method/function</param>
		public static void Error(Exception ex, StackFrame sf, MethodBase method)
		{
			try
			{
				UnloggedAttribute? attrib = method.GetCustomAttribute<UnloggedAttribute>();

				if (attrib == null)
					new Log(ex.Message, LogKind.Error, Def.BlockKind, Def.ExceptKind, DateTime.Now, (sf.GetMethod() ?? method)?.Name ?? "", sf.GetFileName()!, sf.GetFileLineNumber()).Write();
			}
			catch (Exception e)
			{
				Debug.WriteLine($">>>> {e.Message} <<<<");
			}
		}

		public static void Error(Exception ex, BlockKind bkind, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) => new Log(ex.Message, LogKind.Error, bkind, Def.ExceptKind, DateTime.Now, name, path, line).Write();

		public static void Exception(BaseException ex) => new Log(ex.Message, LogKind.Exception, ex.BlockType, ex.ExceptionType, DateTime.Now, ex.Name, ex.Path, ex.Line).Write();
		public static void Exception(Exception ex, BlockKind btype, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) => new Log(ex.Message, LogKind.Exception, btype, Def.ExceptKind, DateTime.Now, name, path, line).Write();

		public static void Info(string message, BlockKind btype, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) => new Log(message, LogKind.Info, btype, Def.ExceptKind, DateTime.Now, name, path, line).Write();
		public static void Info(Exception ex, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) => new Log(ex.Message, LogKind.Info, Def.BlockKind, Def.ExceptKind, DateTime.Now, name, path, line).Write();
		public static void Info(Exception ex, BlockKind btype, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) => new Log(ex.Message, LogKind.Info, btype, Def.ExceptKind, DateTime.Now, name, path, line).Write();

		public static void Warning(string message, BlockKind btype, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) => new Log(message, LogKind.Warning, btype, Def.ExceptKind, DateTime.Now, name, path, line).Write();

		private void Write()
		{
			Thread.BeginCriticalRegion();
			Thread.BeginThreadAffinity();

			lock (this)
			{
				FileStream? fs = null;
				FileStream? backupFS = null;
				StreamWriter? sw = null;
				StreamReader? sr = null;
				DirectoryInfo? di = null;

				try
				{
					if (Line <= 1 || BType == BlockKind.Equals_Operator || BType == BlockKind.Not_Equals_Operator || Name.Equals("EQUALS", StringComparison.InvariantCultureIgnoreCase))
						return;

					string Out = $"[{LogMoment:HH:mm:ss.ffffff}] ";

					di = Directory.CreateDirectory(Def.LogsDir);
					fs = new($"{di.FullName}/{LogType}_{DateTime.Now:yyyy-MM-dd}.log", FileMode.Append, FileAccess.Write, FileShare.ReadWrite);

					if (BType.HasFlag(BlockKind.Constructor) || BType == BlockKind.Destructor || (BType.HasFlag(BlockKind.Operator) && BType != BlockKind.Operator && BType != BlockKind.Get_Operator && BType != BlockKind.Set_Operator && BType != BlockKind.Init_Operator) || BType == BlockKind.Page)
					{
						string[] split;

						try
						{
							Path ??= "";

							try
							{
								split = Path.Split('\\');
							}
							catch (Exception)
							{
								split = Path.Split('/');
							}

							Name = split.LastOrDefault(Def.Name);
						}
						catch (Exception)
						{
							Path = Def.MainDir;
							Name = "???";
						}
						finally
						{
							split = [];
						}
					}
					else if (BType is BlockKind.Get_Operator or BlockKind.Set_Operator or BlockKind.Init_Operator)
						Name = Name.Split('_').Last();

					string bStr = BType.ToString().ToLower().Replace('_', ' ');
					string msg = (Message ?? " ").Trim();

					if (BType == BlockKind.None)
						Message ??= "";
					else if (msg == string.Empty)
						msg = $"'{Name.Split('.').First()}' {bStr}";

					DirectoryInfo dirInfo = new(Path);

					Out += LogType switch
					{
						LogKind.Event => $"{msg} (Location: '{dirInfo.Parent?.Name}/{Path.Split('\\').Last()}', Line: #{Line})\n".Replace("Storage/Storage", "Storage"),
						LogKind.Exception => $"<{ExcType}> {msg} (Location: '{dirInfo.Parent?.Name}/{Path.Split('\\').Last()}', Line: #{Line})\n".Replace("Storage/Storage", "Storage"),
						_ => $"{msg} (Block: '{Name.Split('.').First()}' {bStr}, Location: '{dirInfo.Parent?.Name}/{Path.Split('\\').Last()}', Line: #{Line})\n".Replace("Storage/Storage", "Storage"),
					};

					sw = new(fs, Def.Encoding) { AutoFlush = true };
					sw.Write(Out);

					long? size = null;

					try
					{
						size = fs.Length;
					}
					finally
					{
						size ??= 0;
					}

					string fNm = fs.Name;

					fs.Flush();
					fs.Dispose();

					fs = new(fNm, FileMode.Open, FileAccess.Read);

					if (size.Value > 4294967296)
					{
						sr = new(fs);

						string text = sr.ReadToEnd();

						sr.DiscardBufferedData();
						sr.Dispose();

						di = Directory.CreateDirectory(Def.BackupLogsDir);
						int logNb = 1;

						FileInfo backupFI = new($"{di.FullName}/{LogType}_{DateTime.Now:yyyy-MM-dd}_{logNb}.log");

						while (backupFI.Exists)
							backupFI = new($"{di.FullName}/{LogType}_{DateTime.Now:yyyy-MM-dd}_{logNb++}.log");

						backupFS = new(backupFI.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

						using (sw = new(backupFS, Def.Encoding))
						{
							sw.WriteLine(text);
							sw.Flush();
							sw.BaseStream.Flush();
							sw.Dispose();
						}

						backupFS.Dispose();

						di = new DirectoryInfo(Def.LogsDir);
						sw = File.CreateText($"{di.FullName}/{LogType}_{DateTime.Now:yyyy-MM-dd}.log");
						sw.Dispose();
						fs.Dispose();
					}
				}
				catch (Exception ex)
				{
					Debug.Fail(ex.Message, (ex.InnerException ?? ex).Message);
				}
				finally
				{
					//Current.Set(this);
					if (sw != null && sw.BaseStream.CanWrite)
						sw.Dispose();
					if (sr != null && sr.BaseStream.CanRead)
						sr.Dispose();
					fs?.Dispose();
					backupFS?.Dispose();
					di = null;
				}

				Dispose();
			}

			Thread.EndThreadAffinity();
			Thread.EndCriticalRegion();
		}

		public async void WriteAsync()
		{
			FileStream? fs = null;
			FileStream? backFS = null;
			StreamWriter? sw = null;
			StreamReader? sr = null;

			try
			{
				string Out = $"[{TimeOnly.FromDateTime(DateTime.Now):HH:mm:ss.ffffff}] ";
				var di = Directory.CreateDirectory(Def.LogsDir);
				fs = new($"{di.FullName}/{LogType}.log", FileMode.Append, FileAccess.Write);

				if (BType == BlockKind.Constructor || BType == BlockKind.Destructor || BType == BlockKind.Page || BType.HasFlag(BlockKind.Operator))
					Name = Path.Split('\\').Last();

				string bStr = BType.ToString().ToLower().Replace('_', ' ');

				Out += $"{Message} (Block: '{Name.Split('.').First()}' {bStr}, Location: '{Path.Split('\\').Last()}', Line: #{Line})\n";

				sw = new(fs);
				await sw.WriteAsync(Out);

				long currentLogSize = fs.Length;

				sw.Dispose();

				if (currentLogSize > 50000000)
				{
					sr = new(fs.Name, new FileStreamOptions() { Access = FileAccess.Read });

					string text = sr.ReadToEndAsync().Result;

					sr.DiscardBufferedData();
					sr.Dispose();

					di = Directory.CreateDirectory(Def.BackupLogsDir);
					backFS = new($"{di.FullName}/{LogType}_{DateTime.Now:yyyy-MM-dd}.log", FileMode.OpenOrCreate, FileAccess.ReadWrite);

					using (sw = new(backFS))
					{
						sw.WriteLine(text);
					}

					await backFS.DisposeAsync();

					di = Directory.CreateDirectory(Def.LogsDir);
					fs = new($"{di.FullName}/{LogType}.log", FileMode.Truncate, FileAccess.Write);
				}

				await fs.DisposeAsync();
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"********** {ex.Message} **********");
			}
			finally
			{
				//await Current.SetAsync(this);

				//// vv NOT YET TESTED vv ////
				var vtSw = sw?.DisposeAsync();
				vtSw?.GetAwaiter().GetResult();
				sr?.Dispose();
				var vtFS = fs?.DisposeAsync();
				vtFS?.GetAwaiter().GetResult();
				backFS?.Dispose();
				//// ^^ NOT YET TESTED ^^ ////
			}
		}

		~Log() => Dispose();

		public void Dispose()
		{
			Message = default!;
			Name = default!;
			Path = default!;
			Line = default!;
			BType = default!;
			ExcType = default!;
			LogType = default!;
			LogMoment = default!;
			GC.SuppressFinalize(this);
			GC.Collect();
		}

		public override string ToString()
		{
			string bStr = BType.ToString().ToLower().Replace('_', ' ');
			string location = (Path ?? @"\").Split('\\').Last();
			string msg = (Message ?? Def.ExceptionMessage).Trim() == "" ? $"An {LogType.ToString().ToLower()} occurred" : (Message ?? Def.ExceptionMessage);
			return $"{msg} (Block: '{(Name ?? Def.Name).Split('.').First()}' {bStr.Split('.').First()}, Location: '{location}', Line: #{Line})\n";
		}
	}
}
