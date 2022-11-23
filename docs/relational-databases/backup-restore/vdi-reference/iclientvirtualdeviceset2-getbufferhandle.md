---
title: IClientVirtualDeviceSet2::GetBufferHandle
titlesuffix: SQL Server VDI reference
description: This article provides reference for the IClientVirtualDeviceSet2::GetBufferHandle command.
ms.date: 08/30/2019
ms.service: sql
ms.subservice: backup-restore
ms.topic: reference
author: MashaMSFT
ms.author: mathoma
---

# IClientVirtualDeviceSet2::GetBufferHandle (VDI)

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

Some applications may require more than one process to operate on the buffers returned by **IClientVirtualDevice2::GetCommand**. In such cases, the process that receives the command can use **GetBufferHandle** to obtain a process independent handle that identifies the buffer. This handle can then be communicated to any other process that also has the same Virtual Device Set open. That process would then use IClientVirtualDeviceSet2::MapBufferHandle to obtain the address of the buffer. The address will likely be a different address than in its partner because each process may be mapping buffers at different addresses.

## Syntax

```c
HRESULT IClientVirtualDeviceSet2::GetBufferHandle (
   BYTE*         pBuffer,
   DWORD*      pBufferHandle
);
```

## Parameters

*pBuffer*
This is the address of a buffer obtained from a Read or Write command.

*pBufferHandle*
A unique identifier for the buffer is returned.

## Return Value

|Return Value | Explanation |
|---|---|
| NOERROR | The function succeeded. |
| VD_E_PROTOCOL | The virtual device set is not currently open. |
| VD_E_INVALID | The pBuffer is not a valid address. |

## Remarks

The process that invokes the GetBufferHandle function is responsible for invoking IClientVirtualDevice2::CompleteCommand when the data transfer is complete.

## Next steps

For more information, see the [SQL Server virtual device interface reference overview](reference-virtual-device-interface.md).