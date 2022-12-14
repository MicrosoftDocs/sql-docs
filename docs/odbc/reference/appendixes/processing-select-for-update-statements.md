---
description: "Processing SELECT FOR UPDATE Statements"
title: "Processing SELECT FOR UPDATE Statements | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "ODBC cursor library [ODBC], statement processing"
  - "SQL statements [ODBC], select for update statements"
  - "select for update [ODBC]"
  - "SQL statements [ODBC], cursor library"
  - "cursor library [ODBC], select for update statements"
  - "ODBC cursor library [ODBC], select for update statements"
  - "cursor library [ODBC], statement processing"
ms.assetid: 8d2e79a4-5daf-458e-a536-d8b6e588753e
author: David-Engel
ms.author: v-davidengel
---
# Processing SELECT FOR UPDATE Statements
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work and plan to modify applications that currently use this feature. Microsoft recommends using the driver's cursor functionality.  
  
 For maximum interoperability, applications should generate result sets that will be updated with a positioned update statement by executing a **SELECT FOR UPDATE** statement. Although the cursor library does not require this, it is required by most data sources that support positioned update statements.  
  
 The cursor library ignores the columns in the **FOR UPDATE** clause of a **SELECT FOR UPDATE** statement; it removes this clause before passing the statement to the driver. In the cursor library, the SQL_ATTR_CONCURRENCY statement attribute, along with the restrictions mentioned in the previous section, controls whether the columns in a result set can be updated.
