---
title: "Using Transparent Network IP Resolution"
description: "Reference for the legacy TransparentNetworkIPResolution (TNIR) connection property in the OLE DB Driver for SQL Server, superseded by MultiSubnetFailover."
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: randolphwest, davidengel, sunilbs, mcimfl
ms.date: 08/21/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
ms.custom:
  - ignite-2025
ai-usage: ai-assisted
---
# Using Transparent Network IP Resolution
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics PDW FabricSQLDB](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

> [!NOTE]
> `TransparentNetworkIPResolution` is a legacy multi-IP fallback. For Azure SQL Database, Azure SQL Managed Instance, SQL database in Microsoft Fabric, availability group listeners, and failover cluster instances, the recommended setting is `MultiSubnetFailover=Yes`, which attempts TCP connections to all resolved IP addresses in parallel and uses the first connection that succeeds. When `MultiSubnetFailover=Yes`, `TransparentNetworkIPResolution` doesn't affect the connection sequence. For more information, see [OLE DB Driver for SQL Server Support for High Availability, Disaster Recovery](oledb-driver-for-sql-server-support-for-high-availability-disaster-recovery.md).

## Purpose

`TransparentNetworkIPResolution` (TNIR) is a legacy multi-IP fallback in the OLE DB Driver for SQL Server. It's superseded by `MultiSubnetFailover`, which is the recommended setting for Azure SQL Database, Azure SQL Managed Instance, SQL database in Microsoft Fabric, availability group listeners, and failover cluster instances. When `MultiSubnetFailover=Yes`, the driver attempts TCP connections to all resolved IP addresses in parallel and TNIR doesn't affect the connection sequence.

When `MultiSubnetFailover=No`, TNIR controls how the driver handles multi-IP hostnames. The following table uses the `Yes` and `No` values accepted by `IDBInitialize::Initialize`. When you set the property through `IDataInitialize::GetDataSource` or ADO, use `True` and `False` instead.

| `TransparentNetworkIPResolution` | Behavior when `MultiSubnetFailover=No` |
|---|---|
| `Yes` (default) | Try one IP, then all IPs in parallel. |
| `No` | Try all IPs sequentially. |

## Setting Transparent Network IP Resolution

`TransparentNetworkIPResolution` is enabled by default. `MultiSubnetFailover` is disabled by default. The following articles provide more information about setting these properties:

- [Using Connection String Keywords with OLE DB Driver for SQL Server](../applications/using-connection-string-keywords-with-oledb-driver-for-sql-server.md)
- [Initialization and Authorization Properties](../ole-db-data-source-objects/initialization-and-authorization-properties.md)

## Related content

- [OLE DB Driver for SQL Server Support for High Availability, Disaster Recovery](oledb-driver-for-sql-server-support-for-high-availability-disaster-recovery.md)
