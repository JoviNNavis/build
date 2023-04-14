using UnityEngine;

namespace Voodoo.GameOps
{
	public class UIScaleWave : MonoBehaviour
	{

		[SerializeField] private float _speed = 1.0f;

		[SerializeField] private float _amount = 0.3f;
		
		void Update()
		{
			float t = Mathf.Sin(Time.time * _speed) * _amount;
			transform.localScale = Vector3.one + (Vector3.one * t);
		}
	}
}