using System.Collections.Generic;
using DailyPmsAPI.Models;

namespace DailyPmsAPI.Data
{
    public interface IClasseRepository
    {
        IEnumerable<Classe> GetAllClassesBySchool(string schoolId);

        Classe GetClasseById(string id);

        Classe GetClasseByName(string id);

        void CreateClasse(Classe newClasse);

        void UpdateClasseById(string id, Classe updatedClasse);

        void DeleteClasseById(string id);
    }
}
