---
description: "Using Concise Functions"
title: "Using Concise Functions | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "concise functions [ODBC]"
  - "functions [ODBC], concise functions"
  - "descriptors [ODBC], concise functions"
ms.assetid: 31ac070f-8c59-4fd5-bd5a-466bb27dbca0
author: David-Engel
ms.author: v-davidengel
---
# Using Concise Functions
Some ODBC functions gain implicit access to descriptors. Application writers may find them more convenient than calling **SQLSetDescField** or **SQLGetDescField**. These functions are called *concise* functions because they perform a number of functions, including setting or getting descriptor fields. Some concise functions let an application set or retrieve several related descriptor fields in a single function call.  
  
 Concise functions can be called without first retrieving a descriptor handle for use as an argument. These functions work with the descriptor fields associated with the statement handle that they are called on.  
  
 The concise functions **SQLBindCol** and **SQLBindParameter** bind a column or parameter by setting the descriptor fields that correspond to their arguments. Each of these functions performs more tasks than simply setting descriptors. **SQLBindCol** and **SQLBindParameter** provide a complete specification of the binding of a data column or dynamic parameter. An application can, however, change individual details of a binding by calling **SQLSetDescField** or **SQLSetDescRec** and can completely bind a column or parameter by making a series of suitable calls to these functions.  
  
 The concise functions **SQLColAttribute**, **SQLDescribeCol**, **SQLDescribeParam**, **SQLNumParams**, and **SQLNumResultCols** retrieve values in descriptor fields.  
  
 **SQLSetDescRec** and **SQLGetDescRec** are concise functions that, with one call, set or get multiple descriptor fields that affect the data type and storage of column or parameter data. **SQLSetDescRec** is an effective way to change the binding of column or parameter data in one step.  
  
 **SQLSetStmtAttr** and **SQLGetStmtAttr** serve as concise functions in some cases. (See [Descriptor Fields](../../../odbc/reference/develop-app/descriptor-fields.md).)
