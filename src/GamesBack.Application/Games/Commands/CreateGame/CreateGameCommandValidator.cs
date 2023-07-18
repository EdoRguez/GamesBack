using FluentValidation;
using GamesBack.Application.Common.Interfaces.Persistence;

namespace GamesBack.Application.Games.Commands.CreateGame;

public class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
{
    private readonly IPublisherRepository _publisherRepo;

    public CreateGameCommandValidator(IPublisherRepository publisherRepository)
    {
        _publisherRepo = publisherRepository;

        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);

        RuleForEach(x => x.Reviews).ChildRules(review =>
        {
            review.RuleFor(y => y.Description)
                .MaximumLength(100)
                .NotEmpty();

            review.RuleFor(y => y.Rating)
                .NotNull();
        });

        RuleFor(x => x.PublisherId)
            .MustAsync(PublisherIdExists)
            .WithMessage("Publisher doesn't exist");
    }

    private async Task<bool> PublisherIdExists(string id, CancellationToken token)
    {
        await Task.CompletedTask;
        return _publisherRepo.Exists(Guid.Parse(id));
    }
}