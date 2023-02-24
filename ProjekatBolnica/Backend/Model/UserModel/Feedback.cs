using ProjekatBolnica.Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatBolnica.Backend.Model.UserModel
{
    public class Feedback : IIdentifiable<long>
    {
        private String _description;

        public long Id { get; set; }
        public string Description { get => _description; set => _description = value; }


        public Feedback(long id, string description)
        {
            Description = description;
            Id = id;
        }

        public Feedback(long id)
        {
            Id = id;
        }

        public Feedback(String message)
        {
            Description = message;
        }


        public long GetId() => Id;

        public void SetId(long id) => Id = id;
    }
}
