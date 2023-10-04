---
title: Remove column from SQL Server table (OLE DB driver)
description: "The OLE DB Driver for SQL Server exposes the ITableDefinition::DropColumn function, which allows consumers to remove a column from a SQL Server table."
author: David-Engel
ms.author: v-davidengel
ms.date: "06/14/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
helpviewer_keywords:
  - "columns [OLE DB]"
  - "removing columns"
  - "DropColumn function"
  - "OLE DB Driver for SQL Server, columns"
---
# Removing a Column from a SQL Server Table
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  The OLE DB Driver for SQL Server exposes the **ITableDefinition::DropColumn** function. This allows consumers to remove a column from a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] table.  
  
 Consumers specify the table name as a Unicode character string in the *pwszName*member of the *uName* union in the *pTableID* parameter. The *eKind*member of *pTableID* must be DBKIND_NAME.  
  
 The consumer indicates a column name in the *pwszName*member of the *uName* union in the *pColumnID* parameter. The column name is a Unicode character string. The *eKind* member of *pColumnID* must be DBKIND_NAME.  
  
## Example  
  
### Code  
  
```  
DBID TableID;  
DBID ColumnID;  
HRESULT hr;  
  
TableID.eKind = DBKIND_NAME;  
TableID.uName.pwszName = L"MyTableName";  
  
ColumnID.eKind = DBKIND_NAME;  
ColumnID.uName.pwszName = L"MyColumnName";  
  
hr = m_pITableDefinition->DropColumn(&TableID, &ColumnID);  
```  
  
## See Also  
 [Tables and Indexes](../../oledb/ole-db-tables-indexes/tables-and-indexes.md)  
  
  
