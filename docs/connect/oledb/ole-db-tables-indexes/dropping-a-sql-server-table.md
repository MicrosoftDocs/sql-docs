---
title: "Dropping a SQL Server Table | Microsoft Docs"
description: "Dropping a SQL Server table using OLE DB Driver for SQL Server"
ms.custom: ""
ms.date: "06/14/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
helpviewer_keywords: 
  - "tables [OLE DB]"
  - "deleting tables"
  - "OLE DB Driver for SQL Server, tables"
  - "removing tables"
  - "dropping tables"
author: pmasl
ms.author: pelopes
manager: craigg
---
# Dropping a SQL Server Table
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  The OLE DB Driver for SQL Server exposes the **ITableDefinition::DropTable** function to remove a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] table from a database.  
  
 Specify the table name as a Unicode character string in the *pwszName* member of the *uName* union in the *pTableID* parameter. The *eKind* member of *pTableID* must be DBKIND_NAME.  
  
## See Also  
 [Tables and Indexes](../../oledb/ole-db-tables-indexes/tables-and-indexes.md)  
  
  
