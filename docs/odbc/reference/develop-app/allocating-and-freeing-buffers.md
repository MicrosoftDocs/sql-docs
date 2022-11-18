---
description: "Allocating and Freeing Buffers"
title: "Allocating and Freeing Buffers | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "buffers [ODBC], allocating and freeing"
  - "allocating buffers [ODBC]"
  - "freeing buffers [ODBC]"
ms.assetid: 886bc9ed-39d4-43d2-82ff-aebc35b14d39
author: David-Engel
ms.author: v-davidengel
---
# Allocating and Freeing Buffers
All buffers are allocated and freed by the application. If a buffer is not deferred, it need only exist for the duration of the call to a function. For example, **SQLGetInfo** returns the value associated with a particular option in the buffer pointed to by the *InfoValuePtr* argument. This buffer can be freed immediately after the call to **SQLGetInfo**, as shown in the following code example:  
  
```  
SQLSMALLINT   InfoValueLen;  
SQLCHAR *     InfoValuePtr = malloc(50);   // Allocate InfoValuePtr.  
  
SQLGetInfo(hdbc, SQL_DBMS_NAME, (SQLPOINTER)InfoValuePtr, 50,  
            &InfoValueLen);  
  
free(InfoValuePtr);                        // OK to free InfoValuePtr.  
```  
  
 Because deferred buffers are specified in one function and used in another, it is an application programming error to free a deferred buffer while the driver still expects it to exist. For example, the address of the \**ValuePtr* buffer is passed to **SQLBindCol** for later use by **SQLFetch**. This buffer cannot be freed until the column is unbound, such as with a call to **SQLBindCol** or **SQLFreeStmt** as shown in the following code example:  
  
```  
SQLRETURN    rc;  
SQLINTEGER   ValueLenOrInd;  
SQLHSTMT     hstmt;  
  
// Allocate ValuePtr  
SQLCHAR * ValuePtr = malloc(50);  
  
// Bind ValuePtr to column 1. It is an error to free ValuePtr here.  
SQLBindCol(hstmt, 1, SQL_C_CHAR, ValuePtr, 50, &ValueLenOrInd);  
  
// Fetch each row of data and place the value for column 1 in *ValuePtr.  
// Code to check if rc equals SQL_ERROR or SQL_SUCCESS_WITH_INFO   
// not shown.  
while ((rc = SQLFetch(hstmt)) != SQL_NO_DATA) {  
   // It is an error to free ValuePtr here.  
}  
  
// Unbind ValuePtr from column 1.  It is now OK to free ValuePtr.  
SQLFreeStmt(hstmt, SQL_UNBIND);  
free(ValuePtr);  
```  
  
 Such an error is easily made by declaring the buffer locally in a function; the buffer is freed when the application leaves the function. For example, the following code causes undefined and probably fatal behavior in the driver:  
  
```  
SQLRETURN   rc;  
SQLHSTMT    hstmt;  
  
BindAColumn(hstmt);  
  
// Fetch each row of data and try to place the value for column 1 in  
// *ValuePtr. Because ValuePtr has been freed, the behavior is undefined  
// and probably fatal. Code to check if rc equals SQL_ERROR or   
// SQL_SUCCESS_WITH_INFO not shown.  
while ((rc = SQLFetch(hstmt)) != SQL_NO_DATA) {}  
  
   .  
   .  
   .  
  
void BindAColumn(SQLHSTMT hstmt)  // WARNING! This function won't work!  
{  
   // Declare ValuePtr locally.  
   SQLCHAR      ValuePtr[50];  
   SQLINTEGER   ValueLenOrInd;  
  
   // Bind rgbValue to column.  
   SQLBindCol(hstmt, 1, SQL_C_CHAR, ValuePtr, sizeof(ValuePtr),  
               &ValueLenOrInd);  
  
   // ValuePtr is freed when BindAColumn exits.  
}  
```
