using UnityEngine;
public class PlayerFoot : MonoBehaviour
{
	public bool IsLanding{get;set;} = false;
	void OnTriggerExit(Collider inColl)
	{
		IsLanding = false;
	}
	void OnTriggerEnter(Collider inColl)
	{
		IsLanding = true;
	}
}
