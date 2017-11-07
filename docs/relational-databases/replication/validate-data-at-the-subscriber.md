---
title: "Validate Data at the Subscriber | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Subscribers [SQL Server replication], data validation"
  - "replication [SQL Server], validating data"
  - "transactional replication, validating data"
  - "validating data"
  - "merge replication data validation [SQL Server replication], SQL Server Management Studio"
ms.assetid: 215b4c9a-0ce9-4c00-ac0b-43b54151dfa3
caps.latest.revision: 39
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Validate Data at the Subscriber
  This topic describes how to validate data at the Subscriber in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../includes/tsql-md.md)], or Replication Management Objects (RMO).  
  
 Validating data is a three-part process:  
  
1.  A single subscription or all subscriptions to a publication are *marked* for validation. Mark subscriptions for validation in the **Validate Subscription**, **Validate Subscriptions**, and **Validate All Subscriptions** dialog boxes, which are available from the **Local Publications** folder and the **Local Subscriptions** folder in [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. You can also mark subscriptions from the **All Subscriptions** tab, the **Subscription Watch List** tab, and the publications node in Replication Monitor. For information about starting Replication Monitor, see [Start the Replication Monitor](../../relational-databases/replication/monitor/start-the-replication-monitor.md).  
  
2.  A subscription is validated the next time it is synchronized by the Distribution Agent (for transactional replication) or the Merge Agent (for merge replication). The Distribution Agent typically runs continuously, in which case validation occurs immediately; the Merge Agent typically runs on demand, in which case validation occurs after you run the agent.  
  
3.  View the validation results:  
  
    -   In the detail windows in Replication Monitor: on the **Distributor to Subscriber History** tab for transactional replication and the **Synchronization History** tab for merge replication.  
  
    -   In the **View Synchronization Status** dialog box in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
-   **To validate data at the Subscriber, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
     [Replication Management Objects (RMO)](#RMOProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   The procedures for Replication Monitor are for push subscriptions only because pull subscriptions cannot be synchronized in Replication Monitor. However, you can mark a subscription for validation and view validation results for pull subscriptions in Replication Monitor.  
  
-   The validation results indicate whether validation succeeded or failed, but do not specify which rows failed validation if a failure occurred. To compare data at the Publisher and Subscriber, use the [tablediff Utility](../../tools/tablediff-utility.md). For more information about using this utility with replicated data, see [Compare Replicated Tables for Differences &#40;Replication Programming&#41;](../../relational-databases/replication/administration/compare-replicated-tables-for-differences-replication-programming.md).  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To validate data for subscriptions to a transactional publication (Management Studio)  
  
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
  
#### To validate data for a single subscription to a merge publication (Management Studio)  
  
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
  
#### To validate data for all subscriptions to a merge publication (Management Studio)  
  
1.  Connect to the Publisher in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and then expand the server node.  
  
2.  Expand the **Replication** folder, and then expand the **Local Publications** folder.  
  
3.  Right-click the publication for which you want to validate subscriptions, and then click **Validate All Subscriptions**.  
  
4.  In the **Validate All Subscriptions** dialog box, specify the type of validation to perform (row count, or row count and checksum).  
  
5.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
6.  View validation results in Replication Monitor or the **View Synchronization Status** dialog box. For each subscription:  
  
    1.  Expand the publication, right-click the subscription, and then click **View Synchronization Status**.  
  
    2.  If the agent is not running, click **Start** in the **View Synchronization Status** dialog box. The dialog box will display informational messages regarding validation.  
  
     If you do not see any messages regarding validation, the agent has already logged a subsequent message. In this case, view the validation results in Replication Monitor. For more information, see the Replication Monitor how to procedures in this topic.  
  
#### To validate data for all push subscriptions to a transactional publication (Replication Monitor)  
  
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
  
#### To validate data for a single push subscription to a merge publication (Replication Monitor)  
  
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
  
#### To validate data for all push subscriptions to a merge publication (Replication Monitor)  
  
1.  In Replication Monitor, expand a Publisher group in the left pane, and then expand a Publisher.  
  
2.  Right-click the publication for which you want to validate subscriptions, and then click **Validate All Subscriptions**.  
  
3.  In the **Validate All Subscriptions** dialog box, specify the type of validation to perform (row count, or row count and checksum).  
  
4.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
5.  Click the **All Subscriptions** tab.  
  
6.  View validation results. For each push subscription:  
  
    1.  If the agent is not running, right-click the subscription, and then click **Start Synchronizing**.  
  
    2.  Right-click the subscription, and then click **View Details**.  
  
    3.  View information on the **Synchronization History** tab in the **Last message of the selected session** text area.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To validate data for all articles in a transactional publication  
  
1.  At the Publisher on the publication database, execute [sp_publication_validation &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-publication-validation-transact-sql.md). Specify **@publication** and one of the following values for **@rowcount_only**:  
  
    -   **1** - rowcount check only (the default)  
  
    -   **2** - rowcount and binary checksum.  
  
    > [!NOTE]  
    >  When you execute [sp_publication_validation &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-publication-validation-transact-sql.md), [sp_article_validation &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-article-validation-transact-sql.md) is executed for each article in the publication. To successfully execute [sp_publication_validation &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-publication-validation-transact-sql.md), you must have SELECT permissions on all columns in the published base tables.  
  
2.  (Optional) Start the Distribution Agent for each subscription if it is not already running. For more information, see [Synchronize a Pull Subscription](../../relational-databases/replication/synchronize-a-pull-subscription.md) and [Synchronize a Push Subscription](../../relational-databases/replication/synchronize-a-push-subscription.md).  
  
3.  Check the agent output for the result of the validation. For more information, see [Validate Replicated Data](../../relational-databases/replication/validate-replicated-data.md).  
  
#### To validate data for a single article in a transactional publication  
  
1.  At the Publisher on the publication database, execute [sp_article_validation &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-article-validation-transact-sql.md). Specify **@publication**, the name of the article for **@article**, and one of the following values for **@rowcount_only**:  
  
    -   **1** - Rowcount check only (the default)  
  
    -   **2** - Rowcount and binary checksum.  
  
    > [!NOTE]  
    >  To successfully execute [sp_article_validation &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-article-validation-transact-sql.md), you must have SELECT permissions on all columns in the published base table.  
  
2.  (Optional) Start the Distribution Agent for each subscription if it is not already running. For more information, see [Synchronize a Pull Subscription](../../relational-databases/replication/synchronize-a-pull-subscription.md) and [Synchronize a Push Subscription](../../relational-databases/replication/synchronize-a-push-subscription.md).  
  
3.  Check the agent output for the result of the validation. For more information, see [Validate Replicated Data](../../relational-databases/replication/validate-replicated-data.md).  
  
#### To validate data for a single subscriber to a transactional publication  
  
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
  
#### To validate data in all subscriptions to a merge publication  
  
1.  At the Publisher on the publication database, execute [sp_validatemergepublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-validatemergepublication-transact-sql.md). Specify **@publication** and one of the following values for **@level**:  
  
    -   **1** - Rowcount-only validation.  
  
    -   **3** - Rowcount binary checksum validation.  
  
     This marks all subscriptions for validation.  
  
2.  Start the merge agent for each subscription. For more information, see [Synchronize a Pull Subscription](../../relational-databases/replication/synchronize-a-pull-subscription.md) and [Synchronize a Push Subscription](../../relational-databases/replication/synchronize-a-push-subscription.md).  
  
3.  Check the agent output for the result of the validation. For more information, see [Validate Data at the Subscriber](../../relational-databases/replication/validate-data-at-the-subscriber.md).  
  
#### To validate data in selected subscriptions to a merge publication  
  
1.  At the Publisher on the publication database, execute [sp_validatemergesubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-validatemergesubscription-transact-sql.md). Specify **@publication**, the name of the Subscriber for **@subscriber**, the name of the subscription database for **@subscriber_db**, and one of the following values for **@level**:  
  
    -   **1** - Rowcount-only validation.  
  
    -   **3** - Rowcount binary checksum validation.  
  
     This marks the selected subscription for validation.  
  
2.  Start the merge agent for each subscription. For more information, see [Synchronize a Pull Subscription](../../relational-databases/replication/synchronize-a-pull-subscription.md) and [Synchronize a Push Subscription](../../relational-databases/replication/synchronize-a-push-subscription.md).  
  
3.  Check the agent output for the result of the validation.  
  
4.  Repeat steps 1 through 3 for each subscription being validated.  
  
> [!NOTE]  
>  A subscription to a merge publication can also be validated at the end of a synchronization by specifying the **-Validate** parameter when running the [Replication Merge Agent](../../relational-databases/replication/agents/replication-merge-agent.md).  
  
#### To validate data in a subscription using Merge Agent parameters  
  
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
  
5.  If not already running, start the Distribution Agent to synchronize each subscription. For more information, see [Synchronize a Push Subscription](../../relational-databases/replication/synchronize-a-push-subscription.md) or [Synchronize a Pull Subscription](../../relational-databases/replication/synchronize-a-pull-subscription.md). The result of the validation operation is written to the agent history. For more information, see [Monitoring Replication](../../relational-databases/replication/monitor/monitoring-replication-overview.md).  
  
#### To validate data in all subscriptions to a merge publication  
  
1.  Create a connection to the Publisher by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.MergePublication> class. Set the <xref:Microsoft.SqlServer.Replication.Publication.Name%2A> and <xref:Microsoft.SqlServer.Replication.Publication.DatabaseName%2A> properties for the publication. Set the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property to the connection created in step 1.  
  
3.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method to get the remaining properties of the object. If this method returns **false**, either the publication properties in step 2 were defined incorrectly or the publication does not exist.  
  
4.  Call the <xref:Microsoft.SqlServer.Replication.MergePublication.ValidatePublication%2A> method. Pass the desired <xref:Microsoft.SqlServer.Replication.ValidationOption>.  
  
5.  Run the Merge Agent for each subscription to start validation, or wait for the next scheduled agent run. For more information, see [Synchronize a Pull Subscription](../../relational-databases/replication/synchronize-a-pull-subscription.md) and [Synchronize a Push Subscription](../../relational-databases/replication/synchronize-a-push-subscription.md). The result of the validation operation is written to the agent history, which you view by using Replication Monitor. For more information, see [Monitoring Replication](../../relational-databases/replication/monitor/monitoring-replication-overview.md).  
  
#### To validate data in a single subscription to a merge publication  
  
1.  Create a connection to the Publisher by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.MergePublication> class. Set the <xref:Microsoft.SqlServer.Replication.Publication.Name%2A> and <xref:Microsoft.SqlServer.Replication.Publication.DatabaseName%2A> properties for the publication. Set the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property to the connection created in step 1.  
  
3.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method to get the remaining properties of the object. If this method returns **false**, either the publication properties in step 2 were defined incorrectly or the publication does not exist.  
  
4.  Call the <xref:Microsoft.SqlServer.Replication.MergePublication.ValidateSubscription%2A> method. Pass the name of the Subscriber and subscription database being validated and the desired <xref:Microsoft.SqlServer.Replication.ValidationOption>.  
  
5.  Run the Merge Agent for the subscription to start validation, or wait for the next scheduled agent run. For more information, see [Synchronize a Pull Subscription](../../relational-databases/replication/synchronize-a-pull-subscription.md) and [Synchronize a Push Subscription](../../relational-databases/replication/synchronize-a-push-subscription.md). The result of the validation operation is written to the agent history, which you view by using Replication Monitor. For more information, see [Monitoring Replication](../../relational-databases/replication/monitor/monitoring-replication-overview.md).  
  
###  <a name="RMOExample"></a> Example (RMO)  
 This example marks all subscriptions to a transactional publication for rowcount validation.  
  
 [!code-cs[HowTo#rmo_ValidateTranPub](../../relational-databases/replication/codesnippet/csharp/rmohowto/rmotestevelope.cs#rmo_validatetranpub)]  
  
 [!code-vb[HowTo#rmo_vb_ValidateTranPub](../../relational-databases/replication/codesnippet/visualbasic/rmohowtovb/rmotestenv.vb#rmo_vb_validatetranpub)]  
  
 This example marks a specific subscription to a merge publication for rowcount validation.  
  
 [!code-cs[HowTo#rmo_ValidateMergeSub](../../relational-databases/replication/codesnippet/csharp/rmohowto/rmotestevelope.cs#rmo_validatemergesub)]  
  
 [!code-vb[HowTo#rmo_vb_ValidateMergeSub](../../relational-databases/replication/codesnippet/visualbasic/rmohowtovb/rmotestenv.vb#rmo_vb_validatemergesub)]  
  
  