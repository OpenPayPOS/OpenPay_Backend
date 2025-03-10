﻿using Microsoft.EntityFrameworkCore;

namespace OpenPay.Data.DataModels;
public class ItemDataModel : BaseDataModel
{
    public string Name { get; set; }
    [Precision(16,2)]
    public decimal Price { get; set; }
    [Precision(5,2)]
    public decimal TaxPercentage { get; set; }
}