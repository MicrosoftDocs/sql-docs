---
title: IClientVirtualDeviceSet2::OpenDevice
titlesuffix: SQL Server VDI reference
description: This article provides reference for the IClientVirtualDeviceSet2::OpenDevice command.
ms.date: 08/30/2019
ms.service: sql
ms.subservice: backup-restore
ms.topic: reference
author: MashaMSFT
ms.author: mathoma
---

# IClientVirtualDeviceSet2::OpenDevice (VDI)

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

The **OpenDevice** function opens one of the devices in the virtual device set.

## Syntax

```c
HRESULT IClientVirtualDeviceSet2::OpenDevice (
   LPCWSTR                  lpName,
   IClientVirtualDevice**   ppVirtualDevice

);
```

## Parameters

*lpName*
This identifies the virtual device.

*ppVirtualDevice*
When the function succeeds, an interface pointer to the virtual device is returned. This interface is used for the GetCommand and CompleteCommand.

## Return Value

|Return Value | Explanation |
|---|---|
| NOERROR | The function succeeded. |
| VD_E_ABORT | Abort was requested. |
| VD_E_OPEN |All devices are open. |
| VD_E_PROTOCOL | The set is not in the initializing state or this particular device is already open. |
| VD_E_INVALID | The device name is invalid. It is not one of the names known to comprise the set. |

## Remarks

VD_E_OPEN may be returned without problem. The client may call OpenDevice by means of a loop until this code is returned.
If more than one device is configured (for example, n devices), the virtual device set will return n unique device interfaces. The first device has the same name as the virtual device set. Other devices are named as specified with the VIRTUAL_DEVICE clauses of the BACKUP/RESTORE statement.

The GetConfiguration function can be used to wait until the devices can be opened.

If this function does not succeed, then a null value is returned through the ppVirtualDevice.

## Next steps

For more information, see the [SQL Server virtual device interface reference overview](reference-virtual-device-interface.md).