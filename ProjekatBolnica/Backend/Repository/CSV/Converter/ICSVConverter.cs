using System;

namespace ProjekatBolnica.Backend.Repository.CSV.Converter
{
    public interface ICSVConverter<E> where E : class
    {
        String ConvertEntityToCSVFormat(E entity);

        E ConvertCSVFormatToEntity(String entityCSVFormat);
    }
}
