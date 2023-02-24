using Model;
using Model.UtilityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatBolnica.Backend.Repository.CSV.Converter.UserConverter
{
    class NotificationCSVConverter : ICSVConverter<Notification>
    {
        private readonly string _delimiter;
        private readonly string _datetimeFormat;

        public NotificationCSVConverter(string delimiter, string datetimeFormat)
        {
            _delimiter = delimiter;
            _datetimeFormat = datetimeFormat;
        }


        public Notification ConvertCSVFormatToEntity(string entityCSVFormat)
        {
            string[] tokens = entityCSVFormat.Split(_delimiter.ToCharArray());
            Notification retVal = new Notification();
            retVal.Id = long.Parse(tokens[0]);
            retVal.Sender = new User(long.Parse(tokens[1]));
            retVal.Receiver = new User(long.Parse(tokens[2]));
            retVal.Description = tokens[3];
            retVal.Date = DateTime.ParseExact(tokens[4], _datetimeFormat, null);

            return retVal;
        }

        public string ConvertEntityToCSVFormat(Notification entity)
            => string.Join(_delimiter,
               entity.Id,
               entity.Sender.Id,
               entity.Receiver.Id,
               entity.Description,
               entity.Date.ToString(_datetimeFormat));
    }
}
