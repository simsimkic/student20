using Model.PatientModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatBolnica.Backend.Repository.CSV.Converter.PatientConverter
{
    class QuestionsAndAnswersCSVConverter : ICSVConverter<QuestionsAndAnswers>
    {

        private readonly string _delimiter;

        public QuestionsAndAnswersCSVConverter(string delimiter)
        {
            _delimiter = delimiter;
        }

        public QuestionsAndAnswers ConvertCSVFormatToEntity(string entityCSVFormat)
        {
            String[] tokens = entityCSVFormat.Split(_delimiter.ToCharArray());
            String[] csvList = tokens[2].Split(';');
            List<String> possibleAnswers = csvList.ToList();

            return new QuestionsAndAnswers(
                    long.Parse(tokens[0]),
                    tokens[1],
                    possibleAnswers,
                    tokens[3]
                );
        }

        public string ConvertEntityToCSVFormat(QuestionsAndAnswers entity)
        {
            string csv = String.Join(";", entity.PossibleAnswers.Select(x => x.ToString()).ToArray());
            return  String.Join(_delimiter,
                          entity.Id,
                          entity.Question,
                          csv,
                          entity.SelectedAnswer
              );
        }
    }
}
