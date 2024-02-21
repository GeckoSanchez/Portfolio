namespace ZData00.Bases
{
	using System.Diagnostics;
	using Actions;
	using Enums;
	using MessagePack;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class Base<T> : IEquatable<T>, IEquatable<Base<T>>, IDisposable where T : notnull
	{
		private bool _disposed;

		public T Value { get; protected set; }

		[JsonConstructor]
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
			catch (Exception ex)
			{
				throw new BaseException(ex, sf, ExType.Base);
			}

			return Out ?? false;
		}

		/// <inheritdoc cref="object.Equals(object?)"/>
		/// <exception cref="BaseException"/>
		public bool Equals(T? other)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			bool? Out;

			try
			{
				Out = other is not null && EqualityComparer<T>.Default.Equals(other, Value);
			}
			catch (Exception ex)
			{
				throw new BaseException(ex, sf);
			}

			return Out ?? false;
		}

		/// <inheritdoc cref="object.Equals(object?)"/>
		/// <exception cref="BaseException"/>
		public bool Equals(Base<T>? other)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			bool? Out;

			try
			{
				Out = other is not null && EqualityComparer<T>.Default.Equals(other.Value, Value);
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
				throw new BaseException(ex, sf, ExType.Base);
			}

			return Out ?? base.GetHashCode();
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

		/// <summary>
		/// Function to serialize this data into the JSON format
		/// </summary>
		/// <param name="formatting">The given <see cref="Formatting"/> choice</param>
		/// <returns>A JSON-formatted string</returns>
		/// <exception cref="BaseException"/>
		public virtual string ToJSON(Formatting formatting = Formatting.None)
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
				throw new BaseException($"This data could not serialized into the JSON format", sf);
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
		/// Function to serialize this data into the <see cref="MessagePack"/> format
		/// </summary>
		/// <param name="opts">The given options for the <see cref="MessagePackSerializer"/></param>
		/// <returns>A byte array</returns>
		/// <exception cref="BaseException"/>
		public virtual byte[] ToMessagePack(MessagePackSerializerOptions? opts = null)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			byte[]? Out = null;

			try
			{
				if (typeof(T) != typeof(sbyte) && typeof(T) != typeof(byte) && typeof(T) != typeof(short) &&
						typeof(T) != typeof(ushort) && typeof(T) != typeof(int) && typeof(T) != typeof(uint) &&
						typeof(T) != typeof(long) && typeof(T) != typeof(ulong) && typeof(T) != typeof(float) &&
						typeof(T) != typeof(double) && typeof(T) != typeof(decimal) && typeof(T) != typeof(Half) &&
						typeof(T) != typeof(bool) && typeof(T) != typeof(string) && typeof(T) != typeof(char) &&
						typeof(T) != typeof(char[]) && typeof(T) != typeof(sbyte[]) && typeof(T) != typeof(byte[]) &&
						typeof(T) != typeof(short[]) && typeof(T) != typeof(ushort[]) && typeof(T) != typeof(int[]) &&
						typeof(T) != typeof(uint[]) && typeof(T) != typeof(long[]) && typeof(T) != typeof(ulong[]) &&
						typeof(T) != typeof(float[]) && typeof(T) != typeof(double[]) && typeof(T) != typeof(decimal[]) &&
						typeof(T) != typeof(Half[]) && typeof(T) != typeof(bool[]) && typeof(T) != typeof(string[]) &&
						Value is null)
					throw new Exception($"The given value {Format<T>.ExcValue(Value ?? default!)} could not be serialized into a MessagePack format, since its type {Format<string>.ExcValue(typeof(T).Name)} is not valid");
				else
					Out = MessagePackSerializer.Serialize(Value, opts);
			}
			catch (MessagePackSerializationException)
			{
				throw new BaseException($"This data could not serialized into the MessagePack format", sf);
			}
			catch (Exception ex)
			{
				throw new BaseException(ex, sf);
			}
			finally
			{
				Out ??= Def.MsgPack;
			}

			return Out;
		}

		protected virtual void Dispose(bool disposing)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			if (!_disposed)
			{
				if (disposing)
					Value = default!;

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				_disposed = true;
			}
		}

		/// <inheritdoc cref="IDisposable.Dispose"/>
		/// <exception cref="BaseException"/>
		public void Dispose()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
				Dispose(disposing: true);
				GC.SuppressFinalize(this);
				GC.Collect();
			}
			catch (Exception ex)
			{
				throw new BaseException(ex, sf);
			}
		}
	}
}
