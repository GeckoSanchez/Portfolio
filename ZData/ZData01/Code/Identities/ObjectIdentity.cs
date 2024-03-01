namespace ZData01.Identities
{
	using System.Diagnostics;
	using Actions;
	using Attributes;
	using Bases;
	using Enums;
	using Exceptions;
	using IDs;
	using Newtonsoft.Json;

	/// <summary>
	/// The Identity for any class in the <see cref="ZData01"/>.<see cref="Objects"/> namespace
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public class ObjectIdentity : BaseIdentity<ObjectType>
	{
		/// <summary>
		/// Primary constructor for the <see cref="ObjectIdentity"/> class
		/// </summary>
		/// <param name="name">The given name</param>
		/// <param name="type">The given <see cref="ObjectType"/></param>
		/// <param name="id">The given <see cref="ObjectID"/></param>
		/// <exception cref="IdentityException"/>
		[JsonConstructor, MainConstructor]
		public ObjectIdentity(BaseName<ObjectType> name, BaseType<ObjectType> type, BaseID<ObjectType> id) : base(name, type, id) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="ObjectIdentity"/>
		/// </summary>
		/// <inheritdoc cref="ObjectIdentity(BaseName{ObjectType}, BaseType{ObjectType}, BaseID{ObjectType})"/>
		public ObjectIdentity(BaseName<ObjectType> name, BaseType<ObjectType> type, BaseID id) : this(new(name), new(type), new(id)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="ObjectIdentity(BaseName{ObjectType}, BaseType{ObjectType}, BaseID)"/>
		public ObjectIdentity(BaseName<ObjectType> name, BaseType type, BaseID<ObjectType> id) : this(new(name), new(type), new(id)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="ObjectIdentity(BaseName{ObjectType}, BaseType{ObjectType}, BaseID)"/>
		public ObjectIdentity(BaseName<ObjectType> name, BaseType type, BaseID id) : this(new(name), new(type), new(id)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="ObjectIdentity(BaseName{ObjectType}, BaseType{ObjectType}, BaseID)"/>
		public ObjectIdentity(BaseName<ObjectType> name, BaseType<ObjectType> type) : base(name, type, new BaseID<ObjectType>(type.Value)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="ObjectIdentity(BaseName{ObjectType}, BaseType{ObjectType}, BaseID)"/>
		public ObjectIdentity(BaseName<ObjectType> name, BaseType type) : this(new(name), new(type), new BaseID<ObjectType>((ObjectType)type.Value)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="ObjectIdentity(BaseName{ObjectType}, BaseType{ObjectType}, BaseID)"/>
		public ObjectIdentity(BaseName<ObjectType> name) : this(new(name), new(Def.OType), new BaseID<ObjectType>(Def.OType)) => Log.Event(new StackFrame(true));
	}
}
