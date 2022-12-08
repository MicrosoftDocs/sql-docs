---
title: IServerVirtualDevice::SendCommand
titlesuffix: SQL Server VDI reference
description: This article provides reference for the IServerVirtualDevice::SendCommand command.
ms.date: 08/30/2019
ms.service: sql
ms.subservice: backup-restore
ms.topic: reference
author: MashaMSFT
ms.author: mathoma
---

# IServerVirtualDevice::SendCommand (VDI)

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

The **SendCommand** function sends a command to the client by using a virtual device object returned from IServerVirtualDeviceSet2::OpenDevice.

## Syntax

```c
HRESULT IServerVirtualDevice::SendCommand (
   VDS_Command*   pCmd
);
```

## Parameters

*pCmd*
   This is a pointer to a command request block. For more information, see Commands. The completionFunction field must be set to point to the address of a function with the following signature:

```c
void callbackFunction ( VDS_Command *pCmd);
```

This callback is made by the completion agent when the client indicates that a command has been completed. SQL Server sets the completionContext field of the pCmd. Its purpose is to provide context to the callback function.

## Return Value

|Return Value | Explanation |
|---|---|
| NOERROR | The command is successfully queued to the client. |
| VD_E_QUEUE_FULL | The device queue is full. |
| VD_E_IO_ERROR | The device is in an IO-ERROR state. |
| VD_E_PROTOCOL | The device is not active. |

## Remarks

When an error occurs while attempting to send the command, the callback function is invoked, and the completionCode in the command buffer is set as follows:

| completionCode | Error |
|---|---|
| VD_E_QUEUE_FULL | ERROR_NO_SYSTEM_RESOURCES |
| VD_E_IO_ERROR   | ERROR_IO_DEVICE |
| VD_E_PROTOCOL   | ERROR_INVALID_HANDLE |
| VD_E_ABORT      | ERROR_OPERATION_ABORTED |

## Next steps

For more information, see the [SQL Server virtual device interface reference overview](reference-virtual-device-interface.md).