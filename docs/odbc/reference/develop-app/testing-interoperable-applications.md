---
description: "Testing Interoperable Applications"
title: "Testing Interoperable Applications | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "interoperability [ODBC], testing interoperable applications"
  - "testing interoperable applications [ODBC]"
ms.assetid: 489083cb-8430-40be-9ef2-d75b9a2eea88
author: David-Engel
ms.author: v-davidengel
---
# Testing Interoperable Applications
Testing interoperable applications is at best a time-consuming business and at worst impossible because new drivers continually appear on the market. However, a reasonable degree of testing is possible. Applications with limited or low interoperability need only be tested against those drivers they are guaranteed to support. However, they must be fully tested against these drivers.  
  
 Highly interoperable applications cannot be tested practically against all drivers. The best that most application developers can do is to test them fully against a small number of drivers and cursorily against several more. Tested drivers should include the most popular drivers for the most popular DBMSs in the application's market; if the market covers all DBMSs, drivers for both desktop and server DBMSs should be tested.  
  
 One of the problems in testing ODBC applications is the number of components involved: the application itself, the Driver Manager, the driver, the DBMS, and possibly network software or gateways. Applications can make it easier to track errors by posting the error messages returned by ODBC functions through **SQLGetDiagField** and **SQLGetDiagRec**. These messages identify the manufacturer and component in which errors occur. For more information, see [Diagnostics](../../../odbc/reference/develop-app/diagnostics.md).
