---
title: IServerVirtualDevice::CloseDevice
titlesuffix: SQL Server VDI reference
description: This article provides reference for the IServerVirtualDevice::CloseDevice command.
ms.date: 08/30/2019
ms.service: sql
ms.subservice: backup-restore
ms.topic: reference
author: MashaMSFT
ms.author: mathoma
---

# IServerVirtualDevice::CloseDevice (VDI)

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

The **CloseDevice** function closes a virtual device that had been opened with IServerVirtualDeviceSet2::OpenDevice

## Syntax

```c
HRESULT IServerVirtualDevice::CloseDevice ();
```

## Return Value

|Return Value | Explanation |
|---|---|
| VD_E_CLOSE | The device is already closed. |
| VD_E_ABORT | The interface is in the Abort state. |

## Remarks

CloseDevice is not required after SignalAbort is used to force abnormal termination. If CloseDevice is invoked after SignalAbort is used, no action is taken.

## Next steps

For more information, see the [SQL Server virtual device interface reference overview](reference-virtual-device-interface.md).