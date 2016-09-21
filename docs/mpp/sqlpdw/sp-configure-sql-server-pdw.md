---
title: "sp_configure (SQL Server PDW)"
ms.custom: na
ms.date: 08/10/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: a8252a60-0036-4608-b447-2101018862e2
caps.latest.revision: 19
author: BarbKess
---
# sp_configure (SQL Server PDW)
Displays or changes global configuration settings for SQL Server PDW. Only one configuration change (**hadoop connectivity**) is currently supported by SQL Server PDW.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
--List all of the configuration options  
sp_configure  
[;]  
  
--Configure Hadoop connectivity  
sp_configure [ @configname= ] 'hadoop connectivity',  
             [ @configvalue = ] { 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 }  
[;]  
RECONFIGURE  
[;]  
```  
  
## Arguments  
[ **@configname=** ] **'***option_name***'**  
Is the name of a configuration option. *option_name* is **varchar(35)**, with a default of NULL. If not specified, the complete list of options is returned.  
  
[ **@configvalue=** ] **'***value***'**  
Is the new configuration setting. *value* is **int**, with a default of NULL. The maximum value depends on the individual option.  
  
**'hadoop connectivity'**  
Specifies the type of Hadoop data source for all connections from SQL Server PDW to Hadoop clusters or Microsoft Azure blob storage (WASB). This setting is required in order to create an external data source for an external table. For more information, see [CREATE EXTERNAL DATA SOURCE &#40;SQL Server PDW&#41;](../sqlpdw/create-external-data-source-sql-server-pdw.md),  
  
These are the Hadoop connectivity settings and their corresponding supported Hadoop data sources. Only one setting can be in effect at a time. Options 1, 4, and 7 allow multiple types of external data sources to be created and used.  
  
-   Option 0: Disable Hadoop connectivity  
  
-   Option 1: Hortonworks (HDP 1.3) for Microsoft Server  
  
-   Option 1: HDInsight on Analytics Platform System (version AU1) (HDP 1.3)  
  
-   Option 1: Azure blob storage on Microsoft Azure (WASB[S]) (only applies to AU1)  
  
-   Option 2: Hortonworks (HDP 1.3) for Linux  
  
-   Option 3: Cloudera CDH 4.3 for Linux  
  
-   Option 4: Hortonworks (HDP 2.0) for Windows Server  
  
-   Option 4: HDInsight on Analytics Platform System (version AU2) (HDP 2.0)  
  
-   Option 4: Azure blob storage on Microsoft Azure (WASB[S])  
  
-   Option 5: Hortonworks (HDP 2.0) for Linux  
  
-   Option 6: Cloudera 5.1 on Linux  
  
-   Option 7: Hortonworks 2.1 and 2.2 on Linux  
  
-   Option 7: Hortonworks 2.2 on Windows Server  
  
-   Option 7: Azure blob storage (WASB[S]  
  
For the complete instructions on configuring Hadoop connectivity, including when and how to restart the PDW region, see [Configure PolyBase Connectivity to External Data &#40;Analytics Platform System&#41;](../management/configure-polybase-connectivity-to-external-data-analytics-platform-system.md).  
  
**RECONFIGURE**  
Updates the run value (run_value) to match the configuration value (config_value). See [Result Sets](#ResultSets)for definitions of run_value and config_value. The new configuration value that is set by sp_configure does not become effective until the run value is set by the RECONFIGURE statement. After running RECONFIGURE, some changes require a restart of the PDW region.  
  
## Return Code Values  
0 (success) or 1 (failure)  
  
## <a name="ResultSets"></a>Result Sets  
When executed with no parameters, **sp_configure** returns a result set with five columns.  
  
|Column name|Data type|Description|  
|---------------|-------------|---------------|  
|**name**|**nvarchar(35)**|Name of the configuration option.|  
|**minimum**|**int**|Minimum value of the configuration option.|  
|**maximum**|**int**|Maximum value of the configuration option.|  
|**config_value**|**int**|Value that was set using **sp_configure**.|  
|**run_value**|**int**|Current value in use by PDW. This value is set by running RECONFIGURE.<br /><br />The **config_value** and **run_value** are usually the same unless the value is in the process of being changed.<br /><br />A restart might be required before this run value is accurate, if the reconfiguration is in progress.|  
  
## General Remarks  
  
### Configuration Settings that Require a Restart  
Some changes require a restart of the PDW region before the run value will take effect. To restart the PDW region, [Launch the Configuration Manager &#40;Analytics Platform System&#41;](../management/launch-the-configuration-manager-analytics-platform-system.md) and follow the on-screen instructions.  
  
These configuration settings require a restart after running RECONFIGURE.  
  
-   'hadoop connectivity'  
  
### Configuration Settings for Internal Use  
These configuration names are for internal use only and should only be changed by Microsoft customer support.  
  
-   'redistribute mode'. The config_value is 0 by default. It changes to 1 when Microsoft customer support is re-distributing your data after adding scale units to the PDW region in the appliance.  
  
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
  
The result returns the option name followed by the minimum and maximum values for the option. The **config_value** is the value that SQL Server PDW will use when reconfiguration is complete. The **run_value** is the value that is currently being used. The **config_value** and **run_value** are usually the same unless the value is in the process of being changed.  
  
### B. List the configuration settings for one configuration name  
  
```  
EXEC sp_configure @configname='hadoop connectivity';  
```  
  
### C. Set Hadoop connectivity  
Setting Hadoop connectivity requires a few more steps in addition to running sp_configure. For the full procedure, see [Configure PolyBase Connectivity to External Data &#40;Analytics Platform System&#41;](../management/configure-polybase-connectivity-to-external-data-analytics-platform-system.md)  
  
