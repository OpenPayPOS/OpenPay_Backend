﻿
using Interfaces.Common.Models;
using OpenPay.Interfaces.Services.Common;
using OpenPay.Interfaces.Services.ServiceModels;

namespace OpenPay.Interfaces.Services;

public interface IOrderService : IBaseService<OrderDTO>
{
    Task<Optional<OrderDTO>> CreateAsync(List<CreateOrderItemDTO> orderItems, DateTime? createdTime = null);
    Task<Optional<bool>> IdExistsAsync(Guid itemId);
}
