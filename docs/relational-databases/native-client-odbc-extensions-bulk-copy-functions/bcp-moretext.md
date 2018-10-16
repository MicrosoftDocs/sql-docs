---
title: "bcp_moretext | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
apiname: 
  - "bcp_moretext"
apilocation: 
  - "sqlncli11.dll"
apitype: "DLLExport"
helpviewer_keywords: 
  - "bcp_moretext function"
ms.assetid: 23e98015-a8e4-4434-9b3f-9c7350cf965f
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# bcp_moretext
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  Sends part of a long, variable-length data type value to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Syntax  
  
```  
  
RETCODE bcp_moretext (  
        HDBC hdbc,  
        DBINT cbData,  
        LPCBYTE pData);  
```  
  
## Arguments  
 *hdbc*  
 Is the bulk copy-enabled ODBC connection handle.  
  
 *cbData*  
 Is the number of bytes of data being copied to SQL Server from the data referenced by *pData*. A value of SQL_NULL_DATA indicates NULL.  
  
 *pData*  
 Is a pointer to the supported, long, variable-length data chunk to be sent to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Returns  
 SUCCEED or FAIL.  
  
## Remarks  
 This function can be used in conjunction with [bcp_bind](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-bind.md) and [bcp_sendrow](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-sendrow.md) to copy long, variable-length data values to SQL Server in a number of smaller chunks. **bcp_moretext** can be used with columns that have the following SQL Server data types: **text**, **ntext**, **image**, **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, user-defined type (UDT), and XML. **bcp_moretext** does not support data conversions, the data supplied must match the data type of the target column.  
  
 If **bcp_bind** is called with a non-NULL *pData* parameter for data types that are supported by **bcp_moretext**, **bcp_sendrow** sends the entire data value, regardless of length. If, however, **bcp_bind** has a NULL *pData* parameter for supported data types, **bcp_moretext** can be used to copy data immediately after a successful return from **bcp_sendrow** indicating that any bound columns with data present have been processed.  
  
 If you use **bcp_moretext** to send one supported data type column in a row, you must also use it to send all other supported data type columns in the row. No columns may be skipped. Supported data types are SQLTEXT, SQLNTEXT, SQLIMAGE, SQLUDT and SQLXML. SQLCHARACTER, SQLVARCHAR, SQNCHAR, SQLBINARY and SQLVARBINARY also fall into this category if the column is a varchar(max), nvarchar(max) or varbinary(max), respectively.  
  
 Calling either **bcp_bind** or [bcp_collen](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-collen.md) sets the total length of all data parts to be copied to the SQL Server column. An attempt to send SQL Server more bytes than specified in the call to **bcp_bind** or **bcp_collen** generates an error. This error would arise, for example, in an application which used **bcp_collen** to set the length of available data for an SQL Server **text** column to 4500, then called **bcp_moretext** five times while indicating on each call that the data buffer length was 1000 bytes long.  
  
 If a copied row contains more than one long, variable-length column, **bcp_moretext** first sends its data to the lowest ordinally numbered column, followed by the next lowest ordinally numbered column, and so on. Correct setting of the total length of expected data is important. There is no way to signal, outside of the length setting, that all data for a column has been received by bulk copy.  
  
 When **var(max)** values are sent to the server using bcp_sendrow and bcp_moretext, it is not necessary to call bcp_collen to set the column length. Instead, for these types only, the value is terminated by calling bcp_sendrow with a length of zero.  
  
 An application normally calls **bcp_sendrow** and **bcp_moretext** within loops to send a number of rows of data. Here's an outline of how to do this for a table containing two **text** columns:  
  
```  
while (there are still rows to send)  
{  
bcp_sendrow(...);  
  
for (all the data in the first varbinary(max) column)  
bcp_moretext(...);  
bcp_moretext(hdbc, 0, NULL);  
  
for (all the data in the second varbinary(max) column)  
bcp_moretext(...);  
bcp_moretext(hdbc, 0, NULL);  
  
}  
  
```  
  
## Example  
 This example shows how to use **bcp_moretext** with **bcp_bind** and **bcp_sendrow**:  
  
```  
// Variables like henv not specified.  
HDBC      hdbc;  
DBINT      idRow = 5;  
char*      pPart1 = "This text value isn't very long,";  
char*      pPart2 = " but it's broken into three parts";  
char*      pPart3 = " anyhow.";  
DBINT      cbAllParts;  
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
if (bcp_init(hdbc, "comdb..articles", NULL, NULL, DB_IN) == FAIL)  
   {  
   // Raise error and return.  
   return;  
   }  
  
// Bind program variables to table columns.   
if (bcp_bind(hdbc, (LPCBYTE) &idRow, 0, SQL_VARLEN_DATA, NULL, 0,  
   SQLINT4, 1)    == FAIL)  
   {  
   // Raise error and return.  
   return;  
   }  
  
cbAllParts = (DBINT) (strnlen(pPart1, sizeof(pPart1) + 1) + strnlen(pPart2, sizeof(pPart2) + 1) + strnlen(pPart3, sizeof(pPart3) + 1));  
if (bcp_bind(hdbc, NULL, 0, cbAllParts, NULL, 0, SQLTEXT, 2) == FAIL)  
   {  
   // Raise error and return.  
   return;  
   }  
  
// Send this row, with the text value broken into three chunks.   
if (bcp_sendrow(hdbc) == FAIL)  
   {  
   // Raise error and return.  
   return;  
   }  
  
if (bcp_moretext(hdbc, (DBINT) strnlen(pPart1, sizeof(pPart1) + 1), pPart1) == FAIL)  
   {  
   // Raise error and return.  
   return;  
   }  
if (bcp_moretext(hdbc, (DBINT) strnlen(pPart2, sizeof(pPart2) + 1), pPart2) == FAIL)  
   {  
   // Raise error and return.  
   return;  
   }  
if (bcp_moretext(hdbc, (DBINT) strnlen(pPart3, sizeof(pPart3) + 1), pPart3) == FAIL)  
   {  
   // Raise error and return.  
   return;  
   }  
  
// All done. Get the number of rows processed (should be one).  
nRowsProcessed = bcp_done(hdbc);  
  
// Carry on.  
```  
  
## See Also  
 [Bulk Copy Functions](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/sql-server-driver-extensions-bulk-copy-functions.md)  
  
  
