namespace ZData00.Identities
{
	using System.Diagnostics;
	using Actions;
	using Attributes;
	using Bases;
	using Enums;
	using IDs;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class ObjectIdentity : BaseIdentity<ObjectType>
	{
		[JsonConstructor, MainConstructor]
		public ObjectIdentity(BaseName name, BaseType<ObjectType> type, ObjectID id) : base(name, type, id) => Log.Event(new StackFrame(true));
		public ObjectIdentity(BaseName name, BaseType<ObjectType> type) : this(name, type, ObjectID.Generate(type)) => Log.Event(new StackFrame(true));
		public ObjectIdentity(BaseName name, ObjectType type) : this(name, type, ObjectID.Generate(type)) => Log.Event(new StackFrame(true));
		public ObjectIdentity(string name, ObjectType type) : this(name, type, ObjectID.Generate(type)) => Log.Event(new StackFrame(true));
		public ObjectIdentity(BaseName name) : this(name, Def.OType, ObjectID.Generate(Def.OType)) => Log.Event(new StackFrame(true));
		public ObjectIdentity(string name) : this(new(name), Def.OType, ObjectID.Generate(Def.OType)) => Log.Event(new StackFrame(true));
	}
}
