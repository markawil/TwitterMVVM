using Castle.Windsor;

namespace TwitterMVVM
{
   public class IoC
   {
      private static IWindsorContainer _container = new WindsorContainer();

      public static IWindsorContainer GetIoC_Container()
      {
         return _container;
      }
   }
}