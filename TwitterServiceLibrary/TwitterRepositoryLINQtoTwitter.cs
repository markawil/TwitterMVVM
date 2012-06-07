using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using LinqToTwitter;

namespace TwitterServiceLibrary
{
   public class TwitterRepositoryLINQtoTwitter : ITwitterRepository
   {
      #region ITwitterRepository

      public IEnumerable<TweetFromUser> GetAll()
      {
         var myWall = GetHomeStatuses();
         return BuildTweetsFromStatuses(myWall);
      }

      public IEnumerable<TweetFromUser> GetTweetsByUserId(string id)
      {
         var userWall = GetLast20TweetsFromUser(id);
         return BuildLast20TweetsOfUser(userWall);
      }
      
      #endregion

      private TwitterContext GetTwitterContext()
      {
         return new TwitterContext(GetXAuthAuthorizer(), "https://api.twitter.com/1/", "https://search.twitter.com/");
      }

      private IEnumerable<Status> GetHomeStatuses()
      {
         List<Status> statuses;
         using (var twitterCtx = GetTwitterContext())
         {
            statuses = (from tweet in twitterCtx.Status
                          where tweet.Type == StatusType.Friends
                          select tweet).ToList();
         }
         return statuses;
      }

      private IEnumerable<Status> GetLast20TweetsFromUser(string userId)
      {
         IList<Status> statusTweets;
          using (var twitterCtx = GetTwitterContext())
          {
             statusTweets = (from tweet in twitterCtx.Status
                where tweet.Type == StatusType.User
                      && tweet.UserID == userId
                select tweet).ToList();
          }

         return statusTweets;
      }

      private User BuildUserOffUserId(string userId)
      {
         LinqToTwitter.User user;
         using (var twitterCtx = GetTwitterContext())
         {
            user = (from tweet in twitterCtx.User
                        where tweet.Type == UserType.Show &&
                              tweet.UserID == userId
                        select tweet).FirstOrDefault();
         }

         return new User
                   {
                      ImgAddress = user.ProfileImageUrl,
                      Name = user.Name,
                      Information = user.Description,
                      URL = user.URL,
                      UserID = user.UserID,
                      ScreenName = user.ScreenName
                   };
      }

      private XAuthAuthorizer GetXAuthAuthorizer()
      {
         var auth = new XAuthAuthorizer
                       {
                          Credentials = new XAuthCredentials
                                           {
                                              ConsumerKey = ConfigurationSettings.AppSettings["ConsumerKey"],
                                              ConsumerSecret = ConfigurationSettings.AppSettings["ConsumerSecret"],
                                              AccessToken = ConfigurationSettings.AppSettings["TokenSecret"],
                                              OAuthToken = ConfigurationSettings.AppSettings["Token"]
                                           }
                       };

         auth.Authorize();
         return auth;
      }

      private IEnumerable<TweetFromUser> BuildTweetsFromStatuses(IEnumerable<Status> myWall)
      {
         var tweetFromUserList = new List<TweetFromUser>();
         myWall.ToList().ForEach(tweet => tweetFromUserList
                                             .Add(new TweetFromUser
                                                     {
                                                        TweetText = tweet.Text,
                                                        User = BuildUserOffUserId(tweet.User.Identifier.UserID)
                                                     }));

         return tweetFromUserList;
      }
      
      private IEnumerable<TweetFromUser> BuildLast20TweetsOfUser(IEnumerable<Status> tweets)
      {
         var last20TweetsFromUser = new List<TweetFromUser>();
         tweets.ToList().ForEach(tweet => last20TweetsFromUser
            .Add(new TweetFromUser
                    {
                       TweetText = tweet.Text
                    }));
         return last20TweetsFromUser;
      }

      public void Save(TweetFromUser saveThis)
      {
         throw new NotImplementedException();
      }

   }
}