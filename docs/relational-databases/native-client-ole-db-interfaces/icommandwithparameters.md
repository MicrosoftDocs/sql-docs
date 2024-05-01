---
title: "ICommandWithParameters (Native Client OLE DB provider)"
description: "ICommandWithParameters (Native Client OLE DB provider)"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
---
# ICommandWithParameters (Native Client OLE DB Provider)
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

> [!IMPORTANT]
> [!INCLUDE[snac-removed-oledb-only](../../includes/snac-removed-oledb-only.md)]

  Improvements in the database engine beginning with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] allow ICommandWithParameters::GetParameterInfo to obtain more accurate descriptions of the expected results. These more accurate results may differ from the values returned by CommandWithParameters::GetParameterInfo in previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Metadata Discovery](../../relational-databases/native-client/features/metadata-discovery.md).  
  
 Also beginning in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], when calling ICommandWithParameters::SetParameterInfo, the value passed to the *pwszName* parameter must be a valid identifier. For more information, see [Database Identifiers](../../relational-databases/databases/database-identifiers.md).  
  
## See Also  
 [Interfaces &#40;OLE DB&#41;](./sql-server-native-client-ole-db-interfaces.md)  
  
