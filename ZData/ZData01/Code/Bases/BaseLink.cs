namespace ZData01.Bases
{
	using System.Diagnostics;
	using Actions;
	using Attributes;
	using Exceptions;
	using MessagePack;
	using Newtonsoft.Json;

	/// <summary>
	/// The base class for all link types
	/// </summary>
	/// <typeparam name="TParent">The <see cref="Enums"/> type for the parent data</typeparam>
	/// <typeparam name="TChild">The <see cref="Enums"/> type for the child data</typeparam>
	[JsonObject(MemberSerialization.OptIn)]
	public class BaseLink<TParent, TChild> : Base<Tuple<UInt128, UInt128>>
		where TParent : struct, Enum
		where TChild : struct, Enum
	{
		[JsonProperty(Required = Required.Always)]
		private new Tuple<UInt128, UInt128> Value => base.Value;

		public UInt128 ParentID => Value.Item1;
		public UInt128 ChildID => Value.Item2;

		/// <summary>
		/// Primary constructor for the <see cref="BaseLink{TParent, TChild}"/> class
		/// </summary>
		/// <param name="value">The given <see cref="Tuple{UInt128, UInt128}"/> value</param>
		/// <remarks>
		/// If an exception is encountered, the <see cref="ParentID"/> will be set to <see cref="UInt128.MinValue"/> <![CDATA[&]]> the <see cref="ChildID"/> will be set to <see cref="UInt128.MaxValue"/>
		/// </remarks>
		/// <exception cref="LinkException"/>
		[JsonConstructor, MainConstructor]
		public BaseLink(Tuple<UInt128, UInt128> value) : base(new(UInt128.MinValue, UInt128.MaxValue))
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				if (value.Item1 == value.Item2)
					throw new Exception($"The data #{value.Item1:X32} could not be linked to the data #{value.Item2:X32}, since both of them have the same ID");
				else
					base.Value = value;
			}
			catch (Exception ex)
			{
				throw new LinkException(ex, sf);
			}
		}

		/// <summary>
		/// Constructor for the <see cref="BaseLink{TParent, TChild}"/> class
		/// </summary>
		/// <param name="parent">The parent data's identity</param>
		/// <param name="child">The child data's identity</param>
		/// <exception cref="LinkException"/>
		public BaseLink(BaseIdentity<TParent> parent, BaseIdentity<TChild> child) : this(new(0, 0))
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				if (parent.ID == child.ID)
					throw new Exception($"The {parent} could not be linked to the {child}, since both of them have the same ID");
				else if (parent.Name == child.Name && parent.Type == child.Type)
					throw new Exception($"The {parent} could not be linked to the {child}, since both of them have the same name {parent.Name} and type {parent.Type}");
				else
					base.Value = new(parent.Value.Value, child.Value.Value);
			}
			catch (Exception ex)
			{
				throw new LinkException(ex, sf);
			}
		}

		/// <inheritdoc cref="Base{T}.ToString"/>
		/// <exception cref="LinkException"/>
		public override string ToString()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			string? Out = null;

			try
			{
				Out = $"#{ParentID:X32}-=->#{ChildID:X32}";
			}
			catch (Exception ex)
			{
				throw new LinkException(ex, sf);
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

		public override string ToJSON(Formatting formatting = Formatting.Indented)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			string? Out = null;

			try
			{
				Out = JsonConvert.SerializeObject(this, formatting);
			}
			catch (Exception ex)
			{
				throw new LinkException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= base.ToJSON(formatting);
				}
				catch (Exception)
				{
					Out = Def.JSON;
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
				Out = MessagePackSerializer.Serialize(ToString(), opts);
			}
			catch (BaseException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new LinkException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= base.ToMessagePack(opts);
				}
				catch (Exception)
				{
					Out = Def.MsgPack;
				}
			}

			return Out;
		}
	}
}
