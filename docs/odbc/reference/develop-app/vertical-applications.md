---
description: "Vertical Applications"
title: "Vertical Applications | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "interoperability [ODBC], vertical applications"
  - "vertical applications [ODBC]"
  - "interoperability [ODBC], levels"
ms.assetid: d50ea3e6-7a9e-4fb6-8cd8-1d429d2f7b3c
author: David-Engel
ms.author: v-davidengel
---
# Vertical Applications
Vertical applications typically perform a well-defined task against a single DBMS. For example, an order entry application tracks the orders in a company. What these types of applications have in common is that the database schema is usually designed by the application developer and, while the application might work with a number of different DBMSs, it works with a single DBMS for a single customer.  
  
 Because vertical applications usually require certain functionality, such as scrollable cursors or transactions, they rarely support all DBMSs. Instead, they tend to be highly interoperable among a limited set of DBMSs. Typically, vertical application developers choose to support those DBMSs that represent a large fraction of the market and ignore the rest. They might even choose to support specific drivers for those DBMSs to reduce their testing and product support costs.  
  
 Because vertical applications can support a known set of DBMSs, they sometimes contain driver-specific or DBMS-specific code. However, such code is best kept to a minimum because it requires extra time to maintain.
