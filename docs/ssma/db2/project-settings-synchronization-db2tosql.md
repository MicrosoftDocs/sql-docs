---
title: "Project Settings (Synchronization) (Db2ToSQL)"
description: The Synchronization page of the Project Settings dialog box contains settings that customize how SSMA for Db2 loads and refreshes database objects.
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
# Project Settings (Synchronization) (Db2ToSQL)

The Synchronization page of the **Project Settings** dialog box contains settings that customize how SQL Server Migration Assistant (SSMA) loads and refreshes database objects, such as tables and stored procedures, into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

The default actions options specify default settings for refreshing objects from the Db2 database and for synchronizing objects with the SQL Server database. For more information, see [Refresh from Database](refresh-from-database-db2tosql.md).

You can access two different Synchronization pages that contain the same settings:

- To specify settings for all future SSMA projects, navigate to **Tools** > **Default Project Settings**, and then select **Synchronization** at the bottom of the left pane.

- To specify settings for the current project, navigate to **Tools** > **Project Settings**, and then select **Synchronization** at the bottom of the left pane.

## Miscellaneous options

#### Attempts

Specifies the number of attempts SSMA should make when it loads objects into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Objects that aren't loaded into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] in the current attempt are tried again, until SSMA reaches the maximum number of attempts in the current synchronization process. Default value is `2`.

## Synchronization for Db2 options

#### Action on local and remote object change

Specifies the default setting in the **Synchronization** dialog box when the object definition changes in SSMA and on the database server. Default value set is **Refresh from database**.

- If you select **Refresh from Database**, SSMA loads database definitions into the metadata when the condition is met.

- If you select **Skip**, SSMA doesn't perform any refresh actions.

#### Action on local object change

Specifies the default setting in the **Synchronization** dialog box when the object changes in SSMA. Default value set is **Skip**.

- If you select **Refresh from Database**, SSMA loads database definitions into the metadata when the condition is met.

- If you select **Skip**, SSMA doesn't perform any refresh actions.

#### Action on remote object change

Specifies the default setting in the **Synchronization** dialog box when the objects change on the database server. Default value set is **Refresh from database**.

- If you select **Refresh from Database**, SSMA loads database definitions into the metadata when the condition is met.

- If you select **Skip**, SSMA doesn't perform any refresh actions.

#### Action when local object metadata is missing

Specifies the default setting in the **Synchronization** dialog box when local metadata is missing. Default value set is **Refresh from database**.

- If you select **Refresh from Database**, SSMA loads database definitions into the metadata when the condition is met.

- If you select **Skip**, SSMA doesn't perform any refresh actions.

## Synchronization for SQL Server options

#### Action on local and remote object change

Specifies the default setting in the **Synchronization** dialog box when the object definition changes in SSMA and on the database server. Default value set is **Write to database**.

- If you select **Refresh from Database**, SSMA loads database definitions into the metadata when the condition is met.

- If you select **Write to Database**, SSMA updates objects in the database according to SSMA metadata contents when the condition is met.

- If you select **Skip**, SSMA doesn't perform any refresh actions.

#### Action on local object change

Specifies the default setting in the **Synchronization** dialog box when the object changes in SSMA. Default value set is **Write to database**.

- If you select **Refresh from Database**, SSMA loads database definitions into the metadata when the condition is met.

- If you select **Write to Database**, SSMA updates the object in the database according to SSMA metadata contents when the condition is met.

- If you select **Skip**, SSMA doesn't perform any refresh actions.

#### Action on remote object change

Specifies the default setting in the **Synchronization** dialog box when the objects change on the database server. Default value set is **Refresh from database**.

- If you select **Refresh from Database**, SSMA loads database definitions into the metadata when the condition is met.

- If you select **Write to Database**, SSMA updates the object in the database according to SSMA metadata contents when the condition is met.

- If you select **Skip**, SSMA doesn't perform any refresh actions.

#### Action when local object metadata is missing

Specifies the default setting in the **Synchronization** dialog box when local metadata is missing. Default value set is **Refresh from database**.

- If you select **Refresh from Database**, SSMA selects the **Refresh from Database** option when the condition is met.

- If you select **Write to Database**, SSMA deletes the object from the database when the condition is met.

- If you select **Skip**, SSMA doesn't perform any refresh actions.
