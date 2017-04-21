﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4j.Driver.V1;
using System.Collections;

namespace HolyNoodle.KnowledgeBase.Test
{
    public class Entity : IEntity
    {
        //public int Id { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public string Name { get; set; }
        public List<string> list { get; set; }
        public List<Entity> Children { get; set; }
        public Entity Partner { get; set; }
        public INode Node { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var entity = new Entity();
            entity.Age = 30;
            entity.Name = "Kevin";
            entity.Weight = 60.2;
            entity.list = new List<string> { "tutu", "titi" };
            entity.Children = new List<Entity>
            {
                new Entity
                {
                    Age = 11,
                    Name = "Tomy",
                    Weight = 35.7
                },
                new Entity
                {
                    Age = 14,
                    Name = "Noa",
                    Weight = 40.8
                },
            };
            entity.Partner = new Entity
            {
                Name = "Claire",
                Age = 42,
                Weight = 25.4,
                //Reference the "Kevin" entity
                Partner = entity,
                //reference the "Kevin"'s children entity, because they got exactly the same children
                Children = entity.Children
            };

            var em = new EntityManager();
            em.CreateEntity(entity);

            using (var queryBuilder = em.GetQueryBuilder())
            {
                var resultTyped = queryBuilder.Where<Entity>(e => e.Partner.Name.Contains("lai")).Execute<Entity>();
                var list = resultTyped.ToList();
                list[0].Partner = list[0].Partner.Populate<Entity>(em);

                //var randomUpdateIndex = 0;
                //var tomy = resultTyped.ToList()[0];
                //tomy.Name = "TomyUpdate";
                //tomy.Update(em);
            }
        }
    }
}