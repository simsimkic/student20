using Model.PatientModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace ProjekatBolnica.Backend.Repository.CSV.Converter.PatientConverter
{
    class SurveyCSVConverter:ICSVConverter<Survey>
    {
        private readonly string _delimiter;

        public SurveyCSVConverter(string delimiter)
        {
            _delimiter = delimiter;
        }

        public Survey ConvertCSVFormatToEntity(string entityCSVFormat)
        {
            String[] tokens = entityCSVFormat.Split(_delimiter.ToCharArray());
            List<QuestionsAndAnswers> questionsAndAnswers = new List<QuestionsAndAnswers>();
            String[] csvList = tokens[3].Split(';');

            foreach (String id in csvList)
                questionsAndAnswers.Add(new QuestionsAndAnswers(long.Parse(id)));

            return new Survey(
                    long.Parse(tokens[0]),
                    tokens[1],
                    tokens[2],
                    questionsAndAnswers
                ); 
        }

        public string ConvertEntityToCSVFormat(Survey entity)
        {

            string questionsAndAnswers = String.Join(";", entity.QuestionsAndAnswers.Select(x => x.Id.ToString()).ToArray());

            return String.Join(_delimiter,
                        entity.Id,
                        entity.Name,
                        entity.Description,
                        questionsAndAnswers);
        }
    }
}
