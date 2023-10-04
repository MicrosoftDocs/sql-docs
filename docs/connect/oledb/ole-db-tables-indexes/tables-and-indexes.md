---
title: Tables and indexes (OLE DB driver)
description: Learn about the OLE DB Driver interfaces IIndexDefinition and ITableDefinition, which allow consumers to create, alter, and drop SQL Server tables and indexes.
author: David-Engel
ms.author: v-davidengel
ms.date: "06/14/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
helpviewer_keywords:
  - "OLE DB, indexes"
  - "OLE DB, tables"
  - "ITableDefinition interface"
  - "tables [OLE DB]"
  - "IIndexDefinition interface"
  - "OLE DB Driver for SQL Server, tables"
  - "OLE DB Driver for SQL Server, indexes"
  - "indexes [OLE DB]"
---
# Tables and Indexes
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  The OLE DB Driver for SQL Server exposes the **IIndexDefinition** and **ITableDefinition** interfaces, allowing consumers to create, alter, and drop [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] tables and indexes. Valid table and index definitions depend on the version of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
 The ability to create or drop tables and indexes depends on the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] access rights of the consumer-application user. Dropping a table can be further constrained by the presence of declarative referential integrity constraints or other factors.  
  
 Most applications targeting [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] use SQL-DMO instead of these OLE DB Driver for SQL Server interfaces. SQL-DMO is a collection of OLE Automation objects that support all the administrative functions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. Applications targeting multiple OLE DB providers use these generic OLE DB interfaces that are supported by the various OLE DB providers.  
  
 In the provider-specific property set DBPROPSET_SQLSERVERCOLUMN, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] defines the following property.  
  
|Property ID|Description|  
|-----------------|-----------------|  
|SSPROP_COL_COLLATIONNAME|Type: VT_BSTR<br /><br /> R/W: Write<br /><br /> Default: Null<br /><br /> Description: This property is used only in **ITableDefinition**. The string specified in this property is used when creating a [CREATE TABLE](../../../t-sql/statements/create-table-transact-sql.md)<br /><br /> statement.|  
  
## In This Section  
  
-   [Creating SQL Server Tables](../../oledb/ole-db-tables-indexes/creating-sql-server-tables.md)  
  
-   [Adding a Column to a SQL Server Table](../../oledb/ole-db-tables-indexes/adding-a-column-to-a-sql-server-table.md)  
  
-   [Removing a Column from a SQL Server Table](../../oledb/ole-db-tables-indexes/removing-a-column-from-a-sql-server-table.md)  
  
-   [Dropping a SQL Server Table](../../oledb/ole-db-tables-indexes/dropping-a-sql-server-table.md)  
  
-   [Creating SQL Server Indexes](../../oledb/ole-db-tables-indexes/creating-sql-server-indexes.md)  
  
-   [Dropping a SQL Server Index](../../oledb/ole-db-tables-indexes/dropping-a-sql-server-index.md)  
  
## See Also  
 [OLE DB Driver for SQL Server Programming](../../oledb/ole-db/oledb-driver-for-sql-server-programming.md)   
 [DROP TABLE &#40;Transact-SQL&#41;](../../../t-sql/statements/drop-table-transact-sql.md)   
 [CREATE INDEX &#40;Transact-SQL&#41;](../../../t-sql/statements/create-index-transact-sql.md)   
 [DROP INDEX &#40;Transact-SQL&#41;](../../../t-sql/statements/drop-index-transact-sql.md)  
  
  
