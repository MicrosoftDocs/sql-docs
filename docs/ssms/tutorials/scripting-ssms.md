---
Title: "Tutorial: Script objects in SQL Server Management Studio"
description: "A tutorial for scripting out objects in SSMS" 
keywords: SQL Server, SSMS, SQL Server Management Studio, Scripts, Scripting
author: MashaMSFT
ms.author: mathoma
ms.date: 03/13/2018
ms.topic: Tutorial
ms.suite: "sql"
ms.prod_service: sql-tools
ms.reviewer: sstein
manager: craigg
helpviewer_keywords: 
  - "projects [SQL Server Management Studio], tutorials"
  - "source controls [SQL Server Management Studio], tutorials"
  - "Help [SQL Server], SQL Server Management Studio"
  - "tutorials [SQL Server Management Studio]"
  - "Transact-SQL tutorials"
  - "solutions [SQL Server Management Studio], tutorials"
  - "SQL Server Management Studio [SQL Server], tutorials"
  - "scripts [SQL Server], SQL Server Management Studio"
---

# Tutorial: Script objects in SQL Server Management Studio
This tutorial teaches you how to generate Transact-SQL (T-SQL) scripts for various objects found within SQL Server Management Studio (SSMS). In this tutorial, you find examples of how to script the following objects: 

> [!div class="checklist"]
> * Queries, when you perform actions within the graphical user interface (GUI)
> * Databases in two different ways ("Script As" and "Generate Script")
> * Tables
> * Stored procedures
> * Extended events

This tutorial demonstrates that any object in Object Explorer can be scripted by right-clicking it and selecting the Script Object As option.


## Prerequisites
To complete this tutorial, you need SQL Server Management Studio, access to a SQL server, and an AdventureWorks database.

- Install [SQL Server Management Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms).
- Install [SQL Server 2017 Developer Edition](https://www.microsoft.com/en-us/sql-server/sql-server-downloads).
- Download [AdventureWorks2016 sample databases](https://github.com/Microsoft/sql-server-samples/releases). Instructions for restoring databases in SSMS are found here: [Restoring a database](https://docs.microsoft.com/en-us/sql/relational-databases/backup-restore/restore-a-database-backup-using-ssms). 


## Script queries from the GUI
Any time you perform a task using the GUI in SSMS, you can also generate the T-SQL code associated with that task. The following examples show how to do so when you back up a database and when you shrink the transaction log. These same steps can be applied to any action that's completed via the GUI. 

### Script T-SQL when you back up a database
1. Connect to your SQL server.
2. Expand the Databases node.
3. Right-click the database **Adventureworks2016** > **Tasks** > **Back up**:

    ![Back up a database](media/scripting-ssms/backupdb.png)

4. Configure the backup the way you want. For this tutorial, everything is left at default. However, any changes made in the window will also reflect in the script. 
5. Select the option to **Script** > **Script Action to New Query Window**:
 
    ![Script back up database--script action](media/scripting-ssms/scriptdbbackup.PNG)
6. Review the T-SQL populated in the query window.

    ![Script back up database--review T-SQL](media/scripting-ssms/dbbackupscript.PNG)
7. Select **Execute** to execute the query to back up the database via T-SQL. 


### Script T-SQL when shrinking the transaction log
1. Right-click the database **AdventureWorks2016** > **Tasks** > **Shrink** > **Files**:

     ![Shrink files](media/scripting-ssms/shrinkfiles.png)

2. Select **Log** from the **File type** drop-down list box:

    ![Shrink transaction log](media/scripting-ssms/shrinktlog.png)

3. Select the options **Script** and **Script Action to Clipboard**:

    ![Script to clipboard](media/scripting-ssms/scriptactiontoclipboard.png)

4. Open a **New Query** window and paste (right-click in the window > **Paste**):

    ![Paste script](media/scripting-ssms/paste.png)
5. Select **Execute** to execute the query and shrink the transaction log. 


## Script databases
The following section teaches you how to script out the database using the Script As and Generate Scripts options. The Script As option recreates the database and its configuration options. The Generate Scripts option allows you to script both the schema and the data. In this section, you create two new databases. You use the Script As option to create AdventureWorks2016a. You use the Generate Scripts option to create AdventureWorks2016b.


### Use the Script option to script a database
1. Connect to your SQL server.
2. Expand the Databases node.
3. Right-click the database **AdventureWorks2016** > **Script Database As** > **Create To** > **New Query Window**:

    ![Script database](media/scripting-ssms/scriptdb.png)

4. Review the database creation query in the window: 

    ![Scripted out database](media/scripting-ssms/scriptedoutdb.png)
    - This option scripts out only the database configuration options.
5. On your keyboard, select **Ctrl + F** to open the Find dialog box. Select the down arrow to open the Replace option. On the top **Find** line, type AdventureWorks2016, and on the bottom **Replace** line, type AdventureWorks2016a.
6. Select **Replace All** to replace all instances of AdventureWorks2016 with AdventureWorks2016a. 

    ![Find and replace](media/scripting-ssms/findandreplace.png)

1. Select **Execute** to execute the query and create your new AdventureWorks2016a database. 

### Use the Generate Scripts option to script a database
1. Connect to your SQL server.
2. Expand the Databases node.
3. Right-click the database **AdventureWorks2016** > **Tasks** > **Generate Scripts**:

    ![Generate scripts for database](media/scripting-ssms/generatescriptsfordb.png)

4. The Introduction page opens. Select **Next** to open the Chose Objects page. You can select the entire database or specific objects in the database. Select the option **Script entire database and all database objects**. 
 
    ![Generate scripts for objects](media/scripting-ssms/scriptobjects.png)
 
5. Select **Next** to open the Set Scripting Options page. Here you can configure where to save the script and some additional advanced options. 

    a. Select the option to **Save to new query Window**. 

    b. Select **Advanced** and ensure these options are set: 

      - Script Statistics set to Script Statistics.
      - **Types of data to script** set to *Schema only*.
      - **Script Indexes** set to *true*.

   ![Script objects](media/scripting-ssms/advancedscripts.png)

   > [!NOTE]
   > You have the ability to script the data for the database when you select *Schema and data* for the Types of data to script option. However, this isn't ideal with large databases. It can take more memory than SSMS is able to allocate. This is okay for small databases. If you want to move data for a larger database, use the import and export wizard.
   
   [Import and export wizard](https://docs.microsoft.com/en-us/sql/integration-services/import-export-data/import-and-export-data-with-the-sql-server-import-and-export-wizard)


1. Select **OK** and then select **Next**. 
2. Select **Next** on the Summary. Then select **Next** again to generate the script to a New Query window. 
3. On your keyboard, select **Ctrl + F** to open the Find dialog box. Select the down arrow to open the Replace option. On the top Find line, type AdventureWorks2016. On the bottom Replace line, type AdventureWorks2016b. 
    a. Select **Replace All** to replace all instances of AdventureWorks2016 with AdventureWorks2016b. 

    ![AdventureWorks2016b](media/scripting-ssms/adventureworks2016b.png)
7. Select **Execute** to execute the query and create your new AdventureWorks2016b database. 
 
## Script tables
This section covers how to script out tables from your database. Use this option to either create the table or drop and create the table. You can also use this option to script the T-SQL associated with modifying the table. For example, to insert into it or update to it. In this section, you'll drop a table and then recreate it. 

1. Connect to your SQL server.
2. Expand your Databases node.
3. Expand your AdventureWorks database node. 
4. Expand your Tables node.
5. Right-click **dbo.ErrorLog** >  **Script Table as** > **Drop and Create To** > **New Query Editor Window**:
    
    ![Script table](media/scripting-ssms/scripttable.png)

6. Select **Execute** to execute the query. This action drops the *Errorlog* table and recreates it. 

    >[!NOTE]
    > The Errorlog table is empty by default in the AdventureWorks2016 database, so you're not losing any data by dropping the table. However, following these steps on a table with data causes data loss. 
 
## Script stored procedures
In this section, you learn how to drop and create a stored procedure.  

1. Connect to your SQL server.
2. Expand your Databases node.
3. Expand your Programmability node. 
4. Expand your Stored Procedure node.
5. Right-click the stored procedure **dbo.uspGetBillOfMaterials** > **Script Stored Procedure As** > **Drop and Create to** > **New Query Window**:
    
    ![Script stored procedures](media/scripting-ssms/scriptstoredprocedure.PNG)

## Script extended events
This section covers how to script out extended events.

[Extended events](https://docs.microsoft.com/en-us/sql/relational-databases/extended-events/extended-events)

1. Connect to your SQL server.
2. Expand your Management node.
3. Expand your Extended Events node.
4. Expand your Sessions node.
5. Right-click the extended session you're interested in **Script Session As** > **New Query Editor Window**:

    ![Script xEvents](media/scripting-ssms/scriptxevents.png)

6. In the new query window, modify the new name of the session from system_health to system_health2. Select **Execute** to execute the query. 

    a. Right-click **Sessions** in Object Explorer. Select **Refresh** to see your new extended event session. The green icon next to the session indicates the session is running whereas the red icon indicates the session is stopped. 

    ![New xEvent](media/scripting-ssms/newxevent.png)

    >[!NOTE]
    > You can start the session by right-clicking it and selecting **Start**. However, because this is a copy of the already running system_health session, this step can be skipped. You can delete the copy of the extended event session by right-clicking it and selecting **Delete**. 

## Next steps
The next article introduces you to the prebuilt T-SQL templates found within SSMS. 

Go to the next article to learn more:
> [!div class="nextstepaction"]
> [Next steps](templates-ssms.md)


