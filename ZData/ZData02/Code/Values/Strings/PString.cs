namespace ZData02.Values
{
	using System.Diagnostics;
	using System.Diagnostics.CodeAnalysis;
	using System.Text.RegularExpressions;
	using Actions;
	using Attributes;
	using Enums;
	using Exceptions;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class PString : LString
	{
		/// <summary>
		/// The pattern that the base value should fit in
		/// </summary>
		[JsonProperty]
		public Regex Pattern { get; protected init; }

		/// <summary>
		/// Main constructor for the <see cref="PString"/> class
		/// </summary>
		/// <param name="value">The string value</param>
		/// <param name="range">The length range for <paramref name="value"/></param>
		/// <param name="pattern">The pattern in which <paramref name="value"/> should fit</param>
		/// <exception cref="ValueException"/>
		[JsonConstructor, MainConstructor]
		public PString(string value, Range range, Regex pattern) : base(value, range)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				string patt = $"{pattern}";

				if (patt.Contains("]{"))
					pattern = new($@"{patt.Split("]{").First()}]");

				Pattern = pattern;

				if (Pattern.IsMatch(value))
					throw new Exception($"The given string {Format.ExcValue(value)} does not fit the pattern {Format.ExcValue($"{Pattern}")}");
			}
			catch (Exception ex)
			{
				throw new ValueException(ex, sf);
			}
		}

		/// <summary>
		/// Constructor for the <see cref="PString"/> class
		/// </summary>
		/// <param name="value">The given string value</param>
		/// <param name="pattern">The pattern in which <paramref name="value"/> should fit</param>
		/// <param name="min">The minimum length for the <paramref name="value"/> string</param>
		/// <param name="max">The maximum length for the <paramref name="value"/> string</param>
		public PString(string value, Regex pattern, int min, int max) : this(value, new Range(min, max), new Regex($@"{pattern}{{{min},{max}}}")) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="PString(string, Regex, int, int)"/>
		public PString(string value) : this(value, new Range(0, value.Length), new Regex($@"[\w]{{0,{value.Length}}}")) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="PString(string, Regex, int, int)"/>
		public PString(string value, Regex pattern) : this(value, new Range(0, value.Length), pattern) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="PString(string, Regex, int, int)"/>
		/// <param name="options">The given options that will form the <see cref="Pattern"/></param>
		public PString(string value, params RegexCategory[] options) : this(value, new Range(0, value.Length), Get.Regex(options)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="PString(string, Regex, int, int)"/>
		public PString(string value, [StringSyntax("Regex")] string pattern) : this(value, new Range(0, value.Length), new Regex(pattern)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="PString(string, Regex, int, int)"/>
		/// <param name="options">The given options that will form the <see cref="Pattern"/></param>
		public PString(string value, int max, params RegexCategory[] options) : this(value, new Range(0, max), new Regex($@"{Get.Regex(options)}{{0,{max}}}")) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="PString(string, Regex, int, int)"/>
		public PString(string value, [StringSyntax("Regex")] string pattern, int max) : this(value, new Range(0, max), new Regex($@"{pattern}{{0,{max}}}")) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="PString(string, Regex, int, int)"/>
		public PString(string value, Regex pattern, int max) : this(value, new Range(0, max), new Regex($@"{pattern}{{0,{max}}}")) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="PString(string, Regex, int, int)"/>
		public PString(string value, int min, int max) : this(value, new Range(min, max), new Regex($@"[\w]{{{min},{max}}}")) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="PString(string, Regex, int, int)"/>
		/// <param name="options">The given options that will form the <see cref="Pattern"/></param>
		public PString(string value, int min, int max, params RegexCategory[] options) : this(value, new Range(min, max), new Regex($@"{Get.Regex(options)}{{{min},{max}}}")) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="PString(string, Regex, int, int)"/>
		public PString(string value, [StringSyntax("Regex")] string pattern, int min, int max) : this(value, new Range(min, max), new Regex($@"{pattern}{{{min},{max}}}")) => Log.Event(new StackFrame(true));

	}
}
