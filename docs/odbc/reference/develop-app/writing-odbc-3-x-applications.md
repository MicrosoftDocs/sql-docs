---
description: "Writing ODBC 3.x Applications"
title: "Writing ODBC 3.x Applications | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "application upgrades [ODBC], about upgrading"
  - "ODBC drivers [ODBC], backward compatibility"
  - "upgrading applications [ODBC]"
  - "compatibility [ODBC], upgrading applications"
  - "application upgrades [ODBC]"
  - "upgrading applications [ODBC], about upgrading"
  - "backward compatibility [ODBC], upgrading applications"
ms.assetid: 19c54fc5-9dd6-49b6-8c9f-a38961b40a65
author: David-Engel
ms.author: v-davidengel
---
# Writing ODBC 3.x Applications
When an ODBC *2.x* application is upgraded to ODBC *3.x*, it should be written such that it works with both ODBC *2.x* and *3.x* drivers. The application should incorporate conditional code to take full advantage of the ODBC *3.x* features.  
  
 The SQL_ATTR_ODBC_VERSION environment attribute should be set to SQL_OV_ODBC2. This will ensure that the driver behaves like an ODBC *2.x* driver with respect to the changes described in the section [Behavioral Changes](../../../odbc/reference/develop-app/behavioral-changes.md).  
  
 If the application will use any of the features described in the section [New Features](../../../odbc/reference/develop-app/new-features.md), conditional code should be used to determine whether the driver is an ODBC *3.x* or ODBC *2.x* driver. The application uses **SQLGetDiagField** and **SQLGetDiagRec** to obtain ODBC *3.x* SQLSTATEs while doing error processing on these conditional code fragments. The following points about the new functionality should be considered:  
  
-   An application affected by the change in rowset size behavior should be careful not to call **SQLFetch** when the array size is greater than 1. These applications should replace calls to **SQLExtendedFetch** with calls to **SQLSetStmtAttr** to set the SQL_ATTR_ARRAY_STATUS_PTR statement attribute and to **SQLFetchScroll**, so that they have common code that works with both ODBC *3.x* and ODBC *2.x* drivers. Because **SQLSetStmtAttr** with SQL_ATTR_ROW_ARRAY_SIZE will be mapped to **SQLSetStmtAttr** with SQL_ROWSET_SIZE for ODBC *2.x* drivers, applications can just set SQL_ATTR_ROW_ARRAY_SIZE for their multirow fetch operations.  
  
-   Most applications that are upgrading are not actually affected by changes in SQLSTATE codes. For those applications that are affected, they can do a mechanical search and replace in most cases using the error conversion table in the "SQLSTATE Mapping" section to convert ODBC *3.x* error codes to ODBC *2.x* codes. Since the ODBC *3.x* Driver Manager will perform mapping from ODBC *2.x* SQLSTATEs to ODBC *3.x* SQLSTATEs, these application writers need only check for the ODBC *3.x* SQLSTATEs and not worry about including ODBC *2.x* SQLSTATEs in conditional code.  
  
-   If an application makes great use of date, time, and timestamp data types, the application can declare itself to be an ODBC *2.x* application and use its existing code instead of using conditioning code.  
  
 The upgrade should also include the following steps:  
  
-   Call **SQLSetEnvAttr** before allocating a connection to set the SQL_ATTR_ODBC_VERSION environment attribute to SQL_OV_ODBC2.  
  
-   Replace all calls to **SQLAllocEnv**, **SQLAllocConnect**, or **SQLAllocStmt** with calls to **SQLAllocHandle** with the appropriate *HandleType* argument of SQL_HANDLE_ENV, SQL_HANDLE_DBC, or SQL_HANDLE_STMT.  
  
-   Replace all calls to **SQLFreeEnv** or **SQLFreeConnect** with calls to **SQLFreeHandle** with the appropriate *HandleType* argument of SQL_HANDLE_DBC or SQL_HANDLE_STMT.  
  
-   Replace all calls to **SQLSetConnectOption** with calls to **SQLSetConnectAttr**. If setting an attribute whose value is a string, set the *StringLength* argument appropriately. Change *Attribute* argument from SQL_XXXX to SQL_ATTR_XXXX.  
  
-   Replace all calls to **SQLGetConnectOption** with calls to **SQLGetConnectAttr**. If getting a string or binary attribute, set *BufferLength* to the appropriate value and pass in a *StringLength* argument. Change *Attribute* argument from SQL_XXXX to SQL_ATTR_XXXX.  
  
-   Replace all calls to **SQLSetStmtOption** with calls to **SQLSetStmtAttr**. If setting an attribute whose value is a string, set the *StringLength* argument appropriately. Change *Attribute* argument from SQL_XXXX to SQL_ATTR_XXXX.  
  
-   Replace all calls to **SQLGetStmtOption** with calls to **SQLGetStmtAttr**. If getting a string or binary attribute, set *BufferLength* to the appropriate value and pass in a *StringLength* argument. Change *Attribute* argument from SQL_XXXX to SQL_ATTR_XXXX.  
  
-   Replace all calls to **SQLTransact** with calls to **SQLEndTran**. If the rightmost valid handle in the **SQLTransact** call is an environment handle, a *HandleType* argument of SQL_HANDLE_ENV should be used in the **SQLEndTran** call with the appropriate *Handle* argument. If the rightmost valid handle in your **SQLTransact** call is a connection handle, a *HandleType* argument of SQL_HANDLE_DBC should be used in the **SQLEndTran** call with the appropriate *Handle* argument.  
  
-   Replace all calls to **SQLColAttributes** with calls to **SQLColAttribute**. If the *FieldIdentifier* argument is either SQL_COLUMN_PRECISION, SQL_COLUMN_SCALE, or SQL_COLUMN_LENGTH, do not change anything other than the name of the function. If not, change *FieldIdentifier* from SQL_COLUMN_XXXX to SQL_DESC_XXXX. If *FieldIdentifier* is SQL_DESC_CONCISE_TYPE and the data type is a datetime data type, change to the corresponding ODBC *3.x* data type.  
  
-   If using block cursors, scrollable cursors, or both, the application does the following:  
  
    -   Sets the rowset size, cursor type, and cursor concurrency using **SQLSetStmtAttr**.  
  
    -   Calls **SQLSetStmtAttr** to set SQL_ATTR_ROW_STATUS_PTR to point to an array of status records.  
  
    -   Calls **SQLSetStmtAttr** to set SQL_ATTR_ROWS_FETCHED_PTR to point to an SQLINTEGER.  
  
    -   Performs the required bindings and executes the SQL statement.  
  
    -   Calls **SQLFetchScroll** in a loop to fetch rows and move around in the result set.  
  
    -   If it wants to fetch by bookmark, the application calls **SQLSetStmtAttr** to set SQL_ATTR_FETCH_BOOKMARK_PTR to a variable that will contain the bookmark for the row that it wants to fetch, and calls **SQLFetchScroll** with a *FetchOrientation* argument of SQL_FETCH_BOOKMARK.  
  
-   If using arrays of parameters, the application does the following:  
  
    -   Calls **SQLSetStmtAttr** to set the SQL_ATTR_PARAMSET_SIZE attribute to the size of the parameter array.  
  
    -   Calls **SQLSetStmtAttr** to set SQL_ATTR_ROWS_PROCESSED_PTR to point to an internal UDWORD variable.  
  
    -   Performs prepare, bind, and execute operations as appropriate.  
  
    -   If execution halts for some reason (such as SQL_NEED_DATA), it can find the "current" row of parameters by inspecting the location pointed to by SQL_ATTR_ROWS_PROCESSED_PTR.  
  
 This section contains the following topics.  
  
-   [Mapping Replacement Functions for Backward Compatibility of Applications](../../../odbc/reference/develop-app/mapping-replacement-functions-for-backward-compatibility-of-applications.md)  
  
-   [Calling SQLCloseCursor](../../../odbc/reference/develop-app/calling-sqlclosecursor.md)  
  
-   [Calling SQLGetDiagField](../../../odbc/reference/develop-app/calling-sqlgetdiagfield.md)  
  
-   [Calling SQLSetPos](../../../odbc/reference/develop-app/calling-sqlsetpos.md)  
  
-   [Cursor Library Operations](../../../odbc/reference/develop-app/cursor-library-operations.md)  
  
-   [Mapping the Cursor Attributes1 Information Types](../../../odbc/reference/develop-app/mapping-the-cursor-attributes1-information-types.md)  
  
-   [SQL_NO_DATA](../../../odbc/reference/develop-app/sql-no-data.md)
