---
title: "Row Status | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "ODBC cursor library [ODBC], cache"
  - "cursor library [ODBC], cache"
  - "row status [ODBC]"
  - "cache [ODBC]"
ms.assetid: 0f0b1fb6-f697-4ced-811c-2908e210bc71
caps.latest.revision: 8
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Row Status
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work and plan to modify applications that currently use this feature. Microsoft recommends using the driver's cursor functionality.  
  
 The cursor library creates a buffer in the cache for the row status. The cursor library retrieves values for the row status array (specified with the SQL_ATTR_ROW_STATUS_PTR statement attribute) from this buffer. For each row, the cursor library sets this buffer to:  
  
-   SQL_ROW_DELETED when it executes a positioned delete statement on the row.  
  
-   SQL_ROW_ERROR when it encounters an error retrieving the row from the data source with **SQLFetch**.  
  
-   SQL_ROW_SUCCESS when it successfully fetches the row from the data source with **SQLFetch**.  
  
-   SQL_ROW_UPDATED when it executes a positioned update statement on the row.