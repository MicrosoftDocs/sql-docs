---
title: Using Transparent Network IP Resolution
description: Learn about Transparent Network IP Resolution in the ODBC Driver for SQL Server and how it affects the MultiSubnetFailover feature.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sunilbs, mcimfl, randolphwest
ms.date: 08/17/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ai-usage: ai-assisted
---
# Use transparent network IP resolution with the ODBC driver

[!INCLUDE [Driver_ODBC_Download](../../includes/driver_odbc_download.md)]

> [!NOTE]  
> When you set `MultiSubnetFailover=Yes` (the recommended setting for Azure SQL Database, Azure SQL Managed Instance, SQL database in Microsoft Fabric, availability group listeners, and failover cluster instances), the driver attempts TCP connections to all resolved IP addresses in parallel, and the **TransparentNetworkIPResolution** setting doesn't affect the connection sequence. For more information, see [High availability and disaster recovery](odbc-driver-support-for-high-availability-disaster-recovery.md).

TransparentNetworkIPResolution (TNIR) is a legacy multi-IP fallback in the ODBC Driver for SQL Server, available starting with Microsoft ODBC Driver 13.1. It affects the connect sequence when the first resolved IP of the hostname doesn't respond and multiple IPs are associated with the hostname.

TNIR is superseded by **MultiSubnetFailover**. When `MultiSubnetFailover=Yes`, the driver attempts TCP connections to all resolved IP addresses in parallel and TNIR doesn't affect the connection sequence. When `MultiSubnetFailover=No`, TNIR controls the fallback sequence:

| TransparentNetworkIPResolution | Connect sequence |
| :---: | --- |
| Enabled (default) | Try one IP, then all IPs in parallel |
| Disabled | Try all IPs sequentially |

The `TransparentNetworkIPResolution` connection string and DSN keyword controls this setting at the connection-string level. The default is enabled.

| Keyword | Values | Default |
| --- | --- | --- |
| `TransparentNetworkIPResolution` | `Enabled`, `Disabled` | `Enabled` |

The `SQL_COPT_SS_TNIR` pre-connection attribute allows an application to control this setting programmatically:

| Connection attribute | Size/Type | Values | Default | Description |
| --- | --- | --- | --- | --- |
| `SQL_COPT_SS_TNIR` (1249) | `SQL_IS_INTEGER` or `SQL_IS_UINTEGER` | `SQL_IS_ON` (1), `SQL_IS_OFF` (0) | `SQL_IS_ON` | Enables or disables TNIR. |

## Related content

- [High availability and disaster recovery](odbc-driver-support-for-high-availability-disaster-recovery.md)
- [Microsoft ODBC Driver for SQL Server on Windows](windows/microsoft-odbc-driver-for-sql-server-on-windows.md)
- [SQL Server multi-subnet clustering](../../sql-server/failover-clusters/windows/sql-server-multi-subnet-clustering-sql-server.md)
