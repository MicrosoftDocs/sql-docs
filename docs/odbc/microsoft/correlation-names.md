---
title: "Correlation Names"
description: "Correlation Names"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "correlation names [ODBC]"
  - "SQL grammar [ODBC], correlation names"
---
# Correlation Names
Correlation names are fully supported, including within the table list. For example, in the following string, E1 is the correlation name for the table named Emp:  
  
```  
SELECT * FROM Emp E1   
WHERE E1.LastName = 'Smith'  
```
