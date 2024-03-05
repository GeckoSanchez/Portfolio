namespace ZData02.Bases
{
	using System.Diagnostics;
	using System.Diagnostics.CodeAnalysis;
	using Actions;
	using Attributes;
	using Categories;
	using Enums;
	using Exceptions;
	using Identities;
	using Newtonsoft.Json;
	using Properties;

	[JsonObject(MemberSerialization.OptIn, ItemReferenceLoopHandling = ReferenceLoopHandling.Ignore, ItemNullValueHandling = NullValueHandling.Include)]
	public class BaseLink<TObject> : BaseData<LinkIdentity>, ILink where TObject : ILinkable
	{
		[JsonProperty(nameof(Identity))]
		public LinkIdentity Identity { get => Data; protected init => Data = value; }

		[JsonProperty]
		public UInt128 ParentID { get; protected init; }

		[JsonProperty]
		public UInt128 ChildID { get; protected init; }

		/// <summary>
		/// Primary constructor for the <see cref="BaseLink{TObject}"/> class
		/// </summary>
		/// <param name="identity">The identity information for this link</param>
		/// <param name="parent">The ID of the parent element</param>
		/// <param name="child">The ID of the child element</param>
		/// <exception cref="LinkException"/>
		[JsonConstructor, MainConstructor]
		protected BaseLink([NotNull] LinkIdentity identity, [NotNull] UInt128 parent, [NotNull] UInt128 child) : base(identity)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				if (parent == child)
					throw new Exception($"An element cannot be linked to itself");
				else
				{
					var val = identity.Validate();

					// ADD VERIFICATIONS FOR IF THE GIVEN IDs CAN BE FOND IN THE CONTEXT !!! //

					if (val.Any())
						throw new Exception(string.Join(", ", val.Where(e => e.ErrorMessage != null).Select(e => e.ErrorMessage)));
					else
					{
						ParentID = parent;
						ChildID = child;
					}
				}
			}
			catch (Exception ex)
			{
				throw new LinkException(ex, sf);
			}
		}

		/// <summary>
		/// Constructor for the <see cref="BaseLink{TObject}"/> class
		/// </summary>
		/// <inheritdoc cref="BaseLink{TObject}(LinkIdentity,UInt128,UInt128)"/>
		public BaseLink(LinkIdentity identity, TObject parent, TObject child) : this(identity, 0, 1)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				switch (parent.Identity.Type.Data)
				{
					case ObjectKind.Database:
						if (child.Identity.Type.Data != ObjectKind.Table)
							throw new Exception($"A database type object can only be linked to a table type object");
						break;
					case ObjectKind.Table:
						if (child.Identity.Type.Data != ObjectKind.Field)
							throw new Exception($"A table type object can only be linked to a table type field");
						break;
					case ObjectKind.Object:
						break;
					case ObjectKind.User:
					case ObjectKind.Field:
					case ObjectKind.None:
					default:
						throw new Exception($"A {parent.Identity.Type.Data} type object cannot be linked to objects of any type");
				}

				ParentID = parent.Identity.Data.Data;
				ChildID = child.Identity.Data.Data;
			}
			catch (Exception ex)
			{
				throw new LinkException(ex, sf);
			}
		}
	}
}
