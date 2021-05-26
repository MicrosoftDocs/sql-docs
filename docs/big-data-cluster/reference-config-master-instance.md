---
title: SQL Server master instance configuration properties
titleSuffix: SQL Server big data clusters
description: Reference article for configuration properties for SQL Server master instance.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: rahul.ajmera
ms.date: 02/11/2021
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# SQL Server Master Instance Configuration Properties -  Pre CU9 Release

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]
> [!NOTE]
> The following information is only applicable to pre-CU9 release clusters that are not configuration enabled and require mssql-conf to configure the SQL Server master instance. CU9 and later release clusters take advantage of the configuration management functionality and no longer require an mssql-conf file. Available configurations for the SQL Server master instance and other BDC components can be found [here](reference-config-bdc-overview.md).

## Properties

You can configure the following SQL Server options for the master instance at deployment time.

|Property|Options|
| --- | --- |
|`[sqlagent]`|`enabled = { true | false }` |
|`[telemetry]`|`customerfeedback = { true | false }` |
|`[telemetry]`|`userRequestedLocalAuditDirectory = </path/file>`|
|`[licensing]`| `pid = { Enterprise | Developer }`|
|`[traceflag]`|` traceflag<#> = <####>`|

### Examples

The following example enables SQL Agent, telemetry, sets a PID for Enterprise Edition, and enables trace flag 1204.:

```
[sqlagent]
enabled=true

[telemetry]
customerfeedback=true
userRequestedLocalAuditDirectory = /tmp/audit

[licensing]
pid = Enterprise

[traceflag]
traceflag0 = 1204
```

For instructions, see [Configure master instance of [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](configure-sql-server-master-instance.md).

## Next steps

[Configure master instance of [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](configure-sql-server-master-instance.md)
