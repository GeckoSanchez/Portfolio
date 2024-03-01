namespace ZData01.Bases
{
	using System.Collections.Generic;
	using System.Diagnostics;
	using Actions;
	using Attributes;
	using Categories;
	using Exceptions;
	using Identities;
	using MessagePack;
	using Newtonsoft.Json;

	/// <summary>
	/// Basic structure for an Identity
	/// </summary>
	/// <typeparam name="TEnum">The underlying type for this Identity</typeparam>
	[JsonObject(MemberSerialization.OptIn)]
	public class BaseIdentity<TEnum> : Base<BaseID<TEnum>>, IIdentity<TEnum> where TEnum : struct, Enum
	{
		[JsonProperty]
		public BaseName<TEnum> Name { get; protected set; }

		[JsonProperty]
		public BaseType<TEnum> Type { get; protected set; }

		[JsonProperty(Required = Required.Always)]
		public BaseID<TEnum> ID { get => Value; protected set => Value = value; }

		/// <summary>
		/// Primary constructor for the <see cref="BaseIdentity{TEnum}"/> class
		/// </summary>
		/// <param name="name">The given name</param>
		/// <param name="type">The given <typeparamref name="TEnum"/> type</param>
		/// <param name="id">The given ID</param>
		/// <exception cref="IdentityException"/>
		[JsonConstructor, MainConstructor]
		public BaseIdentity(BaseName<TEnum> name, BaseType<TEnum> type, BaseID<TEnum> id) : base(new(0))
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				Name = name;
				Type = type;
				ID = id;
			}
			catch (Exception ex)
			{
				throw new IdentityException(ex, sf);
			}
		}

		/// <inheritdoc cref="BaseIdentity{TEnum}(BaseName{TEnum}, BaseType{TEnum}, BaseID{TEnum})"/>
		public BaseIdentity(BaseName<TEnum> name, BaseType<TEnum> type, BaseID id) : this(new BaseName<TEnum>(name), new BaseType<TEnum>(type), new BaseID<TEnum>(id))
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				Name = name;
				Type = type;
				ID = new(id);
			}
			catch (Exception ex)
			{
				throw new IdentityException(ex, sf);
			}
		}

		/// <inheritdoc cref="BaseIdentity{TEnum}(BaseName{TEnum}, BaseType{TEnum}, BaseID{TEnum})"/>
		public BaseIdentity(BaseName<TEnum> name, BaseType type, BaseID<TEnum> id) : this(new BaseName<TEnum>(name), new BaseType<TEnum>(type), new BaseID<TEnum>(id))
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				Name = new(name);
				Type = new(type);
				ID = id;
			}
			catch (Exception ex)
			{
				throw new IdentityException(ex, sf);
			}
		}

		/// <inheritdoc cref="BaseIdentity{TEnum}(BaseName{TEnum}, BaseType{TEnum}, BaseID{TEnum})"/>
		public BaseIdentity(BaseName<TEnum> name, BaseType type, BaseID id) : this(new BaseName<TEnum>(name), new BaseType<TEnum>(type), new BaseID<TEnum>(id))
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				Name = name;
				Type = new(type);
				ID = new(id);
			}
			catch (Exception ex)
			{
				throw new IdentityException(ex, sf);
			}
		}

		public BaseIdentity(ObjectIdentity value) : this(new(value.Name.Value), new BaseType<TEnum>(value.Type), new BaseID<TEnum>(value.ID)) => Log.Event(new StackFrame(true));

		protected override void Dispose(bool disposing)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				Name.Dispose();
				Type.Dispose();
				ID.Dispose();
				base.Dispose(disposing);
			}
			catch (BaseException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new IdentityException(ex, sf);
			}
		}

		/// <inheritdoc cref="Base{T}.ToMessagePack(MessagePackSerializerOptions?)"/>
		/// <exception cref="IdentityException"/>
		public override byte[] ToMessagePack(MessagePackSerializerOptions? opts = null)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			byte[] Out = Def.MsgPack;

			try
			{
				Out = Name.ToMessagePack();
				Out = [.. Out, .. Type.ToMessagePack()];
				Out = [.. Out, .. ID.ToMessagePack()];
			}
			catch (MessagePackSerializationException)
			{
				throw new IdentityException($"This data could not be serialized into the MessagePack format", sf);
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
					if (Out.Length == 0)
						Out = base.ToMessagePack();
				}
				catch (BaseException) { }
			}

			return Out;
		}

		/// <inheritdoc cref="Base{T}.ToJSON(Formatting)"/>
		/// <exception cref="IdentityException"/>
		public override string ToJSON(Formatting formatting = Formatting.Indented)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			string? Out = null;

			try
			{
				Out = JsonConvert.SerializeObject(this, formatting);
			}
			catch (JsonException)
			{
				throw new IdentityException($"This data could not be serialized into the JSON format", sf);
			}
			catch (Exception ex)
			{
				throw new IdentityException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= base.ToJSON(formatting);
				}
				catch (BaseException)
				{
					Out = Def.JSON;
				}
			}

			return Out;
		}

		/// <inheritdoc cref="Base{T}.ToString"/>
		/// <exception cref="IdentityException"/>
		public override string ToString()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			string? Out = null;

			try
			{
				Out = $"{Name} {Type.ToString().ToLower()} (#{ID})";
			}
			catch (JsonException)
			{
				throw new IdentityException($"This data could not be serialized into the JSON format", sf);
			}
			catch (IDException)
			{
				throw;
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
					Out ??= base.ToString();
				}
				catch (BaseException)
				{
					Out = Def.JSON;
				}
			}

			return Out;
		}

		/// <inheritdoc cref="Base{BaseID{TEnum}}.Equals(object?)"/>
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
			catch (IdentityException)
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
					Out ??= base.Equals(obj);
				}
				catch (BaseException) { }
			}

			return Out ?? false;
		}

		/// <inheritdoc cref="Base{BaseID{TEnum}}.Equals(Base{BaseID{TEnum}}?)"/>
		/// <exception cref="IdentityException"/>
		public bool Equals(BaseIdentity<TEnum>? other)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			bool? Out = null;

			try
			{
				Out = other is not null && base.Equals(other) &&
						 EqualityComparer<BaseID<TEnum>>.Default.Equals(Value, other.Value) &&
						 EqualityComparer<BaseName<TEnum>>.Default.Equals(Name, other.Name) &&
						 EqualityComparer<BaseType<TEnum>>.Default.Equals(Type, other.Type);
			}
			catch (Exception ex)
			{
				throw new IdentityException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= base.Equals(other);
				}
				catch (BaseException) { }
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
				Out = HashCode.Combine(base.GetHashCode(), Value, Name, Type);
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

		public static bool operator >(BaseIdentity<TEnum> left, BaseIdentity<TEnum> right) => left.ID.Value > right.ID.Value;
		public static bool operator >=(BaseIdentity<TEnum> left, BaseIdentity<TEnum> right) => left.ID.Value >= right.ID.Value;
		public static bool operator <(BaseIdentity<TEnum> left, BaseIdentity<TEnum> right) => left.ID.Value < right.ID.Value;
		public static bool operator <=(BaseIdentity<TEnum> left, BaseIdentity<TEnum> right) => left.ID.Value <= right.ID.Value;
		public static bool operator ==(BaseIdentity<TEnum>? left, BaseIdentity<TEnum>? right) => (left is null && left is null) || (left is not null && right is not null && EqualityComparer<BaseID<TEnum>>.Default.Equals(left.Value, right.Value));
		public static bool operator !=(BaseIdentity<TEnum>? left, BaseIdentity<TEnum>? right) => !(left == right);
	}
}
