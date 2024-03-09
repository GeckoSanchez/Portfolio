namespace ZData04.Attributes
{
	using System.Diagnostics;
	using System.Diagnostics.CodeAnalysis;
	using Actions;

	/// <summary>
	/// Attribute indicating that the given code-block will not have any log messages
	/// in any log file
	/// </summary>
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Constructor, AllowMultiple = false, Inherited = false)]
	public class UnloggedAttribute : Attribute
	{
		public override object TypeId => base.TypeId;

		public override bool Equals([NotNullWhen(true)] object? obj)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			bool? Out;

			try
			{
				Out = base.Equals(obj);
			}
			catch (Exception)
			{
				throw;
			}

			return Out ?? false;
		}

		public override int GetHashCode()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			int? Out;

			try
			{
				Out = base.GetHashCode();
			}
			catch (Exception)
			{
				throw;
			}

			return Out ?? 0;
		}

		public override bool IsDefaultAttribute()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			bool? Out;

			try
			{
				Out = base.IsDefaultAttribute();
			}
			catch (Exception)
			{
				throw;
			}

			return Out ?? false;
		}

		public override bool Match(object? obj)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			bool? Out;

			try
			{
				Out = base.Match(obj);
			}
			catch (Exception)
			{
				throw;
			}

			return Out ?? false;
		}

		public override string ToString()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			string? Out = null;

			try
			{
				Out = nameof(UnloggedAttribute).Split(nameof(Attribute)).First();
			}
			catch (Exception)
			{
				throw;
			}
			finally
			{
				Out ??= base.ToString() ?? "";
			}

			return Out;
		}
	}
}
