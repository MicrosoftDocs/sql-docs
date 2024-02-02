---
title: "ALTER TABLE statement limitations"
description: "ALTER TABLE statement limitations"
author: David-Engel
ms.author: v-davidengel
ms.reviewer: randolphwest
ms.date: 12/14/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "ODBC SQL grammar, ALTER TABLE statement limitations"
  - "ALTER TABLE statement limitations [ODBC]"
---
# ALTER TABLE statement limitations

When the dBASE or Paradox driver is used, once an index is created and a new record added, the structure of the table can't be changed by the `ALTER TABLE` statement unless the index is dropped and the contents of the table are deleted.

`ALTER TABLE` statements aren't supported for the Microsoft Excel or Text drivers.

> [!NOTE]  
> When you use the Paradox driver without implementing the Borland Database Engine, `ALTER TABLE` statements aren't supported; only read and append statements are allowed.
