namespace ZData02.Bases
{
	using System.Diagnostics.CodeAnalysis;
	using Identities;
	using Newtonsoft.Json;

	public class BaseLink([NotNull] LinkIdentity data) : BaseData<LinkIdentity>(data)
	{
		[JsonProperty(nameof(Identity))]
		public LinkIdentity Identity { get => Data; protected init => Data = value; }
	}
}
