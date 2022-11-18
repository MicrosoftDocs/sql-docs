---
title: Tips and tricks using SSMS
description: Learn to comment & uncomment code, indent text, filter objects, access error logs, & find SQL Server instance names with SQL Server Management Studio.
ms.service: sql
ms.subservice: ssms
ms.topic: tutorial
author: markingmyname
ms.author: maghan
ms.reviewer: 
helpviewer_keywords:
  - "source controls [SQL Server Management Studio], tutorials"
  - "Help [SQL Server], SQL Server Management Studio"
  - "tutorials [SQL Server Management Studio]"
  - "Transact-SQL tutorials"
  - "SQL Server Management Studio [SQL Server], tutorials"
  - "Find SQL Server Instance"
  - "find instance name"
  - "find sql server instance name"
ms.custom: seo-lt-2019
ms.date: 03/13/2018
---

# Tips and tricks for using SQL Server Management Studio (SSMS)

This article gives you some tips and tricks for using SQL Server Management Studio (SSMS). This article shows you how to: 

> [!div class="checklist"]
> * Comment/uncomment your Transact-SQL (T-SQL) text
> * Indent your text
> * Filter objects in Object Explorer
> * Access your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log
> * Find the name of your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance

## Prerequisites

To test out the steps provided in this article, you need SQL Server Management Studio, access to a SQL server, and an AdventureWorks database. 

* Install [SQL Server Management Studio](../download-sql-server-management-studio-ssms.md).
* Install [[!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] Developer Edition](https://www.microsoft.com/sql-server/sql-server-downloads).
* Download an [AdventureWorks sample database](https://github.com/Microsoft/sql-server-samples/releases). To learn how to restore a database in SSMS, see [Restoring a database](../../relational-databases/backup-restore/restore-a-database-backup-using-ssms.md). 

## Comment/uncomment your T-SQL code

You can comment and uncomment portions of your text by using the **Comment** button on the toolbar. Text that is commented out is not executed.

1. Open SQL Server Management Studio.

2. Connect to your SQL server.

3. Open a New Query window.

4. Paste the following [!INCLUDE[tsql](../../includes/tsql-md.md)] code in your text window.

    ```sql
    USE master
        GO

        -- Drop the database if it already exists
        IF  EXISTS (
            SELECT name 
                FROM sys.databases 
                WHERE name = N'TutorialDB'
                )

        DROP DATABASE TutorialDB
        GO

        CREATE DATABASE TutorialDB
        GO

        ALTER DATABASE [TutorialDB] SET QUERY_STORE=ON
        GO
   ```

5. Highlight the **Alter Database** portion of the text, and then select the **Comment** button on the toolbar: 

    ![The Comment button](media/ssms-tricks/comment.png)
6. Select **Execute** to run the uncommented portion of the text. 

7. Highlight everything except for the **Alter Database** command, and then select the **Comment** button:

    ![Comment everything](media/ssms-tricks/commenteverything.png)

    > [!NOTE]
    > The keyboard shortcut to comment text is **CTRL + K, CTRL + C**.

8. Highlight the **Alter Database** portion of the text, and then select the **Uncomment** button to uncomment it:

    ![Uncomment text](media/ssms-tricks/uncomment.png)

    > [!NOTE]
    > The keyboard shortcut to uncomment text is **CTRL + K, CTRL + U**.

9. Select **Execute** to run the uncommented portion of the text.

## Indent your text

You can use the indentation buttons on the toolbar to increase or decrease the indent of your text.

1. Open a New Query window.

2. Paste the following [!INCLUDE[tsql](../../includes/tsql-md.md)] code in your text window:

    ```sql
    USE master
      GO

      --Drop the database if it already exists
      IF  EXISTS (
    	    SELECT name
    		    FROM sys.databases
    		    WHERE name = N'TutorialDB'
              )

      DROP DATABASE TutorialDB
      GO

      CREATE DATABASE TutorialDB
      GO

      ALTER DATABASE [TutorialDB] SET QUERY_STORE=ON
      GO
    ```

3. Highlight the **Alter Database** portion of the text, and then select the **Increase Indent** button on the toolbar to move this text forward:

    ![Increase the indent](media/ssms-tricks/increaseindent.png)

4. Highlight the **Alter Database** portion of the text again, and then select the **Decrease Indent** button to move this text back.

    ![Decrease the indent](media/ssms-tricks/decreaseindent.png)

## Filter objects in Object Explorer

In databases that have many objects, you can use filtering to search for specific tables, views, etc. This section describes how to filter tables, but you can use the following steps in any other node in Object Explorer:

1. Connect to your SQL server.

2. Expand **Databases** > **AdventureWorks** > **Tables**. All the tables in the database appear.

3. Right-click **Tables**, and then select **Filter** > **Filter Settings**:

    ![Filter settings](media/ssms-tricks/filtersettings.png)

4. In the **Filter Settings** window, you can modify some of the following filter settings:
    * Filter by name: 

      ![Filter by name](media/ssms-tricks/filterbyname.png)

    * Filter by schema: 

      ![Filter by schema](media/ssms-tricks/filterbyschema.png)

5. To clear the filter, right-click **Tables**, and then select **Remove Filter**.

    ![Remove filter](media/ssms-tricks/removefilter.png)

## Access your SQL Server error log

The error log is a file that contains details about things that occur in your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. You can browse and query the error login SSMS. The error log is a .log file that's located on your disk.

### Open the error log in SSMS

1. Connect to your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  

2. Expand **Management** > **SQL Server Logs**. 

3. Right-click the **Current** error log, and then select **View SQL Server Log**:

    ![View the error log in SSMS](media/ssms-tricks/viewerrorloginssms.png)

### Query the error log in SSMS

1. Connect to your SQL server.

2. Open a New Query window.

3. Paste the following [!INCLUDE[tsql](../../includes/tsql-md.md)] code in your query window:

     ```sql
       sp_readerrorlog 0,1,'Server process ID'
      ```

4. Modify the text in the single quotes to text you want to search for.

5. Execute the query, and then review the results:

    ![Query the error log](media/ssms-tricks/queryerrorlog.png)

### Find the error log location if you're connected to SQL Server

1. Connect to your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

2. Open a New Query window.

3. Paste the following [!INCLUDE[tsql](../../includes/tsql-md.md)] code in your query window, and then select **Execute**:

     ```sql
        SELECT SERVERPROPERTY('ErrorLogFileName') AS 'Error log file location'  
      ```

4. The results show the location of the error log in the file system: 

    ![Find the error log by query](media/ssms-tricks/finderrorlogquery.png)

### Find the error log location if you can't connect to SQL Server

The path for your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log can vary depending on your configuration settings. The path for the error log location can be found in the startup parameters within the SQL Server Configuration Manager. Follow the steps below to locate the relevant startup parameter identifying the location of your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log. *Your path may vary from the path indicated below*.

1. Open SQL Server Configuration Manager.

2. Expand **Services**.

3. Right-click your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance, and then select **Properties**:

    ![Configuration Manager server properties](media/ssms-tricks/serverproperties.PNG)

4. Select the **Startup Parameters** tab.

5. In the **Existing Parameters** area, the path after "-e" is the location of the error log: 

    ![Error log](media/ssms-tricks/errorlog.png)

    There are several error log files in this location. The file name that ends with *.log is the current error log file. File names that end with numbers are previous log files. A new log is created every time the SQL server restarts.

6. Open the errorlog.log file in Notepad.

## Find SQL Server instance name

You have a few options for finding the name of your SQL server before and after you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  

### Before you connect to SQL Server

1. Follow the steps to locate the [SQL Server error log on disk](#find-the-error-log-location-if-you-cant-connect-to-sql-server). Your path may vary from the path in the image below.

2. Open the errorlog.log file in Notepad.

3. Search for the text *Server name is*.

    Whatever is listed in the single quotes is the name of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance that you'll be connecting to:

    ![Find the server name in the error log](media/ssms-tricks/servernameinlog.png)

    The format of the name is HOSTNAME\INSTANCENAME. If you see only the host name, then you've installed the default instance and your instance name is MSSQLSERVER. When you connect to a default instance, the host name is all you need to enter to connect to your SQL server.  

### When you're connected to SQL Server

When you're connected to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can find the server name in three locations: 

1. The name of the server is listed in Object Explorer:

    ![SQL Server instance name in Object Explorer](media/ssms-tricks/nameinobjectexplorer.png)
2. The name of the server is listed in the Query window:

    ![SQL Server instance name in the Query window](media/ssms-tricks/nameinquerywindow.png)
3. The name of the server is listed in **Properties**.
    * In the **View** menu, select **Properties Window**:

      ![SQL Server instance name in the Properties window](media/ssms-tricks/nameinproperties.png)

### If you're connected to an alias or Availability Group listener

If you're connected to an alias or to an Availability Group listener, that information appears in Object Explorer and Properties. In this case, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] name might not be readily apparent, and must be queried:

1. Connect to your SQL server.

2. Open a New Query window.

3. Paste the following [!INCLUDE[tsql](../../includes/tsql-md.md)] code in the window:

      ```sql
       select @@Servername
     ```

4. View the results of the query to identify the name of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance you're connected to: 

    ![Query the SQL Server name](media/ssms-tricks/queryservername.png)

## Next steps

The best way to get acquainted with SSMS is through hands-on practice. These *tutorial* and *how-to* articles help you with various features available within SSMS.  These articles teach you how to manage the components of SSMS and how to find the features that you use regularly.

* [Connect to and query an instance](../quickstarts/ssms-connect-query-sql-server.md)
* [Scripting](scripting-ssms.md)
* [Using Templates in SSMS](../template/templates-ssms.md)
* [SSMS Configuration](ssms-configuration.md)