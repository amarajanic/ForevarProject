using ForevarLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForevarLibrary.DataAccess
{
    public class InMemoryDB
    {
        public List<Place> Places;
        public List<Device> MetricsList;

        public InMemoryDB()
        {
            //MetricsList = new List<Device>()
            //{
            //    new Device
            //    {
            //        DeviceId=1,
            //        AirTemp=22.2f,
            //        GroundTemp=21.7f,
            //        DewPoint= 7.3f,
            //        Humidity=44.3f,
            //        PlaceId=1
            //    },
            //    new Device
            //    {
            //        DeviceId=2,
            //        AirTemp=21.2f,
            //        GroundTemp=22.7f,
            //        DewPoint= 8.3f,
            //        Humidity=45.3f,
            //        PlaceId=1
            //    },
            //    new Device
            //    {
            //        DeviceId=3,
            //        AirTemp=19.2f,
            //        GroundTemp=22.7f,
            //        DewPoint= 9.3f,
            //        Humidity=38.3f,
            //        PlaceId=1
            //    },
            //    new Device
            //    {
            //        DeviceId=4,
            //        AirTemp=22.2f,
            //        GroundTemp=21.7f,
            //        DewPoint= 7.3f,
            //        Humidity=44.3f,
            //        PlaceId=2
            //    },
            //    new Device
            //    {
            //        DeviceId=5,
            //        AirTemp=22.2f,
            //        GroundTemp=21.7f,
            //        DewPoint= 7.3f,
            //        Humidity=44.3f,
            //        PlaceId=2
            //    },
            //    new Device
            //    {
            //        DeviceId=6,
            //        AirTemp=24.2f,
            //        GroundTemp=19.2f,
            //        DewPoint= 4.3f,
            //        Humidity=60.9f,
            //        PlaceId=3
            //    },
            //    new Device
            //    {
            //        DeviceId=7,
            //        AirTemp=14.2f,
            //        GroundTemp=17.2f,
            //        DewPoint= 9.3f,
            //        Humidity=73.9f,
            //        PlaceId=4
            //    },
            //     new Device
            //    {
            //        DeviceId=8,
            //        AirTemp=15.2f,
            //        GroundTemp=22.2f,
            //        DewPoint= 10.3f,
            //        Humidity=47.1f,
            //        PlaceId=4
            //    },
            //      new Device
            //    {
            //        DeviceId=9,
            //        AirTemp=24.2f,
            //        GroundTemp=17.2f,
            //        DewPoint= 9.3f,
            //        Humidity=73.1f,
            //        PlaceId=5
            //    },
            //       new Device
            //    {
            //        DeviceId=10,
            //        AirTemp=34.2f,
            //        GroundTemp=27.2f,
            //        DewPoint= 0.3f,
            //        Humidity=0.9f,
            //        PlaceId=6
            //    }

            //};
            //Places = new List<Place>()
            //{
            //    new Place
            //      {
            //        PlaceId="1",
            //        PlaceName="Avenija",
            //        PlaceLat=44.44334,
            //        PlaceLong=34.45654,
            //        AdministrationUnit="Zapad",
            //        CityId="1",
            //        CityName="Mostar",
            //        Metrics =MetricsList.Where(m=>m.GetPlaceId()==1).ToList()
                    
            //      },
            //      new Place
            //      {
            //        PlaceId="2",
            //        PlaceName="Carina",
            //        PlaceLat=34.44334,
            //        PlaceLong=12.45654,
            //        AdministrationUnit="Stari grad",
            //        CityId="1",
            //        CityName="Mostar",
            //        Metrics = MetricsList.Where(m=>m.GetPlaceId()==2).ToList()
                   
            //      },
            //      new Place
            //      {
            //        PlaceId="3",
            //        PlaceName="Tekija",
            //        PlaceLat=45.44334,
            //        PlaceLong=66.45654,
            //        AdministrationUnit="Stari grad",
            //        CityId="1",
            //        CityName="Mostar",
            //        Metrics = MetricsList.Where(m=>m.GetPlaceId()==3).ToList()
            //      },
            //      new Place
            //      {
            //        PlaceId="4",
            //        PlaceName="Carina",
            //        PlaceLat=34.44334,
            //        PlaceLong=12.45654,
            //        AdministrationUnit="Stari grad",
            //        CityId="1",
            //        CityName="Mostar",
            //        Metrics =MetricsList.Where(m=>m.GetPlaceId()==4).ToList()

            //      },
            //      new Place
            //      {
            //        PlaceId="5",
            //        PlaceName="Strijelcevina",
            //        PlaceLat=4.44334,
            //        PlaceLong=25.45654,
            //        AdministrationUnit="Zapad",
            //        CityId="1",
            //        CityName="Mostar",
            //        Metrics =MetricsList.Where(m=>m.GetPlaceId()==5).ToList()
            //      },
            //      new Place
            //      {
            //        PlaceId="6",
            //        PlaceName="Zgoni",
            //        PlaceLat=7.44334,
            //        PlaceLong=56.45654,
            //        AdministrationUnit="Zapad",
            //        CityId="1",
            //        CityName="Mostar",
            //        Metrics =MetricsList.Where(m=>m.GetPlaceId()==6).ToList()
            //      }
            //};

            
        }
    }
}
