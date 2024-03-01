namespace ZData00
{
	using Actions;
	using Microsoft.Extensions.Logging.Console;

	internal class Program
	{
		private static void Main(string[] args)
		{
			Log.Event($"-=-=-=-=-=-=-=-=-= {Def.AppName} HAS STARTED =-=-=-=-=-=-=-=-=-");

			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddRazorPages();
			builder.Services.AddServerSideBlazor();
			builder.Services.AddAuthorization();
			builder.Services.AddDataProtection();
			builder.Services.AddDistributedMemoryCache(e =>
			{
				e.TrackStatistics = true;
			});

			builder.Logging.AddConsole(e =>
			{
				e.MaxQueueLength = 5000;
				e.QueueFullMode = ConsoleLoggerQueueFullMode.DropWrite;
				e.LogToStandardErrorThreshold = LogLevel.Debug;
			}).AddDebug();

			builder.WebHost.CaptureStartupErrors(true);
			builder.WebHost.SuppressStatusMessages(false);

			var app = builder.Build();

			if (!app.Environment.IsDevelopment())
				app.UseHsts();

			app
				.UseHttpsRedirection()
				.UseAuthorization()
				.UseStaticFiles()
				.UseRouting();

			app.MapBlazorHub();
			app.MapFallbackToPage("/_Host");

			app.Run();
		}
	}
}