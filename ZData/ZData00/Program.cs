namespace ZData00
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddRazorPages();
			builder.Services.AddServerSideBlazor();

			var app = builder.Build();

			if (!app.Environment.IsDevelopment())
				app.UseHsts();

			app.UseHttpsRedirection();

			app.UseStaticFiles();

			app.UseRouting();

			app.MapBlazorHub();
			app.MapFallbackToPage("/_Host");

			app.Run();
		}
	}
}