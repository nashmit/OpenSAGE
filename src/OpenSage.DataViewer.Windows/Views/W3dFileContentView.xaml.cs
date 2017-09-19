﻿using System;
using System.Windows;
using System.Windows.Controls;
using OpenSage.DataViewer.ViewModels;
using LLGfx.Hosting;
using OpenSage.DataViewer.Framework;
using Caliburn.Micro;

namespace OpenSage.DataViewer.Views
{
    public partial class W3dFileContentView : UserControl
    {
        private bool _isLButtonDown;
        private bool _isMButtonDown;
        private bool _isRButtonDown;

        private Point _previousMousePoint;

        public W3dFileContentView()
        {
            InitializeComponent();

            GraphicsDeviceControl.GraphicsDevice = IoC.Get<GraphicsDeviceManager>().GraphicsDevice;
        }

        private W3dFileContentViewModel TypedDataContext => (W3dFileContentViewModel) DataContext;

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            TypedDataContext.Dispose();
        }

        private void OnGraphicsInitialize(object sender, GraphicsEventArgs e)
        {
            TypedDataContext.Initialize(e.GraphicsDevice, e.SwapChain);
        }

        private void OnGraphicsDraw(object sender, GraphicsEventArgs e)
        {
            TypedDataContext.Draw(e.GraphicsDevice, e.SwapChain);
        }

        private void OnHwndLButtonDown(object sender, HwndMouseEventArgs e)
        {
            GraphicsDeviceControl.CaptureMouse();
            _isLButtonDown = true;

            _previousMousePoint = e.ScreenPosition;
        }

        private void OnHwndLButtonUp(object sender, HwndMouseEventArgs e)
        {
            GraphicsDeviceControl.ReleaseMouseCapture();
            _isLButtonDown = false;
        }

        private void OnHwndMButtonDown(object sender, HwndMouseEventArgs e)
        {
            GraphicsDeviceControl.CaptureMouse();
            _isMButtonDown = true;

            _previousMousePoint = e.ScreenPosition;
        }

        private void OnHwndMButtonUp(object sender, HwndMouseEventArgs e)
        {
            GraphicsDeviceControl.ReleaseMouseCapture();
            _isMButtonDown = false;
        }

        private void OnHwndRButtonDown(object sender, HwndMouseEventArgs e)
        {
            GraphicsDeviceControl.CaptureMouse();
            _isRButtonDown = true;

            _previousMousePoint = e.ScreenPosition;
        }

        private void OnHwndRButtonUp(object sender, HwndMouseEventArgs e)
        {
            GraphicsDeviceControl.ReleaseMouseCapture();
            _isRButtonDown = false;
        }

        private void OnHwndMouseMove(object sender, HwndMouseEventArgs e)
        {
            var delta = _previousMousePoint - e.ScreenPosition;

            if (_isLButtonDown)
            {
                TypedDataContext.Camera.Rotate((float) delta.X, (float) delta.Y);
            }
            else if (_isMButtonDown)
            {
                TypedDataContext.Camera.Zoom((float)delta.Y);
            }
            else if (_isRButtonDown)
            {
                TypedDataContext.Camera.Pan((float) delta.X, (float) delta.Y);
            }

            _previousMousePoint = e.ScreenPosition;
        }
    }
}