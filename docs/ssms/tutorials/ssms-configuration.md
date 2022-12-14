---
title: SSMS components and configuration"
description: A tutorial that describes the components and basic configuration options for your SQL Server Management Studio environment. 
ms.service: sql
ms.subservice: ssms
ms.topic: tutorial
keywords: SQL Server, SSMS, SQL Server Management Studio
author: markingmyname
ms.author: maghan
ms.reviewer: 
ms.date: 03/16/2018
---

# SQL Server Management Studio components and configuration

This tutorial describes the various window components in SQL Server Management Studio (SSMS), and some basic configuration options for your workspace. In this article, you learn how to: 

> [!div class="checklist"]
> * Identify the components that make up the SSMS environment
> * Change the environment layout, and reset it to the default
> * Maximize the query editor
> * Change the font
> * Configure startup options
> * Reset the configuration to the default

## Prerequisites

To complete this tutorial, you need SQL Server Management Studio.  

* Install [SQL Server Management Studio](../download-sql-server-management-studio-ssms.md).

## SQL Server Management Studio components

This section describes the various window components that are available in the workspace, and how to use them.

* To close a window, select the **X** in the right corner of the title bar.
* To reopen a window, select the window in the **View** menu.

    ![The View menu](media/ssms-configuration/viewmenu.png)

* **Object Explorer** (F8): Object Explorer is a tree view of all the database objects in a server. This view includes the databases of the SQL Server Database Engine, SQL Server Analysis Services, SQL Server Reporting Services, and SQL Server Integration Services. Object Explorer includes information for all servers that are connected to it. 

    ![Object Explorer](media/ssms-configuration/objectexplorer.png)
* **Query Window** (Ctrl+N): After you select **New Query**, enter your Transact-SQL (T-SQL) queries in this window. The results of your queries also appear here.

    ![New Query window](media/ssms-configuration/newquery.png)

* **Properties** (F4): You can see the Properties view when the Query Window is open. The view displays basic properties of the query. For example, it shows the time that a query started, the number of rows returned, and connection details.  

    ![Config Properties](media/ssms-configuration/properties.png)

* **Template Browser** (Ctrl+Alt+T): Template Browser has various pre-built T-SQL templates. You can use these templates to perform various functions, such as creating or backing up a database. 

    ![Template Browser](media/ssms-configuration/templates.png)

* **Object Explorer Details** (F7): This view is more granular than the view in Object Explorer. You can use Object Explorer Details to manipulate multiple objects at the same time. For example, in this window, you can select multiple databases, and then either delete them or script them out simultaneously. 

    ![Object Explorer Details](media/ssms-configuration/objectexplorerdetails.PNG) 

## Change the environment layout 

This section describes how to change the environment layout, such as how to move various windows. 

* To move a window, press and hold the title, and then drag the window. 
* To pin or unpin a window, select the pushpin icon in the title bar:

    ![Pin an object](media/ssms-configuration/pushpin.png)

* Each window component has a drop-down menu that you can use to manipulate the window in various ways: 

    ![Window options](media/ssms-configuration/windowoptions.png)

* When two or more query windows are open, the windows can be tabbed vertically or horizontally so that both query windows are visible. To view tabbed windows, right-click the title of the query, and then select the tabbed option that you want:

    ![Query tab options](media/ssms-configuration/querytabbedoptions.png)

    * Here's a Horizontal Tab Group:

      ![An example of the Horizontal Tab Group](media/ssms-configuration/horizontaltab.png)

    * Here's a Vertical Tab Group:

      ![An example of the Vertical Tab Group](media/ssms-configuration/verticaltabgroup.png)

    * To merge the tabs, right-click the query title, and then select **Move to Previous Tab Group**  or **Move to Next Tab Group**:

      ![Merge query tabs](media/ssms-configuration/mergetabgroups.png)

* To restore the default environment layout, in the **Window** menu, select **Reset Window Layout**:

    ![Restore window layout](media/ssms-configuration/resetwindowlayout.png)

## Maximize Query Editor

You can maximize Query Editor to full-screen mode:

1. Click anywhere in the Query Editor window.

2. Press Shift+Alt+Enter to toggle between full-screen mode and regular mode. 

This keyboard shortcut works with any document window. 

## Change basic settings

This section describes how to modify some basic settings in SSMS from the **Tools** menu.

  ![Tools menu](media/ssms-configuration/tools.png)

* To modify the highlighted toolbar, select **Tools** > **Customize**:

    ![Customize a toolbar](media/ssms-configuration/toolbar.png)

### Change the font

* To change the font, select **Tools** > **Options** > **Fonts and Colors**:

     ![Change fonts and colors](media/ssms-configuration/fontsandcolors.png)

### Change startup options

* Startup options determine what your workspace looks like when you first open SSMS. To change startup options, select **Tools** > **Options** > **Startup**:

    ![Change startup options](media/ssms-configuration/startup.png)

### Reset settings to the default

* You can export and import all these settings from the menu. To import or export settings, or to restore default settings, select **Tools** > **Import and Export Settings** 

    ![Import and export settings](media/ssms-configuration/settings.png)

## Next steps

The best way to get acquainted with SSMS is through hands-on practice. These *tutorial* and *how-to* articles help you with various features available within SSMS.  These articles teach you how to manage the components of SSMS and how to find the features that you use regularly.

* [Connect to and query an instance](../quickstarts/ssms-connect-query-sql-server.md)
* [Scripting](scripting-ssms.md)
* [Using Templates in SSMS](../template/templates-ssms.md)
* [Additional Tips and Tricks for using SSMS](ssms-tricks.md)