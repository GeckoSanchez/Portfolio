namespace ZData01.Bases
{
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Diagnostics;
	using Actions;
	using Attributes;
	using Categories;
	using Enums;
	using Exceptions;
	using Identities;
	using IDs;
	using Newtonsoft.Json;
	using Values;

	[JsonObject(MemberSerialization.OptIn)]
	public class BaseObject : Base<ObjectIdentity>, IObject
	{
		[JsonProperty(Required = Required.Always)]
		public ObjectIdentity Identity => Value;

		[JsonProperty("Initialization moment", Required = Required.Always)]
		public Moment IMoment { get; protected init; }

		public bool IsAccessible => !CanDelete && IsActive;

		[JsonProperty("Is active?", Required = Required.Always)]
		public bool IsActive { get; protected set; }

		[DefaultValue(0L)]
		[JsonProperty("Creation moment", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public Moment CMoment { get; protected set; }
		public bool IsCreated => CMoment.Value != DateTime.MinValue;

		[DefaultValue(0L)]
		[JsonProperty("Modification moments", Required = Required.Always)]
		public ICollection<Moment> EMoments { get; protected set; } = [];

		[DefaultValue(0L)]
		[JsonProperty("Deletion moment", Required = Required.Always)]
		public Moment DelMoment { get; protected set; }
		public bool CanDelete => DelMoment != Moment.MinValue;

		/// <summary>
		/// Primary constructor for the <see cref="BaseObject"/> class
		/// </summary>
		/// <param name="identity">The given identity</param>
		[JsonConstructor, MainConstructor]
		public BaseObject(ObjectIdentity identity, bool isactive, Moment imoment, Moment cmoment, Moment delmoment) : base(identity)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				IsActive = isactive;
				IMoment = imoment;
				CMoment = cmoment;
				DelMoment = delmoment;
			}
			catch (Exception ex)
			{
				throw new ObjectException(ex, sf);
			}
		}

		public BaseObject(BaseName<ObjectType> name, BaseType<ObjectType> type, ObjectID id) : this(new(name, type, id), true, new Moment(), Moment.MinValue, Moment.MinValue) => Log.Event(new StackFrame(true));
		public BaseObject(BaseName<ObjectType> name, BaseType<ObjectType> type) : this(new(name, type, new ObjectID(type)), true, new Moment(), Moment.MinValue, Moment.MinValue) => Log.Event(new StackFrame(true));

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
			throw new NotImplementedException();
		}

		public void Create()
		{
			throw new NotImplementedException();
		}

		public void ChangeName(BaseName<ObjectType> name)
		{
			throw new NotImplementedException();
		}

		public void ChangeType(BaseType<ObjectType> type)
		{
			throw new NotImplementedException();
		}

		public void ChangeType(ObjectType name)
		{
			throw new NotImplementedException();
		}

		public void ChangeID(BaseID<ObjectType> id)
		{
			throw new NotImplementedException();
		}

		public void ChangeID()
		{
			throw new NotImplementedException();
		}

		public void Delete()
		{
			throw new NotImplementedException();
		}
	}
}
