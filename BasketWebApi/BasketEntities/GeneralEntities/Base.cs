using System;

namespace BasketModuleEntities.GeneralEntities
{
    public enum OperationTypes
    {
        Unknown = 0,
        Added = 1,
        Updated = 2,
        Deleted = 3
    }

    public class Base
    {
        public OperationTypes OperationType { get; set; }

        public DateTime OperationDate { get; set; }
    }
}
