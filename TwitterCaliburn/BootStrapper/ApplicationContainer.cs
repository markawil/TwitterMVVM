using Caliburn.Micro;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using TwitterCaliburn.ViewModels;
using TwitterServiceLibrary;

namespace TwitterCaliburn
{
    public class ApplicationContainer : WindsorContainer
    {
        public ApplicationContainer()
        {
           Register(
              Component.For<IWindowManager>().ImplementedBy<WindowManager>().LifeStyle.Is(LifestyleType.Singleton),
              Component.For<IEventAggregator>().ImplementedBy<EventAggregator>().LifeStyle.Is(LifestyleType.Singleton),
              Component.For<ITwitterRepository>().ImplementedBy<TwitterRepositoryLINQtoTwitter>().Named("LINQtoTwitter"),
              Component.For<ITwitterRepository>().ImplementedBy<TwitterRepositoryTweetSharp>().Named("TweetSharp")
              );

            RegisterViewModels();
        }

        private void RegisterViewModels()
        {
            Register(AllTypes.FromAssembly(GetType().Assembly)
                         .Where(x => x.Name.EndsWith("ViewModel"))
                         .Configure(x => x.LifeStyle.Is(LifestyleType.Transient)));
        }
    }
}