namespace ZData00.Classes
{
	using System.Collections;
	using System.Diagnostics;
	using Actions;
	using Attributes;
	using Bases;
	using Exceptions;
	using MessagePack;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class Array<TArr> : Base<TArr[]>, ICollection<TArr> where TArr : notnull
	{
		/// <inheritdoc cref="Base{T}.Value"/>
		[JsonProperty]
		public TArr[] Values => base.Value;
		[JsonProperty("Is read-only?")]
		public bool IsReadOnly { get; private init; }

		/// <summary>
		/// <c>
		/// DO NOT USE THIS ONE, PLZ !!!
		/// </c>
		/// </summary>
		/// <exception cref="NotImplementedException"/>
		public new TArr[] Value => throw new NotImplementedException();
		public int Count => Value.Length;

		/// <summary>
		/// Primary constructor for the <see cref="Array{TArr}"/> class
		/// </summary>
		/// <param name="values">The content for this <see cref="Array{TArr}"/> object</param>
		/// <param name="isReadonly">If this object can be modified or not</param>
		[JsonConstructor, MainConstructor]
		public Array(TArr[] values, bool isReadonly = false) : base(values)
		{
			Log.Event(new StackFrame(true));
			IsReadOnly = isReadonly;
		}

		/// <summary>
		/// Constructor for the <see cref="Array{TArr}"/> class
		/// </summary>
		/// <param name="value">A single value for this <see cref="Array{TArr}"/> object</param>
		/// <inheritdoc cref="Array{TArr}.Array(TArr[], bool)"/>
		public Array(TArr value, bool isReadonly = false) : this([value], isReadonly) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="Array{TArr}"/> class
		/// </summary>
		/// <inheritdoc cref="Array{TArr}.Array(TArr[], bool)"/>
		public Array(IEnumerable<TArr> values, bool isReadonly = false) : this([.. values], isReadonly) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Array{TArr}.Array(IEnumerable{TArr}, bool)"/>
		public Array(ICollection<TArr> values, bool isReadonly = false) : this([.. values], isReadonly) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="ICollection{TArr}.Add(TArr)"/>
		/// <exception cref="ClassException"/>
		public void Add(TArr item)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				if (IsReadOnly)
					throw new Exception($"This array cannot be added to, since it is set as a read-only array");
				else
					base.Value = [.. Value, item];
			}
			catch (Exception ex)
			{
				throw new ClassException(ex, sf);
			}
		}

		/// <summary>
		/// Adds at least one <typeparamref name="TArr"/> item to this <see cref="Array{TArr}"/>
		/// </summary>
		/// <exception cref="ClassException"/>
		public void Add(params TArr[] items)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				if (IsReadOnly)
					throw new Exception($"This array cannot be added to, since it is set as a read-only array");
				else
					base.Value = [.. Value, .. items];
			}
			catch (Exception ex)
			{
				throw new ClassException(ex, sf);
			}
		}

		/// <inheritdoc cref="ICollection{TArr}.Clear"/>
		/// <exception cref="ClassException"/>
		public void Clear()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				if (IsReadOnly)
					throw new Exception($"This array cannot be added to, since it is set as a read-only array");
				else
					base.Value = [];
			}
			catch (Exception ex)
			{
				throw new ClassException(ex, sf);
			}
		}

		/// <inheritdoc cref="ICollection{TArr}.Contains(TArr)"/>
		/// <exception cref="ClassException"/>
		public bool Contains(TArr item)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			bool? Out;

			try
			{
				Out = Values.Contains(item);
			}
			catch (Exception ex)
			{
				throw new ClassException(ex, sf);
			}

			return Out ?? false;
		}

		/// <inheritdoc cref="ICollection{TArr}.CopyTo(TArr[], int)"/>
		/// <exception cref="ClassException"/>
		public void CopyTo(TArr[] array, int arrayIndex)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				if (arrayIndex > Count)
					throw new Exception($"The given index {Format<int>.ExcValue(arrayIndex)} is greater than the last index of the array");
				else
					array = Values[arrayIndex..];
			}
			catch (Exception ex)
			{
				throw new ClassException(ex, sf);
			}
		}

		/// <inheritdoc cref="IEnumerable{TArr}.GetEnumerator"/>
		/// <exception cref="ClassException"/>
		public IEnumerator<TArr> GetEnumerator()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			IEnumerator<TArr>? Out;

			try
			{
				Out = (IEnumerator<TArr>)Values.GetEnumerator();
			}
			catch (ClassException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new ClassException(ex, sf);
			}

			return Out;
		}

		/// <inheritdoc cref="ICollection{TArr}.Remove(TArr)"/>
		/// <exception cref="ClassException"/>
		public bool Remove(TArr item)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			bool? Out;

			try
			{
				if (IsReadOnly)
					throw new Exception($"This array cannot be added to, since it is set as a read-only array");
				else
				{
					Out = Value.Contains(item);

					var init = Value;
					bool skipped = false;

					if (Out ?? false)
					{
						base.Value = [];

						foreach (var i in init)
						{
							if (!skipped && i.Equals(item))
							{
								skipped = true;
								continue;
							}

							base.Value = [.. Value, i];
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw new ClassException(ex, sf);
			}

			return Out ?? false;
		}

		/// <inheritdoc cref="IEnumerable.GetEnumerator"/>
		/// <exception cref="ClassException"/>
		IEnumerator IEnumerable.GetEnumerator()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			IEnumerator<TArr>? Out;

			try
			{
				Out = GetEnumerator();
			}
			catch (Exception ex)
			{
				throw new ClassException(ex, sf);
			}

			return Out;
		}

		public override string ToString()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			string Out = "[";

			try
			{
				foreach (var i in Value)
					Out += $"{i},";

				Out = $"{Out.TrimEnd(',')}]";
			}
			catch (Exception ex)
			{
				throw new ClassException(ex, sf);
			}

			return Out;
		}

		public override byte[] ToMessagePack(MessagePackSerializerOptions? opts = null)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			byte[] Out = [];

			try
			{
				foreach (var i in Values)
					Out = [.. Out, .. MessagePackSerializer.Serialize(i)];
			}
			catch (Exception ex)
			{
				throw new ClassException(ex, sf);
			}
			finally
			{
				try
				{
					if (Out.Length == 0)
						Out ??= base.ToMessagePack(opts);
				}
				catch (BaseException)
				{
					Out = Def.MsgPack;
				}
			}

			return Out;
		}
	}
}
