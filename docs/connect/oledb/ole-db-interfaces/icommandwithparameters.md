---
title: "ICommandWithParameters | Microsoft Docs"
description: "ICommandWithParameters interface"
ms.custom: ""
ms.date: "03/26/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.component: "ole-db-interfaces"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "reference"
author: "pmasl"
ms.author: "Pedro.Lopes"
manager: craigg
---
# ICommandWithParameters
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

  Improvements in the database engine beginning with [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)] allow ICommandWithParameters::GetParameterInfo to obtain more accurate descriptions of the expected results. These more accurate results may differ from the values returned by CommandWithParameters::GetParameterInfo in previous versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. For more information, see [Metadata Discovery](../../oledb/features/metadata-discovery.md).  
  
 Also beginning in [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)], when calling ICommandWithParameters::SetParameterInfo, the value passed to the *pwszName* parameter must be a valid identifier. For more information, see [Database Identifiers](../../../relational-databases/databases/database-identifiers.md).  
  
## See Also  
 [Interfaces &#40;OLE DB&#41;](../../oledb/ole-db-interfaces/oledb-driver-for-sql-server-ole-db-interfaces.md) 
  
  
