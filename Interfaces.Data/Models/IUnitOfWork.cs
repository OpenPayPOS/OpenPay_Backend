﻿
namespace OpenPay.Interfaces.Data.Models;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}
