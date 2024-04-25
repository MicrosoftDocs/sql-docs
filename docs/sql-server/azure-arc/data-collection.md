---
title: Data collection and reporting
description: Explains data that Microsoft collects for reporting for SQL Server enabled by Azure Arc, and how to configure related settings.
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, randolphwest
ms.date: 12/15/2023
ms.topic: conceptual
ms.custom: references_regions
---

# Data collection and reporting for SQL Server enabled by Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

This article describes the data that [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] transmits to Microsoft. [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] collects usage data as described in this article and at [Monitor Azure Arc-enabled [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]](sql-monitoring.md).

[!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] does not collect any personally identifiable information (PII) or end-user identifiable information or store any customer data.

## Related products

[!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] uses the following products:

- Azure Arc-enabled servers

## SQL Server enabled by Azure Arc instance

The following data is collected for [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] instances:

| Description | Property name | Property type |
| :-- | :-- | :-- |
| SQL Server edition | `Edition` | `string` |
| Resource ID of the hosting Azure Arc for Servers resource | `ContainerResourceId` | `string` |
| Time when the resource was created | `CreateTime` | `string` |
| The number of logical processors used by the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance | `VCore` | `string` |
| Cloud connectivity status | `Status` | `string` |
| SQL Server update level | `PatchLevel` | `string` |
| SQL Server collation | `Collation` | `string` |
| SQL Server current version | `CurrentVersion` | `string` |
| SQL Server instance name | `InstanceName` | `string` |
| Dynamic TCP ports used by SQL Server | `TcpDynamicPorts` | `string` |
| Static TCP ports used by SQL Server | `TcpStaticPorts` | `string` |
| SQL Server product ID | `ProductId` | `string` |
| [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] provisioning state | `ProvisioningState` | `string` |

The following JSON document is an example of the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] - Azure Arc resource

```json
{
    "name": "<server name>",
    "version": "SQL Server 2022",
    "edition": "Enterprise",
    "containerResourceId": "/subscriptions/00000000-0000-0000-0000-000000000000/resourcegroups/arc-eastasia/providers/Microsoft.HybridCompute/machines/<server name>",
    "vCore": "8",
    "status": "Connected",
    "patchLevel": "16.0.1000.6",
    "collation": "SQL_Latin1_General_CP1_CI_AS",
    "currentVersion": "16.0.1000.6",
    "instanceName": "<instance name>",
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
| Memory optimization enabled | `isMemoryOptimizationEnabled` | `boolean` |
| Encryption enabled | `isEncrypted` | `boolean` |
| Trustworthy mode enabled | `isTrustworthyOn` | `boolean` |
| Backup information | `backupInformation` | `object` |
| Provisioning state | `provisioningState` | `string` |

The following JSON document is an example of the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] database - Azure Arc resource.

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

## Extension logs

The extension sends logs to Azure about extension events.

[!INCLUDE [extension-logs](includes/extension-logs.md)]

## Migration assessment metrics

Migration assessment automatically produces an assessment for migration to Azure. Learn more at [Select the optimal Azure SQL target using Migration assessment (preview) - SQL Server enabled by Azure Arc](migration-assessment.md).

[!INCLUDE [assessment-metrics](includes/assessment-metrics.md)]

## Monitoring data

The agent sends SQL Server monitoring data to Azure. You can enable and disable monitoring data that is collected. See [Monitor SQL Server enabled by Azure Arc (preview)](sql-monitoring.md).

The following lists reflect the monitoring data that is collected from DMV datasets on [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] when the monitoring feature is enabled. No personally identifiable information (PII), end-user identifiable information (EUII), or customer content is collected.

[!INCLUDE [dmv-collection](includes/dmv-collection.md)]

## Related content

- [Automatically connect your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] to Azure Arc](automatically-connect.md)
- [Configure advanced data security for your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance](configure-advanced-data-security.md)
- [Configure best practices assessment on an Azure Arc-enabled [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance](assess.md)
