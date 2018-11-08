---
title: "Removing a Column from a SQL Server Table | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "columns [OLE DB]"
  - "removing columns"
  - "DropColumn function"
  - "SQL Server Native Client OLE DB provider, columns"
ms.assetid: 210811b7-cbd6-421e-bc6e-df9482236768
author: MightyPen
ms.author: genemi
manager: craigg
---
# Removing a Column from a SQL Server Table
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
 [Tables and Indexes](tables-and-indexes.md)  
  
  
