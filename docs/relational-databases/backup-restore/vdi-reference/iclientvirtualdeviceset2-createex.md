---
title: IClientVirtualDeviceSet2::CreateEx
titlesuffix: SQL Server VDI reference
description: This article provides reference for the IClientVirtualDeviceSet2::CreateEx command.
ms.date: 08/30/2019
ms.service: sql
ms.subservice: backup-restore
ms.topic: reference
author: MashaMSFT
ms.author: mathoma
---

# IClientVirtualDeviceSet2::CreateEx (VDI)

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

The **CreateEx** function creates the virtual device set.

## Syntax

```c
HRESULT IClientVirtualDeviceSet2::CreateEx (
   LPCWSTR         lpInstanceName,
   LPCWSTR         lpName,
   VDConfig*      pCfg
);
```

## Parameters

*lpInstanceName*
This string identifies the SQL Server instance to which the SQL command will be sent.

*lpName*
This identifies the virtual device set. The rules for names used by CreateFileMapping() must be followed. Any character except backslash (\) may be used. This is a wide-character Unicode string. Prefixing the string with the user's product or company name and database name is recommended.

*pCfg*
This is the configuration for the virtual device set. For more information, see Configuration.

## Return Value

|Return Value | Explanation |
|---|---|
| NOERROR | The function succeeded. |
| VD_E_NOTSUPPORTED | One or more of the fields in the configuration was invalid or otherwise unsupported. |
| VD_E_PROTOCOL | The virtual device set has been created. |

## Remarks

The CreateEx method should be called only once per BACKUP or RESTORE operation. After invoking the Close method, the client can reuse the interface to create another virtual device set.

The instance name must identify the instance to which the Transact-SQL is issued. NULL identifies the default instance. No "machineName\" prefix is accepted.

The CreateEx (and Create) calls will modify the security DACL on the process handle in the client process. Because of this, any other modification of the process handle must be serialized with invocation of CreateEx. CreateEx will serialize with other calls to CreateEx, but is unable to serialize with external processing. Access is granted to the account running the SQL Server service.

The CreateEx method supersedes the Create method defined in the original IClientVirtualDeviceSet. The original Create method is deprecated and should not be used in future development. The original Create method implements a form of instance name support with the _VIRTUAL_SERVER_NAME_ environment variable. If that variable is set in the environment, then the Create method internally calls CreateEx, passing the value of _VIRTUAL_SERVER_NAME_ as the instance name.

## Next steps

For more information, see the [SQL Server virtual device interface reference overview](reference-virtual-device-interface.md).