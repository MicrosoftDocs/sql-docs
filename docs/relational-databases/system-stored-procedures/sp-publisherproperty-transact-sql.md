---
description: "sp_publisherproperty (Transact-SQL)"
title: "sp_publisherproperty (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_publisherproperty"
  - "sp_publisherproperty_TSQL"
helpviewer_keywords: 
  - "sp_publisherproperty"
ms.assetid: 0ed1ebc1-a1bd-4aed-9f46-615c5cf07827
author: markingmyname
ms.author: maghan
---
# sp_publisherproperty (Transact-SQL)
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  Displays or changes publisher properties for non- [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publishers. This stored procedure is executed at the Distributor.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_publisherproperty [ @publisher = ] 'publisher'   
   [ , [ @propertyname = ] 'propertyname' ]   
   [ , [ @propertyvalue = ] 'propertyvalue' ]  
```  
  
## Arguments  
`[ @publisher = ] 'publisher'`
 Is the name of the heterogeneous Publisher. *publisher* is **sysname**, with no default.  
  
`[ @propertyname = ] 'propertyname'`
 Is the name of the property being set. *propertyname* is **sysname**, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**xactsetbatching**|If transactions at the Publisher are grouped into transactionally consistent sets for subsequent processing, known as Xactsets. A value of **enabled** means that Xactsets can be created, which is the default. A value of **disabled** means that existing Xactsets are processed by no new Xactsets are created.|  
|**xactsetjob**|If the Xactset job is enabled for the creation of Xactsets. A value of **enabled** means that the Xactset job runs periodically to create Xactsets at the publisher. A value of **disabled** means that the Xactsets are only created by the Log Reader Agent when it polls the Publisher for changes.|  
|**xactsetjobinterval**|Interval between executions of the Xactset job, in minutes.|  
  
 When *propertyname* is omitted all settable properties are returned.  
  
 `[ @propertyvalue = ] 'propertyvalue'`  
 Is the new value for the property setting. *propertyvalue* is **sysname**, with a default value of NULL. When *propertyvalue* is omitted, the current setting for the property is returned.  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**propertyname**|**sysname**|Returns the following publication properties that can be set:<br /><br /> **xactsetbatching**<br /><br /> **xactsetjob**<br /><br /> **xactsetjobinterval**|  
|**propertyvalue**|**sysname**|Is the current setting for the property in the **propertyname** column.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_publisherproperty** is used in transactional replication for non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publishers.  
  
 When only *publisher* is specified, the result set includes the current settings for all properties that can be set.  
  
 When *propertyname* is specified, only the named property appears in the result set.  
  
 When all parameters are specified, the property is changed and a result set is not returned.  
  
 When changing the **xactsetjobinterval** property for a running job, you must restart the job for the new interval to take effect.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role at the Distributor can execute **sp_publisherproperty**.  
  
## See Also  
 [Configure the Transaction Set Job for an Oracle Publisher &#40;Replication Transact-SQL Programming&#41;](../../relational-databases/replication/administration/configure-the-transaction-set-job-for-an-oracle-publisher.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
