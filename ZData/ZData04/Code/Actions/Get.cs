namespace ZData04.Actions
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
		/// <returns>The found block type, or <see cref="BlockKind.Function"/> otherwise</returns>
		/// <exception cref="ActionException"/>
		[Unlogged]
		public static BlockKind BlockKind(MethodBase? method)
		{
			BlockKind? Out;

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
							Out = Enums.BlockKind.Not_Equals_Operator;
						else if (name.EndsWith("Equality"))
							Out = Enums.BlockKind.Equals_Operator;
						else if (name.EndsWith("GreaterThan"))
							Out = Enums.BlockKind.Greater_Operator;
						else if (name.EndsWith("GreaterThanOrEqual"))
							Out = Enums.BlockKind.Greater_Or_Equals_Operator;
						else if (name.EndsWith("LessThan"))
							Out = Enums.BlockKind.Lesser_Operator;
						else if (name.EndsWith("LessThanOrEqual"))
							Out = Enums.BlockKind.Lesser_Or_Equals_Operator;
						else if (name.EndsWith("UnaryPlus"))
							Out = Enums.BlockKind.Positive_Operator;
						else if (name.EndsWith("UnaryNegation"))
							Out = Enums.BlockKind.Negative_Operator;
						else if (name.EndsWith("Addition"))
							Out = Enums.BlockKind.Addition_Operator;
						else if (name.EndsWith("Subtraction"))
							Out = Enums.BlockKind.Subtraction_Operator;
						else if (name.EndsWith("Multiply"))
							Out = Enums.BlockKind.Multiplication_Operator;
						else if (name.EndsWith("Division"))
							Out = Enums.BlockKind.Division_Operator;
						else if (name.EndsWith("Modulus"))
							Out = Enums.BlockKind.Modulo_Operator;
						else if (name.EndsWith("Implicit"))
							Out = Enums.BlockKind.Implicit_Operator;
						else if (name.EndsWith("Explicit"))
							Out = Enums.BlockKind.Explicit_Operator;
						else
							Out = Enums.BlockKind.Operator;
					}
					else if (name.StartsWith("get_"))
						Out = Enums.BlockKind.Get_Operator;
					else if (name.StartsWith("set_"))
						Out = Enums.BlockKind.Set_Operator;
					else if (name.StartsWith("init_"))
						Out = Enums.BlockKind.Init_Operator;
					else if (name.EndsWith(nameof(Attribute)))
						Out = Enums.BlockKind.Attribute;
					else if (method.IsConstructor && method.GetParameters().Length == 0)
						Out = Enums.BlockKind.Empty_Constructor;
					else if (method.IsConstructor && (method.GetCustomAttribute<JsonConstructorAttribute>(false) != null || method.GetCustomAttribute<MainConstructorAttribute>(false) != null))
						Out = Enums.BlockKind.Primary_Constructor;
					else if (method.Attributes.HasFlag(MethodAttributes.CheckAccessOnOverride))
						Out = Enums.BlockKind.Constructor;
					else if (method.IsConstructor)
						Out = Enums.BlockKind.Constructor;
					else if (method.Name is (nameof(Equals)) or (nameof(ToString)) or "SetParametersAsync" or "OnInitialized" or "OnInitializedAsync" or "OnParametersSet" or "OnParametersSetAsync" or "OnAfterRender" or "OnAfterRenderAsync" or "IsDefaultAttribute" or "Match")
						Out = Enums.BlockKind.Override_Function;
					else
						Out = Enums.BlockKind.Function;
				}
			}
			catch (Exception ex)
			{
				throw new ActionException(ex, new StackFrame(true));
			}

			return Out ?? Def.BlockKind;
		}

		/// <inheritdoc cref="BlockKind(MethodBase?)"/>
		[Unlogged]
		public static BlockKind BlockKind(StackFrame sf)
		{
			BlockKind? Out;

			try
			{
				Out = BlockKind(sf.GetMethod());
			}
			catch (Exception ex)
			{
				throw new ActionException(ex, new StackFrame(true));
			}

			return Out ?? Def.BlockKind;
		}

		/// <inheritdoc cref="BlockKind(MethodBase?)"/>
		[Unlogged]
		public static BlockKind BlockKindOrDefault(StackFrame sf)
		{
			BlockKind? Out;

			try
			{
				Out = BlockKind(sf.GetMethod());
			}
			catch (BaseException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new ActionException(ex, new StackFrame(true));
			}

			return Out ?? Def.BlockKind;
		}

		/// <inheritdoc cref="BlockKind(MethodBase?)"/>
		[Unlogged]
		public static BlockKind BlockKindOrDefault(StackFrame sf, BlockKind defaultValue)
		{
			BlockKind? Out;

			try
			{
				Out = BlockKind(sf.GetMethod());
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
		/// Function to get the <see cref="Enums.ObjectKind"/> from the given <paramref name="id"/>
		/// </summary>
		/// <param name="id">The given id</param>
		/// <returns>An <see cref="Enums.ObjectKind"/> (Default: <see cref="Def.ObjectKind"/>)</returns>
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
		public static string Prefix(BlockKind type)
		{
			string? Out = null;

			try
			{
				Out = type switch
				{
					Enums.BlockKind.None => "Non",
					Enums.BlockKind.Constructor => "Ctr",
					Enums.BlockKind.Primary_Constructor => "PCr",
					Enums.BlockKind.Empty_Constructor => "ECr",
					Enums.BlockKind.Destructor => "Dtr",
					Enums.BlockKind.Property => "Prp",
					Enums.BlockKind.Field => "Fld",
					Enums.BlockKind.Event => "Evt",
					Enums.BlockKind.Parameter => "Prm",
					Enums.BlockKind.Delegate => "Dlg",
					Enums.BlockKind.Function => "Fun",
					Enums.BlockKind.Override_Function => "OFn",
					Enums.BlockKind.Static_Function => "SFn",
					Enums.BlockKind.Virtual_Function => "VFn",
					Enums.BlockKind.Page => "Pge",
					Enums.BlockKind.Component => "Cmp",
					Enums.BlockKind.Operator => "Opr",
					Enums.BlockKind.Program => "Prg",
					Enums.BlockKind.Get_Operator => "OpG",
					Enums.BlockKind.Set_Operator => "OpS",
					Enums.BlockKind.Addition_Operator => "Op+",
					Enums.BlockKind.Subtraction_Operator => "Op-",
					Enums.BlockKind.Multiplication_Operator => "Op*",
					Enums.BlockKind.Division_Operator => "Op/",
					Enums.BlockKind.Modulo_Operator => "Op%",
					Enums.BlockKind.Concat_Operator => "O+=",
					Enums.BlockKind.Explicit_Operator => "OEx",
					Enums.BlockKind.Implicit_Operator => "OIm",
					Enums.BlockKind.Equals_Operator => "O==",
					Enums.BlockKind.Not_Equals_Operator => "O!=",
					Enums.BlockKind.Lesser_Operator => "Op<",
					Enums.BlockKind.Greater_Operator => "Op>",
					Enums.BlockKind.Lesser_Or_Equals_Operator => "O<=",
					Enums.BlockKind.Greater_Or_Equals_Operator => "O>=",
					Enums.BlockKind.Init_Operator => "OIn",
					Enums.BlockKind.Increment_Operator => "O++",
					Enums.BlockKind.Decrement_Operator => "O--",
					Enums.BlockKind.Positive_Operator => "OV+",
					Enums.BlockKind.Negative_Operator => "OV-",
					Enums.BlockKind.Default => "Def",
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
		public static string Prefix(DeviceKind type)
		{
			string? Out = null;

			try
			{
				Out = type switch
				{
					DeviceKind.None => "Non",
					DeviceKind.AccessPoint => "APt",
					DeviceKind.Bridge => "Bdg",
					DeviceKind.Cellphone => "CPh",
					DeviceKind.Computer => "Cpr",
					DeviceKind.Firewall => "Fwl",
					DeviceKind.Internet => "Int",
					DeviceKind.Laptop => "Ltp",
					DeviceKind.Modem => "Mdm",
					DeviceKind.Printer => "Prn",
					DeviceKind.Repeater => "Rep",
					DeviceKind.Router => "Rtr",
					DeviceKind.Scanner => "Scn",
					DeviceKind.Screen => "Scr",
					DeviceKind.Server => "Srv",
					DeviceKind.Switch => "Swt",
					DeviceKind.Tablet => "Tbt",
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
		public static string Prefix(ExceptKind type)
		{
			string? Out = null;

			try
			{
				Out = type switch
				{
					ExceptKind.Base => "Bas",
					ExceptKind.Action => "Act",
					ExceptKind.Attribute => "Atr",
					ExceptKind.Component => "Cmp",
					ExceptKind.Context => "Ctx",
					ExceptKind.Current => "Cur",
					ExceptKind.Date => "Dat",
					ExceptKind.Link => "Dvc",
					ExceptKind.Enum => "Enm",
					ExceptKind.Exception => "Exc",
					ExceptKind.History => "Hry",
					ExceptKind.Identity => "Idt",
					ExceptKind.ID => "Id_",
					ExceptKind.Model => "Mdl",
					ExceptKind.Moment => "Mmt",
					ExceptKind.Name => "Nam",
					ExceptKind.Number => "Num",
					ExceptKind.Object => "Obj",
					ExceptKind.Page => "Pge",
					ExceptKind.Permission => "Pmn",
					ExceptKind.Program => "Prg",
					ExceptKind.Property => "Pty",
					ExceptKind.Time => "Tim",
					ExceptKind.Type => "Typ",
					ExceptKind.Value => "Val",
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
		public static string Prefix(LinkKind type)
		{
			string? Out = null;

			try
			{
				Out = type switch
				{
					LinkKind.None or LinkKind.Object or LinkKind.All => $"{type}"[..3],
					LinkKind.Link => "Dvc",
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
		public static string Prefix(LogKind type)
		{
			string? Out = null;

			try
			{
				Out = type switch
				{
					LogKind.Event => "Evt",
					LogKind.Error or LogKind.Exception or LogKind.Info => $"{type}"[..3],
					LogKind.Warning => "Wrn",
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
		public static string Prefix(ObjectKind type)
		{
			string? Out = null;

			try
			{
				Out = type switch
				{
					Enums.ObjectKind.Database => "Dtb",
					Enums.ObjectKind.Field => "Fld",
					Enums.ObjectKind.Object => "Obj",
					Enums.ObjectKind.Table => "Tbl",
					Enums.ObjectKind.User => "Usr",
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
		public static string Prefix(PageKind type)
		{
			string? Out = null;

			try
			{
				Out = type switch
				{
					PageKind.Home or PageKind.Auth or PageKind.Register or PageKind.Account or PageKind.Create or PageKind.Details or PageKind.Delete or PageKind.Admin or PageKind.Exception or PageKind.Unknown => $"{type}"[..3],
					PageKind.Exit => "Ext",
					PageKind.Login => "Lgn",
					PageKind.Logout => "Lgt",
					PageKind.Index => "Idx",
					PageKind.Update => "Udt",
					PageKind.NotFound => "NFd",
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
		public static string Prefix(PlatformKind type)
		{
			string? Out = null;

			try
			{
				Out = type switch
				{
					PlatformKind.None or PlatformKind.iPhone or PlatformKind.iPod or PlatformKind.iPad or PlatformKind.Android or PlatformKind.Mac or PlatformKind.Web => $"{type}"[..3],
					PlatformKind.PC => "PC_",
					PlatformKind.ChromeOS => "COS",
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
		public static string Prefix(UserKind type)
		{
			string? Out = null;

			try
			{
				Out = type switch
				{
					UserKind.Guest => "Gst",
					UserKind.Normal => "Nml",
					UserKind.Moderator or UserKind.Administrator or UserKind.Root or UserKind.None => $"{type}"[..3],
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

		/// <summary>
		/// Function to build a regular expression with the + modifier at the end of it
		/// </summary>
		/// <param name="categories">The categories to build the regular expression</param>
		/// <returns>A regular expression</returns>
		/// <exception cref="ActionException"/>
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

		/// <summary>
		/// Function to build a regular expression with a fixed length
		/// </summary>
		/// <param name="range">A range of lengths for the regular expression</param>
		/// <param name="categories">The categories to build the regular expression</param>
		/// <returns>A regular expression</returns>
		/// <exception cref="ActionException"/>
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

		/// <summary>
		/// Function to build a regular expression with a fixed length
		/// </summary>
		/// <param name="length">The specified length</param>
		/// <param name="categories">The categories to build the regular expression</param>
		/// <returns>A regular expression</returns>
		/// <exception cref="ActionException"/>
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

		//public static string FullPath(BaseObject obj, bool isJSON = true)
		//{
		//	var sf = new StackFrame(true);
		//	Log.Event(sf);

		//	string? Out = null;
		//		DirectoryInfo? dir;

		//	try
		//	{
		//		if (isJSON)
		//		{
		//			dir = Directory.CreateDirectory($"{Def.ObjectsDir}/JSON/{obj.Identity.Type}s");
		//			Out = $"{dir.FullName}/{obj.Identity.Data.ToString()[..16]}.json";
		//		}
		//		else
		//		{
		//			dir = Directory.CreateDirectory($"{Def.ObjectsDir}/MSGPACK/{obj.Identity.Type}s");
		//			Out = $"{dir.FullName}/{obj.Identity.Data.ToString()[..16]}.msgpack";
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		throw new ActionException(ex, sf);
		//	}
		//	finally
		//	{
		//		Out ??= $"{Def.MainDir}/txt.txt";
		//	}

		//	return Out;
		//}
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
				var oArr = Enum.GetValues<ObjectKind>();
				var otype = (type as ObjectKind?);

				if (otype.HasValue && oArr.Contains(otype.Value))
					Out = (otype.ToString() ?? "").ToUpper();
				else
				{
					oArr = null;
					otype = null;

					var uArr = Enum.GetValues<UserKind>();
					var utype = (type as UserKind?);

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
