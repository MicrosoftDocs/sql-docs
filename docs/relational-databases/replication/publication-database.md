---
title: "Publication Database | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.newpubwizard.publicationdatabase.f1"
ms.assetid: a9fafc9b-9963-4b59-97a0-3472158fa665
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Publication Database
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The publication database is the database on the Publisher that is the source of data and database objects to be replicated. Each publication database used in replication must be enabled. The database is enabled when a member of the **sysadmin** fixed server role:  
  
-   Creates a publication on that database using the New Publication Wizard.  
  
-   Selects the database in the **Publisher Properties** dialog box.  
  
-   Executes **sp_replicationdboption** and sets the option **publish** (for snapshot or transactional publications) or **merge publish** (for merge publications) to **True**.  
  
## Options  
 **Databases**  
 Select the name of the database that contains the data and database objects that you want to publish.  
  
## See Also  
 [Publish Data and Database Objects](../../relational-databases/replication/publish/publish-data-and-database-objects.md)   
 [Create a Publication](../../relational-databases/replication/publish/create-a-publication.md)   
 [View and Modify Distributor and Publisher Properties](../../relational-databases/replication/view-and-modify-distributor-and-publisher-properties.md)   
 [sp_replicationdboption &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-replicationdboption-transact-sql.md)  
  
  
