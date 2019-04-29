---
title: "Tables and Indexes | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "OLE DB, indexes"
  - "OLE DB, tables"
  - "ITableDefinition interface"
  - "tables [OLE DB]"
  - "IIndexDefinition interface"
  - "SQL Server Native Client OLE DB provider, tables"
  - "SQL Server Native Client OLE DB provider, indexes"
  - "indexes [OLE DB]"
ms.assetid: 4217c6d8-8cd2-43dc-b36f-3cfd8a58fabc
author: MightyPen
ms.author: genemi
manager: craigg
---
# Tables and Indexes
  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider exposes the **IIndexDefinition** and **ITableDefinition** interfaces, allowing consumers to create, alter, and drop [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tables and indexes. Valid table and index definitions depend on the version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 The ability to create or drop tables and indexes depends on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] access rights of the consumer-application user. Dropping a table can be further constrained by the presence of declarative referential integrity constraints or other factors.  
  
 Most applications targeting [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] use SQL-DMO instead of these [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider interfaces. SQL-DMO is a collection of OLE Automation objects that support all the administrative functions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Applications targeting multiple OLE DB providers use these generic OLE DB interfaces that are supported by the various OLE DB providers.  
  
 In the provider-specific property set DBPROPSET_SQLSERVERCOLUMN, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] defines the following property.  
  
|Property ID|Description|  
|-----------------|-----------------|  
|SSPROP_COL_COLLATIONNAME|Type: VT_BSTR<br /><br /> R/W: Write<br /><br /> Default: Null<br /><br /> Description: This property is used only in **ITableDefinition**. The string specified in this property is used when creating a [CREATE TABLE](/sql/t-sql/statements/create-table-transact-sql)<br /><br /> statement.|  
  
## In This Section  
  
-   [Creating SQL Server Tables](../../relational-databases/native-client-ole-db-tables-indexes/creating-sql-server-tables.md)  
  
-   [Adding a Column to a SQL Server Table](../../relational-databases/native-client-ole-db-tables-indexes/adding-a-column-to-a-sql-server-table.md)  
  
-   [Removing a Column from a SQL Server Table](../../relational-databases/native-client-ole-db-tables-indexes/removing-a-column-from-a-sql-server-table.md)  
  
-   [Dropping a SQL Server Table](../../relational-databases/native-client-ole-db-tables-indexes/dropping-a-sql-server-table.md)  
  
-   [Creating SQL Server Indexes](../../relational-databases/indexes/indexes.md)  
  
-   [Dropping a SQL Server Index](../../relational-databases/native-client-ole-db-tables-indexes/dropping-a-sql-server-index.md)  
  
## See Also  
 [SQL Server Native Client &#40;OLE DB&#41;](../../relational-databases/native-client/ole-db/sql-server-native-client-ole-db.md)   
 [DROP TABLE &#40;Transact-SQL&#41;](/sql/t-sql/statements/drop-table-transact-sql)   
 [CREATE INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-index-transact-sql)   
 [DROP INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/drop-index-transact-sql)  
  
  
