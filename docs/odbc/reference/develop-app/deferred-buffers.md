---
title: "Deferred Buffers"
description: "Deferred Buffers"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sunilbs, mcimfl
ms.date: "01/27/2026"
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
helpviewer_keywords:
  - "buffers [ODBC], deferred"
  - "deferred buffers [ODBC]"
---
# Deferred Buffers

A *deferred buffer* is one whose value is used at some time *after* you specify it in a function call. For example, you use **SQLBindParameter** to associate, or *bind,* a data buffer with a parameter in a SQL statement. You specify the number of the parameter and pass the address, byte length, and type of the buffer. The driver saves this information but doesn't examine the contents of the buffer. Later, when you execute the statement, the driver retrieves the information and uses it to retrieve the parameter data and send it to the data source. Therefore, the input of data in the buffer is deferred. Because deferred buffers are specified in one function and used in another, it's an application programming error to free a deferred buffer while the driver still expects it to exist. For more information, see [Allocating and Freeing Buffers](../../../odbc/reference/develop-app/allocating-and-freeing-buffers.md), later in this section.  
  
## Deferred buffer types

Both input and output buffers can be deferred. The following table summarizes the uses of deferred buffers. Note that deferred buffers bound to result set columns are specified with **SQLBindCol**, and deferred buffers bound to SQL statement parameters are specified with **SQLBindParameter**.  
  
|Buffer use|Type|Specified with|Used by|  
|----------------|----------|--------------------|-------------|  
|Sending data for input parameters|Deferred input|**SQLBindParameter**|**SQLExecute**<br /> **SQLExecDirect**|  
|Sending data to update or insert a row in a result set|Deferred input|**SQLBindCol**|**SQLSetPos**<br /> **SQLBulkOperations**|  
|Returning data for output and input/output parameters|Deferred output|**SQLBindParameter**|**SQLExecute**<br /> **SQLExecDirect**|  
|Returning result set data|Deferred output|**SQLBindCol**|**SQLFetch**<br /> **SQLFetchScroll SQLSetPos**|

## Related content

- [Allocating and Freeing Buffers](allocating-and-freeing-buffers.md)
- [Data Buffer Address](data-buffer-address.md)
- [Binding Parameters ODBC](binding-parameters-odbc.md)
