---
title: IServerVirtualDeviceSet2::AllocateBuffer
titlesuffix: SQL Server VDI reference
description: This article provides reference for the IServerVirtualDeviceSet2::AllocateBuffer command.
ms.date: 08/30/2019
ms.service: sql
ms.subservice: backup-restore
ms.topic: reference
author: MashaMSFT
ms.author: mathoma
---

# IServerVirtualDeviceSet2::AllocateBuffer (VDI)

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

The **AllocateBuffer** function obtains a shared memory buffer from the virtual device set.

## Syntax

```c
HRESULT IServerVirtualDeviceSet2::AllocateBuffer (
   LPVOID*      ppBuffer,
   UINT32      dwSize,
   UINT32      dwAlignment
);
```

## Parameters

*ppBuffer*
This returns a pointer to the start of the buffer.

*dwSize*
This is the size of the buffer in bytes. This does not include any prefix zone requested by the client. Any such zone is hidden from the server and there will be space available prior to when the buffer address is returned.

*dwAlignment*
This specifies the alignment boundary for the buffer. For example, a value of 4096 would ensure that the buffer is aligned on a 4096-byte boundary. This means that the address returned would have the low order 12 bits set to zero. This parameter must be a power of 2.

## Return Value

|Return Value | Explanation |
|---|---|
| NOERROR | The buffer is returned. |
| VD_E_MEMORY | An out of memory condition has occurred. |
| VD_E_INVALID | A parameter was invalid. |

## Next steps

For more information, see the [SQL Server virtual device interface reference overview](reference-virtual-device-interface.md).