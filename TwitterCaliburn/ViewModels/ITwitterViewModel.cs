using System.Collections.ObjectModel;
using TwitterServiceLibrary;

namespace TwitterCaliburn.ViewModels
{
    public interface ITwitterViewModel
    {
        string NextTweet { get; set; }
        User SelectedUser { get; }
        ObservableCollection<TweetFromUser> Tweets { get; }
        ObservableCollection<TweetFromUser> TweetsFromSelectedUser { get; }
        TweetFromUser SelectedTweet { get; set; }
        void SendNextTweet();
        bool CanSendNextTweet { get; }
    }
}