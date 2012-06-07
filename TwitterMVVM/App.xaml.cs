using System.Windows;
using Castle.Windsor;

namespace TwitterMVVM
{
   /// <summary>
   /// Interaction logic for App.xaml
   /// </summary>
   public partial class App : Application
   {
      private IWindsorContainer _container;
      private IView _view;

      protected override void OnStartup(StartupEventArgs e)
      {
         base.OnStartup(e);

         var IoCBuilder = new IoCBuilder();
         var container = IoCBuilder.BuildAndGetContainer();
         // we'll do View first (some do view first others do ViewModel first)
         _view = container.Resolve<IView>();
         _view.Show();
      }
   }
}
