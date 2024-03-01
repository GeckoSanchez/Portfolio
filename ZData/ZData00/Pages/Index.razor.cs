namespace ZData00.Pages
{
	using System.Diagnostics;
	using System.Net.Http.Headers;
	using System.Threading.Tasks;
	using Actions;
	using Classes;
	using Enums;
	using Exceptions;
	using Identities;
	using Microsoft.AspNetCore.Components;
	using ZData00.Devices;

	public partial class Index
	{
		[Parameter]
		public string? ID { get; set; }

		public Array<int> Array { get; set; }

		public ObjectIdentity Identity { get; set; }

		public Cellphone Cellphone { get; set; }

		public Index()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				Cellphone = new(new(Def.Name), "5148388430");
			}
			catch (Exception ex)
			{
				Log.Error(ex, sf);
			}
			finally
			{
				Array ??= new([]);
				Identity ??= new(Def.Name, ObjectType.Table);
				Cellphone ??= new(new(Def.Name), "5555555555");
			}
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

		public override string ToString()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			string? Out = null;

			try
			{
				Out = NavMgr.Uri;
			}
			catch (Exception ex)
			{
				throw new PageException(ex, sf);
			}
			finally
			{
				Out ??= "";
			}

			return Out;
		}

		protected override void OnAfterRender(bool firstRender)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{

			}
			catch (Exception ex)
			{
				throw new PageException(ex, sf);
			}
			finally
			{
				base.OnAfterRender(firstRender);
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

		protected override void OnInitialized()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{

			}
			catch (Exception ex)
			{
				throw new PageException(ex, sf);
			}
			finally
			{
				base.OnInitialized();
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

			}
			catch (Exception ex)
			{
				throw new PageException(ex, sf);
			}
			finally
			{
				base.OnParametersSet();
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

			bool? Out;

			try
			{
				Out = base.ShouldRender();
			}
			catch (Exception ex)
			{
				throw new PageException(ex, sf);
			}

			return Out ?? false;
		}
	}
}