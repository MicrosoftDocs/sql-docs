---
Title: "Tutorial: Additional Tips and Tricks for using SSMS"
description: "A tutorial that covers some additional Tips and Tricks for using SSMS. "
keywords: SQL Server, SSMS, SQL Server Management Studio
author: MashaMSFT
ms.author: mathoma
ms.date: 03/13/2018
ms.topic: Tutorial
ms.suite: "sql"
ms.prod_service: sql-tools
ms.reviewer: sstein
manager: craigg
helpviewer_keywords:  
  - "source controls [SQL Server Management Studio], tutorials"
  - "Help [SQL Server], SQL Server Management Studio"
  - "tutorials [SQL Server Management Studio]"
  - "Transact-SQL tutorials"
  - "SQL Server Management Studio [SQL Server], tutorials"
---



# Tutorial: Additional Tips and Tricks for using SSMS
This tutorial will provide you with some additional tricks for using SQL Server Management Studio. This article will teach you how to: 

> [!div class="checklist"]
> * Comment / Uncomment your Transact-SQL (T-SQL) text
> * Indent your text
> * Filter Objects in Object Explorer
> * Access your SQL Server Error log
> * Find the name of your SQL Server Instance

## Prerequisites
To complete this Tutorial, you need SQL Server Management Studio, access to a SQL Server, and an AdventureWorks database. 

- Install [SQL Server Management Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms).
- Install [SQL Server 2017 Developer Edition](https://www.microsoft.com/en-us/sql-server/sql-server-downloads).
- Download an [AdventureWorks Sample Databases](https://github.com/Microsoft/sql-server-samples/releases). Instructions for restoring databases in SSMS can be found here: [Restoring a Database](https://docs.microsoft.com/en-us/sql/relational-databases/backup-restore/restore-a-database-backup-using-ssms). 

## Comment / Uncomment your T-SQL Code
Portions of your text can be commented and uncommented by using the comment button in the toolbar. Text that is commented out will not be executed. 

1. Open SQL Server Management Studio. 
2. Connect to your SQL Server.
3. Open a **New Query** window. 
4. Paste the following T-SQL code snippet into your text window: 

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


5. Highlight the **Alter Database** portion of the text and click **Comment** in the toolbar: 

    ![Comment](media/ssms-tricks/comment.png)
6. Click **Execute** to run the uncommented portion of the text. 
7. Highlight everything other than the **Alter Database** command and click **Comment** in the toolbar:

    ![Comment Everything](media/ssms-tricks/commenteverything.png)

8. Highlight the **Alter Database** portion and click **Uncomment** to uncomment it:

    ![Uncomment](media/ssms-tricks/uncomment.png)
    
9. Click **Execute** to run the uncommented portion of the text. 

## Indent your Text
The indentation buttons allow you to increase and decrease the indent of your text. 

1. Open a **New Query** window. 
2. Paste the following T-SQL code snippet into your text window: 

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
 
3. Highlight the **Alter Database** portion of the text and press **Increase Indent** in the toolbar to move this text forward:

    ![Increase Indent](media/ssms-tricks/increaseindent.png)

4. Highlight the **Alter Database** portion of the text again and this time click **Decrease Indent** to move this text back. 
    ![Decrease Indent](media/ssms-tricks/decreaseindent.png)


## Filter Objects in Object Explorer
When a database has many objects, finding a specific object can prove difficult. To make this easier, you have the ability to filter objects. This section explains how to filter tables, but the same steps can be applied to any other node within **Object Explorer**

1. Connect to your SQL Server.
2. Expand your **Databases** node.
3. Expand your **AdventureWorks** database node. 
4. Expand your **Tables** node. 
   - You'll notice that you can see all the tables that are present in the database.
5. Right Click the **Tables** node > **Filter** > **Filter Settings**:

    ![Filter Settings](media/ssms-tricks/filtersettings.png)

6. In the Filter Settings window, you can modify filter settings. A few examples:
    - Filter by name: 
   ![Filter By Name](media/ssms-tricks/filterbyname.png)
    - Filter by schema: 
    ![Filter by Schema](media/ssms-tricks/filterbyschema.png)

7. To clear the filter, right-click **Tables** > **Remove Filter**

    ![Remove Filter](media/ssms-tricks/removefilter.png)
    


## Access your SQL Server Error log
The error log is a file that contains details about things occurring within your SQL Server. It can be browsed and queried within SSMS. It can also be found as a .log file on disk.

### Open Error log within SSMS
1. Connect to your SQL Server.
2. Expand the **Management** node. 
3. Expand the **SQL Server Logs** node. 
4. Right-click the **Current** error log > **View SQL Server Log**:

    ![View Error log within SSMS](media/ssms-tricks/viewerrorloginssms.png)

### Query error log within SSMS
1. Connect to your SQL Server.
2. Open a **New Query** window.
3. Paste the following T-SQL code snippet into your query Window:

 ```sql
   sp_readerrorlog 0,1,'Server process ID' 
  ``` 
4. Modify the text in the single quotes to text of interest.
5. Execute the query and review the results:
   
    ![Query Error Log](media/ssms-tricks/queryerrorlog.png)


### Find error log location if you're connected to SQL
1. Connect to  your SQL Server.
2. Open a **New Query** window.
3. Paste the following T-SQL code snippet into your query window and click **Execute**:
  ```sql
   SELECT SERVERPROPERTY('ErrorLogFileName') AS 'Error log file location' 
  ```
4. The results show you the location of the error log within the file system: 

    ![Find Error Log by Query](media/ssms-tricks/finderrorlogquery.png)

### Find error log location if you cannot connect to SQL
1. Open your SQL Server Configuration Manager. 
2. Expand the **Services** node.
3. Right click on your SQL Server instance > **Properties**:

    ![Config Manager Server Properties](media/ssms-tricks/serverproperties.PNG)

4. Select the **Startup Parameters** tab.
5. In the **Existing Parameters** area, the path after the "-e" is the location of the error log: 
    
    ![Error Log](media/ssms-tricks/errorlog.png)
    - You'll notice that there are several errorlog.* in this location. The one ending with *.log is the current one. The ones ending with numbers are previous logs, as a new log is created every time the SQL Server restarts. 
6. Open this file in Notepad. 

## Determine SQL Server Name...
There are different ways to determine the name of your SQL Server before and after you connect to your SQL Server.  

### ...When you don't know it
1. Follow the steps to locate the [SQL Server Error log on disk](#finding-your-error-log-if-you-cannot-connect-to-sql). 
2. Open the errorlog.log in Notepad. 
3. Navigate through it until you find the text "Server name is":
  - Whatever is listed in the single quotes is the name of the SQL Server and what you'll be connecting to: 
    ![Server Name in Error Log](media/ssms-tricks/servernameinlog.png)
    The format of the name is 'HOSTNAME\SERVERNAME'. If all you see is the hostname, then you've installed the default instance, and your instance name is 'MSSQLSERVER'. When connecting to a default instance, the hostname is all you need to type in to connect to  your SQL Server. 

### ...Once you're connected to SQL 
There are three places to find which SQL Server you're connected to. 

1. The name of the server will be listed in **Object Explorer**:

    ![Instance Name in Object Explorer](media/ssms-tricks/nameinobjectexplorer.png)
2. The name of the server will be listed in the query window:

    ![Name in Query Window](media/ssms-tricks/nameinquerywindow.png)
3. The name of the server will also be listed in the **Properties window**.
    - To access this open the **View** Menu > **Properties Window**:

    ![Name in Properties](media/ssms-tricks/nameinproperties.png)

### ...If you're connected to an Alias or Availability Group Listener 
When you're connected to an alias or an Availability Group listener, then that's what will show up **Object Explorer** and **Properties**. In this case, the SQL Server name may not be readily apparent, and must be queried. 

1. Connect to SQL Server.
2. Open a **New Query** window.
3. Paste the following T-SQL Code snippet into the window: 

  ```sql
   select @@Servername 
 ``` 
4. View the results of the query to identify the name of the SQL Server you're connected to: 
    
    ![Query Server Name](media/ssms-tricks/queryservername.png)


