---
title: IServerVirtualDeviceSet2::Close
titlesuffix: SQL Server VDI reference
description: This article provides reference for the IServerVirtualDeviceSet2::Close command.
ms.date: 08/30/2019
ms.service: sql
ms.subservice: backup-restore
ms.topic: reference
author: MashaMSFT
ms.author: mathoma
---

# IServerVirtualDeviceSet2::Close (VDI)

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

The **Close** function closes a virtual device set opened by IServerVirtualDeviceSet2::Open. It results in releasing all resources associated with the virtual device. The IServerVirtualDeviceSet2 handle is not useful after this function returns and it should be returned to COM.

## Syntax

```c
HRESULT IServerVirtualDeviceSet2::Close ();
```

## Return Value

|Return Value | Explanation |
|---|---|
| VD_E_PROTOCOL | The devices were still open. |

## Remarks

Closing the virtual device set before closing the devices should not be performed. If this situation occurs, VD_E_PROTOCOL is returned. This action results in Close immediately releasing its mapping of shared memory. The server is subject to access violations if it continues to expect ownership of resources returned from the virtual device interface. The interface performs SignalAbort processing.

The completion agent, if running, completes any outstanding commands before returning to its caller. Any outstanding commands are completed with ERROR_OPERATION_ABORTED. That is, the callback function is invoked for each such command.

In all cases including when errors are returned, Close releases all resources for the virtual device interface. Any buffers and other interface pointers returned from the VDI become invalid.

It is important to ensure that the completion agent has been terminated before the COM library is unloaded. If the library is unloaded before the completion agent returns to its caller, then the process could cause an instruction violation.

## Next steps

For more information, see the [SQL Server virtual device interface reference overview](reference-virtual-device-interface.md).