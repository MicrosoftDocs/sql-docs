---
title: "ICommandWithParameters (OLE DB driver)"
description: "Learn how improvements allow ICommandWithParameters::GetParameterInfo to obtain more accurate descriptions of expected results for OLE DB Driver for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.date: "06/14/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# ICommandWithParameters
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  Improvements in the database engine beginning with [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)] allow ICommandWithParameters::GetParameterInfo to obtain more accurate descriptions of the expected results. These more accurate results may differ from the values returned by CommandWithParameters::GetParameterInfo in previous versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. For more information, see [Metadata Discovery](../../oledb/features/metadata-discovery.md).  
  
 Also beginning in [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)], when calling ICommandWithParameters::SetParameterInfo, the value passed to the *pwszName* parameter must be a valid identifier. For more information, see [Database Identifiers](../../../relational-databases/databases/database-identifiers.md).  
  
## See Also  
 [Interfaces &#40;OLE DB&#41;](../../oledb/ole-db-interfaces/oledb-driver-for-sql-server-ole-db-interfaces.md) 
  
  
