namespace ZData02.Objects
{
	using System.Diagnostics;
	using System.Diagnostics.CodeAnalysis;
	using Actions;
	using Attributes;
	using Bases;
	using Enums;
	using Identities;
	using IDs;
	using Names;
	using Newtonsoft.Json;
	using Values;

	[JsonObject(MemberSerialization.OptIn)]
	public class Object : BaseObject
	{
		[JsonConstructor, MainConstructor]
		public Object([NotNull] ObjectIdentity identity, bool isactive, Moment imoment, Moment? cmoment, Moment? dmoment) : base(new ObjectIdentity(identity.Name.Data, ObjectKind.Object, new(identity.Data.Data)), isactive, imoment, cmoment, dmoment) => Log.Event(new StackFrame(true));

		public Object([NotNull] ObjectName name, [NotNull] ObjectID id) : this(new(name, ObjectKind.Object, id), true, Moment.Now, null, null) => Log.Event(new StackFrame(true));

		public Object([NotNull] ObjectName name) : this(new(name, ObjectKind.Object), true, Moment.Now, null, null) => Log.Event(new StackFrame(true));
	}
}
