---
title: "BETWEEN Predicate"
description: "BETWEEN Predicate"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "BETWEEN predicate [ODBC]"
  - "SQL grammar [ODBC], between predicate"
---
# BETWEEN Predicate
The syntax:  
  
```  
expression1 BETWEEN expression2 AND expression3  
```  
  
 returns true only if *expression1* is greater than or equal to *expression2* and *expression1* is less than or equal to *expression3*.  
  
 The semantics of this syntax are different for the Desktop Database Drivers and the Microsoft Jet engine. In Microsoft Jet SQL, *expression2* can be greater than *expression3* so that the statement will return TRUE only if *expression1* is greater than or equal to *expression3*, and *expression1* is less than or equal to *expression2*.
