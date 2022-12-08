---
title: "Add column to SQL Server table (OLE DB driver)"
description: "Learn how the ITableDefinition::AddColumn method allows consumers to add a column to a SQL Server table in OLE DB Driver for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.date: "06/14/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
helpviewer_keywords:
  - "columns [OLE DB]"
  - "AddColumn function"
  - "OLE DB Driver for SQL Server, columns"
  - "adding columns"
---
# Adding a Column to a SQL Server Table
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  The OLE DB Driver for SQL Server exposes the **ITableDefinition::AddColumn** function. This allows consumers to add a column to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] table.  
  
 When you add a column to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] table, the OLE DB Driver for SQL Server consumer is constrained as follows:  
  
-   If DBPROP_COL_AUTOINCREMENT is VARIANT_TRUE, DBPROP_COL_NULLABLE must be VARIANT_FALSE.  
  
-   If the column is defined by using the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] **timestamp** data type, DBPROP_COL_NULLABLE must be VARIANT_FALSE.  
  
-   For any other column definition, DBPROP_COL_NULLABLE must be VARIANT_TRUE.  
  
 Consumers specify the table name as a Unicode character string in the *pwszName* member of the *uName* union in the *pTableID* parameter. The *eKind* member of *pTableID* must be DBKIND_NAME.  
  
 The new column name is specified as a Unicode character string in the *pwszName* member of the *uName* union in the *dbcid* member of the DBCOLUMNDESC parameter *pColumnDesc*. The *eKind* member must be DBKIND_NAME.  
  
## See Also  
 [Tables and Indexes](../../oledb/ole-db-tables-indexes/tables-and-indexes.md)   
 [ALTER TABLE &#40;Transact-SQL&#41;](../../../t-sql/statements/alter-table-transact-sql.md)  
  
  
