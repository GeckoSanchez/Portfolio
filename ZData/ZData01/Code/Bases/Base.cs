namespace ZData01.Bases
{
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Globalization;
	using Actions;
	using Attributes;
	using Categories;
	using MessagePack;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class Base<T> : IDisposable, IBase where T : notnull
	{
		private bool _disposed;

		/// <summary>
		/// The base <typeparamref name="T"/> value
		/// </summary>
		public T Value { get; protected set; }

		/// <summary>
		/// Primary constructor for the <see cref="Base{TBase}"/> class
		/// </summary>
		/// <param name="value">The given value</param>
		[JsonConstructor, MainConstructor]
		public Base(T value)
		{
			Log.Event(new StackFrame(true));
			Value = value;
		}

		/// <inheritdoc cref="object.Equals(object?)"/>
		/// <exception cref="BaseException"/>
		public override bool Equals(object? obj)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			bool? Out;

			try
			{
				Out = Equals(obj as Base<T>);
			}
			catch (BaseException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new BaseException(ex, sf);
			}

			return Out ?? false;
		}

		/// <inheritdoc cref="IEquatable{T}.Equals(T)"/>
		/// <exception cref="BaseException"/>
		public bool Equals(Base<T>? other)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			bool? Out;

			try
			{
				Out = other is not null && EqualityComparer<T>.Default.Equals(Value, other.Value);
			}
			catch (Exception ex)
			{
				throw new BaseException(ex, sf);
			}

			return Out ?? false;
		}

		/// <inheritdoc cref="object.GetHashCode"/>
		/// <exception cref="BaseException"/>
		public override int GetHashCode()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			int? Out;

			try
			{
				Out = HashCode.Combine(Value);
			}
			catch (Exception ex)
			{
				throw new BaseException(ex, sf);
			}

			return Out ?? 0;
		}

		/// <inheritdoc cref="object.ToString"/>
		/// <exception cref="BaseException"/>
		public override string ToString()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			string? Out = null;

			try
			{
				Out = $"{Value}";
			}
			catch (Exception ex)
			{
				throw new BaseException(ex, sf);
			}
			finally
			{
				Out ??= base.ToString() ?? "";
			}

			return Out;
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					Value = default!;
					GC.Collect();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				_disposed = true;
			}
		}

		// TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		~Base()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: false);
		}

		/// <inheritdoc cref="IDisposable.Dispose"/>
		/// <exception cref="BaseException"/>
		public void Dispose()
		{
			try
			{
				// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
				Dispose(disposing: true);
				GC.SuppressFinalize(this);
				GC.Collect();
			}
			catch (Exception ex)
			{
				throw new BaseException(ex, new StackFrame(true));
			}
		}

		/// <summary>
		/// Function to serialize this data into the <see cref="MessagePack"/> format
		/// </summary>
		/// <param name="opts">The options for the <see cref="MessagePackSerializer"/></param>
		/// <returns>A byte array of <see cref="MessagePack"/> data</returns>
		/// <exception cref="BaseException"/>
		/// <exception cref="MessagePackSerializationException"/>
		public virtual byte[] ToMessagePack(MessagePackSerializerOptions? opts = null)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			byte[] Out;

			try
			{
				if (typeof(T) != typeof(sbyte) && typeof(T) != typeof(byte) && typeof(T) != typeof(short) &&
						typeof(T) != typeof(ushort) && typeof(T) != typeof(int) && typeof(T) != typeof(uint) &&
						typeof(T) != typeof(long) && typeof(T) != typeof(ulong) && typeof(T) != typeof(float) &&
						typeof(T) != typeof(double) && typeof(T) != typeof(decimal) && typeof(T) != typeof(Half) &&
						typeof(T) != typeof(bool) && typeof(T) != typeof(string) && typeof(T) != typeof(char) &&
						typeof(T) != typeof(Int128) && typeof(T) != typeof(UInt128) && typeof(T) != typeof(char[]) &&
						typeof(T) != typeof(sbyte[]) && typeof(T) != typeof(byte[]) && typeof(T) != typeof(short[]) &&
						typeof(T) != typeof(ushort[]) && typeof(T) != typeof(int[]) && typeof(T) != typeof(uint[]) &&
						typeof(T) != typeof(long[]) && typeof(T) != typeof(ulong[]) && typeof(T) != typeof(float[]) &&
						typeof(T) != typeof(double[]) && typeof(T) != typeof(decimal[]) && typeof(T) != typeof(Half[]) &&
						typeof(T) != typeof(bool[]) && typeof(T) != typeof(string[]) && typeof(T) != typeof(Int128[]) &&
						typeof(T) != typeof(Int128[]) && Value is null)
					throw new Exception($"The given value {Format<T>.ExcValue(Value ?? default!)} could not be serialized into a MessagePack format, since its type {Format<string>.ExcValue(typeof(T).Name)} is not valid");
				else if (typeof(T) == typeof(UInt128))
				{
					ulong top = ulong.Parse($"{(UInt128)(object)Value:X32}"[..16], NumberStyles.HexNumber);
					ulong btm = ulong.Parse($"{(UInt128)(object)Value:X32}"[16..], NumberStyles.HexNumber);

					Out = MessagePackSerializer.Serialize(top, opts);
					Out = [.. Out, .. MessagePackSerializer.Serialize(btm, opts)];
				}
				else if (typeof(T) == typeof(Int128))
				{
					ulong top = ulong.Parse($"{(Int128)(object)Value:X32}"[..16], NumberStyles.HexNumber);
					ulong btm = ulong.Parse($"{(Int128)(object)Value:X32}"[16..], NumberStyles.HexNumber);

					Out = MessagePackSerializer.Serialize(top, opts);
					Out = [.. Out, .. MessagePackSerializer.Serialize(btm, opts)];
				}
				else
					Out = MessagePackSerializer.Serialize(Value, opts);
			}
			catch (MessagePackSerializationException ex)
			{
				throw new MessagePackSerializationException($"This data could not be serialized into the MessagePack format", ex);
			}
			catch (Exception ex)
			{
				throw new BaseException(ex, sf);
			}

			return Out;
		}

		/// <summary>
		/// Function to serialize this data into the JSON format
		/// </summary>
		/// <param name="opts">The formatting option for the output</param>
		/// <returns>A JSON-formatted string</returns>
		/// <exception cref="BaseException"/>
		public virtual string ToJSON(Formatting formatting = Formatting.Indented)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			string? Out = null;

			try
			{
				Out = JsonConvert.SerializeObject(this, formatting);
			}
			catch (JsonException)
			{
				throw new BaseException($"This data could not be serialized into the JSON format", sf);
			}
			catch (Exception ex)
			{
				throw new BaseException(ex, sf);
			}
			finally
			{
				Out ??= Def.JSON;
			}

			return Out;
		}

		/// <summary>
		/// Equality operator for the <see cref="Base{TBase}"/> class
		/// </summary>
		/// <param name="left">The first <see cref="Base{TBase}"/></param>
		/// <param name="right">The second <see cref="Base{TBase}"/></param>
		/// <returns>If both values are equal or not</returns>
		/// <exception cref="BaseException"/>
		public static bool operator ==(Base<T>? left, Base<T>? right)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			bool? Out;

			try
			{
				Out = EqualityComparer<Base<T>>.Default.Equals(left, right);
			}
			catch (Exception ex)
			{
				throw new BaseException(ex, sf);
			}

			return Out ?? false;
		}

		/// <summary>
		/// Inequality operator for the <see cref="Base{TBase}"/> class
		/// </summary>
		/// <param name="left">The first <see cref="Base{TBase}"/></param>
		/// <param name="right">The second <see cref="Base{TBase}"/></param>
		/// <returns>If both values are inequal or not</returns>
		/// <exception cref="BaseException"/>
		public static bool operator !=(Base<T>? left, Base<T>? right)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			bool? Out;

			try
			{
				Out = !(left == right);
			}
			catch (Exception ex)
			{
				throw new BaseException(ex, sf);
			}

			return Out ?? false;
		}
	}
}
