using NUnit.Framework;

namespace TwitterMVVM.Tests
{
   public static class AssertHelpers
   {
      public static void ShouldBe<T>(this T actual, T expected)
      {
         Assert.AreEqual(expected, actual);
      }

      public static void ShouldNotBe<T>(this T actual, T expected)
      {
         Assert.AreNotEqual(expected, actual);
      }
   }
}