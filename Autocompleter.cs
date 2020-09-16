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
            List<Completion> completions = FromFile(fileName);

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
        public void AddCompletions(List<Completion> completions)
        {
            // Insert new completions
            foreach (var completion in completions)
            {
                db.Completions.Add(completion);
            }
            db.SaveChanges();
        }
        private List<Completion> FromFile(string fileName)
        {
            List<Completion> completions = new List<Completion>();

            List<string> words = FileParser.Parse(fileName);

            foreach (var word in words)
            {
                // TODO : FIX frequency calculation here.
                completions.Add(new Completion() { Word = word, Frequency = 1 });
            }

            return completions;
        }
        ~Autocompleter()
        {
            db.Dispose();
        }
    }
}
