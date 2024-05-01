---
title: "Publication Database"
description: "Publication Database"
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: replication
ms.topic: reference
ms.custom: updatefrequency5
f1_keywords:
  - "sql13.rep.newpubwizard.publicationdatabase.f1"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Publication Database
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
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
  
  
