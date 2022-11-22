---
description: "Retrieving Results (Basic)"
title: "Retrieving Results (Basic) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "result sets [ODBC], about result sets"
  - "data sources [ODBC], result sets"
  - "empty result sets [ODBC]"
ms.assetid: 052870e3-3f3f-4f07-91da-b649348225f4
author: David-Engel
ms.author: v-davidengel
---
# Retrieving Results (Basic)
A *result set* is a set of rows on the data source that matches certain criteria. It is a conceptual table that results from a query and that is available to an application in tabular form. **SELECT** statements, catalog functions, and some procedures create result sets. In the following example, the first SQL statement creates a result set containing all the rows and all the columns in the Orders table, and the second SQL statement creates a result set containing OrderID, SalesPerson, and Status columns for the rows in the Orders table in which the Status is OPEN:  
  
```  
SELECT * FROM Orders  
SELECT OrderID, SalesPerson, Status FROM Orders WHERE Status = 'OPEN'  
```  
  
 A result set can be empty, which is different from no result set at all. For example, the following SQL statement creates an empty result set:  
  
```  
SELECT * FROM Orders WHERE 1 = 2  
```  
  
 An empty result set is no different from any other result set except that it has no rows. For example, the application can retrieve metadata for the result set, can attempt to fetch rows, and must close the cursor over the result set.  
  
 The process of retrieving rows from the data source and returning them to the application is called *fetching*. This section explains the basic parts of that process. For information about more advanced topics, such as block and scrollable cursors, see [Block Cursors](../../../odbc/reference/develop-app/block-cursors.md) and [Scrollable Cursors](../../../odbc/reference/develop-app/scrollable-cursors.md). For information about updating, deleting, and inserting rows, see [Updating Data Overview](../../../odbc/reference/develop-app/updating-data-overview.md).  
  
 This section contains the following topics.  
  
-   [Was a Result Set Created?](../../../odbc/reference/develop-app/was-a-result-set-created.md)  
  
-   [Result Set Metadata](../../../odbc/reference/develop-app/result-set-metadata.md)  
  
-   [Binding Columns](../../../odbc/reference/develop-app/binding-columns.md)  
  
-   [Fetching Data](../../../odbc/reference/develop-app/fetching-data.md)  
  
-   [Closing the Cursor](../../../odbc/reference/develop-app/closing-the-cursor.md)
