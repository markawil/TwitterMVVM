using System;
using System.IO;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;

namespace TwitterMVVM.Tests
{
   [TestFixture]
   public class IoCWindsorTests
   {
      private IWindsorContainer _container;

      private IMyCar _myCar;
      private IEngine _engine;
      private IFuelType _fuelType;

      [SetUp]
      public void Setup()
      {
         _container = new WindsorContainer();
         _container.Register(Component.For<IEngine>().ImplementedBy<BMWI6Engine>().ServiceOverrides(
            ServiceOverride.ForKey("fuelType").Eq("90")).Named("BMWEngine"));
         _container.Register(Component.For<IEngine>().ImplementedBy<HondaV6Engine>().ServiceOverrides(
            ServiceOverride.ForKey("fuelType").Eq("87")).Named("HondaEngine"));

         _container.Register(Component.For<IFuelType>().ImplementedBy<FuelType90>().Named("90"));
         _container.Register(Component.For<IFuelType>().ImplementedBy<FuelType87>().Named("87"));
         _container.Register(Component.For<IMyCar>().ImplementedBy<MyCar>().ServiceOverrides(
            ServiceOverride.ForKey("engine").Eq("HondaEngine")));

         _myCar = _container.Resolve<IMyCar>();
      }

      [Test]
      public void MyCar_should_return_octane_fuel_type()
      {
         // should have resolved the fuel type dependency as the 90
         _myCar.GetFuelType().ShouldBe("87");
      }

      [Test]
      public void MyCar_should_return_engineHorsepower()
      {
         _myCar.GetEngineHorsePower().ShouldBe("220");
      }

       [Test]
       public void TestReadingCharsFromFile()
       {
           var charReader = new readChars();
           charReader.ReadCharsInFile(@"C:\rigviewid.txt").ShouldBe(2);
       }
   }

   public interface IMyCar
   {
      string GetFuelType();
      string GetEngineHorsePower();
   }

   public class MyCar : IMyCar
   {
      private readonly IEngine _engine;
      
      public MyCar(IEngine engine)
      {
         _engine = engine;
      }

      public string GetFuelType()
      {
         return _engine.FuelType;
      }

      public string GetEngineHorsePower()
      {
         return _engine.EngineHorsePower;
      }
   }

   public interface IEngine
   {
      string EngineHorsePower { get; }
      string FuelType { get; }
   }

   class HondaV6Engine : IEngine
   {
      private readonly IFuelType _fuelType;

      public HondaV6Engine(IFuelType fuelType)
      {
         _fuelType = fuelType;
      }

      public string EngineHorsePower
      {
         get { return "220"; }
      }

      public string FuelType
      {
         get { return _fuelType.OctaneType; }
      }
   }

   public class BMWI6Engine : IEngine
   {
      private readonly IFuelType _fuelType;

      public BMWI6Engine(IFuelType fuelType)
      {
         _fuelType = fuelType;
      }

      public string EngineHorsePower
      {
         get { return "240"; }
      }

      public string FuelType
      {
         get { return _fuelType.OctaneType; }
      }
   }

   public interface IFuelType
   {
      string OctaneType { get; }
   }

   class FuelType90 : IFuelType
   {
      public string OctaneType
      {
         get { return "90"; }
      }
   }

   class FuelType87 : IFuelType
   {
      public string OctaneType
      {
         get { return "87"; }
      }
   }

   internal class readChars
   {
       public int ReadCharsInFile(string filePath)
       {
           int count = 0;
           using (var sr = new StreamReader(filePath))
           {
               while (sr.Read() != -1)
                   count++;
           }
           return count;
       }
   }
}