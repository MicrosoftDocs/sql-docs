---
title: "Measure latency & validate connections (Transactional)"
description: Learn how to measure the latency and validate connections for a Transaction Publication in SQL Server using Replication Monitor in SQL Server Management Studio (SSMS), Transact-SQL (T-SQL), or Replication Management Objects (RMO).
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "Replication Monitor, performance"
  - "tracer tokens [SQL Server replication]"
  - "latency [SQL Server replication]"
  - "transactional replication, tracer tokens"
  - "monitoring performance [SQL Server replication], tracer tokens"
ms.assetid: 4addd426-7523-4067-8d7d-ca6bae4c9e34
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Measure Latency and Validate Connections for Transactional Replication
[!INCLUDE[sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]
  This topic describes how to measure latency and validate connections for transactional replication in [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)] by using Replication Monitor, [!INCLUDE[tsql](../../../includes/tsql-md.md)], or Replication Management Objects (RMO). Transactional replication provides the tracer token feature, which provides a convenient way to measure latency in transactional replication topologies and to validate the connections between the Publisher, Distributor and Subscribers. A token (a small amount of data) is written to the transaction log of the publication database, marked as though it were a typical replicated transaction, and sent through the system, allowing a calculation of:  
  
-   How much time elapses between a transaction being committed at the Publisher and the corresponding command being inserted in the distribution database at the Distributor.  
  
-   How much time elapses between a command being inserted in the distribution database and the corresponding transaction being committed at a Subscriber.  
  
 From these calculations, you can answer a number of questions, including:  
  
-   Which Subscribers take the longest to receive a change from the Publisher?  
  
-   Of the Subscribers expected to receive the tracer token, which, if any, have not received it?  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
-   **To measure latency and validate connections, using:**  
  
     [SQL Server Replication Monitor](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
     [Replication Management Objects](#RMOProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
 Tracer tokens can also be useful when quiescing a system, which involves stopping all activity and verifying that all nodes have received all outstanding changes. For more information, see [Quiesce a Replication Topology &#40;Replication Transact-SQL Programming&#41;](../../../relational-databases/replication/administration/quiesce-a-replication-topology-replication-transact-sql-programming.md).  
  
 To use tracer tokens, you must use certain versions of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]:  
  
-   The Distributor must be [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] or later.  
  
-   The Publisher must be [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] or later or be an Oracle Publisher.  
  
-   For push subscriptions, tracer token statistics are gathered from the Publisher, Distributor, and Subscribers if the Subscriber is [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] 7.0 or later.  
  
-   For pull subscriptions, tracer token statistics are gathered from Subscribers only if the Subscriber is [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] or later. If the Subscriber is [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] 7.0 or [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssVersion2000](../../../includes/ssversion2000-md.md)], statistics are gathered only from the Publisher and Distributor.  
  
 There are also a number of other issues and restrictions to be aware of:  
  
-   Subscriptions must be active to receive a tracer token. A subscription is active if it has been initialized.  
  
-   Reinitialization removes any pending tracer tokens for the relevant subscriptions.  
  
-   Subscribers only receive tracer tokens that were created after their initial synchronization.  
  
-   Tracer tokens are not forwarded by republishing Subscribers.  
  
-   After failover to a secondary, Replication Monitor is unable to adjust the name of the publishing instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] and will continue to display replication information under the name of the original primary instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. After failover, a tracer token cannot be entered by using the Replication Monitor, however a tracer token entered on the new publisher by using [!INCLUDE[tsql](../../../includes/tsql-md.md)], is visible in Replication Monitor.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Replication Monitor  
 For information about starting Replication Monitor, see [Start the Replication Monitor](../../../relational-databases/replication/monitor/start-the-replication-monitor.md).  
  
#### To insert a tracer token and view information on the token  
  
1.  Expand a Publisher group in the left pane, expand a Publisher, and then click a publication.  
  
2.  Click the **Tracer Tokens** tab.  
  
3.  Click **Insert Tracer**.  
  
4.  View elapsed time for the tracer token in the following columns: **Publisher to Distributor**, **Distributor to Subscriber**, **Total Latency**. A value of **Pending** indicates that the token has not reached a given point.  
  
#### To view information on a tracer token inserted previously  
  
1.  Expand a Publisher group in the left pane, expand a Publisher, and then click a publication.  
  
2.  Click the **Tracer Tokens** tab.  
  
3.  Select a time from the **Time inserted** drop-down list.  
  
4.  View elapsed time for the tracer token in the following columns: **Publisher to Distributor**, **Distributor to Subscriber**, **Total Latency**. A value of **Pending** indicates that the token has not reached a given point.  
  
    > [!NOTE]  
    >  Tracer token information is retained for the same time period as other historical data, which is governed by the history retention period of the distribution database. For information about changing distribution database properties, see [View and Modify Distributor and Publisher Properties](../../../relational-databases/replication/view-and-modify-distributor-and-publisher-properties.md).  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To post a tracer token to a transactional publication  
  
1.  (Optional) At the Publisher on the publication database, execute [sp_helppublication &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-helppublication-transact-sql.md). Verify that the publication exists and that the status is active.  
  
2.  (Optional) At the Publisher on the publication database, execute [sp_helpsubscription &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-helpsubscription-transact-sql.md). Verify that the subscription exists and that the status is active.  
  
3.  At the Publisher on the publication database, execute [sp_posttracertoken &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-posttracertoken-transact-sql.md), specifying **\@publication**. Note the value of the **\@tracer_token_id** output parameter.  
  
#### To determine latency and validate connections for a transactional publication  
  
1.  Post a tracer token to the publication using the previous procedure.  
  
2.  At the Publisher on the publication database, execute [sp_helptracertokens &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-helptracertokens-transact-sql.md), specifying **\@publication**. This returns a list of all tracer tokens posted to the publication. Note the desired **tracer_id** in the result set.  
  
3.  At the Publisher on the publication database, execute [sp_helptracertokenhistory &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-helptracertokenhistory-transact-sql.md), specifying **\@publication** and the tracer token ID from step 2 for **\@tracer_id**. This returns latency information for the selected tracer token.  
  
#### To remove tracer tokens  
  
1.  At the Publisher on the publication database, execute [sp_helptracertokens &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-helptracertokens-transact-sql.md), specifying **\@publication**. This returns a list of all tracer tokens posted to the publication. Note the **tracer_id** for the tracer token to delete in the result set.  
  
2.  At the Publisher on the publication database, execute [sp_deletetracertokenhistory &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-deletetracertokenhistory-transact-sql.md), specifying **\@publication** and the ID of the tracer to delete from step 2 for `@tracer_id`.  
  
###  <a name="TsqlExample"></a> Example (Transact-SQL)  
 This example posts a tracer token record and uses the returned ID of the posted tracer token to view latency information.  
  
 [!code-sql[HowTo#sp_tracertokens](../../../relational-databases/replication/codesnippet/tsql/measure-latency-and-vali_1.sql)]  
  
##  <a name="RMOProcedure"></a> Using Replication Management Objects (RMO)  
  
#### To post a tracer token to a transactional publication  
  
1.  Create a connection to the Publisher by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.TransPublication> class.  
  
3.  Set the <xref:Microsoft.SqlServer.Replication.Publication.Name%2A> and <xref:Microsoft.SqlServer.Replication.Publication.DatabaseName%2A> properties for the publication, and set the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property to the connection created in step 1.  
  
4.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method to get the properties of the object. If this method returns **false**, either the publication properties in step 3 were defined incorrectly or the publication does not exist.  
  
5.  Call the <xref:Microsoft.SqlServer.Replication.TransPublication.PostTracerToken%2A> method. This method inserts a tracer token into the publication's transaction log.  
  
#### To determine latency and validate connections for a transactional publication  
  
1.  Create a connection to the Distributor by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.PublicationMonitor> class.  
  
3.  Set the <xref:Microsoft.SqlServer.Replication.PublicationMonitor.Name%2A>, <xref:Microsoft.SqlServer.Replication.PublicationMonitor.DistributionDBName%2A>, <xref:Microsoft.SqlServer.Replication.PublicationMonitor.PublisherName%2A>, and <xref:Microsoft.SqlServer.Replication.PublicationMonitor.PublicationDBName%2A> properties, and set the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property to the connection created in step 1.  
  
4.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method to get the properties of the object. If this method returns **false**, either the publication monitor properties in step 3 were defined incorrectly or the publication does not exist.  
  
5.  Call the <xref:Microsoft.SqlServer.Replication.PublicationMonitor.EnumTracerTokens%2A> method. Cast the returned <xref:System.Collections.ArrayList> object to an array of <xref:Microsoft.SqlServer.Replication.TracerToken> objects.  
  
6.  Call the <xref:Microsoft.SqlServer.Replication.PublicationMonitor.EnumTracerTokenHistory%2A> method. Pass a value of <xref:Microsoft.SqlServer.Replication.TracerToken.TracerTokenId%2A> for a tracer token from step 5. This returns latency information for the selected tracer token as a <xref:System.Data.DataSet> object. If all tracer token information is returned, the connection between the Publisher and Distributor and the connection between the Distributor and the Subscriber both exist and the replication topology is functioning.  
  
#### To remove tracer tokens  
  
1.  Create a connection to the Distributor by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.PublicationMonitor> class.  
  
3.  Set the <xref:Microsoft.SqlServer.Replication.PublicationMonitor.Name%2A>, <xref:Microsoft.SqlServer.Replication.PublicationMonitor.DistributionDBName%2A>, <xref:Microsoft.SqlServer.Replication.PublicationMonitor.PublisherName%2A>, and <xref:Microsoft.SqlServer.Replication.PublicationMonitor.PublicationDBName%2A> properties, and set the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property to the connection created in step 1.  
  
4.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method to get the properties of the object. If this method returns **false**, either the publication monitor properties in step 3 were defined incorrectly or the publication does not exist.  
  
5.  Call the <xref:Microsoft.SqlServer.Replication.PublicationMonitor.EnumTracerTokens%2A> method. Cast the returned <xref:System.Collections.ArrayList> object to an array of <xref:Microsoft.SqlServer.Replication.TracerToken> objects.  
  
6.  Call the <xref:Microsoft.SqlServer.Replication.PublicationMonitor.CleanUpTracerTokenHistory%2A> method. Pass one of the following values:  
  
    -   The <xref:Microsoft.SqlServer.Replication.TracerToken.TracerTokenId%2A> for a tracer token from step 5. This deletes information for a selected token.  
  
    -   A <xref:System.DateTime> object. This deletes information for all tokens older than the specified date and time.  
  
  
