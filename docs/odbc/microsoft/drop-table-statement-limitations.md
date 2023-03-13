---
title: "DROP TABLE Statement Limitations"
description: "DROP TABLE Statement Limitations"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "ODBC SQL grammar, DROP TABLE statement limitations"
  - "DROP TABLE statement limitations [ODBC]"
---
# DROP TABLE Statement Limitations
When the Microsoft Excel 5.0, 7.0, or 97 driver is used, the DROP TABLE statement clears the worksheet but does not delete the worksheet name. Because the worksheet name still exists in the workbook, another worksheet cannot be created with the same name.
