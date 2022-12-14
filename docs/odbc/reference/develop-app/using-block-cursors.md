---
description: "Using Block Cursors"
title: "Using Block Cursors | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "cursors [ODBC], block"
  - "block cursors [ODBC]"
  - "result sets [ODBC], block cursors"
ms.assetid: 2aad7d6b-216e-47e7-b3cb-f95ad096f21a
author: David-Engel
ms.author: v-davidengel
---
# Using Block Cursors
Support for block cursors is built into ODBC 3.*x*. **SQLFetch** can be used only for multirow fetches when called in ODBC 3.*x*; if an ODBC 2.*x* application calls **SQLFetch**, it will open only a single-row, forward-only cursor. When an ODBC 3.*x* application calls **SQLFetch** in an ODBC 2.*x* driver, it returns a single row unless the driver supports **SQLExtendedFetch**. For more information, see [Block Cursors, Scrollable Cursors, and Backward Compatibility](../../../odbc/reference/appendixes/block-cursors-scrollable-cursors-and-backward-compatibility.md) in Appendix G: Driver Guidelines for Backward Compatibility.  
  
 To use block cursors, the application sets the rowset size, binds the rowset buffers (as described in the previous section), optionally sets the SQL_ATTR_ROWS_FETCHED_PTR and SQL_ATTR_ROW_STATUS_PTR statement attributes, and calls **SQLFetch** or **SQLFetchScroll** to fetch a block of rows. The application can change the rowset size and bind new rowset buffers (by calling **SQLBindCol** or specifying a bind offset) even after rows have been fetched.  
  
 This section contains the following topics.  
  
-   [Rowset Size](../../../odbc/reference/develop-app/rowset-size.md)  
  
-   [Number of Rows Fetched and Status](../../../odbc/reference/develop-app/number-of-rows-fetched-and-status.md)  
  
-   [SQLGetData and Block Cursors; block curso](../../../odbc/reference/develop-app/sqlgetdata-and-block-cursors.md)
