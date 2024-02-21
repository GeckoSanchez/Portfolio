namespace ZData00.Actions
{
	using System.Diagnostics;
	using System.Reflection;
	using System.Text.RegularExpressions;
	using Attributes;
	using Bases;
	using Enums;
	using Exceptions;
	using Newtonsoft.Json;

	public static class Get
	{
		/// <summary>
		/// Function to extract the block type from the given <see cref="MethodBase"/> value
		/// </summary>
		/// <param name="sf">The given <see cref="MethodBase"/> value</param>
		/// <returns>The found block type, or <see cref="BlockType.Function"/> otherwise</returns>
		/// <exception cref="ActionException"/>
		[Unlogged]
		public static BlockType BlockType(MethodBase? method)
		{
			BlockType? Out;

			try
			{
				if (method == null)
					throw new Exception($"The given {nameof(MethodBase)} value was found to be null, and therefore invalid for this function/method");
				else
				{
					string name = method.Name;

					if (name.StartsWith("op_", StringComparison.CurrentCultureIgnoreCase))
					{
						if (name.EndsWith("Inequality"))
							Out = Enums.BlockType.Not_Equals_Operator;
						else if (name.EndsWith("Equality"))
							Out = Enums.BlockType.Equals_Operator;
						else if (name.EndsWith("GreaterThan"))
							Out = Enums.BlockType.Greater_Operator;
						else if (name.EndsWith("GreaterThanOrEqual"))
							Out = Enums.BlockType.Greater_Or_Equals_Operator;
						else if (name.EndsWith("LessThan"))
							Out = Enums.BlockType.Lesser_Operator;
						else if (name.EndsWith("LessThanOrEqual"))
							Out = Enums.BlockType.Lesser_Or_Equals_Operator;
						else if (name.EndsWith("UnaryPlus"))
							Out = Enums.BlockType.Positive_Operator;
						else if (name.EndsWith("UnaryNegation"))
							Out = Enums.BlockType.Negative_Operator;
						else if (name.EndsWith("Addition"))
							Out = Enums.BlockType.Addition_Operator;
						else if (name.EndsWith("Subtraction"))
							Out = Enums.BlockType.Subtraction_Operator;
						else if (name.EndsWith("Multiply"))
							Out = Enums.BlockType.Multiplication_Operator;
						else if (name.EndsWith("Division"))
							Out = Enums.BlockType.Division_Operator;
						else if (name.EndsWith("Modulus"))
							Out = Enums.BlockType.Modulo_Operator;
						else if (name.EndsWith("Implicit"))
							Out = Enums.BlockType.Implicit_Operator;
						else if (name.EndsWith("Explicit"))
							Out = Enums.BlockType.Explicit_Operator;
						else
							Out = Enums.BlockType.Operator;
					}
					else if (name.StartsWith("get_"))
						Out = Enums.BlockType.Get_Operator;
					else if (name.StartsWith("set_"))
						Out = Enums.BlockType.Set_Operator;
					else if (name.StartsWith("init_"))
						Out = Enums.BlockType.Init_Operator;
					else if (method.IsConstructor && method.GetParameters().Length == 0)
						Out = Enums.BlockType.Empty_Constructor;
					else if (method.IsConstructor && (method.GetCustomAttribute<JsonConstructorAttribute>(false) != null || method.GetCustomAttribute<PrimaryConstructorAttribute>(false) != null))
						Out = Enums.BlockType.Primary_Constructor;
					else if (method.Attributes.HasFlag(MethodAttributes.CheckAccessOnOverride))
						Out = Enums.BlockType.Constructor;
					else if (method.IsConstructor)
						Out = Enums.BlockType.Constructor;
					else if (method.Name is (nameof(Equals)) or (nameof(ToString)) or "SetParametersAsync" or "OnInitialized" or "OnInitializedAsync" or "OnParametersSet" or "OnParametersSetAsync" or "OnAfterRender" or "OnAfterRenderAsync" or "IsDefaultAttribute" or "Match")
						Out = Enums.BlockType.Override_Function;
					else
						Out = Enums.BlockType.Function;
				}
			}
			catch (Exception ex)
			{
				throw new ActionException(ex, new StackFrame(true));
			}

			return Out ?? Enums.BlockType.Function;
		}

		/// <inheritdoc cref="BlockType(MethodBase?)"/>
		[Unlogged]
		public static BlockType BlockType(StackFrame sf)
		{
			BlockType? Out;

			try
			{
				Out = BlockType(sf.GetMethod());
			}
			catch (Exception ex)
			{
				throw new ActionException(ex, new StackFrame(true));
			}

			return Out ?? Def.BType;
		}

		/// <inheritdoc cref="BlockType(MethodBase?)"/>
		[Unlogged]
		public static BlockType BlockTypeOrDefault(StackFrame sf)
		{
			BlockType? Out;

			try
			{
				Out = BlockType(sf.GetMethod());
			}
			catch (BaseException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new ActionException(ex, new StackFrame(true));
			}

			return Out ?? Def.BType;
		}

		/// <inheritdoc cref="BlockType(MethodBase?)"/>
		[Unlogged]
		public static BlockType BlockTypeOrDefault(StackFrame sf, BlockType defaultValue)
		{
			BlockType? Out;

			try
			{
				Out = BlockType(sf.GetMethod());
			}
			catch (BaseException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new ActionException(ex, new StackFrame(true));
			}

			return Out ?? defaultValue;
		}

		//public static Enum Type(BaseID id)
		//{
		//	Enum? Out = null;

		//	try
		//	{

		//	}
		//	catch (Exception ex)
		//	{
		//		throw new ActionException(ex, new StackFrame(true));
		//	}
		//	finally
		//	{
		//		Out ??= default!;
		//	}

		//	return Out;
		//}

		/// <summary>
		/// Function to get the <see cref="Enums.ObjectType"/> from the given <paramref name="id"/>
		/// </summary>
		/// <param name="id">The given id</param>
		/// <returns>An <see cref="Enums.ObjectType"/> (Default: <see cref="Def.OType"/>)</returns>
		/// <exception cref="ActionException"/>
		//[Unlogged]
		//public static ObjectType ObjectType(ObjectID id)
		//{
		//	ObjectType? Out;

		//	try
		//	{
		//		var topmiddleID = id.Value;
		//		string binStr = $"{topmiddleID:B64}";
		//		string partStr = binStr[5..32];

		//		var middleID = uint.Parse(partStr, NumberStyles.BinaryNumber) % (1 << 28);

		//		Out = (ObjectType)middleID;
		//	}
		//	catch (Exception ex)
		//	{
		//		throw new ActionException(ex, new StackFrame(true));
		//	}

		//	return Out ?? Def.OType;
		//}

		/// <inheritdoc cref="ObjectType(ObjectID)"/>
		/// <param name="def">The alternative value</param>
		/// <returns>An <see cref="Enums.ObjectType"/> (Default: <paramref name="def"/>)</returns>
		/// <exception cref="ActionException"/>
		//[Unlogged]
		//public static ObjectType ObjectTypeOrDefault(ObjectID id, ObjectType? def)
		//{
		//	ObjectType? Out;

		//	try
		//	{
		//		var topmiddleID = id.Value;
		//		string binStr = $"{topmiddleID:B64}";
		//		string partStr = binStr[5..32];

		//		var middleID = uint.Parse(partStr, NumberStyles.BinaryNumber) % (1 << 28);

		//		Out = (ObjectType)middleID;
		//	}
		//	catch (Exception ex)
		//	{
		//		throw new ActionException(ex, new StackFrame(true));
		//	}

		//	return Out ?? def ?? Def.OType;
		//}

		//[Unlogged(IsLogged = false)]
		//public static string DirPath(ObjectType otype)
		//{
		//	try
		//	{
		//		var di = Directory.CreateDirectory($"{Def.ObjectsDir}/{otype}s");
		//		return di.FullName;
		//	}
		//	catch (Exception ex)
		//	{
		//		throw new ActionException(ex, new StackFrame(true));
		//	}
		//}

		///// <summary>
		///// Function to get a 3-letter prefix for the given type
		///// </summary>
		///// <param name="type">The given type</param>
		///// <returns>A 3-letter prefix</returns>
		///// <exception cref="ActionException"/>
		//[Unlogged(IsLogged = false)]
		//public static string Prefix(ActionType type)
		//{
		//	string? Out = null;

		//	try
		//	{
		//		Out = type switch
		//		{
		//			ActionType.None => "Non",
		//			ActionType.Read => "Rea",
		//			ActionType.Write => "Wrt",
		//			ActionType.Update => "Upd",
		//			ActionType.Add => "Add",
		//			ActionType.Remove => "Rem",
		//			ActionType.Show => "Sho",
		//			ActionType.Hide => "Hid",
		//			ActionType.Delete => "Del",
		//			ActionType.Create => "Crt",
		//			ActionType.Activate => "Act",
		//			ActionType.Deactivate => "Dea",
		//			ActionType.Link => "Lnk",
		//			ActionType.Unlink => "Ulk",
		//			ActionType.All => "All",
		//			ActionType.Unknown => "Unk",
		//			_ => throw new Exception($"A prefix could not be found for the given {type.GetType().Name} {Format<Enum>.ExcValue(type)}"),
		//		};
		//	}
		//	catch (Exception ex)
		//	{
		//		throw new ActionException(ex, new StackFrame(true));
		//	}
		//	finally
		//	{
		//		Out ??= "___";
		//	}

		//	return Out;
		//}

		/// <inheritdoc cref="Prefix(ActionType)"/>
		[Unlogged]
		public static string Prefix(BlockType type)
		{
			string? Out = null;

			try
			{
				Out = type switch
				{
					Enums.BlockType.None => "Non",
					Enums.BlockType.Constructor => "Ctr",
					Enums.BlockType.Primary_Constructor => "PCr",
					Enums.BlockType.Empty_Constructor => "ECr",
					Enums.BlockType.Destructor => "Dtr",
					Enums.BlockType.Property => "Prp",
					Enums.BlockType.Field => "Fld",
					Enums.BlockType.Event => "Evt",
					Enums.BlockType.Parameter => "Prm",
					Enums.BlockType.Delegate => "Dlg",
					Enums.BlockType.Function => "Fun",
					Enums.BlockType.Override_Function => "OFn",
					Enums.BlockType.Static_Function => "SFn",
					Enums.BlockType.Virtual_Function => "VFn",
					Enums.BlockType.Page => "Pge",
					Enums.BlockType.Component => "Cmp",
					Enums.BlockType.Operator => "Opr",
					Enums.BlockType.Program => "Prg",
					Enums.BlockType.Get_Operator => "OpG",
					Enums.BlockType.Set_Operator => "OpS",
					Enums.BlockType.Addition_Operator => "Op+",
					Enums.BlockType.Subtraction_Operator => "Op-",
					Enums.BlockType.Multiplication_Operator => "Op*",
					Enums.BlockType.Division_Operator => "Op/",
					Enums.BlockType.Modulo_Operator => "Op%",
					Enums.BlockType.Concat_Operator => "O+=",
					Enums.BlockType.Explicit_Operator => "OEx",
					Enums.BlockType.Implicit_Operator => "OIm",
					Enums.BlockType.Equals_Operator => "O==",
					Enums.BlockType.Not_Equals_Operator => "O!=",
					Enums.BlockType.Lesser_Operator => "Op<",
					Enums.BlockType.Greater_Operator => "Op>",
					Enums.BlockType.Lesser_Or_Equals_Operator => "O<=",
					Enums.BlockType.Greater_Or_Equals_Operator => "O>=",
					Enums.BlockType.Init_Operator => "OIn",
					Enums.BlockType.Increment_Operator => "O++",
					Enums.BlockType.Decrement_Operator => "O--",
					Enums.BlockType.Positive_Operator => "OV+",
					Enums.BlockType.Negative_Operator => "OV-",
					Enums.BlockType.Default => "Def",
					_ => throw new Exception($"A prefix could not be found for the given {type.GetType().Name} {Format<Enum>.ExcValue(type)}"),
				};
			}
			catch (Exception ex)
			{
				throw new ActionException(ex, new StackFrame(true));
			}
			finally
			{
				Out ??= "___";
			}

			return Out;
		}

		/// <inheritdoc cref="Prefix(ActionType)"/>
		//[Unlogged]
		//public static string Prefix(DataType type)
		//{
		//	string? Out = null;

		//	try
		//	{
		//		Out = type switch
		//		{
		//			DataType.Binary or DataType.Byte or DataType.Date or DataType.Decimal or DataType.Number or DataType.String or DataType.Time or DataType.File => type.GetType().ToString()[..3],
		//			DataType.Boolean => "Bln",
		//			DataType.Char => "Chr",
		//			DataType.Double => "Dbl",
		//			DataType.Empty => "Ety",
		//			DataType.Int16 => "I16",
		//			DataType.Int32 => "I32",
		//			DataType.Int64 => "I64",
		//			DataType.Moment => "Mmt",
		//			DataType.SByte => "SBy",
		//			DataType.Single => "Sng",
		//			DataType.Text => "Txt",
		//			_ => throw new Exception($"A prefix could not be found for the given {type.GetType().Name} {Format<Enum>.ExcValue(type)}"),
		//		};
		//	}
		//	catch (Exception ex)
		//	{
		//		throw new ActionException(ex, new StackFrame(true));
		//	}
		//	finally
		//	{
		//		Out ??= "___";
		//	}

		//	return Out;
		//}

		/// <inheritdoc cref="Prefix(ActionType)"/>
		[Unlogged]
		public static string Prefix(DeviceType type)
		{
			string? Out = null;

			try
			{
				Out = type switch
				{
					DeviceType.None => "Non",
					DeviceType.AccessPoint => "APt",
					DeviceType.Bridge => "Bdg",
					DeviceType.Cellphone => "CPh",
					DeviceType.Computer => "Cpr",
					DeviceType.Firewall => "Fwl",
					DeviceType.Internet => "Int",
					DeviceType.Laptop => "Ltp",
					DeviceType.Modem => "Mdm",
					DeviceType.Printer => "Prn",
					DeviceType.Repeater => "Rep",
					DeviceType.Router => "Rtr",
					DeviceType.Scanner => "Scn",
					DeviceType.Screen => "Scr",
					DeviceType.Server => "Srv",
					DeviceType.Switch => "Swt",
					DeviceType.Tablet => "Tbt",
					_ => throw new Exception($"A prefix could not be found for the given {type.GetType().Name} {Format<Enum>.ExcValue(type)}"),
				};
			}
			catch (Exception ex)
			{
				throw new ActionException(ex, new StackFrame(true));
			}
			finally
			{
				Out ??= "___";
			}

			return Out;
		}

		/// <inheritdoc cref="Prefix(ActionType)"/>
		[Unlogged]
		public static string Prefix(ExType type)
		{
			string? Out = null;

			try
			{
				Out = type switch
				{
					ExType.Base => "Bas",
					ExType.Action => "Act",
					ExType.Attribute => "Atr",
					ExType.Component => "Cmp",
					ExType.Context => "Ctx",
					ExType.Current => "Cur",
					ExType.Date => "Dat",
					ExType.Device => "Dvc",
					ExType.Enum => "Enm",
					ExType.Exception => "Exc",
					ExType.History => "Hry",
					ExType.Identity => "Idt",
					ExType.ID => "Id_",
					ExType.Link => "Lnk",
					ExType.Model => "Mdl",
					ExType.Moment => "Mmt",
					ExType.Name => "Nam",
					ExType.Number => "Num",
					ExType.Object => "Obj",
					ExType.Page => "Pge",
					ExType.Permission => "Pmn",
					ExType.Program => "Prg",
					ExType.Property => "Pty",
					ExType.Time => "Tim",
					ExType.Type => "Typ",
					ExType.Value => "Val",
					_ => throw new Exception($"A prefix could not be found for the given {type.GetType().Name} {Format<Enum>.ExcValue(type)}"),
				};
			}
			catch (Exception ex)
			{
				throw new ActionException(ex, new StackFrame(true));
			}
			finally
			{
				Out ??= "___";
			}

			return Out;
		}

		/// <inheritdoc cref="Prefix(ActionType)"/>
		[Unlogged]
		public static string Prefix(LinkType type)
		{
			string? Out = null;

			try
			{
				Out = type switch
				{
					LinkType.None or LinkType.Object or LinkType.All => $"{type}"[..3],
					LinkType.Device => "Dvc",
					_ => throw new Exception($"A prefix could not be found for the given {type.GetType().Name} {Format<Enum>.ExcValue(type)}"),
				};
			}
			catch (Exception ex)
			{
				throw new ActionException(ex, new StackFrame(true));
			}
			finally
			{
				Out ??= "___";
			}

			return Out;
		}

		/// <inheritdoc cref="Prefix(ActionType)"/>
		[Unlogged]
		public static string Prefix(LogType type)
		{
			string? Out = null;

			try
			{
				Out = type switch
				{
					LogType.Event => "Evt",
					LogType.Error or LogType.Exception or LogType.Info => $"{type}"[..3],
					LogType.Warning => "Wrn",
					_ => throw new Exception($"A prefix could not be found for the given {type.GetType().Name} {Format<Enum>.ExcValue(type)}"),
				};
			}
			catch (Exception ex)
			{
				throw new ActionException(ex, new StackFrame(true));
			}
			finally
			{
				Out ??= "___";
			}

			return Out;
		}

		/// <inheritdoc cref="Prefix(ActionType)"/>
		[Unlogged]
		public static string Prefix(Months type)
		{
			string? Out = null;

			try
			{
				Out = type switch
				{
					Months.January or Months.February or Months.March or Months.April or Months.May or Months.June or Months.July or Months.August or Months.September or Months.October or Months.November or Months.December => $"{type}"[..3],
					_ => throw new Exception($"A prefix could not be found for the given {type.GetType().Name} {Format<Enum>.ExcValue(type)}"),
				};
			}
			catch (Exception ex)
			{
				throw new ActionException(ex, new StackFrame(true));
			}
			finally
			{
				Out ??= "___";
			}

			return Out;
		}

		/// <inheritdoc cref="Prefix(ActionType)"/>
		[Unlogged]
		public static string Prefix(ObjectType type)
		{
			string? Out = null;

			try
			{
				Out = type switch
				{
					Enums.ObjectType.Database => "Dtb",
					Enums.ObjectType.Field => "Fld",
					Enums.ObjectType.Object => "Obj",
					Enums.ObjectType.Table => "Tbl",
					Enums.ObjectType.User => "Usr",
					_ => throw new Exception($"A prefix could not be found for the given {type.GetType().Name} {Format<Enum>.ExcValue(type)}"),
				};
			}
			catch (Exception ex)
			{
				throw new ActionException(ex, new StackFrame(true));
			}
			finally
			{
				Out ??= "___";
			}

			return Out;
		}

		/// <inheritdoc cref="Prefix(ActionType)"/>
		[Unlogged]
		public static string Prefix(PageType type)
		{
			string? Out = null;

			try
			{
				Out = type switch
				{
					PageType.Home or PageType.Auth or PageType.Register or PageType.Account or PageType.Create or PageType.Details or PageType.Delete or PageType.Admin or PageType.Exception or PageType.Unknown => $"{type}"[..3],
					PageType.Exit => "Ext",
					PageType.Login => "Lgn",
					PageType.Logout => "Lgt",
					PageType.Index => "Idx",
					PageType.Update => "Udt",
					PageType.NotFound => "NFd",
					_ => throw new Exception($"A prefix could not be found for the given {type.GetType().Name} {Format<Enum>.ExcValue(type)}"),
				};
			}
			catch (Exception ex)
			{
				throw new ActionException(ex, new StackFrame(true));
			}
			finally
			{
				Out ??= "___";
			}

			return Out;
		}

		/// <inheritdoc cref="Prefix(ActionType)"/>
		[Unlogged]
		public static string Prefix(PlatformType type)
		{
			string? Out = null;

			try
			{
				Out = type switch
				{
					PlatformType.None or PlatformType.iPhone or PlatformType.iPod or PlatformType.iPad or PlatformType.Android or PlatformType.Mac or PlatformType.Web => $"{type}"[..3],
					PlatformType.PC => "PC_",
					PlatformType.ChromeOS => "COS",
					_ => throw new Exception($"A prefix could not be found for the given {type.GetType().Name} {Format<Enum>.ExcValue(type)}"),
				};
			}
			catch (Exception ex)
			{
				throw new ActionException(ex, new StackFrame(true));
			}
			finally
			{
				Out ??= "___";
			}

			return Out;
		}

		/// <inheritdoc cref="Prefix(ActionType)"/>
		[Unlogged]
		public static string Prefix(UserType type)
		{
			string? Out = null;

			try
			{
				Out = type switch
				{
					UserType.Guest => "Gst",
					UserType.Normal => "Nml",
					UserType.Moderator or UserType.Administrator or UserType.Root or UserType.None => $"{type}"[..3],
					_ => throw new Exception($"A prefix could not be found for the given {type.GetType().Name} {Format<Enum>.ExcValue(type)}"),
				};
			}
			catch (Exception ex)
			{
				throw new ActionException(ex, new StackFrame(true));
			}
			finally
			{
				Out ??= "___";
			}

			return Out;
		}

		[Unlogged]
		public static Regex Regex(params RegexCategory[] categories)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			Regex? Out = null;
			string pattern = "[";

			try
			{
				foreach (var i in categories.DistinctBy(e => e.ToString()))
				{
					if (i.HasFlag(RegexCategory.None))
						break;
					else
						pattern += "/p{";

					if (i.HasFlag(RegexCategory.UppercaseLetter))
						pattern += "Lu";

					if (i.HasFlag(RegexCategory.LowercaseLetter))
						pattern += "Ll";

					if (i.HasFlag(RegexCategory.TitleCaseLetter))
						pattern += "Lt";

					if (i.HasFlag(RegexCategory.ModifierLetter))
						pattern += "Lm";

					if (i.HasFlag(RegexCategory.OtherLetter))
						pattern += "Lo";

					if (i.HasFlag(RegexCategory.NonSpacingMark))
						pattern += "Mn";

					if (i.HasFlag(RegexCategory.SpacingCombiningMark))
						pattern += "Mc";

					if (i.HasFlag(RegexCategory.EnclosingMark))
						pattern += "Me";

					if (i.HasFlag(RegexCategory.DigitNumber))
						pattern += "Nd";

					if (i.HasFlag(RegexCategory.LetterNumber))
						pattern += "Nl";

					if (i.HasFlag(RegexCategory.OtherNumber))
						pattern += "No";

					if (i.HasFlag(RegexCategory.ConnectorPunctuation))
						pattern += "Pc";

					if (i.HasFlag(RegexCategory.DashPunctuation))
						pattern += "Pd";

					if (i.HasFlag(RegexCategory.OpenPunctuation))
						pattern += "Ps";

					if (i.HasFlag(RegexCategory.ClosePunctuation))
						pattern += "Pe";

					if (i.HasFlag(RegexCategory.InitQuotePunctuation))
						pattern += "Pi";

					if (i.HasFlag(RegexCategory.FinalQuotePunctuation))
						pattern += "Pf";

					if (i.HasFlag(RegexCategory.OtherPunctuation))
						pattern += "Po";

					if (i.HasFlag(RegexCategory.MathSymbol))
						pattern += "Sm";

					if (i.HasFlag(RegexCategory.CurrencySymbol))
						pattern += "Sc";

					if (i.HasFlag(RegexCategory.ModifierSymbol))
						pattern += "Sk";

					if (i.HasFlag(RegexCategory.OtherSymbol))
						pattern += "So";

					if (i.HasFlag(RegexCategory.SpaceSeparator))
						pattern += "Zs";

					if (i.HasFlag(RegexCategory.LineSeparator))
						pattern += "Zl";

					if (i.HasFlag(RegexCategory.ParagraphSeparator))
						pattern += "Zp";

					if (i.HasFlag(RegexCategory.ControlOther))
						pattern += "Cc";

					if (i.HasFlag(RegexCategory.FormatOther))
						pattern += "Cf";

					if (i.HasFlag(RegexCategory.SurrogateOther))
						pattern += "Cs";

					if (i.HasFlag(RegexCategory.PrivateUseOther))
						pattern += "Co";

					if (i.HasFlag(RegexCategory.NotAssignedOther))
						pattern += "Cn";

					pattern += "}";
				}

				Out = new($@"{pattern}]+");
			}
			catch (Exception ex)
			{
				throw new ActionException(ex, sf);
			}
			finally
			{
				Out ??= new(@"[\w]*");
			}

			return Out;
		}

		public static Regex Regex(Range range, params RegexCategory[] categories)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			Regex? Out = null;
			string pattern = "[";

			try
			{
				foreach (var i in categories.DistinctBy(e => e.ToString()))
				{
					if (i.HasFlag(RegexCategory.None))
						break;
					else
						pattern += "/p{";

					if (i.HasFlag(RegexCategory.UppercaseLetter))
						pattern += "Lu";

					if (i.HasFlag(RegexCategory.LowercaseLetter))
						pattern += "Ll";

					if (i.HasFlag(RegexCategory.TitleCaseLetter))
						pattern += "Lt";

					if (i.HasFlag(RegexCategory.ModifierLetter))
						pattern += "Lm";

					if (i.HasFlag(RegexCategory.OtherLetter))
						pattern += "Lo";

					if (i.HasFlag(RegexCategory.NonSpacingMark))
						pattern += "Mn";

					if (i.HasFlag(RegexCategory.SpacingCombiningMark))
						pattern += "Mc";

					if (i.HasFlag(RegexCategory.EnclosingMark))
						pattern += "Me";

					if (i.HasFlag(RegexCategory.DigitNumber))
						pattern += "Nd";

					if (i.HasFlag(RegexCategory.LetterNumber))
						pattern += "Nl";

					if (i.HasFlag(RegexCategory.OtherNumber))
						pattern += "No";

					if (i.HasFlag(RegexCategory.ConnectorPunctuation))
						pattern += "Pc";

					if (i.HasFlag(RegexCategory.DashPunctuation))
						pattern += "Pd";

					if (i.HasFlag(RegexCategory.OpenPunctuation))
						pattern += "Ps";

					if (i.HasFlag(RegexCategory.ClosePunctuation))
						pattern += "Pe";

					if (i.HasFlag(RegexCategory.InitQuotePunctuation))
						pattern += "Pi";

					if (i.HasFlag(RegexCategory.FinalQuotePunctuation))
						pattern += "Pf";

					if (i.HasFlag(RegexCategory.OtherPunctuation))
						pattern += "Po";

					if (i.HasFlag(RegexCategory.MathSymbol))
						pattern += "Sm";

					if (i.HasFlag(RegexCategory.CurrencySymbol))
						pattern += "Sc";

					if (i.HasFlag(RegexCategory.ModifierSymbol))
						pattern += "Sk";

					if (i.HasFlag(RegexCategory.OtherSymbol))
						pattern += "So";

					if (i.HasFlag(RegexCategory.SpaceSeparator))
						pattern += "Zs";

					if (i.HasFlag(RegexCategory.LineSeparator))
						pattern += "Zl";

					if (i.HasFlag(RegexCategory.ParagraphSeparator))
						pattern += "Zp";

					if (i.HasFlag(RegexCategory.ControlOther))
						pattern += "Cc";

					if (i.HasFlag(RegexCategory.FormatOther))
						pattern += "Cf";

					if (i.HasFlag(RegexCategory.SurrogateOther))
						pattern += "Cs";

					if (i.HasFlag(RegexCategory.PrivateUseOther))
						pattern += "Co";

					if (i.HasFlag(RegexCategory.NotAssignedOther))
						pattern += "Cn";

					pattern += "}";
				}

				Out = new($@"{pattern}]{{{range.Start},{range.End}}}");
			}
			catch (Exception ex)
			{
				throw new ActionException(ex, sf);
			}
			finally
			{
				Out ??= new(@"[\w]*");
			}

			return Out;
		}

		public static Regex Regex(int length, params RegexCategory[] categories)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			Regex? Out = null;
			string pattern = "[";

			try
			{
				foreach (var i in categories.DistinctBy(e => e.ToString()))
				{
					if (i.HasFlag(RegexCategory.None))
						break;
					else
						pattern += "/p{";

					if (i.HasFlag(RegexCategory.UppercaseLetter))
						pattern += "Lu";

					if (i.HasFlag(RegexCategory.LowercaseLetter))
						pattern += "Ll";

					if (i.HasFlag(RegexCategory.TitleCaseLetter))
						pattern += "Lt";

					if (i.HasFlag(RegexCategory.ModifierLetter))
						pattern += "Lm";

					if (i.HasFlag(RegexCategory.OtherLetter))
						pattern += "Lo";

					if (i.HasFlag(RegexCategory.NonSpacingMark))
						pattern += "Mn";

					if (i.HasFlag(RegexCategory.SpacingCombiningMark))
						pattern += "Mc";

					if (i.HasFlag(RegexCategory.EnclosingMark))
						pattern += "Me";

					if (i.HasFlag(RegexCategory.DigitNumber))
						pattern += "Nd";

					if (i.HasFlag(RegexCategory.LetterNumber))
						pattern += "Nl";

					if (i.HasFlag(RegexCategory.OtherNumber))
						pattern += "No";

					if (i.HasFlag(RegexCategory.ConnectorPunctuation))
						pattern += "Pc";

					if (i.HasFlag(RegexCategory.DashPunctuation))
						pattern += "Pd";

					if (i.HasFlag(RegexCategory.OpenPunctuation))
						pattern += "Ps";

					if (i.HasFlag(RegexCategory.ClosePunctuation))
						pattern += "Pe";

					if (i.HasFlag(RegexCategory.InitQuotePunctuation))
						pattern += "Pi";

					if (i.HasFlag(RegexCategory.FinalQuotePunctuation))
						pattern += "Pf";

					if (i.HasFlag(RegexCategory.OtherPunctuation))
						pattern += "Po";

					if (i.HasFlag(RegexCategory.MathSymbol))
						pattern += "Sm";

					if (i.HasFlag(RegexCategory.CurrencySymbol))
						pattern += "Sc";

					if (i.HasFlag(RegexCategory.ModifierSymbol))
						pattern += "Sk";

					if (i.HasFlag(RegexCategory.OtherSymbol))
						pattern += "So";

					if (i.HasFlag(RegexCategory.SpaceSeparator))
						pattern += "Zs";

					if (i.HasFlag(RegexCategory.LineSeparator))
						pattern += "Zl";

					if (i.HasFlag(RegexCategory.ParagraphSeparator))
						pattern += "Zp";

					if (i.HasFlag(RegexCategory.ControlOther))
						pattern += "Cc";

					if (i.HasFlag(RegexCategory.FormatOther))
						pattern += "Cf";

					if (i.HasFlag(RegexCategory.SurrogateOther))
						pattern += "Cs";

					if (i.HasFlag(RegexCategory.PrivateUseOther))
						pattern += "Co";

					if (i.HasFlag(RegexCategory.NotAssignedOther))
						pattern += "Cn";

					pattern += "}";
				}

				Out = new($@"{pattern}]{{{length}}}");
			}
			catch (Exception ex)
			{
				throw new ActionException(ex, sf);
			}
			finally
			{
				Out ??= new(@"[\w]*");
			}

			return Out;
		}
	}

	public static class Get<TEnum> where TEnum : struct, Enum
	{
		[Unlogged]
		public static string Prefix(TEnum type)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			string? Out = null;

			try
			{
				var oArr = Enum.GetValues<ObjectType>();
				var otype = (type as ObjectType?);

				if (otype.HasValue && oArr.Contains(otype.Value))
					Out = (otype.ToString() ?? "").ToUpper();
				else
				{
					oArr = null;
					otype = null;

					var uArr = Enum.GetValues<UserType>();
					var utype = (type as UserType?);

					if (utype.HasValue && uArr.Contains(utype.Value))
						Out = (utype.ToString() ?? "").ToUpper();
				}

				if (Out == null)
					throw new Exception($"The given value {Format<TEnum>.ExcValue(type)} is not valid for this function");
			}
			catch (Exception ex)
			{
				throw new ActionException(ex, new StackFrame(true));
			}
			finally
			{
				Out ??= "";
			}

			return Out;
		}

		//public static TEnum Type(BaseID<TEnum> id)
		//{
		//	TEnum? Out = null;

		//	try
		//	{
		//		string str = $"{id.Value:B64}";
		//		string topStr = str[..4];
		//		byte topID = Def.TopTypeIDs.GetValueOrDefault(typeof(TEnum));

		//		if (byte.Parse(topStr) != topID)
		//			throw new Exception($"The given id {Format<BaseID<TEnum>>.ExcValue(id)} does not correspond to any valid type");
		//		else
		//		{
		//			//Def.TopTypeIDs.Keys.
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		throw new ActionException(ex, new StackFrame(true));
		//	}

		//	return Out ?? Def<TEnum>.Value;
		//}
	}
}
