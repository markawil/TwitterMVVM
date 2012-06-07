using System;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using TwitterMVVM.ViewModels;
using TwitterServiceLibrary;

namespace TwitterMVVM.Tests
{
   [TestFixture]
   public class TwitterViewModelTests
   {//SUT
      private TwitterViewModel _vm;

      // Dependency
      private ITwitterRepository _repo;


      [SetUp]
      public void Setup()
      {
         _repo = MockRepository.GenerateMock<ITwitterRepository>();

         // Stub list to return when GetAll or GetPredicate is called
         var list = new List<TweetFromUser>()
                       {
                          new TweetFromUser
                             {
                                TweetText = "This is my tweet",
                                User = new User
                                {
                                  Name = "mark"
                                }
                             },
                          new TweetFromUser
                             {
                                TweetText = "This is Tom's tweet"
                                , User = new User
                                {
                                  Name = "Tom"
                                }
                             },
                          new TweetFromUser
                             {
                                TweetText = "This is another tweet"
                                , User = new User
                                {
                                  Name = "mark"
                                }
                             }
                       };

         _repo.Stub(d => d.GetAll()).Return(list);

         // when they use UserName == "mark"
       //  _repo.Stub(d => d.GetTweetsByScreenName("mark")).Return(list.Where(d => d.User.Name == "mark"));

         _vm = new TwitterViewModel(_repo);

      }

      [Test]
      public void Tweets_returns_tweets_from_users()
      {
         var tweets = _vm.Tweets;
         _repo.AssertWasCalled(d => d.GetAll());
         Assert.AreNotEqual(0, tweets.Count);
         Console.Out.WriteLine(tweets[0].TweetText);
      }

//      [Test]
//      public void Users_should_get_all_users_available()
//      {
//         var users = _vm.Users;
//         Assert.AreEqual(3, users.Count);
//      }
//
//      [Test]
//      public void When_SelectedUser_changed_should_get_only_tweets_from_that_user()
//      {
//         _vm.SelectedUser = "mark";
//         var tweetsFromMark = _vm.Tweets;
//         Assert.AreEqual(2, tweetsFromMark.Count);
//      }

   }
}