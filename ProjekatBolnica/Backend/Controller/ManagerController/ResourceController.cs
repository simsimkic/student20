/***********************************************************************
 * Module:  ResourceController.cs
 * Purpose: Definition of the Class Controller.ManagerController.ResourceController
 ***********************************************************************/

using Model.ManagerModel;
using Service.ManagerService;
using System.Collections.Generic;

namespace Controller.ManagerController
{
    public class ResourceController : IResourceController
    {
        private readonly IResourceService _service;

        public ResourceController(IResourceService service)
        {
            _service = service;
        }

        public void Delete(Resource resource) => _service.Delete(resource);

        public Resource Get(long id) => _service.Get(id);

        public IEnumerable<Resource> GetAll() => _service.GetAll();

        public void New(Resource resource) => _service.New(resource);

        public void Set(Resource resource) => _service.Set(resource);

        public void IncreaseQuantity(long id, int quantity) => _service.IncreaseQuantity(id, quantity);

        public void DecreaseQuantity(long id, int quantity) => _service.DecreaseQuantity(id, quantity);

        public int GetAmountOfResource(long id) => _service.GetAmountOfResource(id);

        public int CountNumberOfBedsInRoom(Room room) => _service.CountNumberOfBedsInRoom(room);

        public List<Bed> GetBedsInRoom(Room room) => _service.GetBedsInRoom(room);
    }
}