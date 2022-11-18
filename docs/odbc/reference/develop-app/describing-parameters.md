---
description: "Describing Parameters"
title: "Describing Parameters | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLBindParameter function [ODBC], describing parameters"
ms.assetid: 118d0f47-2afd-4955-bb47-38b1e2c2f38f
author: David-Engel
ms.author: v-davidengel
---
# Describing Parameters
**SQLBindParameter** has arguments that describe the parameter: its SQL type, precision, and scale. The driver uses this information, or *metadata,* to convert the parameter value to the type needed by the data source. At first glance, it might seem that the driver is in a better position to know the parameter metadata than the application; after all, the driver can easily discover the metadata for a result set column. As it turns out, this is not the case. First, most data sources do not provide a way for the driver to discover parameter metadata. Second, most applications already know the metadata.  
  
 If an SQL statement is hard-coded in the application, the application writer already knows the type of each parameter. If an SQL statement is constructed by the application at run time, the application can determine the metadata as it builds the statement. For example, when the application constructs the clause  
  
```  
WHERE OrderID = ?  
```  
  
 it can call **SQLColumns** for the OrderID column.  
  
 The only situation in which the application cannot easily determine the parameter metadata is when the user enters a parameterized statement. In this case, the application calls **SQLPrepare** to prepare the statement, **SQLNumParams** to determine the number of parameters, and **SQLDescribeParam** to describe each parameter. However, as was noted earlier, most data sources do not provide a way for the driver to discover parameter metadata, so **SQLDescribeParam** is not widely supported.
