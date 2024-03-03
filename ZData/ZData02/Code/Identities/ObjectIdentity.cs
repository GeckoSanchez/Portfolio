namespace ZData02.Identities
{
	using System.Diagnostics;
	using System.Diagnostics.CodeAnalysis;
	using Actions;
	using Attributes;
	using Bases;
	using Enums;
	using Exceptions;
	using Names;
	using IDs;
	using Newtonsoft.Json;
	using Types;

	[JsonObject(MemberSerialization.OptIn)]
	public class ObjectIdentity : BaseIdentity<ObjectKind>
	{
		/// <summary>
		/// Primary constructor for the <see cref="ObjectIdentity"/> class
		/// </summary>
		/// <param name="name">The <see cref="ObjectName"/> name</param>
		/// <param name="type">The <see cref="ObjectType"/> type</param>
		/// <param name="id">The <see cref="ObjectID"/> ID</param>
		/// <exception cref="IdentityException"/>
		[JsonConstructor, MainConstructor]
		public ObjectIdentity([NotNull] ObjectName name, [NotNull] ObjectType type, [NotNull] ObjectID id) : base(name, type, new(id)) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="ObjectIdentity"/> class
		/// </summary>
		/// <inheritdoc cref="ObjectIdentity(ObjectName,ObjectType,ObjectID)"/>
		public ObjectIdentity([NotNull] ObjectName name, [NotNull] ObjectKind type) : base(name, type) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="ObjectIdentity(ObjectName,ObjectKind)"/>
		public ObjectIdentity([NotNull] ObjectName name) : base(name) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="ObjectIdentity(ObjectName,ObjectKind)"/>
		public ObjectIdentity([NotNull] ObjectName name, [NotNull] ObjectType type) : base(name, type) => Log.Event(new StackFrame(true));
	}
}
