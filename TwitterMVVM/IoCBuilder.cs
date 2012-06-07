using Castle.MicroKernel.Registration;
using Castle.Windsor;
using TwitterMVVM.ViewModels;
using TwitterServiceLibrary;

namespace TwitterMVVM
{
   public class IoCBuilder
   {
      public IWindsorContainer BuildAndGetContainer()
      {
         var _container = IoC.GetIoC_Container();

         _container.Register(Component.For<ITwitterViewModel>().ImplementedBy<TwitterViewModel>());
         _container.Register(Component.For<ITwitterRepository>().ImplementedBy<TwitterRepositoryLINQtoTwitter>().Named("LINQtoTwitter"));
         _container.Register(Component.For<IView>().ImplementedBy<MainWindow>());

         return _container;
      }
   }
}