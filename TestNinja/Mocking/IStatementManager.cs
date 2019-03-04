using System;

namespace TestNinja.Mocking
{
    public interface IStatementManager
    {
        string SaveStatement(int housekeeperOid, string housekeeperName, DateTime statementDate);
    }
}