/// Date	: 14/04/2018
/// Company	: Fantastic, yes
/// Author	: Maximilian Rötzer
/// License	: This code is licensed under MIT license

using UnityEditor;
using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;

namespace FantasticYes.Tools
{
	public class ToolbarWindow : EditorWindow, IHasCustomMenu
	{
		#region Fields
		private string m_path;
		private ToolbarGroup m_group;
		private List<ToolbarControl> m_controls;
		#endregion

		private void PopulateControls (ToolbarGroup group)
		{
			foreach (Type type in Assembly.GetExecutingAssembly ().GetTypes ())
			{
				AddToToolbarAttribute attribute = Attribute.GetCustomAttribute (type, typeof (AddToToolbarAttribute)) as AddToToolbarAttribute;

				// Skip types without attribute and that don't match category
				if (attribute == null || (group & attribute.Group) != attribute.Group)
				{
					continue;
				}

				// Skip types that are already in the list or not properly defined
				if (m_controls.Find (x => x.name == "" || x.name == attribute.Name))
				{
					Debug.LogWarning ("[EditorToolbar] Duplicate or invalid toolbar attribute name: " + attribute.Name);

					continue;
				}

				if (type.BaseType == typeof (EditorWindow))
				{
					ToggleEditorWindow control = CreateInstance<ToggleEditorWindow> ();

					control.name = attribute.Name;
					control.Init (m_path, type, attribute.Name, attribute.Tooltip, attribute.Order, attribute.Group);

					m_controls.Add (control);
				}
				else if (type.BaseType == typeof (SceneWindow))
				{
					ToggleSceneWindow control = CreateInstance<ToggleSceneWindow> ();

					control.name = attribute.Name;
					control.Init (m_path, type, attribute.Name, attribute.Tooltip, attribute.Order, attribute.Group);

					m_controls.Add (control);
				}
				else
				{
					ToolbarControl control = (ToolbarControl) CreateInstance (type);

					control.name = attribute.Name;
					control.Init (m_path, attribute.Name, attribute.Tooltip, attribute.Order, attribute.Group);

					m_controls.Add (control);
				}
			}

			// Sort controls by order
			m_controls.Sort ((x, y) => (x.Order).CompareTo (y.Order));
		}

		/// <summary>
		/// Dispose controls and remove them from the list.
		/// </summary>
		private void DisposeControls (ToolbarGroup group)
		{
			for (int i = m_controls.Count - 1; i >= 0; i--)
			{
				ToolbarControl control = m_controls [i];

				if ((group & control.Group) == control.Group)
				{
					DestroyImmediate (control);
					m_controls.RemoveAt (i);
				}
			}
		}

		#region Custom Menu
		/// <summary>
		/// 
		/// </summary>
		/// <param name="menu"></param>
		private void ShowAll ()
		{
			m_group = ToolbarGroup.All;
			PopulateControls (m_group);
		}

		private void ShowArt ()
		{
			m_group ^= ToolbarGroup.Art;

			DisposeControls (~m_group);
			PopulateControls (m_group);
		}

		private void ShowCode ()
		{
			m_group ^= ToolbarGroup.Code;

			DisposeControls (~m_group);
			PopulateControls (m_group);
		}

		private void ShowDesign ()
		{
			m_group ^= ToolbarGroup.Design;

			DisposeControls (~m_group);
			PopulateControls (m_group);
		}

		private void Reload ()
		{
			DisposeControls (ToolbarGroup.All);
			PopulateControls (m_group);
		}

		public void AddItemsToMenu (GenericMenu menu)
		{
			menu.AddItem (new GUIContent ("Show/All"), false, ShowAll);
			menu.AddItem (new GUIContent ("Show/Art"), (m_group & ToolbarGroup.Art) == ToolbarGroup.Art, ShowArt);
			menu.AddItem (new GUIContent ("Show/Code"), (m_group & ToolbarGroup.Code) == ToolbarGroup.Code, ShowCode);
			menu.AddItem (new GUIContent ("Show/Design"), (m_group & ToolbarGroup.Design) == ToolbarGroup.Design, ShowDesign);

			menu.AddSeparator (string.Empty);
			menu.AddItem (new GUIContent ("Reload"), false, Reload);
		}
		#endregion

		#region Unity Lifetime Calls
		/// <summary>
		/// Call all controls GUI methods in the toolbar.
		/// </summary>
		private void OnGUI ()
		{
			float x = 4;
			float y = 4;
			float minWidth = 0;

			foreach (ToolbarControl control in m_controls)
			{
				if (x + control.Width > EditorGUIUtility.currentViewWidth)
				{
					x = 4;
					y += 26;
				}

				if (control.Width > minWidth)
				{
					minWidth = control.Width + 8;
				}

				control.Draw (new Rect (x, y, control.Width, 22));
				x += control.Width + 4;
			}

			minSize = new Vector2 (minWidth, 30);
		}

		/// <summary>
		/// Enable the toolbar window.
		/// </summary>
		private void OnEnable ()
		{
			if (m_controls == null)
			{
				m_group = ToolbarGroup.All;
				minSize = new Vector2 (30, 30);
				m_controls = new List<ToolbarControl> ();
				titleContent = new GUIContent ("Toolbar", "A custom toolbar container.");

				// Load toolbar group
				if (EditorPrefs.HasKey ("ToolbarGroup"))
				{
					m_group = (ToolbarGroup) EditorPrefs.GetInt ("ToolbarGroup");
				}

				m_path = AssetUtility.FindAssetPath ("EditorToolbar", "t:Folder");

				PopulateControls (m_group);
			}
		}

		/// <summary>
		/// Handles the closing of the toolbar window.
		/// </summary>
		private void OnDestroy ()
		{
			DisposeControls (ToolbarGroup.All);
			EditorPrefs.SetInt ("ToolbarGroup", (int) m_group);
		}
		#endregion

		[MenuItem ("Window/EditorToolbar")]
		static void Init ()
		{
			GetWindow<ToolbarWindow> ().Show ();
		}
	}
}