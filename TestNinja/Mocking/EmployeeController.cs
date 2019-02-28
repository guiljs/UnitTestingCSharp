using System.Data.Entity;

namespace TestNinja.Moq
{
    public class EmployeeController
    {
        private readonly IEmployeeStorage _storege;

        public EmployeeController(IEmployeeStorage storage = null)
        {
            _storege = storage ?? new EmployeeStorage();
        }

        public ActionResult DeleteEmployee(int id)
        {
            _storege.RemoveEmployee(id);
            return RedirectToAction("Employees");
        }

        private ActionResult RedirectToAction(string employees)
        {
            return new RedirectResult();
        }
    }

    public class ActionResult { }

    public class RedirectResult : ActionResult { }

    public class EmployeeContext
    {
        public DbSet<Employee> Employees { get; set; }

        public void SaveChanges()
        {
        }
    }

    public class Employee
    {
    }

    public class EmployeeStorage : IEmployeeStorage
    {
        private EmployeeContext _db;
        public EmployeeStorage()
        {
            _db = new EmployeeContext();
        }
        public void RemoveEmployee(int id)
        {
            var employee = _db.Employees.Find(id);
            if (employee == null) return;
            _db.Employees.Remove(employee);
            _db.SaveChanges();
        }
    }
}