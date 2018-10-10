---
title: "Deferred Buffers | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "buffers [ODBC], deferred"
  - "deferred buffers [ODBC]"
ms.assetid: 02c9a75c-2103-4f68-a1db-e31f7e0f1f03
author: MightyPen
ms.author: genemi
manager: craigg
---
# Deferred Buffers
A *deferred buffer* is one whose value is used at some time *after* it is specified in a function call. For example, **SQLBindParameter** is used to associate, or *bind,* a data buffer with a parameter in an SQL statement. The application specifies the number of the parameter and passes the address, byte length, and type of the buffer. The driver saves this information but does not examine the contents of the buffer. Later, when the application executes the statement, the driver retrieves the information and uses it to retrieve the parameter data and send it to the data source. Therefore, the input of data in the buffer is deferred. Because deferred buffers are specified in one function and used in another, it is an application programming error to free a deferred buffer while the driver still expects it to exist; for more information, see [Allocating and Freeing Buffers](../../../odbc/reference/develop-app/allocating-and-freeing-buffers.md), later in this section.  
  
 Both input and output buffers can be deferred. The following table summarizes the uses of deferred buffers. Note that deferred buffers bound to result set columns are specified with **SQLBindCol**, and deferred buffers bound to SQL statement parameters are specified with **SQLBindParameter**.  
  
|Buffer use|Type|Specified with|Used by|  
|----------------|----------|--------------------|-------------|  
|Sending data for input parameters|Deferred input|**SQLBindParameter**|**SQLExecute**<br /> **SQLExecDirect**|  
|Sending data to update or insert a row in a result set|Deferred input|**SQLBindCol**|**SQLSetPos**|  
|Returning data for output and input/output parameters|Deferred output|**SQLBindParameter**|**SQLExecute**<br /> **SQLExecDirect**|  
|Returning result set data|Deferred output|**SQLBindCol**|**SQLFetch**<br /> **SQLFetchScroll SQLSetPos**|
