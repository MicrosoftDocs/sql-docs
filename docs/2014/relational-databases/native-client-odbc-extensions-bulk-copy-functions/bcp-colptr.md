---
title: "bcp_colptr | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
api_name: 
  - "bcp_colptr"
api_location: 
  - "sqlncli11.dll"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "bcp_colptr function"
ms.assetid: 02ece13e-1da3-4f9d-b860-3177e43d2471
author: MightyPen
ms.author: genemi
manager: craigg
---
# bcp_colptr
  Sets the program variable data address for the current copy into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Syntax  
  
```  
  
RETCODE bcp_colptr (  
HDBC   
hdbc  
,  
LPCBYTE   
pData  
,  
INT   
idxServerCol  
);  
  
```  
  
## Arguments  
 *hdbc*  
 Is the bulk copy-enabled ODBC connection handle.  
  
 *pData*  
 Is a pointer to the data to copy. If the bound data type is large value type (such as SQLTEXT or SQLIMAGE), *pData* can be NULL. A NULL *pData* indicates long data values will be sent to SQL Server in chunks using [bcp_moretext](bcp-moretext.md).  
  
 If *pData* is set to NULL and the column corresponding to the bound field is not a large value type, **bcp_colptr** fails.  
  
 For more information on large value types, see [bcp_bind](bcp-bind.md)**.**  
  
 *idxServerCol*  
 Is the ordinal position of the column in the database table to which the data is copied. The first column in a table is column 1. The ordinal position of a column is reported by [SQLColumns](../native-client-odbc-api/sqlcolumns.md).  
  
## Returns  
 SUCCEED or FAIL.  
  
## Remarks  
 The **bcp_colptr** function allows you to change the address of source data for a particular column when copying data to SQL Server with [bcp_sendrow](bcp-sendrow.md).  
  
 Initially, the pointer to user data is set by a call to **bcp_bind**. If the program variable data address changes between calls to **bcp_sendrow**, you can call **bcp_colptr** to reset the pointer to the data. The next call to **bcp_sendrow** sends the data addressed by the call to **bcp_colptr**.  
  
 There must be a separate **bcp_colptr** call for every column in the table whose data address you want to modify.  
  
## See Also  
 [Bulk Copy Functions](sql-server-driver-extensions-bulk-copy-functions.md)  
  
  
