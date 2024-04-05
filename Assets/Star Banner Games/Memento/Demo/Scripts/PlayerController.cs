using UnityEngine;

namespace SBG.Memento.Demo
{
	public class PlayerController : MonoBehaviour
	{
		#region MOVEMENT CODE

		[SerializeField] private float moveSpeed = 250;

		private Rigidbody2D _rb2d;

        private void Awake()
		{
			_rb2d = GetComponent<Rigidbody2D>();
		}

		void FixedUpdate()
		{
			Vector2 input = new Vector2();

			//Input Axes could be edited, so this is hardcoded.
			if (Input.GetKey(KeyCode.D)) input += Vector2.right;
			if (Input.GetKey(KeyCode.A)) input += Vector2.left;
			if (Input.GetKey(KeyCode.W)) input += Vector2.up;
			if (Input.GetKey(KeyCode.S)) input += Vector2.down;

			_rb2d.velocity = input.normalized * moveSpeed * Time.fixedDeltaTime;
		}

		#endregion


		private void OnEnable()
		{
			SaveManager.OnBeforeSave += OnSave;
			SaveManager.OnLoadFinished += OnLoad;
		}

		private void OnDisable()
		{
			SaveManager.OnBeforeSave -= OnSave;
			SaveManager.OnLoadFinished -= OnLoad;
		}

		private void OnSave(SaveType fileType)
        {
			if (fileType != SaveType.GameFile) return;

			SaveManager.SetValue("PlayerPos", transform.position);
        }

		private void OnLoad(SaveType fileType)
        {
			if (fileType != SaveType.GameFile) return;

			transform.position = SaveManager.GetVector3("PlayerPos");
        }
    }
}