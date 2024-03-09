//namespace ZData02.Actions
//{
//	using System.Diagnostics;
//	using Attributes;
//	using Enums;
//	using Exceptions;
//	using IDs;
//	using Microsoft.IO;
//	using ZData02.Bases;

//	public static class Generate
//	{
//		[Unlogged]
//		public static DeviceID DeviceID(DeviceType? type = null)
//		{
//			DeviceID? Out = null;

//			try
//			{
//				Out = new(type ?? Def.DvcType);
//			}
//			catch (Exception ex)
//			{
//				throw new ActionException(ex, new StackFrame(true));
//			}
//			finally
//			{
//				Out ??= new();
//			}

//			return Out;
//		}

//		[Unlogged]
//		public static ObjectID ObjectID(ObjectType? type = null)
//		{
//			ObjectID? Out = null;

//			try
//			{
//				Out = new(type ?? Def.OType);
//			}
//			catch (Exception ex)
//			{
//				throw new ActionException(ex, new StackFrame(true));
//			}
//			finally
//			{
//				Out ??= new();
//			}

//			return Out;
//		}

//		[Unlogged]
//		public static BaseName<DeviceType> DeviceName()
//		{
//			BaseName<DeviceType>? Out = null;

//			try
//			{
//				string name = "";
//				string str = "";
//				int rdm;

//				IEnumerable<string> strings = File.ReadAllLines("text.txt").Where(e => e.Length <= 8);

//				while (name.Length <= 41)
//				{
//					rdm = Random.Shared.Next(strings.Count());
//					str = strings.ElementAt(rdm);
//					name += $"{str[0].ToString().ToUpper()}{str[1..].ToLower()}";
//				}

//				Out = new(name[..40]);
//			}
//			catch (Exception ex)
//			{
//				throw new ActionException(ex, new StackFrame(true));
//			}
//			finally
//			{
//				Out ??= new(Def.Name);
//			}

//			return Out;
//		}

//		[Unlogged]
//		public static BaseName<LinkType> LinkName()
//		{
//			BaseName<LinkType>? Out = null;

//			try
//			{
//				string name = "";
//				string str = "";
//				int rdm;

//				IEnumerable<string> strings = File.ReadAllLines("text.txt").Where(e => e.Length <= 8);

//				while (name.Length <= 41)
//				{
//					rdm = Random.Shared.Next(strings.Count());
//					str = strings.ElementAt(rdm);
//					name += $"{str[0].ToString().ToUpper()}{str[1..].ToLower()}";
//				}

//				Out = new(name[..40]);
//			}
//			catch (Exception ex)
//			{
//				throw new ActionException(ex, new StackFrame(true));
//			}
//			finally
//			{
//				Out ??= new(Def.Name);
//			}

//			return Out;
//		}

//		[Unlogged]
//		public static BaseName<ObjectType> ObjectName()
//		{
//			BaseName<ObjectType>? Out = null;

//			try
//			{
//				string name = "";
//				string str = "";
//				int rdm;

//				IEnumerable<string> strings = File.ReadAllLines("text.txt").Where(e => e.Length <= 8);

//				while (name.Length <= 41)
//				{
//					rdm = Random.Shared.Next(strings.Count());
//					str = strings.ElementAt(rdm);
//					name += $"{str[0].ToString().ToUpper()}{str[1..].ToLower()}";
//				}

//				Out = new(name[..40]);
//			}
//			catch (Exception ex)
//			{
//				throw new ActionException(ex, new StackFrame(true));
//			}
//			finally
//			{
//				Out ??= new(Def.Name);
//			}

//			return Out;
//		}
//	}
//}
