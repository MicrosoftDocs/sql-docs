---
title: "View and Modify Publication Properties | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "viewing replication properties"
  - "modifying replication properties, articles"
  - "articles [SQL Server replication], modifying"
  - "publications [SQL Server replication], properties"
  - "articles [SQL Server replication], properties"
  - "modifying replication properties, publications"
  - "publications [SQL Server replication], modifying"
ms.assetid: 27d72ea4-bcb6-48f2-b4aa-eb1410da7efc
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# View and Modify Publication Properties
  This topic describes how to view and modify publication properties in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../../includes/tsql-md.md)], or Replication Management Objects (RMO).  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Recommendations](#Recommendations)  
  
-   **To view and modify publication properties, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
     [Replication Management Objects (RMO)](#RMOProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   Some properties cannot be modified after a publication has been created, and others cannot be modified if there are subscriptions to the publication. Properties that cannot be modified are displayed as read-only.  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   After a publication is created, some property changes require a new snapshot. If a publication has subscriptions, some changes also require all subscriptions to be reinitialized. For more information, see [Change Publication and Article Properties](change-publication-and-article-properties.md) and [Add Articles to and Drop Articles from Existing Publications](add-articles-to-and-drop-articles-from-existing-publications.md).  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 View and modify publication properties in the **Publication Properties - \<Publication>** dialog box, which is available in [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] and Replication Monitor. For information about starting Replication Monitor, see [Start the Replication Monitor](../monitor/start-the-replication-monitor.md).  
  
 The **Publication Properties - \<Publication>** dialog box includes the following pages:  
  
-   The **General** page includes the publication name and description, the database name, the type of publication, and the subscription expiration settings.  
  
-   The **Articles** page corresponds to the **Articles** page in the New Publication Wizard. Use this page to add and delete articles, and to change properties and column filtering for articles.  
  
-   The **Filter Rows** page corresponds to the **Filter Table Rows** page in the New Publication Wizard. Use this page to add, edit, and delete static row filters for all types of publications, and to add, edit, and delete parameterized row filters and join filters for merge publications.  
  
-   The **Snapshot** page allows you to specify the format and location of the snapshot, whether the snapshot should be compressed, and scripts to run before and after the snapshot is applied.  
  
-   The **FTP Snapshot** page (for snapshot and transactional publications, and merge publications for Publishers running versions prior to SQL Server 2005) allows you to specify whether Subscribers can download snapshot files through File Transfer Protocol (FTP).  
  
-   The **FTP Snapshot and Internet** page (for merge publications from Publishers running SQL Server 2005 or later) allows you to specify whether Subscribers can download snapshot files through FTP, and whether Subscribers can synchronize subscriptions through HTTPS.  
  
-   The **Subscription Options** page allows you to set a number of options that apply to all subscriptions. The options differ depending on the type of publication.  
  
-   The **Publication Access List** page allows you to specify which logins and groups can access a publication.  
  
-   The **Agent Security** page allows you to access settings for the accounts under which the following agents run and make connections to the computers in a replication topology: the Snapshot Agent for all publications; the Log Reader Agent for all transactional publications; and the Queue Reader Agent for transactional publications that allow queued updating subscriptions.  
  
-   The **Data Partitions** page (for merge publications from Publishers running SQL Server 2005 or later) allows you to specify whether Subscribers to publications with parameterized filters can request a snapshot if one is not available. It also allows you to generate snapshots for one or more partitions, either once or on a recurring schedule.  
  
#### To view and modify publication properties in Management Studio  
  
1.  Connect to the Publisher in [!INCLUDE[ssManStudio](../../../includes/ssmanstudio-md.md)], and then expand the server node.  
  
2.  Expand the **Replication** folder, and then expand the **Local Publications** folder.  
  
3.  Right-click a publication, and then click **Properties**.  
  
4.  Modify any properties if necessary, and then click **OK**.  
  
#### To view and modify publication properties in Replication Monitor  
  
1.  Expand a Publisher group in the left pane of Replication Monitor, and then expand a Publisher.  
  
2.  Right-click a publication, and then click **Properties**.  
  
3.  Modify any properties if necessary, and then click **OK**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 Publications can be modified and their properties returned programmatically using replication stored procedures. The stored procedures that you use will depend on the type of publication.  
  
#### To view the properties of a snapshot or transactional publication  
  
1.  Execute [sp_helppublication](/sql/relational-databases/system-stored-procedures/sp-helppublication-transact-sql), specifying the name of the publication for the **@publication** parameter. If you do not specify this parameter, information on all publications at the Publisher is returned.  
  
#### To change the properties of a snapshot or transactional publication  
  
1.  Execute [sp_changepublication](/sql/relational-databases/system-stored-procedures/sp-changepublication-transact-sql), specifying the publication property to change in the **@property** parameter and the new value of this property in the **@value** parameter.  
  
    > [!NOTE]  
    >  If the change will require the generation of a new snapshot, you must also specify a value of **1** for **@force_invalidate_snapshot**, and if the change will require that Subscribers be reinitialized, you must specify a value of **1** for **@force_reinit_subscription**. For more information on the properties that, when changed, require a new snapshot or reinitialization, see [Change Publication and Article Properties](change-publication-and-article-properties.md).  
  
#### To view the properties of a merge publication  
  
1.  Execute [sp_helpmergepublication](/sql/relational-databases/system-stored-procedures/sp-helpmergepublication-transact-sql), specifying the name of the publication for the **@publication** parameter. If you do not specify this parameter, information on all publications at the Publisher is returned.  
  
#### To change the properties of a merge publication  
  
1.  Execute [sp_changemergepublication](/sql/relational-databases/system-stored-procedures/sp-changemergepublication-transact-sql), specifying the publication property being changed in the **@property** parameter and the new value of this property in the **@value** parameter.  
  
    > [!NOTE]  
    >  If the change will require the generation of a new snapshot, you must also specify a value of **1** for **@force_invalidate_snapshot**, and if the change will require that Subscribers be reinitialized, you must specify a value of **1** for **@force_reinit_subscription** For more information on the properties that, when changed, require a new snapshot or reinitialization, see [Change Publication and Article Properties](change-publication-and-article-properties.md).  
  
#### To view the properties of a snapshot  
  
1.  Execute [sp_helppublication_snapshot](/sql/relational-databases/system-stored-procedures/sp-helppublication-snapshot-transact-sql), specifying the name of the publication for the **@publication** parameter.  
  
#### To change the properties of a snapshot  
  
1.  Execute [sp_changepublication_snapshot](/sql/relational-databases/system-stored-procedures/sp-changepublication-snapshot-transact-sql), specifying one or more of the new snapshot properties for the appropriate snapshot parameters.  
  
###  <a name="TsqlExample"></a> Examples (Transact-SQL)  
 This transactional replication example returns the properties of the publication.  
  
 [!code-sql[HowTo#sp_helppublication](../../../snippets/tsql/SQL15/replication/howto/tsql/changetranpub.sql#sp_helppublication)]  
  
 This transactional replication example disables schema replication for the publication.  
  
 [!code-sql[HowTo#sp_changepublication](../../../snippets/tsql/SQL15/replication/howto/tsql/changetranpub.sql#sp_changepublication)]  
  
 This merge replication example returns the properties of the publication.  
  
 [!code-sql[HowTo#sp_helpmergepublication](../../../snippets/tsql/SQL15/replication/howto/tsql/changemergepub.sql#sp_helpmergepublication)]  
  
 This merge replication example disables schema replication for the publication.  
  
 [!code-sql[HowTo#sp_changemergepublication](../../../snippets/tsql/SQL15/replication/howto/tsql/changemergepub.sql#sp_changemergepublication)]  
  
##  <a name="RMOProcedure"></a> Using Replication Management Objects (RMO)  
 You can modify publications and access their properties programmatically by using Replication Management Objects (RMO). The RMO classes that you use to view or modify publication properties depend on the type of publication.  
  
#### To view or modify properties of a snapshot or transactional publication  
  
1.  Create a connection to the Publisher by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.TransPublication> class, set the <xref:Microsoft.SqlServer.Replication.Publication.Name%2A> and <xref:Microsoft.SqlServer.Replication.Publication.DatabaseName%2A> properties for the publication, and set the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property to the connection created in step 1.  
  
3.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method to get the properties of the object. If this method returns `false`, either the publication properties in step 2 were defined incorrectly or the publication does not exist.  
  
4.  (Optional) To change properties, set a new value for one or more of the settable properties. Use the logical AND operator (`&` in Microsoft Visual C# and `And` in Microsoft Visual Basic) to determine if a given <xref:Microsoft.SqlServer.Replication.PublicationAttributes> value is set for the <xref:Microsoft.SqlServer.Replication.Publication.Attributes%2A> property. Use the inclusive logical OR operator (`|` in Visual C# and `Or` in Visual Basic) and the exclusive logical OR operator (`^` in Visual C# and `Xor` in Visual Basic) to change the <xref:Microsoft.SqlServer.Replication.PublicationAttributes> values for the <xref:Microsoft.SqlServer.Replication.Publication.Attributes%2A> property.  
  
5.  (Optional) If you specified a value of `true` for <xref:Microsoft.SqlServer.Replication.ReplicationObject.CachePropertyChanges%2A>, call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.CommitPropertyChanges%2A> method to commit changes on the server. If you specified a value of `false` for <xref:Microsoft.SqlServer.Replication.ReplicationObject.CachePropertyChanges%2A> (the default), changes are sent to the server immediately.  
  
#### To view or modify properties of a merge publication  
  
1.  Create a connection to the Publisher by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.MergePublication> class, set the <xref:Microsoft.SqlServer.Replication.Publication.Name%2A> and <xref:Microsoft.SqlServer.Replication.Publication.DatabaseName%2A> properties for the publication, and set the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property to the connection created in step 1.  
  
3.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method to get the properties of the object. If this method returns `false`, either the publication properties in step 2 were defined incorrectly or the publication does not exist.  
  
4.  (Optional) To change properties, set a new value for one or more of the settable properties. Use the logical AND operator (`&` in Visual C# and `And` in Visual Basic) to determine if a given <xref:Microsoft.SqlServer.Replication.PublicationAttributes> value is set for the <xref:Microsoft.SqlServer.Replication.Publication.Attributes%2A> property. Use the inclusive logical OR operator (`|` in Visual C# and `Or` in Visual Basic) and the exclusive logical OR operator (`^` in Visual C# and `Xor` in Visual Basic) to change the <xref:Microsoft.SqlServer.Replication.PublicationAttributes> values for the <xref:Microsoft.SqlServer.Replication.Publication.Attributes%2A> property.  
  
5.  (Optional) If you specified a value of `true` for <xref:Microsoft.SqlServer.Replication.ReplicationObject.CachePropertyChanges%2A>, call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.CommitPropertyChanges%2A> method to commit changes on the server. If you specified a value of `false` for <xref:Microsoft.SqlServer.Replication.ReplicationObject.CachePropertyChanges%2A> (the default), changes are sent to the server immediately.  
  
###  <a name="PShellExample"></a> Examples (RMO)  
 This example sets publication attributes for a transactional publication. The changes are cached until explicitly sent to the server.  
  
 [!code-csharp[HowTo#rmo_ChangeTranPub_cached](../../../snippets/csharp/SQL15/replication/howto/cs/rmotestevelope.cs#rmo_changetranpub_cached)]  
  
 [!code-vb[HowTo#rmo_vb_ChangeTranPub_cached](../../../snippets/visualbasic/SQL15/replication/howto/vb/rmotestenv.vb#rmo_vb_changetranpub_cached)]  
  
 This example disables DDL replication for a merge publication.  
  
 [!code-csharp[HowTo#rmo_ChangeMergePub_ddl](../../../snippets/csharp/SQL15/replication/howto/cs/rmotestevelope.cs#rmo_changemergepub_ddl)]  
  
 [!code-vb[HowTo#rmo_vb_ChangeMergePub_ddl](../../../snippets/visualbasic/SQL15/replication/howto/vb/rmotestenv.vb#rmo_vb_changemergepub_ddl)]  
  
## See Also  
 [Publish Data and Database Objects](publish-data-and-database-objects.md)   
 [Change Publication and Article Properties](change-publication-and-article-properties.md)   
 [Make Schema Changes on Publication Databases](make-schema-changes-on-publication-databases.md)   
 [Replication System Stored Procedures Concepts](../concepts/replication-system-stored-procedures-concepts.md)   
 [Add Articles to and Drop Articles from a Publication](add-articles-to-and-drop-articles-from-a-publication.md)   
 [View Information and Perform Tasks using Replication Monitor](../monitor/view-information-and-perform-tasks-replication-monitor.md)   
 [View and Modify Article Properties](view-and-modify-article-properties.md)  
  
  
