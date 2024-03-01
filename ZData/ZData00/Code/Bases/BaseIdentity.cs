namespace ZData00.Bases
{
	using System.Diagnostics;
	using Actions;
	using Attributes;
	using Exceptions;
	using MessagePack;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class BaseIdentity<TEnum> : Base<BaseID<TEnum>> where TEnum : struct, Enum
	{
		[JsonProperty]
		public BaseName Name { get; protected init; }
		[JsonProperty]
		public BaseType<TEnum> Type { get; protected init; }
		[JsonProperty]
		public BaseID<TEnum> ID => Value;

		/// <summary>
		/// Primary constructor for the <see cref="BaseIdentity{TEnum}"/> class
		/// </summary>
		/// <param name="name">The given name</param>
		/// <param name="type">The given type</param>
		/// <param name="id">The given ID</param>
		/// <exception cref="IdentityException"/>
		[JsonConstructor, MainConstructor]
		public BaseIdentity(BaseName name, BaseType<TEnum> type, BaseID<TEnum> id) : base(id)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				Name = name;
				Type = type;
			}
			catch (Exception ex)
			{
				throw new IdentityException(ex, sf);
			}
		}

		/// <summary>
		/// Constructor for the <see cref="BaseIdentity{TEnum}"/> class
		/// </summary>
		/// <inheritdoc cref="BaseIdentity{TEnum}.BaseIdentity(BaseName, BaseType{TEnum}, BaseID{TEnum})"/>
		/// <exception cref="IdentityException"/>
		public BaseIdentity(BaseName name, BaseType<TEnum> type) : this(name, type, BaseID<TEnum>.Generate(type)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="BaseIdentity{TEnum}.BaseIdentity(BaseName, BaseType{TEnum})"/>
		public BaseIdentity(BaseName name, TEnum type) : this(name, type,BaseID<TEnum>.Generate(type)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="BaseIdentity{TEnum}.BaseIdentity(BaseName, BaseType{TEnum})"/>
		public BaseIdentity(string name, TEnum type) : this(name, type,BaseID<TEnum>.Generate(type)) => Log.Event(new StackFrame(true));

		public override string ToString()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			string? Out = null;

			try
			{
				Out = $"{Name} {Type.ToString().ToLower()} (#{ID})";
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
					Out = "";
				}
			}

			return Out;
		}

		public override byte[] ToMessagePack(MessagePackSerializerOptions? opts = null)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			byte[]? Out = null;

			try
			{
				Out = Name.ToMessagePack(opts);
				Out = [.. Out, .. Type.ToMessagePack(opts)];
				Out = [.. Out, .. ID.ToMessagePack(opts)];
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
				catch (BaseException)
				{
					Out = Def.MsgPack;
				}
			}

			return Out;
		}

		/// <exception cref="IdentityException"/>
		protected override void Dispose(bool disposing)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				Name.Dispose();
				Type.Dispose();
				ID.Dispose();
			}
			catch (Exception ex)
			{
				throw new IdentityException(ex, sf);
			}
			finally
			{
				base.Dispose(disposing);
			}
		}
	}
}
