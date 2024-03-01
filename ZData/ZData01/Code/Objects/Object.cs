namespace ZData01.Objects
{
	using Bases;
	using Identities;
	using Newtonsoft.Json;
	using Values;

	[JsonObject(MemberSerialization.OptIn)]
	public class Object(ObjectIdentity identity, bool isactive, Moment imoment, Moment cmoment, Moment delmoment) : BaseObject(identity, isactive, imoment, cmoment, delmoment)
	{
	}
}
