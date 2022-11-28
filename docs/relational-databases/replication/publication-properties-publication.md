---
title: "Publication Properties - Dialog box"
description: Describes the pages found within the 'Publication Properties" Dialog Box in SQL Server Management Studio (SSMS).
ms.custom: seo-lt-2019
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.newpubwizard.pubproperties.agentsecurity.f1"
  - "sql13.rep.newpubwizard.pubproperties.filterrows.f1"
  - "sql13.rep.newpubwizard.pubproperties.internetsynchronization.f1"
  - "sql13.rep.newpubwizard.pubproperties.general.f1"
  - "sql13.rep.newpubwizard.pubproperties.publicationaccesslist.f1"
  - "sql13.rep.newpubwizard.pubproperties.snapshotformat.f1"
helpviewer_keywords: 
  - "Publication Properties dialog box"
ms.assetid: 66e845e9-1308-4288-9110-ad2f22f1fc58
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# SQL Server Replication 'Publication Properties'  dialog box
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

This page describes the pages found within the Publication Properties dialog box. 

## General
 The **General** page of the **Publication Properties** dialog box contains basic information on the publication, including name, description, and the subscription expiration policy.  
  
### Options  
 **Name**  
 The name of the publication (read-only).  
  
 **Database**  
 The name of the publication database (read-only).  
  
 **Description**  
 The description of the publication.  
  
 **Type**  
 The type of publication (read-only).  
  
 **Subscription expiration**  
 Select one of the options for subscription expiration: **Subscriptions never expire** or **Subscriptions expire**, with an explicit time period (**Interval**).  
  
 For snapshot and transactional publications, [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends that you accept the default of **Subscriptions never expire**.  
  
 For merge replication, [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends that you accept the default of **Subscriptions expire** and set as low a value as possible for **Interval**. As the subscription expiration period increases, so does the amount of metadata stored, which can affect performance. Balance the need for Subscribers to be disconnected or simply not to synchronize for an extended period against the potential performance issues of storing and processing a large amount of metadata.  
  
 For more information, see [Subscription Expiration and Deactivation](../../relational-databases/replication/subscription-expiration-and-deactivation.md).  
  
 **Compatibility level**  
 [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later versions only; merge publications only. Select the minimum [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version required for Subscribers that synchronize with this publication. There are a number of rules associated with determining the compatibility level.  

## Filter rows
  The **Filter Rows** page of the **Publication Properties** dialog allows you to add, edit, or delete:  
  
-   Apply static row filters to table articles in snapshot, transactional, and merge publications.   
-   Apply parameterized row filters to table articles in merge publications.    
-   Use join filters to extend filters on merge table articles to related table articles. For more information about filtering options, see [Filter Published Data](../../relational-databases/replication/publish/filter-published-data.md).  
  
> [!NOTE]  
>  Adding, editing, or deleting a filter requires a new snapshot for the publication and requires that all subscriptions be reinitialized.  
  
To maximize application performance and reduce the amount of remote storage required, or to restrict the availability of certain data to specific Subscribers, you should publish only the data required. Your publication can include both unfiltered and filtered tables. For example, you could include the complete (unfiltered) table of company products and use row filters to provide a filtered table of customers for a specific region. By filtering published data, you can:  
  
-   Minimize the amount of data sent over the network.    
-   Reduce the amount of storage space required at the Subscriber.    
-   Customize publications and applications based on individual Subscriber requirements.    
-   Avoid or reduce conflicts if Subscribers are updating data, because different data partitions can be sent to different Subscribers (no two Subscribers will be updating the same data values).    
-   Avoid transmitting sensitive data. Row filters and column filters can be used to restrict a Subscriber's access to data. For merge replication, there are security considerations if you use a parameterized filter that includes HOST_NAME(). For more information, see the section "Filtering with HOST_NAME()" in [Parameterized Row Filters](../../relational-databases/replication/merge/parameterized-filters-parameterized-row-filters.md).  
  
### Options  
 **Filtered Tables**  
 This pane is populated with filters as you add them to table articles in the publication. Tables with row filters are shown as top-level nodes in the pane. For merge publications, tables to which filtering has been extended through a join filter are shown as child nodes.  
  
 **Add**  
 Click **Add** to launch a dialog box that enables you to filter table articles. Clicking **Add** for a snapshot or transactional publication launches a dialog box immediately. Clicking **Add** for a merge publication displays three choices: **Add Filter**; **Add Join to Extend the Selected Filter**; **Automatically Generate Filters**.  
  
-   Select **Add Filter** to launch the **Add Filter** dialog box. This dialog box allows you to apply row filters to a table article. In the **Add Filter** dialog box, you could, for example, specify that a table with customer data should only contain data on French customers when it is replicated to Subscribers.  
  
-   Select **Add Join to Extend the Selected Filter** to launch the **Add Join** dialog box. The **Add Join** dialog box allows you to extend a row filter so that it filters data in tables related to the table with the row filter. For example, if a customer table is filtered so that it only contains data on French customers and there is a related table for customer orders, you can define a join between the two tables so that the orders table only includes orders from French customers.  
  
    > [!NOTE]  
    >  This option is available only if you first select the base table of the join in the filter pane.  
  
-   Select **Automatically Generate Filters** to launch the **Generate Filters** dialog box. This dialog box allows you to define a row filter on one table in a merge publication that replication automatically extends to other tables that are related through foreign key relationships. For example, a publication could include three tables: a customer table, an orders table (with a foreign key to the customer table), and an order details table (with a foreign key to the orders table). Define a row filter on the customer table, and replication will extend it to the other tables.  
  
    > [!NOTE]  
    >  When filters are automatically generated by replication, any existing filters on the publication are deleted. To include both filters generated automatically and ones specified manually, generate filters first. You can only specify one set of automatically generated filters for each publication.  
  
 **Edit**  
 Select a row filter or join filter in the filter pane and click **Edit** to launch the **Edit Filter** or **Edit Join** dialog box.  
  
 **Delete**  
 Select a row filter or join filter in the filter pane and click **Delete** to delete the filter.  
  
 **Find Table**  
 Merge publications only. Click **Find Table** to find a table in a complex filter tree. In a database with complex relationships, a table can be joined to multiple tables, and therefore can appear in more than one place in the filter tree.  
  
 The actual table appears in only one place in the tree, and in other places, the table is represented by a shortcut. A shortcut to a table is only a reference to the table; it does not show the child nodes of the table. A shortcut node is marked with a shortcut arrow, and expanding that node shows the text **Click Find Table to see table for \<tablename>**.  
  
 Select a shortcut node in the pane and click **Find Table** The pane is expanded and the table is highlighted. If you click **Find Table** without a shortcut node selected, a **Find Table** dialog box is launched.  
  
 **Filter**  
 Contains the [!INCLUDE[tsql](../../includes/tsql-md.md)] definition for the filter selected in the filter pane.  

## Publication Access List
  The **Publication Access List** page of the **Publication Properties** dialog box allows you to add and remove logins, accounts, and groups from the publication access list (PAL). The PAL is the primary mechanism for securing the Publisher. When you create a publication, replication creates a PAL for the publication. The PAL, which functions similarly to a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows access control list, contains a list of logins, accounts, and groups that are granted access to the publication.  
  
 When a Subscriber connects to the Publisher or Distributor and requests access to a publication, the login of the Subscriber is compared against the authentication information in the PAL. This provides additional security for the Publisher by preventing the Publisher and Distributor login from being used by a client tool to perform modifications on the Publisher directly. For more information, see [Secure the Publisher](../../relational-databases/replication/security/secure-the-publisher.md).  
  
### Options  
 **Add**  
 Add a new entry to the list. You can add only those login, account, or group names that are already defined at both the Publisher and Distributor. They are defined at both servers if domain accounts are used or local accounts have been created at both servers.  
  
 **Remove**  
 Remove the selected entry from the list.  
  
 **Remove All**  
 Remove all entries from the list.  

## FTP Snapshot and Internet

-   Set properties for delivering the snapshot through File Transfer Protocol (FTP). For more information, see [Deliver a snapshot through FTP](../../relational-databases/replication/publish/deliver-a-snapshot-through-ftp.md). To use FTP for snapshot delivery you must set up an FTP server. See the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows documentation for more information.  
  
    > [!NOTE]  
    >  Changes to any FTP settings require a new snapshot to be generated.  
  
-   Set properties for Web synchronization for merge replication on [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later versions, which allows subscriptions to be synchronized over HTTPS (secure hypertext transfer protocol). To use Web synchronization, you must configure an [!INCLUDE[msCoName](../../includes/msconame-md.md)] Internet Information Services (IIS) server. For more information, see [Web Synchronization for Merge Replication](../../relational-databases/replication/web-synchronization-for-merge-replication.md).  
  
### Options  
 **Access snapshot files via FTP**  
 Select **Allow Subscribers to download snapshot files using FTP (File Transfer Protocol)**, and specify **FTP server name**, **Port number**, **Path from the FTP root folder**, **Login**, and **Password**, to allow Subscribers to use FTP for snapshot delivery.  
  
 This option allows Subscribers to use FTP to retrieve snapshot files, but does not require them to do so. If you select this option, the New Subscription Wizard defaults to having the Subscriber retrieve snapshot files through FTP. To change the setting, use the **Subscription Properties** dialog box. If you allow Subscribers to access the snapshot files through FTP, specify the FTP folder as the location for snapshot files on the **Snapshot** page of the **Publication Properties** dialog box. Doing so will cause the Snapshot Agent to update the files in the FTP folder automatically when a new snapshot is generated. If the location is not set to the FTP folder, you will have to update the files manually when new snapshots are generated. For more information, see [Deliver a Snapshot Through FTP](../../relational-databases/replication/publish/deliver-a-snapshot-through-ftp.md).  
  
 **Web Synchronization**  
 Merge replication only. Select **Allow Subscribers to synchronize by connecting to a Web server**, and specify a Web server address to allow merge Subscribers to use Web synchronization. The Web server must use Transport Layer Security (TLS), previously known as Secure Sockets Layer (SSL), and the Web address must be fully qualified, such as `https://server.domain.com/synchronize`. For more information, see [Configure Web Synchronization](../../relational-databases/replication/configure-web-synchronization.md).  


## Agent Security
  The **Agent Security** page of the **Publication Properties** dialog box allows you to access the settings for the accounts under which the following agents run and make connections to the computers in a replication topology:  
  
-   The Snapshot Agent for all publications.  
  
-   The Log Reader Agent for all transactional publications. There is one Log Reader Agent for each database published for transactional replication. Changing the Log Reader Agent settings affects all transactional publications in a database.  
  
-   The Queue Reader Agent for transactional publications that allow queued updating subscriptions. There is one Queue Reader Agent for each distribution database. Changing the Queue Reader Agent settings affects all transactional publications with queued updating subscriptions that use the same distribution database. For more information about Queue Reader Agent security settings, see [View and Modify Replication Security Settings](../../relational-databases/replication/security/view-and-modify-replication-security-settings.md).  
  
 For more information about security settings and the permissions required by each agent, see [Replication Agent Security Model](../../relational-databases/replication/security/replication-agent-security-model.md).  
  
### Options  
 **Security settings** or **Create Agent**  
 If an agent job has been created, click **Security settings** to access a dialog box that allows you to change the security settings for an agent. If an agent job has not been created, click **Create Agent** to create one and specify security settings.  

## Data Partitions
Data Partitions  
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]  
  The **Data Partitions** page of the **Publication Properties** dialog box allows you to define data partitions for merge publications that use parameterized filtering. After defining partitions, you can then generate snapshots for these partitions, providing different initial data sets for different Subscribers based on the connection properties (login and/or computer name) of the Subscribers. You can also select to allow Subscribers to request snapshot delivery and generation if they do not have a snapshot available for their partition the first time they synchronize. For more information, see [Create a Snapshot for a Merge Publication with Parameterized Filters](../../relational-databases/replication/create-a-snapshot-for-a-merge-publication-with-parameterized-filters.md).  
  
### Options  
 **Add**  
 Click **Add** to define a partition. In the **Add Data Partition** dialog box, specify values for **HOST_NAME()** and/or **SUSER_SNAME()**, and define a schedule to refresh snapshots.  
  
 **Edit**  
 Select an existing partition in the grid, and click **Edit** to edit the partition.  
  
 **Delete**  
 Select an existing partition in the grid, and click **Delete** to delete the partition.  
  
 **Generate the selected snapshots now**  
 Select one or more partitions in the grid, and click **Generate the selected snapshots now** to generate snapshots for these partitions.  
  
 **Clean up the existing snapshots**  
 Select one or more partitions in the grid, and click **Clean up the existing snapshots** to clean up snapshots for these partitions.  
  
 **Automatically define a partition and generate a snapshot if needed when a new Subscriber tries to synchronize**  
 Select this option if you want to allow Subscribers to request snapshot generation and application. Subscribers might require this option if they do not have a snapshot available for their partition the first time they synchronize.  

## Snapshot
Snapshot  
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]  
  The **Snapshot** page of the **Publication Properties** dialog box allows you to set the snapshot format, snapshot folder location, and scripts to run before and after the application of the snapshot. The snapshot folder must be designated as a share and have sufficient permissions for the agents that read and write files to the folder. For more information about securing the folder appropriately, see [Secure the Snapshot Folder](../../relational-databases/replication/security/secure-the-snapshot-folder.md).  
  
> [!NOTE]  
>  Changes require a new snapshot for the publication. For more information, see [Change Publication and Article Properties](../../relational-databases/replication/publish/change-publication-and-article-properties.md).  
  
### Options  
 **Snapshot format**  
 Select native mode or character mode for the snapshot format.  
  
-   Select **Native SQL Server - all Subscribers must be servers running SQL Server** if all Subscribers are instances of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] other than [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssEW](../../includes/ssew-md.md)]. The native snapshot format provides the best performance.    
-   Select **Character - required if a Publisher or Subscriber is not running SQL Server** if any Subscribers are running [!INCLUDE[ssEW](../../includes/ssew-md.md)] or are non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers.    
 **Location of snapshot files**  
 Select the location to store snapshot files. They can be stored in the default location; they can also be stored in an alternate location instead of, or in addition to, the default location. Files stored in an alternate location can be compressed.  
  
-   Select **Put files in the default folder** to use the default snapshot folder for the Publisher. The snapshot folder location is read-only in this dialog box, because it can only be changed for the Publisher in the **Distributor Properties** dialog box. For more information, see [Modify snapshot properties](../../relational-databases/replication/snapshot-options.md).    
-   Select **Put files in the following folder** to specify an alternate location instead of, or in addition to, the default location. Enter a path in the text box or click **Browse** and navigate to a location. Select **Compress snapshot files in this folder** to compress the files in the alternate snapshot location. The alternate location can be on another server, on a network drive, or on removable media such as a CD-ROM or removable disk. For more information, see [Modify snapshot properties](../../relational-databases/replication/snapshot-options.md).  
  
 **Run additional scripts**  
 Specify scripts to be executed before and after the snapshot is applied at the Subscriber. Scripts cannot be specified if **Snapshot format** is **Character**.  
  
 Scripts are optional, but they provide a convenient way to execute commands and apply administrative changes at Subscribers. For more information about executing scripts, see [Execute Scripts Before and After the Snapshot Is Applied](../../relational-databases/replication/snapshot-options.md#execute-scripts-before-and-after-snapshot-is-applied).  
  
-   Enter a path in the **Before applying the snapshot, execute this script** text box or click **Browse** to specify a location for the script.    
-   Enter a path in the **After applying the snapshot, execute this script** text box or click **Browse** to specify a location for the script. 
  
## See Also  
 [Create a Publication](../../relational-databases/replication/publish/create-a-publication.md)   
 [View and Modify Publication Properties](../../relational-databases/replication/publish/view-and-modify-publication-properties.md)   
 [Publish Data and Database Objects](../../relational-databases/replication/publish/publish-data-and-database-objects.md)   

  
  
