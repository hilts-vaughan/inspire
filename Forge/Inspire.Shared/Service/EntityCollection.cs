using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Inspire.Shared.Components;

namespace Inspire.Shared.Service
{
    public class EntityCollection
    {
        public ObservableCollection<Entity> Entities { get; set; }

        public EntityCollection(ObservableCollection<Entity> entities)
        {
            Entities = entities           
        }

        public EntityCollection()
        {
            Entities = new ObservableCollection<Entity>();
        }


        /// <summary>
        /// Filters the entity collection to only contain entities with the desired type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public EntityCollection Filter<T>() where T : Component
        {
            var filteredList = Entities.Where(x => x.HasComponent<T>());
            return new EntityCollection(new ObservableCollection<Entity>(filteredList.ToList()));
        }

  




    }
}
