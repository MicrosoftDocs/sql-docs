---
description: "ODBC Driver Behavior Change When Handling Character Conversions"
title: "ODBC change handling char conversions"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
ms.assetid: 682a232a-bf89-4849-88a1-95b2fbac1467
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# ODBC Driver Behavior Change When Handling Character Conversions
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

> [!IMPORTANT] 
> [!INCLUDE[snac-removed-oledb-and-odbc](../../../includes/snac-removed-oledb-and-odbc.md)]

  The [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)] Native Client ODBC Driver (SQLNCLI11.dll) changed how it does of SQL_WCHAR* (NCHAR/NVARCHAR/NVARCHAR(MAX)) and SQL_CHAR\* (CHAR/VARCHAR/NARCHAR(MAX)) conversions. ODBC functions, such as SQLGetData, SQLBindCol, SQLBindParameter, return (-4) SQL_NO_TOTAL as the length/indicator parameter when using the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] 2012 Native Client ODBC driver. Prior versions of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver returned a length value, which can be incorrect.  
  
## SQLGetData Behavior  
 Many Windows functions let you specify a buffer size of 0 and the returned length is the size of the returned data. The following pattern is common for Windows programmers:  
  
```  
int iSize = 0;  
BYTE * pBuffer = NULL;  
GetMyFavoriteAPI(pBuffer, &iSize);   // Returns needed size in iSize  
pBuffer = new BYTE[iSize];   // Allocate buffer   
GetMyFavoriteAPI(pBuffer, &iSize);   // Retrieve actual data  
```  
  
 However, **SQLGetData** should not be used in this scenario. The following pattern should not be used:  
  
```  
// bad  
int iSize = 0;  
WCHAR * pBuffer = NULL;  
SQLGetData(hstmt, SQL_W_CHAR, ...., (SQLPOINTER*)0x1, 0, &iSize);   // Get storage size needed  
pBuffer = new WCHAR[(iSize/sizeof(WCHAR)) + 1];   // Allocate buffer  
SQLGetData(hstmt, SQL_W_CHAR, ...., (SQLPOINTER*)pBuffer, iSize, &iSize);   // Retrieve data  
```  
  
 **SQLGetData** can only be called to retrieve chunks of actual data. Using **SQLGetData** to get the size of data is not unsupported.  
  
 The following shows the impact of the driver change when you use the incorrect pattern. This application queries a **varchar** column and binding as Unicode (SQL_UNICODE/SQL_WCHAR):  
  
 Query:  `select convert(varchar(36), '123')`  
  
```  
SQLGetData(hstmt, SQL_WCHAR, ....., (SQLPOINTER*) 0x1, 0 , &iSize);   // Attempting to determine storage size needed  
```  
  
|[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC Driver version|Length or Indicator Outcome|Description|  
|-----------------------------------------------------------------|---------------------------------|-----------------|  
|[!INCLUDE[ssKilimanjaro](../../../includes/sskilimanjaro-md.md)] Native Client or earlier|6|The driver incorrectly assumed that converting CHAR to WCHAR could be accomplished as length * 2.|  
|[!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)] Native Client (version 11.0.2100.60) or later|-4 (SQL_NO_TOTAL)|The driver no longer assumes that converting from CHAR to WCHAR or WCHAR to CHAR is a (multiply) \*2 or (divide)/2 action.<br /><br /> Calling **SQLGetData** no longer returns the length of the expected conversion. The driver detects the conversion to or from CHAR and WCHAR and returns (-4) SQL_NO_TOTAL instead of *2 or /2 behavior that could be incorrect.|  
  
 Use **SQLGetData** to retrieve the chunks of the data. (Pseudo code shown:)  
  
```  
while( (SQL_SUCCESS or SQL_SUCCESS_WITH_INFO) == SQLFetch(...) ) {  
   SQLNumCols(...iTotalCols...)  
   for(int iCol = 1; iCol < iTotalCols; iCol++) {  
      WCHAR* pBufOrig, pBuffer = new WCHAR[100];  
      SQLGetData(.... iCol ... pBuffer, 100, &iSize);   // Get original chunk  
      while(NOT ALL DATA RETREIVED (SQL_NO_TOTAL, ...) ) {  
         pBuffer += 50;   // Advance buffer for data retrieved  
         // May need to realloc the buffer when you reach current size  
         SQLGetData(.... iCol ... pBuffer, 100, &iSize);   // Get next chunk  
      }  
   }  
}  
```  
  
## SQLBindCol Behavior  
 Query:  `select convert(varchar(36), '1234567890')`  
  
```  
SQLBindCol(... SQL_W_CHAR, ...)   // Only bound a buffer of WCHAR[4] - Expecting String Data Right Truncation behavior  
```  
  
|[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC Driver version|Length or Indicator Outcome|Description|  
|-----------------------------------------------------------------|---------------------------------|-----------------|  
|[!INCLUDE[ssKilimanjaro](../../../includes/sskilimanjaro-md.md)] Native Client or earlier|20|**SQLFetch** reports that there is a truncation on the right side of the data.<br /><br /> Length is the length of the data returned, not what was stored (assumes *2 CHAR to WCHAR conversion which can be incorrect for glyphs).<br /><br /> Data stored in buffer is 123\0. Buffer is guaranteed to be NULL terminated.|  
|[!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)] Native Client (version 11.0.2100.60) or later|-4 (SQL_NO_TOTAL)|**SQLFetch** reports that there is a truncation on the right side of the data.<br /><br /> Length indicates -4 (SQL_NO_TOTAL) because the rest of the data was not converted.<br /><br /> Data stored in the buffer is 123\0. - Buffer is guaranteed to be NULL terminated.|  
  
## SQLBindParameter (OUTPUT Parameter Behavior)  
 Query:  `create procedure spTest @p1 varchar(max) OUTPUT`  
  
 `select @p1 = replicate('B', 1234)`  
  
```  
SQLBindParameter(... SQL_W_CHAR, ...)   // Only bind up to first 64 characters  
```  
  
|[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC Driver version|Length or Indicator Outcome|Description|  
|-----------------------------------------------------------------|---------------------------------|-----------------|  
|[!INCLUDE[ssKilimanjaro](../../../includes/sskilimanjaro-md.md)] Native Client or earlier|2468|**SQLFetch** returns no more data available.<br /><br /> **SQLMoreResults** returns no more data available.<br /><br /> Length indicates the size of the data returned from server, not stored in buffer.<br /><br /> Original buffer contains 63 bytes and a NULL terminator. Buffer is guaranteed to be NULL terminated.|  
|[!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)] Native Client (version 11.0.2100.60) or later|-4 (SQL_NO_TOTAL)|**SQLFetch** returns no more data available.<br /><br /> **SQLMoreResults** returns no more data available.<br /><br /> Length indicates (-4) SQL_NO_TOTAL because the rest of the data was not converted.<br /><br /> Original buffer contains 63 bytes and a NULL terminator. Buffer is guaranteed to be NULL terminated.|  
  
## Performing CHAR and WCHAR Conversions  
 The [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)] Native Client ODBC driver offers several ways to perform CHAR and WCHAR conversions. The logic is similar to manipulating blobs (varchar(max), nvarchar(max), ...):  
  
-   Data is saved or truncated into the specified buffer when binding with **SQLBindCol** or **SQLBindParameter**.  
  
-   If you do not bind, you can retrieve the data in chunks by using **SQLGetData** and **SQLParamData**.  
  
## See Also  
 [SQL Server Native Client Features](../../../relational-databases/native-client/features/sql-server-native-client-features.md)  
  
  
