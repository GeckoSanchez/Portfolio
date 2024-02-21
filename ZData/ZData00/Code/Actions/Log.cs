namespace ZData00.Actions
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
	[method: JsonConstructor]
	[method: PrimaryConstructor]
	public class Log(string? message, LogType ltype, BlockType btype, ExType etype, DateTime logMoment, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) : IDisposable
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
		private BlockType BType { get; set; } = btype;

		[JsonProperty("Exception type")]
		private ExType ExcType { get; set; } = etype;

		[JsonProperty("Log type")]
		public LogType LogType { get; private set; } = ltype;

		[JsonProperty("Log date & time")]
		public DateTime LogMoment { get; private set; } = logMoment;

		public Log([CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) : this(null, Def.LType, Def.BType, Def.ExType, DateTime.Now, name, path, line) { Write(); }

		public Log(BlockType btype, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) : this(null, Def.LType, btype, Def.ExType, DateTime.Now, name, path, line) { Write(); }

		public Log(ExType etype, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) : this(null, Def.LType, Def.BType, etype, DateTime.Now, name, path, line) { Write(); }

		public Log(Exception ex, BlockType btype, ExType etype, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) : this($"{ex.Message}", LogType.Exception, btype, etype, DateTime.Now, name, path, line) { Write(); }

		public static void Event(StackFrame sf, [CallerFilePath] string path = "")
		{
			try
			{
				MethodBase? method = sf.GetMethod();
				var unlogged = method?.GetCustomAttribute<UnloggedAttribute>();
				var primconst = method?.GetCustomAttribute<PrimaryConstructorAttribute>();

				if (method?.Name.ToUpper() == "EQUALS")
					return;

				if (unlogged == null)
				{
					if (primconst == null)
						new Log(null, LogType.Event, Get.BlockType(sf), Def.ExType, DateTime.Now, sf.GetMethod()!.Name, sf.GetFileName() ?? path, sf.GetFileLineNumber()).Write();
					else
						new Log(null, LogType.Event, BlockType.Primary_Constructor, Def.ExType, DateTime.Now, sf.GetMethod()!.Name, sf.GetFileName() ?? path, sf.GetFileLineNumber()).Write();
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($">>>> {ex.Message} <<<<");
			}
		}

		public static void Event(MethodBase? method, [CallerFilePath] string path = "", [CallerLineNumber] int line = 0)
		{
			try
			{
				var attrib = method?.GetCustomAttribute<UnloggedAttribute>();

				if (method?.Name.ToUpper() == "EQUALS")
					return;

				if (attrib == null)
					new Log(null, LogType.Event, Get.BlockType(method), Def.ExType, DateTime.Now, method!.Name, path, line).Write();
			}
			catch (Exception ex)
			{
				Debug.WriteLine($">>>> {ex.Message} <<<<");
			}
		}

		public static void Event(StackFrame sf, BlockType btype, [CallerFilePath] string path = "")
		{
			try
			{
				MethodBase? method = sf.GetMethod();
				UnloggedAttribute? attrib = method?.GetCustomAttribute<UnloggedAttribute>();

				if (attrib == null)
					new Log(null, LogType.Event, btype, Def.ExType, DateTime.Now, sf.GetMethod()!.Name, sf.GetFileName() ?? path, sf.GetFileLineNumber()).Write();
			}
			catch (Exception ex)
			{
				Debug.WriteLine($">>>> {ex.Message} <<<<");
			}
		}

		public static void Event(BlockType btype, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) => new Log(null, LogType.Event, btype, Def.ExType, DateTime.Now, name, path, line).Write();
		public static void Event(string message, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) => new Log(message, LogType.Event, Def.BType, Def.ExType, DateTime.Now, name, path, line).Write();
		public static void Event(string message, BlockType btype, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) => new Log(message, LogType.Event, btype, Def.ExType, DateTime.Now, name, path, line).Write();
		public static void Event(string message, ExType etype, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) => new Log(message, LogType.Event, Def.BType, etype, DateTime.Now, name, path, line).Write();

		public static void Error([CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) => new Log("", LogType.Error, Def.BType, Def.ExType, DateTime.Now, name, path, line).Write();
		public static void Error(BlockType btype, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) => new Log("", LogType.Error, btype, Def.ExType, DateTime.Now, name, path, line).Write();
		public static void Error(string message, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) => new Log(message, LogType.Error, Def.BType, Def.ExType, DateTime.Now, name, path, line).Write();
		public static void Error(string message, BlockType btype, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) => new Log(message, LogType.Error, btype, Def.ExType, DateTime.Now, name, path, line).Write();
		public static void Error(Exception ex, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) => new Log(ex.Message, LogType.Error, Def.BType, Def.ExType, DateTime.Now, name, path, line).Write();
		public static void Error(BaseException ex) => new Log(ex.Message, LogType.Error, ex.BType, ex.ExType, DateTime.Now, ex.Name, ex.Path, ex.Line).Write();
		public static void Error(Exception ex, StackFrame sf)
		{
			try
			{
				MethodBase? method = sf.GetMethod();
				UnloggedAttribute? attrib = method?.GetCustomAttribute<UnloggedAttribute>();

				if (attrib == null)
					new Log(ex.Message, LogType.Error, Def.BType, Def.ExType, DateTime.Now, sf.GetMethod()!.Name, sf.GetFileName()!, sf.GetFileLineNumber()).Write();
			}
			catch (Exception e)
			{
				Debug.WriteLine($">>>> {e.Message} <<<<");
			}
		}

		public static void Error(Exception ex, StackFrame sf, MethodBase method)
		{
			try
			{
				UnloggedAttribute? attrib = method.GetCustomAttribute<UnloggedAttribute>();

				if (attrib == null)
					new Log(ex.Message, LogType.Error, Def.BType, Def.ExType, DateTime.Now, (sf.GetMethod() ?? method)?.Name ?? "", sf.GetFileName()!, sf.GetFileLineNumber()).Write();
			}
			catch (Exception e)
			{
				Debug.WriteLine($">>>> {e.Message} <<<<");
			}
		}

		public static void Error(Exception ex, BlockType btype, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) => new Log(ex.Message, LogType.Error, btype, Def.ExType, DateTime.Now, name, path, line).Write();

		public static void Exception(BaseException ex) => new Log(ex.Message, LogType.Exception, ex.BType, ex.ExType, DateTime.Now, ex.Name, ex.Path, ex.Line).Write();
		public static void Exception(Exception ex, BlockType btype, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) => new Log(ex.Message, LogType.Exception, btype, Def.ExType, DateTime.Now, name, path, line).Write();

		public static void Info(string message, BlockType btype, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) => new Log(message, LogType.Info, btype, Def.ExType, DateTime.Now, name, path, line).Write();
		public static void Info(Exception ex, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) => new Log(ex.Message, LogType.Info, Def.BType, Def.ExType, DateTime.Now, name, path, line).Write();
		public static void Info(Exception ex, BlockType btype, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) => new Log(ex.Message, LogType.Info, btype, Def.ExType, DateTime.Now, name, path, line).Write();

		public static void Warning(string message, BlockType btype, [CallerMemberName] string name = "", [CallerFilePath] string path = "", [CallerLineNumber] int line = default) => new Log(message, LogType.Warning, btype, Def.ExType, DateTime.Now, name, path, line).Write();

		private void Write()
		{
			Thread.BeginCriticalRegion();
			Thread.BeginThreadAffinity();

			lock (this)
			{
				FileStream? fs = null;
				FileStream? backFS = null;
				StreamWriter? sw = null;
				StreamReader? sr = null;
				DirectoryInfo? di = null;

				try
				{
					if (Line <= 1 || BType == BlockType.Equals_Operator || Name.Equals("EQUALS", StringComparison.InvariantCultureIgnoreCase))
						return;

					string Out = $"[{LogMoment:HH:mm:ss.ffffff}] ";

					di = Directory.CreateDirectory(Def.LogsDir);
					fs = new($"{di.FullName}/{LogType}_{DateTime.Now:yyyy-MM-dd}.log", FileMode.Append, FileAccess.Write, FileShare.ReadWrite);

					if (BType.HasFlag(BlockType.Constructor) || BType == BlockType.Destructor || (BType.HasFlag(BlockType.Operator) && BType != BlockType.Operator && BType != BlockType.Get_Operator && BType != BlockType.Set_Operator && BType != BlockType.Init_Operator) || BType == BlockType.Page)
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
					else if (BType is BlockType.Get_Operator or BlockType.Set_Operator or BlockType.Init_Operator)
						Name = Name.Split('_').Last();

					string bStr = BType.ToString().ToLower().Replace('_', ' ');
					string msg = (Message ?? " ").Trim();

					if (BType == BlockType.None)
						Message ??= "";
					else if (msg == string.Empty)
						msg = $"'{Name.Split('.').First()}' {bStr}";

					DirectoryInfo dirInfo = new(Path);

					Out += LogType switch
					{
						LogType.Event => $"{msg} (Location: '{dirInfo.Parent?.Name}/{Path.Split('\\').Last()}', Line: #{Line})\n".Replace("Storage/Storage", "Storage"),
						LogType.Exception => $"<{ExcType}> {msg} (Location: '{dirInfo.Parent?.Name}/{Path.Split('\\').Last()}', Line: #{Line})\n".Replace("Storage/Storage", "Storage"),
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

						backFS = new(backupFI.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

						using (sw = new(backFS, Def.Encoding))
						{
							sw.WriteLine(text);
							sw.Flush();
							sw.BaseStream.Flush();
							sw.Dispose();
						}

						backFS.Dispose();

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
					backFS?.Dispose();
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

				if (BType == BlockType.Constructor || BType == BlockType.Destructor || BType == BlockType.Page || BType.HasFlag(BlockType.Operator))
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
