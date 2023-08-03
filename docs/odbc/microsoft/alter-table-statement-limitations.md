---
title: "ALTER TABLE Statement Limitations"
description: "ALTER TABLE Statement Limitations"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "ODBC SQL grammar, ALTER TABLE statement limitations"
  - "ALTER TABLE statement limitations [ODBC]"
---
# ALTER TABLE Statement Limitations
When the dBASE or Paradox driver is used, once an index has been created and a new record added, the structure of the table cannot be changed by the ALTER TABLE statement unless the index is dropped and the contents of the table are deleted.  
  
 ALTER TABLE statements are not supported for the Microsoft Excel or Text drivers.  
  
> [!NOTE]  
>  When you use the Paradox driver without implementing the Borland Database Engine, ALTER TABLE statements are not supported; only read and append statements are allowed.
