---
title: IServerVirtualDeviceSet2::OpenDevice
titlesuffix: SQL Server VDI reference
description: This article provides reference for the IServerVirtualDeviceSet2::OpenDevice command.
ms.date: 08/30/2019
ms.service: sql
ms.subservice: backup-restore
ms.topic: reference
author: MashaMSFT
ms.author: mathoma
---

# IServerVirtualDeviceSet2::OpenDevice (VDI)

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

The **OpenDevice** function obtains virtual device interfaces from the virtual device set.

## Syntax

```c
HRESULT IServerVirtualDeviceSet2::OpenDevice (
   LPCWSTR                     lpName,
   IServerVirtualDevice**      ppVirtualDevice
);
```

## Parameters

*lpName*
This is provided from the first VIRTUAL_DEVICE= clause of the BACKUP or RESTORE command. This name is used as the key to obtain access to the virtual device set created by the client.

*ppVirtualDevice*
This is used to return a virtual device interface.

## Return Value

|Return Value | Explanation |
|---|---|
| NOERROR | The function succeeded. |
| VD_E_OPEN |All devices have been opened. |

## Remarks

Each call returns the next unopened device. The function can be called only the number of times equal to the number of devices specified in the virtual device set configuration.

## Next steps

For more information, see the [SQL Server virtual device interface reference overview](reference-virtual-device-interface.md).