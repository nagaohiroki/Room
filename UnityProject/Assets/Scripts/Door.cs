using UnityEngine;
public class Door : CheckObject
{
	[SerializeField]
	float mDoorSpeed = 50.0f;
	[SerializeField]
	float mDoorOpen = 90.0f;
	[SerializeField]
	float mDoorClose = 0.0f;
	bool mIsOpen = false;
	float mAngleY = 0.0f;
	public override void Check()
	{
		mIsOpen = !mIsOpen;
		base.Check();
	}
	void Update()
	{
		mAngleY += (mIsOpen ? mDoorSpeed : -mDoorSpeed) * Time.deltaTime;
		mAngleY = Mathf.Clamp(mAngleY, Mathf.Min(mDoorClose, mDoorOpen), Mathf.Max(mDoorClose, mDoorOpen));
		if (name == "door01")
		{
			Debug.Log("" + mAngleY);
		}
		transform.localRotation = Quaternion.Euler(0.0f, mAngleY, 0.0f);
	}
}
