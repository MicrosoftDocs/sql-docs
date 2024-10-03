---
title: Tips and tricks using SSMS
description: Learn to comment & uncomment code, indent text, filter objects, access error logs, & find SQL Server instance names with SQL Server Management Studio.
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan, randolphwest, mathoma
ms.date: 09/27/2024
ms.service: sql
ms.subservice: ssms
ms.topic: tutorial
helpviewer_keywords:
  - "source controls [SQL Server Management Studio], tutorials"
  - "Help [SQL Server], SQL Server Management Studio"
  - "tutorials [SQL Server Management Studio]"
  - "Transact-SQL tutorials"
  - "SQL Server Management Studio [SQL Server], tutorials"
  - "Find SQL Server Instance"
  - "find instance name"
  - "find sql server instance name"
---

# Tips and tricks for using SQL Server Management Studio (SSMS)

This article includes some tips and tricks for using SQL Server Management Studio (SSMS). This article shows you how to:

> [!div class="checklist"]
>  
> * Comment/uncomment your Transact-SQL (T-SQL) text
> * Indent your text
> * Filter objects in Object Explorer
> * Access your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] error log
> * Find the name of your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance

## Prerequisites

To test the steps provided in this article, you need SQL Server Management Studio, access to an instance of  SQL Server and an AdventureWorks database.

- Install [SQL Server Management Studio](../download-sql-server-management-studio-ssms.md).
- Install [SQL Server Developer Edition](https://www.microsoft.com/sql-server/sql-server-downloads).
- Download an [AdventureWorks sample database](https://github.com/Microsoft/sql-server-samples/releases). To learn how to restore a database in SSMS, see [Restoring a database](../../relational-databases/backup-restore/restore-a-database-backup-using-ssms.md).

## Comment/uncomment your T-SQL code

You can comment and uncomment portions of your text by using the **Comment** button on the toolbar. Text that is commented out isn't executed.

1. Open SQL Server Management Studio.

1. Connect to your SQL Server instance.

1. Open a New Query window.

1. Paste the following [!INCLUDE [tsql](../../includes/tsql-md.md)] code in your text window.

    ```sql
    USE master;
    GO

    --Drop the database if it already exists
    IF EXISTS (SELECT name
               FROM sys.databases
               WHERE name = N'TutorialDB')
        DROP DATABASE TutorialDB;
    GO

    CREATE DATABASE TutorialDB;
    GO

    ALTER DATABASE [TutorialDB]
        SET QUERY_STORE = ON;
    GO
    ```

1. Highlight the **Alter Database** portion of the text, and then select the **Comment** button on the toolbar:

    :::image type="content" source="media/ssms-tricks/comment.png" alt-text="Screenshot of The Comment button." lightbox="media/ssms-tricks/comment.png":::

1. Select **Execute** to run the uncommented portion of the text.

1. Highlight everything except for the **Alter Database** command, and then select the **Comment** button:

    :::image type="content" source="media/ssms-tricks/comment-everything.png" alt-text="Screenshot of Comment everything." lightbox="media/ssms-tricks/comment-everything.png":::

    > [!NOTE]  
    > The keyboard shortcut to comment text is **CTRL + K, CTRL + C**.

1. Highlight the **Alter Database** portion of the text, and then select the **Uncomment** button to uncomment it:

    :::image type="content" source="media/ssms-tricks/uncomment.png" alt-text="Screenshot of Uncomment text." lightbox="media/ssms-tricks/uncomment.png":::

    > [!NOTE]  
    > The keyboard shortcut to uncomment text is **CTRL + K, CTRL + U**.

1. Select **Execute** to run the uncommented portion of the text.

## Indent your text

You can use the indentation buttons on the toolbar to increase or decrease the indent of your text.

1. Open a New Query window.

1. Paste the following [!INCLUDE [tsql](../../includes/tsql-md.md)] code in your text window:

    ```sql
    USE master;
    GO
    --Drop the database if it already exists
    IF EXISTS (SELECT name
               FROM sys.databases
               WHERE name = N'TutorialDB')
        DROP DATABASE TutorialDB;
    GO

    CREATE DATABASE TutorialDB;
    GO

    ALTER DATABASE [TutorialDB]
        SET QUERY_STORE = ON;
    GO
    ```

1. Highlight the **Alter Database** portion of the text, and then select the **Increase Indent** button on the toolbar to move the highlighted text forward:

    :::image type="content" source="media/ssms-tricks/increase-indent.png" alt-text="Screenshot of Increase the indent." lightbox="media/ssms-tricks/increase-indent.png":::

1. Highlight the **Alter Database** portion of the text again, and then select the **Decrease Indent** button to move the highlighted text back.

    :::image type="content" source="media/ssms-tricks/decrease-indent.png" alt-text="Screenshot of Decrease the indent." lightbox="media/ssms-tricks/decrease-indent.png":::

## Filter objects in Object Explorer

In databases that have many objects, you can use filtering to search for specific tables, views, etc. This section describes how to filter tables, but you can use the following steps in any other nodes in Object Explorer:

1. Connect to your SQL Server instance.

1. Expand **Databases** > **AdventureWorks** > **Tables**. All the tables in the database appear.

1. Right-click **Tables**, and then select **Filter** > **Filter Settings**:

    :::image type="content" source="media/ssms-tricks/filter-settings.png" alt-text="Screenshot of Filter settings." lightbox="media/ssms-tricks/filter-settings.png":::

1. In the **Filter Settings** window, you can modify some of the following filter settings:
1. 
    * Filter by name:

      :::image type="content" source="media/ssms-tricks/filter-by-name.png" alt-text="Screenshot of Filter by name." lightbox="media/ssms-tricks/filter-by-name.png":::

    * Filter by schema:

      :::image type="content" source="media/ssms-tricks/filter-by-schema.png" alt-text="Screenshot of Filter by schema." lightbox="media/ssms-tricks/filter-by-schema.png":::

1. To clear the filter, right-click **Tables**, and then select **Remove Filter**.

    :::image type="content" source="media/ssms-tricks/remove-filter.png" alt-text="Screenshot of Remove filter." lightbox="media/ssms-tricks/remove-filter.png":::

## Access your SQL Server error log

The error log is a file that contains details about things that occur in your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance. You can browse and query the error login SSMS. The error log is a .log file that exists in your file system.

### Open the error log in SSMS

1. Connect to your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance.

1. Expand **Management** > **SQL Server Logs**.

1. Right-click the **Current** error log, and then select **View SQL Server Log**:

    :::image type="content" source="media/ssms-tricks/view-error-log-in-ssms.png" alt-text="Screenshot of View the error log in SSMS." lightbox="media/ssms-tricks/view-error-log-in-ssms.png":::

### Query the error log in SSMS

1. Connect to your SQL Server instance.

1. Open a **New Query** window.

1. Paste the following [!INCLUDE [tsql](../../includes/tsql-md.md)] code in your query window:

     ```sql
     EXECUTE sp_readerrorlog 0, 1,'Server process ID'
     ```

1. Modify the text in the single quotes to text you want to search for.

1. Execute the query, and then review the results:

    :::image type="content" source="media/ssms-tricks/query-error-log.png" alt-text="Screenshot of Query the error log." lightbox="media/ssms-tricks/query-error-log.png":::

### Find the error log location if you're connected to SQL Server

1. Connect to your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance.

1. Open a **New Query** window.

1. Paste the following [!INCLUDE [tsql](../../includes/tsql-md.md)] code in your query window, and then select **Execute**:

     ```sql
     SELECT SERVERPROPERTY('ErrorLogFileName') AS 'Error log file location';
     ```

1. The results show the location of the error log in the file system:

    :::image type="content" source="media/ssms-tricks/find-error-log-query.png" alt-text="Screenshot of Find the error log by query." lightbox="media/ssms-tricks/find-error-log-query.png":::

### Find the error log location if you can't connect to SQL Server

The path for your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] error log can vary depending on your configuration settings. The path for the error log location can be found in the SQL Server startup parameters within the SQL Server Configuration Manager. 

Follow these steps to locate the relevant startup parameter that identifies the location of your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] error log. *Your path might vary from the path indicated in the example*.

1. Open SQL Server Configuration Manager.

1. Expand **Services**.

1. Right-click your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance, and then select **Properties**:

    :::image type="content" source="media/ssms-tricks/server-properties.png" alt-text="Screenshot of Configuration Manager server properties." lightbox="media/ssms-tricks/server-properties.png":::

1. Select the **Startup Parameters** tab.

1. In the **Existing Parameters** area, the path after `-e` is the location of the error log:

    :::image type="content" source="media/ssms-tricks/error-log.png" alt-text="Screenshot of Error log." lightbox="media/ssms-tricks/error-log.png":::

    There are several error log files in this location. The file name that ends with *log is the current error log file. File names that end with numbers are previous log files. A new log is created every time the SQL Server restarts.

1. Open the errorlog.log file in your preferred text editor.

## Find SQL Server instance name

You have a few options to find the name of your SQL Server instance before and after you connect to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

### Before you connect to SQL Server

1. Follow the steps to locate the [SQL Server error log on disk](#find-the-error-log-location-if-you-cant-connect-to-sql-server). 

1. Open the errorlog.log file in Notepad.

1. Search for the text *Server name is*.

    The text listed in the single quotes is the name of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance to use in the connection:

    :::image type="content" source="media/ssms-tricks/server-name-in-log.png" alt-text="Screenshot of Find the server name in the error log." lightbox="media/ssms-tricks/server-name-in-log.png":::

    The format of the name is `HOSTNAME\INSTANCENAME`. If you see only the host name, then you've installed the default instance and your instance name is `MSSQLSERVER`. When you connect to a default instance, the host name is all you need to enter to connect to your SQL Server. Your path might vary from the path in the sample image.

### When you're connected to SQL Server

When you're connected to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], you can find the server name in three locations:

- The name of the server is listed in Object Explorer:

    :::image type="content" source="media/ssms-tricks/name-in-object-explorer.png" alt-text="Screenshot of SQL Server instance name in Object Explorer." lightbox="media/ssms-tricks/name-in-object-explorer.png":::

- The name of the server is listed in the Query window:

    :::image type="content" source="media/ssms-tricks/name-in-query-window.png" alt-text="Screenshot of SQL Server instance name in the Query window." lightbox="media/ssms-tricks/name-in-query-window.png":::

- The name of the server is listed in **Properties**.
    * In the **View** menu, select **Properties Window**:

      :::image type="content" source="media/ssms-tricks/name-in-properties.png" alt-text="Screenshot of SQL Server instance name in the Properties window." lightbox="media/ssms-tricks/name-in-properties.png":::

### If you're connected to an alias or availability group listener

If you're connected to an alias or to an availability group listener, that information appears in **Object Explorer** and Properties. In this case, the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance name might not be readily apparent, and must be queried:

1. Connect to your SQL Server instance.

1. Open a **New Query** window.

1. Paste the following [!INCLUDE [tsql](../../includes/tsql-md.md)] code in the window:

      ```sql
      SELECT @@Servername;
      ```

1. View the results of the query to identify the name of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance you're connected to:

    :::image type="content" source="media/ssms-tricks/query-server-name.png" alt-text="Screenshot of Query the SQL Server name." lightbox="media/ssms-tricks/query-server-name.png":::

## Related content

- [Quickstart: Connect and query a SQL Server instance using SQL Server Management Studio (SSMS)](../quickstarts/ssms-connect-query-sql-server.md)
- [Script objects in SQL Server Management Studio](scripting-ssms.md)
- [Use templates in SQL Server Management Studio](../template/templates-ssms.md)
- [SQL Server Management Studio components and configuration](ssms-configuration.md)
