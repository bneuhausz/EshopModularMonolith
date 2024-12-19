﻿using MediatR;

namespace Shared.CQRS;
public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, Unit>
    where TCommand : ICommand<Unit>
{
}

public interface ICommandHandler<in TCommand, TResposne>
    : IRequestHandler<TCommand, TResposne>
    where TCommand : ICommand<TResposne>
    where TResposne : notnull
{
}
