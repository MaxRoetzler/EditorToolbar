using UnityEngine;
using UnityEditor;

namespace FantasticYes.Tools
{
	[AddToToolbar ("EditorWindow", "Example Editor Window", ToolbarGroup.Design)]
	public class ExampleEditorWindow : EditorWindow
	{
		[SerializeField]
		private float m_value;

		public void OnGUI ()
		{
			EditorGUILayout.LabelField ("Editor Window");

			m_value = EditorGUILayout.Slider (new GUIContent ("Value", "Tooltip"), m_value, 0f, 1f);
		}
	}
}