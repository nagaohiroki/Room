using UnityEngine;
public class Player : MonoBehaviour
{
	[SerializeField]
	float mMoveSpeed = 0.0f;
	[SerializeField]
	float mRotateSpeed = 0.0f;
	[SerializeField]
	float mMinAngleX = 0.0f;
	[SerializeField]
	float mMaxAngleX = 0.0f;
	[SerializeField]
	Rigidbody mRigidbody = null;
	Vector3 mAngle;
	void Move()
	{
		var vec = Vector3.zero;
		vec.x = Input.GetAxis("Horizontal");
		vec.z = Input.GetAxis("Vertical");
		mRigidbody.velocity = transform.rotation * vec * mMoveSpeed;
	}

	void Rotate()
	{
		var angle = Vector3.zero;
		mAngle.x = Mathf.Clamp(mAngle.x - Input.GetAxis("Mouse Y"), mMinAngleX, mMaxAngleX);
		mAngle.y += Input.GetAxis("Mouse X");
		if(mAngle.y <= 0.0f)
		{
			mAngle.y = 360.0f;
		}
		else if(mAngle.y >= 360.0f)
		{
			mAngle.y = 0.0f;
		}
		var cam = Camera.main.transform;
		var euler = cam.localEulerAngles;
		euler = mAngle * mRotateSpeed;
		cam.localEulerAngles = euler;
		cam.position = transform.position + Vector3.up;
	}
	void Update()
	{
		Rotate();
		Move();
	}
	void OnApplicationFocus(bool hasFocus)
	{
		Cursor.lockState = hasFocus ? CursorLockMode.Locked : CursorLockMode.None;
	}
}
