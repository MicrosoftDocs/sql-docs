---
description: "Replication System Stored Procedures Concepts"
title: "Replication System Stored Procedures Concepts | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "stored procedures [SQL Server replication], programming"
  - "programming [SQL Server replication], system stored procedures"
  - "programming interfaces [SQL Server replication]"
  - "system stored procedures [SQL Server replication]"
  - "replication [SQL Server], how-to topics"
ms.assetid: 816d2bda-ed72-43ec-aa4d-7ee3dc25fd8a
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Replication System Stored Procedures Concepts
[!INCLUDE[sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]

  In [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], programmatic access to all of the user-configurable functionality in a replication topology is provided by system stored procedures. While stored procedures may be executed individually using the [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or the sqlcmd command-line utility, it may be beneficial to write [!INCLUDE[tsql](../../../includes/tsql-md.md)] script files that can be executed to perform a logical sequence of replication tasks.  
  
 Scripting replication tasks provides the following benefits:  
  
-   Keeps a permanent copy of the steps used to deploy your replication topology.  
  
-   Uses a single script to configure multiple Subscribers.  
  
-   Quickly educates new database administrators by enabling them to evaluate, understand, change, or troubleshoot the code.  
  
    > [!IMPORTANT]  
    >  Scripts can be the source of security vulnerabilities; they can invoke system functions without user knowledge or intervention and may contain security credentials in plain text. Review scripts for security issues before you use them.  
  
## Creating Replication Scripts  
 From the standpoint of replication, a script is a series of one or more [!INCLUDE[tsql](../../../includes/tsql-md.md)] statements where each statement executes a replication stored procedure. Scripts are text files, often with a .sql file extension, that can be run using the sqlcmd utility. When a script file is run, the utility executes the SQL statements stored in the file. Similarly, a script can be stored as a query object in a [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] project.  
  
 Replication scripts can be created in the following ways:  
  
-   Manually create the script.  
  
-   Use the script generation features that are provided in the replication wizards or  
  
-   [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)]. For more information, see [Scripting Replication](../../../relational-databases/replication/scripting-replication.md).  
  
-   Use Replication Management Objects (RMOs) to programmatically generate the script to create an RMO object.  
  
 When you manually create replication scripts, keep the following considerations in mind:  
  
-   [!INCLUDE[tsql](../../../includes/tsql-md.md)] scripts have one or more batches. The GO command signals the end of a batch. If a [!INCLUDE[tsql](../../../includes/tsql-md.md)] script does not have any GO commands, it is executed as a single batch.  
  
-   When executing multiple replication stored procedures in a single batch, after the first procedure, all subsequent procedures in the batch must be preceded by the EXECUTE keyword.  
  
-   All stored procedures in a batch must compile before a batch will execute. However, once the batch has been compiled, and an execution plan has been created, a run-time error may or may not occur.  
  
-   When creating scripts to configure replication, you should use Windows Authentication to avoid storing security credentials in the script file. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.  
  
## Sample Replication Script  
 The following script can be executed to setup publishing and distribution on a server.  
  
```  
-- This script uses sqlcmd scripting variables. They are in the form  
-- $(MyVariable). For information about how to use scripting variables    
-- on the command line and in SQL Server Management Studio, see the   
-- "Executing Replication Scripts" section in the topic  
-- "Programming Replication Using System Stored Procedures".  
  
-- Install the Distributor and the distribution database.  
DECLARE @distributor AS sysname;  
DECLARE @distributionDB AS sysname;  
DECLARE @publisher AS sysname;  
DECLARE @directory AS nvarchar(500);  
DECLARE @publicationDB AS sysname;  
-- Specify the Distributor name.  
SET @distributor = $(DistPubServer);  
-- Specify the distribution database.  
SET @distributionDB = N'distribution';  
-- Specify the Publisher name.  
SET @publisher = $(DistPubServer);  
-- Specify the replication working directory.  
SET @directory = N'\\' + $(DistPubServer) + '\repldata';  
-- Specify the publication database.  
SET @publicationDB = N'AdventureWorks2012';   
  
-- Install the server MYDISTPUB as a Distributor using the defaults,  
-- including autogenerating the distributor password.  
USE master  
EXEC sp_adddistributor @distributor = @distributor;  
  
-- Create a new distribution database using the defaults, including  
-- using Windows Authentication.  
USE master  
EXEC sp_adddistributiondb @database = @distributionDB,   
    @security_mode = 1;  
GO  
  
-- Create a Publisher and enable AdventureWorks2012 for replication.  
-- Add MYDISTPUB as a publisher with MYDISTPUB as a local distributor  
-- and use Windows Authentication.  
DECLARE @distributionDB AS sysname;  
DECLARE @publisher AS sysname;  
-- Specify the distribution database.  
SET @distributionDB = N'distribution';  
-- Specify the Publisher name.  
SET @publisher = $(DistPubServer);  
  
USE [distribution]  
EXEC sp_adddistpublisher @publisher=@publisher,   
    @distribution_db=@distributionDB,   
    @security_mode = 1;  
GO  
  
```  
  
 This script can then be saved locally as `instdistpub.sql` so that it can be run or rerun when needed.  
  
 The previous script includes **sqlcmd** scripting variables, which are used in many of the replication code samples in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Books Online. Scripting variables are defined by using `$(MyVariable)` syntax. Values for variables can be passed to a script at the command line or in [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)]. For more information, see the next section in this topic, "Executing Replication Scripts."  
  
## Executing Replication Scripts  
 Once created, a replication script can be executed in one of the following ways:  
  
### Creating a SQL Query File in SQL Server Management Studio  
 A replication [!INCLUDE[tsql](../../../includes/tsql-md.md)] script file can be created as a SQL Query file in a [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] project. After the script is written, a connection can be made to the database for this query file and the script can be executed. For more information about how to create [!INCLUDE[tsql](../../../includes/tsql-md.md)] scripts by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], see [Query and Text Editors &#40;SQL Server Management Studio&#41;](../../../ssms/f1-help/database-engine-query-editor-sql-server-management-studio.md).  
  
 To use a script that includes scripting variables, [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] must be running in **sqlcmd** mode. In **sqlcmd** mode, the Query Editor accepts additional syntax specific to **sqlcmd**, such as `:setvar`, which is used to a value for a variable. For more information about **sqlcmd** mode, see [Edit SQLCMD Scripts with Query Editor](../../../ssms/scripting/edit-sqlcmd-scripts-with-query-editor.md). In the following script, `:setvar` is used to provide a value for the `$(DistPubServer)` variable.  
  
```  
:setvar DistPubServer N'MyPublisherAndDistributor';  
  
-- Install the Distributor and the distribution database.  
DECLARE @distributor AS sysname;  
DECLARE @distributionDB AS sysname;  
DECLARE @publisher AS sysname;  
DECLARE @directory AS nvarchar(500);  
DECLARE @publicationDB AS sysname;  
-- Specify the Distributor name.  
SET @distributor = $(DistPubServer);  
-- Specify the distribution database.  
SET @distributionDB = N'distribution';  
-- Specify the Publisher name.  
SET @publisher = $(DistPubServer);  
  
--  
-- Additional code goes here  
--  
```  
  
### Using the sqlcmd Utility from the Command Line  
 The following example shows how the command line is used to execute the `instdistpub.sql` script file using the [sqlcmd utility](../../../tools/sqlcmd-utility.md):  
  
```  
sqlcmd.exe -E -S sqlserverinstance -i C:\instdistpub.sql -o C:\output.log -v DistPubServer="N'MyDistributorAndPublisher'"  
```  
  
 In this example, the `-E` switch indicates that Windows Authentication is used when connecting to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. When using Windows Authentication, there is no need to store a username and password in the script file. The name and path of the script file is specified by the `-i` switch and the name of the output file is specified by the `-o` switch (output from [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] is written to this file instead of the console when this switch is used). The `sqlcmd` utility enables you to pass scripting variables to a [!INCLUDE[tsql](../../../includes/tsql-md.md)] script at runtime using the `-v` switch. In this example, `sqlcmd` replaces every instance of `$(DistPubServer)` in the script with the value `N'MyDistributorAndPublisher'` before execution.  
  
> [!NOTE]  
>  The `-X` switch disables scripting variables.  
  
### Automating Tasks in a Batch File  
 By using a batch file, replication administration tasks, replication synchronization tasks, and other tasks can be automated in the same batch file. The following batch file uses the **sqlcmd** utility to drop and recreate the subscription database and add a merge pull subscription. Then the file invokes the merge agent to synchronize the new subscription:  
  
```  
REM ----------------------Script to synchronize merge subscription ----------------------  
REM -- Creates subscription database and   
REM -- synchronizes the subscription to MergeSalesPerson.  
REM -- Current computer acts as both Publisher and Subscriber.  
REM -------------------------------------------------------------------------------------  
  
SET Publisher=%computername%  
SET Subscriber=%computername%  
SET PubDb=AdventureWorks  
SET SubDb=AdventureWorksReplica  
SET PubName=AdvWorksSalesOrdersMerge  
  
REM -- Drop and recreate the subscription database at the Subscriber  
sqlcmd /S%Subscriber% /E /Q"USE master IF EXISTS (SELECT * FROM sysdatabases WHERE name='%SubDb%' ) DROP DATABASE %SubDb%"  
sqlcmd /S%Subscriber% /E /Q"USE master CREATE DATABASE %SubDb%"  
  
REM -- Add a pull subscription at the Subscriber  
sqlcmd /S%Subscriber% /E /Q"USE %SubDb% EXEC sp_addmergepullsubscription @publisher = %Publisher%, @publication = %PubName%, @publisher_db = %PubDb%"  
sqlcmd /S%Subscriber% /E /Q"USE %SubDb%  EXEC sp_addmergepullsubscription_agent @publisher = %Publisher%, @publisher_db = %PubDb%, @publication = %PubName%, @subscriber = %Subscriber%, @subscriber_db = %SubDb%, @distributor = %Publisher%"  
  
REM -- This batch file starts the merge agent at the Subscriber to   
REM -- synchronize a pull subscription to a merge publication.  
REM -- The following must be supplied on one line.  
"\Program Files\Microsoft SQL Server\130\COM\REPLMERG.EXE"  -Publisher  %Publisher% -Subscriber  %Subscriber%  -Distributor %Publisher%  -PublisherDB  %PubDb% -SubscriberDB %SubDb% -Publication %PubName% -PublisherSecurityMode 1 -OutputVerboseLevel 1  -Output  -SubscriberSecurityMode 1  -SubscriptionType 1 -DistributorSecurityMode 1 -Validate 3  
  
```  
  
## Scripting Common Replication Tasks  
 The following are some of the most common replication tasks can be scripted using system stored procedures:  
  
-   Configuring publishing and distribution  
  
-   Modifying Publisher and Distributor properties  
  
-   Disabling publishing and distribution  
  
-   Creating publications and defining articles  
  
-   Deleting publications and articles  
  
-   Creating a pull subscription  
  
-   Modifying a pull subscription  
  
-   Deleting a pull subscription  
  
-   Creating a push subscription  
  
-   Modifying a push subscription  
  
-   Deleting a push subscription  
  
-   Synchronizing a pull subscription  
  
## See Also  
 [Replication Programming Concepts](../../../relational-databases/replication/concepts/replication-programming-concepts.md)   
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)   
 [Scripting Replication](../../../relational-databases/replication/scripting-replication.md)  
  
