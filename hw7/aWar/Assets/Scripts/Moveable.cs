using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{
	private Vector3 m_camRot;
	private Transform m_camTransform;//摄像机Transform
	private Transform m_transform;//摄像机父物体Transform
	public float m_movSpeed=10;//移动系数
	public float m_rotateSpeed=1;//旋转系数
	public Animator m_Animator;

	public MsgManager msgm;

	private void Start()
	{
		m_camTransform = Camera.main.transform;
		m_transform = GetComponent<Transform>();
		msgm = Singleton<MsgManager>.Instance;
	}
	private void Update()
	{
		Control();
	}
	void OnCollisionEnter(Collision collision){
		// Destroy(this.gameObject);
		string name = collision.collider.name;
		if (name.Substring (0, 3) == "egg") {
			msgm.SendMsg("win","MV","FC") ;
		}
		//Debug.Log("Name is " + name);
	}

	void Control() 
	{
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		// m_transform.rotation = Quaternion.Euler(0, m_transform.rotation.y, 0);
		m_Animator = gameObject.GetComponent<Animator>();
		if (Input.GetMouseButton(0))
		{
			//获取鼠标移动距离
			float rh = Input.GetAxis("Mouse X");
			float rv = Input.GetAxis("Mouse Y");
			
			// 旋转摄像机
			m_camRot.x -= rv * m_rotateSpeed;
			m_camRot.y += rh*m_rotateSpeed;
			
		}

		
		// 定义3个值控制移动
		float xm = 0, ym = 0, zm = 0;
		//按键盘W向上移动
		if (Input.GetKey(KeyCode.W))
		{
			m_Animator.SetTrigger("standMove");
			m_transform.rotation = Quaternion.Euler(0, 180, 0);
			zm += m_movSpeed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.S))//按键盘S向下移动
		{
			m_Animator.SetTrigger("standMove");
			m_transform.rotation = Quaternion.Euler(0, 0, 0);
			zm -= m_movSpeed * Time.deltaTime;
		}
		
		if (Input.GetKey(KeyCode.A))//按键盘A向左移动
		{
			m_Animator.SetTrigger("standMove");
			m_transform.rotation = Quaternion.Euler(0, 90, 0);
			xm -= m_movSpeed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.D))//按键盘D向右移动
		{
			m_Animator.SetTrigger("standMove");
			m_transform.rotation = Quaternion.Euler(0, 270, 0);
			xm += m_movSpeed * Time.deltaTime;
		}
		m_transform.position = m_transform.position + new Vector3 (xm, ym, zm);

	}
}