namespace ZData00.IDs
{
	using System.Diagnostics;
	using Actions;
	using Attributes;
	using Bases;
	using Enums;
	using Exceptions;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class ObjectID : BaseID<ObjectType>
	{
		/// <summary>
		/// Primary constructor for the <see cref="ObjectID"/> class
		/// </summary>
		/// <param name="value">The given value</param>
		/// <exception cref="IDException"/>
		[JsonConstructor, MainConstructor]
		protected ObjectID(ulong value) : base(value) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="ObjectID"/> class
		/// </summary>
		/// <param name="type">The given type</param>
		/// <exception cref="IDException"/>
		public ObjectID(ObjectType type) : base(type) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="ObjectID(ObjectType)"/>
		/// <param name="id">The given sub-ID</param>
		public ObjectID(ObjectType type, ulong id) : base(type, id) => Log.Event(new StackFrame(true));

		public static implicit operator ObjectID(ObjectType v) => new(v);

		/// <inheritdoc cref="BaseID{ObjectType}.Generate(ObjectType)"/>
		public new static ObjectID Generate(ObjectType type)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			ObjectID? Out = null;

			try
			{
				Out = new(type);
			}
			catch (Exception ex)
			{
				throw new IDException(ex, sf);
			}
			finally
			{
				Out ??= Def.OType;
			}

			return Out;
		}

		public new static ObjectID Generate(BaseType<ObjectType> type)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			ObjectID? Out = null;

			try
			{
				Out = new(type.Value);
			}
			catch (Exception ex)
			{
				throw new IDException(ex, sf);
			}
			finally
			{
				Out ??= Def.OType;
			}

			return Out;
		}
	}
}
