---
title: "Adding a Column to a SQL Server Table | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "columns [OLE DB]"
  - "AddColumn function"
  - "SQL Server Native Client OLE DB provider, columns"
  - "adding columns"
ms.assetid: 22bae18a-bc9d-4617-8660-ed8b17a468d4
author: MightyPen
ms.author: genemi
manager: craigg
---
# Adding a Column to a SQL Server Table
  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider exposes the **ITableDefinition::AddColumn** function. This allows consumers to add a column to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table.  
  
 When you add a column to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider consumer is constrained as follows:  
  
-   If DBPROP_COL_AUTOINCREMENT is VARIANT_TRUE, DBPROP_COL_NULLABLE must be VARIANT_FALSE.  
  
-   If the column is defined by using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **timestamp** data type, DBPROP_COL_NULLABLE must be VARIANT_FALSE.  
  
-   For any other column definition, DBPROP_COL_NULLABLE must be VARIANT_TRUE.  
  
 Consumers specify the table name as a Unicode character string in the *pwszName* member of the *uName* union in the *pTableID* parameter. The *eKind* member of *pTableID* must be DBKIND_NAME.  
  
 The new column name is specified as a Unicode character string in the *pwszName* member of the *uName* union in the *dbcid* member of the DBCOLUMNDESC parameter *pColumnDesc*. The *eKind* member must be DBKIND_NAME.  
  
## See Also  
 [Tables and Indexes](tables-and-indexes.md)   
 [ALTER TABLE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-table-transact-sql)  
  
  
