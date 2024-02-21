namespace ZData00.Pages
{
	using System.Diagnostics;
	using System.Threading.Tasks;
	using Actions;
	using Bases;
	using Enums;
	using Microsoft.AspNetCore.Components;

	public partial class Index
	{
		public BaseID<ObjectType> ID { get; set; }

		public Index()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				ID = new(ObjectType.Object);
			}
			catch (Exception ex)
			{
				Log.Error(ex, sf);
			}
			finally
			{
				ID ??= new(321);
			}
		}

		public override Task SetParametersAsync(ParameterView parameters)
		{
			return base.SetParametersAsync(parameters);
		}

		public override string ToString()
		{
			return NavMgr.Uri;
		}

		protected override void OnAfterRender(bool firstRender)
		{
			base.OnAfterRender(firstRender);
		}

		protected override Task OnAfterRenderAsync(bool firstRender)
		{
			return base.OnAfterRenderAsync(firstRender);
		}

		protected override void OnInitialized()
		{
			base.OnInitialized();
		}

		protected override Task OnInitializedAsync()
		{
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