---
title: "bcp_columns | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
api_name: 
  - "bcp_columns"
api_location: 
  - "sqlncli11.dll"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "bcp_columns function"
ms.assetid: 5376f6fe-9508-439a-8c66-778d77f19ac3
author: MightyPen
ms.author: genemi
manager: craigg
---
# bcp_columns
  Sets the total number of columns found in the user file for use with a bulk copy into or out of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. [bcp_setbulkmode](bcp-setbulkmode.md) can be used instead of bcp_columns and [bcp_colfmt](bcp-colfmt.md).  
  
## Syntax  
  
```  
  
RETCODE bcp_columns (  
HDBC   
hdbc  
,  
INT   
nColumns  
);  
  
```  
  
## Arguments  
 *hdbc*  
 Is the bulk copy-enabled ODBC connection handle.  
  
 *nColumns*  
 Is the total number of columns in the user file. Even if you are preparing to bulk copy data from the user file to an [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table and do not intend to copy all columns in the user file, you must still set *nColumns* to the total number of user-file columns.  
  
## Returns  
 SUCCEED or FAIL.  
  
## Remarks  
 This function can be called only after [bcp_init](bcp-init.md) has been called with a valid file name.  
  
 You should call this function only if you intend to use a user-file format that differs from the default. For more information about a description of the default user-file format, see **bcp_init**.  
  
 After calling `bcp_columns`, you must call [bcp_colfmt](bcp-colfmt.md)for each column in the user file to completely define a custom file format.  
  
## See Also  
 [Bulk Copy Functions](sql-server-driver-extensions-bulk-copy-functions.md)  
  
  
