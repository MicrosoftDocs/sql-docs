---
title: IServerVirtualDeviceSet2::SignalAbort
titlesuffix: SQL Server VDI reference
description: This article provides reference for the IServerVirtualDeviceSet2::SignalAbort command.
ms.date: 08/30/2019
ms.service: sql
ms.subservice: backup-restore
ms.topic: reference
author: MashaMSFT
ms.author: mathoma
---

# IServerVirtualDeviceSet2::SignalAbort (VDI)

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

The **SignalAbort** function signals that an abnormal termination should occur.

## Syntax

```c
HRESULT IServerVirtualDeviceSet2::SignalAbort ();
```

## Return Value

Returns an *HRESULT* indicating success or failure of the method call. A value of NOERROR indicates that the method call was successful. A non-zero value indicates that an error has occurred.

## Remarks

At any time, the server may choose to abort the BACKUP or RESTORE operation.

This routine signals that all operations should cease. The overall interface enters an abort state. No further commands are accepted on any devices. The completion agent returns ERROR_OPERATION_ABORTED for each outstanding request and returns to its caller. Any completions recorded at the client are ignored.

The server ensures that there is no further required use of the buffers or devices returned from the virtual device interface. The server then performs abnormal termination cleanup, which should include calling the IServerVirtualDeviceSet2::Close function.

## Next steps

For more information, see the [SQL Server virtual device interface reference overview](reference-virtual-device-interface.md).