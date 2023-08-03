---
title: Azure Arc-enabled SQL Server data collection and reporting
description: Explains data that Microsoft collects for reporting for Azure Arc-enabled SQL Server
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, randolphwest
ms.date: 07/06/2023
ms.topic: conceptual
ms.custom: references_regions
---
# Azure Arc-enabled SQL Server data collection and reporting

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

This article describes the data that Azure Arc-enabled SQL Server transmits to Microsoft.

Azure Arc-enabled SQL Server, and any of the related Azure Arc-enabled services, don't store any customer data.

## Related products

Azure Arc-enabled SQL Server uses the following products:

- Azure Arc-enabled servers

The following data is collected to Azure for Azure Arc-enabled SQL Servers:

## Azure Arc-enabled SQL Server

| Description | Property name | Property type |
| :-- | :-- | :-- |
| SQL Server edition | `Edition` | `string` |
| Resource ID of the hosting Azure Arc for Servers resource | `ContainerResourceId` | `string` |
| Time when the resource was created | `CreateTime` | string |
| The number of logical processors used by the SQL Server instance | `VCore` | `string` |
| Cloud connectivity status | `Status` | `string` |
| SQL Server update level | `PatchLevel` | `string` |
| SQL Server collation | `Collation` | `string` |
| SQL Server current version | `CurrentVersion` | `string` |
| SQL Server instance name | `InstanceName` | `string` |
| Dynamic TCP ports used by SQL Server | `TcpDynamicPorts` | `string` |
| Static TCP ports used by SQL Server | `TcpStaticPorts` | `string` |
| SQL Server product ID | `ProductId` | `string` |
| SQL Server provisioning state | `ProvisioningState` | `string` |

The following JSON document is an example of the SQL Server - Azure Arc resource.

```json
{

    "name": "SQL22-EE",
    "version": "SQL Server 2022",
    "edition": "Enterprise",
    "containerResourceId": "/subscriptions/00000000-0000-0000-0000-000000000000/resourcegroups/arc-eastasia/providers/Microsoft.HybridCompute/machines/SQL22-EE",
    "vCore": "8",
    "status": "Connected",
    "patchLevel": "16.0.1000.6",
    "collation": "SQL_Latin1_General_CP1_CI_AS",
    "currentVersion": "16.0.1000.6",
    "instanceName": "SQL22-EE",
    "tcpDynamicPorts": "61394",
    "tcpStaticPorts": "",
    "productId": "00488-00010-05000-AB944",
    "licenseType": "PAYG",
    "azureDefenderStatusLastUpdated": "2023-02-08T07:57:37.5597421Z",
    "azureDefenderStatus": "Protected",
    "provisioningState": "Succeeded"
}
```

## SQL Server database - Azure Arc

| Description | Property name | Property type |
| :-- | :-- | :-- |
| Database name | `name` | `string` |
| Collation | `collationName` | `string` |
| Database creation date | `databaseCreationDate` | `System.DateTime` |
| Compatibility level | `compatibilityLevel` | `string` |
| Database state | `state` | `string` |
| Readonly mode | `isReadOnly` | `boolean` |
| Recovery mode | `recoveryMode` | `boolean` |
| Auto close enabled | `isAutoCloseOn` | `boolean` |
| Auto shrink enabled | `isAutoShrinkOn` | `boolean` |
| Auto create stats enabled | `isAutoCreateStatsOn` | `boolean` |
| Auto update stats enabled | `isAutoUpdateStatsOn` | `boolean` |
| Remote data archive enabled | `isRemoteDataArchiveEnabled` | `boolean` |
! Memory optimization enabled | `isMemoryOptimizationEnabled` | `boolean` |
| Encryption enabled | `isEncrypted` | `boolean` |
| Trustworthy mode enabled | `isTrustworthyOn` | `boolean` |
| Backup information | `backupInformation` | `object` |
| Provisioning state | `provisioningState` | `string` |

The following JSON document is an example of the SQL Server database - Azure Arc resource.

```json
{
    "name": "newDb80",
    "collationName": "SQL_Latin1_General_CP1_CI_AS",
    "databaseCreationDate": "2023-01-09T03:40:45Z",
    "compatibilityLevel": 150,
    "state": "Online",
    "isReadOnly": false,
    "recoveryMode": "Full",
    "databaseOptions": {
        "isAutoCloseOn": false,
        "isAutoShrinkOn": false,
        "isAutoCreateStatsOn": true,
        "isAutoUpdateStatsOn": true,
        "isRemoteDataArchiveEnabled": false,
        "isMemoryOptimizationEnabled": true,
        "isEncrypted": false,
        "isTrustworthyOn": false
    },
    "backupInformation": {},
    "provisioningState": "Succeeded"
}
```

## Next steps

- [Automatically connect your SQL Server to Azure Arc](automatically-connect.md)

- [Configure advanced data security for your SQL Server instance](configure-advanced-data-security.md)
- [Configure best practices assessment on an Azure Arc-enabled SQL Server instance](assess.md)
