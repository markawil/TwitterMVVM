using System;
using System.Collections.Generic;

namespace TwitterServiceLibrary
{
   public interface IRepository<T>
   {
      IEnumerable<T> GetAll();
      IEnumerable<T> GetTweetsByUserId(string id);
      void Save(T saveThis);
   }
}