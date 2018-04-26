/// Date	: 14/04/2018
/// Company	: Fantastic, yes
/// Author	: Maximilian Rötzler
/// License	: This code is licensed under MIT license

using UnityEngine;

namespace FantasticYes.Tools
{
	public abstract class ToolbarControl : ScriptableObject
	{
		#region Fields
		[SerializeField]
		protected int m_order;
		[SerializeField]
		protected GUIContent m_label;
		[SerializeField]
		protected ToolbarGroup m_group;
		#endregion

		#region Getters
		/// <summary>
		/// The order of the control in the toolbar.
		/// </summary>
		public int Order
		{
			get
			{
				return m_order;
			}
		}

		/// <summary>
		/// The group to filter toolbar controls.
		/// </summary>
		public ToolbarGroup Group
		{
			get
			{
				return m_group;
			}
		}

		/// <summary>
		/// The width of the UI element.
		/// </summary>
		public abstract float Width
		{
			get;
		}
		#endregion

		/// <summary>
		/// Initialize the toolbar control.
		/// </summary>
		/// <param name="label">The button label and tooltip.</param>
		/// <param name="order">The order of the toggle in toolbar, 99 by default.</param>
		/// <param name="group">The toolbar group, ToolbarGroup.Any by default.</param>
		public void Init (string path, string name, string tooltip, int order, ToolbarGroup group)
		{
			m_order = order;
			m_group = group;
			m_label = new GUIContent (name, tooltip);

			hideFlags = HideFlags.HideAndDontSave;
		}

		public abstract void Draw (Rect rect);
	}
}