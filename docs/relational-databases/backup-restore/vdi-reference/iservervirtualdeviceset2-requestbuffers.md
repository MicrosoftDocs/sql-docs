---
title: IServerVirtualDeviceSet2::RequestBuffers
titlesuffix: SQL Server VDI reference
description: This article provides reference for the IServerVirtualDeviceSet2::RequestBuffers command.
ms.date: 08/30/2019
ms.service: sql
ms.subservice: backup-restore
ms.topic: reference
author: MashaMSFT
ms.author: mathoma
---

# IServerVirtualDeviceSet2::RequestBuffers (VDI)

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

The **RequestBuffers** function informs the VDI that the server will need a certain number of buffers with a given size and alignment requirement.

## Syntax

```c
HRESULT IServerVirtualDeviceSet2::RequestBuffers (
   DWORD   dwSize,
   DWORD   dwAlignment,
   DWORD   dwCount
);
```

## Parameters

*dwSize*
Identifies the size of each buffer. This size should only include the region needed for data. The VDI takes care of any alignment and prefix requirements.

*dwAlignment*
The alignment required for these buffers. A value more restrictive than the basic alignment value specified with 'BeginConfiguration' may be used. If the value is 0, it will default to the basic alignment value.

*dwCount*
The number of buffers which will be requested by 'AllocateBuffer' with the given size and alignment.

## Return Value

|Return Value | Explanation |
|---|---|
| NOERROR | The function succeeded. |
| VD_E_ABORT | Abort was requested. |
| VD_E_PROTOCOL | The set is not in a state in which buffer allocations may be declared (see the state transition matrix). |
| VD_E_MEMORY | The requested memory could not be obtained. |

## Remarks

This method should be used before buffers are allocated with AllocateBuffer. Sets of buffers with a given size and alignment are requested with RequestBuffers and then individual buffers are allocated with AllocateBuffer.

During the configuration phase, RequestBuffers calls are "summed" together so that at the EndConfiguration call a single buffer area can be used (it is allocated at that time). After configuration is complete, any RequestBuffers calls result in immediate allocation of more buffer space.

## Next steps

For more information, see the [SQL Server virtual device interface reference overview](reference-virtual-device-interface.md).