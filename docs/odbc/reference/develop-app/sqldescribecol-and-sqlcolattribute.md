---
description: "SQLDescribeCol and SQLColAttribute"
title: "SQLDescribeCol and SQLColAttribute | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLColAttribute function [ODBC], and SQLDescribeCol"
  - "SQLDescribeCol function [ODBC], and SQLColAttribute"
  - "result sets [ODBC], metadata"
  - "retrieving result set meta data [ODBC]"
  - "metadata [ODBC], result set"
ms.assetid: c2ca442c-03a8-4e0f-9e67-b300bb15962f
author: David-Engel
ms.author: v-davidengel
---
# SQLDescribeCol and SQLColAttribute
**SQLDescribeCol** and **SQLColAttribute** are used to retrieve result set metadata. The difference between these two functions is that **SQLDescribeCol** always returns the same five pieces of information (a column's name, data type, precision, scale, and nullability), while **SQLColAttribute** returns a single piece of information requested by the application. However, **SQLColAttribute** can return a much richer selection of metadata, including a column's case-sensitivity, display size, updatability, and searchability.  
  
 Many applications, especially ones that only display data, require only the metadata returned by **SQLDescribeCol**. For these applications, it is faster to use **SQLDescribeCol** than **SQLColAttribute** because the information is returned in a single call. Other applications, especially ones that update data, require the additional metadata returned by **SQLColAttribute** and therefore use both functions. In addition, **SQLColAttribute** supports driver-specific metadata; for more information, see [Driver-Specific Data Types, Descriptor Types, Information Types, Diagnostic Types, and Attributes](../../../odbc/reference/develop-app/driver-specific-data-types-descriptor-information-diagnostic.md).  
  
 An application can retrieve result set metadata at any time after a statement has been prepared or executed and before the cursor over the result set is closed. Very few applications require result set metadata after the statement is prepared and before it is executed. If possible, applications should wait to retrieve metadata until after the statement is executed, because some data sources cannot return metadata for prepared statements and emulating this capability in the driver is often a slow process. For example, the driver might generate a zero-row result set by replacing the **WHERE** clause of a **SELECT** statement with the clause **WHERE 1 = 2** and executing the resulting statement.  
  
 Metadata is often expensive to retrieve from the data source. Because of this, drivers should cache any metadata they retrieve from the server and hold it for as long as the cursor over the result set is open. Also, applications should request only the metadata they absolutely need.
