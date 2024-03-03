namespace ZData02.Bases
{
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Diagnostics.CodeAnalysis;
	using System.Globalization;
	using System.Xml.Linq;
	using Actions;
	using Attributes;
	using Enums;
	using Exceptions;
	using MessagePack;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class BaseType : BaseData<Enum>, IEquatable<BaseType?>
	{
		/// <summary>
		/// The base <see cref="Enum"/> data for this datum
		/// </summary>
		[JsonProperty]
		public new Enum Data { get => base.Data; set => base.Data = value; }

		/// <summary>
		/// Primary constructor for the <see cref="BaseType"/> class
		/// </summary>
		/// <param name="type">The <see cref="Enum"/> type data</param>
		/// <exception cref="TypeException"/>
		[JsonConstructor, MainConstructor]
		protected BaseType([NotNull] Enum type) : base(default!)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				if (type.GetType() != typeof(DeviceKind) && type.GetType() != typeof(LinkKind) &&
						type.GetType() != typeof(ObjectKind) && type.GetType() != typeof(PlatformKind))
					throw new Exception($"The given type {Format.ExcValue(type.GetType().Name)} is not valid");
				else
					Data = type;
			}
			catch (Exception ex)
			{
				throw new TypeException(ex, sf);
			}
		}

		/// <inheritdoc cref="BaseData{Enum}.Equals(BaseData{Enum}?)"/>
		/// <exception cref="TypeException"/>
		public bool Equals(BaseType? other)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			bool? Out = null;

			try
			{
				Out = other is not null && EqualityComparer<Enum>.Default.Equals(other);
			}
			catch (Exception ex)
			{
				throw new TypeException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= base.Equals(other);
				}
				catch (Exception) { }
			}

			return Out ?? false;
		}

		/// <inheritdoc cref="BaseData{Enum}.Equals(object?)"/>
		/// <exception cref="TypeException"/>
		public override bool Equals(object? obj)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			bool? Out = null;

			try
			{
				Out = Equals(obj as BaseType);
			}
			catch (TypeException)
			{
				throw;
			}
			catch (BaseException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new TypeException(ex, sf);
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

		/// <inheritdoc cref="BaseData{Enum}.GetHashCode"/>
		/// <exception cref="TypeException"/>
		public override int GetHashCode()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			int? Out = null;

			try
			{
				Out = HashCode.Combine(Data, Data.GetHashCode());
			}
			catch (Exception ex)
			{
				throw new TypeException(ex, sf);
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

		public static bool operator ==(BaseType? left, BaseType? right) => EqualityComparer<BaseType>.Default.Equals(left, right);
		public static bool operator !=(BaseType? left, BaseType? right) => !(left == right);

		public static bool operator ==(BaseType? left, Enum? right) => left is not null && EqualityComparer<Enum>.Default.Equals(left.Data, right);
		public static bool operator !=(BaseType? left, Enum? right) => !(left == right);
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class BaseType<TEnum> : BaseType, IEquatable<BaseType<TEnum>?>, IEquatable<TEnum?> where TEnum : struct, Enum
	{
		/// <summary>
		/// The base <typeparamref name="TEnum"/> data for this datum
		/// </summary>
		[JsonProperty]
		public new TEnum Data { get => (TEnum)base.Data; protected set => base.Data = value; }

		/// <summary>
		/// Primary constructor for the <see cref="BaseType{TEnum}"/> class
		/// </summary>
		/// <param name="type">The <typeparamref name="TEnum"/> type data</param>
		/// <exception cref="TypeException"/>
		[JsonConstructor, MainConstructor]
		public BaseType([NotNull] TEnum type) : base(Def<TEnum>.Value)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				if (type.GetType() != typeof(DeviceKind) && type.GetType() != typeof(LinkKind) &&
						type.GetType() != typeof(ObjectKind) && type.GetType() != typeof(PlatformKind))
					throw new Exception($"The given type {Format.ExcValue(type.GetType().Name)} is not valid");
				else
					Data = type;
			}
			catch (Exception ex)
			{
				throw new TypeException(ex, sf);
			}
		}
		
		public BaseType([NotNull] BaseType<TEnum> type) : base(Def<TEnum>.Value)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				if (type.Data.GetType() != typeof(DeviceKind) && type.Data.GetType() != typeof(LinkKind) &&
						type.Data.GetType() != typeof(ObjectKind) && type.Data.GetType() != typeof(PlatformKind))
					throw new Exception($"The given type {Format.ExcValue(type.GetType().Name)} is not valid");
				else
					Data = type.Data;
			}
			catch (Exception ex)
			{
				throw new TypeException(ex, sf);
			}
		}

		public static implicit operator BaseType<TEnum>([NotNull] TEnum v) => new(v);

		public override bool Equals(object? obj)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			bool? Out = null;

			try
			{
				Out = Equals(obj as BaseType<TEnum>);
			}
			catch (TypeException)
			{
				throw;
			}
			catch (BaseException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new TypeException(ex, sf);
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

		public bool Equals(BaseType<TEnum>? other)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			bool? Out = null;

			try
			{
				Out = other is not null && base.Equals(other) && EqualityComparer<TEnum>.Default.Equals(Data, other.Data);
			}
			catch (TypeException)
			{
				throw;
			}
			catch (BaseException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new TypeException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= base.Equals(other);
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
				Out = HashCode.Combine(Data, base.GetHashCode());
			}
			catch (TypeException)
			{
				throw;
			}
			catch (BaseException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new TypeException(ex, sf);
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

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		/// <see langword="true"/> if the current object is equal to the other parameter; otherwise, <see langword="false"/>.
		/// </returns>
		/// <exception cref="BaseException"/>
		public bool Equals(TEnum? other)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			bool? Out = null;

			try
			{
				Out = other is not null && base.Equals(other) && EqualityComparer<Enum>.Default.Equals(Data, other);
			}
			catch (TypeException)
			{
				throw;
			}
			catch (BaseException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new TypeException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= other is not null && base.Equals(other);
				}
				catch (Exception) { }
			}

			return Out ?? false;
		}

		/// <inheritdoc cref="BaseData{TData}.ToMessagePack(MessagePackSerializerOptions?)"/>
		/// <exception cref="TypeException"/>
		public override byte[] ToMessagePack(MessagePackSerializerOptions? opts = null)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			byte[]? Out = null;

			try
			{
				Out = MessagePackSerializer.Serialize(ulong.Parse($"{Data.GetType().GetHashCode():X8}{Data.GetHashCode():X8}", NumberStyles.HexNumber), opts);
			}
			catch (MessagePackSerializationException ex)
			{
				throw new TypeException(new Exception($"This data could not be serialized into the MessagePack format", ex), sf);
			}
			catch (BaseException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new TypeException(ex, sf);
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

		public static bool operator ==(BaseType<TEnum>? left, BaseType<TEnum>? right) => EqualityComparer<BaseType<TEnum>>.Default.Equals(left, right);
		public static bool operator !=(BaseType<TEnum>? left, BaseType<TEnum>? right) => !(left == right);

		public static bool operator ==(BaseType<TEnum>? left, TEnum? right) => left is not null && EqualityComparer<Enum>.Default.Equals(left.Data, right);
		public static bool operator !=(BaseType<TEnum>? left, TEnum? right) => !(left == right);
	}
}
