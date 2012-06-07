using System.Collections.ObjectModel;
using Caliburn.Micro;
using TwitterServiceLibrary;

namespace TwitterCaliburn.ViewModels
{
    public class MainViewModel : PropertyChangedBase, ITwitterViewModel
    {
        private readonly ITwitterRepository _repository;

        public MainViewModel(ITwitterRepository repository)
        {
            _repository = repository;
            _nextTweet = "Write your next tweet here.";
            BuildTweetsFromAllUsers();
        }

        // backing stores
        private string _nextTweet;
        private User _selectedUser;
        private ObservableCollection<TweetFromUser> _tweets = new ObservableCollection<TweetFromUser>();
        private ObservableCollection<TweetFromUser> _tweetsFromSelectedUser = new ObservableCollection<TweetFromUser>();
        private TweetFromUser _selectedTweet;

        // properties the View binds to
        public string NextTweet
        {
            get { return _nextTweet; }
            set
            {
                _nextTweet = value;
                NotifyOfPropertyChange(() => NextTweet);
            }
        }

        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                NotifyOfPropertyChange(() => SelectedUser);
            }
        }

        public ObservableCollection<TweetFromUser> Tweets
        {
            get { return _tweets; }
        }

        public ObservableCollection<TweetFromUser> TweetsFromSelectedUser
        {
            get { return _tweetsFromSelectedUser; }
        }

        public TweetFromUser SelectedTweet
        {
            get { return _selectedTweet; }
            set
            {
                _selectedTweet = value;
                BuildTweetsOfSelectedUser();
                NotifyOfPropertyChange(() => SelectedTweet);
            }
        }

        public bool CanSendNextTweet
        {
            get { return !string.IsNullOrWhiteSpace(_nextTweet); }
        }

        public void SendNextTweet()
        {
            _repository.Save(new TweetFromUser { TweetText = _nextTweet});
        }

        void BuildTweetsFromAllUsers()
        {
            Tweets.Clear();
            var tweets = _repository.GetAll();
            foreach (var tweetFromUser in tweets)
            {
                Tweets.Add(tweetFromUser);
            }
        }

        void BuildTweetsOfSelectedUser()
        {
            SelectedUser = _selectedTweet.User;
            TweetsFromSelectedUser.Clear();
            var tweetsFromSelectedUser = _repository.GetTweetsByUserId(_selectedUser.UserID);

            foreach (var tweetFromUser in tweetsFromSelectedUser)
            {
                TweetsFromSelectedUser.Add(tweetFromUser);
            }
        }
    }

}