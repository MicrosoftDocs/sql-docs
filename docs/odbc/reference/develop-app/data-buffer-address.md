---
title: "Data Buffer Address | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "address of data buffers [ODBC]"
  - "buffers [ODBC], data"
  - "data buffers [ODBC], address"
ms.assetid: f2426d68-71bc-4ef7-a5cb-ee9d6c1c9671
author: MightyPen
ms.author: genemi
manager: craigg
---
# Data Buffer Address
The application passes the address of the data buffer to the driver in an argument, often named *ValuePtr* or a similar name. For example, in the following call to **SQLBindCol**, the application specifies the address of the *Date* variable:  
  
```  
SQL_DATE_STRUCT Date;  
SQLINTEGER DateInd;  
SQLBindCol(hstmt, 1, SQL_C_TYPE_DATE, &dsDate, 0, &DateInd);  
```  
  
 As mentioned in the [Allocating and Freeing Buffers](../../../odbc/reference/develop-app/allocating-and-freeing-buffers.md) section, the address of a deferred buffer must remain valid until the buffer is unbound.  
  
 Unless it is specifically prohibited, the address of a data buffer can be a null pointer. For buffers used to send data to the driver, this causes the driver to ignore the information normally contained in the buffer. For buffers used to retrieve data from the driver, this causes the driver to not return a value. In both cases, the driver ignores the corresponding data buffer length argument.
