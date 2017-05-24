using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDDesignPrinciplesPractice
{
    public enum Relationship
    {
        Parent, Child, Sibling, Spouse
    }

    public class Person
    {
        public string Name;
    }

    //Example of Good practice: create an interface access low level code
    public interface IRelationshipBrowser
    {
        IEnumerable<Person> FindAllChildrenOf(string name);
    }

    //Low Level
    public class Relationships : IRelationshipBrowser
    {
        //First need to nuget: Install-Package "System.ValueTuple"
        private List<(Person, Relationship, Person)> relations
            = new List<(Person, Relationship, Person)>();

        public void AddParentAndChild(Person parent, Person child)
        {
            relations.Add((parent, Relationship.Parent, child));
            relations.Add((child, Relationship.Child, parent));
        }

        public IEnumerable<Person> FindAllChildrenOf(string name)
        {
            return relations.Where(
                x => x.Item1.Name == name &&
                     x.Item2 == Relationship.Parent
            ).Select(r => r.Item3);
        }

        //Example of bad practice: special List that makes private property private

        // public List<(Person, Relationship, Person)> Relations => relations;
    }

    //High Level
    class Research
    {
        //Example of bad practice: accessing low level code through special List inside of it

        //public Research(Relationships relationships)
        //{
        //    var relations = relationships.Relations;
        //    foreach (var r in relations.Where(
        //        x => x.Item1.Name == "Francis" && 
        //        x.Item2 == Relationship.Parent
        //        ))
        //    {
        //        Console.WriteLine($"Francis has a child called {r.Item3.Name}");
        //    }
        //}

        //Example of Good practice: depend on the interface, not the low level method
        public Research(IRelationshipBrowser browser)
        {
            foreach (var p in browser.FindAllChildrenOf("Francis"))
            {
                Console.WriteLine($"Francis has a child called {p.Name}");
            }
        }

        static void Main(string[] args)
        {
            var parent = new Person {Name = "Francis"};
            var child1 = new Person {Name = "Zoe" };
            var child2 = new Person {Name = "Louis" };

            var relationships = new Relationships();
            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);

            new Research(relationships);
            Console.ReadLine();
        }
    }
}
