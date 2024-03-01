namespace ZData00.Devices
{
	using System.Diagnostics;
	using System.Net.Http.Headers;
	using Actions;
	using Attributes;
	using Bases;
	using Enums;
	using Exceptions;
	using Identities;
	using IDs;
	using Newtonsoft.Json;
	using Values;

	[JsonObject(MemberSerialization.OptIn)]
	public class Cellphone : BaseDevice
	{
		[JsonProperty]
		public string PhoneNumber { get; private init; }

		/// <summary>
		/// Primary constructor for the <see cref="Cellphone"/> class
		/// </summary>
		/// <param name="name">The given name</param>
		/// <param name="identity">The given <see cref="DeviceIdentity"/></param>
		/// <param name="addr">The given MAC address</param>
		/// <param name="phoneNumber">
		/// The given phone number
		/// (<c>This phone number is filtered in order to remove all but the digits (0-9) found in the given string</c>)
		/// </param>
		/// <exception cref="DeviceException"/>
		[JsonConstructor, MainConstructor]
		public Cellphone(DeviceIdentity identity, MACAddress addr, string phoneNumber) : base(identity, addr)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				HttpClient client = new() { BaseAddress = new Uri("https://phonevalidation.abstractapi.com/v1/") };
				HttpResponseMessage response;

				string phone = "";

				foreach (var i in phoneNumber.Where(char.IsAsciiDigit))
					phone += i;

				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				response = client.GetAsync($"?api_key=efe9fa564e9d4ac3a4ceb7a9c52a02df&phone={phone[..10]}").Result;

				string res = response.Content.ReadAsStringAsync().Result;

				if (!res.Contains(@"""valid"":true,", StringComparison.CurrentCultureIgnoreCase))
					throw new Exception($"The given phone number {Format.ExcValue(phoneNumber)} is not valid, according to the Abstract Phone API");
				else
					PhoneNumber = phoneNumber;
			}
			catch (Exception ex)
			{
				throw new DeviceException(ex, sf);
			}
			finally
			{
				PhoneNumber ??= "5145550000";
			}
		}

		/// <summary>
		/// Constructor for the <see cref="Cellphone"/> class
		/// </summary>
		/// <inheritdoc cref="Cellphone(DeviceIdentity, MACAddress, string)"/>
		/// <param name="name">The given name</param>
		/// <param name="id">The given ID</param>
		public Cellphone(BaseName name, DeviceID id, string phoneNumber) : this(new DeviceIdentity(name, DeviceType.Cellphone, id), new(), phoneNumber) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Cellphone(BaseName, DeviceID, string)"/>
		public Cellphone(BaseName name, string phoneNumber) : this(new DeviceIdentity(name, DeviceType.Cellphone), new MACAddress(), phoneNumber) => Log.Event(new StackFrame(true));
	}
}
