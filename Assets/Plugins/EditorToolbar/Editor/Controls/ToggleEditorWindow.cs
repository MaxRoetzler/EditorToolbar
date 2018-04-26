/// Date	: 14/04/2018
/// Company	: Fantastic, yes
/// Author	: Maximilian Rötzler
/// License	: This code is licensed under MIT license

using UnityEngine;
using UnityEditor;
using System;

namespace FantasticYes.Tools
{
	public class ToggleEditorWindow : ToolbarControl
	{
		[SerializeField]
		private bool m_state;
		[SerializeField]
		private string m_type;
		[SerializeField]
		private Texture m_icon;

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
			m_type = type.AssemblyQualifiedName;
			m_label = new GUIContent (m_icon, tooltip);

			hideFlags = HideFlags.HideAndDontSave;
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
					GetWindow ().Show ();
				}
				else
				{
					GetWindow ().Close ();
				}
			}
		}

		private EditorWindow GetWindow ()
		{
			return EditorWindow.GetWindow (Type.GetType (m_type));
		}

		/// <summary>
		/// Handles the object destruction, close window.
		/// </summary>
		private void OnDestroy ()
		{
			GetWindow ().Close ();
		}
	}
}