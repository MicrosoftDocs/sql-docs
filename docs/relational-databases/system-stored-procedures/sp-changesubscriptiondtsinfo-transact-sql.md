---
title: "sp_changesubscriptiondtsinfo (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_changesubscriptiondtsinfo"
  - "sp_changesubscriptiondtsinfo_TSQL"
helpviewer_keywords: 
  - "sp_changesubscriptiondtsinfo"
ms.assetid: 64fc085f-f81b-493b-b59a-ee6192d9736d
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_changesubscriptiondtsinfo (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Changes the Data Transformation Services (DTS) package properties of a subscription. This stored procedure is executed at the Subscriber on the subscription database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_changesubscriptiondtsinfo [ [ @job_id = ] job_id ]  
    [ , [ @dts_package_name= ] 'dts_package_name' ]  
    [ , [ @dts_package_password= ] 'dts_package_password' ]  
    [ , [ @dts_package_location= ] 'dts_package_location' ]  
```  
  
## Arguments  
`[ @job_id = ] job_id`
 Is the job ID of the Distribution Agent for the push subscription. *job_id* is **varbinary(16)**, with no default. To find the Distribution Job ID, run **sp_helpsubscription** or **sp_helppullsubscription**.  
  
`[ @dts_package_name = ] 'dts_package_name'`
 Specifies the name of the DTS package. *dts_package_name* is a **sysname**, with a default of NULL. For example, to specify a package named **DTSPub_Package**, you would specify `@dts_package_name = N'DTSPub_Package'`.  
  
`[ @dts_package_password = ] 'dts_package_password'`
 Specifies the password on the package. *dts_package_password* is **sysname** with a default of NULL, which specifies that the password property is to be left unchanged.  
  
> [!NOTE]  
>  A DTS package must have a password.  
  
`[ @dts_package_location = ] 'dts_package_location'`
 Specifies the package location. *dts_package_location* is a **nvarchar(12)**, with a default of NULL, which specifies that the package location is to be left unchanged. The location of the package can be changed to **distributor** or **subscriber**.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_changesubscriptiondtsinfo** is used for snapshot replication and transactional replication that are push subscriptions only.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role, **db_owner** fixed database role, or the creator of the subscription can execute **sp_changesubscriptiondtsinfo**.  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
