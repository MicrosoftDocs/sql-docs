---
title: IClientVirtualDevice::CompleteCommand
titlesuffix: SQL Server VDI reference
description: This article provides reference for the IClientVirtualDevice::CompleteCommand command.
ms.date: 08/30/2019
ms.service: sql
ms.subservice: backup-restore
ms.topic: reference
author: MashaMSFT
ms.author: mathoma
---

# IClientVirtualDevice::CompleteCommand (VDI)

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

The **CompleteCommand** function is used to notify SQL Server that a command has finished. Completion information appropriate for the command should be returned.

## Syntax

```c
HRESULT IClientVirtualDevice::CompleteCommand (
   VDC_Command*         const pCmd,
   UINT32               dwCompletionCode,
   UINT32               dwBytesTransferred,
   UINT64               dwlPosition
);
```

## Parameters

*pCmd*
This is the address of a command previously returned from IClientVirtualDevice::GetCommand.

*dwCompletionCode*
This is a WIN32 status code that indicates the completion status. This parameter must be returned for all commands. The code returned should be appropriate to the command being performed. ERROR_SUCCESS is used in all cases to denote a successfully executed command. For the complete list of possible codes, see the file, Winerror.h. A list of typical status codes for each command appears in Commands.

*dwBytesTransferred*
This is the number of successfully transferred bytes. This is returned only for data transfer commands Read and Write.

*dwlPosition*
This is a response to the GetPosition command only.

## Return Value

|Return Value | Explanation |
|---|---|
| NOERROR | The completion was correctly noted. |
| VD_E_INVALID | pCmd was not an active command. |
| VD_E_ABORT | Abort was signaled. |
| VD_E_PROTOCOL | The device is not open. |

## Next steps

For more information, see the [SQL Server virtual device interface reference overview](reference-virtual-device-interface.md).
