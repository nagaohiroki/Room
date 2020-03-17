using UnityEngine;
using UnityEngine.UI;
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
	float mJumpPower = 0.0f;
	[SerializeField]
	Rigidbody mRigidbody = null;
	[SerializeField]
	Text mDebug = null;
	[SerializeField]
	CapsuleCollider mFootCollider = null;
	[SerializeField]
	PlayerFoot mFoot = null;
	Vector3 mAngle;
	void Move()
	{
		var vec = mRigidbody.velocity;
		vec.x = Input.GetAxis("Horizontal") * mMoveSpeed;
		vec.z = Input.GetAxis("Vertical") * mMoveSpeed;
		if (Input.GetButtonDown("Jump") && mFoot.IsLanding)
		{
			vec.y = mJumpPower;
		}
		mRigidbody.velocity = Quaternion.Euler(0.0f, Camera.main.transform.rotation.eulerAngles.y, 0.0f) * vec;
	}

	void Rotate()
	{
		var angle = Vector3.zero;
		mAngle.x -= Input.GetAxis("Mouse Y") * mRotateSpeed;
		mAngle.y += Input.GetAxis("Mouse X") * mRotateSpeed;
		if(mAngle.y <= 0.0f)
		{
			mAngle.y = 360.0f;
		}
		else if(mAngle.y >= 360.0f)
		{
			mAngle.y = 0.0f;
		}
		mAngle.x = Mathf.Clamp(mAngle.x, mMinAngleX, mMaxAngleX);
		var cam = Camera.main.transform;
		cam.localEulerAngles = mAngle;;
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
