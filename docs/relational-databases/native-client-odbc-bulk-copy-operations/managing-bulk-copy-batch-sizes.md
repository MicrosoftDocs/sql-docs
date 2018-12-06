---
title: "Managing Bulk Copy Batch Sizes | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Native Client ODBC driver, bulk copy"
  - "ODBC, bulk copy operations"
  - "batches [ODBC]"
  - "bulk copy [ODBC], batch sizes"
ms.assetid: 4b24139f-788b-45a6-86dc-ae835435d737
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Managing Bulk Copy Batch Sizes
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  The primary purpose of a batch in bulk copy operations is to define the scope of a transaction. If a batch size is not set, then bulk copy functions consider an entire bulk copy to be one transaction. If a batch size is set, then each batch constitutes a transaction that is committed when the batch finishes.  
  
 If a bulk copy is performed with no batch size specified and an error is encountered, the entire bulk copy is rolled back. The recovery of a long-running bulk copy can take a long time. When a batch size is set, bulk copy considers each batch a transaction and commits each batch. If an error is encountered, only the last outstanding batch needs to be rolled back.  
  
 The batch size can also affect locking overhead. When performing a bulk copy against [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the TABLOCK hint can be specified using [bcp_control](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-control.md) to acquire a table lock instead of row locks. The single table lock can be held with minimal overhead for an entire bulk copy operation. If TABLOCK is not specified then locks are held on individual rows and the overhead of maintaining all the locks for the duration of the bulk copy can slow performance. Because locks are only held for the length of a transaction, specifying a batch size addresses this problem by periodically generating a commit that frees the locks currently held.  
  
 The number of rows making up a batch can have significant performance effects when bulk copying a large number of rows. The recommendations for batch size depend on the type of bulk copy being performed.  
  
-   When bulk copying to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], specify the TABLOCK bulk copy hint and set a large batch size.  
  
-   When TABLOCK is not specified, limit batch sizes to less than 1,000 rows.  
  
 When bulk copying in from a data file, the batch size is specified by calling **bcp_control** with the BCPBATCH option before calling [bcp_exec](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-exec.md). When bulk copying from program variables using [bcp_bind](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-bind.md) and [bcp_sendrow](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-sendrow.md), the batch size is controlled by calling [bcp_batch](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-batch.md) after calling [bcp_sendrow](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-sendrow.md) *x* times, where *x* is the number of rows in a batch.  
  
 In addition to specifying the size of a transaction, batches also affect when rows are sent across the network to the server. Bulk copy functions normally cache the rows from **bcp_sendrow** until a network packet is filled, and then send the full packet to the server. When an application calls **bcp_batch**, however, the current packet is sent to the server regardless of whether it has been filled. Using a very low batch size can slow performance if it results in sending many partially filled packets to the server. For example, calling **bcp_batch** after every **bcp_sendrow** causes each row to be sent in a separate packet and, unless the rows are very large, wastes space in each packet. The default size of network packets for SQL Server is 4 KB, although an application can change the size by calling [SQLSetConnectAttr](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md) specifying the SQL_ATTR_PACKET_SIZE attribute.  
  
 Another side effect of batches is that each batch is considered an outstanding result set until it is completed with **bcp_batch**. If any other operations are attempted on a connection handle while a batch is outstanding, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver issues an error with SQLState = "HY000" and an error message string of:  
  
```  
"[Microsoft][SQL Server Native Client] Connection is busy with  
results for another hstmt."  
```  
  
## See Also  
 [Performing Bulk Copy Operations &#40;ODBC&#41;](../../relational-databases/native-client-odbc-bulk-copy-operations/performing-bulk-copy-operations-odbc.md)   
 [Bulk Import and Export of Data &#40;SQL Server&#41;](../../relational-databases/import-export/bulk-import-and-export-of-data-sql-server.md)  
  
  
