using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Baximocker
{
    public enum PaymentStatus
    {
        Pending,
        Paid,
        PayAtStation,
        Error,
        Refunded,
        Deleted,
        Canceled,
        RefundInProcess,
        CancelInProcess
    }
}
