---
description: "DELETE Statement Limitations"
title: "DELETE Statement Limitations | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "DELETE statement limitations [ODBC]"
  - "ODBC SQL grammar, DELETE statement limitations"
ms.assetid: 084761fe-e65b-4f38-ba4f-69884b2a7700
author: David-Engel
ms.author: v-davidengel
---
# DELETE Statement Limitations
The DELETE statement is not supported for the Microsoft Excel or Text driver. Note that the INSERT statement is supported for the Text driver.  
  
 The dBASE driver does not support packing a table to remove "deleted" values.  
  
 For the Paradox driver to delete a row from a table, the table must have a unique index (Paradox primary key).
