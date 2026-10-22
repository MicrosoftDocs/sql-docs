---
title: Execute a Partial Query
description: Get help debugging a section of a complex query. Use the Transact-SQL Editor to highlight a specific script segment and execute it as a single query.
author: cwebb-msft
ms.author: chweb
ms.reviewer: vanto
ms.date: 09/09/2025
ms.service: sql
ms.subservice: ssdt
ms.topic: how-to
---

# How to: Execute a partial query

The Transact-SQL Editor allows you to highlight a specific segment of the script and execute it as a single query. This makes it easy for you to debug sections of complex queries.

## Partially execute a query

1. In **SQL Server Object Explorer**, double-click `PerishableFruits` under **Views** to open it in Transact-SQL editor.

1. Highlight the `SELECT p.Id, p.Name FROM dbo.Product p` segment in the code, right-click and select **Execute Query**.

1. All the rows with the specified fields in the `Products` table are returned in the **Results** pane.
