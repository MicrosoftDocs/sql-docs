---
Title: "Tutorial: SQL Server Management Studio Components and Configuration"
description: A tutorial describing the components and basic configuration options for your SQL Server Management Studio environment. 
keywords: SQL Server, SSMS, SQL Server Management Studio
author: MashaMSFT
ms.author: mathoma
ms.date: 03/16/2018
ms.topic: Tutorial
ms.suite: "sql"
ms.prod: sql
ms.technology: ssms
ms.prod_service: sql-tools
ms.reviewer: sstein
manager: craigg
---

# Tutorial: SQL Server Management Studio Components and Configuration
This Tutorial describes the  different window components within SQL Server Management Studio (SSMS) and some basic configuration options for your workspace. In this article, you will learn how about: 

> [!div class="checklist"]
> * The different components that make up the SSMS environment
> * Changing the environmental layout and resetting it to default
> * Maximizing the query editor
> * Changing the font 
> * Configuring startup options 
> * Resetting the configuration back to default 

## Prerequisites
To complete this Tutorial, you need SQL Server Management Studio.  

- Install [SQL Server Management Studio](https://docs.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms).

## SQL Server Management Studio Components
This section covers the different window components available in the workspace, and their purpose. 

- Every window component can be closed by hitting the X in the corner of the title bar and then reopened from the **View** dropdown in the main menu. 

    ![View Menu](media/ssms-configuration/viewmenu.png)

- **Object Explorer** (F8): Object Explorer is a tree view of all the database objects in a server. This can include the databases of the SQL Server Database Engine, Analysis Services, Reporting Services, and Integration Services. Object Explorer includes information for all servers to which it is connected. 
    
    ![Object Explorer](media/ssms-configuration/objectexplorer.png)
- **Query Window** (Ctrl+N): Once you've clicked on **New Query**, this is the window where you will type in your Transact-SQL (T-SQL) queries. Results of your queries are visible here as well.
    
    ![New Query Window](media/ssms-configuration/newquery.png)

- **Properties** (F4): This is visible once the **Query Window** is open and displays basic properties of the query. For example, it will show the time a query started, the number of rows returned, and connection details.  

    ![Properties](media/ssms-configuration/properties.png)

- **Template Browser** (Ctrl+Alt+T): There are a number of pre-built T-SQL Templates that can be found in the template browser. These templates allow you to perform various functions such as creating or backing up a database. 

    ![Template Browser](media/ssms-configuration/templates.png)

- **Object Explorer Details**(F7): This is a more granular view of what's visible in the Object Explorer, and allows you to manipulate multiple objects at once. For example, the Object Explorer Details window allows you to select multiple databases simultaneously and either delete them or script them out. 

    ![Object Explorer Details](media/ssms-configuration/objectexplorerdetails.PNG) 
 

    

## Change the Environmental Layout 
This section discusses manipulating the environmental layout, such as moving the various windows around. 

-  Each window component can be moved around by holding down the title and dragging the window around. 
- Each window component can be pinned and unpinned by selecting the pushpin icon in the title bar:
    
    ![Pinning Objects](media/ssms-configuration/pushpin.png)

- Each window component has a drop-down arrow that allows for the window to be manipulated in various ways: 

    ![Window Options](media/ssms-configuration/windowoptions.png)

- Once you have two or more query windows open, they can be tabbed vertically or horizontally so that both query windows are visible at once. To achieve this, right-click the title of the query and select the desired tabbed option. 
 
    ![Query Tab Options](media/ssms-configuration/querytabbedoptions.png)

    - This is the **Horizontal Tab Group**:
    ![Horizontal Tab Group](media/ssms-configuration/horizontaltab.png)     
    
    - This is the **Vertical Tab Group**:  
        ![Vertical Tab Group](media/ssms-configuration/verticaltabgroup.png)
        

    - To merge the tabs back again, right-click the query title again and **Move to Next Tab Group**  or **Move to Previous Tab Group**:
    
        ![Merge Query Tabs](media/ssms-configuration/mergetabgroups.png)

- To restore the default environmental layout, click on the **Window Menu** > **Reset Window Layout**:
 
    ![Restore Window Layout](media/ssms-configuration/resetwindowlayout.png)
    
## Maximize Query Editor
The query editor can be maximized to full screen mode.

1. Click anywhere within the Query Editor Window.
2. Press SHIFT + ALT + ENTER to toggle between full-screen mode and regular mode. 

This keyboard shortcut works with any document window. 



## Change Basic Settings
This section discusses how to modify some basic settings within SSMS. These options are found within the **Tools** menu option:

  ![Tools Menu](media/ssms-configuration/tools.png)


- The highlighted toolbar can be modified by going to the menu: **Tools** > **Customize**:

    ![Customize Toolbar](media/ssms-configuration/toolbar.png)

### Change the font
- The font can be changed from the menu: **Tools** > **Options** > **Fonts and Colors**:

     ![Fonts and Colors](media/ssms-configuration/fontsandcolors.png)

### Change the Startup Options
- The startup options determine what your workspace looks like when you first launch SSMS. These can be configured from the menu: **Tools** > **Options** > **Startup**:
 
    ![Startup Options](media/ssms-configuration/startup.png)

### Reset Settings to Default
- All of these settings can be exported and imported from the menu: **Tools** > **Import and Export Settings** 

    ![Import + Export Settings](media/ssms-configuration/settings.png)
    - This is also where you can reset all of your settings to default. 


## Next steps
The next article will teach you some additional tips and tricks for using SSMS, such as finding your SQL Server error log and your SQL instance name. 

Advance to the next article to learn more
> [!div class="nextstepaction"]
> [Next steps button](ssms-tricks.md)
 
 




