using System.Collections.Generic;
using RestAPI.Models;

namespace RestAPI.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public void DeleteCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
        public void UpdateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
        public void CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
            {
                new Command{Id=0,HowTo="unlock phone",Line="use pin or password",Platform="lock screen"},
                new Command{Id=1,HowTo="go out",Line="use balcony",Platform="jump from balcony"},
                new Command{Id=2,HowTo="get back in",Line="use balcony",Platform="no you cannot because its tooo high"}
            };
            return commands;
        }

        public Command GetCommandById(int Id)
        {
            return new Command{Id=0,HowTo="unlock phone",Line="use pin or password",Platform="lock screen"};
        }
    }
}