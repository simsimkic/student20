﻿using ProjekatBolnica.Backend.Repository.CSV.Converter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjekatBolnica.Backend.Repository.CSV.Stream
{
    public class CSVStream<E> : ICSVStream<E> where E : class
    {
        private readonly String _path;
        private readonly ICSVConverter<E> _converter;

        public CSVStream(String path, ICSVConverter<E> converter)
        {
            _path = path;
            _converter = converter;
        }

        public void AppendToFile(E entity)
            => File.AppendAllText(_path,
               _converter.ConvertEntityToCSVFormat(entity) + Environment.NewLine);

        public IEnumerable<E> ReadAll()
            => File.ReadAllLines(_path)
                .Select(_converter.ConvertCSVFormatToEntity)
                .ToList();

        public void SaveAll(IEnumerable<E> entities)
            => WriteAllLinesToFile(
                 entities
                 .Select(_converter.ConvertEntityToCSVFormat)
                 .ToList());

        public void WriteAllLinesToFile(IEnumerable<String> content)
            => File.WriteAllLines(_path, content.ToArray());
    }
}
