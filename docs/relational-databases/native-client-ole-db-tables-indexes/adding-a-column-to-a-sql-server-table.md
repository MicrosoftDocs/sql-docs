---
description: "Add column to SQL Server table (Native Client OLE DB provider)"
title: "Add column to SQL Server table (Native Client OLE DB provider) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "columns [OLE DB]"
  - "AddColumn function"
  - "SQL Server Native Client OLE DB provider, columns"
  - "adding columns"
ms.assetid: 22bae18a-bc9d-4617-8660-ed8b17a468d4
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Adding a Column to a Table in SQL Server Native Client
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider exposes the **ITableDefinition::AddColumn** function. This allows consumers to add a column to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table.  
  
 When you add a column to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider consumer is constrained as follows:  
  
-   If DBPROP_COL_AUTOINCREMENT is VARIANT_TRUE, DBPROP_COL_NULLABLE must be VARIANT_FALSE.  
  
-   If the column is defined by using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **timestamp** data type, DBPROP_COL_NULLABLE must be VARIANT_FALSE.  
  
-   For any other column definition, DBPROP_COL_NULLABLE must be VARIANT_TRUE.  
  
 Consumers specify the table name as a Unicode character string in the *pwszName* member of the *uName* union in the *pTableID* parameter. The *eKind* member of *pTableID* must be DBKIND_NAME.  
  
 The new column name is specified as a Unicode character string in the *pwszName* member of the *uName* union in the *dbcid* member of the DBCOLUMNDESC parameter *pColumnDesc*. The *eKind* member must be DBKIND_NAME.  
  
## See Also  
 [Tables and Indexes](../../relational-databases/native-client-ole-db-tables-indexes/tables-and-indexes.md)   
 [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)  
  
  
