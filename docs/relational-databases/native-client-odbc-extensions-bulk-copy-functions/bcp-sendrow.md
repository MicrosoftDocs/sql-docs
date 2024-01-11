---
title: "bcp_sendrow"
description: "bcp_sendrow"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords:
  - "bcp_sendrow function"
apilocation: "sqlncli11.dll"
apiname: "bcp_sendrow"
apitype: "DLLExport"
---
# bcp_sendrow
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Sends a row of data from program variables to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Syntax  
  
```  
  
RETCODE bcp_sendrow (  
    HDBC hdbc);  
```  
  
## Arguments  
 *hdbc*  
 Is the bulk copy-enabled ODBC connection handle.  
  
## Returns  
 SUCCEED or FAIL.  
  
## Remarks  
 The **bcp_sendrow** function builds a row from program variables and sends it to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 Before calling **bcp_sendrow**, you must make calls to [bcp_bind](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-bind.md) to specify the program variables containing row data.  
  
 If **bcp_bind** is called specifying a long, variable-length data type, for example, an *eDataType* parameter of SQLTEXT and a non-NULL *pData* parameter, **bcp_sendrow** sends the entire data value, just as it does for any other data type. If, however, **bcp_bind** has a NULL *pData* parameter, **bcp_sendrow** returns control to the application immediately after all columns with data specified are sent to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The application can then call [bcp_moretext](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-moretext.md) repeatedly to send the long, variable-length data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], a chunk at a time. For more information, see [bcp_moretext](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-moretext.md).  
  
 When **bcp_sendrow** is used to bulk copy rows from program variables into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tables, rows are committed only when the user calls [bcp_batch](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-batch.md) or [bcp_done](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-done.md). The user can choose to call **bcp_batch** once every *n* rows or when there is a lull between periods of incoming data. If **bcp_batch** is never called, the rows are committed when **bcp_done** is called.  
  
 For information about a breaking change in bulk-copying beginning in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], see [Performing Bulk Copy Operations &#40;ODBC&#41;](../../relational-databases/native-client-odbc-bulk-copy-operations/performing-bulk-copy-operations-odbc.md).  
  
## See Also  
 [Bulk Copy Functions](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/sql-server-driver-extensions-bulk-copy-functions.md)  
  
  
