using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DomainModelEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public EntityStore EntityStore { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            EntityStore = new EntityStore();

            var demoData = new[] { new Tuple<int, string, int, int>(1, "Order", 100, 100), new Tuple<int, string, int, int>(2, "OrderLine", 200, 200) };
            EntityStore.Load(demoData);

            var entitiesBinding = new Binding {Source = EntityStore};
            EditorCanvas.SetBinding(ItemsControl.ItemsSourceProperty, entitiesBinding);
        }

        private void AddEntity_Click(object sender, RoutedEventArgs e)
        {
            var popup = new AddEntityDialog();
            popup.ShowDialog();
            if (!string.IsNullOrEmpty(popup.EntityName))
            {
                var randomNrGenerator = new Random();
                EntityStore.Add(popup.EntityName, randomNrGenerator.Next((int)EditorCanvas.ActualWidth - 80), randomNrGenerator.Next((int)EditorCanvas.ActualHeight - 50));
            }
        }
    }
}
