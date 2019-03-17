---
title: "bcp_batch | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
api_name: 
  - "bcp_batch"
api_location: 
  - "sqlncli11.dll"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "bcp_batch function"
ms.assetid: 0bda489e-86bc-4a7e-80f6-96047e03f281
author: MightyPen
ms.author: genemi
manager: craigg
---
# bcp_batch
  Commits all rows previously bulk copied from program variables and sent to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by [bcp_sendrow](bcp-sendrow.md).  
  
## Syntax  
  
```  
  
DBINT bcp_batch (HDBC  
hdbc  
);  
  
```  
  
## Arguments  
 *hdbc*  
 Is the bulk copy-enabled ODBC connection handle.  
  
## Returns  
 The number of rows saved after the last call to **bcp_batch**, or -1 in case of error.  
  
## Remarks  
 Bulk copy batches define transactions. When an application uses [bcp_bind](bcp-bind.md) and **bcp_sendrow** to bulk copy rows from program variables to SQL Server tables, the rows are committed only when the program calls **bcp_batch** or [bcp_done](bcp-done.md).  
  
 You can call **bcp_batch** once every *n* rows or when there is a lull in incoming data (as in a telemetry application). If an application does not call **bcp_batch** the bulk copied rows are committed only when **bcp_done** is called.  
  
## See Also  
 [Bulk Copy Functions](sql-server-driver-extensions-bulk-copy-functions.md)  
  
  
