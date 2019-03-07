---
title: "bcp_readfmt | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
api_name: 
  - "bcp_readfmt"
api_location: 
  - "sqlncli11.dll"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "bcp_readfmt function"
ms.assetid: 654001c8-ae9f-425c-b820-f0191bf89367
author: MightyPen
ms.author: genemi
manager: craigg
---
# bcp_readfmt
  Reads a data file format definition from the specified format file.  
  
## Syntax  
  
```  
  
RETCODE bcp_readfmt (  
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
 Is the path and file name of the file containing the format values for the data file.  
  
## Returns  
 SUCCEED or FAIL.  
  
## Remarks  
 After `bcp_readfmt` reads the format values, it makes the appropriate calls to [bcp_columns](bcp-columns.md) and [bcp_colfmt](bcp-colfmt.md). There is no need for you to parse a format file and make these calls.  
  
 To persist a format file, call [bcp_writefmt](bcp-writefmt.md). Calls to `bcp_readfmt` can reference saved formats. For more information, see [bcp_init](bcp-init.md).  
  
 Alternately, the bulk-copy utility (**bcp**) can save user-defined data formats in files that can be referenced by `bcp_readfmt`. For more information about the **bcp** utility and the structure of **bcp** data format files, see [Bulk Import and Export of Data &#40;SQL Server&#41;](../import-export/bulk-import-and-export-of-data-sql-server.md).  
  
 The `BCPDELAYREADFMT` value of the *eOption* parameter of [bcp_control](bcp-control.md) modifies the behavior of bcp_readfmt.  
  
> [!NOTE]  
>  The format file must have been produced by version 4.2 or later of the **bcp** utility.  
  
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
   _T("myErrors"),    DB_IN) == FAIL)  
   {  
   // Raise error and return.  
   return;  
   }  
  
if (bcp_readfmt(hdbc, _T("myFmtFile.fmt")) == FAIL)  
   {  
   // Raise error and return.  
   return;  
   }  
  
if (bcp_exec(hdbc, &nRowsProcessed) == SUCCEED)  
   {  
   cout << nRowsProcessed << " rows copied to SQL Server\n";  
   }  
  
// Carry on.  
```  
  
## See Also  
 [Bulk Copy Functions](sql-server-driver-extensions-bulk-copy-functions.md)  
  
  
