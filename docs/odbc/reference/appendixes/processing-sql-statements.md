---
title: "Processing SQL Statements | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "ODBC cursor library [ODBC], statement processing"
  - "SQL statements [ODBC], cursor library"
  - "cursor library [ODBC], statement processing"
ms.assetid: 54dad6a3-e86c-477b-ba7c-4e95e0385ec1
author: MightyPen
ms.author: genemi
manager: craigg
---
# Processing SQL Statements
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work and plan to modify applications that currently use this feature. Microsoft recommends using the driver's cursor functionality.  
  
 The ODBC cursor library passes all SQL statements directly to the driver except the following:  
  
-   Positioned update and delete statements  
  
-   **SELECT FOR UPDATE** statements  
  
-   Batched SQL statements  
  
 To execute positioned update and delete statements and to position the cursor on a row to call **SQLGetData** for that row, the cursor library constructs a searched statement that identifies the row.  
  
 This section contains the following topics.  
  
-   [Processing Positioned Update and Delete Statements](../../../odbc/reference/appendixes/processing-positioned-update-and-delete-statements.md)  
  
-   [Processing SELECT FOR UPDATE Statements](../../../odbc/reference/appendixes/processing-select-for-update-statements.md)  
  
-   [Processing Batches of SQL Statements](../../../odbc/reference/appendixes/processing-batches-of-sql-statements.md)  
  
-   [Constructing Searched Statements](../../../odbc/reference/appendixes/constructing-searched-statements.md)
