---
title: " SQL Server Options page - Environment - Startup | Microsoft Docs"
ms.date: 11/05/2018
ms.prod: sql
ms.prod_service: "sql-tools"
ms.technology: ssms
ms.topic: conceptual
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Options (Environment - Startup page)

[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

Use the **Options** dialog box to configure [!INCLUDE[msCoName](../../includes/msconame_md.md)] [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] startup actions, general window management options, and other general settings. On the **Tools** menu, click **Options**, expand the **Environment** folder, and then click **Startup**.

## UIElement List

**At startup**

Select the action for [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to perform when it's started. The options are:

- **Open Object Explorer** prompts for a connection and then opens Object Explorer.

- **Open new query window** prompts for a connection and then opens SQL Query Editor.

- **Open Object Explorer and query window** prompts for a connection, then opens both Object Explorer and SQL Query Editor with that connection.

- **Open Object Explorer and Activity Monitor**

- **Open empty environment** opens [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] without a SQL Query editor window and without connecting Object Explorer to a server.

**Hide system objects in Object Explorer**

Select this check box to remove the system databases, system tables, system views, and system stored procedures from tree view in Object Explorer. System functions and system data types are not hidden. This option only applies to instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and does not affect servers already connected in Object Explorer.

## See also

- [Options Dialog Boxes F1 Help](options-dialog-boxes-f1-help.md)
- [Options (Environment - General Page)](options-environment-general-page.md)
