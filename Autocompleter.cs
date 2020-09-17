using System;
using System.Collections.Generic;

namespace Prom_IT
{
    class Autocompleter : IAutocompleter
    {
        private readonly CompletionContext db;
        public Autocompleter()
        {
            db = new CompletionContext();
        }

        public List<Completion> GetCompletions()
        {
            List<Completion> completions = new List<Completion>();
            foreach (var completion in completions)
            {
                completions.Add(completion);
            }
            return completions;
        }
        public void Create(string fileName)
        {
            HashSet<Completion> completions = FileParser.ParseCompletions(fileName);

            Remove();

            AddCompletions(completions);
        }
        public void Update(string fileName)
        {
            throw new NotImplementedException();
        }

        public void Remove()
        {
            // Remove all completions
            db.Clear();
        }
        public void AddCompletions(HashSet<Completion> completions)
        {
            // Insert new completions
            foreach (var completion in completions)
            {
                db.Completions.Add(completion);
            }
            db.SaveChanges();
        }
        ~Autocompleter()
        {
            db.Dispose();
        }
    }
}
