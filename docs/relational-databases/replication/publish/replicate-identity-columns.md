---
title: "Replicate Identity Columns | Microsoft Docs"
ms.custom: ""
ms.date: "10/04/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "identities [SQL Server replication]"
  - "identity values [SQL Server replication]"
  - "merge replication [SQL Server replication], identity range management"
  - "publishing [SQL Server replication], identity columns"
  - "transactional replication, identity range management"
  - "identity columns [SQL Server], replication"
ms.assetid: eb2f23a8-7ec2-48af-9361-0e3cb87ebaf7
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Replicate Identity Columns
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  When you assign an IDENTITY property to a column, [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] automatically generates sequential numbers for new rows inserted in the table containing the identity column. For more information, see [IDENTITY &#40;Property&#41; &#40;Transact-SQL&#41;](../../../t-sql/statements/create-table-transact-sql-identity-property.md). Because identity columns might be included as a part of the primary key, it is important to avoid duplicate values in the identity columns. To use identity columns in a replication topology that has updates at more than one node, each node in the replication topology must use a different range of identity values, so that duplicates do not occur.  
  
 For example, the Publisher could be assigned the range 1-100, Subscriber A the range 101-200, and Subscriber B the range 201-300. If a row is inserted at the Publisher and the identity value is, for example, 65, that value is replicated to each Subscriber. When replication inserts data at each Subscriber, it does not increment the identity column value in the Subscriber table; instead, the literal value 65 is inserted. Only user inserts, but not replication agent inserts cause the identity column value to be incremented.  
  
 Replication handles identity columns across all publication and subscription types, allowing you to manage the columns manually or have replication manage them automatically.  
  
> [!NOTE]  
>  Adding an identity column to a published table is not supported, because it can result in non-convergence when the column is replicated to the Subscriber. The values in the identity column at the Publisher depend on the order in which the rows for the affected table are physically stored. The rows might be stored differently at the Subscriber; therefore the value for the identity column can be different for the same rows.  
  
## Specifying an Identity Range Management Option  
 Replication offers three identity range management options:  
  
-   Automatic. Used for merge replication and transactional replication with updates at the Subscriber. Specify size ranges for the Publisher and Subscribers, and replication automatically manages the assignment of new ranges. Replication sets the NOT FOR REPLICATION option on the identity column at the Subscriber, so that only user inserts cause the value to be incremented at the Subscriber.  
  
    > [!NOTE]  
    >  Subscribers must synchronize with the Publisher to receive new ranges. Because Subscribers are assigned identity ranges automatically, it is possible for any Subscriber to exhaust the entire supply of identity ranges if it repeatedly requests new ranges.  
  
-   Manual. Used for snapshot and transactional replication without updates at the Subscriber, peer-to-peer transactional replication, or if your application must control identity ranges programmatically. If you specify manual management, you must ensure that ranges are assigned to the Publisher and each Subscriber and that new ranges are assigned if the initial ranges are used. Replication sets the NOT FOR REPLICATION option on the identity column at the Subscriber.  
  
-   None. This option is recommended only for backwards compatibility with earlier versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] and is available only from the stored procedure interface for transactional publications.  
  
 To specify an identity range management option, see [Manage Identity Columns](../../../relational-databases/replication/publish/manage-identity-columns.md).  
  
## Assigning Identity Ranges  
 Merge replication and transactional replication use different methods for assigning ranges; these methods are described in this section.  
  
 There are two types of ranges to take into account when replicating identity columns: the ranges assigned to the Publisher and Subscribers, and the range of the data type in the column. The following table shows the ranges available for the data types typically used in identity columns. The range is used across all nodes in a topology. For example, if you use **smallint** starting at 1 with an increment of 1, the maximum number of inserts is 32,767 for the Publisher and all Subscribers. The actual number of inserts depends on whether there are gaps in the values used and whether a threshold value is used. For more information about thresholds, see the following sections "Merge Replication" and "Transactional Replication with Queued Updating Subscriptions".  
  
 If the Publisher exhausts its identity range after an insert, it can automatically assign a new range if the insert was performed by a member of the **db_owner** fixed database role. If the insert was performed by a user not in that role, the Log Reader Agent, Merge Agent, or a user who is a member of the **db_owner** role must run [sp_adjustpublisheridentityrange &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-adjustpublisheridentityrange-transact-sql.md). For transactional publications, the Log Reader Agent must be running to automatically allocate a new range (the default is for the agent to run continuously).  
  
> [!WARNING]  
>  During a large batch insert the replication trigger is fired only once, not for each row of the insert. This can lead to a failure of the insert statement if an identity range is exhausted during an large insert, such as an **INSERT INTO** statement.  
  
|Data type|Range|  
|---------------|-----------|  
|**tinyint**|Not supported for automatic management|  
|**smallint**|-2^15 (-32,768) to 2^15-1 (32,767)|  
|**int**|-2^31 (-2,147,483,648) to 2^31-1 (2,147,483,647)|  
|**bigint**|-2^63 (-9,223,372,036,854,775,808) to 2^63-1 (9,223,372,036,854,775,807)|  
|**decimal** and **numeric**|-10^38+1 through 10^38-1|  
  
> [!NOTE]  
>  To create an automatically incrementing number that can be used in multiple tables or that can be called from applications without referencing any table, see [Sequence Numbers](../../../relational-databases/sequence-numbers/sequence-numbers.md).  
  
### Merge Replication  
 Identity ranges are managed by the Publisher and propagated to Subscribers by the Merge Agent (in a republishing hierarchy, ranges are managed by the root Publisher and the republishers). The identity values are assigned from a pool at the Publisher. When you add an article with an identity column to a publication in the New Publication Wizard or by using [sp_addmergearticle &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md), you specify values for:  
  
-   The **@identity_range** parameter, which controls the identity range size initially allocated both to the Publisher and to Subscribers with client subscriptions.  
  
    > [!NOTE]  
    >  For Subscribers running previous versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], this parameter (rather than the **@pub_identity_range** parameter) also controls the identity range size at republishing Subscribers.  
  
-   The **@pub_identity_range** parameter, which controls the identity range size for republishing allocated to Subscribers with server subscriptions (required for republishing data). All Subscribers with server subscriptions receive a range for republishing, even if they don't actually republish data.  
  
-   The **@threshold** parameter, which is used to determine when a new range of identities is required for a subscription to [!INCLUDE[ssEW](../../../includes/ssew-md.md)] or a previous version of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
 For example, you could specify 10000 for **@identity_range** and 500000 for **@pub_identity_range**. The Publisher and all Subscribers running [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] or a later version, including the Subscriber with the server subscription, are assigned a primary range of 10000. The Subscriber with the server subscription is also assigned a primary range of 500000, which can be used by Subscribers that synchronize with the republishing Subscriber (you must also specify **@identity_range**, **@pub_identity_range**, and **@threshold** for the articles in the publication at the republishing Subscriber).  
  
 Each Subscriber running [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] or a later version also receives a secondary identity range. The secondary range is equal in size to the primary range; when the primary range is exhausted, the secondary range is used, and the Merge Agent assigns a new range to the Subscriber. The new range becomes the secondary range, and the process continues as the Subscriber uses identity values.  
  
  
### Transactional Replication with Queued Updating Subscriptions  
 Identity ranges are managed by the Distributor and propagated to Subscribers by the Distribution Agent. The identity values are assigned from a pool at the Distributor. The pool size is based on the size of the data type and the increment used for the identity column. When you add an article with an identity column to a publication in the New Publication Wizard or by using [sp_addarticle &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addarticle-transact-sql.md), you specify values for:  
  
-   The **@identity_range** parameter, which controls the identity range size initially allocated to all Subscribers.  
  
-   The **@pub_identity_range** parameter, which controls the identity range size allocated to the Publisher.  
  
-   The **@threshold** parameter, which is used to determine when a new range of identities is required for a subscription.  
  
 For example, you could specify 10000 for **@pub_identity_range**, 1000 for **@identity_range** (assuming fewer updates at the Subscriber), and 80 percent for **@threshold**. After 800 inserts at a Subscriber (80 percent of 1000), a Subscriber is assigned a new range. After 8000 inserts at the Publisher, the Publisher is assigned a new range. When a new range is assigned, there will be a gap in the identity range values in the table. Specifying a higher threshold results in smaller gaps, but the system is less fault-tolerant: if the Distribution Agent cannot run for some reason, a Subscriber could more easily run out of identities.  
  
## Assigning ranges for manual identity range management  
 If you specify manual identity range management, you must ensure that the Publisher and each Subscriber use different identity ranges. For example, consider a table at the Publisher with an identity column defined as `IDENTITY(1,1)`: the identity column starts at 1 and is incremented by 1 each time a row is inserted. If the table at the Publisher has 5,000 rows, and you expect some growth in the table over the life of the application, the Publisher could use the range 1-10,000. Given two Subscribers, Subscriber A could use 10,001–20,000, and Subscriber B could use 20,001-30,000.  
  
 After a Subscriber is initialized with a snapshot or through another means, execute DBCC CHECKIDENT to assign the Subscriber a starting point for its identity range. For example, at Subscriber A, you would execute `DBCC CHECKIDENT('<TableName>','reseed',10001)`. At Subscriber B, you would execute `CHECKIDENT('<TableName>','reseed',20001)`.  
  
 To assign new ranges to the Publisher or Subscribers, execute DBCC CHECKIDENT and specify a new value to reseed the table. You should have some way to determine when a new range must be assigned. For example, your application could have a mechanism that detects when a node is about to use up its range and assign a new range using DBCC CHECKIDENT. You can also add a check constraint to ensure that a row cannot be added if it would cause an out of range identity value to be used.  
  
## Handling Identity Ranges after a Database Restore  
 If you are using automatic identity range management, when a Subscriber is restored from a backup, it automatically requests a new range of identity values. If a Publisher is restored from a backup, you must ensure that the Publisher is assigned an appropriate range. For merge replication, assign a new range using [sp_restoremergeidentityrange &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-restoremergeidentityrange-transact-sql.md). For transactional replication, determine the highest value that has been used and then set the starting point for new ranges. Use the following procedure after the publication database has been restored:  
  
1.  Stop all activity on all Subscribers.  
  
2.  For each published table that includes an identity column:  
  
    1.  In the subscription database at each Subscriber, execute `IDENT_CURRENT('<TableName>')`.  
  
    2.  Record the highest value found across all Subscribers.  
  
    3.  In the publication database at the Publisher, execute `DBCC CHECKIDENT(<TableName>','reseed',<HighestValueFound+1>`).  
  
    4.  In the publication database at the Publisher, execute `sp_adjustpublisheridentityrange <PublicationName>, <TableName>`.  
  
    > [!NOTE]  
    >  If the value in the identity column is set to decrement rather than increment, record the lowest value found, and then reseed with that value.  
  
## See Also  
 [BACKUP &#40;Transact-SQL&#41;](../../../t-sql/statements/backup-transact-sql.md)   
 [DBCC CHECKIDENT &#40;Transact-SQL&#41;](../../../t-sql/database-console-commands/dbcc-checkident-transact-sql.md)   
 [IDENT_CURRENT &#40;Transact-SQL&#41;](../../../t-sql/functions/ident-current-transact-sql.md)   
 [IDENTITY &#40;Property&#41; &#40;Transact-SQL&#41;](../../../t-sql/statements/create-table-transact-sql-identity-property.md)   
 [sp_adjustpublisheridentityrange &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-adjustpublisheridentityrange-transact-sql.md)  
  
  
