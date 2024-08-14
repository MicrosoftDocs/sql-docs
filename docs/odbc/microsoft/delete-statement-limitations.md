---
title: "DELETE statement limitations"
description: "DELETE statement limitations"
author: David-Engel
ms.author: davidengel
ms.reviewer: randolphwest
ms.date: 12/14/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "DELETE statement limitations [ODBC]"
  - "ODBC SQL grammar, DELETE statement limitations"
---
# DELETE statement limitations

The `DELETE` statement isn't supported for the Microsoft Excel or Text driver. The `INSERT` statement is supported for the Text driver.

The dBASE driver doesn't support packing a table to remove "deleted" values.

For the Paradox driver to delete a row from a table, the table must have a unique index (Paradox primary key).
