namespace ZData01.Enums
{
	public enum DeviceType : int
	{
		None = 0,
		/// <summary>
		/// (Non)-wireless access point
		/// </summary>
		AccessPoint,
		/// <summary>
		/// Network bridge
		/// </summary>
		Bridge,
		/// <summary>
		/// Any kind of mobile device that is not a <see cref="Tablet"/>
		/// </summary>
		Cellphone,
		/// <summary>
		/// Windows/Mac/Linux/ChromeOS desktop machine
		/// </summary>
		Computer,
		/// <summary>
		/// Network firewall
		/// </summary>
		Firewall,
		/// <summary>
		/// Incoming/outgoing Internet connection
		/// </summary>
		Internet,
		/// <summary>
		/// Any non-desktop computer
		/// </summary>
		Laptop,
		/// <summary>
		/// Network modem
		/// </summary>
		Modem,
		/// <summary>
		/// Printer
		/// </summary>
		Printer,
		/// <summary>
		/// Network repeater
		/// </summary>
		Repeater,
		/// <summary>
		/// Network router
		/// </summary>
		Router,
		/// <summary>
		/// Scanner
		/// </summary>
		Scanner,
		/// <summary>
		/// Displays that are connected to another device, external to it
		/// </summary>
		Screen,
		/// <summary>
		/// Server
		/// </summary>
		Server,
		/// <summary>
		/// Network switch
		/// </summary>
		Switch,
		/// <summary>
		/// Tablet mobile device
		/// </summary>
		Tablet,
	}
}
