---
title: "PolyBase Connectivity Configuration (Transact-SQL)"
description: Find out how to use sp_configure to display or change global configuration settings for PolyBase Hadoop and Azure Blob Storage connectivity.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "09/18/2022"
ms.service: sql
ms.subservice: polybase
ms.topic: reference
helpviewer_keywords:
  - "PolyBase"
monikerRange: ">=aps-pdw-2016||>=sql-server-2016||>=sql-server-linux-2017"
---
# PolyBase Connectivity Configuration (Transact-SQL)
[!INCLUDE[appliesto-ss-xxxx-xxxx-pdw-md](../../includes/appliesto-ss-xxxx-xxxx-pdw-md.md)]

  Displays or changes global configuration settings for PolyBase Hadoop and Microsoft Azure Blob Storage connectivity.
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql 
  
--List all of the configuration options  
sp_configure  
[;]  
  
--Configure Hadoop connectivity  
sp_configure [ @configname = ] 'hadoop connectivity',  
             [ @configvalue = ] { 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 }  
[;]  
  
RECONFIGURE  
[;]  
```  
  
## Arguments  
 [ **@configname=** ] **'**_option\_name_**'**  
 Is the name of a configuration option. *option_name* is **varchar(35)**, with a default of `NULL`. If not specified, the complete list of options is returned.  
  
 [ **@configvalue=** ] **'**_value_**'**  
 Is the new configuration setting. *value* is **int**, with a default of `NULL`. The maximum value depends on the individual option.  
  
 **'hadoop connectivity'**  
 Specifies the type of Hadoop data source for all connections from PolyBase to Hadoop clusters or Azure Blob Storage. For more information, see [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md).
  
 These are the Hadoop connectivity settings and their corresponding supported Hadoop data sources. Only one setting can be in effect at a time.  

 Options 1, 4, 7, and 8 allow multiple types of external data sources to be created and used across all sessions on the server.   
  
-   Option 0: Disable Hadoop connectivity  
  
-   Option 1: Hortonworks HDP 1.3 on Windows Server  
  
-   Option 1: Azure Blob Storage (WASB[S])  
  
-   Option 2: Hortonworks HDP 1.3 on Linux  
  
-   Option 3: Cloudera CDH 4.3 on Linux  
  
-   Option 4: Hortonworks HDP 2.0 on Windows Server  
  
-   Option 4: Azure Blob Storage (WASB[S])  
  
-   Option 5: Hortonworks HDP 2.0 on Linux  
  
-   Option 6: Cloudera CDH 5.1, 5.2, 5.3, 5.4, 5.5, 5.9, 5.10, 5.11, 5.12, and 5.13 on Linux  
  
-   Option 7: Hortonworks HDP 2.1, 2.2, 2.3, 2.4, 2.5, 2.6, 3.0 on Linux  
  
-   Option 7: Hortonworks HDP 2.1, 2.2, 2.3, 2.4 on Windows Server  
  
-   Option 7: Azure Blob Storage (WASB[S])  
 
-   Option 8:\* Hortonworks HDP 3.1, Cloudera CDH 6.1, 6.2, 6.3, Azure Blob Storage (WASB[S]) and Azure Data Lake Storage Gen2 (ABFS[S])  

   \* Option 8 introduced with SQL Server 2019 CU11.

 ​By default, the Hadoop connectivity is set to 0 (disabled). You should configure the PolyBase hadoop connectivity value after installing then enabling PolyBase. For more information, see [Install PolyBase on Windows](../../relational-databases/polybase/polybase-installation.md) and [Configure PolyBase to access external data in Hadoop](../../relational-databases/polybase/polybase-configure-hadoop.md).

 **RECONFIGURE**  
 Updates the run value (`run_value`) to match the configuration value (`config_value`). See [Result Sets](#ResultSets) for definitions of `run_value` and `config_value`. The new configuration value that is set by `sp_configure` does not become effective until the run value is set by the `RECONFIGURE` statement. Then, after running `RECONFIGURE`, you must stop and restart the SQL Server service. 

> [!IMPORTANT]
> Note that when stopping the SQL Server service, the two additional services will also automatically stop: PolyBase Engine and Data Movement Service. After restarting the SQL Server engine service, manually start these two services again, as they won't start automatically.  
  
## Return code values  
 0 (success) or 1 (failure)  
  
##  <a name="ResultSets"></a> Result Sets  
 When executed with no parameters, `sp_configure` returns a result set with five columns.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**nvarchar(35)**|Name of the configuration option.|  
|**minimum**|**int**|Minimum value of the configuration option.|  
|**maximum**|**int**|Maximum value of the configuration option.|  
|**config_value**|**int**|Value that was set using **sp_configure**.|  
|**run_value**|**int**|Current value in use by PolyBase. This value is set by running RECONFIGURE.<br /><br /> The **config_value** and **run_value** are usually the same unless the value is in the process of being changed.<br /><br /> A restart might be required before this run value is accurate, if the reconfiguration is in progress.|  
  
## General remarks  
 In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], after running `RECONFIGURE`, for the run value of the 'hadoop connectivity' to take effect, you need to restart [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  

In [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], after running `RECONFIGURE`, for the run value of the 'hadoop connectivity' to take effect, you need to restart the [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] region.  

Starting in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], Hadoop is no longer supported in PolyBase.
  
## Limitations and restrictions  
 RECONFIGURE is not allowed in an explicit or implicit transaction.  
  
## Permissions  
 All users can execute `sp_configure` with no parameters or the @configname parameter.  
  
 Requires `ALTER SETTINGS` server-level permission or membership in the **sysadmin** fixed server role to change a configuration value or to run `RECONFIGURE`.  
  
## Examples  
  
### A. List all available configuration settings  
 The following example shows how to list all configuration options.  
  
```tsql  
EXEC sp_configure;  
```  
  
 The result returns the option name followed by the minimum and maximum values for the option. The **config_value** is the value that PolyBase will use when reconfiguration is complete. The **run_value** is the value that is currently being used. The **config_value** and **run_value** are usually the same unless the value is in the process of being changed.  
  
### B. List the configuration settings for one configuration name  
  
```tsql  
EXEC sp_configure @configname='hadoop connectivity';  
```  
  
### C. Set Hadoop connectivity  
 This example sets PolyBase to option 7. This option allows PolyBase to create and use external tables on Hortonworks HDP 2.1, 2.2, and 2.3 on Linux and Windows Server, and Azure Blob Storage. For example, SQL could have 30 external tables with 7 of them referencing data on Hortonworks HDP 2.1 on Linux, 4 on Hortonworks HDP 2.2 on Linux, 7 on Hortonworks HDP 2.3 on Linux, and the other 12 referencing Azure Blob Storage.  
  
```tsql
--Configure external tables to reference data on Hortonworks HDP 2.1, 2.2, and 2.3 on Linux, and Azure Blob Storage  
  
sp_configure @configname = 'hadoop connectivity', @configvalue = 7;  
GO  
  
RECONFIGURE  
GO  
```  
  
## Next steps 

 - [What is PolyBase?](../../relational-databases/polybase/polybase-guide.md)
 - [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)   
 - [CREATE EXTERNAL TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-table-transact-sql.md)   
 - [CREATE EXTERNAL FILE FORMAT &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-file-format-transact-sql.md)   
 - [CREATE EXTERNAL DATA SOURCE &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-data-source-transact-sql.md)   
