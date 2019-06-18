---
title: "sp_adddistributiondb (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/30/2018"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_adddistributiondb_TSQL"
  - "sp_adddistributiondb"
helpviewer_keywords: 
  - "sp_adddistributiondb"
ms.assetid: e9bad56c-d2b3-44ba-a4d7-ff2fd842e32d
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# sp_adddistributiondb (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Creates a new distribution database and installs the Distributor schema. The distribution database stores procedures, schema, and metadata used in replication. This stored procedure is executed at the Distributor on the master database in order to create the distribution database, and install the necessary tables and stored procedures required to enable the replication distribution.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_adddistributiondb [ @database= ] 'database'   
    [ , [ @data_folder= ] 'data_folder' ]   
    [ , [ @data_file= ] 'data_file' ]   
    [ , [ @data_file_size= ] data_file_size ]   
    [ , [ @log_folder= ] 'log_folder' ]   
    [ , [ @log_file= ] 'log_file' ]   
    [ , [ @log_file_size= ] log_file_size ]   
    [ , [ @min_distretention= ] min_distretention ]   
    [ , [ @max_distretention= ] max_distretention ]   
    [ , [ @history_retention= ] history_retention ]   
    [ , [ @security_mode= ] security_mode ]   
    [ , [ @login= ] 'login' ]   
    [ , [ @password= ] 'password' ]   
    [ , [ @createmode= ] createmode ]  
    [ , [ @from_scripting = ] from_scripting ] 
    [ , [ @deletebatchsize_xact = ] deletebatchsize_xact ] 
    [ , [ @deletebatchsize_cmd = ] deletebatchsize_cmd ] 
```  
  
## Arguments  
`[ @database = ] database'`
 Is the name of the distribution database to be created. *database* is **sysname**, with no default. If the specified database already exists and is not already marked as a distribution database, then the objects needed to enable distribution are installed and the database is marked as a distribution database. If the specified database is already enabled as a distribution database, an error is returned.  
  
`[ @data_folder = ] 'data_folder'_`
 Is the name of the directory used to store the distribution database data file. *data_folder* is **nvarchar(255)**, with a default of NULL. If NULL, the data directory for that instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is used, for example, `C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Data`.  
  
`[ @data_file = ] 'data_file'`
 Is the name of the database file. *data_file* is **nvarchar(255)**, with a default of **database**. If NULL, the stored procedure constructs a file name using the database name.  
  
`[ @data_file_size = ] data_file_size`
 Is the initial data file size in megabytes (MB). *data_file_size i*s **int**, with a default of 5MB.  
  
`[ @log_folder = ] 'log_folder'`
 Is the name of the directory for the database log file. *log_folder* is **nvarchar(255)**, with a default of NULL. If NULL, the data directory for that instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is used (for example, `C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Data`).  
  
`[ @log_file = ] 'log_file'`
 Is the name of the log file. *log_file* is **nvarchar(255)**, with a default of NULL. If NULL, the stored procedure constructs a file name using the database name.  
  
`[ @log_file_size = ] log_file_size`
 Is the initial log file size in megabytes (MB). *log_file_size* is **int**, with a default of 0 MB, which means the file size is created using the smallest log file size allowed by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
`[ @min_distretention = ] min_distretention`
 Is the minimum retention period, in hours, before transactions are deleted from the distribution database. *min_distretention* is **int**, with a default of 0 hours.  
  
`[ @max_distretention = ] max_distretention`
 Is the maximum retention period, in hours, before transactions are deleted. *max_distretention* is **int**, with a default of 72 hours. Subscriptions that have not received replicated commands that are older than the maximum distribution retention period are marked as inactive and need to be reinitialized. RAISERROR 21011 is issued for each inactive subscription. A value of **0** means that replicated transactions are not stored in the distribution database.  
  
`[ @history_retention = ] history_retention`
 Is the number of hours to retain history. *history_retention* is **int**, with a default of 48 hours.  
  
`[ @security_mode = ] security_mode`
 Is the security mode to use when connecting to the Distributor. *security_mode* is **int**, with a default of 1. **0** specifies [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication; **1** specifies Windows Integrated Authentication.  
  
`[ @login = ] 'login'`
 Is the login name used when connecting to the Distributor to create the distribution database. This is required if *security_mode* is set to **0**. *login* is **sysname**, with a default of NULL.  
  
`[ @password = ] 'password'`
 Is the password used when connecting to the Distributor. This is required if *security_mode* is set to **0**. *password* is **sysname**, with a default of NULL.  
  
`[ @createmode = ] createmode`
 *createmode* is **int**, with a default of 1, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**0**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**1** (default)|CREATE DATABASE or use existing database and then apply **instdist.sql** file to create replication objects in the distribution database.|  
|**2**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
  
`[ @from_scripting = ] from_scripting`
 [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]  
 
`[ @deletebatchsize_xact = ] deletebatchsize_xact`
 Specifies the batch size to be used during cleanup of expired transactions from the MSRepl_Transactions tables. *deletebatchsize_xact* is **int**, with a default of 5000. This parameter was first introduced in SQL Server 2017, followed by releases in SQL Server 2012 SP4 and SQL Server 2016 SP2.  

`[ @deletebatchsize_cmd = ] deletebatchsize_cmd`
 Specifies the batch size to be used during cleanup of expired commands from the MSRepl_Commands tables. *deletebatchsize_cmd* is **int**, with a default of 2000. This parameter was first introduced in SQL Server 2017, followed by releases in SQL Server 2012 SP4 and SQL Server 2016 SP2. 
 
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 **sp_adddistributiondb** is used in all types of replication. However, this stored procedure only runs at a distributor.  
  
 You must configure the distributor by executing [sp_adddistributor](../../relational-databases/system-stored-procedures/sp-adddistributor-transact-sql.md) before executing **sp_adddistributiondb**.  
  
 Run **sp_adddistributor** prior to running **sp_adddistributiondb**.  
  
## Example  
  
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
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_adddistributiondb**.  
  
## See Also  
 [Configure Publishing and Distribution](../../relational-databases/replication/configure-publishing-and-distribution.md)   
 [sp_changedistributiondb &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changedistributiondb-transact-sql.md)   
 [sp_dropdistributiondb &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropdistributiondb-transact-sql.md)   
 [sp_helpdistributiondb &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpdistributiondb-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [Configure Distribution](../../relational-databases/replication/configure-distribution.md)  
  
  
