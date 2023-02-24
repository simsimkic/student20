using Model.ManagerModel;
using Model.UtilityModel;
using System;

namespace ProjekatBolnica.Backend.Repository.CSV.Converter.ManagerConverter
{
    class RoomsCSVConverter : ICSVConverter<Room>
    {
        private readonly String _delimiter;
        private readonly String _datetimeFormat;

        public RoomsCSVConverter(String delimiter, String datetimeFormat)
        {
            _delimiter = delimiter;
            _datetimeFormat = datetimeFormat;
        }

        public Room ConvertCSVFormatToEntity(String entityCSVFormat)
        {
            String[] tokens = entityCSVFormat.Split(_delimiter.ToCharArray());
            TimePeriod renovation = new TimePeriod();
            renovation.StartTime = DateTime.Parse(tokens[1]);
            renovation.EndTime = DateTime.Parse(tokens[2]);
            FunctionOfRoom function = (FunctionOfRoom)Enum.Parse(typeof(FunctionOfRoom), tokens[4]);

            return new Room(
                long.Parse(tokens[0]),
                renovation,
                tokens[3], function);
        }

        public String ConvertEntityToCSVFormat(Room entity)
            => String.Join(_delimiter,
                   entity.Id,
                   entity.Renovation.StartTime.ToString(_datetimeFormat),
                   entity.Renovation.EndTime.ToString(_datetimeFormat),
                   entity.Name,
                   entity.Function);
    }
}
