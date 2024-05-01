---
title: "bcp_columns"
description: "bcp_columns"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords:
  - "bcp_columns function"
apilocation: "sqlncli11.dll"
apiname: "bcp_columns"
apitype: "DLLExport"
---
# bcp_columns
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Sets the total number of columns found in the user file for use with a bulk copy into or out of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. [bcp_setbulkmode](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-setbulkmode.md) can be used instead of bcp_columns and [bcp_colfmt](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-colfmt.md).  
  
## Syntax  
  
```  
  
RETCODE bcp_columns (  
        HDBC hdbc,  
        INT nColumns);  
```  
  
## Arguments  
 *hdbc*  
 Is the bulk copy-enabled ODBC connection handle.  
  
 *nColumns*  
 Is the total number of columns in the user file. Even if you are preparing to bulk copy data from the user file to an [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table and do not intend to copy all columns in the user file, you must still set *nColumns* to the total number of user-file columns.  
  
## Returns  
 SUCCEED or FAIL.  
  
## Remarks  
 This function can be called only after [bcp_init](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-init.md) has been called with a valid file name.  
  
 You should call this function only if you intend to use a user-file format that differs from the default. For more information about a description of the default user-file format, see **bcp_init**.  
  
 After calling **bcp_columns**, you must call [bcp_colfmt](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-colfmt.md) for each column in the user file to completely define a custom file format.  
  
## See Also  
 [Bulk Copy Functions](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/sql-server-driver-extensions-bulk-copy-functions.md)  
  
  
