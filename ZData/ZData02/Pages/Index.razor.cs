namespace ZData02.Pages
{
	using System.ComponentModel.DataAnnotations;
	using System.Diagnostics;
	using System.Threading.Tasks;
	using Actions;
	using Bases;
	using Exceptions;
	using Microsoft.AspNetCore.Components;

	public partial class Index
	{
		public Index()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				BaseData<int> data = new(default);
				var results = data.Validate();

				foreach (ValidationResult? i in results.Where(e => e != null))
					Debug.WriteLine($"{i.ErrorMessage} (For: {string.Join(", ", i.MemberNames)})");
			}
			catch (Exception ex)
			{
				Log.Exception(new PageException(ex, sf));
			}
		}

		public override Task SetParametersAsync(ParameterView parameters)
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

			return base.SetParametersAsync(parameters);
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
				Log.Exception(new PageException(ex, sf));
			}
			finally
			{
				Out ??= base.ToString() ?? "";
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
				Log.Exception(new PageException(ex, sf));
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

			try
			{

			}
			catch (Exception ex)
			{
				Log.Exception(new PageException(ex, sf));
			}

			return base.OnAfterRenderAsync(firstRender);
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
				Log.Exception(new PageException(ex, sf));
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

			try
			{

			}
			catch (Exception ex)
			{
				Log.Exception(new PageException(ex, sf));
			}

			return base.OnInitializedAsync();
		}

		protected override void OnParametersSet()
		{
			base.OnParametersSet();
		}

		protected override Task OnParametersSetAsync()
		{
			return base.OnParametersSetAsync();
		}

		protected override bool ShouldRender()
		{
			return base.ShouldRender();
		}
	}
}