---
title: "Binding Columns for Use with Block Cursors | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "column-wise binding [ODBC]"
  - "row-wise binding [ODBC]"
  - "result sets [ODBC], binding columns"
  - "cursors [ODBC], block"
  - "binding columns [ODBC]"
  - "block cursors [ODBC]"
  - "result sets [ODBC], block cursors"
ms.assetid: 231beede-cdfa-4e28-8b10-2760b983250f
author: MightyPen
ms.author: genemi
manager: craigg
---
# Binding Columns for Use with Block Cursors
Because block cursors return multiple rows, applications that use them must bind an array of variables to each column instead of a single variable. These arrays are collectively known as the *rowset buffers*. Following are the two styles of binding:  
  
-   Bind an array to each column. This is called *column-wise binding* because each data structure (array) contains data for a single column.  
  
-   Define a structure to hold the data for an entire row and bind an array of these structures. This is called *row-wise binding* because each data structure contains the data for a single row.  
  
 As when the application binds single variables to columns, it calls **SQLBindCol** to bind arrays to columns. The only difference is that the addresses passed are array addresses, not single variable addresses. The application sets the SQL_BIND_BY_COLUMN statement attribute to specify whether it is using column-wise or row-wise binding. Whether to use column-wise or row-wise binding is largely a matter of application preference. Row-wise binding might correspond more closely to the application's layout of data, in which case it would provide better performance.  
  
 This section contains the following topics.  
  
-   [Column-Wise Binding](../../../odbc/reference/develop-app/column-wise-binding.md)  
  
-   [Row-Wise Binding](../../../odbc/reference/develop-app/row-wise-binding.md)
