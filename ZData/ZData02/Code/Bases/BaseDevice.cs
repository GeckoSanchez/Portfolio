namespace ZData02.Bases
{
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Diagnostics.CodeAnalysis;
	using Actions;
	using Attributes;
	using Categories;
	using Exceptions;
	using Identities;
	using Newtonsoft.Json;
	using Properties;
	using Values;

	[JsonObject(MemberSerialization.OptIn)]
	[method: JsonConstructor, MainConstructor]
	public class BaseDevice([NotNull] DeviceIdentity data) : BaseData<DeviceIdentity>(data), IDevice, ILinkable
	{
		[JsonProperty(nameof(Identity))]
		public DeviceIdentity Identity => Data;

		public bool IsAccessible => IsAccessible;

		[JsonProperty("Is active?")]
		public bool IsActive { get; private set; }
		public Moment? CMoment { get; }
		public Moment? DMoment { get; }
		public HashSet<Moment>? EMoments { get; }

		public void Activate()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				if (IsActive)
					throw new Exception($"The {Identity} could not be activated, since it is already active");
				else
					IsActive = true;
			}
			catch (Exception ex)
			{
				throw new DeviceException(ex, sf);
			}
		}

		public void Deactivate()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				if (!IsActive)
					throw new Exception($"The {Identity} could not be deactivated, since it is already inactive");
				else
					IsActive = false;
			}
			catch (Exception ex)
			{
				throw new DeviceException(ex, sf);
			}
		}

		public void Create()
		{
			throw new NotImplementedException();
		}

		public void Save()
		{
			throw new NotImplementedException();
		}

		public void ChangeName(string name)
		{
			throw new NotImplementedException();
		}

		public void Delete()
		{
			throw new NotImplementedException();
		}

		public override string ToJSON(Formatting? formatting = null)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{

			}
			catch (Exception ex)
			{
				throw new DeviceException(ex, sf);
			}

			return base.ToJSON(formatting);
		}
	}
}
