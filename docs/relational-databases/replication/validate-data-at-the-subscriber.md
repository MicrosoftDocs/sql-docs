---
title: "Validate Replicated Data | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "Subscribers [SQL Server replication], data validation"
  - "replication [SQL Server], validating data"
  - "transactional replication, validating data"
  - "validating data"
  - "merge replication data validation [SQL Server replication], SQL Server Management Studio"
ms.assetid: 215b4c9a-0ce9-4c00-ac0b-43b54151dfa3
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Validate Replicated Data
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  This topic describes how to validate data at the Subscriber in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../includes/tsql-md.md)], or Replication Management Objects (RMO).  
  
Transactional and merge replication allow you to validate that data at the Subscriber matches data at the Publisher. Validation can be performed for specific subscriptions or for all subscriptions to a publication. Specify one of the following validation types and the Distribution Agent or Merge Agent will validate data the next time it runs:  
  
-   **Row count only.** This validates whether the table at the Subscriber has the same number of rows as the table at the Publisher, but does not validate that the content of the rows matches. Row count validation provides a lightweight approach to validation that can make you aware of issues with your data.   
-   **Row count and binary checksum**. In addition to taking a count of rows at the Publisher and Subscriber, a checksum of all the data is calculated using the checksum algorithm. If the row count fails, the checksum is not performed.  
  
 In addition to validating that data at the Subscriber and Publisher match, merge replication provides the ability to validate that data is partitioned correctly for each Subscriber. For more information, see [Validate Partition Information for a Merge Subscriber](../../relational-databases/replication/validate-partition-information-for-a-merge-subscriber.md).  
   
## How Data Validation Works  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] validates data by calculating a row count or a checksum at the Publisher and then comparing those values to the row count or checksum calculated at the Subscriber. One value is calculated for the entire publication table and one value is calculated for the entire subscription table, but data in **text**, **ntext**, or **image** columns is not included in the calculations.  
  
 While the calculations are performed, shared locks are placed temporarily on tables for which row counts or checksums are being run, but the calculations are completed quickly and the shared locks removed, usually in a matter of seconds.  
  
 When binary checksums are used, 32-bit redundancy check (CRC) occurs on a column-by-column basis rather than a CRC on the physical row on the data page. This allows the columns with the table to be in any order physically on the data page, but still compute to the same CRC for the row. Binary checksum validation can be used when there are row or column filters on the publication.  

 Validating data is a three-part process:  
  
1.  A single subscription or all subscriptions to a publication are *marked* for validation. Mark subscriptions for validation in the **Validate Subscription**, **Validate Subscriptions**, and **Validate All Subscriptions** dialog boxes, which are available from the **Local Publications** folder and the **Local Subscriptions** folder in [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. You can also mark subscriptions from the **All Subscriptions** tab, the **Subscription Watch List** tab, and the publications node in Replication Monitor. For information about starting Replication Monitor, see [Start the Replication Monitor](../../relational-databases/replication/monitor/start-the-replication-monitor.md).  
  
2.  A subscription is validated the next time it is synchronized by the Distribution Agent (for transactional replication) or the Merge Agent (for merge replication). The Distribution Agent typically runs continuously, in which case validation occurs immediately; the Merge Agent typically runs on demand, in which case validation occurs after you run the agent.  
  
3.  View the validation results:   
    -   In the detail windows in Replication Monitor: on the **Distributor to Subscriber History** tab for transactional replication and the **Synchronization History** tab for merge replication.    
    -   In the **View Synchronization Status** dialog box in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].  
 
## Considerations and restrictions  
 Take the following issues into consideration when validating data:  
  
-   You must stop all update activity at Subscribers before validating data (it is not necessary to stop activity at the Publisher when validation is occurring).  
-   Because checksums and binary checksums can require large amounts of processor resources when validating a large data set, you should schedule validation to occur when there is the least activity on the servers used in replication.   
-   Replication validates tables only; it does not validate whether schema only articles (such as stored procedures) are the same at the Publisher and Subscriber.   
-   Binary checksum can be used with any published table. Checksum cannot validate tables with column filters, or logical table structures where column offsets differ (due to ALTER TABLE statements that drop or add columns).   
-   Replication validation uses the **checksum** and **binary_checksum** functions. For  information about their behavior, see  [CHECKSUM &#40;Transact-SQL&#41;](../../t-sql/functions/checksum-transact-sql.md) and [BINARY_CHECKSUM  &#40;Transact-SQL&#41;](../../t-sql/functions/binary-checksum-transact-sql.md).   
-   Validation by using binary checksum or checksum can incorrectly report a failure if data types are different at the Subscriber than they are at the Publisher. This can occur if you do any one of the following:    
    -   Explicitly set schema options to map data types for earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
    -   Set the publication compatibility level for a merge publication to an earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and published tables contain one or more data types that must be mapped for this version.    
    -   Manually initialize a subscription and are using different data types at the Subscriber.   
-   Binary checksum and checksum validations are not supported for transformable subscriptions for transactional replication.   
-   Validation is not supported for data replicated to non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers.    
-   The procedures for Replication Monitor are for push subscriptions only because pull subscriptions cannot be synchronized in Replication Monitor. However, you can mark a subscription for validation and view validation results for pull subscriptions in Replication Monitor.    
-   The validation results indicate whether validation succeeded or failed, but do not specify which rows failed validation if a failure occurred. To compare data at the Publisher and Subscriber, use the [tablediff Utility](../../tools/tablediff-utility.md). For more information about using this utility with replicated data, see [Compare Replicated Tables for Differences &#40;Replication Programming&#41;](../../relational-databases/replication/administration/compare-replicated-tables-for-differences-replication-programming.md).  


## Data Validation Results  
 When validation is complete, the Distribution Agent or Merge Agent logs messages regarding success or failure (replication does not report which rows failed). These messages can be viewed in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], Replication Monitor, and replication system tables. The how-to topic listed above demonstrates how to run validation and view the results.  
  
 To handle validation failures, consider the following:  
  
-   Configure the replication alert named **Replication: Subscriber has failed data validation** so that you are notified of the failure. For more information, see [Configure Predefined Replication Alerts &#40;SQL Server Management Studio&#41;](../../relational-databases/replication/administration/configure-predefined-replication-alerts-sql-server-management-studio.md).  
  
-   Is the fact that validation failed an issue for your application? If the validation failure is an issue, manually update the data so that it is synchronized, or reinitialize the subscription:  
  
    -   Data can be updated using the [tablediff utility](../../tools/tablediff-utility.md). For more information about using this utility, see [Compare Replicated Tables for Differences &#40;Replication Programming&#41;](../../relational-databases/replication/administration/compare-replicated-tables-for-differences-replication-programming.md).  
  
    -   For more information about reinitializaton, see [Reinitialize Subscriptions](../../relational-databases/replication/reinitialize-subscriptions.md).  

  
  
## Articles in Transactional Replication 

### Using SQL Server Management Studio
  
1.  Connect to the Publisher in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and then expand the server node.    
2.  Expand the **Replication** folder, and then expand the **Local Publications** folder.    
3.  Right-click the publication for which you want to validate subscriptions, and then click **Validate Subscriptions**.    
4.  In the **Validate Subscriptions** dialog box, select which subscriptions to validate:   
    -   Select **Validate all SQL Server subscriptions**.    
    -   Select **Validate the following subscriptions**, and then select one or more subscriptions.    
5.  To specify the type of validation to perform (row count, or row count and checksum) click **Validation Options**, and then specify options in the **Subscription Validation Options** dialog box.  
6.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]   
7.  View validation results in Replication Monitor or the **View Synchronization Status** dialog box. For each subscription:   
    1.  Expand the publication, right-click the subscription, and then click **View Synchronization Status**.    
    2.  If the agent is not running click **Start** in the **View Synchronization Status** dialog box. The dialog box will display informational messages regarding validation.    
     If you do not see any messages regarding validation, the agent has already logged a subsequent message. In this case, view the validation results in Replication Monitor. For more information, see the Replication Monitor how to procedures in this topic.  

### Using Transact-SQL

#### All articles 
  
1.  At the Publisher on the publication database, execute [sp_publication_validation &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-publication-validation-transact-sql.md). Specify **@publication** and one of the following values for **@rowcount_only**:  
  
    -   **1** - rowcount check only (the default)    
    -   **2** - rowcount and binary checksum.  
  
    > [!NOTE]  
    >  When you execute [sp_publication_validation &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-publication-validation-transact-sql.md), [sp_article_validation &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-article-validation-transact-sql.md) is executed for each article in the publication. To successfully execute [sp_publication_validation &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-publication-validation-transact-sql.md), you must have SELECT permissions on all columns in the published base tables.    
2.  (Optional) Start the Distribution Agent for each subscription if it is not already running. For more information, see [Synchronize a Pull Subscription](../../relational-databases/replication/synchronize-a-pull-subscription.md) and [Synchronize a Push Subscription](../../relational-databases/replication/synchronize-a-push-subscription.md).    
3.  Check the agent output for the result of the validation. 
  
#### Single article  
  
1.  At the Publisher on the publication database, execute [sp_article_validation &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-article-validation-transact-sql.md). Specify **@publication**, the name of the article for **@article**, and one of the following values for **@rowcount_only**:  
  
    -   **1** - Rowcount check only (the default)    
    -   **2** - Rowcount and binary checksum.  
  
    > [!NOTE]  
    >  To successfully execute [sp_article_validation &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-article-validation-transact-sql.md), you must have SELECT permissions on all columns in the published base table.  
  
2.  (Optional) Start the Distribution Agent for each subscription if it is not already running. For more information, see [Synchronize a Pull Subscription](../../relational-databases/replication/synchronize-a-pull-subscription.md) and [Synchronize a Push Subscription](../../relational-databases/replication/synchronize-a-push-subscription.md).    
3.  Check the agent output for the result of the validation.
  
#### Single subscriber 
  
1.  At the Publisher on the publication database, open an explicit transaction using [BEGIN TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/begin-transaction-transact-sql.md).    
2.  At the Publisher on the publication database, execute [sp_marksubscriptionvalidation &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-marksubscriptionvalidation-transact-sql.md). Specify the publication for **@publication**, the name of the Subscriber for **@subscriber**, and the name of the subscription database for **@destination_db**.    
3.  (Optional) Repeat step 2 for each subscription being validated.    
4.  At the Publisher on the publication database, execute [sp_article_validation &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-article-validation-transact-sql.md). Specify **@publication**, the name of the article for **@article**, and one of the following values for **@rowcount_only**:    
    -   **1** - Rowcount check only (the default)    
    -   **2** - Rowcount and binary checksum.  
  
    > [!NOTE]  
    >  To successfully execute [sp_article_validation &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-article-validation-transact-sql.md), you must have SELECT permissions on all columns in the published base table.  
  
5.  At the Publisher on the publication database, commit the transaction using [COMMIT TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/commit-transaction-transact-sql.md).    
6.  (Optional) Repeat steps 1 through 5 for each article being validated.   
7.  (Optional) Start the Distribution Agent if it is not already running. For more information, see [Synchronize a Pull Subscription](../../relational-databases/replication/synchronize-a-pull-subscription.md) and [Synchronize a Push Subscription](../../relational-databases/replication/synchronize-a-push-subscription.md).    
8.  Check the agent output for the result of the validation. For more information, see [Validate Data at the Subscriber](../../relational-databases/replication/validate-data-at-the-subscriber.md).  

## All push subscriptions to a transactional publication 

### Using Replication Monitor
  
1.  In Replication Monitor, expand a Publisher group in the left pane, and then expand a Publisher.   
2.  Right-click the publication for which you want to validate subscriptions, and then click **Validate Subscriptions**.   
3.  In the **Validate Subscriptions** dialog box, select which subscriptions to validate:  
  
    -   Select **Validate all SQL Server subscriptions**.    
    -   Select **Validate the following subscriptions**, and then select one or more subscriptions.    
4.  To specify the type of validation to perform (row count, or row count and checksum) click **Validation Options**, and then specify options in the **Subscription Validation Options** dialog box.    
5.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]    
6.  Click the **All Subscriptions** tab.  
7.  View validation results. For each push subscription:    
    1.  If the agent is not running, right-click the subscription, and then click **Start Synchronizing**.    
    2.  Right-click the subscription, and then click **View Details**.   
    3.  View information on the **Distributor to Subscriber History** tab in the **Actions in the selected session** text area.  
  
## For a single subscription to a Merge Publication

### Using SQL Server Management Studio
  
1.  Connect to the Publisher in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and then expand the server node.    
2.  Expand the **Replication** folder, and then expand the **Local Publications** folder.   
3.  Expand the publication for which you want to validate subscriptions, right-click the subscription, and then click **Validate Subscription**.    
4.  In the **Validate Subscription** dialog box, select **Validate this subscription**.    
5.  To specify the type of validation to perform (row count, or row count and checksum) click **Options**, and then specify options in the **Subscription Validation Options** dialog box.    
6.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]    
7.  View validation results in Replication Monitor or the **View Synchronization Status** dialog box:  
  
    1.  Expand the publication, right-click the subscription, and then click **View Synchronization Status**.    
    2.  If the agent is not running, click **Start** in the **View Synchronization Status** dialog box. The dialog box will display informational messages regarding validation.  
  
     If you do not see any messages regarding validation, the agent has already logged a subsequent message. In this case, view the validation results in Replication Monitor. For more information, see the Replication Monitor how to procedures in this topic.  
  
## For all subscriptions to a Merge Publication

### Using SQL Server Management Studio  
1.  Connect to the Publisher in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and then expand the server node.    
2.  Expand the **Replication** folder, and then expand the **Local Publications** folder.   
3.  Right-click the publication for which you want to validate subscriptions, and then click **Validate All Subscriptions**.    
4.  In the **Validate All Subscriptions** dialog box, specify the type of validation to perform (row count, or row count and checksum).   
5.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]    
6.  View validation results in Replication Monitor or the **View Synchronization Status** dialog box. For each subscription:    
    1.  Expand the publication, right-click the subscription, and then click **View Synchronization Status**.    
    2.  If the agent is not running, click **Start** in the **View Synchronization Status** dialog box. The dialog box will display informational messages regarding validation.  
  
     If you do not see any messages regarding validation, the agent has already logged a subsequent message. In this case, view the validation results in Replication Monitor. For more information, see the Replication Monitor how to procedures in this topic.  
  
 
## For a single push subscription to a Merge Publication 

### Using Replication Monitor  
1.  In Replication Monitor, expand a Publisher group in the left pane, expand a Publisher, and then click a publication.    
2.  Click the **All Subscriptions** tab.    
3.  Right-click the subscription you want to validate, and then click **Validate Subscription**.    
4.  In the **Validate Subscription** dialog box, select **Validate this subscription**.    
5.  To specify the type of validation to perform (row count, or row count and checksum) click **Options**, and then specify options in the **Subscription Validation Options** dialog box.    
6.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]    
7.  Click the **All Subscriptions** tab.    
8.  View validation results:    
    1.  If the agent is not running, right-click the subscription, and then click **Start Synchronizing**.    
    2.  Right-click the subscription, and then click **View Details**.    
    3.  View information on the **Synchronization History** tab in the **Last message of the selected session** text area.  

### Using Transact-SQL
1.  At the Publisher on the publication database, execute [sp_validatemergesubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-validatemergesubscription-transact-sql.md). Specify **@publication**, the name of the Subscriber for **@subscriber**, the name of the subscription database for **@subscriber_db**, and one of the following values for **@level**:   
    -   **1** - Rowcount-only validation.    
    -   **3** - Rowcount binary checksum validation.  
  
     This marks the selected subscription for validation.  
  
2.  Start the merge agent for each subscription. For more information, see [Synchronize a Pull Subscription](../../relational-databases/replication/synchronize-a-pull-subscription.md) and [Synchronize a Push Subscription](../../relational-databases/replication/synchronize-a-push-subscription.md).  
3.  Check the agent output for the result of the validation.   
4.  Repeat steps 1 through 3 for each subscription being validated.  
  
> [!NOTE]  
>  A subscription to a merge publication can also be validated at the end of a synchronization by specifying the **-Validate** parameter when running the [Replication Merge Agent](../../relational-databases/replication/agents/replication-merge-agent.md).  
  
## For all push subscriptions to a Merge Publication
  
### Using Replication Monitor    
1.  In Replication Monitor, expand a Publisher group in the left pane, and then expand a Publisher.    
2.  Right-click the publication for which you want to validate subscriptions, and then click **Validate All Subscriptions**.    
3.  In the **Validate All Subscriptions** dialog box, specify the type of validation to perform (row count, or row count and checksum).    
4.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]    
5.  Click the **All Subscriptions** tab.    
6.  View validation results. For each push subscription:    
    1.  If the agent is not running, right-click the subscription, and then click **Start Synchronizing**.    
    2.  Right-click the subscription, and then click **View Details**.    
    3.  View information on the **Synchronization History** tab in the **Last message of the selected session** text area. 
  
### Using Transact-SQL
1.  At the Publisher on the publication database, execute [sp_validatemergepublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-validatemergepublication-transact-sql.md). Specify **@publication** and one of the following values for **@level**:    
    -   **1** - Rowcount-only validation.   
    -   **3** - Rowcount binary checksum validation.  
  
     This marks all subscriptions for validation.   
2.  Start the merge agent for each subscription. For more information, see [Synchronize a Pull Subscription](../../relational-databases/replication/synchronize-a-pull-subscription.md) and [Synchronize a Push Subscription](../../relational-databases/replication/synchronize-a-push-subscription.md).  
  
3.  Check the agent output for the result of the validation. For more information, see [Validate Data at the Subscriber](../../relational-databases/replication/validate-data-at-the-subscriber.md).  

  
## Validate data using Merge Agent parameters  
  
1.  Start the Merge Agent at the Subscriber (pull subscription) or at the Distributor (push subscription) from the command prompt in one of the following ways.   
    -   Specifying a value of **1** (rowcount) or **3** (rowcount and binary checksum) for the **-Validate** parameter.    
    -   Specifying **rowcount validation** or **rowcount and checksum validation** for the **-ProfileName** parameter.  
  
     For more information, see [Synchronize a Pull Subscription](../../relational-databases/replication/synchronize-a-pull-subscription.md) or [Synchronize a Push Subscription](../../relational-databases/replication/synchronize-a-push-subscription.md).  
  
##  <a name="RMOProcedure"></a> Using Replication Management Objects (RMO)  
 Replication enables you to use Replication Management Objects (RMO) to programmatically validate that data at the Subscriber matches data at the Publisher. The objects you use depend on the type of replication topology. Transactional replication requires validation of all subscriptions to a publication.  
  
> [!NOTE]  
>  For an example, see [Example (RMO)](#RMOExample), later in this section.  
  
#### To validate data for all articles in a transactional publication  
  
1.  Create a connection to the Publisher by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.TransPublication> class. Set the <xref:Microsoft.SqlServer.Replication.Publication.Name%2A> and <xref:Microsoft.SqlServer.Replication.Publication.DatabaseName%2A> properties for the publication. Set the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property to the connection created in step 1.  
  
3.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method to get the remaining properties of the object. If this method returns **false**, either the publication properties in step 2 were defined incorrectly or the publication does not exist.  
  
4.  Call the <xref:Microsoft.SqlServer.Replication.TransPublication.ValidatePublication%2A> method. Pass the following:  
  
    -   <xref:Microsoft.SqlServer.Replication.ValidationOption>  
  
    -   <xref:Microsoft.SqlServer.Replication.ValidationMethod>  
  
    -   A Boolean that indicates whether to stop the Distribution Agent after validation is completed.  
  
     This marks the articles for validation.  
  
5.  If not already running, start the Distribution Agent to synchronize each subscription. For more information, see [Synchronize a Push Subscription](../../relational-databases/replication/synchronize-a-push-subscription.md) or [Synchronize a Pull Subscription](../../relational-databases/replication/synchronize-a-pull-subscription.md). The result of the validation operation is written to the agent history. For more information, see [Monitoring Replication](../../relational-databases/replication/monitor/monitoring-replication.md).  
  
#### To validate data in all subscriptions to a merge publication  
  
1.  Create a connection to the Publisher by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.MergePublication> class. Set the <xref:Microsoft.SqlServer.Replication.Publication.Name%2A> and <xref:Microsoft.SqlServer.Replication.Publication.DatabaseName%2A> properties for the publication. Set the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property to the connection created in step 1.  
  
3.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method to get the remaining properties of the object. If this method returns **false**, either the publication properties in step 2 were defined incorrectly or the publication does not exist.  
  
4.  Call the <xref:Microsoft.SqlServer.Replication.MergePublication.ValidatePublication%2A> method. Pass the desired <xref:Microsoft.SqlServer.Replication.ValidationOption>.  
  
5.  Run the Merge Agent for each subscription to start validation, or wait for the next scheduled agent run. For more information, see [Synchronize a Pull Subscription](../../relational-databases/replication/synchronize-a-pull-subscription.md) and [Synchronize a Push Subscription](../../relational-databases/replication/synchronize-a-push-subscription.md). The result of the validation operation is written to the agent history, which you view by using Replication Monitor. For more information, see [Monitoring Replication](../../relational-databases/replication/monitor/monitoring-replication.md).  
  
#### To validate data in a single subscription to a merge publication  
  
1.  Create a connection to the Publisher by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.MergePublication> class. Set the <xref:Microsoft.SqlServer.Replication.Publication.Name%2A> and <xref:Microsoft.SqlServer.Replication.Publication.DatabaseName%2A> properties for the publication. Set the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property to the connection created in step 1.  
  
3.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method to get the remaining properties of the object. If this method returns **false**, either the publication properties in step 2 were defined incorrectly or the publication does not exist.  
  
4.  Call the <xref:Microsoft.SqlServer.Replication.MergePublication.ValidateSubscription%2A> method. Pass the name of the Subscriber and subscription database being validated and the desired <xref:Microsoft.SqlServer.Replication.ValidationOption>.  
  
5.  Run the Merge Agent for the subscription to start validation, or wait for the next scheduled agent run. For more information, see [Synchronize a Pull Subscription](../../relational-databases/replication/synchronize-a-pull-subscription.md) and [Synchronize a Push Subscription](../../relational-databases/replication/synchronize-a-push-subscription.md). The result of the validation operation is written to the agent history, which you view by using Replication Monitor. For more information, see [Monitoring Replication](../../relational-databases/replication/monitor/monitoring-replication.md).  
  
###  <a name="RMOExample"></a> Example (RMO)  
 This example marks all subscriptions to a transactional publication for rowcount validation.  
  
 [!code-cs[HowTo#rmo_ValidateTranPub](../../relational-databases/replication/codesnippet/csharp/rmohowto/rmotestevelope.cs#rmo_validatetranpub)]  
  
 [!code-vb[HowTo#rmo_vb_ValidateTranPub](../../relational-databases/replication/codesnippet/visualbasic/rmohowtovb/rmotestenv.vb#rmo_vb_validatetranpub)]  
  
 This example marks a specific subscription to a merge publication for rowcount validation.  
  
 [!code-cs[HowTo#rmo_ValidateMergeSub](../../relational-databases/replication/codesnippet/csharp/rmohowto/rmotestevelope.cs#rmo_validatemergesub)]  
  
 [!code-vb[HowTo#rmo_vb_ValidateMergeSub](../../relational-databases/replication/codesnippet/visualbasic/rmohowtovb/rmotestenv.vb#rmo_vb_validatemergesub)]  
  
 ## See Also  
[Best Practices for Replication Administration](../../relational-databases/replication/administration/best-practices-for-replication-administration.md)  
  
  
