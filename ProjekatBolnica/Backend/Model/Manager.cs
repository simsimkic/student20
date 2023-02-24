/***********************************************************************
 * Module:  Manager.cs
 * Purpose: Definition of the Class Manager
 ***********************************************************************/

using Model.ManagerModel;
using System.Collections.Generic;

namespace Model
{
    public class Manager : User
    {
        public List<WorkHoursForDoctor> workHoursForDoctor;
        public List<Resource> resource;
        public List<Room> room;

        /// <pdGenerated>default getter</pdGenerated>
        public List<WorkHoursForDoctor> GetWorkHoursForDoctor()
        {
            if (workHoursForDoctor == null)
                workHoursForDoctor = new List<WorkHoursForDoctor>();
            return workHoursForDoctor;
        }

        /// <pdGenerated>default setter</pdGenerated>
        public void SetWorkHoursForDoctor(List<WorkHoursForDoctor> newWorkHoursForDoctor)
        {
            RemoveAllWorkHoursForDoctor();
            foreach (WorkHoursForDoctor oWorkHoursForDoctor in newWorkHoursForDoctor)
                AddWorkHoursForDoctor(oWorkHoursForDoctor);
        }

        /// <pdGenerated>default Add</pdGenerated>
        public void AddWorkHoursForDoctor(WorkHoursForDoctor newWorkHoursForDoctor)
        {
            if (newWorkHoursForDoctor == null)
                return;
            if (this.workHoursForDoctor == null)
                this.workHoursForDoctor = new List<WorkHoursForDoctor>();
            if (!this.workHoursForDoctor.Contains(newWorkHoursForDoctor))
                this.workHoursForDoctor.Add(newWorkHoursForDoctor);
        }

        /// <pdGenerated>default Remove</pdGenerated>
        public void RemoveWorkHoursForDoctor(WorkHoursForDoctor oldWorkHoursForDoctor)
        {
            if (oldWorkHoursForDoctor == null)
                return;
            if (this.workHoursForDoctor != null)
                if (this.workHoursForDoctor.Contains(oldWorkHoursForDoctor))
                    this.workHoursForDoctor.Remove(oldWorkHoursForDoctor);
        }

        /// <pdGenerated>default removeAll</pdGenerated>
        public void RemoveAllWorkHoursForDoctor()
        {
            if (workHoursForDoctor != null)
                workHoursForDoctor.Clear();
        }

        /// <pdGenerated>default getter</pdGenerated>
        public List<Resource> GetResource()
        {
            if (resource == null)
                resource = new List<Resource>();
            return resource;
        }

        /// <pdGenerated>default setter</pdGenerated>
        public void SetResource(List<Resource> newResource)
        {
            RemoveAllResource();
            foreach (Resource oResource in newResource)
                AddResource(oResource);
        }

        /// <pdGenerated>default Add</pdGenerated>
        public void AddResource(Resource newResource)
        {
            if (newResource == null)
                return;
            if (this.resource == null)
                this.resource = new List<Resource>();
            if (!this.resource.Contains(newResource))
                this.resource.Add(newResource);
        }

        /// <pdGenerated>default Remove</pdGenerated>
        public void RemoveResource(Resource oldResource)
        {
            if (oldResource == null)
                return;
            if (this.resource != null)
                if (this.resource.Contains(oldResource))
                    this.resource.Remove(oldResource);
        }

        /// <pdGenerated>default removeAll</pdGenerated>
        public void RemoveAllResource()
        {
            if (resource != null)
                resource.Clear();
        }

        /// <pdGenerated>default getter</pdGenerated>
        public List<Room> GetRoom()
        {
            if (room == null)
                room = new List<Room>();
            return room;
        }

        /// <pdGenerated>default setter</pdGenerated>
        public void SetRoom(List<Room> newRoom)
        {
            RemoveAllRoom();
            foreach (Room oRoom in newRoom)
                AddRoom(oRoom);
        }

        /// <pdGenerated>default Add</pdGenerated>
        public void AddRoom(Room newRoom)
        {
            if (newRoom == null)
                return;
            if (this.room == null)
                this.room = new List<Room>();
            if (!this.room.Contains(newRoom))
                this.room.Add(newRoom);
        }

        /// <pdGenerated>default Remove</pdGenerated>
        public void RemoveRoom(Room oldRoom)
        {
            if (oldRoom == null)
                return;
            if (this.room != null)
                if (this.room.Contains(oldRoom))
                    this.room.Remove(oldRoom);
        }

        /// <pdGenerated>default removeAll</pdGenerated>
        public void RemoveAllRoom()
        {
            if (room != null)
                room.Clear();
        }
    }
}