---
title: IServerVirtualDeviceSet2::ExecuteCompletionAgent
titlesuffix: SQL Server VDI reference
description: This article provides reference for the IServerVirtualDeviceSet2::ExecuteCompletionAgent command.
ms.date: 08/30/2019
ms.service: sql
ms.subservice: backup-restore
ms.topic: reference
author: MashaMSFT
ms.author: mathoma
---

# IServerVirtualDeviceSet2::ExecuteCompletionAgent (VDI)

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

The **ExecuteCompletionAgent** function is used to implement the main loop of the completion agent.

## Syntax

```c
HRESULT IServerVirtualDeviceSet2::ExecuteCompletionAgent ();
```

## Return Value

Returns an *HRESULT* indicating success or failure of the method call. A value of NOERROR indicates that the method call was successful. A non-zero value indicates that an error has occurred.

## Remarks

The completion agent provides a mechanism through which SQL Server can synchronize itself with virtual device command completions. It must be active before any commands can be issued and it should remain active until all devices are closed.

Since SQL Server might have to perform special thread initialization, this interface does not start a new thread of control. Instead, SQL Server sets up a thread, and then passes control to this routine. The thread must be blockable on Windows NT Inter-process Communication (IPC) mechanisms and capable of calling any of the callback functions that are provided with commands sent through IServerVirtualDevice::SendCommand.

This function will not return until IServerVirtualDeviceSet2::Close or SignalAbort is invoked.

## Next steps

For more information, see the [SQL Server virtual device interface reference overview](reference-virtual-device-interface.md).