/// Date	: 14/04/2018
/// Company	: Fantastic, yes
/// Author	: Maximilian Rötzler
/// License	: This code is licensed under MIT license

using UnityEngine;
using UnityEditor;
using System;

namespace FantasticYes.Tools
{
	public class ToggleSceneWindow : ToolbarControl
	{
		[SerializeField]
		private bool m_state;
		[SerializeField]
		private Texture m_icon;
		[SerializeField]
		private SceneWindow m_window;

		/// <summary>
		/// Initialize the scene window toggle.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="label">The button label and tooltip.</param>
		/// <param name="order">The order of the toggle in toolbar, 99 by default.</param>
		/// <param name="group">The toolbar group, ToolbarGroup.Any by default.</param>
		public void Init (string path, Type type, string name, string tooltip, int order, ToolbarGroup group)
		{
			m_icon = AssetDatabase.LoadAssetAtPath<Texture> (path + "/Editor/Icons/Icon" + name + ".png") as Texture;

			if (m_icon == null)
			{
				m_icon = AssetDatabase.LoadAssetAtPath<Texture> (path + "/Editor/Icons/IconDefault.png") as Texture;
			}

			m_order = order;
			m_group = group;
			m_label = new GUIContent (m_icon, tooltip);

			hideFlags = HideFlags.HideAndDontSave;
			m_window = (SceneWindow) CreateInstance (type);
			m_window.hideFlags = HideFlags.HideAndDontSave;
		}

		public override float Width
		{
			get
			{
				return 28f;
			}
		}

		public override void Draw (Rect rect)
		{
			EditorGUI.BeginChangeCheck ();
			m_state = GUIControls.Toggle (rect, m_state, m_label);

			if (EditorGUI.EndChangeCheck ())
			{
				if (m_state)
				{
					m_window.Show ();
				}
				else
				{
					m_window.Close ();
				}
			}
		}

		/// <summary>
		/// Window loses callback when entering playmode, needs to be updated if opened.
		/// </summary>
		private void OnEnable ()
		{
			if (m_state)
			{
				m_window.Show ();
			}
		}

		/// <summary>
		/// Handles the object destruction, close the window and cleanup.
		/// </summary>
		private void OnDestroy ()
		{
			DestroyImmediate (m_window);
			m_window = null;
		}
	}
}