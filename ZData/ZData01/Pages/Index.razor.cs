namespace ZData01.Pages
{
	using System.Diagnostics;
	using System.Threading.Tasks;
	using Actions;
	using Exceptions;
	using Microsoft.AspNetCore.Components;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn, Id = "Main page")]
	public partial class Index
	{
		[Parameter]
		[JsonProperty]
		public string ID { get; set; }

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
			finally
			{
				ID = "";
			}
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
				base.OnParametersSet();
			}
			catch (Exception ex)
			{
				throw new PageException(ex, sf);
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
