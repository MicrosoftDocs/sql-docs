---
title: "Default Project Settings (Loading System Objects) (OracleToSQL)"
description: "Default Project Settings (Loading System Objects) (OracleToSQL)"
author: nilabjaball
ms.author: niball
ms.reviewer: vanto
ms.date: 07/21/2025
ms.service: sql
ms.subservice: ssma
ms.topic: concept-article
ms.collection:
  - sql-migration-content
f1_keywords:
  - "ssma.oracle.projectsettingloadingobject.f1"
---
# Default Project Settings (Loading System Objects) (OracleToSQL)

The Loading System Objects page of the **Default Project Settings** dialog box lets you specify which Oracle system objects SQL Server Migration Assistant (SSMA) converts and loads into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

The Loading System Objects pane is available in the **Default Project Settings** dialog box:

- To specify settings for all SSMA projects, on the **Tools** menu, select **Default Project Settings**, select the migration project type for the settings you want to view or change from the **Migration Target Version** dropdown list, select **General** at the bottom of the left pane, and then select **Loading System Objects**.

## Default settings

Converting system objects consumes system resources and takes time. To improve performance, SSMA selects only the most frequently used system objects, as shown in the following list:

- `SYS.DBMS_OUTPUT`
- `SYS.DBMS_PIPE`
- `SYS.DBMS_UTILITY`
- `SYS.STANDARD`
- `SYS.UTL_FILE`
- `SYS.DBMS_LOB`
- `SYS.DBMS_SQL`
- `SYS.DBMS_SESSION`

If your Oracle objects refer to other system objects, you should select those objects. If you don't select the system objects that are referenced by your Oracle database objects, SSMA reports conversion errors. If you receive conversion errors caused by missing system objects, select the missing objects in this dialog box. You can then repeat the conversion as necessary.
