using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using TwitterServiceLibrary;

namespace TwitterMVVM.ViewModels
{
   public class TwitterViewModel : INotifyPropertyChanged, ITwitterViewModel
   {
      private readonly ITwitterRepository _repository;

      public TwitterViewModel(ITwitterRepository repository)
      {
         _repository = repository;
         _nextTweet = "Write your next tweet here.";
         BuildTweetsFromAllUsers();
      }
      
      private string _nextTweet;
      public string NextTweet
      {
         get
         {
            return _nextTweet;
         }
         set
         {
            _nextTweet = value;
            PropertyChanged(this, new PropertyChangedEventArgs("NextTweet"));
         }
      }

      private User _selectedUser;
      public User SelectedUser
      {
         get
         {
            return _selectedUser;
         }
         set
         {
            _selectedUser = value;
            PropertyChanged(this, new PropertyChangedEventArgs("SelectedUser"));
         }
      }

      private ObservableCollection<TweetFromUser> _tweets = new ObservableCollection<TweetFromUser>();
      public ObservableCollection<TweetFromUser> Tweets
      {
         get
         {
            return _tweets;
         }
      }

      private ObservableCollection<TweetFromUser> _tweetsFromSelectedUser = new ObservableCollection<TweetFromUser>();
      public ObservableCollection<TweetFromUser> TweetsFromSelectedUser
      {
         get
         {
            return _tweetsFromSelectedUser;
         }
      }

      private TweetFromUser _selectedTweet;
      public TweetFromUser SelectedTweet
      {
         get
         {
            return _selectedTweet;
         }
         set
         {
            _selectedTweet = value;
            PropertyChanged(this, new PropertyChangedEventArgs("SelectedTweetIndex"));
            BuildTweetsOfSelectedUser();
         } 
      }

      private void BuildTweetsOfSelectedUser()
      {
         SelectedUser = _selectedTweet.User;
         _tweetsFromSelectedUser.Clear();
         var tweetsFromSelectedUser = _repository.GetTweetsByUserId(_selectedUser.UserID);

         foreach (var tweetFromUser in tweetsFromSelectedUser)
         {
            _tweetsFromSelectedUser.Add(tweetFromUser);
         }
      }

      void BuildTweetsFromAllUsers()
      {
         _tweets.Clear();
         var tweets = _repository.GetAll();
         foreach (var tweetFromUser in tweets)
         {
            _tweets.Add(tweetFromUser);
         }
      }

      public void SendNextTweet()
      {
         _repository.Save(new TweetFromUser { TweetText = _nextTweet });

         while (true)
         {
            var tweets = _repository.GetAll();
            foreach (var tweetFromUser in tweets)
            {
               if (tweetFromUser.TweetText.Contains(_nextTweet))
               {
                  BuildTweetsFromAllUsers();
                  return;
               }
            }

            Thread.Sleep(2000);
         }
      }
      public event PropertyChangedEventHandler PropertyChanged = delegate { };
   }

}