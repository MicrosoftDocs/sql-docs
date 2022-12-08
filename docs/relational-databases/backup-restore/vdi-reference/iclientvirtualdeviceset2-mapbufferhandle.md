---
title: IClientVirtualDeviceSet2::MapBufferHandle
titlesuffix: SQL Server VDI reference
description: This article provides reference for the IClientVirtualDeviceSet2::MapBufferHandle command.
ms.date: 08/30/2019
ms.service: sql
ms.subservice: backup-restore
ms.topic: reference
author: MashaMSFT
ms.author: mathoma
---

# IClientVirtualDeviceSet2::MapBufferHandle (VDI)

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

The **MapBufferHandle** function is used to obtain a valid buffer address from a buffer handle obtained from some other process.

## Syntax

```c
HRESULT IClientVirtualDeviceSet2::MapBufferHandle (
   DWORD      dwBuffer,
   BYTE**      ppBuffer
);
```

## Parameters

*dwBuffer*
This is the handle returned by IClientVirtualDeviceSet2::GetBufferHandle.

*ppBuffer*
This is the address of the buffer that is valid in the current process.

## Return Value

|Return Value | Explanation |
|---|---|
| NOERROR | The function succeeded. |
| VD_E_PROTOCOL | The virtual device set is not currently open. |
| VD_E_INVALID | The ppBuffer is an invalid handle. |

## Remarks

Care must be taken to communicate the handles correctly. Handles are local to a single virtual device set. The partner processes sharing a handle must ensure that buffer handles are used only within the scope of the virtual device set from which the buffer was originally obtained.

## Next steps

For more information, see the [SQL Server virtual device interface reference overview](reference-virtual-device-interface.md).