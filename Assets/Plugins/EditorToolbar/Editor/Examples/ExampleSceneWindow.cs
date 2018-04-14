using UnityEngine;

namespace FantasticYes.Tools
{
	[AddToToolbar ("SceneWindow", "Editor Scene Viewport Window", ToolbarGroup.Art)]
	public class ExampleSceneWindow : SceneWindow
	{
		public override void WindowGUI (int windowID)
		{
			if (GUI.Button (new Rect (10, 20, 100, 20), "Window1"))
			{
				Debug.Log ("Got a click");
			}
		}

		protected override Vector2 GetWindowSize ()
		{
			return new Vector2 (500, 400);
		}

		protected override string GetWindowTitle ()
		{
			return "EditorSceneWindow";
		}
	}
}