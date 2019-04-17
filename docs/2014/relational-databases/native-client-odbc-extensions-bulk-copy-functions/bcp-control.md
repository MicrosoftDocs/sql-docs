---
title: "bcp_control | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
api_name: 
  - "bcp_control"
api_location: 
  - "sqlncli11.dll"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "bcp_control function"
ms.assetid: 32187282-1385-4c52-9134-09f061eb44f5
author: MightyPen
ms.author: genemi
manager: craigg
---
# bcp_control
  Changes the default settings for various control parameters for a bulk copy between a file and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Syntax  
  
```  
  
RETCODE bcp_control (  
HDBC   
hdbc  
,  
INT   
eOption  
,  
void*   
iValue  
);  
  
```  
  
## Arguments  
 *hdbc*  
 Is the bulk copy-enabled ODBC connection handle.  
  
 *eOption*  
 Is one of the following:  
  
 BCPABORT  
 Stops a bulk-copy operation that is already in progress. Call **bcp_control** with an *eOption* of BCPABORT from another thread to stop a running bulk copy operation. The *iValue* parameter is ignored.  
  
 BCPBATCH  
 Is the number of rows per batch. The default is 0, which indicates either all rows in a table, when data is being extracted, or all rows in the user data file, when data is being copied to an [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. A value less than 1 resets BCPBATCH to the default.  
  
 BCPDELAYREADFMT  
 A Boolean, if set to true, will cause [bcp_readfmt](bcp-readfmt.md) to read at execution. If false (the default), bcp_readfmt will immediately read the format file. A sequence error will occur if BCPDELAYREADFMT is true and you call bcp_columns or bcp_setcolfmt.  
  
 A sequence error will also occur if you call `bcp_control(hdbc,` BCPDELAYREADFMT`, (void *)FALSE)` after calling `bcp_control(hdbc,` BCPDELAYREADFMT`, (void *)TRUE)` and bcp_writefmt.  
  
 For more information, see [Metadata Discovery](../native-client/features/metadata-discovery.md).  
  
 BCPFILECP  
 *iValue* contains the number of the code page for the data file. You can specify the number of the code page, such as 1252 or 850, or one of these values:  
  
 BCPFILE_ACP: data in the file is in the Microsoft Windows?? code page of the client.  
  
 BCPFILE_OEMCP: data in the file is in the OEM code page of the client (default).  
  
 BCPFILE_RAW: data in the file is in the code page of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 BCPFILEFMT  
 The version number of the data file format. This can be 80 ([!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)]), 90 ([!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]), 100 ([!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] or [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]), 110 ([!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]), or 120 ([!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]). 120 is the default. This is useful for exporting and importing data in formats that were supported by earlier version of the server. For example, to import data that was obtained from a text column in a [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] server into a **varchar(max)** column in a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] or later server, you should specify 80. Similarly, if you specify 80 when exporting data from a **varchar(max)** column, it will be saved just like text columns are saved in the [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] format, and can be imported into a text column of a [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] server.  
  
 BCPFIRST  
 Is the first row of data to file or table to copy. The default is 1; a value less than 1 resets this option to its default.  
  
 BCPFIRSTEX  
 For BCP out operations, specifies the first row of the database table to copy into the data file.  
  
 For BCP in operations, specifies the first row of the data file to copy into the database table.  
  
 The *iValue* parameter is expected to be the address of a signed 64-bit integer containing the value. The maximum value that can be passed to BCPFIRSTEX is 2^63-1.  
  
 BCPFMTXML  
 Specifies that the format file generated should be in XML format. It is off by default.  
  
 XML format files provide greater flexibility but with some added constraints. For example, you can not specify the prefix and terminator for a field simultaneously, which was possible in older format files.  
  
> [!NOTE]  
>  XML format files are only supported when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed along with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client.  
  
 BCPHINTS  
 *iValue* contains an SQLTCHAR character string pointer. The string addressed specifies either [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] bulk-copy processing hints or a Transact-SQL statement that returns a result set. If a Transact-SQL statement is specified that returns more than one result set, all result sets after the first are ignored. For more information about bulk-copy processing hints, see [bcp Utility](../../tools/bcp-utility.md).  
  
 BCPKEEPIDENTITY  
 When *iValue* is TRUE, specifies that bulk copy functions insert data values supplied for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] columns defined with an identity constraint. The input file must supply values for the identity columns. If this is not set, new identity values are generated for the inserted rows. Any data present in the file for the identity columns is ignored.  
  
 BCPKEEPNULLS  
 Specifies whether empty data values in the file will be converted to NULL values in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table. When *iValue* is TRUE, empty values will be converted to NULL in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table. The default is for empty values to be converted to a default value for the column in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table if a default exists.  
  
 BCPLAST  
 Is the last row to copy. The default is to copy all rows; a value less than 1 resets this option to its default.  
  
 BCPLASTEX  
 For BCP out operations, specifies the last row of the database table to copy into the data file.  
  
 For BCP in operations, specifies the last row of the data file to copy into the database table.  
  
 The *iValue* parameter is expected to be the address of a signed 64-bit integer containing the value. The maximum value that can be passed to BCPLASTEX is 2^63-1.  
  
 BCPMAXERRS  
 Is the number of errors allowed before the bulk copy operation fails. The default is 10; a value less than 1 resets this option to its default. Bulk copy imposes a maximum of 65,535 errors. An attempt to set this option to a value larger than 65,535 results in the option being set to 65,535.  
  
 BCPODBC  
 When TRUE, specifies that **datetime** and **smalldatetime** values saved in character format will use the ODBC timestamp escape sequence prefix and suffix. The BCPODBC option only applies to BCP_OUT.  
  
 When FALSE, a **datetime** value representing January 1, 1997 is converted to the character string: 1997-01-01 00:00:00.000. When TRUE, the same **datetime** value is represented as: {ts '1997-01-01 00:00:00.000'}.  
  
 BCPROWCOUNT  
 Returns the number of rows affected by the current (or last) BCP operation.  
  
 BCPTEXTFILE  
 When TRUE, specifies that the data file is a text file, rather than a binary file. If the file is a text file, BCP determines whether or not it is Unicode by checking the Unicode byte marker in the first two bytes of the data file.  
  
 BCPUNICODEFILE  
 When TRUE, specifies the input file is a Unicode file.  
  
 *iValue*  
 Is the value for the specified *eOption*. *iValue* is an integer (LONGLONG) value cast to a void pointer to allow for future expansion to 64 bit values.  
  
## Returns  
 SUCCEED or FAIL.  
  
## Remarks  
 This function sets various control parameters for bulk-copy operations, including the number of errors allowed before canceling a bulk copy, the numbers of the first and last rows to copy from a data file, and the batch size.  
  
 This function is also used to specify the SELECT statement when bulk copying out from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] the result set of a SELECT. Set *eOption* to BCPHINTS and set *iValue* to have a pointer to an SQLTCHAR string containing the SELECT statement.  
  
 These control parameters are only meaningful when copying between a user file and an [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table. Control parameter settings have no effect on rows copied to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] with [bcp_sendrow](bcp-sendrow.md).  
  
## Example  
  
```  
// Variables like henv not specified.  
SQLHDBC      hdbc;  
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
if (bcp_init(hdbc, _T("address"), _T("address.add"), _T("addr.err"),  
   DB_IN) == FAIL)  
   {  
   // Raise error and return.  
   return;  
   }  
  
// Set the number of rows per batch.   
if (bcp_control(hdbc, BCPBATCH, (void*) 1000) == FAIL)  
   {  
   // Raise error and return.  
   return;  
   }  
  
// Set file column count.   
if (bcp_columns(hdbc, 1) == FAIL)  
   {  
   // Raise error and return.  
   return;  
   }  
  
// Set the file format.   
if (bcp_colfmt(hdbc, 1, 0, 0, SQL_VARLEN_DATA, '\n', 1, 1)  
   == FAIL)  
   {  
   // Raise error and return.  
   return;  
   }  
  
// Execute the bulk copy.   
if (bcp_exec(hdbc, &nRowsProcessed) == FAIL)  
   {  
   // Raise error and return.  
   return;  
   }  
  
printf_s("%ld rows processed by bulk copy.", nRowsProcessed);  
  
```  
  
## See Also  
 [Bulk Copy Functions](sql-server-driver-extensions-bulk-copy-functions.md)  
  
  
