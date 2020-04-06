using System.Collections.Generic;
using UnityEngine;
public class Swich : CheckObject
{
	[SerializeField]
	List<CheckObject> mCheckObjects = null;
	public override void Check(Player inPlayer)
	{
		base.Check(inPlayer);
		foreach(var check in mCheckObjects)
		{
			check.Check(inPlayer);
		}
	}
}
