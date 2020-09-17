using System;
using System.Collections.Generic;
using System.Linq;

namespace Prom_IT
{
    class Autocompleter : IAutocompleter
    {
        // Make it static ?
        private readonly CompletionContext dbContext;
        public Autocompleter()
        {
            dbContext = new CompletionContext();
        }

        public List<Completion> GetCompletions(string word)
        {
            List<Completion> completions =
                (from c in dbContext.Completions
                          where c.Word.StartsWith(word)
                          orderby c.Frequency, c.Word
                          select c).Take(5).ToList();
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
            HashSet<Completion> completions = FileParser.ParseCompletions(fileName);
            UpdateCompletions(completions);
        }

        public void Remove()
        {
            // Remove all completions
            dbContext.Clear();
        }
        public void AddCompletions(HashSet<Completion> completions)
        {
            // Insert new completions
            foreach (var completion in completions)
            {
                dbContext.Completions.Add(completion);
            }
            dbContext.SaveChanges();
        }
        public void UpdateCompletions(HashSet<Completion> completions)
        {
            foreach (Completion completion in completions)
            {
                // This is extremeley inefficient O(N*N). Better way to do this is using stored procedure.
                Completion entity = dbContext.Completions.FirstOrDefault(item => item.Word == completion.Word);
                
                if (entity != null)
                {
                    entity.Frequency = completion.Frequency;
                }
                else
                {
                    dbContext.Completions.Add(completion);
                }
                dbContext.SaveChanges();
            }
        }
        ~Autocompleter()
        {
            dbContext.Dispose();
        }
    }
}
