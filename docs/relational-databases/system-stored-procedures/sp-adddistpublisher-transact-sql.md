---
description: "sp_adddistpublisher (Transact-SQL)"
title: "sp_adddistpublisher (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/29/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_adddistpublisher"
  - "sp_adddistpublisher_TSQL"
helpviewer_keywords: 
  - "sp_adddistpublisher"
ms.assetid: 04e15011-a902-4074-b38c-3ec2fc73b838
author: mashamsft
ms.author: mathoma
---
# sp_adddistpublisher (Transact-SQL)
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  Configures a Publisher to use a specified distribution database. This stored procedure is executed at the Distributor on any database. Note that the stored procedures [sp_adddistributor &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-adddistributor-transact-sql.md) and [sp_adddistributiondb &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-adddistributiondb-transact-sql.md) must have been run prior to using this stored procedure.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_adddistpublisher [ @publisher= ] 'publisher'   
        , [ @distribution_db= ] 'distribution_db'   
    [ , [ @security_mode= ] security_mode ]   
    [ , [ @login= ] 'login' ]   
    [ , [ @password= ] 'password' ]   
    [ , [ @working_directory= ] 'working_directory' ]   
    [ , [ @storage_connection_string= ] 'storage_connection_string']
    [ , [ @trusted= ] 'trusted' ]   
    [ , [ @encrypted_password= ] encrypted_password ]   
    [ , [ @thirdparty_flag = ] thirdparty_flag ]  
    [ , [ @publisher_type = ] 'publisher_type' ]  
```  
  
## Arguments  
`[ @publisher = ] 'publisher'`
 Is the Publisher name. *publisher* is **sysname**, with no default.  

<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15 "

> [!NOTE]
> Server name can be specified as `<Hostname>,<PortNumber>`. You may need to specify the port number for your connection when SQL Server is deployed on Linux or Windows with a custom port, and browser service is disabled. The use of custom port numbers for remote distributor applies to SQL Server 2019 only.

::: moniker-end
  
`[ @distribution_db = ] 'distribution_db'`
 Is the name of the distribution database. *distributor_db* is **sysname**, with no default. This parameter is used by replication agents to connect to the Publisher.  
  
`[ @security_mode = ] security_mode`
 Is the implemented security mode. This parameter is only used by replication agents to connect to the Publisher for queued updating subscriptions or with a non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. *security_mode* is **int**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**0**|Replication agents at the Distributor use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication to connect to the Publisher.|  
|**1** (default)|Replication agents at the Distributor use Windows Authentication to connect to the Publisher.|  
  
`[ @login = ] 'login'`
 Is the login. This parameter is required if *security_mode* is **0**. *login* is **sysname**, with a default of NULL. This parameter is used by replication agents to connect to the Publisher.  
  
`[ @password = ] 'password']`
 Is the password. *password* is **sysname**, with a default of NULL. This parameter is used by replication agents to connect to the Publisher.  
  
> [!IMPORTANT]  
>  Do not use a blank password. Use a strong password.  
  
`[ @working_directory = ] 'working_directory'`
 Is the name of the working directory used to store data and schema files for the publication. *working_directory* is **nvarchar(255)**, and defaults to the ReplData folder for this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], for example `C:\Program Files\Microsoft SQL Server\MSSQL\MSSQ.1\ReplData`. The name should be specified in UNC format.  

 For Azure SQL Database, use `\\<storage_account>.file.core.windows.net\<share>`.

`[ @storage_connection_string = ] 'storage_connection_string'`
 Is required for SQL Database. Use the access key from Azure Portal under storage > settings.

 > [!INCLUDE[Azure SQL Database link](../../includes/azure-sql-db-repl-for-more-information.md)]

`[ @trusted = ] 'trusted'`
 This parameter has been deprecated and is provided for backward compatibility only. *trusted* is **nvarchar(5)**, and setting it to anything but **false** will result in an error.  
  
`[ @encrypted_password = ] encrypted_password`
 Setting *encrypted_password* is no longer supported. Attempting to set this **bit** parameter to **1** will result in an error.  
  
`[ @thirdparty_flag = ] thirdparty_flag`
 Is when the Publisher is [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. *thirdparty_flag* is **bit**, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**0** (default)|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.|  
|**1**|Database other than [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
  
`[ @publisher_type = ] 'publisher_type'`
 Specifies the Publisher type when the Publisher is not [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. *publisher_type* is sysname, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**MSSQLSERVER**<br /><br /> (default)|Specifies a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.|  
|**ORACLE**|Specifies a standard Oracle Publisher.|  
|**ORACLE GATEWAY**|Specifies an Oracle Gateway Publisher.|  
  
 For more information about the differences between an Oracle Publisher and an Oracle Gateway Publisher, see [Configure an Oracle Publisher](../../relational-databases/replication/non-sql/configure-an-oracle-publisher.md).  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 **sp_adddistpublisher** is used by snapshot replication, transactional replication, and merge replication.  
  
## Example  
 [!code-sql[HowTo#AddDistPub](../../relational-databases/replication/codesnippet/tsql/sp-adddistpublisher-tran_1.sql)]  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_adddistpublisher**.  
  
## See Also  
 [Configure Publishing and Distribution](../../relational-databases/replication/configure-publishing-and-distribution.md)   
 [sp_changedistpublisher &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changedistpublisher-transact-sql.md)   
 [sp_dropdistpublisher &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropdistpublisher-transact-sql.md)   
 [sp_helpdistpublisher &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpdistpublisher-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [Configure Distribution](../../relational-databases/replication/configure-distribution.md)  
  
  
