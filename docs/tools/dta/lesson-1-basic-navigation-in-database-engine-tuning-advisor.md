---
title: "Basic Navigation in DTA"
description: Database Engine Tuning Advisor (DTA) provides a graphical user interface (GUI) based way to view tuning sessions and tuning recommendation reports.
author: thesqlsith
ms.author: derekw
ms.date: 01/02/2026
ms.service: sql
ms.subservice: performance
ms.topic: how-to
ms.collection:
  - data-tools
helpviewer_keywords:
  - "Database Engine Tuning Advisor [SQL Server], tutorials"
---

# Lesson 1: Basic navigation in Database Engine Tuning Advisor (DTA)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Database Engine Tuning Advisor provides a graphical user interface (GUI) that you can use to view tuning sessions and tuning recommendation reports. This lesson shows you how to start the tool and how to configure the display. By the end of this lesson, you know the different ways you can start the tool and how to configure its display to support the tuning tasks that you regularly perform.

## Prerequisites

To complete this tutorial, you need SQL Server Management Studio, access to a server that's running SQL Server, and an [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database.

- Install [SQL Server Management Studio](/ssms/install/install).
- Install [SQL Server 2022 Developer Edition](https://www.microsoft.com/sql-server/sql-server-downloads).
- Download [AdventureWorks sample databases](../../samples/adventureworks-install-configure.md).

For instructions on restoring databases in SSMS, see [Restore a Database Backup Using SSMS](../../relational-databases/backup-restore/restore-a-database-backup-using-ssms.md).

  > [!NOTE]  
  > This tutorial is meant for a user familiar with using SQL Server Management Studio and basic database administration tasks.

## Launch Database Engine Tuning Advisor

To begin, open the Database Engine Tuning Advisor (DTA) graphical user interface (GUI). On first use, a member of the **sysadmin** fixed server role must launch Database Engine Tuning Advisor to initialize the application. After initialization, members of the **db_owner** fixed database role can use Database Engine Tuning Advisor to tune databases that they own. For more information about initializing Database Engine Tuning Advisor, see [Start and use the Database Engine Tuning Advisor](../../relational-databases/performance/start-and-use-the-database-engine-tuning-advisor.md).

1. Start SQL Server Management Studio (SSMS). On the Windows **Start Menu**, point to **All Programs** and locate **SQL Server Management Studio**.
1. Once SSMS is open, select the **Tools** menu and select **Database Engine Tuning Advisor**.

:::image type="content" source="media/dta-tutorials/launch-dta.png" alt-text="Screenshot of launch DTA from SSMS.":::

1. Database Tuning Advisor launches, and opens the **Connect to Server** dialog box. Verify the default settings, and then select **Connect** to connect to your SQL Server.

By default, Database Engine Tuning Advisor opens to the configuration in the following illustration:

:::image type="content" source="media/dta-tutorials/dta-default-gui.png" alt-text="Screenshot of Database Engine Tuning Advisor default window." lightbox="media/dta-tutorials/dta-default-gui.png":::

> [!NOTE]  
> The **Session Monitor** tab displays the session name, which is the name of the connected user and current date.

Two main panes appear in the Database Engine Tuning Advisor GUI when you first open it.

- The left pane contains the Session Monitor, which lists all tuning sessions that you performed on this [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance. When you open Database Engine Tuning Advisor, it displays a new session at the top of the pane. You can name this session in the adjacent pane. Initially, only a default session appears. This is the default session that Database Engine Tuning Advisor automatically creates for you. After you tune databases, all tuning sessions for the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance to which you're connected appear below the new session. You can right-click a tuning session to rename it, close it, delete it, or clone it. If you right-click in the list, you can sort the sessions by name, status, or creation time, or create a new session. In the bottom section of this pane, details of the selected tuning session are displayed.

  You can choose to display the details organized into categories by using the **Categorized** button, or you can display them in an alphabetized list by using the **Alphabetical** button. You can also hide Session Monitor by dragging the right pane border to the left side of the window. To view it again, drag the pane border back to the right. Session Monitor enables you to view previous tuning sessions, or to use them to create new sessions with similar definitions. You can also use Session Monitor to evaluate tuning recommendations. For more information, see [View and Work with the Output from the Database Engine Tuning Advisor (DTA)](../../relational-databases/performance/view-and-work-with-the-output-from-the-database-engine-tuning-advisor.md). Use the **Back** button on your browser to return to this tutorial.

- The right pane contains the **General** and the **Tuning Options** tabs. This is where you define your Database Engine Tuning session. In the **General** tab, you type the name for your tuning session, specify the workload file or table to use, and select the databases and tables you want to tune in this session. A workload is a set of [!INCLUDE [tsql](../../includes/tsql-md.md)] statements that execute against a database or databases that you want to tune.

    Database Engine Tuning Advisor uses trace files, trace tables, [!INCLUDE [tsql](../../includes/tsql-md.md)] scripts, or XML files as workload input when tuning databases. On the **Tuning Options** tab, you can select the physical database design structures (indexes or indexed views) and the partitioning strategy that you want Database Engine Tuning Advisor to consider during its analysis. On this tab, you also specify the maximum time that Database Engine Tuning Advisor takes to tune a workload. By default, Database Engine Tuning Advisor tunes a workload for one hour.

> [!NOTE]  
> Database Engine Tuning Advisor can take XML files as input when a [!INCLUDE [tsql](../../includes/tsql-md.md)] script is imported from [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] Query Editor. For more information, see the section on launching Database Engine Tuning Advisor from the [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] Query Editor in [Start and use the Database Engine Tuning Advisor](../../relational-databases/performance/start-and-use-the-database-engine-tuning-advisor.md).

## Configure tool options and layout

1. On the **Tools** menu, select **Options**.

   :::image type="content" source="media/dta-tutorials/dta-settings.png" alt-text="Screenshot of DTA Options." lightbox="media/dta-tutorials/dta-settings.png":::

1. In the **Options** dialog box, view the following options:

   - Expand the **On startup** list to view what Database Engine Tuning Advisor can display when it starts. By default, **Show a new session** is selected.

   - Select **Change Font** to see what fonts you can choose for the lists of databases and tables on the **General** tab. The fonts you choose for this option are also used in Database Engine Tuning Advisor recommendation grids and reports after you perform tuning. By default, Database Engine Tuning Advisor uses system fonts.

   - Set the **Number of items in most recently used lists** between `1` and `10`. This number sets the maximum number of items in the lists displayed by selecting **Recent Sessions** or **Recent Files** on the **File** menu. By default, this option is set to `4`.

   - When **Remember my last tuning options** is checked, Database Engine Tuning Advisor uses the tuning options you specified for your last tuning session for the next tuning session. Clear this check box to use Database Engine Tuning Advisor tuning option defaults. By default, this option is selected.

     - By default, **Ask before permanently deleting sessions** is checked to avoid accidentally deleting tuning sessions.

     - By default, **Ask before stopping session analysis** is checked to avoid accidentally stopping a tuning session before Database Engine Tuning Advisor finishes analyzing a workload.

## Next step

> [!div class="nextstepaction"]
> [Lesson 2: Using Database Engine Tuning Advisor](lesson-2-using-database-engine-tuning-advisor.md)
