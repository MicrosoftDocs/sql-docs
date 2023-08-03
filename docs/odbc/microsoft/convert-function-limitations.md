---
title: "CONVERT Function Limitations"
description: "CONVERT Function Limitations"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "ODBC SQL grammar, CONVERT function limitations"
  - "Convert function limitations [ODBC]"
---
# CONVERT Function Limitations
Type conversion failures result in the affected column being set to NULL.  
  
 Neither the DATE nor TIMESTAMP data type can be converted to another data type (or itself) by the CONVERT function.
