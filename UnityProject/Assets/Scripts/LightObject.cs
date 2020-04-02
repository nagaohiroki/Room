using UnityEngine;
public class LightObject : CheckObject
{
	[SerializeField]
	Light mLight = null;
	public override void Check()
	{
		base.Check();
		mLight.enabled = !mLight.enabled;
		if (mLight.enabled)
		{
			GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
		}
		else
		{
			GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
		}
	}
}
