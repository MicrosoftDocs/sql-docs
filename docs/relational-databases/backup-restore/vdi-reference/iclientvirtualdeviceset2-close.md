---
title: IClientVirtualDeviceSet2::Close
titlesuffix: SQL Server VDI reference
description: This article provides reference for the IClientVirtualDeviceSet2::Close command.
ms.date: 08/30/2019
ms.service: sql
ms.subservice: backup-restore
ms.topic: reference
author: MashaMSFT
ms.author: mathoma
---

# IClientVirtualDeviceSet2::Close (VDI)

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

The **Close** function closes the virtual device set created by IClientVirtualDeviceSet2::Create. It results in the release of all resources associated with the virtual device set.

## Syntax

```c
HRESULT IClientVirtualDeviceSet2::Close ();
```

## Return Value

|Return Value | Explanation |
|---|---|
| NOERROR | This is returned when the virtual device set was successfully closed. |
| VD_E_PROTOCOL | No action was taken because the virtual device set was not open. |
| VD_E_OPEN | Devices were still open. |

## Remarks

The invocation of Close is a declaration by the client that all resources used by the virtual device set should be released. The client must ensure that all activity involving data buffers and virtual devices is terminated before invoking Close. All virtual device interfaces returned by OpenDevice are invalidated by Close.

The client is permitted to issue a Create call on the virtual device set interface after the Close call returns. Such a call would create a new virtual device set for a subsequent BACKUP or RESTORE operation.

If Close is called when one or more virtual devices are still open, VD_E_OPEN is returned. In this case, SignalAbort is internally triggered, to ensure a proper shutdown if possible. VDI resources are released. The client should wait for a VD_E_CLOSE indication on each device before invoking IClientVirtualDeviceSet2::Close. If the client knows that the virtual device set is already in an Abnormally Terminated state, then it should not expect a VD_E_CLOSE indication from GetCommand, and may invoke IClientVirtualDeviceSet2::Close as soon as activity on the shared buffers is terminated.

## Next steps

For more information, see the [SQL Server virtual device interface reference overview](reference-virtual-device-interface.md).