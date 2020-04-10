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
	Text mHUD = null;
	[SerializeField]
	Light mLight = null;
	[SerializeField]
	GameObject mHand = null;
	Vector3 mAngle;
	bool HasItem{get{return mHand.transform.childCount != 0;}}
	public void TakeHand(CheckObject inCheckObject)
	{
		if(HasItem)
		{
			return;
		}
		var rigid = inCheckObject.GetComponent<Rigidbody>();
		rigid.isKinematic = true;
		rigid.useGravity = false;
		rigid.detectCollisions = false;
		inCheckObject.transform.localPosition = Vector3.zero;
		inCheckObject.transform.SetParent(mHand.transform, false);
	}

	void ReleaseFromHand()
	{
		var rigid = mHand.transform.GetChild(0).GetComponent<Rigidbody>();
		mHand.transform.GetChild(0).transform.SetParent(null);
		rigid.isKinematic = false;
		rigid.useGravity = true;
		rigid.detectCollisions = true;
	}
	void Sight()
	{
		if(HasItem)
		{
			if(Input.GetButtonDown("Fire1"))
			{
				ReleaseFromHand();
			}
			return;
		}
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
			check.Check(this);
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
		mRigidbody.velocity = Quaternion.Euler(0.0f, mRigidbody.rotation.eulerAngles.y, 0.0f) * vec;
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
		Camera.main.transform.localRotation = Quaternion.Euler(mAngle.x, 0.0f, 0.0f);
		mRigidbody.MoveRotation(Quaternion.Euler(0.0f, mAngle.y, 0.0f));
	}
	void Update()
	{
		Rotate();
		Move();
		Sight();
		if(Input.GetKeyDown(KeyCode.F))
		{
			mLight.enabled = !mLight.enabled;
		}
	}
	void OnApplicationFocus(bool hasFocus)
	{
		Cursor.lockState = hasFocus ? CursorLockMode.Locked : CursorLockMode.None;
	}
}
