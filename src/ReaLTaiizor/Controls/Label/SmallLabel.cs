﻿#region Imports

using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor
{
    #region SmallLabel

    public class SmallLabel : Label
    {
        public SmallLabel()
        {
            Font = new Font("Segoe UI", 8);
            ForeColor = Color.FromArgb(142, 142, 142);
            BackColor = Color.Transparent;
        }
    }

    #endregion
}