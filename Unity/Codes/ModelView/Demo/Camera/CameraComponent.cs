using UnityEngine;

namespace ET
{
	[ObjectSystem]
	public class CameraComponentAwakeSystem : AwakeSystem<CameraComponent>
	{
		public override void Awake(CameraComponent self)
		{
			self.Awake();
		}
	}

	[ObjectSystem]
	public class CameraComponentLateUpdateSystem : LateUpdateSystem<CameraComponent>
	{
		public override void LateUpdate(CameraComponent self)
		{
			self.LateUpdate();
		}
	}

	public class CameraComponent : Entity, IAwake, ILateUpdate
	{
		// 战斗摄像机
		public Camera mainCamera;

		public Unit Unit;

		public Camera MainCamera
		{
			get
			{
				return this.mainCamera;
			}
		}

		public void Awake()
		{
			this.mainCamera = Camera.main;
			//this.CinemachineVirtualCamera = GameObject.Find("/Global/CM vcam1").GetComponent<CinemachineVirtualCamera>();
		}

		public void Lock(Unit unit)
		{
			this.Unit = unit;
		}

		public void LateUpdate()
		{
			if (this.Unit != null) 
			{
				// 摄像机每帧更新位置
				UpdatePosition();
			}
		}

		private void UpdatePosition()
		{
			//this.CinemachineVirtualCamera.Follow = this.Unit.GetComponent<GameObjectComponent>().GameObject.transform;
			Vector3 cameraPos = this.mainCamera.transform.position;
			this.mainCamera.transform.position = new Vector3(this.Unit.Position.x, cameraPos.y, this.Unit.Position.z - 5);
		}
	}
}
