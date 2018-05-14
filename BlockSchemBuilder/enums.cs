using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockSchemBuilder
{
	public enum blockTypes
	{
		Operator,
		Start,
		IO,
		Condition
	}

	public enum exitTypes
	{
		EndofBlock,
		Continue,
		Return
	}
}
