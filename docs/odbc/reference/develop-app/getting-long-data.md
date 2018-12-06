---
title: "Getting Long Data | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "long data [ODBC]"
  - "fetches [ODBC], long data"
  - "result sets [ODBC], fetching"
  - "SQLGetData function [ODBC], getting long data"
  - "retrieving long data [ODBC]"
ms.assetid: 6ccb44bc-8695-4bad-91af-363ef22bdb85
author: MightyPen
ms.author: genemi
manager: craigg
---
# Getting Long Data
DBMSs define *long data* as any character or binary data over a certain size, such as 255 characters. This data may be small enough to be stored in a single buffer, such as a part description of several thousand characters. However, it might be too long to store in memory, such as long text documents or bitmaps. Because such data cannot be stored in a single buffer, it is retrieved from the driver in parts with **SQLGetData** after the other data in the row has been fetched.  
  
> [!NOTE]  
>  An application can actually retrieve any type of data with **SQLGetData**, not just long data, although only character and binary data can be retrieved in parts. However, if the data is small enough to fit in a single buffer, there is generally no reason to use **SQLGetData**. It is much easier to bind a buffer to the column and let the driver return the data in the buffer.  
  
 To retrieve long data from a column, an application first calls **SQLFetchScroll** or **SQLFetch** to move to a row and fetch the data for bound columns. The application then calls **SQLGetData**. **SQLGetData** has the same arguments as **SQLBindCol**: a statement handle; a column number; the C data type, address, and byte length of an application variable; and the address of a length/indicator buffer. Both functions have the same arguments because they perform essentially the same task: They both describe an application variable to the driver and specify that the data for a particular column should be returned in that variable. The major differences are that **SQLGetData** is called after a row is fetched (and is sometimes referred to as *late binding* for this reason) and that the binding specified by **SQLGetData** lasts only for the duration of the call.  
  
 Regarding a single column, **SQLGetData** behaves like **SQLFetch**: It retrieves the data for the column, converts it to the type of the application variable, and returns it in that variable. It also returns the byte length of the data in the length/indicator buffer. For more information about how **SQLFetch** returns data, see [Fetching a Row of Data](../../../odbc/reference/develop-app/fetching-a-row-of-data.md).  
  
 **SQLGetData** differs from **SQLFetch** in one important respect. If it is called more than once in succession for the same column, each call returns a successive part of the data. Each call except the last call returns SQL_SUCCESS_WITH_INFO and SQLSTATE 01004 (String data, right truncated); the last call returns SQL_SUCCESS. This is how **SQLGetData** is used to retrieve long data in parts. When there is no more data to return, **SQLGetData** returns SQL_NO_DATA. The application is responsible for putting the long data together, which might mean concatenating the parts of the data. Each part is null-terminated; the application must remove the null-termination character if concatenating the parts. Retrieving data in parts can be done for variable-length bookmarks as well as for other long data. The value returned in the length/indicator buffer decreases in each call by the number of bytes returned in the previous call, although it is common for the driver to be unable to discover the amount of available data and return a byte length of SQL_NO_TOTAL. For example:  
  
```  
// Declare a binary buffer to retrieve 5000 bytes of data at a time.  
SQLCHAR       BinaryPtr[5000];  
SQLUINTEGER   PartID;  
SQLINTEGER    PartIDInd, BinaryLenOrInd, NumBytes;  
SQLRETURN     rc;   
SQLHSTMT      hstmt;  
  
// Create a result set containing the ID and picture of each part.  
SQLExecDirect(hstmt, "SELECT PartID, Picture FROM Pictures", SQL_NTS);  
  
// Bind PartID to the PartID column.  
SQLBindCol(hstmt, 1, SQL_C_ULONG, &PartID, 0, &PartIDInd);  
  
// Retrieve and display each row of data.  
while ((rc = SQLFetch(hstmt)) != SQL_NO_DATA) {  
   // Display the part ID and initialize the picture.  
   DisplayID(PartID, PartIDInd);  
   InitPicture();  
  
   // Retrieve the picture data in parts. Send each part and the number   
   // of bytes in each part to a function that displays it. The number   
   // of bytes is always 5000 if there were more than 5000 bytes   
   // available to return (cbBinaryBuffer > 5000). Code to check if   
   // rc equals SQL_ERROR or SQL_SUCCESS_WITH_INFO not shown.  
   while ((rc = SQLGetData(hstmt, 2, SQL_C_BINARY, BinaryPtr, sizeof(BinaryPtr),  
                           &BinaryLenOrInd)) != SQL_NO_DATA) {  
      NumBytes = (BinaryLenOrInd > 5000) || (BinaryLenOrInd == SQL_NO_TOTAL) ?  
                  5000 : BinaryLenOrInd;  
      DisplayNextPictPart(BinaryPtr, NumBytes);  
   }  
}  
  
// Close the cursor.  
SQLCloseCursor(hstmt);  
```  
  
 There are several restrictions on using **SQLGetData**. Generally, columns accessed with **SQLGetData**:  
  
-   Must be accessed in order of increasing column number (because of the way the columns of a result set are read from the data source). For example, it is an error to call **SQLGetData** for column 5 and then call it for column 4.  
  
-   Cannot be bound.  
  
-   Must have a higher column number than the last bound column. For example, if the last bound column is column 3, it is an error to call **SQLGetData** for column 2. For this reason, applications should make sure to place long data columns at the end of the select list.  
  
-   Cannot be used if **SQLFetch** or **SQLFetchScroll** was called to retrieve more than one row. For more information, see [Using Block Cursors](../../../odbc/reference/develop-app/using-block-cursors.md).  
  
 Some drivers do not enforce these restrictions. Interoperable applications should either assume they exist or determine which restrictions are not enforced by calling **SQLGetInfo** with the SQL_GETDATA_EXTENSIONS option.  
  
 If the application does not need all the data in a character or binary data column, it can reduce network traffic in DBMS-based drivers by setting the SQL_ATTR_MAX_LENGTH statement attribute before executing the statement. This restricts the number of bytes of data that will be returned for any character or binary column. For example, suppose a column contains long text documents. An application that browses the table containing this column might have to display only the first page of each document. Although this statement attribute can be simulated in the driver, there is no reason to do this. In particular, if an application wants to truncate character or binary data, it should bind a small buffer to the column with **SQLBindCol** and let the driver truncate the data.
