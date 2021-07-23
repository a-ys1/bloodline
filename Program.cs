using System;
using System.Collections.Generic;

namespace ProblemB
{
    class Program
    {
        public class Person
        {
            public string Name;
            public float Percentage;

            public Person(string name, float percentage)
            {
                Name = name;
                Percentage = percentage;
            }
        }

        static void Main(string[] args)
        {
            string[] parameters = Console.ReadLine().Split(' ');
            int nOfChildren = int.Parse(parameters[0]);
            int nOfClaimants = int.Parse(parameters[1]);
            var parents = new Dictionary<string, List<string>>();
            Person founder = new Person(Console.ReadLine(), 100);

            /*Adding children to each parent*/
            for (int i = nOfChildren-1; i >= 0; i--)
            {
                var input = Console.ReadLine().Split(' ');

                List<string> child = new List<string>();
                child.Add(input[0]);
                
                /*Append*/
                if (parents.ContainsKey(input[1]))
                {
                    parents[input[1]].Add(input[0]);
                }
                /*Add*/
                else
                {
                    parents.Add(input[1], child);
                }

                if (parents.ContainsKey(input[2]))
                {
                    parents[input[2]].Add(input[0]);
                }
                else
                {
                    parents.Add(input[2], child);
                }
            }

            var queue = new Queue<Person>();
            queue.Enqueue(founder);
            var record = new Dictionary<string, float>();

            /*Building the family record*/
            while (queue.Count > 0)
            {
                var person = queue.Dequeue();
                if (record.ContainsKey(person.Name))
                {
                    record[person.Name] += person.Percentage;
                }
                else
                {
                    record[person.Name] = person.Percentage;
                }

                if (parents.ContainsKey(person.Name))
                {
                    foreach(string entry in parents[person.Name])
                    {
                        queue.Enqueue(new Person(entry, person.Percentage / 2));
                    }
                }
            }

            /*Placeholder heir(1st claimant)*/
            Person heir = new Person("", 0);
            heir.Name = Console.ReadLine();
            if (record.ContainsKey(heir.Name))
            {
                heir = new Person(heir.Name, record[heir.Name]);
            }
            else
            {
                heir = new Person(heir.Name, 0);
            }

            /*Compare with other claimants*/
            for (int i = 1; i < nOfClaimants-1; i++)
            {
                string name = Console.ReadLine();
                if (record.ContainsKey(name) && record[name] >= heir.Percentage)
                {
                    heir = new Person(name, record[name]);
                }
            }
            Console.WriteLine(heir.Name);
        }
    }
}
