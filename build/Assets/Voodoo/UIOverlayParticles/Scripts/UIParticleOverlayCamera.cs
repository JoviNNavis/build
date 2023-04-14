using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.Experimental.SceneManagement;
using UnityEngine.SceneManagement;
#endif

namespace Voodoo.UI.Particles
{
	/// <summary>
	/// </summary>
	[ExecuteInEditMode]
	public class UIParticleOverlayCamera : MonoBehaviour
	{
		/// <summary>
		/// Get instance object.
		/// If instance does not exist, Find instance in scene, or create new one.
		/// </summary>
		public static UIParticleOverlayCamera instance
		{
			get
			{
#if UNITY_EDITOR
				// If current scene is prefab mode, create OverlayCamera for editor.
				//var prefabStage = PrefabStageUtility.GetCurrentPrefabStage ();
				//if (prefabStage != null && prefabStage.scene.isLoaded)
				//{
				//	if (!s_InstanceForPrefabMode)
				//	{
				//		// This GameObject is not saved in prefab.
				//		// This GameObject is not shown in the hierarchy view.
				//		// When you exit prefab mode, this GameObject is destroyed automatically.
				//		GameObject go = new GameObject (nameof(UIParticleOverlayCamera) + "_ForEditor")
				//		{
				//			hideFlags = HideFlags.HideAndDontSave,
				//			tag = "EditorOnly",
				//		};
				//		SceneManager.MoveGameObjectToScene (go, prefabStage.scene);
				//		s_InstanceForPrefabMode = go.AddComponent<UIParticleOverlayCamera> ();
				//	}
				//	return s_InstanceForPrefabMode;
				//}
#endif

				// Find instance in scene, or create new one.
				if (ReferenceEquals (s_Instance, null))
				{
					s_Instance = FindObjectOfType<UIParticleOverlayCamera> () ?? new GameObject (nameof(UIParticleOverlayCamera), typeof (UIParticleOverlayCamera)).GetComponent<UIParticleOverlayCamera> ();
					s_Instance.gameObject.SetActive (true);
					s_Instance.enabled = true;
				}
				return s_Instance;
			}
		}

		public static Camera GetCameraForOverlay (Canvas canvas)
		{
			UIParticleOverlayCamera i = instance;
			RectTransform rt = canvas.rootCanvas.transform as RectTransform;
			Camera cam = i.cameraForOvrelay;
			Transform trans = i.transform;
			cam.enabled = false;

			Vector3 pos = rt.localPosition;
			cam.orthographic = true;
			cam.orthographicSize = Mathf.Max (pos.x, pos.y);
			cam.nearClipPlane = 0.3f;
			cam.farClipPlane = 1000f;
			pos.z -= 100;
			trans.localPosition = pos;

			return cam;
		}

		private Camera cameraForOvrelay => m_Camera ? m_Camera : (m_Camera = GetComponent<Camera>()) ? m_Camera : (m_Camera = gameObject.AddComponent<Camera>());
		private Camera m_Camera;
		private static UIParticleOverlayCamera s_Instance;
#if UNITY_EDITOR
		private static UIParticleOverlayCamera s_InstanceForPrefabMode;
#endif

		/// <summary>
		/// Awake is called when the script instance is being loaded.
		/// </summary>
		private void Awake ()
		{
#if UNITY_EDITOR
			// OverlayCamera for editor.
			if (hideFlags == HideFlags.HideAndDontSave || s_InstanceForPrefabMode == this)
			{
				s_InstanceForPrefabMode = GetComponent<UIParticleOverlayCamera> ();
				return;
			}
#endif

			// Hold the instance.
			if (s_Instance == null)
			{
				s_Instance = GetComponent<UIParticleOverlayCamera> ();
			}
			// If the instance is duplicated, destroy itself.
			else if (s_Instance != this)
			{
				Debug.LogWarning ("Multiple " + nameof(UIParticleOverlayCamera) + " in scene.", gameObject);
				enabled = false;
#if UNITY_EDITOR

				if (!Application.isPlaying)
				{
					DestroyImmediate (gameObject);
				}
				else
#endif
				{
					Destroy (gameObject);
				}
				return;
			}

			cameraForOvrelay.enabled = false;

			// Singleton has DontDestroy flag.
			if (Application.isPlaying)
			{
				DontDestroyOnLoad (gameObject);
			}
		}

		/// <summary>
		/// This function is called when the MonoBehaviour will be destroyed.
		/// </summary>
		private void OnDestroy ()
		{
#if UNITY_EDITOR
			if (s_InstanceForPrefabMode == this)
			{
				s_InstanceForPrefabMode = null;
			}
#endif

			// Clear instance on destroy.
			if (s_Instance == this)
			{
				s_Instance = null;
			}
		}
	}
}