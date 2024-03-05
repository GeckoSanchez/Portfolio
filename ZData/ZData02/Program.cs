namespace ZData02
{
	using Actions;
	using Enums;

	internal class Program
	{
		private static void Main(string[] args)
		{
			Log.Event($"-=-=-=-=-=-=-=-= {Def.AppName} HAS BOOTED =-=-=-=-=-=-=-=-", BlockKind.None);
			
			var builder = WebApplication.CreateSlimBuilder(args);
			//= WebApplication.CreateBuilder(args);
			builder.Services.AddRazorPages();
			builder.Services.AddServerSideBlazor();
			builder.WebHost.UseKestrelHttpsConfiguration();

			var app = builder.Build();

			if (!app.Environment.IsDevelopment())
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.MapBlazorHub();
			app.MapFallbackToPage("/_Host");

			app.Run();
		}
	}
}