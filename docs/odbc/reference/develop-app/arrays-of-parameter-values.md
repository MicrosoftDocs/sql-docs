---
description: "Arrays of Parameter Values"
title: "Arrays of Parameter Values | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "arrays of parameter values [ODBC]"
  - "parameter arrays [ODBC]"
ms.assetid: 9b572c5b-1dfe-40af-bebd-051548ab6d90
author: David-Engel
ms.author: v-davidengel
---
# Arrays of Parameter Values
It is often useful for applications to pass arrays of parameters. For example, using arrays of parameters and a parameterized **INSERT** statement, an application can insert a number of rows at once. There are several advantages to using arrays. First, network traffic is reduced because the data for many statements is sent in a single packet (if the data source supports parameter arrays natively). Second, some data sources can execute SQL statements using arrays faster than executing the same number of separate SQL statements. Finally, when the data is stored in an array, as is often the case for screen data, the application can bind all of the rows in a particular column with a single call to **SQLBindParameter** and update them by executing a single statement.  
  
 Unfortunately, not many data sources support parameter arrays. However, a driver can emulate parameter arrays by executing an SQL statement once for each set of parameter values. This can lead to increases in speed because the driver can then prepare the statement that it plans to execute once for each parameter set. It might also lead to simpler application code.  
  
 This section contains the following topics.  
  
-   [Binding Arrays of Parameters](../../../odbc/reference/develop-app/binding-arrays-of-parameters.md)  
  
-   [Using Arrays of Parameters](../../../odbc/reference/develop-app/using-arrays-of-parameters.md)
