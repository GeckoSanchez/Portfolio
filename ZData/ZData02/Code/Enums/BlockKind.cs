namespace ZData02.Enums
{
	/// <summary>
	/// Types of code-blocks
	/// </summary>
	[Flags]
	public enum BlockKind : ulong
	{
		None = 0,
		Constructor = 1 << 0,
		Primary_Constructor = 1 << 20 | Constructor,
		Empty_Constructor = 1 << 21 | Constructor,
		Destructor = 1 << 1,
		Property = 1 << 2,
		Field = 1 << 3,
		Event = 1 << 4,
		Parameter = 1 << 5,
		Delegate = 1 << 6,
		Function = 1 << 7,
		Override_Function = 1 << 12 | Function,
		Static_Function = 1 << 13 | Function,
		Virtual_Function = 1 << 14 | Function,
		Page = 1 << 8,
		Component = 1 << 9,
		Operator = 1 << 10,
		Program = 1 << 11,
		Get_Operator = 1 << 12 | Operator,
		Set_Operator = 1 << 13 | Operator,
		Addition_Operator = 1 << 14 | Operator,
		Subtraction_Operator = 1 << 15 | Operator,
		Multiplication_Operator = 1 << 16 | Operator,
		Division_Operator = 1 << 17 | Operator,
		Modulo_Operator = 1 << 18 | Operator,
		Concat_Operator = 1 << 19 | Operator,
		Explicit_Operator = 1 << 20 | Operator,
		Implicit_Operator = 1 << 21 | Operator,
		Equals_Operator = 1 << 22 | Operator,
		Not_Equals_Operator = 1 << 23 | Operator,
		Lesser_Operator = 1 << 24 | Operator,
		Greater_Operator = 1 << 25 | Operator,
		Lesser_Or_Equals_Operator = Lesser_Operator | Equals_Operator | Operator,
		Greater_Or_Equals_Operator = Greater_Operator | Not_Equals_Operator | Operator,
		Init_Operator = 1 << 26 | Operator,
		Increment_Operator = 1 << 27 | Operator,
		Decrement_Operator = 1 << 28 | Operator,
		Positive_Operator = 1 << 29 | Operator,
		Negative_Operator = 1 << 30 | Operator,
		Attribute = ulong.MaxValue - 1,
		Default = ulong.MaxValue ^ Constructor ^ Destructor ^ Property ^ Field ^ Event ^ Parameter ^ Delegate ^ Function ^ Page ^ Operator ^ Component ^ Program,
	}
}
