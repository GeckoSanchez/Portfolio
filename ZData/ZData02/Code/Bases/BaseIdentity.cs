namespace ZData02.Bases
{
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Diagnostics.CodeAnalysis;
	using Actions;
	using Attributes;
	using Exceptions;
	using MessagePack;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class BaseIdentity<TEnum> : BaseData<BaseID<TEnum>>, IEquatable<BaseIdentity<TEnum>?> where TEnum : struct, Enum
	{
		/// <summary>
		/// The base name for this Identity
		/// </summary>
		[JsonProperty]
		public BaseName<TEnum> Name { get; protected init; }

		/// <summary>
		/// The base type for this Identity
		/// </summary>
		[JsonProperty]
		public BaseType<TEnum> Type { get; protected init; }

		/// <summary>
		/// The base ID for this Identity
		/// </summary>
		[JsonProperty("ID")]
		public new BaseID<TEnum> Data => base.Data;

		/// <summary>
		/// Primary constructor for the <see cref="BaseIdentity{TEnum}"/> class
		/// </summary>
		/// <param name="name">The <see cref="BaseName{TEnum}"/> name</param>
		/// <param name="type">The <see cref="BaseType{TEnum}"/> type</param>
		/// <param name="id">The <see cref="BaseID{TEnum}"/> ID</param>
		/// <exception cref="IdentityException"/>
		[JsonConstructor, MainConstructor]
		public BaseIdentity([NotNull] BaseName<TEnum> name, [NotNull] BaseType<TEnum> type, [NotNull] BaseID<TEnum> id) : base(new(type.Data))
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				Name = new(name);
				Type = new(type);
			}
			catch (Exception ex)
			{
				throw new IdentityException(ex, sf);
			}
			finally
			{
				try
				{
					Name ??= new(Def.Name);
					Type ??= new(Def<TEnum>.Value);
					base.Data = new(Type, ulong.CreateTruncating(id.Data));
				}
				catch (Exception) { }
			}
		}

		/// <summary>
		/// Constructor for the <see cref="BaseIdentity{TEnum}"/>
		/// </summary>
		/// <inheritdoc cref="BaseIdentity{TEnum}(BaseName{TEnum},BaseType{TEnum},BaseID{TEnum})"/>
		public BaseIdentity([NotNull] BaseName<TEnum> name, [NotNull] BaseType<TEnum> type) : this(name, type, new(type.Data)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="BaseIdentity{TEnum}(BaseName{TEnum},BaseType{TEnum})"/>
		public BaseIdentity([NotNull] BaseName<TEnum> name, [NotNull] TEnum type) : this(name, type, new(type)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="BaseIdentity{TEnum}(BaseName{TEnum},BaseType{TEnum})"/>
		public BaseIdentity([NotNull] BaseName<TEnum> name) : this(name, Def<TEnum>.Value, new(Def<TEnum>.Value)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="BaseData{TData}.Equals(object?)"/>
		/// <exception cref="IdentityException"/>
		public override bool Equals(object? obj)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			bool? Out = null;

			try
			{
				Out = Equals(obj as BaseIdentity<TEnum>);
			}
			catch (Exception ex)
			{
				throw new IdentityException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= base.Equals(obj);
				}
				catch (Exception) { }
			}

			return Out ?? false;
		}

		/// <inheritdoc cref="BaseData{TData}.Equals(BaseData{TData}?)"/>
		/// <exception cref="IdentityException"/>
		public bool Equals(BaseIdentity<TEnum>? other)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			bool? Out = null;

			try
			{
				Out = other is not null && base.Equals(other) &&
						 EqualityComparer<BaseID<TEnum>>.Default.Equals(Data, other.Data) &&
						 EqualityComparer<BaseName<TEnum>>.Default.Equals(Name, other.Name) &&
						 EqualityComparer<BaseType<TEnum>>.Default.Equals(Type, other.Type);
			}
			catch (BaseException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new IdentityException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= Equals(other);
				}
				catch (Exception) { }
			}

			return Out ?? false;
		}

		public override int GetHashCode()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			int? Out = null;

			try
			{
				Out = HashCode.Combine(base.GetHashCode(), Data, Name, Type);
			}
			catch (BaseException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new IdentityException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= base.GetHashCode();
				}
				catch (Exception) { }
			}

			return Out ?? 0;
		}

		/// <inheritdoc cref="BaseData{TData}.ToString"/>
		/// <exception cref="IdentityException"/>
		public override string ToString()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			string? Out = null;

			try
			{
				Out = $"{Name} {Type.ToString().ToLower()} (#{Data})";
			}
			catch (Exception ex)
			{
				throw new IdentityException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= base.ToString();
				}
				catch (Exception)
				{
					Out = "";
				}
			}

			return Out;
		}

		/// <inheritdoc cref="BaseData{TData}.ToMessagePack(MessagePackSerializerOptions?)"/>
		/// <exception cref="IdentityException"/>
		public override byte[] ToMessagePack(MessagePackSerializerOptions? opts = null)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			byte[]? Out = null;

			try
			{
				Out = Name.ToMessagePack(opts);
				Out = [.. Out, .. Type.ToMessagePack(opts)];
				Out = [.. Out, .. Data.ToMessagePack(opts)];
			}
			catch (BaseException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new IdentityException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= base.ToMessagePack(opts);
				}
				catch (Exception) { }
			}

			return Out;
		}

		public static bool operator ==(BaseIdentity<TEnum>? left, BaseIdentity<TEnum>? right) => EqualityComparer<BaseIdentity<TEnum>>.Default.Equals(left, right);
		public static bool operator !=(BaseIdentity<TEnum>? left, BaseIdentity<TEnum>? right) => !(left == right);
	}
}
