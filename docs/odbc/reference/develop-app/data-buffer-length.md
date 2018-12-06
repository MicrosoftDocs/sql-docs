---
title: "Data Buffer Length | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "data buffers [ODBC], length"
  - "buffers [ODBC], data"
  - "length of data buffers [ODBC]"
  - "buffers [ODBC], length"
ms.assetid: 7288d143-f9e5-4f90-9b31-2549df79c109
author: MightyPen
ms.author: genemi
manager: craigg
---
# Data Buffer Length
The application passes the byte length of the data buffer to the driver in an argument, named *BufferLength* or a similar name. For example, in the following call to **SQLBindCol**, the application specifies the length of the *ValuePtr* buffer (**sizeof(***ValuePtr***)**):  
  
```  
SQLCHAR      ValuePtr[50];  
SQLINTEGER   ValueLenOrInd;  
SQLBindCol(hstmt, 1, SQL_C_CHAR, ValuePtr, sizeof(ValuePtr), &ValueLenOrInd);  
```  
  
 A driver will always return the number of bytes, not the number of characters, in the buffer length argument of any function that has an output string argument.  
  
 Data buffer lengths are required only for output buffers; the driver uses them to avoid writing past the end of the buffer. However, the driver checks the data buffer length only when the buffer contains variable-length data, such as character or binary data. If the buffer contains fixed-length data, such as an integer or date structure, the driver ignores the data buffer length and assumes the buffer is large enough to hold the data; that is, it never truncates fixed-length data. It is therefore important for the application to allocate a large enough buffer for fixed-length data.  
  
 When a truncation of non-data output strings occurs (such as the cursor name returned for **SQLGetCursorName**), the returned length in the buffer length argument is the maximum character length possible.  
  
 Data buffer lengths are not required for input buffers because the driver does not write to these buffers.  
  
 This section contains the following topics.  
  
-   [Using Length/Indicator Values](../../../odbc/reference/develop-app/using-length-and-indicator-values.md)  
  
-   [Data Length, Buffer Length, and Truncation](../../../odbc/reference/develop-app/data-length-buffer-length-and-truncation.md)  
  
-   [Character Data and C Strings](../../../odbc/reference/develop-app/character-data-and-c-strings.md)
