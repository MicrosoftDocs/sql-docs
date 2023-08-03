---
title: "Drop SQL Server index (OLE DB driver)"
description: "Learn about the IIndexDefinition::DropIndex function in OLE DB Driver for SQL Server, which allows consumers to remove an index from a SQL Server table."
author: David-Engel
ms.author: v-davidengel
ms.date: "06/14/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
helpviewer_keywords:
  - "removing indexes"
  - "deleting indexes"
  - "DropIndex function"
  - "dropping indexes"
  - "OLE DB Driver for SQL Server, indexes"
  - "indexes [OLE DB]"
---
# Dropping a SQL Server Index
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  The OLE DB Driver for SQL Server exposes the **IIndexDefinition::DropIndex** function. This allows consumers to remove an index from a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] table.  
  
 The OLE DB Driver for SQL Server exposes some [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] PRIMARY KEY and UNIQUE constraints as indexes. The table owner, database owner, and some administrative role members can modify a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] table, dropping a constraint. By default, only the table owner can drop an existing index. Therefore, **DropIndex** success or failure depends not only on the application user's access rights but also on the type of index indicated.  
  
 Consumers specify the table name as a Unicode character string in the *pwszName* member of the *uName* union in the *pTableID* parameter. The *eKind* member of *pTableID* must be DBKIND_NAME.  
  
 Consumers specify the index name as a Unicode character string in the *pwszName* member of the *uName* union in the *pIndexID* parameter. The *eKind* member of *pIndexID* must be DBKIND_NAME. The OLE DB Driver for SQL Server does not support the OLE DB feature of dropping all indexes on a table when *pIndexID* is null. If *pIndexID* is null, E_INVALIDARG is returned.  
  
## See Also  
 [Tables and Indexes](../../oledb/ole-db-tables-indexes/tables-and-indexes.md)   
 [ALTER TABLE &#40;Transact-SQL&#41;](../../../t-sql/statements/alter-table-transact-sql.md)   
 [DROP INDEX &#40;Transact-SQL&#41;](../../../t-sql/statements/drop-index-transact-sql.md)  
  
  
