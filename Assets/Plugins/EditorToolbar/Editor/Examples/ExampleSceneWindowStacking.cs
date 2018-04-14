using UnityEngine;

namespace FantasticYes.Tools
{
	[AddToToolbar ("StackWindow", "Editor Scene Viewport Window Stacking", ToolbarGroup.Code)]
	public class ExampleSceneWindowStacking : SceneWindow
	{
		public override void WindowGUI (int windowID)
		{
			GUILayout.Label ("Stacked Window");
		}

		protected override Vector2 GetWindowSize ()
		{
			return new Vector2 (200, 50);
		}
	}
}