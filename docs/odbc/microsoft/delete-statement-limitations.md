---
title: "DELETE Statement Limitations | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "DELETE statement limitations [ODBC]"
  - "ODBC SQL grammar, DELETE statement limitations"
ms.assetid: 084761fe-e65b-4f38-ba4f-69884b2a7700
author: MightyPen
ms.author: genemi
manager: craigg
---
# DELETE Statement Limitations
The DELETE statement is not supported for the Microsoft Excel or Text driver. Note that the INSERT statement is supported for the Text driver.  
  
 The dBASE driver does not support packing a table to remove "deleted" values.  
  
 For the Paradox driver to delete a row from a table, the table must have a unique index (Paradox primary key).
