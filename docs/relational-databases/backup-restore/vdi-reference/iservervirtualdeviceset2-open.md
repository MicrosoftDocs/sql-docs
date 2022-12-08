---
title: IServerVirtualDeviceSet2::Open
titlesuffix: SQL Server VDI reference
description: This article provides reference for the IServerVirtualDeviceSet2::Open command.
ms.date: 08/30/2019
ms.service: sql
ms.subservice: backup-restore
ms.topic: reference
author: MashaMSFT
ms.author: mathoma
---

# IServerVirtualDeviceSet2::Open (VDI)

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

The **Open** function opens the virtual device set on the server, and it must be the first call made using the COM-provided interface handle.

## Syntax

```c
HRESULT IServerVirtualDeviceSet2::Open (
   LPCWSTR      lpInstanceName,
   LPCWSTR      lpName
);
```

## Parameters

*lpInstanceName*
This string identifies the SQL Server instance to which the SQL command will be sent. NULL may be passed to identify the default instance on the current machine.

*lpName*
This is provided from the first VIRTUAL_DEVICE= clause of the BACKUP or RESTORE command. This name is used as the key to obtain access to the virtual device set created by the client.

## Return Value

|Return Value | Explanation |
|---|---|
| NOERROR | The function succeeded. |
| VD_E_INVALID | The name provided did not identify a virtual device set that is accessible to the server. |

## Remarks

After this function is successfully invoked, the server may proceed to configure the virtual device set by using GetConfiguration and SetConfiguration.

## Next steps

For more information, see the [SQL Server virtual device interface reference overview](reference-virtual-device-interface.md).