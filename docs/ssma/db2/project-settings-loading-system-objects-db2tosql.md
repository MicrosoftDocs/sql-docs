---
title: Project Settings (Loading System Objects) (Db2ToSQL)
description: Project Settings specify which Db2 system objects SSMA converts and loads into SQL Server.
author: cpichuka
ms.author: cpichuka
ms.reviewer: randolphwest
ms.date: 09/24/2024
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
f1_keywords:
  - "ssma.db2.projectsettingsloadingobjects.f1"
---
# Project Settings (Loading System Objects) (Db2ToSQL)

The Loading System Objects page of the **Project Settings** dialog box lets you specify which Db2 system objects SQL Server Migration Assistant (SSMA) converts and loads into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

The Loading System Objects pane is available in the **Project Settings** and **Default Project Settings** dialog boxes:

- To specify settings for all SSMA projects, navigate to **Tools** > **Default Project Settings**, and select the migration project type for which settings are required to be viewed or changed. From the **Migration Target Version** dropdown list, select **General** at the bottom of the left pane, and then select **Loading System Objects**.

- To specify settings for the current project, navigate to **Tools** > **Project Settings**, select **General** at the bottom of the left pane, and then select **Loading System Objects**.

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

If your Db2 objects refer to extra system objects, you should select those objects. If you don't select the system objects referenced by your Db2 database objects, SSMA reports conversion errors. If you receive conversion errors caused by missing system objects, select the missing objects in this dialog box. You can then repeat the conversion as necessary.
