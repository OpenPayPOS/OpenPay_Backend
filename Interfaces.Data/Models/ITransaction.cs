﻿namespace OpenPay.Interfaces.Data.Models;
public interface ITransaction : IDisposable
{
    Task CommitAsync(CancellationToken cancellationToken = default);
    Task RollbackAsync(CancellationToken cancellationToken = default);
}