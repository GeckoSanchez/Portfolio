namespace ZData02
{
	using System.Diagnostics;
	using Actions;
	using Bases;
	using Exceptions;
	using Identities;

	public static class Context
	{
		public static HashSet<BaseObject> BaseObjects { get; set; } = [];

		/// <summary>
		/// Function to add a new element to the <see cref="Context"/> for the application
		/// </summary>
		/// <param name="elem">The element to be attempted to be added to the <see cref="Context"/></param>
		/// <exception cref="ContextException"/>
		public static void Add(BaseObject elem)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			if (!BaseObjects.Add(elem))
				throw new ContextException($"The newest element {Format<ObjectIdentity>.ExcValue(elem.Identity)} could not be added to the {nameof(Context)}, since it had already been added before", sf);
		}
	}
}
