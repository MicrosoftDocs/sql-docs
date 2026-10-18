---
title: Refresh from Database
description: Learn how to select objects to refresh from the source database in SQL Server Migration Assistant (SSMA).
author: cwebb-msft
ms.author: chweb
ms.date: 04/16/2026
ms.service: sql
ms.subservice: ssma
ms.topic: concept-article
ms.collection:
  - sql-migration-content
ai-usage: ai-assisted
---
# Refresh from Database

The **Refresh from Database** dialog box in SQL Server Migration Assistant (SSMA) lets you select which objects to refresh from the source database. Rows in the dialog box are color coded based on the state of the metadata:

- If the object metadata changed locally and in the source database, the row is *blue*.
- If the object metadata changed in the source database but not in SSMA, the row is *yellow*.
- If the object metadata changed locally, but not in the source database, the row is *green*.
- If the object is new in the source database, the row is *pink*.

You can specify default object refresh settings in the **Project Settings** dialog box.

To access the **Refresh from Database** dialog box, right-click any **database** node in the source Metadata Explorer and select **Refresh from Database**.

## Related content

- [SQL Server Migration Assistant](../sql-server-migration-assistant.md)
