using System.Collections.Generic;
using RestAPI.Models;
using System.Linq;
using System;

namespace RestAPI.Data
{
    public class SqlCommanderRepo : ICommanderRepo
    {
        private readonly CommanderContext _context;
        // public SqlCommanderRepo(CommanderContext context) => _context = context;
        public SqlCommanderRepo(CommanderContext context)
        {
            _context = context;
        }
        public IEnumerable<Command> GetAllCommands()
         {
             return _context.Commands.ToList();

         }

        public Command GetCommandById(int Id)
        {
            return _context.Commands.FirstOrDefault(p => p.Id == Id);

        }
        public void CreateCommand(Command cmd)
        {
            if(cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }
            
            _context.Commands.Add(cmd);
        }
        
        public void UpdateCommand(Command cmd)
        {
            // throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            if(cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
                
            }
            _context.Commands.Remove(cmd);
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }


    }
    
}