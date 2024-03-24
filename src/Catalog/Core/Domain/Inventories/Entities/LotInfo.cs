using Octopus.Catalog.Core.Domain.Inventories.Enums;
using Octopus.Core.Domain.Entities;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.Catalog.Core.Domain.Inventories.Entities;

public class LotInfo : EntityBase<LotId>
{
    #region Properties

    public LotStatusType Status { get; private protected set; }
    
    public decimal SalesPrice { get; private protected set; }
    
    public int SalesQuantity { get; private protected set; }

    public int ReservedQuantity { get; private protected set; }

    public int SoldQuantity { get; private protected set; }

    #endregion
}