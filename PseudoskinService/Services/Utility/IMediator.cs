using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoSkinServices.Utility
{
    public interface IMediator
    {
        Task<TResult> ExecuteCommand<TCommand, TCommandHandler, TResult>(TCommand command, TCommandHandler handler)
            where TCommand : Command
            where TCommandHandler : CommandHandler<TCommand, TResult>;


    }
    public class Mediator : IMediator
    {
        public Task<TResult> ExecuteCommand<TCommand, TCommandHandler, TResult>(TCommand command, TCommandHandler handler) 
            where TCommand: Command
            where TCommandHandler: CommandHandler<TCommand, TResult>
        {
            return handler.HandleAsync(command);
        }
    }

    public abstract class CommandHandler<TCommand, TResult> where TCommand : Command 
    {
        public abstract Task<TResult> HandleAsync(TCommand command);
           
    }


    public abstract class Command
    {
        public abstract void Validation();
    }
}
