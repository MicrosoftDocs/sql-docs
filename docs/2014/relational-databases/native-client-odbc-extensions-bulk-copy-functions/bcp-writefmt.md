---
title: "bcp_writefmt | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
api_name: 
  - "bcp_writefmt"
api_location: 
  - "sqlncli11.dll"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "bcp_writefmt function"
ms.assetid: cb4c1d37-667d-4bcd-b13c-eb638bcc9b69
author: MightyPen
ms.author: genemi
manager: craigg
---
# bcp_writefmt
  Creates a format file containing a description of the format of the current bulk copy data file.  
  
## Syntax  
  
```  
  
RETCODE bcp_writefmt (  
HDBC   
hdbc  
,  
LPCTSTR   
szFormatFile  
);  
  
```  
  
## Arguments  
 *hdbc*  
 Is the bulk copy-enabled ODBC connection handle.  
  
 *szFormatFile*  
 Is the path and file name of the user file to receive format values for the data file.  
  
## Returns  
 SUCCEED or FAIL.  
  
## Remarks  
 The format file specifies the data format of a data file created by bulk copy. Calls to [bcp_columns](bcp-columns.md) and [bcp_colfmt](bcp-colfmt.md) define the format of the data file. **bcp_writefmt** saves this definition in the file referenced by *szFormatFile*. For more information, see [bcp_init](bcp-init.md).  
  
 For more information about the structure of **bcp** data format files, see [Import and Export Bulk Data by Using the bcp Utility &#40;SQL Server&#41;](../import-export/import-and-export-bulk-data-by-using-the-bcp-utility-sql-server.md).  
  
 To load a saved format file, use [bcp_readfmt](bcp-readfmt.md).  
  
> [!NOTE]  
>  The format file produced by **bcp_writefmt** is supported only by versions of the **bcp** utility distributed with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version 7.0 and later.  
  
## Example  
  
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
if (bcp_init(hdbc, _T("myTable"), _T("myData.csv"),  
   _T("myErrors"),    DB_OUT) == FAIL)  
   {  
   // Raise error and return.  
   return;  
   }  
  
if (bcp_columns(hdbc, 3) == FAIL)  
   {  
   // Raise error and return.  
   return;  
   }  
  
bcp_colfmt(hdbc, 1, SQLCHARACTER, 0, SQL_VARLEN_DATA, '\t', 1, 1);  
bcp_colfmt(hdbc, 2, SQLCHARACTER, 0, SQL_VARLEN_DATA, '\t', 1, 2);  
bcp_colfmt(hdbc, 3, SQLCHARACTER, 0, SQL_VARLEN_DATA, '\t', 1, 3);  
  
if (bcp_writefmt(hdbc, _T("myFmtFile.fmt")) == FAIL)  
   {  
   // Raise error and return.  
   return;  
   }  
  
if (bcp_exec(hdbc, &nRowsProcessed) == SUCCEED)  
   {  
   printf_s("%ld rows copied from SQL Server\n", nRowsProcessed);  
   }  
  
// Carry on.  
```  
  
## See Also  
 [Bulk Copy Functions](sql-server-driver-extensions-bulk-copy-functions.md)  
  
  
