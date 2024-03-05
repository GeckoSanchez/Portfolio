namespace ZData02.Bases
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Diagnostics;
	using System.Diagnostics.CodeAnalysis;
	using Actions;
	using Attributes;
	using Categories;
	using Enums;
	using Exceptions;
	using Identities;
	using IDs;
	using MessagePack;
	using Names;
	using Newtonsoft.Json;
	using Properties;
	using Types;
	using Values;

	[JsonObject(MemberSerialization.OptIn)]
	public class BaseObject : BaseData<ObjectIdentity>, IObject, IValidatableObject
	{
		[JsonProperty(nameof(Identity))]
		public ObjectIdentity Identity { get => Data; protected init => Data = value; }

		public bool IsAccessible => IsActive && CMoment is not null && (DMoment ?? Moment.MaxValue) > DateTime.Now;

		[JsonProperty("Initialization moment", NullValueHandling = NullValueHandling.Include)]
		public Moment IMoment { get; private init; }

		[JsonProperty("Is active?")]
		public bool IsActive { get; protected set; }

		[JsonProperty("Creation moment", NullValueHandling = NullValueHandling.Include)]
		public Moment? CMoment { get; protected set; }

		[JsonProperty("Modification moments", NullValueHandling = NullValueHandling.Include)]
		public HashSet<Moment> EMoments { get; protected set; } = [];

		[JsonProperty("Deletion moment", NullValueHandling = NullValueHandling.Include)]
		public Moment? DMoment { get; protected set; }

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

		/// <inheritdoc cref="IActivatable.Activate"/>
		/// <exception cref="ObjectException"/>
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
				throw new ObjectException(ex, sf);
			}
		}

		/// <inheritdoc cref="IActivatable.Deactivate"/>
		/// <exception cref="ObjectException"/>
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
				throw new ObjectException(ex, sf);
			}
		}

		/// <inheritdoc cref="ICreatable.Create(SaveFileKind)"/>
		/// <exception cref="ObjectException"/>
		public void Create()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			FileStream? fs = null;
			StreamWriter? sw = null;
			BinaryWriter? bw = null;

			try
			{
				if (CMoment is not null)
					throw new Exception($"The save file for the {Identity} could not be created, since it has already been created");
				else
					CMoment = Moment.Now;

				string txt = ToJSON();
				fs = new(Get.FullPath(this), FileMode.CreateNew, FileAccess.Write);

				using (sw = new(fs, Def.Encoding) { AutoFlush = true })
					sw.Write(txt);
			}
			catch (Exception ex)
			{
				throw new ObjectException(ex, sf);
			}
			finally
			{
				sw?.Dispose();
				bw?.Dispose();
				fs?.Dispose();
			}
		}

		/// <inheritdoc cref="ISavable.Save"/>
		/// <exception cref="ObjectException"/>
		public void Save()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			FileStream? fs = null;
			StreamWriter? sw = null;
			BinaryWriter? bw = null;

			try
			{
				if (CMoment is null)
					throw new Exception($"The save file for the {Identity} could not be updated, since it has not yet been created");
				else
				{
					string txt = ToJSON();
					fs = new(Get.FullPath(this), FileMode.Open, FileAccess.Write);

					using (sw = new(fs, Def.Encoding) { AutoFlush = true })
						sw.Write(txt);
				}
			}
			catch (BaseException)
			{
				throw;
			}
			catch (FileNotFoundException)
			{
				throw new ObjectException($"The data for the {Identity} could not be saved, since the save file does not yet exist", sf);
			}
			catch (IOException ex)
			{
				throw new ObjectException(new Exception($"The data for the {Identity} could not be saved", ex), sf);
			}
			catch (Exception ex)
			{
				throw new ObjectException(ex, sf);
			}
			finally
			{
				sw?.Dispose();
				bw?.Dispose();
				fs?.Dispose();
			}
		}

		/// <inheritdoc cref="IEditable.ChangeName(string)"/>
		/// <exception cref="ObjectException"/>
		public void ChangeName(string name)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				Data = new ObjectIdentity(name, Data.Type.Data, new(Data.Data.Data));
				EMoments.Add(Moment.Now);
				Save();
			}
			catch (Exception ex)
			{
				throw new ObjectException(ex, sf);
			}
		}

		/// <inheritdoc cref="IDeletable.Delete"/>
		/// <exception cref="ObjectException"/>
		public void Delete()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				string path = Get.FullPath(this);

				File.Delete(path);
			}
			catch (Exception ex) when (ex is not PathTooLongException || ex is not IOException)
			{
				throw new ObjectException(ex, sf);
			}
		}

		/// <inheritdoc cref="BaseData{TData}.ToMessagePack(MessagePackSerializerOptions?)"/>
		/// <exception cref="ObjectException"/>
		public override byte[] ToMessagePack(MessagePackSerializerOptions? opts = null)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			byte[]? Out = null;

			try
			{
				Out = Identity.ToMessagePack(opts);
				Out = [.. Out, .. IMoment.ToMessagePack(opts)];
				Out = [.. Out, .. MessagePackSerializer.Serialize(IsActive, opts)];

				if (CMoment is not null)
				{
					Out = [.. Out, .. CMoment.ToMessagePack(opts)];
				}

				if (EMoments != null)
				{
					foreach (var i in EMoments)
						Out = [.. Out, .. i.ToMessagePack()];
				}

				if (DMoment is not null)
					Out = [.. Out, .. DMoment.ToMessagePack(opts)];
			}
			catch (Exception ex)
			{
				throw new ObjectException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= base.ToMessagePack(opts);
				}
				catch (Exception)
				{
					Out = [];
				}
			}

			return Out;
		}

		public override IEnumerable<ValidationResult> Validate(ValidationContext? validationContext)
		{
			Log.Event(new StackFrame(true));

			if (string.IsNullOrEmpty(Identity.Name.Data))
				yield return new ValidationResult($"This object's name cannot be left empty", [nameof(Identity.Name)]);

			var vals = Identity.Validate();

			if (vals.Any())
			{
				yield return new ValidationResult(string.Join(" | ", vals.Where(e => e.ErrorMessage != null).Select(e => e.ErrorMessage)));
			}

			if (CMoment is not null)
			{
				vals = CMoment.Validate();
				yield return new ValidationResult(string.Join(" | ", vals.Where(e => e.ErrorMessage != null).Select(e => e.ErrorMessage)));
			}
		}
	}
}
