using UnityEngine;
using UnityEditor;

namespace FantasticYes.Tools
{
	[AddToToolbar ("ExampleControl", "Example Custom Control", ToolbarGroup.Code, 0)]
	public class ExampleControl : ToolbarControl
	{
		[SerializeField]
		private float m_value;

		public override float Width
		{
			get
			{
				return 120;
			}
		}

		public override void Draw (Rect rect)
		{
			m_value = EditorGUI.Slider (rect, GUIContent.none, m_value, 0f, 1f);
		}
	}
}