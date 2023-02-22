using PseudoSkinDataAccess.UnitOfWorks;
using PseudoSkinDomain.Models;
using PseudoSkinServices.Utility;
using System;
using System.Threading.Tasks;

namespace PseudoSkinApplication.CreatePseudoskin
{
    public class CreatePseudoskinCommandHandler : CommandHandler<CreatePseudoskinCommand, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreatePseudoskinCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public override async Task<Result> HandleAsync(CreatePseudoskinCommand command)
        {
            var skin = await unitOfWork.PseudoSkin.NameExist(x=>x.Name == command.Name);

            if (skin) throw new ArgumentException($"{command.Name} already exist");

            var pseudoskin = new PseudoSkin
            {
                Name = command.Name,
                DistanceFromTopOfSandToTopOfPerforation = command.DistanceFromTopOfSandToTopOfPerforation,
                DateCreated = DateTime.UtcNow,
                HorizontalPermeability = command.HorizontalPermeability,
                VerticalPermeability = command.VerticalPermeability,
                LenghtOfPerforationInterval = command.LenghtOfPerforationInterval,
                ReservoirThickness = command.ReservoirThickness,
                WellboreRadius = command.WellboreRadius
            };

            unitOfWork.PseudoSkin.Add(pseudoskin);

            var commit = unitOfWork.SaveChangesAsync();

            if (commit.IsCompletedSuccessfully) return new Result { Id = pseudoskin.Id, Name=pseudoskin.Name };

            throw new ArgumentException("failed to commit to database");

        }
    }
}
