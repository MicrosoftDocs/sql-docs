---
title: IServerVirtualDeviceSet2::IsSharedBuffer
titlesuffix: SQL Server VDI reference
description: This article provides reference for the IServerVirtualDeviceSet2::IsSharedBuffer command.
ms.date: 08/30/2019
ms.service: sql
ms.subservice: backup-restore
ms.topic: reference
author: MashaMSFT
ms.author: mathoma
---

# IServerVirtualDeviceSet2::IsSharedBuffer (VDI)

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

The **IsSharedBuffer** function determines if the given buffer address refers to one of the shared buffers made available by the AllocateBuffer method.

## Syntax

```c
HRESULT IServerVirtualDeviceSet2::IsSharedBuffer (
   LPVOID   lpBuffer
);
```

## Parameters

*lpBuffer*
This is an address of a buffer.

## Return Value

|Return Value | Explanation |
|---|---|
| TRUE | The buffer is a shared buffer. |
| FALSE | The buffer is not part of the virtual device set. |

## Next steps

For more information, see the [SQL Server virtual device interface reference overview](reference-virtual-device-interface.md).