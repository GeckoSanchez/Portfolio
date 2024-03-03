namespace ZData02.Bases
{
	using System.Diagnostics;
	using System.Diagnostics.CodeAnalysis;
	using Actions;
	using Attributes;
	using Categories;
	using Exceptions;
	using Identities;
	using IDs;
	using Names;
	using Newtonsoft.Json;
	using Types;
	using Values;

	[JsonObject(MemberSerialization.OptIn)]
	public class BaseObject : BaseData<ObjectIdentity>, IObject
	{
		[JsonProperty(nameof(Identity))]
		public ObjectIdentity Identity { get => Data; protected init => Data = value; }

		public bool IsAccessible => IsActive && CMoment != null;

		[JsonProperty("Initialization moment", NullValueHandling = NullValueHandling.Include)]
		public Moment IMoment { get; private init; }

		[JsonProperty("Is active?")]
		public bool IsActive { get; protected set; }

		[JsonProperty("Creation moment", NullValueHandling = NullValueHandling.Include)]
		public Moment? CMoment { get; protected set; }

		[JsonProperty("Deletion moment", NullValueHandling = NullValueHandling.Include)]
		public Moment? DMoment { get; }

		/// <summary>
		/// Primary constructor for the <see cref="BaseObject"/> class
		/// </summary>
		/// <param name="identity">The <see cref="ObjectIdentity"/> data</param>
		/// <param name="isactive">Whether or not this object is active</param>
		/// <param name="imoment">The <see cref="Moment"/> at which this object was initialized</param>
		/// <param name="cmoment">The <see cref="Moment"/> at which this object's save file was created</param>
		/// <exception cref="ObjectException"/>
		[JsonConstructor, MainConstructor]
		public BaseObject([NotNull] ObjectIdentity identity, bool isactive, Moment imoment, Moment? cmoment, Moment? dmoment) : base(identity)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				IsActive = isactive;
				IMoment = imoment;
				CMoment = cmoment;
				DMoment = dmoment;
			}
			catch (Exception ex)
			{
				throw new ObjectException(ex, sf);
			}
		}

		public BaseObject([NotNull] ObjectName name, [NotNull] ObjectType type, [NotNull] ObjectID id) : this(new ObjectIdentity(name, type, id), true, Moment.Now, null, null) => Log.Event(new StackFrame(true));
		public BaseObject([NotNull] ObjectName name, [NotNull] ObjectType type) : this(new ObjectIdentity(name, type), true, Moment.Now, null, null) => Log.Event(new StackFrame(true));

		public void Activate()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{

			}
			catch (Exception ex)
			{
				throw new ObjectException(ex, sf);
			}
		}

		public void Deactivate()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{

			}
			catch (Exception ex)
			{
				throw new ObjectException(ex, sf);
			}
		}

		public void Create(bool json = true, bool msgpack = true)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{

			}
			catch (Exception ex)
			{
				throw new ObjectException(ex, sf);
			}
		}

		public void Delete()
		{
			throw new NotImplementedException();
		}
	}
}
