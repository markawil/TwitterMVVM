using System;
using System.Windows;
using TwitterMVVM.ViewModels;

namespace TwitterMVVM
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : IView
   {
      private readonly ITwitterViewModel _vm;

      public MainWindow() : this (IoC.GetIoC_Container().Resolve<ITwitterViewModel>())
      { }

      public MainWindow(ITwitterViewModel vm)
      {
         _vm = vm;
         DataContext = _vm;

         InitializeComponent();
         
         ListBox1.Focus();
      }

      private void txtBox_Tweet_GotFocus(object sender, RoutedEventArgs e)
      {
         txtBox_Tweet.Clear();
      }

      private void btn_Send_Click(object sender, RoutedEventArgs e)
      {
         _vm.SendNextTweet();
      }
   }

   public interface IView
   {
      void Show();
   }
}
