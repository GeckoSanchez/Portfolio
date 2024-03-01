namespace ZData01.Components
{
	using System.Diagnostics;
	using System.Threading.Tasks;
	using Actions;
	using Bases;
	using Enums;
	using Exceptions;
	using Microsoft.AspNetCore.Components;

	public partial class Header
	{
		[Parameter]
		public PageType PageType { get; set; }

		[Parameter]
		public ObjectType? ObjectType { get; set; }

		[Parameter]
		public BaseID? ID { get; set; }

		private string Title { get; set; }

		public Header()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);
			Title = "HEADER_TITLE";
		}

		public override string ToString()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);
			return NavMgr.Uri;
		}

		protected override void OnInitialized()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				base.OnInitialized();
			}
			catch (Exception ex)
			{
				throw new PageException(ex, sf);
			}
		}

		protected override Task OnInitializedAsync()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			Task Out;

			try
			{

			}
			catch (Exception ex)
			{
				throw new PageException(ex, sf);
			}
			finally
			{
				Out = base.OnInitializedAsync();
			}

			return Out;
		}

		protected override void OnParametersSet()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				if (ObjectType != null)
				{
					switch (PageType)
					{
						case PageType.Home:
							break;
						case PageType.Exit:
							break;
						case PageType.Auth:
							break;
						case PageType.Login:
							break;
						case PageType.Logout:
							break;
						case PageType.Register:
							break;
						case PageType.Account:
							break;
						case PageType.Index:
							break;
						case PageType.Create:
							break;
						case PageType.Update:
							break;
						case PageType.Details:
							break;
						case PageType.Delete:
							break;
						case PageType.Admin:
							break;
						case PageType.Exception:
							break;
						case PageType.Unknown:
							break;
						case PageType.NotFound:
							break;
						default:
							break;
					}
				}
				else
				{
					switch (PageType)
					{
						case PageType.Home:
							break;
						case PageType.Exit:
							break;
						case PageType.Auth:
							break;
						case PageType.Login:
							break;
						case PageType.Logout:
							break;
						case PageType.Register:
							break;
						case PageType.Account:
							break;
						case PageType.Index:
							break;
						case PageType.Create:
							break;
						case PageType.Update:
							break;
						case PageType.Details:
							break;
						case PageType.Delete:
							break;
						case PageType.Admin:
							break;
						case PageType.Exception:
							break;
						case PageType.Unknown:
							break;
						case PageType.NotFound:
							break;
						default:
							break;
					}
				}

				base.OnParametersSet();
			}
			catch (Exception ex)
			{
				throw new PageException(ex, sf);
			}
			finally
			{
				if (Title == "")
					Title = "<INSERT_TITLE_HERE>";
			}
		}

		protected override Task OnParametersSetAsync()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			Task Out;

			try
			{

			}
			catch (Exception ex)
			{
				throw new PageException(ex, sf);
			}
			finally
			{
				Out = base.OnParametersSetAsync();
			}

			return Out;
		}

		protected override bool ShouldRender()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			bool Out;

			try
			{

			}
			catch (Exception ex)
			{
				throw new PageException(ex, sf);
			}
			finally
			{
				Out = base.ShouldRender();
			}

			return Out;
		}

		protected override void OnAfterRender(bool firstRender)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				base.OnAfterRender(firstRender);
			}
			catch (Exception ex)
			{
				throw new PageException(ex, sf);
			}
		}

		protected override Task OnAfterRenderAsync(bool firstRender)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			Task Out;

			try
			{

			}
			catch (Exception ex)
			{
				throw new PageException(ex, sf);
			}
			finally
			{
				Out = base.OnAfterRenderAsync(firstRender);
			}

			return Out;
		}

		public override Task SetParametersAsync(ParameterView parameters)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			Task Out;

			try
			{

			}
			catch (Exception ex)
			{
				throw new PageException(ex, sf);
			}
			finally
			{
				Out = base.SetParametersAsync(parameters);
			}

			return Out;
		}
	}
}