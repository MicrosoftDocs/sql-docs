---
title: "Manage Partitions for a Merge Publication with Parameterized Filters | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "partitions [SQL Server replication]"
  - "merge replication partitions [SQL Server replication], SQL Server Management Studio"
  - "parameterized filters [SQL Server replication], partition management"
ms.assetid: fb5566fe-58c5-48f7-8464-814ea78e6221
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Manage Partitions for a Merge Publication with Parameterized Filters
  This topic describes how to manage partitions for a merge publication with parameterized filters in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../../includes/tsql-md.md)], or Replication Management Objects (RMO). Parameterized row filters can be used to generate nonoverlapping partitions. These partitions can be restricted so that only one subscription receives a given partition. In these cases, a large number of subscribers will result in a large number of partitions, which in turn requires an equal number of partitioned snapshots. For more information, see [Parameterized Row Filters](../merge/parameterized-filters-parameterized-row-filters.md).  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Recommendations](#Recommendations)  
  
-   **To manage partitions for a merge publication with parameterized filters, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
     [Replication Management Objects (RMO)](#RMOProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   If you script a replication topology, which is recommended, publication scripts contain the stored procedure calls to create data partitions. The script provides a reference for the partitions created and a way in which to re-create one or more partitions if necessary. For more information, see [Scripting Replication](../scripting-replication.md).  
  
-   When a publication has parameterized filters that yield subscriptions with nonoverlapping partitions, and if a particular subscription is lost and needs to be re-created, you must do the following: remove the partition that was subscribed to, re-create the subscription, and then re-create the partition. For more information, see [Parameterized Row Filters](../merge/parameterized-filters-parameterized-row-filters.md). Replication generates creation scripts for existing Subscriber partitions when a publication creation script is generated. For more information, see [Scripting Replication](../scripting-replication.md).  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 Manage partitions on the **Data Partitions** page of the **Publication Properties - \<Publication>** dialog box. For more information about accessing this dialog box, see [View and Modify Publication Properties](view-and-modify-publication-properties.md). On this page you can: create and delete partitions; allow Subscribers to initiate snapshot generation and delivery; generate snapshots for one or more partitions; and clean up snapshots.  
  
#### To create a partition  
  
1.  On the **Data Partitions** page of the **Publication Properties - \<Publication>** dialog box, click **Add**.  
  
2.  In the **Add Data Partition** dialog box, enter a value for the **HOST_NAME()** and/or **SUSER_SNAME()** value associated with the partition you want to create.  
  
3.  Optionally specify a schedule for refreshing snapshots:  
  
    1.  Select **Schedule the Snapshot Agent for this partition to run at the following time(s)**  
  
    2.  Accept the default schedule for refreshing snapshots, or click **Change** to specify a different schedule.  
  
4.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
#### To delete a partition  
  
1.  On the **Data Partitions** page, select a partition in the grid.  
  
2.  Click **Delete**.  
  
#### To allow Subscribers to initiate snapshot generation and delivery  
  
1.  On the **Data Partitions** page, select **Automatically define a partition and generate a snapshot if needed when a new Subscriber tries to synchronize**.  
  
2.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
#### To generate a snapshot for a partition  
  
1.  On the **Data Partitions** page, select a partition in the grid.  
  
2.  Click **Generate the selected snapshots now**.  
  
#### To clean up a snapshot for a partition  
  
1.  On the **Data Partitions** page, select a partition in the grid.  
  
2.  Click **Clean up the existing snapshots**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 To better manage a publication with parameterized filters, you can programmatically enumerate the existing partitions using replication stored procedures. You can also create and delete existing partitions. The following information on existing partitions can be obtained:  
  
-   How a partition is filtered (using [SUSER_SNAME &#40;Transact-SQL&#41;](/sql/t-sql/functions/suser-sname-transact-sql) or [HOST_NAME &#40;Transact-SQL&#41;](/sql/t-sql/functions/host-name-transact-sql)).  
  
-   The name of the job that generates a partitioned snapshot.  
  
-   The last time that a partitioned snapshot job ran.  
  
 While the second part of the two-part snapshot can be generated on-demand when a new subscription is initialized, the procedures below enable you to control how this snapshot is generated and to pre-generate this snapshot when it is most convenient. For more information, see [Snapshots for Merge Publications with Parameterized Filters](../snapshots-for-merge-publications-with-parameterized-filters.md).  
  
#### To view information on existing partitions  
  
1.  At the Publisher on the publication database, execute [sp_helpmergepartition &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-helpmergepartition-transact-sql). Specify the name of the publication for **@publication**. (Optional) Specify **@suser_sname** or **@host_name** to return only information based on a single filtering criterion.  
  
#### To define a new partition and generate a new partitioned snapshot  
  
1.  At the Publisher on the publication database, execute [sp_addmergepartition &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-addmergepartition-transact-sql). Specify the name of the publication for **@publication**, and the parameterized value that defines the partition for one of the following:  
  
    -   **@suser_sname** - when the parameterized filter is defined by the value returned by [SUSER_SNAME &#40;Transact-SQL&#41;](/sql/t-sql/functions/suser-sname-transact-sql).  
  
    -   **@host_name** - when the parameterized filter is defined by the value returned by [HOST_NAME &#40;Transact-SQL&#41;](/sql/t-sql/functions/host-name-transact-sql).  
  
2.  Create and initialize the parameterized snapshot for this new partition. For more information, see [Create a Snapshot for a Merge Publication with Parameterized Filters](../create-a-snapshot-for-a-merge-publication-with-parameterized-filters.md).  
  
#### To delete a partition  
  
1.  At the Publisher on the publication database, execute [sp_dropmergepartition &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-dropmergepartition-transact-sql). Specify the name of the publication for **@publication** and the parameterized value that defines the partition for one of the following:  
  
    -   **@suser_sname** - when the parameterized filter is defined by the value returned by [SUSER_SNAME &#40;Transact-SQL&#41;](/sql/t-sql/functions/suser-sname-transact-sql).  
  
    -   **@host_name** - when the parameterized filter is defined by the value returned by [HOST_NAME &#40;Transact-SQL&#41;](/sql/t-sql/functions/host-name-transact-sql).  
  
     This also removes the snapshot job and any snapshot files for the partition.  
  
##  <a name="RMOProcedure"></a> Using Replication Management Objects (RMO)  
 To better manage a publication with parameterized filters, you can programmatically create new Subscriber partitions, enumerate the existing Subscriber partitions, and delete Subscriber partitions by using Replication Management Objects (RMO). For information about how to create Subscriber partitions, see [Create a Snapshot for a Merge Publication with Parameterized Filters](../create-a-snapshot-for-a-merge-publication-with-parameterized-filters.md). The following information about existing partitions can be obtained:  
  
-   The value and filtering function upon which the partition is based.  
  
-   The name of the job that generates a parameterized snapshot for the Subscriber.  
  
-   The last time that a parameterized snapshot job ran.  
  
#### To view information on existing partitions  
  
1.  Create a connection to the Publisher by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.MergePublication> class. Set the <xref:Microsoft.SqlServer.Replication.Publication.Name%2A> and <xref:Microsoft.SqlServer.Replication.Publication.DatabaseName%2A> properties for the publication, and set the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property to the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> created in step 1.  
  
3.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method to get the properties of the object. If this method returns `false`, either the publication properties in step 2 were defined incorrectly or the publication does not exist.  
  
4.  Call the <xref:Microsoft.SqlServer.Replication.MergePublication.EnumMergePartitions%2A> method, and pass the result to an array of <xref:Microsoft.SqlServer.Replication.MergePartition> objects.  
  
5.  For each <xref:Microsoft.SqlServer.Replication.MergePartition> object in the array, get any properties of interest.  
  
#### To delete existing partitions  
  
1.  Create a connection to the Publisher by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.MergePublication> class. Set the <xref:Microsoft.SqlServer.Replication.Publication.Name%2A> and <xref:Microsoft.SqlServer.Replication.Publication.DatabaseName%2A> properties for the publication, and set the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property to the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> created in step 1.  
  
3.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method to get the properties of the object. If this method returns `false`, either the publication properties in step 2 were defined incorrectly or the publication does not exist.  
  
4.  Call the <xref:Microsoft.SqlServer.Replication.MergePublication.EnumMergePartitions%2A> method, and pass the result to an array of <xref:Microsoft.SqlServer.Replication.MergePartition> objects.  
  
5.  For each <xref:Microsoft.SqlServer.Replication.MergePartition> object in the array, determine whether the partition should be deleted. This decision is usually based on the value of the <xref:Microsoft.SqlServer.Replication.MergePartition.DynamicFilterLogin%2A> property or the <xref:Microsoft.SqlServer.Replication.MergePartition.DynamicFilterHostName%2A> property.  
  
6.  Call the <xref:Microsoft.SqlServer.Replication.MergePublication.RemoveMergePartition%2A> method on the <xref:Microsoft.SqlServer.Replication.MergePublication> object from step 2. Pass the <xref:Microsoft.SqlServer.Replication.MergePartition> object from step 5.  
  
7.  Repeat step 6 for each partition that is deleted.  
  
## See Also  
 [Parameterized Row Filters](../merge/parameterized-filters-parameterized-row-filters.md)   
 [Snapshots for Merge Publications with Parameterized Filters](../snapshots-for-merge-publications-with-parameterized-filters.md)  
  
  
