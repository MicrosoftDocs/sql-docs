---
title: "sysdac_history_internal (Transact-SQL)"
description: Data-tier Application Tables - sysdac_history_internal
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sysdac_history_internal"
  - "sysdac_history_internal_TSQL"
helpviewer_keywords:
  - "sysdac_history_internal"
dev_langs:
  - "TSQL"
ms.assetid: 774a1678-0b27-42be-8adc-a6d7a4a56510
---
# Data-tier Application Tables - sysdac_history_internal
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Contains information about the actions taken to manage data-tier applications (DAC). This table is stored in the **dbo** schema of the **msdb** database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**action_id**|**int**|Identifier of the action|  
|**sequence_id**|**int**|Identifies a step within an action.|  
|**instance_id**|**uniqueidentifier**|Identifier of the DAC instance. This column can be joined on the **instance_id** column in [dbo.sysdac_instances &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/data-tier-application-views-dbo-sysdac-instances.md).|  
|**action_type**|**tinyint**|Identifier of the action type:<br /><br /> **0** = deploy<br /><br /> **1** = create<br /><br /> **2** = rename<br /><br /> **3** = detach<br /><br /> **4** = delete|  
|**action_type_name**|**varchar(19)**|Name of the action type:<br /><br /> **deploy**<br /><br /> **create**<br /><br /> **rename**<br /><br /> **detach**<br /><br /> **delete**|  
|**dac_object_type**|**tinyint**|Identifier of the type of object affected by the action:<br /><br /> **0** = dacpac<br /><br /> **1** = login<br /><br /> **2** = database|  
|**dac_object_type_name**|**varchar(8)**|Name of the type of object affected by the action:<br /><br /> **dacpac** = DAC instance<br /><br /> **login**<br /><br /> **database**|  
|**action_status**|**tinyint**|Code identifying the current status of the action:<br /><br /> **0** = pending<br /><br /> **1** = success<br /><br /> **2** = fail|  
|**action_status_name**|**varchar(11)**|Current status of the action:<br /><br /> **pending**<br /><br /> **success**<br /><br /> **fail**|  
|**Required**|**bit**|Used by the [!INCLUDE[ssDE](../../includes/ssde-md.md)] when rolling back a DAC operation.|  
|**dac_object_name_pretran**|**sysname**|Name of the object before the transaction containing the action is committed. Used only for databases and logins.|  
|**dac_object_name_posttran**|**sysname**|Name of the object after the transaction containing the action is committed. Used only for databases and logins.|  
|**sqlscript**|**nvarchar(max)**|[!INCLUDE[tsql](../../includes/tsql-md.md)] script that implements an action on a database or login.|  
|**payload**|**varbinary(max)**|DAC package definition saved in a binary encoded string.|  
|**Comments**|**varchar(max)**|Records the login of a user who accepted potential data loss in a DAC upgrade.|  
|**error_string**|**nvarchar(max)**|Error message generated if the action encounters an error.|  
|**created_by**|**sysname**|The login that launched the action that created this entry.|  
|**date_created**|**datetime**|The date and time this entry was created.|  
|**date_modified**|**datetime**|The date and time the entry was last modified.|  
  
## Remarks  
 DAC management actions, such as deploying or deleting a DAC, generate multiple steps. Each action is assigned an action identifier. Each step is assigned a sequence number and a row in **sysdac_history_internal**, where the status of the step is recorded. Each row is created when the action step starts, and is updated as needed to reflect the status of the operation. For example, a deploy DAC action could be assigned **action_id** 12 and get four rows in **sysdac_history_internal**:  
  
| action_id | sequence_id | action_type_name | dac_object_type_name |
| --------- | ----------- | ---------------- | -------------------- |
|12|0|create|dacpac|  
|12|1|create|login|  
|12|2|create|database|  
|12|3|rename|database|  
  
 DAC operations, such as delete, do not remove rows from **sysdac_history_internal**. You can use the following query to manually delete the rows for DACs no longer deployed on an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]:  
  
```sql  
DELETE FROM msdb.dbo.sysdac_history_internal  
WHERE instance_id NOT IN  
   (SELECT instance_id  
    FROM msdb.dbo.sysdac_instances_internal);  
```  
  
 Deleting rows for active DACs does not impact DAC operations; the only impact is that you will not be able to report the full history for the DAC.  
  
> [!NOTE]  
>  Currently, there is no mechanism for deleting **sysdac_history_internal** rows on [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].  
  
## Permissions  
 Requires membership in the sysadmin fixed server role. Read-only access to this view is available to all users with permissions to connect to the master database.  
  
## See Also  
 [Data-tier Applications](../../relational-databases/data-tier-applications/data-tier-applications.md)   
 [dbo.sysdac_instances &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/data-tier-application-views-dbo-sysdac-instances.md)   
 [sysdac_instances_internal &#40;Transact-SQL&#41;](../../relational-databases/system-tables/data-tier-application-tables-sysdac-instances-internal.md)  
  
  
