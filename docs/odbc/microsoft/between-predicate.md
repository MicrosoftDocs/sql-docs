---
title: "BETWEEN Predicate | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "BETWEEN predicate [ODBC]"
  - "SQL grammar [ODBC], between predicate"
ms.assetid: 0cc7464b-d788-4720-98d8-411e1169185f
author: MightyPen
ms.author: genemi
manager: craigg
---
# BETWEEN Predicate
The syntax:  
  
```  
expression1 BETWEEN expression2 AND expression3  
```  
  
 returns true only if *expression1* is greater than or equal to *expression2* and *expression1* is less than or equal to *expression3*.  
  
 The semantics of this syntax are different for the Desktop Database Drivers and the Microsoft Jet engine. In Microsoft Jet SQL, *expression2* can be greater than *expression3* so that the statement will return TRUE only if *expression1* is greater than or equal to *expression3*, and *expression1* is less than or equal to *expression2*.
