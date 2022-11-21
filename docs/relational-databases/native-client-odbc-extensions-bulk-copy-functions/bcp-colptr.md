---
description: "bcp_colptr"
title: "bcp_colptr | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
apiname: 
  - "bcp_colptr"
apilocation: 
  - "sqlncli11.dll"
apitype: "DLLExport"
helpviewer_keywords: 
  - "bcp_colptr function"
ms.assetid: 02ece13e-1da3-4f9d-b860-3177e43d2471
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# bcp_colptr
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Sets the program variable data address for the current copy into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Syntax  
  
```  
  
RETCODE bcp_colptr (  
        HDBC hdbc,  
        LPCBYTE pData,  
        INT idxServerCol);  
```  
  
## Arguments  
 *hdbc*  
 Is the bulk copy-enabled ODBC connection handle.  
  
 *pData*  
 Is a pointer to the data to copy. If the bound data type is large value type (such as SQLTEXT or SQLIMAGE), *pData* can be NULL. A NULL *pData* indicates long data values will be sent to SQL Server in chunks using [bcp_moretext](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-moretext.md).  
  
 If *pData* is set to NULL and the column corresponding to the bound field is not a large value type, **bcp_colptr** fails.  
  
 For more information on large value types, see [bcp_bind](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-bind.md)**.**  
  
 *idxServerCol*  
 Is the ordinal position of the column in the database table to which the data is copied. The first column in a table is column 1. The ordinal position of a column is reported by [SQLColumns](../../relational-databases/native-client-odbc-api/sqlcolumns.md).  
  
## Returns  
 SUCCEED or FAIL.  
  
## Remarks  
 The **bcp_colptr** function allows you to change the address of source data for a particular column when copying data to SQL Server with [bcp_sendrow](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-sendrow.md).  
  
 Initially, the pointer to user data is set by a call to **bcp_bind**. If the program variable data address changes between calls to **bcp_sendrow**, you can call **bcp_colptr** to reset the pointer to the data. The next call to **bcp_sendrow** sends the data addressed by the call to **bcp_colptr**.  
  
 There must be a separate **bcp_colptr** call for every column in the table whose data address you want to modify.  
  
## See Also  
 [Bulk Copy Functions](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/sql-server-driver-extensions-bulk-copy-functions.md)  
  
  
