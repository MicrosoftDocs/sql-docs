---
title: "UPDATE statement limitations"
description: "UPDATE statement limitations"
author: David-Engel
ms.author: v-davidengel
ms.reviewer: randolphwest
ms.date: 12/14/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "UPDATE statement limitations [ODBC]"
  - "ODBC SQL grammar, UPDATE statement limitations"
---
# UPDATE statement limitations

For the Paradox driver to update a table, the table must have a unique index (Paradox primary key). When you use the Paradox driver without implementing the Borland Database Engine, it's not possible to update a Paradox table.

Not supported by the Text driver.

When the Microsoft Excel driver is used, it's possible to update values, but a row can't be deleted from a table based on a Microsoft Excel spreadsheet. As a result, the UPDATE statement isn't considered officially supported by the Microsoft Excel driver. Only the `INSERT` statement is considered supported.
