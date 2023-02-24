using Model.ManagerModel;
using System;

namespace ProjekatBolnica.Backend.Repository.CSV.Converter.ManagerConverter
{
    class ResourcesCSVConverter : ICSVConverter<Resource>
    {
        private readonly String _delimiter;

        public ResourcesCSVConverter(String delimiter)
        {
            _delimiter = delimiter;
        }

        public Resource ConvertCSVFormatToEntity(String entityCSVFormat)
        {
            String[] tokens = entityCSVFormat.Split(_delimiter.ToCharArray());
            Room room = new Room(long.Parse(tokens[3]));
            Resource retVal;
            if(tokens[1].Equals("Bed")) { retVal = new Bed(long.Parse(tokens[0])); } else { retVal = new Resource(); }
            retVal.Name = tokens[1];
            retVal.Amount = int.Parse(tokens[2]);
            retVal.Room = room;
            return retVal;
        }

        public String ConvertEntityToCSVFormat(Resource entity)
            => String.Join(_delimiter,
                   entity.Id,
                   entity.Name,
                   entity.Amount,
                   entity.Room.GetId());
    }
}
