﻿using Octopus.Catalog.Core.Domain.Products.Enums;
using Octopus.Core.Domain.Entities;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.Catalog.Core.Domain.Categories.Entities;

public class Category : AggregateRoot<CategoryId>
{
    #region Properties

    public string Name { get; private protected set; }

    public string ImageUrl { get; private protected set; }

    public string Description { get; private protected set; }

    public CategoryId ParentId { get; private protected set; }

    public ProductStatusType Status { get; private protected set; }

    #endregion
}