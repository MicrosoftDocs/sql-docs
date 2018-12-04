---
title: "Manage Identity Columns | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "identity values [SQL Server replication]"
  - "merge replication [SQL Server replication], identity range management"
  - "publishing [SQL Server replication], identity columns"
  - "transactional replication, identity range management"
  - "identity columns [SQL Server], replication"
ms.assetid: 98892836-cf63-494a-bd5d-6577d9810ddf
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Manage Identity Columns
  This topic describes how to manage identity columns in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../../includes/tsql-md.md)]. When Subscriber inserts are replicated back to the Publisher, identity columns must be managed to avoid assignment of the same identity value at both the Subscriber and Publisher. Replication can manage identity ranges automatically or you can choose to manually handle identity range management.  For information about the identity range management options provided by replication, see [Replicate Identity Columns](replicate-identity-columns.md).  
  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   When publishing a table in more than one publication, you must specify the same identity range management options for both publications. For more information, see "Publishing Tables in More Than One Publication" in [Publish Data and Database Objects](publish-data-and-database-objects.md).  
  
-   To create an automatically incrementing number that can be used in multiple tables or that can be called from applications without referencing any table, see [Sequence Numbers](../../sequence-numbers/sequence-numbers.md).  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 Specify an identity column management option on the **Properties** tab of the **Article Properties -\<Article>** dialog box of the New Publication Wizard. For more information about using this wizard, see [Create a Publication](create-a-publication.md). In the New Publication Wizard:  
  
-   If you select **Merge publication** or **Transactional publication with updating subscriptions** on the **Publication Type** page, select automatic or manual identity range management (automatic, the default, is recommended). After the table is published, the property cannot be modified, but other related properties can be modified.  
  
-   If you select other publication types, identity range management should be set to manual.  
  
 Modify identity ranges and thresholds on the **Properties** tab of the **Article Properties -\<Article>**, which is available in the **Publication Properties - \<Publication>** dialog box. For more information about accessing this dialog box, see [View and Modify Publication Properties](view-and-modify-publication-properties.md).  
  
#### To specify an identity column management option  
  
1.  If the Publisher is running a version of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] prior to [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], on the **Publication Type** page of the New Publication Wizard, select **Merge publication** or **Transactional publication with updating subscriptions**.  
  
2.  On the **Articles** page, select a table with an identity column.  
  
3.  Click **Article Properties**, and then click **Set Properties of Highlighted Table Article**.  
  
4.  On the **Properties** tab of the **Article Properties - \<Article>** dialog box, in the **Identity Range Management** section, set the **Automatically manage identity ranges** property to **Automatic** or **Manual** (for Publishers running [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] or later), or **True** or **False** (for Publishers running a version of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] prior to [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)]).  
  
5.  If you selected **Automatic** or **True** in step 4, enter values for the options in the following table. For more information on how these settings are used, see the "Assigning Identity Ranges" section of [Replicate Identity Columns](replicate-identity-columns.md).  
  
    |Option|Value|Description|  
    |------------|-----------|-----------------|  
    |**Publisher range size**|Integer value for range size (for example, 20000).|See the "Assigning Identity Ranges" section of [Replicate Identity Columns](replicate-identity-columns.md).|  
    |**Subscriber range size**|Integer value for range size (for example, 10000).|See the "Assigning Identity Ranges" section of [Replicate Identity Columns](replicate-identity-columns.md).|  
    |**Range threshold percentage**|Integer value for percent threshold (for example, 90 is equivalent to 90 percent).|Percent of total identity values used at a node before a new identity range is assigned.<br /><br /> Note: This value must be specified, but it is only used by: Subscribers using queued updating subscriptions; and Subscribers to merge publications running [!INCLUDE[ssEW](../../../includes/ssew-md.md)] or previous versions of other SQL Server editions. For more information, see the "Assigning Identity Ranges" section of [Replicate Identity Columns](replicate-identity-columns.md).|  
    |**Next range starting value**|Integer value. Read-only.|The value at which the next range will start. For example, if the current range is 5001-6000, this value will be 6001.|  
    |**Maximum identity value**|Integer value. Read-only.|The largest value for the identity column. Determined by the base data type of the column.|  
    |**Increment**|Integer value. Read-only.|The amount by which the number in the identity column should increase or decrease for each insert: typically set to 1.|  
  
6.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
#### To modify identity ranges and thresholds after a table is published  
  
1.  On the **Articles** page of the **Publication Properties - \<Publication>** dialog box, select a table with an identity column.  
  
2.  Click **Article Properties**, and then click **Set Properties of Highlighted Table Article**.  
  
3.  On the **Properties** tab of the **Article Properties - \<Article>** dialog box, in the **Identity Range Management** section, enter values for one or more of the following properties: **Publisher range size**, **Subscriber range size**, and **Range threshold percentage**.  
  
4.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
5.  Click **OK** on the **Publication Properties - \<Publication>** dialog box.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 You can use replication stored procedures to specify identity range management options when an article is created.  
  
#### To enable automatic identity range management when defining articles for a transactional publication  
  
1.  At the Publisher on the publication database, execute [sp_addarticle](/sql/relational-databases/system-stored-procedures/sp-addarticle-transact-sql). If the source table being published has an identity column, specify a value of **auto** for **@identityrangemanagementoption**, the range of identity values assigned to the Publisher for **@pub_identity_range**, the range of identity values assigned to each Subscribers for **@identity_range**, and the percent of total identity values used before a new identity range is assigned for **@threshold**. For more information about defining articles, see [Define an Article](define-an-article.md).  
  
    > [!NOTE]  
    >  Ensure that the data type of the identity column is large enough to support the total range of identities being assigned to all Subscribers.  
  
#### To disable automatic identity range management when defining articles for a transactional publication  
  
1.  At the Publisher on the publication database, execute [sp_addarticle](/sql/relational-databases/system-stored-procedures/sp-addarticle-transact-sql). Specify a value of **manual** for **@identityrangemanagementoption**. For more information about defining articles, see [Define an Article](define-an-article.md).  
  
2.  Assign ranges to identity article columns at the Subscriber to avoid generating conflicts for updating Subscribers. For more information, see the section on assigning ranges for manual identity range management in the topic [Replicate Identity Columns](replicate-identity-columns.md).  
  
#### To enable automatic identity range management when defining articles for a merge publication  
  
1.  At the Publisher on the publication database, execute [sp_addmergearticle](/sql/relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql). If the source table being published has an identity column, specify a value of **auto** for **@identityrangemanagementoption**, the range of identity values assigned to a server subscription for **@pub_identity_range**, the range of identity values assigned to the Publisher and each client subscription for **@identity_range**, and the percent of total identity values used before a new identity range is assigned for **@threshold**. For more information on when new identity ranges are assigned, see Assigning Identity Ranges in the topic [Replicate Identity Columns](replicate-identity-columns.md). For more information about defining articles, see [Define an Article](define-an-article.md).  
  
    > [!NOTE]  
    >  Ensure that the data type of the identity column is large enough to support the total range of identities being assigned to all Subscribers, particularly for Subscribers with server subscriptions.  
  
#### To disable automatic identity range management when defining articles for a merge publication  
  
1.  At the Publisher on the publication database, execute [sp_addmergearticle](/sql/relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql). Specify one of the following values for **@identityrangemanagementoption**:  
  
    -   **manual** - Identity ranges must be assigned manually for updating Subscribers.  
  
    -   **none** - Identity columns at the Publisher will not be defined as identity columns at the Subscriber.  
  
     For more information about defining articles, see [Define an Article](define-an-article.md).  
  
2.  Assign ranges to identity article columns at the Subscriber to avoid generating conflicts for updating Subscribers.  
  
#### To change automatic identity range management settings for an existing article in a snapshot or transactional publication  
  
1.  At the Publisher on the publication database, execute [sp_helparticle](/sql/relational-databases/system-stored-procedures/sp-helparticle-transact-sql) and note the value of **identityrangemanagementoption** in the result set. If this value is **0**, automatic identity range management is not enabled.  
  
2.  If the value of **identityrangemanagementoption** in the result set is **1**, change the settings as follows:  
  
    -   To change the assigned identity ranges, execute [sp_changearticle](/sql/relational-databases/system-stored-procedures/sp-changearticle-transact-sql) at the Publisher on the publication database. Specify a value of **identity_range** or **pub_identity_range** for **@property** and the new range value for **@value**.  
  
    -   To change the threshold at which new ranges are assigned, execute [sp_changearticle](/sql/relational-databases/system-stored-procedures/sp-changearticle-transact-sql) at the Publisher on the publication database. Specify a value of **threshold** for **@property** and the new threshold value for **@value**.  
  
#### To change automatic identity range management settings for an existing article in a merge publication  
  
1.  At the Publisher on the publication database, execute [sp_helpmergearticle](/sql/relational-databases/system-stored-procedures/sp-helpmergearticle-transact-sql) and note the value of **identity_support** in the result set. If this value is **0**, automatic identity range management is not enabled.  
  
2.  If the value of **identity_support** in the result set is **1**, change the settings as follows:  
  
    -   To change the assigned identity ranges, execute [sp_changemergearticle](/sql/relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql) at the Publisher on the publication database. Specify a value of **identity_range** or **pub_identity_range** for **@property** and the new range value for **@value**.  
  
    -   To change the threshold at which new ranges are assigned, execute [sp_changemergearticle](/sql/relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql) at the Publisher on the publication database. Specify a value of **threshold** for **@property** and the new threshold value for **@value**. For more information on when new identity ranges are assigned, see Assigning Identity Ranges in the topic [Replicate Identity Columns](replicate-identity-columns.md).  
  
    -   To disable automatic identity range management, execute [sp_changemergearticle](/sql/relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql) at the Publisher on the publication database. Specify a value of **identityrangemanagementoption** for **@property** and either **manual** or **none** for **@value**.  
  
## See Also  
 [Peer-to-Peer Transactional Replication](../transactional/peer-to-peer-transactional-replication.md)   
 [Replication System Stored Procedures Concepts](../concepts/replication-system-stored-procedures-concepts.md)   
 [Replicate Identity Columns](replicate-identity-columns.md)  
  
  
