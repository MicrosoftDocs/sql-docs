---
title: IClientVirtualDevice::GetCommand
titlesuffix: SQL Server VDI reference
description: This article provides reference for the IClientVirtualDevice::GetCommand command.
ms.date: 08/30/2019
ms.service: sql
ms.subservice: backup-restore
ms.topic: reference
author: MashaMSFT
ms.author: mathoma
---

# IClientVirtualDevice::GetCommand (VDI)

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

The **GetCommand** function is used to obtain the next command queued to a device. When requested, this function waits for the next command.

## Syntax

```c
HRESULT IClientVirtualDevice::GetCommand (
   DWORD               dwTimeOut,
   VDC_Command**      const ppCmd
);
```

## Parameters

*ppCmd*
   When a command is successfully returned, the parameter returns the address of a command to execute. The memory returned is read-only. When the command is completed, this pointer is passed to the CompleteCommand routine. For more information about each command, see Commands.

*dwTimeOut*
   This is the time to wait, in milliseconds. Use INFINTE to wait indefinitely. Use 0 to poll for a command. VD_E_TIMEOUT is returned if no command is currently available. If the time-out occurs, the client decides the next action.

## Return Value

|Return Value | Explanation |
|---|---|
| NOERROR | A command was fetched. |
| VD_E_CLOSE | The device has been closed by the server. |
| VD_E_TIMEOUT | No command was available and the time-out expired. |
| VD_E_ABORT | Either the client or the server has used the SignalAbort to force a shutdown. |

## Remarks

When VD_E_CLOSE is returned, SQL Server has closed the device. This is part of the normal shutdown. After all devices have been closed, the client invokes IClientVirtualDeviceSet2::Close to close the virtual device set.

When this routine must block to wait for a command, the thread is left in an Alertable condition.

## Next steps

For more information, see the [SQL Server virtual device interface reference overview](reference-virtual-device-interface.md).