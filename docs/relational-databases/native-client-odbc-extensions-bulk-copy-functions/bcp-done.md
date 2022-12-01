---
description: "bcp_done"
title: "bcp_done | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
apiname: 
  - "bcp_done"
apilocation: 
  - "sqlncli11.dll"
apitype: "DLLExport"
helpviewer_keywords: 
  - "bcp_done function"
ms.assetid: e59b3f16-5b59-40da-880f-f3edf657d1ee
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# bcp_done
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Ends a bulk copy from program variables to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] performed with [bcp_sendrow](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-sendrow.md).  
  
## Syntax  
  
```  
  
DBINT bcp_done (  
    HDBC hdbc);  
```  
  
## Arguments  
 *hdbc*  
 Is the bulk copy-enabled ODBC connection handle.  
  
## Returns  
 The number of rows permanently saved after the last call to [bcp_batch](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-batch.md) or -1 in case of error.  
  
## Remarks  
 Call **bcp_done** after the last call to [bcp_sendrow](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-sendrow.md) or [bcp_moretext](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-moretext.md). Failure to call **bcp_done** after copying all data results in errors.  
  
## See Also  
 [Bulk Copy Functions](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/sql-server-driver-extensions-bulk-copy-functions.md)  
  
  
