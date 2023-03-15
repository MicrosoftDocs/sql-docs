---
title: "DELETE Statement Limitations"
description: "DELETE Statement Limitations"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "DELETE statement limitations [ODBC]"
  - "ODBC SQL grammar, DELETE statement limitations"
---
# DELETE Statement Limitations
The DELETE statement is not supported for the Microsoft Excel or Text driver. Note that the INSERT statement is supported for the Text driver.  
  
 The dBASE driver does not support packing a table to remove "deleted" values.  
  
 For the Paradox driver to delete a row from a table, the table must have a unique index (Paradox primary key).
