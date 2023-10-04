---
title: "UPDATE Statement Limitations"
description: "UPDATE Statement Limitations"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "UPDATE statement limitations [ODBC]"
  - "ODBC SQL grammar, UPDATE statement limitations"
---
# UPDATE Statement Limitations
For the Paradox driver to update a table, the table must have a unique index (Paradox primary key). When you use the Paradox driver without implementing the Borland Database Engine, it is not possible to update a Paradox table.  
  
 Not supported by the Text driver.  
  
 When the Microsoft Excel driver is used, it is possible to update values, but a row cannot be deleted from a table based on a Microsoft Excel spreadsheet. As a result, the UPDATE statement is not considered officially supported by the Microsoft Excel driver. Only the INSERT statement is considered supported.
