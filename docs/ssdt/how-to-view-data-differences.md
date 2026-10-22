---
title: View Data Differences
description: Find out how to compare two databases and then see how their database objects differ. See how to view records within objects and how to filter the view.
author: cwebb-msft
ms.author: chweb
ms.reviewer: vanto
ms.date: 09/09/2025
ms.service: sql
ms.subservice: ssdt
ms.topic: how-to
f1_keywords:
  - "sql.data.tools.datacompare.f1"
---

# How to: View data differences

After you compare the data in two databases, you see each *database object* that you compared and its status. You can also view results for the records within each object, grouped by status.

After you view the differences, you can update the *target* to match the *source* for some or all of the objects or records that are different, missing, or new.

## View data differences

1. Compare the data in a source and a target.

1. (Optional) Do one or both of the following steps:

   - By default, the results for all objects appear, regardless of their status. To display only those objects that have a particular status, select an option in the **Filter** list.

   - To view results for records within a particular object, select the object in the main results pane, and then select a tab in the Records View pane. Each tab displays all records within that object that have a particular status: different, only in source, only in target, and identical. Data appears by record and column.

## Related content

- [Schema comparison overview](../tools/sql-database-projects/concepts/schema-comparison.md)
