using UnityEngine;
public class CheckObject : MonoBehaviour
{
	[SerializeField]
	string mCheckObjectName = "CheckObject";
	public virtual string GetCheckObjectName{get{return mCheckObjectName;}}
	public virtual void Check(Player inPlayer)
	{
		Debug.Log("Check:" + mCheckObjectName);
	}
}
