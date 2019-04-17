---
title: "bcp_exec | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
apiname: 
  - "bcp_exec"
apilocation: 
  - "sqlncli11.dll"
apitype: "DLLExport"
helpviewer_keywords: 
  - "bcp_exec function"
ms.assetid: b23ea2cc-8545-4873-b0c1-57e76b0a3a7b
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# bcp_exec
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  Executes a complete bulk copy of data between a database table and a user file.  
  
## Syntax  
  
```  
  
RETCODE bcp_exec (  
        HDBC hdbc,  
        LPDBINT pnRowsProcessed);  
```  
  
## Arguments  
 *hdbc*  
 Is the bulk copy-enabled ODBC connection handle.  
  
 *pnRowsProcessed*  
 Is a pointer to a DBINT. The **bcp_exec** function fills this DBINT with the number of rows successfully copied. If *pnRowsProcessed* is NULL, it is ignored by **bcp_exec**.  
  
## Returns  
 SUCCEED, SUCCEED_ASYNC, or FAIL. The **bcp_exec** function returns SUCCEED if all rows are copied. **bcp_exec** returns SUCCEED_ASYNC if an asynchronous bulk copy operation is still outstanding. **bcp_exec** returns FAIL if a complete failure occurs, or if the number of rows generating errors reaches the value specified for BCPMAXERRS using [bcp_control](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-control.md). BCPMAXERRS defaults to 10. The BCPMAXERRS option affects only the syntax errors detected by the provider while reading the rows from the data file (and not the rows sent to the server). Server aborts the batch when it detects an error with a row. Check the *pnRowsProcessed* parameter for the number of rows successfully copied.  
  
## Remarks  
 This function copies data from a user file to a database table or vice versa, depending on the value of the *eDirection* parameter in [bcp_init](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-init.md).  
  
 Before calling **bcp_exec**, call **bcp_init** with a valid user file name. Failure to do so results in an error.  
  
 **bcp_exec** is the only bulk copy function that is likely to be outstanding for any length of time. It is therefore the only bulk copy function that supports asynchronous mode. To set asynchronous mode, use [SQLSetConnectAttr](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md) to set SQL_ATTR_ASYNC_ENABLE to SQL_ASYNC_ENABLE_ON before calling **bcp_exec**. To test for completion, call **bcp_exec** with the same parameters. If the bulk copy has not yet completed, **bcp_exec** returns SUCCEED_ASYNC. It also returns in *pnRowsProcessed* a status count of the number of rows that have been sent to the server. Rows sent to the server are not committed until the end of a batch has been reached.  
  
 For information about a breaking change in bulk-copying beginning in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], see [Performing Bulk Copy Operations &#40;ODBC&#41;](../../relational-databases/native-client-odbc-bulk-copy-operations/performing-bulk-copy-operations-odbc.md).  
  
## Example  
 The following example shows how to use **bcp_exec**:  
  
```  
// Variables like henv not specified.  
HDBC      hdbc;  
DBINT      nRowsProcessed;  
  
// Application initiation, get an ODBC environment handle, allocate the  
// hdbc, and so on.  
...   
  
// Enable bulk copy prior to connecting on allocated hdbc.  
SQLSetConnectAttr(hdbc, SQL_COPT_SS_BCP, (SQLPOINTER) SQL_BCP_ON,  
   SQL_IS_INTEGER);  
  
// Connect to the data source, return on error.  
if (!SQL_SUCCEEDED(SQLConnect(hdbc, _T("myDSN"), SQL_NTS,  
   _T("myUser"), SQL_NTS, _T("myPwd"), SQL_NTS)))  
   {  
   // Raise error and return.  
   return;  
   }  
  
// Initialize bulk copy.   
if (bcp_init(hdbc, _T("pubs..authors"), _T("authors.sav"), NULL, DB_OUT)  
   == FAIL)  
   {  
   // Raise error and return.  
   return;  
   }  
  
// Now, execute the bulk copy.   
if (bcp_exec(hdbc, &nRowsProcessed) == FAIL)  
   {  
   if (nRowsProcessed == -1)  
      {  
      printf_s("No rows processed on bulk copy execution.\n");  
      }  
   else  
      {  
      printf_s("Incomplete bulk copy.   Only %ld row%s copied.\n",  
         nRowsProcessed, (nRowsProcessed == 1) ? "": "s");  
      }  
   return;  
   }  
  
printf_s("%ld rows processed.\n", nRowsProcessed);  
  
// Carry on.  
```  
  
## See Also  
 [Bulk Copy Functions](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/sql-server-driver-extensions-bulk-copy-functions.md)  
  
  
