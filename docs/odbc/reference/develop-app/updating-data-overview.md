---
description: "Updating Data Overview"
title: "Updating Data Overview | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "updating data [ODBC], about updating data"
  - "data updates [ODBC]"
  - "updating data [ODBC]"
  - "data updates [ODBC], about data updates"
ms.assetid: 062036a4-cda6-4aaa-9765-f1ec3e0b31b1
author: David-Engel
ms.author: v-davidengel
---
# Updating Data Overview
Applications can update data either by executing SQL statements or by calling **SQLSetPos** or **SQLBulkOperations**. **UPDATE**, **DELETE**, and **INSERT** statements act directly on the data source and are usually supported by drivers. Searched update and delete statements contain a specification of the rows to change. Positioned update and delete statements and **SQLSetPos** act on the data source through a cursor and are less widely supported.  
  
 Whether cursors can detect changes made to the result set with the methods described in this section depends on the type of the cursor and how it is implemented. Forward-only cursors do not revisit rows and therefore will not detect any changes. For information about whether scrollable cursors can detect changes, see [Scrollable Cursors](../../../odbc/reference/develop-app/scrollable-cursors.md).  
  
 This section contains the following topics.  
  
-   [UPDATE, DELETE, and INSERT Statements](../../../odbc/reference/develop-app/update-delete-and-insert-statements.md)  
  
-   [Positioned Update and Delete Statements](../../../odbc/reference/develop-app/positioned-update-and-delete-statements.md)  
  
-   [Simulating Positioned Update and Delete Statements](../../../odbc/reference/develop-app/simulating-positioned-update-and-delete-statements.md)  
  
-   [Determining the Number of Affected Rows](../../../odbc/reference/develop-app/determining-the-number-of-affected-rows.md)  
  
-   [Updating Data with SQLSetPos](../../../odbc/reference/develop-app/updating-data-with-sqlsetpos.md)  
  
-   [Updating Data with SQLBulkOperations](../../../odbc/reference/develop-app/updating-data-with-sqlbulkoperations.md)  
  
-   [Long Data and SQLSetPos and SQLBulkOperations](../../../odbc/reference/develop-app/long-data-and-sqlsetpos-and-sqlbulkoperations.md)
