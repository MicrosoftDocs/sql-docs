---
title: Remove column from SQL Server table (Native Client OLE DB provider)
description: "Removing a Column from a SQL Server Table (Native Client OLE DB provider)"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords:
  - "columns [OLE DB]"
  - "removing columns"
  - "DropColumn function"
  - "SQL Server Native Client OLE DB provider, columns"
---
# Removing a Column from a SQL Server Table (Native Client OLE DB provider)
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider exposes the **ITableDefinition::DropColumn** function. This allows consumers to remove a column from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table.  
  
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
 [Tables and Indexes](../../relational-databases/native-client-ole-db-tables-indexes/tables-and-indexes.md)  
  
  
