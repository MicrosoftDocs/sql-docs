---
title: "PolyBase Connectivity Configuration (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, pdw"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "PolyBase"
ms.assetid: 82252e4f-b1d0-49e5-aa0b-3624aade2add
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017"
---
# PolyBase Connectivity Configuration (Transact-SQL)
[!INCLUDE[appliesto-ss-xxxx-xxxx-pdw-md](../../includes/appliesto-ss-xxxx-xxxx-pdw-md.md)]

  Displays or changes global configuration settings for PolyBase Hadoop and Azure blob storage connectivity.
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
--List all of the configuration options  
sp_configure  
[;]  
  
--Configure Hadoop connectivity  
sp_configure [ @configname = ] 'hadoop connectivity',  
             [ @configvalue = ] { 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 }  
[;]  
  
RECONFIGURE  
[;]  
```  
  
## Arguments  
 [ **@configname=** ] **'**_option\_name_**'**  
 Is the name of a configuration option. *option_name* is **varchar(35)**, with a default of NULL. If not specified, the complete list of options is returned.  
  
 [ **@configvalue=** ] **'**_value_**'**  
 Is the new configuration setting. *value* is **int**, with a default of NULL. The maximum value depends on the individual option.  
  
 **'hadoop connectivity'**  
 Specifies the type of Hadoop data source for all connections from PolyBase to Hadoop clusters or Azure blob storage (WASB). This setting is required in order to create an external data source for an external table. For more information, see [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md),  
  
 These are the Hadoop connectivity settings and their corresponding supported Hadoop data sources. Only one setting can be in effect at a time. Options 1, 4, and 7 allow multiple types of external data sources to be created and used across all sessions on the server.  
  
-   Option 0: Disable Hadoop connectivity  
  
-   Option 1: Hortonworks HDP 1.3 on Windows Server  
  
-   Option 1: Azure blob storage (WASB[S])  
  
-   Option 2: Hortonworks HDP 1.3 on Linux  
  
-   Option 3: Cloudera CDH 4.3 on Linux  
  
-   Option 4: Hortonworks HDP 2.0 on Windows Server  
  
-   Option 4: Azure blob storage (WASB[S])  
  
-   Option 5: Hortonworks HDP 2.0 on Linux  
  
-   Option 6: Cloudera 5.1, 5.2, 5.3, 5.4, 5.5, 5.9, 5.10, 5.11, 5.12, and 5.13 on Linux  
  
-   Option 7: Hortonworks 2.1, 2.2, 2.3, 2.4, 2.5, 2.6, 3.0 on Linux  
  
-   Option 7: Hortonworks 2.1, 2.2, and 2.3 on Windows Server  
  
-   Option 7: Azure blob storage (WASB[S])  
  
 **RECONFIGURE**  
 Updates the run value (run_value) to match the configuration value (config_value). See [Result Sets](#ResultSets) for definitions of run_value and config_value. The new configuration value that is set by sp_configure does not become effective until the run value is set by the RECONFIGURE statement.  
  
 After running RECONFIGURE, you must stop and restart the SQL Server service. Note that when stopping the SQL Server service, the two additional PolyBase Engine and Data Movement Service will automatically stop. After restarting the SQL Server engine service, re-start these two services again (they won't start automatically).  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
##  <a name="ResultSets"></a> Result Sets  
 When executed with no parameters, **sp_configure** returns a result set with five columns.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**nvarchar(35)**|Name of the configuration option.|  
|**minimum**|**int**|Minimum value of the configuration option.|  
|**maximum**|**int**|Maximum value of the configuration option.|  
|**config_value**|**int**|Value that was set using **sp_configure**.|  
|**run_value**|**int**|Current value in use by PolyBase. This value is set by running RECONFIGURE.<br /><br /> The **config_value** and **run_value** are usually the same unless the value is in the process of being changed.<br /><br /> A restart might be required before this run value is accurate, if the reconfiguration is in progress.|  
  
## General Remarks  
 In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], after running RECONFIGURE, for the run value of the 'hadoop connectivity' to take effect, you need to restart [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
In [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], after running RECONFIGURE, for the run value of the 'hadoop connectivity' to take effect, you need to restart the [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] region.  
  
## Limitations and Restrictions  
 RECONFIGURE is not allowed in an explicit or implicit transaction.  
  
## Permissions  
 All users can execute **sp_configure** with no parameters or the @configname parameter.  
  
 Requires **ALTER SETTINGS** server-level permission or membership in the **sysadmin** fixed server role to change a configuration value or to run RECONFIGURE.  
  
## Examples  
  
### A. List all available configuration settings  
 The following example shows how to list all configuration options.  
  
```  
EXEC sp_configure;  
```  
  
 The result returns the option name followed by the minimum and maximum values for the option. The **config_value** is the value that SQL, or PolyBase, will use when reconfiguration is complete. The **run_value** is the value that is currently being used. The **config_value** and **run_value** are usually the same unless the value is in the process of being changed.  
  
### B. List the configuration settings for one configuration name  
  
```  
EXEC sp_configure @configname='hadoop connectivity';  
```  
  
### C. Set Hadoop connectivity  
 This example sets PolyBase to option 7. This option allows PolyBase to create and use external tables on Hortonworks 2.1, 2.2, and 2.3 on Linux and Windows Server, and Azure blob storage. For example, SQL could have 30 external tables with 7 of them referencing data on Hortonworks 2.1 on Linux, 4 on Hortonworks 2.2 on Linux, 7 on Hortonworks 2.3 on Linux, and the other 12 referencing Azure blob storage.  
  
```  
--Configure external tables to reference data on Hortonworks 2.1, 2.2, and 2.3 on Linux, and Azure blob storage  
  
sp_configure @configname = 'hadoop connectivity', @configvalue = 7;  
GO  
  
RECONFIGURE  
GO  
```  
  
## See Also  
 [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)   
 [CREATE EXTERNAL TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-table-transact-sql.md)   
 [CREATE EXTERNAL FILE FORMAT &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-file-format-transact-sql.md)   
 [CREATE EXTERNAL DATA SOURCE &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-data-source-transact-sql.md)  
  
  
