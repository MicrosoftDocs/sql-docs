---
description: "DROP TABLE Statement Limitations"
title: "DROP TABLE Statement Limitations | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "ODBC SQL grammar, DROP TABLE statement limitations"
  - "DROP TABLE statement limitations [ODBC]"
ms.assetid: 0a1c80f5-c9f2-4655-9bfd-0131b2f015a9
author: David-Engel
ms.author: v-davidengel
---
# DROP TABLE Statement Limitations
When the Microsoft Excel 5.0, 7.0, or 97 driver is used, the DROP TABLE statement clears the worksheet but does not delete the worksheet name. Because the worksheet name still exists in the workbook, another worksheet cannot be created with the same name.
