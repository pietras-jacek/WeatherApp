using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Net.Http;
using GalaSoft.MvvmLight.Command;

namespace WeatherApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        public RelayCommand SearchAndUpdateCommand { get; private set; }

        /// <summary>
        /// The <see cref="City" /> property's name.
        /// </summary>
        public const string CityPropertyName = "City";

        private string _city = "";

        /// <summary>
        /// Sets and gets the City property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string City
        {
            get
            {
                return _city;
            }

            set
            {
                if (_city == value)
                {
                    return;
                }

                _city = value;
                RaisePropertyChanged(CityPropertyName);
            }
        }

        
        /// <summary>
        /// The <see cref="SelectedDay" /> property's name.
        /// </summary>
        public const string SelectedDayPropertyName = "SelectedDay";

        private DayElem _sulectedDay = null;

        /// <summary>
        /// Sets and gets the SelectedDay property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DayElem SelectedDay
        {
            get
            {
                return _sulectedDay;
            }

            set
            {
                if (_sulectedDay == value)
                {
                    return;
                }

                _sulectedDay = value;
                RaisePropertyChanged(SelectedDayPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="WeatherList" /> property's name.
        /// </summary>
        public const string WeatherListPropertyName = "WeatherList";

        private ObservableCollection<DayElem> _myProperty = null;

        /// <summary>
        /// Sets and gets the WeatherList property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<DayElem> WeatherList
        {
            get
            {
                return _myProperty;
            }

            set
            {
                if (_myProperty == value)
                {
                    return;
                }

                _myProperty = value;
                RaisePropertyChanged(WeatherListPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Root" /> property's name.
        /// </summary>
        public const string RootPropertyName = "Root";

        private RootObject _root = null;

        /// <summary>
        /// Sets and gets the Root property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public RootObject Root
        {
            get
            {
                return _root;
            }

            set
            {
                if (_root == value)
                {
                    return;
                }

                _root = value;
                RaisePropertyChanged(RootPropertyName);
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            SearchAndUpdateCommand = new RelayCommand(SearchAndUpdateMethod);

            if (IsInDesignMode)
            {
                WeatherList = new ObservableCollection<DayElem>();
                WeatherList.Add(new DayElem { temp = new Temp { day = 7, max = 5, min = 10 }, Date = DateTime.Now, weather = new System.Collections.Generic.List<Weather> { new Weather { icon = "01d", description = "light snow" } } });
                WeatherList.Add(new DayElem { temp = new Temp { day = 8, max = 4, min = 12 }, Date = DateTime.Now.AddDays(1), weather = new System.Collections.Generic.List<Weather> { new Weather { icon = "02d", description = "light snow" } } });
                WeatherList.Add(new DayElem { temp = new Temp { day = 9, max = 8, min = 13 }, Date = DateTime.Now.AddDays(2), weather = new System.Collections.Generic.List<Weather> { new Weather { icon = "03d", description = "light snow" } } });
                WeatherList.Add(new DayElem { temp = new Temp { day = 10, max = 9, min = 14 }, Date = DateTime.Now.AddDays(3), weather = new System.Collections.Generic.List<Weather> { new Weather { icon = "04d", description = "light snow" } } });
                WeatherList.Add(new DayElem { temp = new Temp { day = 11, max = 3, min = 15 }, Date = DateTime.Now.AddDays(4), weather = new System.Collections.Generic.List<Weather> { new Weather { icon = "10d", description = "light snow" } } });
                WeatherList.Add(new DayElem { temp = new Temp { day = 12, max = 2, min = 16 }, Date = DateTime.Now.AddDays(4), weather = new System.Collections.Generic.List<Weather> { new Weather { icon = "13d", description = "light snow" } } });
                SelectedDay = WeatherList[0];
                Root = new RootObject();
                Root.city = new City();
                Root.city.name = "Gdansk";
                Root.city.country = "PL";
            }
            else
            {
                UpdateWeather("Gdansk");
            }
        }

        const int CNT = 16;

        public void UpdateWeather(string city)
        {
            string url = String.Format("http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&cnt={1}&units=metric&mode=json&APPID={2}", city, CNT, WeatherApiClient.GetAPIKey());
            Task<RootObject> t = Task.Run(() => WeatherApiClient.GetWeatherForecast(url));
            t.Wait();
            RootObject r = t.Result;
            if (r != null)
            { 
                WeatherList = new ObservableCollection<DayElem>(r.list);
                SelectedDay = WeatherList[0];
                Root = r;
            }
        }

        public void SearchAndUpdateMethod()
        {
            UpdateWeather(City);
        }
    }

}
