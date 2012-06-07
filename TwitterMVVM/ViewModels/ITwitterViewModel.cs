using System.Collections.ObjectModel;
using TwitterServiceLibrary;

namespace TwitterMVVM.ViewModels
{
   public interface ITwitterViewModel
   {
      string NextTweet { get; set; }
      User SelectedUser { get; set; }
      ObservableCollection<TweetFromUser> Tweets { get; }
      ObservableCollection<TweetFromUser> TweetsFromSelectedUser { get; }
      TweetFromUser SelectedTweet { get; set; }
      void SendNextTweet();
   }
}