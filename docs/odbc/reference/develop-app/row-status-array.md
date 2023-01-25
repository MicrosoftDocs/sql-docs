---
description: "Row Status Array"
title: "Row Status Array | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "row status array [ODBC]"
  - "cursors [ODBC], block"
  - "result sets [ODBC], row status array"
  - "block cursors [ODBC]"
  - "result sets [ODBC], block cursors"
  - "rowset status [ODBC]"
ms.assetid: 4b69f189-2722-4314-8a02-f4ffecd6dabd
author: David-Engel
ms.author: v-davidengel
---
# Row Status Array
In addition to data, **SQLFetch** and **SQLFetchScroll** can return an array that gives the status of each row in the rowset. This array is specified through the SQL_ATTR_ROW_STATUS_PTR statement attribute. This array is allocated by the application and must have as many elements as are specified by the SQL_ATTR_ROW_ARRAY_SIZE statement attribute. The values in the array are set by **SQLBulkOperations**, **SQLFetch**, **SQLFetchScroll**, and **SQLSetPos.** The values describe the status of the row and whether that status has changed since it was last fetched.  
  
|Row status array value|Description|  
|----------------------------|-----------------|  
|SQL_ROW_SUCCESS|The row was successfully fetched and has not changed since it was last fetched.|  
|SQL_ROW_SUCCESS_WITH_INFO|The row was successfully fetched and has not changed since it was last fetched. However, a warning was returned about the row.|  
|SQL_ROW_ERROR|An error occurred while fetching the row.|  
|SQL_ROW_UPDATED|The row was successfully fetched and has been updated since it was last fetched. If the row is fetched again or refreshed by **SQLSetPos**, its status is changed to the new status.<br /><br /> Some drivers cannot detect changes to data and therefore cannot return this value. To determine whether a driver can detect updates to refetched rows, an application calls **SQLGetInfo** with the SQL_ROW_UPDATES option.|  
|SQL_ROW_DELETED|The row has been deleted since it was last fetched.|  
|SQL_ROW_ADDED|The row was inserted by **SQLBulkOperations**. If the row is fetched again or is refreshed by **SQLSetPos**, its status is SQL_ROW_SUCCESS.<br /><br /> This value is not set by **SQLFetch** or **SQLFetchScroll**.|  
|SQL_ROW_NOROW|The rowset overlapped the end of the result set, and no row was returned that corresponded to this element of the row status array.|
