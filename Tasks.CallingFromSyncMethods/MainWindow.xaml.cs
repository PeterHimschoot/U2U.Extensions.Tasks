using System.Windows;

namespace Tasks.CallingFromSyncMethods
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    // Look at MyViewModel, this file holds nothing interesting
    public MainWindow() => InitializeComponent();

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      this.DataContext = new MyViewModel();
    }
  }
}
