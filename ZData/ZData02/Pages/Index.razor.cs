namespace ZData02.Pages
{
	using System.Diagnostics;
	using Actions;
	using Exceptions;

	public partial class Index
	{
		public Index()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{

			}
			catch (Exception ex)
			{
				Log.Exception(new PageException(ex, sf));
			}
		}
	}
}