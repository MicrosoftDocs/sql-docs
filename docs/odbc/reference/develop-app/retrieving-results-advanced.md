---
title: "Retrieving Results (Advanced) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "offsets [ODBC]"
  - "result sets [ODBC], about result sets"
  - "bind offsets [ODBC]"
ms.assetid: bc00c379-71a7-407a-975c-898243f39bb6
author: MightyPen
ms.author: genemi
manager: craigg
---
# Retrieving Results (Advanced)
An application can specify that an offset is added to bound data buffer addresses and the corresponding length/indicator buffer addresses when **SQLBulkOperations**, **SQLFetch**, **SQLFetchScroll**, or **SQLSetPos** is called. The results of these additions determine the addresses used in these operations.  
  
 Bind offsets allow an application to change bindings without calling **SQLBindCol** for previously bound columns. A call to **SQLBindCol** to rebind data changes the buffer address and the length/indicator pointer. Rebinding with an offset, on the other hand, simply adds an offset to the existing bound data buffer address and length/indicator buffer address. When offsets are used, the bindings are a "template" of how the application buffers are laid out and the application can move this "template" to different areas of memory by changing the offset. A new offset can be specified at any time and is always added to the originally bound values.  
  
 To specify a bind offset, the application sets the SQL_ATTR_ROW_BIND_OFFSET_PTR statement attribute to the address of an SQLINTEGER buffer. Before the application calls a function that uses the bindings, such as **SQLBulkOperations**, **SQLFetch**, **SQLFetchScroll**, or **SQLSetPos**, it places an offset in bytes in this buffer, as long as neither the data buffer address nor the length/indicator buffer address is 0, and as long as the bound column is in the result set. The sum of the address and the offset must be a valid address. (This means that either or both the offset and the address to which the offset is added can be invalid, as long as their sum is a valid address.) The SQL_ATTR_ROW_BIND_OFFSET_PTR statement attribute is a pointer so that the offset value can be applied to more than one set of binding data, all of which can be changed by changing one offset value. An application must make sure that the pointer remains valid until the cursor is closed.  
  
> [!NOTE]  
>  Binding offsets are not supported by ODBC 2.*x* drivers.  
  
 This section contains the following topics.  
  
-   [Block Cursors](../../../odbc/reference/develop-app/block-cursors.md)  
  
-   [Scrollable Cursors](../../../odbc/reference/develop-app/scrollable-cursors.md)  
  
-   [The ODBC Cursor Library](../../../odbc/reference/develop-app/the-odbc-cursor-library.md)  
  
-   [Multiple Results](../../../odbc/reference/develop-app/multiple-results.md)
