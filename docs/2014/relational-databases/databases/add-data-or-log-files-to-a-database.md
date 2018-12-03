---
title: "Add Data or Log Files to a Database | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "logs [SQL Server], files"
  - "adding data files"
  - "adding files"
  - "adding log files"
  - "file additions [SQL Server], steps"
  - "files [SQL Server], adding"
  - "data additions [SQL Server]"
ms.assetid: 8ead516a-1334-4f40-84b2-509d0a8ffa45
author: stevestein
ms.author: sstein
manager: craigg
---
# Add Data or Log Files to a Database
  This topic describes how to add data or log files to a database in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To add data or log files to a database, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   You cannot add or remove a file while a BACKUP statement is running.  
  
-   A maximum of 32,767 files and 32,767 filegroups can be specified for each database.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the database.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To add data or log files to a database  
  
1.  In **Object Explorer**, connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] and then expand that instance.  
  
2.  Expand **Databases**, right-click the database from which to add the files, and then click **Properties**.  
  
3.  In the **Database Properties** dialog box, select the **Files** page.  
  
4.  To add a data or transaction log file, click **Add**.  
  
5.  In the **Database files** grid, enter a logical name for the file. The file name must be unique within the database.  
  
6.  Select the file type, data or log.  
  
7.  For a data file, select the filegroup in which the file should be included from the list, or select **\<new filegroup>** to create a new filegroup. Transaction logs cannot be put in filegroups.  
  
8.  Specify the initial size of the file. Make the data file as large as possible, based on the maximum amount of data you expect in the database.  
  
9. To specify how the file should grow, click (**...**) in the **Autogrowth** column. Select from the following options:  
  
    1.  To allow for the currently selected file to grow as more data space is required, select the **Enable Autogrowth** check box and then select from the following options:  
  
    2.  To specify that the file should grow by fixed increments, select **In Megabytes** and specify a value.  
  
    3.  To specify that the file should grow by a percentage of the current file size, select **In Percent** and specify a value.  
  
10. To specify the maximum file size limit, select from the following options:  
  
    1.  To specify the maximum size the file should be able to grow to, select **Restricted File Growth (MB)** and specify a value.  
  
    2.  To allow for the file to grow as much as needed, select **Unrestricted File Growth**.  
  
    3.  To prevent the file from growing, clear the **Enable Autogrowth** check box. The size of the file will not grow beyond the value specified in the **Initial Size (MB)** column.  
  
    > [!NOTE]  
    >  The maximum database size is determined by the amount of disk space available and the licensing limits determined by the version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that you are using.  
  
11. Specify the path for the file location. The specified path must exist before adding the file.  
  
    > [!NOTE]  
    >  By default, the data and transaction logs are put on the same drive and path to accommodate single-disk systems, but may not be optimal for production environments. For more information, see [Database Files and Filegroups](database-files-and-filegroups.md).  
  
12. Click **OK**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To add data or log files to a database  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. The example adds a filegroup with two files to a database. The example creates the filegroup `Test1FG1` in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database and adds two 5-MB files to the filegroup.  
  
 [!code-sql[DatabaseDDL#AlterDatabase2](../../snippets/tsql/SQL14/tsql/databaseddl/transact-sql/alterdatabase.sql#alterdatabase2)]  
  
 For more examples, see [ALTER DATABASE File and Filegroup Options &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql-file-and-filegroup-options).  
  
## See Also  
 [Database Files and Filegroups](database-files-and-filegroups.md)   
 [Delete Data or Log Files from a Database](delete-data-or-log-files-from-a-database.md)   
 [Increase the Size of a Database](increase-the-size-of-a-database.md)  
  
  
