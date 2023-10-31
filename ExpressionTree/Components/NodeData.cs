using System;
namespace HKSH.HIS5.LIB.DS.ExpressionTree.Components
{
	public abstract class NodeData
	{
		public string Id;
		public float Amount = 0;

		public float GetAmount()
		{
			return this.Amount;
		}
	}
}

