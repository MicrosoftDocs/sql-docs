---
title: "Removing a Column from a SQL Server Table | Microsoft Docs"
ms.custom: ""
ms.date: "02/28/2018"
ms.prod: "sql-non-specified"
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.service: ""
ms.component: "ole-db-tables-indexes"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "columns [OLE DB]"
  - "removing columns"
  - "DropColumn function"
  - "OLE DB Driver for SQL Server, columns"
ms.assetid: 210811b7-cbd6-421e-bc6e-df9482236768
caps.latest.revision: 33
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
ms.workload: "Inactive"
---
# Removing a Column from a SQL Server Table
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

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
  
  
