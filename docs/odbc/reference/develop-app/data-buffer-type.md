---
title: "Data Buffer Type | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "data types [ODBC], buffers"
  - "data buffers [ODBC], types"
  - "buffers [ODBC], data"
  - "C data types [ODBC], buffers"
ms.assetid: 58bea3e9-d552-447f-b3ad-ce1dab213b72
author: MightyPen
ms.author: genemi
manager: craigg
---
# Data Buffer Type
The C data type of a buffer is specified by the application. With a single variable, this occurs when the application allocates the variable. With generic memory - that is, memory pointed to by a pointer of type void - this occurs when the application casts the memory to a particular type. The driver discovers this type in two ways:  
  
-   **Data buffer type argument.** Buffers used to transfer parameter values and result set data, such as the buffer bound with *TargetValuePtr* in **SQLBindCol**, usually have an associated type argument, such as the *TargetType* argument in **SQLBindCol**. In this argument, the application passes the C type identifier that corresponds to the type of the buffer. For example, in the following call to **SQLBindCol**, the value SQL_C_TYPE_DATE tells the driver that the *Date* buffer is an SQL_DATE_STRUCT:  
  
    ```  
    SQL_DATE_STRUCT Date;  
    SQLINTEGER  DateInd;  
    SQLBindCol(hstmt, 1, SQL_C_TYPE_DATE, &Date, 0, &DateInd);  
    ```  
  
     For more information about type identifiers, see the [Data Types in ODBC](../../../odbc/reference/develop-app/data-types-in-odbc.md) section, later in this section.  
  
-   **Predefined type.** Buffers used to send and retrieve options or attributes, such as the buffer pointed to by the *InfoValuePtr* argument in **SQLGetInfo**, have a fixed type that depends on the option specified. The driver assumes that the data buffer is of this type; it is the application's responsibility to allocate a buffer of this type. For example, in the following call to **SQLGetInfo**, the driver assumes the buffer is a 32-bit integer because this is what the SQL_STRING_FUNCTIONS option requires:  
  
    ```  
    SQLUINTEGER StringFuncs;  
    SQLGetInfo(hdbc, SQL_STRING_FUNCTIONS, (SQLPOINTER) &StringFuncs, 0,  
                NULL);  
    ```  
  
 The driver uses the C data type to interpret the data in the buffer.
