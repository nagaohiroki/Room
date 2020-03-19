using UnityEngine;
public class Door : CheckObject
{
	[SerializeField]
	float mDoorSpeed = 10.0f;
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
		mAngleY = Mathf.Clamp(mAngleY, 0.0f, 90.0f);
		transform.parent.transform.rotation = Quaternion.Euler(0.0f, mAngleY, 0.0f);
	}
}
