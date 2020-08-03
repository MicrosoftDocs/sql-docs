---
title: SQL Server master instance configuration properties
titleSuffix: SQL Server big data clusters
description: Reference article for configuration properties for SQL Server master instance.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: rahul.ajmera
ms.date: 07/16/2020
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# SQL Server master instance configuration properties

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

## Properties

You can configure the following SQL Server options for the master instance at deployment time.

|Property|Options|
| --- | --- |
|`[sqlagent]`|`enabled = { true | false }` |
|`[telemetry]`|`customerfeedback = { true | false }` |
|`[telemetry]`|`userRequestedLocalAuditDirectory = </path/file>`|
|`[DEFAULT]`| `pid = { Enterprise | Developer }`|
|`[traceflag]`|` traceflag<#> = <####>`|

### Examples

The following example enables SQL Agent, telemetry, sets a PID for Enterprise Edition, and enables trace flag 1204.:

```
[sqlagent]
enabled=true

[telemetry]
customerfeedback=true
userRequestedLocalAuditDirectory = /tmp/audit

[DEFAULT]
pid = Enterprise

[traceflag]
traceflag0 = 1204
```

For instructions, see [Configure master instance of [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](configure-sql-server-master-instance.md).

## Next steps

[Configure master instance of [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](configure-sql-server-master-instance.md)
