---
title: IClientVirtualDeviceSet2::OpenInSecondaryEx
titlesuffix: SQL Server VDI reference
description: This article provides reference for the IClientVirtualDeviceSet2::OpenInSecondaryEx command.
ms.date: 08/30/2019
ms.service: sql
ms.subservice: backup-restore
ms.topic: reference
author: MashaMSFT
ms.author: mathoma
---

# IClientVirtualDeviceSet2::OpenInSecondaryEx (VDI)

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

The **OpenInSecondaryEx** function opens the virtual device set in a secondary client. The primary client must have already used CreateEx and GetConfiguration to set up the virtual device set.

## Syntax

```c
HRESULT IClientVirtualDeviceSet2::OpenInSecondaryEx (
   LPCWSTR      lpInstanceName,
   LPCWSTR      lpSetName
);
```

## Parameters

*lpInstanceName*
   This string identifies the SQL Server instance to which the SQL command will be sent.

*lpSetName*
   This identifies the set. This name is case-sensitive and must match the name used by the primary client when it invoked IClientVirtualDeviceSet2::Create.

## Return Value

|Return Value | Explanation |
|---|---|
| NOERROR | The function succeeded. |
| VD_E_PROTOCOL | The virtual device set has been opened or the virtual device set is not ready to accept open requests from secondary clients. |
| VD_E_ABORT | The operation is being aborted. |

## Remarks

When using a multiple process model, the primary client is responsible for detecting normal and abnormal termination of secondary clients.

The instance name must identify the instance to which the T-SQL is issued. NULL identifies the default instance. No "machineName\" prefix is accepted.

OpenInSecondaryEx supersedes the original IClientVirtualDeviceSet::OpenInSecondary that was defined in the original SQL Server version 7.0 interface. New development should use OpenInSecondaryEx.

## Next steps

For more information, see the [SQL Server virtual device interface reference overview](reference-virtual-device-interface.md).