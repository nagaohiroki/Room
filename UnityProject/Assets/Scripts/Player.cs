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
	PlayerFoot mFoot = null;
	[SerializeField]
	LayerMask mSightTargetLayer = 0;
	[SerializeField]
	LayerMask mBlock = 0;
	[SerializeField]
	float mSightRange = 10.0f;
	[SerializeField]
	Vector3 mCameraOffset = Vector3.up;
	[SerializeField]
	Text mHUD = null;
	Vector3 mAngle;
	void Sight()
	{
		RaycastHit hitInfo;
		var start = Camera.main.transform.position;
		var end = start + Camera.main.transform.forward * mSightRange;
		mHUD.text = string.Empty;
		if(!Physics.Linecast(start, end, out hitInfo, mSightTargetLayer))
		{
			return;
		}
		if(hitInfo.collider.gameObject.layer == LayerMask.GetMask("Default"))
		{
			return;
		}
		var check = hitInfo.collider.gameObject.GetComponent<CheckObject>();
		if(check == null)
		{
			return;
		}
		mHUD.text = check.GetCheckObjectName;
		if(Input.GetButtonDown("Fire1"))
		{
			check.Check();
		}
	}
	void Move()
	{
		var vec = mRigidbody.velocity;
		vec.x = Input.GetAxis("Horizontal") * mMoveSpeed;
		vec.z = Input.GetAxis("Vertical") * mMoveSpeed;
		if(Input.GetButtonDown("Jump") && mFoot.IsLanding)
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
		cam.position = transform.position + mCameraOffset;
	}
	void Update()
	{
		Rotate();
		Move();
		Sight();
	}
	void OnApplicationFocus(bool hasFocus)
	{
		Cursor.lockState = hasFocus ? CursorLockMode.Locked : CursorLockMode.None;
	}
}
