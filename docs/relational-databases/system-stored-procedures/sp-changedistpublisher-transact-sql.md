---
title: "sp_changedistpublisher (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
applies_to: 
  - "SQL Server"
f1_keywords: 
  - "sp_changedistpublisher_TSQL"
  - "sp_changedistpublisher"
helpviewer_keywords: 
  - "sp_changedistpublisher"
ms.assetid: 7ef5c89d-faaa-4f8e-aef7-00649ebc8bc9
caps.latest.revision: 36
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# sp_changedistpublisher (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Changes the properties of the distribution Publisher. This stored procedure is executed at the Distributor on any database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_changedistpublisher [ @publisher = ] 'publisher'  
    [ , [ @property = ] 'property' ]  
    [ , [ @value = ] 'value' ]  
```  
  
## Arguments  
 [ **@publisher=** ] **'***publisher***'**  
 Is the Publisher name. *publisher* is **sysname**, with no default.  
  
 [ **@property=** ] **'***property***'**  
 Is a property to change for the given Publisher. *property* is **sysname** and can be one of these values.  
  
 [ **@value=** ] **'***value***'**  
 Is the value for the given property. *value* is **nvarchar(255)**, with a default of NULL.  
  
 This table describes the properties of Publishers and the values for those properties.  
  
|Property|Values|Description|  
|--------------|------------|-----------------|  
|**active**|**true**|Activates the Publisher.|  
||**false**|Deactivates the publisher|  
|**distribution_db**||Name of the distribution database.|  
|**login**||Login name.|  
|**password**||Strong password for the supplied login.|  
|**security_mode**|**1**|Use Windows Authentication when connecting to the Publisher. *This cannot be changed for a non-*[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] *publisher.*|  
||**0**|Use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication when connecting to the Publisher. *This cannot be changed for a non-*[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] *publisher.*|  
|**working_directory**||Working directory used to store data and schema files for the publication.|  
|NULL (default)||All available *property* options are printed.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_changedistpublisher** is used in all types of replication.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_changedistpublisher**.  
  
## See Also  
 [View and Modify Distributor and Publisher Properties](../../relational-databases/replication/view-and-modify-distributor-and-publisher-properties.md)   
 [sp_adddistpublisher &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-adddistpublisher-transact-sql.md)   
 [sp_dropdistpublisher &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropdistpublisher-transact-sql.md)   
 [sp_helpdistpublisher &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpdistpublisher-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  