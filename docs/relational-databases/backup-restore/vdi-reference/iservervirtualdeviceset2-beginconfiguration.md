---
title: IServerVirtualDeviceSet2::BeginConfiguration
titlesuffix: SQL Server VDI reference
description: This article provides reference for the IServerVirtualDeviceSet2::BeginConfiguration command.
ms.date: 08/30/2019
ms.service: sql
ms.subservice: backup-restore
ms.topic: reference
author: MashaMSFT
ms.author: mathoma
---

# IServerVirtualDeviceSet2::BeginConfiguration (VDI)

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

The server invokes the **BeginConfiguration** function to begin configuration of the virtual device set.

## Syntax

```c
HRESULT IServerVirtualDeviceSet2::BeginConfiguration (
   DWORD   dwFeatures,
   DWORD   dwAlignment,
   DWORD   dwBlockSize,
   DWORD   dwMaxTransferSize,
   DWORD   dwTimeout
);
```

## Parameters

*dwFeatures*
The modified features mask. VDF_WriteMedia and/or VDF_ReadMedia.

*dwAlignment*
The final alignment. If 0, defaults to dwBlockSize. Must be a power of 2, >= dwBlockSize and <= 64 KB.

*dwBlockSize*
The minimum unit of transfer, in bytes. Must be a power of 2, >=512 and <= 64 KB.

*dwMaxTransferSize*
The largest transfer which will be attempted. Must be a multiple of 64 KB.

*dwTimeout*
Milliseconds to wait for the primary client to finish declaring buffer areas it will provide.

## Return Value

|Return Value | Explanation |
|---|---|
| NOERROR | The virtual device set is in the Configurable state. |
| VD_E_ABORT | The SignalAbort was invoked. |
| VD_E_PROTOCOL | The virtual device set is not in the Connected state. |

## Remarks

After this function is invoked, the virtual device set moves to the Configurable state, in which buffer layout is decided.
Once the basic configuration is set (as per the parameters), these values remain fixed for the life of the virtual device set. The alignment property for the virtual device set is used to control alignment of data buffers. This value sets a minimum alignment value that may be overridden on a buffer-by-buffer basis.

## Next steps

For more information, see the [SQL Server virtual device interface reference overview](reference-virtual-device-interface.md).