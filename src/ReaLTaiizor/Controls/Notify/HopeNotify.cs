﻿#region Imports

using ReaLTaiizor.Colors;
using ReaLTaiizor.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region HopeNotify

    public class HopeNotify : Control
    {
        #region Variables

        private bool _Close = true;

        private readonly Color _DefaultBackColor = HopeColors.PrimaryColor;
        private readonly Color _DefaultTextColor = HopeColors.PrimaryColor;

        private Color _SuccessBackColor = Color.FromArgb(25, HopeColors.Success);
        private Color _SuccessTextColor = HopeColors.Success;
        private Color _WarningBackColor = Color.FromArgb(25, HopeColors.Warning);
        private Color _WarningTextColor = HopeColors.Warning;
        private Color _InfoBackColor = Color.FromArgb(25, HopeColors.Info);
        private Color _InfoTextColor = HopeColors.Info;
        private Color _ErrorBackColor = Color.FromArgb(25, HopeColors.Danger);
        private Color _ErrorTextColor = HopeColors.Danger;

        private Color _CloseColor = RoundRectangle.DarkBackColor;

        public enum AlertType
        {
            Success = 0,
            Warning = 1,
            Info = 2,
            Error = 3
        };
        #endregion

        #region Settings

        [RefreshProperties(RefreshProperties.Repaint)]
        public AlertType Type { get; set; } = AlertType.Success;

        private Timer _timer;
        private Timer _Timer
        {
            get => _timer;
            set
            {
                if (_timer != null)
                {
                    _timer.Tick -= Timer_Tick;
                }

                _timer = value;
                if (_timer != null)
                {
                    _timer.Tick += Timer_Tick;
                }
            }
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        public bool Close
        {
            get => _Close;
            set => _Close = value;
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color SuccessBackColor
        {
            get => _SuccessBackColor;
            set => _SuccessBackColor = value;
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color SuccessTextColor
        {
            get => _SuccessTextColor;
            set => _SuccessTextColor = value;
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color WarningBackColor
        {
            get => _WarningBackColor;
            set => _WarningBackColor = value;
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color WarningTextColor
        {
            get => _WarningTextColor;
            set => _WarningTextColor = value;
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color InfoBackColor
        {
            get => _InfoBackColor;
            set => _InfoBackColor = value;
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color InfoTextColor
        {
            get => _InfoTextColor;
            set => _InfoTextColor = value;
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color ErrorBackColor
        {
            get => _ErrorBackColor;
            set => _ErrorBackColor = value;
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color ErrorTextColor
        {
            get => _ErrorTextColor;
            set => _ErrorTextColor = value;
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color CloseColor
        {
            get => _CloseColor;
            set => _CloseColor = value;
        }

        #endregion

        #region Events
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = 34;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (_Close)
            {
                Visible = false;
            }
        }
        #endregion

        #region OnPaint

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.Clear(Parent.BackColor);

            SolidBrush backBrush = new(_DefaultBackColor);
            SolidBrush textBrush = new(_DefaultTextColor);
            switch (Type)
            {
                case AlertType.Success:
                    backBrush = new(_SuccessBackColor);
                    textBrush = new(_SuccessTextColor);
                    break;
                case AlertType.Warning:
                    backBrush = new(_WarningBackColor);
                    textBrush = new(_WarningTextColor);
                    break;
                case AlertType.Info:
                    backBrush = new(_InfoBackColor);
                    textBrush = new(_InfoTextColor);
                    break;
                case AlertType.Error:
                    backBrush = new(_ErrorBackColor);
                    textBrush = new(_ErrorTextColor);
                    break;
                default:
                    break;
            }

            GraphicsPath back = RoundRectangle.CreateRoundRect(0.5f, 0.5f, Width - 1, Height - 1, 3);
            graphics.FillPath(new SolidBrush(Color.White), back);
            graphics.FillPath(backBrush, back);
            graphics.DrawPath(new(textBrush, 1f), back);
            if (_Close)
            {
                graphics.DrawString(Text, Font, textBrush, new RectangleF(20, 0, Width - 40, Height), HopeStringAlign.Left);
                graphics.DrawString("r", new Font("Marlett", 10), new SolidBrush(_CloseColor), new Rectangle(Width - 34, 1, 34, 34), HopeStringAlign.Center);
            }
            else
            {
                graphics.DrawString(Text, Font, textBrush, new RectangleF(20, 0, Width - 35, Height), HopeStringAlign.Left);
            }
        }

        #endregion

        public HopeNotify()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            Font = new("Segoe UI", 12);
            Width = 150;
            Cursor = Cursors.Hand;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_Close)
            {
                Visible = false;
            }

            _Timer.Enabled = false;
            _Timer.Dispose();
        }

        /// <summary>
        /// How to use: HopeNotify.ShowAlertBox(Type, String, Interval)
        /// </summary>

        public void ShowAlertBox(AlertType type, string text, int Interval)
        {
            Type = type;
            Text = text;
            Visible = true;
            _Timer = new Timer
            {
                Interval = Interval,
                Enabled = true
            };
        }
    }

    #endregion
}