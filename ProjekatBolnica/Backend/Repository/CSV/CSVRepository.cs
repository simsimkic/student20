using ProjekatBolnica.Backend.Exception;
using ProjekatBolnica.Backend.Repository.CSV.Stream;
using ProjekatBolnica.Backend.Repository.Sequencer;
using Repository.HospitalRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjekatBolnica.Backend.Repository.CSV
{
    public class CSVRepository<E, ID> : IRepository<E, ID>
        where E : IIdentifiable<ID>
        where ID : IComparable
    {
        private const String NOT_FOUND_ERROR = "{0} with {1}:{2} can not be found!";

        protected String _entityName;
        protected ICSVStream<E> _stream;
        protected ISequencer<ID> _sequencer;

        public CSVRepository(String entityName, ICSVStream<E> stream, ISequencer<ID> sequencer)
        {
            _entityName = entityName;
            _stream = stream;
            _sequencer = sequencer;
            InitializeId();
        }

        public void CloseFile(String myPath)
        {
            File.Create(myPath).Close();
        }

        public void Delete(E entity)
        {
            var entities = _stream.ReadAll().ToList();
            var entityToRemove = entities.SingleOrDefault(ent => ent.GetId().CompareTo(entity.GetId()) == 0);
            if (entityToRemove != null)
            {
                entities.Remove(entityToRemove);
                _stream.SaveAll(entities);
            }
            else
            {
                ThrowEntityNotFoundException("id", entity.GetId());
            }
        }

        public IEnumerable<E> Find(Func<E, bool> predicate)
        => _stream
            .ReadAll()
            .Where(predicate);

        public E Get(ID id)
        {
            try
            {
                return _stream
                    .ReadAll()
                    .SingleOrDefault(entity => entity.GetId().CompareTo(id) == 0);
            }
            catch (ArgumentException)
            {
                throw new EntityNotFoundException(string.Format(NOT_FOUND_ERROR, _entityName, "id", id));
            }
        }

        public IEnumerable<E> GetAll() => _stream.ReadAll();

        public void New(E entity)
        {
            entity.SetId(_sequencer.GenerateId());
            _stream.AppendToFile(entity);
        }

        public void OpenFile(String myPath)
        {
            File.OpenRead(myPath);
        }

        public void Set(E entity)
        {
            try
            {
                var entities = _stream.ReadAll().ToList();
                entities[entities.FindIndex(ent => ent.GetId().CompareTo(entity.GetId()) == 0)] = entity;
                _stream.SaveAll(entities);
            }
            catch (ArgumentException)
            {
                ThrowEntityNotFoundException("id", entity.GetId());
            }
        }

        protected void InitializeId() => _sequencer.Initialize(GetMaxId(_stream.ReadAll()));

        private ID GetMaxId(IEnumerable<E> entities)
           => entities.Count() == 0 ? default : entities.Max(entity => entity.GetId());

        private void ThrowEntityNotFoundException(string key, object value)
          => throw new EntityNotFoundException(string.Format(NOT_FOUND_ERROR, _entityName, key, value));

    }
}
