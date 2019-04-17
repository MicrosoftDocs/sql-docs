---
title: "What the Driver Does | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "scrollable cursors [ODBC]"
  - "cursors [ODBC], backward compatibility"
  - "compatibility [ODBC], cursors"
  - "backward compatibility [ODBC], cursors"
  - "block cursors [ODBC]"
ms.assetid: 75dcdea6-ff6b-4ac8-aa11-a1f9edbeb8e6
author: MightyPen
ms.author: genemi
manager: craigg
---
# What the Driver Does
The following table summarizes what functions and statement attributes an ODBC 3*.x* driver should implement for block and scrollable cursors.  
  
|Function or<br /><br /> statement attribute|Comments|  
|-----------------------------------------|--------------|  
|SQL_ATTR_ROW_STATUS_PTR|Sets the address of the row status array filled by **SQLFetch** and **SQLFetchScroll**. This array is also filled by **SQLSetPos** if **SQLSetPos** is called in statement state S6. If **SQLSetPos** is called in state S7, this array is not filled but the array pointed to by the *RowStatusArray* argument of **SQLExtendedFetch** is filled. For more information, see [Statement Transitions](../../../odbc/reference/appendixes/statement-transitions.md) in Appendix B: ODBC State Transition Tables.|  
|SQL_ATTR_ROWS_FETCHED_PTR|Sets the address of the buffer in which **SQLFetch** and **SQLFetchScroll** return the number of rows fetched. If **SQLExtendedFetch** is called, this buffer is not filled but the *RowCountPtr* argument points to the number of rows fetched.|  
|SQL_ATTR_ROW_ARRAY_SIZE|Sets the rowset size used by **SQLFetch** and **SQLFetchScroll**.|  
|SQL_ROWSET_SIZE|Sets the rowset size used by **SQLExtendedFetch**. ODBC 3*.x* drivers implement this if they want to work with ODBC 2.*x* applications that call **SQLExtendedFetch** or **SQLSetPos**.|  
|**SQLBulkOperations**|If an ODBC 3*.x* driver should work with ODBC 2.*x* applications that use **SQLSetPos** with an *Operation* of SQL_ADD, the driver must support **SQLSetPos** with an *Operation* of SQL_ADD in addition to **SQLBulkOperations** with an *Operation* of SQL_ADD.|  
|**SQLExtendedFetch**|Returns the specified rowset. ODBC 3*.x* drivers implement this if they want to work with ODBC 2.*x* applications that call **SQLExtendedFetch** or **SQLSetPos**. The following are implementation details:<br /><br /> -   The driver retrieves the rowset size from the value of the SQL_ROWSET_SIZE statement attribute.<br />-   The driver retrieves the address of the row status array from the *RowStatusArray* argument, not the SQL_ATTR_ROW_STATUS_PTR statement attribute. The *RowStatusArray* argument in a call to **SQLExtendedFetch** must not be a null pointer. (Note that in ODBC 3*.x*, the SQL_ATTR_ROW_STATUS_PTR statement attribute can be a null pointer.)<br />-   The driver retrieves the address of the rows fetched buffer from the *RowCountPtr* argument, not the SQL_ATTR_ROWS_FETCHED_PTR statement attribute.<br />-   The driver returns SQLSTATE 01S01 (Error in row) to indicate that an error has occurred while rows were fetched by a call to **SQLExtendedFetch**. An ODBC 3*.x* driver should return SQLSTATE 01S01 (Error in row) only when **SQLExtendedFetch** is called, not when **SQLFetch** or **SQLFetchScroll** is called. To preserve backward compatibility, when SQLSTATE 01S01 (Error in row) is returned by **SQLExtendedFetch**, the Driver Manager does not order status records in the error queue according to the rules stated in the "Sequence of Status Records" section of [SQLGetDiagField](../../../odbc/reference/syntax/sqlgetdiagfield-function.md).|  
|**SQLFetch**|Returns the next rowset. The following are implementation details:<br /><br /> -   The driver retrieves the rowset size from the value of the SQL_ATTR_ROW_ARRAY_SIZE statement attribute.<br />-   The driver retrieves the address of the row status array from the SQL_ATTR_ROW_STATUS_PTR statement attribute.<br />-   The driver retrieves the address of the rows fetched buffer from the SQL_ATTR_ROWS_FETCHED_PTR statement attribute.<br />-   The application can mix calls between **SQLFetchScroll** and **SQLFetch**.<br />-   **SQLFetch** returns bookmarks if column 0 is bound.<br />-   **SQLFetch** can be called to return more than one row.<br />-   The driver does not return SQLSTATE 01S01 (Error in row) to indicate that an error has occurred while rows were fetched by a call to **SQLFetch**.|  
|**SQLFetchScroll**|Returns the specified rowset. The following are implementation details:<br /><br /> -   The driver retrieves the rowset size from the SQL_ATTR_ROW_ARRAY_SIZE statement attribute.<br />-   The driver retrieves the address of the row status array from the SQL_ATTR_ROW_STATUS_PTR statement attribute.<br />-   The driver retrieves the address of the rows fetched buffer from the SQL_ATTR_ROWS_FETCHED_PTR statement attribute.<br />-   The application can mix calls between **SQLFetchScroll** and **SQLFetch**.<br />-   The driver does not return SQLSTATE 01S01 (Error in row) to indicate that an error has occurred while rows were fetched by a call to **SQLFetchScroll**.|  
|**SQLSetPos**|Performs various positioned operations. The following are implementation details:<br /><br /> -   This can be called in statement states S6 or S7. For more details, see [Statement Transitions](../../../odbc/reference/appendixes/statement-transitions.md) in Appendix B: ODBC State Transition Tables.<br />-   If this is called in statement state S5 or S6, the driver retrieves the rowset size from the SQL_ATTR_ROWS_FETCHED_PTR statement attribute and the address of the row status array from the SQL_ATTR_ROW_STATUS_PTR statement attribute.<br />-   If this is called in statement state S7, the driver retrieves the rowset size from the SQL_ROWSET_SIZE statement attribute and the address of the row status array from the *RowStatusArray* argument of **SQLExtendedFetch**.<br />-   The driver returns SQLSTATE 01S01 (Error in row) only to indicate that an error has occurred while rows were fetched by a call to **SQLSetPos** to perform a bulk operation when the function is called in state S7. To preserve backward compatibility, if SQLSTATE 01S01 (Error in row) is returned by **SQLSetPos**, the Driver Manager does not order status records in the error queue according to the rules stated in the "Sequence of Status Records" section of [SQLGetDiagField](../../../odbc/reference/syntax/sqlgetdiagfield-function.md).<br />-   If the driver should work with ODBC 2.*x* applications that call **SQLSetPos** with an *Operation* argument of SQL_ADD, the driver must support **SQLSetPos** with an *Operation* argument of SQL_ADD.|
