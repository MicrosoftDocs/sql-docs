---
description: "Block Cursors, Scrollable Cursors, and Backward Compatibility for ODBC 3.x Applications"
title: "Block and Scrollable Cursors Compatibility for ODBC 3.x | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "compatibility [ODBC], cursors"
  - "backward compatibility [ODBC], cursors"
  - "SQLExtendedFetch function [ODBC], block cursors"
  - "cursors [ODBC], compatibility issues"
  - "SQLFetchScroll function [ODBC], block cursors"
ms.assetid: 82f6cf68-cfde-4417-9788-d6382ca14bf8
author: David-Engel
ms.author: v-davidengel
---
# Block Cursors, Scrollable Cursors, and Backward Compatibility for ODBC 3.x Applications
The existence of both **SQLFetchScroll** and **SQLExtendedFetch** represents the first clear split in ODBC between the Application Programming Interface (API), which is the set of functions the application calls, and the Service Provider Interface (SPI), which is the set of functions the driver implements. This split is required to balance the requirement in ODBC *3.x*, which uses **SQLFetchScroll**, to align with the standards and be compatible with ODBC *2.x*, which uses **SQLExtendedFetch**.  
  
 The ODBC *3.x* API, which is the set of functions the application calls, includes **SQLFetchScroll** and related statement attributes. The ODBC *3.x* SPI, which is the set of functions the driver implements, includes **SQLFetchScroll**, **SQLExtendedFetch**, and related statement attributes. Because ODBC does not formally enforce this split between the API and the SPI, it is possible for ODBC *3.x* applications to call **SQLExtendedFetch** and related statement attributes. However, there is no reason for ODBC *3.x* applications to do this. For more information about APIs and SPIs, see the introduction to [ODBC Architecture](../../../odbc/reference/odbc-architecture.md).  
  
 For information about how the ODBC *3.x* Driver Manager maps calls to ODBC *2.x* and ODBC *3.x* drivers, and what functions and statement attributes an ODBC *3.x* driver should implement for block and scrollable cursors, see [What the Driver Does](../../../odbc/reference/appendixes/what-the-driver-does.md) in Appendix G: Driver Guidelines for Backward Compatibility.  
  
 The following table summarizes what functions and statement attributes an ODBC *3.x* application should use with block and scrollable cursors. It also lists changes between ODBC *2.x* and ODBC *3.x* in this area that ODBC *3.x* applications should be aware of to be compatible with ODBC *2.x* drivers.  
  
|Function or<br /><br /> statement attribute|Comments|  
|-----------------------------------------|--------------|  
|SQL_ATTR_FETCH_BOOKMARK_PTR|Points to the bookmark to use with **SQLFetchScroll**.<br /><br /> When an application sets this in an ODBC *2.x* driver, this must point to a fixed-length bookmark.|  
|SQL_ATTR_ROW_STATUS_PTR|Points to the row status array filled by **SQLFetch**, **SQLFetchScroll**, **SQLBulkOperations**, and **SQLSetPos**.<br /><br /> If an application sets this in an ODBC *2.x* driver and calls **SQLBulkOperation** with an *Operation* of SQL_ADD before calling **SQLFetchScroll**, **SQLFetch**, or **SQLExtendedFetch**, SQLSTATE HY011 (Attribute cannot be set now) is returned.<br /><br /> When an application calls **SQLFetch** in an ODBC *2.x* driver, **SQLFetch** is mapped to **SQLExtendedFetch** and therefore returns values in this array.|  
|SQL_ATTR_ROWS_FETCHED_PTR|Points to the buffer in which **SQLFetch** and **SQLFetchScroll** return the number of rows fetched.<br /><br /> When an application calls **SQLFetch** in an ODBC *2.x* driver, **SQLFetch** is mapped to **SQLExtendedFetch** and therefore returns a value in this buffer.|  
|SQL_ATTR_ROW_ARRAY_SIZE|Sets the rowset size.<br /><br /> If an application calls **SQLBulkOperations** with an *Operation* of SQL_ADD in an ODBC *2.x* driver, SQL_ROWSET_SIZE will be used for the call, not SQL_ATTR_ROW_ARRAY_SIZE, because the call is mapped to **SQLSetPos** with an *Operation* of SQL_ADD, which uses SQL_ROWSET_SIZE.<br /><br /> Calling **SQLSetPos** with an *Operation* of SQL_ADD or **SQLExtendedFetch** in an ODBC *2.x* driver uses SQL_ROWSET_SIZE.<br /><br /> Calling **SQLFetch** or **SQLFetchScroll** in an ODBC *2.x* driver uses SQL_ATTR_ROW_ARRAY_SIZE.|  
|**SQLBulkOperations**|Performs insert and bookmark operations. When **SQLBulkOperations** with an *Operation* of SQL_ADD is called in an ODBC *2.x* driver, it is mapped to **SQLSetPos** with an *Operation* of SQL_ADD. The following are implementation details:<br /><br /> -   When working with an ODBC *2.x* driver, an application must use only the implicitly allocated ARD associated with the *StatementHandle*; it cannot allocate another ARD for adding rows, because explicit descriptor operations are not supported in an ODBC *2.x* driver. An application must use **SQLBindCol** to bind to the ARD, not **SQLSetDescField** or **SQLSetDescRec**.<br />-   When calling an ODBC *3.x* driver, an application can call **SQLBulkOperations** with an *Operation* of SQL_ADD before calling **SQLFetch** or **SQLFetchScroll**. When calling an ODBC *2.x* driver, an application must call **SQLFetchScroll** before calling **SQLBulkOperations** with an Operation of SQL_ADD.|  
|**SQLFetch**|Returns the next rowset. The following are implementation details:<br /><br /> -   When an application calls **SQLFetch** in an ODBC *2.x* driver, it is mapped to **SQLExtendedFetch**.<br />-   When an application calls **SQLFetch** in an ODBC *3.x* driver, it returns the number of rows specified with the SQL_ATTR_ROW_ARRAY_SIZE statement attribute.|  
|**SQLFetchScroll**|Returns the specified rowset. The following are implementation details:<br /><br /> -   When an application calls **SQLFetchScroll** in an ODBC *2.x* driver, it returns SQLSTATE 01S01 (Error in row) before each error that applies to a single row. It does this only because the ODBC *3.x* Driver Manager maps this to **SQLExtendedFetch** and **SQLExtendedFetch** returns this SQLSTATE. When an application calls **SQLFetchScroll** in an ODBC *3.x* driver, it never returns SQLSTATE 01S01 (Error in row).<br />-   When an application calls **SQLFetchScroll** in an ODBC *2.x* driver with *FetchOrientation* set to SQL_FETCH_BOOKMARK, the *FetchOffset* argument must be set to 0. SQLSTATE HYC00 (Optional feature not implemented) is returned if offset-based bookmark fetching is attempted with an ODBC *2.x* driver.|  
  
> [!NOTE]  
>  ODBC *3.x* applications should not use **SQLExtendedFetch** or the SQL_ROWSET_SIZE statement attribute. Instead, they should use **SQLFetchScroll** and the SQL_ATTR_ROW_ARRAY_SIZE statement attribute. ODBC *3.x* applications should not use **SQLSetPos** with an *Operation* of SQL_ADD but should use **SQLBulkOperations** with an *Operation* of SQL_ADD.
