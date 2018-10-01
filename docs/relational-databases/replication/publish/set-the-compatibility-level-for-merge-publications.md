---
title: "Set the Compatibility Level for Merge Publications | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "compatibility [SQL Server], replication"
  - "backward compatibility [SQL Server], replication"
  - "publications [SQL Server replication], backward compatibility"
ms.assetid: db47ac73-948b-4d77-b272-bb3565135ea5
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Set the Compatibility Level for Merge Publications
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to set the compatibility level for merge publications in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../../includes/tsql-md.md)]. Merge replication uses the publication compatibility level to determine which features can be used by publications in a given database.  
  
 **In This Topic**  
  
-   **To set the compatibility level for merge publications, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 Set the compatibility level on the **Subscriber Types** page of the New Publication Wizard. For more information on accessing this wizard, see [Create a Publication](../../../relational-databases/replication/publish/create-a-publication.md). After a publication snapshot is created, the compatibility level can be increased but cannot be decreased. Increase the compatibility level on the **General** page of the **Publication Properties - \<Publication>** dialog box. For more information about accessing this dialog box, see [View and Modify Publication Properties](../../../relational-databases/replication/publish/view-and-modify-publication-properties.md). If you increase the publication compatibility level, any existing subscriptions at servers running versions prior to the compatibility level will no longer be able to synchronize.  
  
> [!NOTE]  
>  Because the compatibility level has implications for other publication properties and for which article properties are valid, do not change the compatibility level and other properties in the same use of the dialog box. The snapshot for the publication should be regenerated after the property is changed.  
  
#### To set the publication compatibility level  
  
-   On the **Subscriber Types** page of the New Publication Wizard, select the types of Subscribers that the publication should support.  
  
#### To increase the publication compatibility level  
  
-   On the **General** page of the **Publication Properties - \<Publication>** dialog box, select for **Compatibility level**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 The compatibility level for a merge publication can either be set programmatically when a publication is created or modified programmatically at a later time. You can use replication stored procedures to set or change this publication property.  
  
#### To set the publication compatibility level for a merge publication  
  
1.  At the Publisher, execute [sp_addmergepublication &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addmergepublication-transact-sql.md), specifying a value for **@publication_compatibility_level** to make the publication compatible with older versions of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. For more information, see [Create a Publication](../../../relational-databases/replication/publish/create-a-publication.md).  
  
#### To change the publication compatibility level of a merge publication  
  
1.  Execute [sp_changemergepublication &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-changemergepublication-transact-sql.md), specifying **publication_compatibility_level** for **@property** and the appropriate publication compatibility level for **@value**.  
  
#### To determine the publication compatibility level of a merge publication  
  
1.  Execute [sp_helpmergepublication &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-helpmergepublication-transact-sql.md), specifying the desired publication.  
  
2.  Locate the publication compatibility level in the **backward_comp_level** column in the result set.  
  
###  <a name="TsqlExample"></a> Examples (Transact-SQL)  
 This example creates a merge publication and sets the publication compatibility level.  
  
```  
-- To avoid storing the login and password in the script file, the values   
-- are passed into SQLCMD as scripting variables. For information about   
-- how to use scripting variables on the command line and in SQL Server  
-- Management Studio, see the "Executing Replication Scripts" section in  
-- the topic "Programming Replication Using System Stored Procedures".  
  
--Add a new merge publication.  
DECLARE @publicationDB AS sysname;  
DECLARE @publication AS sysname;  
DECLARE @login AS sysname;  
DECLARE @password AS sysname;  
SET @publicationDB = N'AdventureWorks2012';   
SET @publication = N'AdvWorksSalesOrdersMerge'   
SET @login = $(Login);  
SET @password = $(Password);  
  
-- Create a new merge publication.   
USE [AdventureWorks2012]  
EXEC sp_addmergepublication   
@publication = @publication,   
-- Set the compatibility level to SQL Server 2014.  
@publication_compatibility_level = '120RTM';   
  
-- Create the snapshot job for the publication.  
EXEC sp_addpublication_snapshot   
@publication = @publication,  
@job_login = @login,  
@job_password = @password;  
GO  
```  
  
 This example changes the publication compatibility level for the merge publication.  
  
> [!NOTE]  
>  Changing the publication compatibility level might not be allowed if the publication uses any features that require a particular compatibility level. For more information, see [Replication Backward Compatibility](../../../relational-databases/replication/replication-backward-compatibility.md).  
  
```  
DECLARE @publication AS sysname;  
SET @publication = N'AdvWorksSalesOrdersMerge' ;  
  
-- Change the publication compatibility level to   
-- SQL Server 2008 or later.  
EXEC sp_changemergepublication   
@publication = @publication,   
@property = N'publication_compatibility_level',   
@value = N'100RTM';  
GO  
  
```  
  
 This example returns the current publication compatibility level for the merge publication.  
  
```  
DECLARE @publication AS sysname;  
SET @publication = N'AdvWorksSalesOrdersMerge' ;  
EXEC sp_helpmergepublication   
@publication = @publication;  
GO  
  
```  
  
## See Also  
 [Create a Publication](../../../relational-databases/replication/publish/create-a-publication.md)  
  
  
