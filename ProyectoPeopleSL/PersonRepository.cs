using ProyectoPeopleSL.Models;
using SQLite;

namespace ProyectoPeopleSL
{
    public class PersonRepository
    {
        private SQLiteConnection conn;

        string _dbPath;

        public string StatusMessage { get; set; }

        private void Init()
        {
            if (conn != null)
                return;

            conn = new SQLiteConnection(_dbPath);
            conn.CreateTable<PersonSL>();
        }

        public PersonRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public void AddNewPerson(string name)
        {
            int result = 0;
            try
            {
                Init();

                if (string.IsNullOrEmpty(name))
                    throw new Exception("Valid name required");

                result = conn.Insert(new PersonSL { Name = name });

                StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, name);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
            }

        }

        public List<PersonSL> GetAllPeople()
        {
            try
            {
                Init();
                return conn.Table<PersonSL>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<PersonSL>();
        }

        public void DeletePerson(int id)
        {
            try
            {
                Init();
                var personToDelete = conn.Find<PersonSL>(id);
                if (personToDelete != null)
                {
                    conn.Delete(personToDelete);
                    StatusMessage = $"Registro con Id {id} eliminado.";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al eliminar el registro: {ex.Message}";
            }
        }

    }
}
