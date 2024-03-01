namespace ZData01
{
	using Actions;

	internal class Program
	{
		private static void Main(string[] args)
		{
			Log.Event($"-=-=-=-=-=-=-=-=-= {Def.AppName} HAS STARTED =-=-=-=-=-=-=-=-=-");

			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddRazorPages();
			builder.Services.AddServerSideBlazor();

			builder.Environment.ApplicationName = Def.AppName;

			var app = builder.Build();

			if (!app.Environment.IsDevelopment())
				app.UseHsts();

			app
				.UseHttpsRedirection()
				.UseStaticFiles()
				.UseRouting()
				.UseAuthentication();

			app.Environment.ApplicationName = Def.AppName;

			app.MapBlazorHub();
			app.MapFallbackToPage("/_Host");

			app.Run();
		}
	}
}
