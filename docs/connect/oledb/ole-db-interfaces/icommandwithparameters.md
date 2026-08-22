---
title: "ICommandWithParameters (OLE DB driver)"
description: "Learn how improvements allow ICommandWithParameters::GetParameterInfo to obtain more accurate descriptions of expected results for OLE DB Driver for SQL Server."
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sunilbs, mcimfl
ms.date: "06/14/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
ms.custom:
  - ignite-2025
---
# ICommandWithParameters
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics PDW FabricSQLDB](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  Improvements in the database engine beginning with [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)] allow ICommandWithParameters::GetParameterInfo to obtain more accurate descriptions of the expected results. These more accurate results may differ from the values returned by CommandWithParameters::GetParameterInfo in previous versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. For more information, see [Metadata Discovery](../../oledb/features/metadata-discovery.md).  
  
 Also beginning in [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)], when calling ICommandWithParameters::SetParameterInfo, the value passed to the *pwszName* parameter must be a valid identifier. For more information, see [Database Identifiers](../../../relational-databases/databases/database-identifiers.md).  
  
## Related content

- [OLE DB Driver for SQL Server (OLE DB) Interfaces](oledb-driver-for-sql-server-ole-db-interfaces.md)
