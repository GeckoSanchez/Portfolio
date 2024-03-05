namespace ZData02.Bases
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Diagnostics;
	using System.Diagnostics.CodeAnalysis;
	using System.Globalization;
	using Actions;
	using Attributes;
	using Categories;
	using MessagePack;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class BaseData<TData> : IEquatable<BaseData<TData>?>, IEqualityComparer<TData>, IValidatableObject, IBase<TData>
		where TData : notnull
	{
		/// <summary>
		/// The base <typeparamref name="TData"/> data for this datum
		/// </summary>
		public TData Data { get; protected set; }

		/// <summary>
		/// Primary constructor for the <see cref="BaseData{T}"/> class
		/// </summary>
		/// <param name="data">The given base data</param>
		[JsonConstructor, MainConstructor]
		public BaseData([NotNull] TData data)
		{
			Log.Event(new StackFrame(true));
			Data = data;
		}

		public static implicit operator BaseData<TData>(TData v) => new(v);

		/// <summary>
		/// Returns a string that represents the current data's content.
		/// </summary>
		/// <exception cref="BaseException"/>
		public override string ToString()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			string? Out = null;

			try
			{
				Out = $"{Data}";
			}
			catch (Exception ex)
			{
				throw new BaseException(ex, sf);
			}
			finally
			{
				Out ??= "";
			}

			return Out;
		}

		/// <summary>
		/// Function to serialize this data into the JSON format
		/// </summary>
		/// <param name="formatting">The formatting for the JSON string</param>
		/// <exception cref="BaseException"/>
		public virtual string ToJSON(Formatting? formatting = null)
		{

			var sf = new StackFrame(true);
			Log.Event(sf);

			string? Out = null;

			try
			{
				Out = JsonConvert.SerializeObject(this, formatting ?? Def.JSONFormatting);
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
		/// <param name="opts">The options for the <see cref="MessagePackSerializer"/></param>
		/// <returns>A byte array of <see cref="MessagePack"/> data</returns>
		/// <exception cref="BaseException"/>
		public virtual byte[] ToMessagePack(MessagePackSerializerOptions? opts = null)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			byte[] Out = [];
			string s;
			ulong top, btm;

			var type = typeof(TData);

			try
			{
				if (type != typeof(sbyte) && type != typeof(byte) && type != typeof(short) &&
						type != typeof(ushort) && type != typeof(int) && type != typeof(uint) &&
						type != typeof(long) && type != typeof(ulong) && type != typeof(float) &&
						type != typeof(double) && type != typeof(decimal) && type != typeof(Half) &&
						type != typeof(bool) && type != typeof(string) && type != typeof(char) &&
						type != typeof(Int128) && type != typeof(UInt128) && type != typeof(char[]) &&
						type != typeof(sbyte[]) && type != typeof(byte[]) && type != typeof(short[]) &&
						type != typeof(ushort[]) && type != typeof(int[]) && type != typeof(uint[]) &&
						type != typeof(long[]) && type != typeof(ulong[]) && type != typeof(float[]) &&
						type != typeof(double[]) && type != typeof(decimal[]) && type != typeof(Half[]) &&
						type != typeof(bool[]) && type != typeof(string[]) && type != typeof(Int128[]) &&
						type != typeof(UInt128[]) && Data is null)
					throw new Exception($"The given value {Format<TData>.ExcValue(Data ?? default!)} could not be serialized into a MessagePack format, since its type {Format<string>.ExcValue(type.Name)} is not valid");
				else if (type == typeof(UInt128))
				{
					s = $"{(UInt128)(object)Data:X32}";
					top = ulong.Parse(s[..16], NumberStyles.HexNumber);
					btm = ulong.Parse(s[16..], NumberStyles.HexNumber);

					Out = [.. MessagePackSerializer.Serialize(top, opts), .. MessagePackSerializer.Serialize(btm, opts)];
				}
				else if (type == typeof(Int128))
				{
					s = $"{(Int128)(object)Data:X32}";
					top = ulong.Parse(s[..16], NumberStyles.HexNumber);
					btm = ulong.Parse(s[16..], NumberStyles.HexNumber);

					Out = [.. MessagePackSerializer.Serialize(top, opts), .. MessagePackSerializer.Serialize(btm, opts)];
				}
				else if (type == typeof(UInt128[]))
				{
					foreach (var i in (UInt128[])(object)Data)
					{
						s = $"{i:X32}";
						top = ulong.Parse(s[..16], NumberStyles.HexNumber);
						btm = ulong.Parse(s[16..], NumberStyles.HexNumber);

						Out = [.. Out, .. MessagePackSerializer.Serialize(top, opts), .. MessagePackSerializer.Serialize(btm, opts)];
					}
				}
				else if (type == typeof(Int128[]))
				{
					foreach (var i in (Int128[])(object)Data)
					{
						s = $"{i:X32}";
						top = ulong.Parse(s[..16], NumberStyles.HexNumber);
						btm = ulong.Parse(s[16..], NumberStyles.HexNumber);

						Out = [.. Out, .. MessagePackSerializer.Serialize(top, opts), .. MessagePackSerializer.Serialize(btm, opts)];
					}
				}
				else
					Out = MessagePackSerializer.Serialize(Data, opts);
			}
			catch (MessagePackSerializationException ex)
			{
				throw new BaseException(new Exception($"This data could not be serialized into the MessagePack format", ex), sf);
			}
			catch (Exception ex)
			{
				throw new BaseException(ex, sf);
			}

			return Out;
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
				Out = Equals(obj as BaseData<TData>);
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

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		/// <see langword="true"/> if the current object is equal to the other parameter; otherwise, <see langword="false"/>.
		/// </returns>
		/// <exception cref="BaseException"/>
		public bool Equals(BaseData<TData>? other)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			bool? Out;

			try
			{
				Out = other is not null && EqualityComparer<TData>.Default.Equals(Data, other.Data);
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

		/// <inheritdoc cref="object.Equals(object?, object?)"/>
		/// <param name="x">The first object of type <typeparamref name="TData"/> to compare.</param>
		/// <param name="y">The second object of type <typeparamref name="TData"/> to compare.</param>
		/// <exception cref="BaseException"/>
		public bool Equals(TData? x, TData? y)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			bool? Out;

			try
			{
				Out = x is null && y is null || x is not null && x.Equals(y);
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
				Out = HashCode.Combine(Data, Data.GetHashCode());
			}
			catch (Exception ex)
			{
				throw new BaseException(ex, sf);
			}

			return Out ?? 0;
		}

		/// <inheritdoc cref="IEqualityComparer{T}.GetHashCode(T)"/>
		/// <exception cref="BaseException"/>
		public int GetHashCode([DisallowNull] TData obj)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			int? Out;

			try
			{
				Out = HashCode.Combine(obj, obj.GetHashCode());
			}
			catch (Exception ex)
			{
				throw new BaseException(ex, sf);
			}

			return Out ?? 0;
		}

		public virtual IEnumerable<ValidationResult> Validate(ValidationContext? validationContext = null)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			if (string.IsNullOrWhiteSpace($"{Data}"))
				yield return new ValidationResult($"The given data {Format<TData>.ExcValue(Data)} was found to be null", [nameof(Data)]);
		}

		public static bool operator ==(BaseData<TData>? left, BaseData<TData>? right) => EqualityComparer<BaseData<TData>>.Default.Equals(left, right);
		public static bool operator !=(BaseData<TData>? left, BaseData<TData>? right) => !(left == right);
	}
}
