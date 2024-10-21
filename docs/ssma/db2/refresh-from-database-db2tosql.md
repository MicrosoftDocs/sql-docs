---
title: "Refresh from Database (Db2ToSQL)"
description: Select which objects to refresh from the Db2 database in SSMA for Db2.
author: cpichuka
ms.author: cpichuka
ms.reviewer: randolphwest
ms.date: 09/24/2024
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
---
# Refresh from Database (Db2ToSQL)

The **Refresh from Database** dialog box lets you select which objects to refresh from the Db2 database. Rows in the dialog box are color coded based on the state of the metadata:

- If the object metadata changed locally and in the Db2 database, the row is blue.

- If the object metadata changed in the Db2 database but not in _SQL Server Migration Assistant (SSMA), the row is yellow.

- If the object metadata changed locally, but not in the Db2 database, the row is green.

- If the object is new in the Db2 database, the row is pink.

You can specify default object refresh settings in the **Project Settings** dialog box. For more information, see [Project Settings(Synchronization)](project-settings-synchronization-db2tosql.md).

To access the **Refresh from Database** dialog box, right-click an object in Db2 Metadata Explorer and select **Refresh from Database**.

## Options

#### Collapse (-)

Collapse all object groups to hide individual objects.

#### Expand (+)

Expand all object groups to show individual objects.

#### Hide/Show Equal Objects

Hides objects from the list if the object metadata is the same in the Db2 database and in SSMA.

#### Refresh from Database (arrow button)

Use the arrow button to specify that the metadata for the selected objects should be updated in SSMA.

#### Do Not Refresh from Database (X button)

Use the X button to specify that the metadata for the selected objects shouldn't be updated in SSMA.

#### Legend

Displays a **Legend** dialog box. The legend contains the mapping between row colors and metadata states.

To keep the **Legend** dialog box on top of the **Refresh from Database** dialog box, select the **Show on top** check box.
