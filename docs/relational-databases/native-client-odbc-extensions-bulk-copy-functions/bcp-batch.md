---
title: "bcp_batch"
description: "bcp_batch"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords:
  - "bcp_batch function"
apilocation: "sqlncli11.dll"
apiname: "bcp_batch"
apitype: "DLLExport"
---
# bcp_batch
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Commits all rows previously bulk copied from program variables and sent to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by [bcp_sendrow](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-sendrow.md).  
  
## Syntax  
  
```  
  
DBINT bcp_batch (HDBC  
        hdbc);  
```  
  
## Arguments  
 *hdbc*  
 Is the bulk copy-enabled ODBC connection handle.  
  
## Returns  
 The number of rows saved after the last call to **bcp_batch**, or -1 in case of error.  
  
## Remarks  
 Bulk copy batches define transactions. When an application uses [bcp_bind](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-bind.md) and **bcp_sendrow** to bulk copy rows from program variables to SQL Server tables, the rows are committed only when the program calls **bcp_batch** or [bcp_done](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-done.md).  
  
 You can call **bcp_batch** once every *n* rows or when there is a lull in incoming data (as in a telemetry application). If an application does not call **bcp_batch** the bulk copied rows are committed only when **bcp_done** is called.  
  
## See Also  
 [Bulk Copy Functions](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/sql-server-driver-extensions-bulk-copy-functions.md)  
  
  
