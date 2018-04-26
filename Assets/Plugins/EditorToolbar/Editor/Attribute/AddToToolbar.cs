/// Date	: 14/04/2018
/// Company	: Fantastic, yes
/// Author	: Maximilian Rötzler
/// License	: This code is licensed under MIT license

using System;

namespace FantasticYes.Tools
{
	[AttributeUsage (AttributeTargets.Class)]
	public class AddToToolbarAttribute : Attribute
	{
		#region Fields
		private int m_order;
		private string m_name;
		private string m_tooltip;
		private ToolbarGroup m_group;
		#endregion

		#region Constructors
		public AddToToolbarAttribute (string name, string tooltip, ToolbarGroup group = ToolbarGroup.Any, int order = 99)
		{
			m_name = name;
			m_order = order;
			m_group = group;
			m_tooltip = tooltip;
		}
		#endregion

		#region Getters
		/// <summary>
		/// The order of the element in the toolbar.
		/// </summary>
		public int Order
		{
			get
			{
				return m_order;
			}
		}

		/// <summary>
		/// The name of the control.
		/// </summary>
		public string Name
		{
			get
			{
				return m_name;
			}
		}

		/// <summary>
		/// The tooltip when hovering over the control.
		/// </summary>
		public string Tooltip
		{
			get
			{
				return m_tooltip;
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
		#endregion
	}
}