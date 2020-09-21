using System.Collections.Generic;

namespace Prom_IT
{
    interface IAutocompleter
    {
        public List<Completion> GetCompletions(string word);
        public void Create(string fileName);
        public void Update(string fileName);
        public void Remove();
    }
}
