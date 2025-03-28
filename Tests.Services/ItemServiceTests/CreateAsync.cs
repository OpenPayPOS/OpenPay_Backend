﻿using Interfaces.Common.Exceptions;
using Interfaces.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OpenPay.Interfaces.Data.DataModels.Item;
using OpenPay.Interfaces.Data.Models;
using OpenPay.Interfaces.Data.Repositories;
using OpenPay.Interfaces.Services.ServiceModels;
using OpenPay.Services;

namespace OpenPay.Tests.Services.ItemServiceTests;
public class CreateAsync
{

    [Fact]
    public async Task CreateAsync_ReturnsItem_IfCreated()
    {
        // Arrange
        IItemRepository _repository = Substitute.For<IItemRepository>();
        IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
        ILogger<ItemService> _logger = Substitute.For<ILogger<ItemService>>();
        ItemService _service = new ItemService(_repository, _logger, _unitOfWork);

        Guid id = Guid.NewGuid();

        _repository.NameExistsAsync("test").Returns(false);
        _repository.CreateAsync(Arg.Any<ItemDataDTO>()).Returns(new ItemDataDTO
        {
            Id = id,
            Name = "asdf",
            Price = 11,
            TaxPercentage = 11,
            ImagePath = "file.png"
        });

        // Act
        var response = await _service.CreateAsync("test", 10, 10, "filename.png");

        // Assert
        await _repository.Received().NameExistsAsync(Arg.Any<string>());
        await _repository.Received().CreateAsync(Arg.Any<ItemDataDTO>());
        await _unitOfWork.Received(1).SaveChangesAsync();
        response.Handle(value =>
        {
            Assert.Equal(id, value.Id);
        }, _ =>
        {
            Assert.Fail();
        });
    }

    [Fact]
    public async Task CreateAsync_ReturnsBadRequestException_IfNameExists()
    {
        // Arrange
        IItemRepository _repository = Substitute.For<IItemRepository>();
        IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
        ILogger<ItemService> _logger = Substitute.For<ILogger<ItemService>>();
        ItemService _service = new ItemService(_repository, _logger, _unitOfWork);

        Guid id = Guid.NewGuid();

        _repository.NameExistsAsync("test").Returns(true);


        // Act
        var response = await _service.CreateAsync("test", 10, 10, "filename.png");

        // Assert
        await _repository.Received().NameExistsAsync(Arg.Any<string>());
        await _repository.DidNotReceive().CreateAsync(Arg.Any<ItemDataDTO>());
        await _unitOfWork.DidNotReceive().SaveChangesAsync();


        response.Handle(_ =>
        {
            Assert.Fail();
        }, ex =>
        {
            Assert.IsType<BadRequestException>(ex);
        });
    }
}