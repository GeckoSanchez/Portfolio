namespace ZData02.Categories
{
	using System;
	using ZData02.Identities;

	public interface ILink
	{
		LinkIdentity Identity { get; }
		UInt128 ParentID { get; }
		UInt128 ChildID { get; }
	}
}
