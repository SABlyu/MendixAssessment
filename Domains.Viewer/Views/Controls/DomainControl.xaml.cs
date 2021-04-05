using Domains.Common.Models.Bindable;
using Domains.InterfaceTools.Tools;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Domains.Viewer.Views.Controls
{
    /// <summary>
    /// Interaction logic for DomainControl.xaml
    /// </summary>
    public partial class DomainControl : UserControl
    {
        public DomainControl()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty EditCommandProperty = DependencyProperty.Register(
            nameof(EditCommand), typeof(ICommand), typeof(DomainControl));
        
        public static readonly DependencyProperty DomainProperty = DependencyProperty.Register(
            nameof(Domain), typeof(BindableEntity), typeof(DomainControl));
        
        public static readonly DependencyProperty SaveActionProperty = DependencyProperty.Register(
            nameof(SaveAction), typeof(Action<BindableEntity>), typeof(DomainControl));


        public ICommand EditCommand
        {
            get => (ICommand)GetValue(EditCommandProperty);
            set => SetValue(EditCommandProperty, value);
        }

        public BindableEntity Domain
        {
            get => (BindableEntity)GetValue(DomainProperty);
            set => SetValue(DomainProperty, value);
        }

        public Action<BindableEntity> SaveAction
        {
            get => (Action<BindableEntity>)GetValue(SaveActionProperty);
            set => SetValue(SaveActionProperty, value);
        }


        private void OnDragStarted(object sender, MouseButtonEventArgs e)
        {
            _isDragged = true;
            var parent = TreeHelper.FindAncestor<ItemsControl>(ControlRoot);
            _lastPosition = e.GetPosition(parent);
        }

        private void OnDragEnded(object sender, MouseButtonEventArgs e)
        {
            _isDragged = false;
            if (_hasBeenDragged)
                SaveAction?.Invoke(Domain);
            _hasBeenDragged = false;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDragged)
                return;
            var parent = TreeHelper.FindAncestor<ItemsControl>(ControlRoot);
            var parentPosition = e.GetPosition(parent);
            
            var shift = new Point(parentPosition.X - _lastPosition.X, parentPosition.Y - _lastPosition.Y);

            if (Math.Abs(shift.X) != 0 || Math.Abs(shift.Y) != 0)
            {
                Domain.X += (int)shift.X;
                Domain.Y += (int)shift.Y;
                _hasBeenDragged = true;
            }

            _lastPosition = parentPosition;
        }


        private bool _isDragged;
        private Point _lastPosition;
        private volatile bool _hasBeenDragged;
    }
}
