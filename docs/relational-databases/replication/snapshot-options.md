---
title: "Modify snapshot initialization options"
description: Modify various Replication snapshot initialization options such as the snapshot format, and the snapshot folder location in SQL Server Management Studio. 
ms.custom: seo-lt-2019
ms.date: "11/20/2018"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "snapshot replication [SQL Server], options"
  - "snapshots [SQL Server replication], options"
ms.assetid: 759fab42-66c7-4541-a7a3-bb6fb868493c
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Modify Snapshot Initialization Options for SQL Replication 
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

There are a number of options to specify when [initializing a subscription with a snapshot](initialize-a-subscription-with-a-snapshot.md).

## Specify Snapshot Format (SQL Server Management Studio)
  Specify snapshot format on the **Snapshot** page of the **Publication Properties - \<Publication>** dialog box. For more information about accessing this dialog box, see [View and Modify Publication Properties](../../relational-databases/replication/publish/view-and-modify-publication-properties.md).  
  
### To specify snapshot format  
  
1.  On the **Snapshot** page of the **Publication Properties - \<Publication>** dialog box, select **Native SQL Server - all Subscribers must be servers running SQL Server** or **Character - required if a Publisher or Subscriber is not running SQL Server**.  
  
    > [!NOTE]  
    >  It is recommended to select the native format unless this publication must support subscriptions to a  SQL Server Compact database or a non-SQL Server database.  
  
2.  Select **OK**.   

## Snapshot Folder Locations

### Default snapshot location
Specify the default snapshot location on the **Snapshot Folder** page of the Configure Distribution Wizard. For more information about using this wizard, see [Configure Publishing and Distribution](../../relational-databases/replication/configure-publishing-and-distribution.md). If you create a publication on a server that is not configured as a Distributor, specify a default snapshot location on the **Snapshot Folder** page of the New Publication Wizard. For more information about using this wizard, see [Create a Publication](../../relational-databases/replication/publish/create-a-publication.md).  
  
 Modify the default snapshot location on the **Publishers** page of the **Distributor Properties - \<Distributor>** dialog box. For more information, see [View and Modify Distributor and Publisher Properties](../../relational-databases/replication/view-and-modify-distributor-and-publisher-properties.md). Set the snapshot folder for each publication in the **Publication Properties - \<Publication>** dialog box. For more information, see [View and Modify Publication Properties](../../relational-databases/replication/publish/view-and-modify-publication-properties.md).  
  
### To modify the default snapshot location  
  
1.  On the **Publishers** page of the **Distributor Properties - \<Distributor>** dialog box, click the properties button (**...**) for the Publisher for which you want to change the default snapshot location.    
2.  In the **Publisher Properties - \<Publisher>** dialog box, enter a value for the **Default Snapshot Folder** property.  
  
    > [!NOTE]  
    >  The Snapshot Agent must have write permissions for the directory you specify, and the Distribution Agent or Merge Agent must have read permissions. If pull subscriptions are used, you must specify a shared directory as a universal naming convention (UNC) path, such as \\\computername\snapshot. For more information, see [Secure the Snapshot Folder](../../relational-databases/replication/security/secure-the-snapshot-folder.md).    
3.  Select **OK**.
 

### Alternate snapshot locations
Alternate snapshot locations enable you to store snapshot files in a location other than, or in addition to, the default location, which is typically located on the Distributor. Alternate locations can be on another server, on a network drive, or on removable media such as CD-ROMs or removable disks.  
  
Alternate snapshot locations are stored as a property of the publication. Because the alternate snapshot location is a publication property, the Distribution Agent and the Merge Agent are able to locate the proper snapshot as part of the synchronization process.  
  
If you want to specify an alternate snapshot folder location or if you want to compress snapshot files, create the publication without creating the initial snapshot immediately, set the publication properties for the snapshot location, and then run the Snapshot Agent for that publication. If you change the alternate location after creating the initial snapshot, the location of any generated snapshot for the publication will not be relocated to the new alternate location. In this case, depending on the publication settings, the Merge Agent or Distribution Agent might not be able to find the snapshot files at the new alternate location.  
  
> [!NOTE]  
>  Do not specify an alternate location (using the **Publication Properties** dialog box or [sp_changepublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changepublication-transact-sql.md)) that is the same as the default snapshot folder location.  
  
> [!CAUTION]  
>  Do not use both WebSync and alternate snapshot folder locations at the same time.  
  
#### Use SQL Server Management Studio
1.  On the **Snapshot** page of the **Publication Properties - \<Publication>** dialog box:  
  
    1.  Select **Put files in the following folder**, and then click **Browse** to navigate to a directory, or enter the path to the directory in which the snapshot files should be stored.  
  
        > [!NOTE]  
        >  The Snapshot Agent must have write permissions for the directory you specify, and the Distribution Agent or Merge Agent must have read permissions. If pull subscriptions are used, you must specify a shared directory as a universal naming convention (UNC) path, such as \\\computername\snapshot. For more information, see [Secure the Snapshot Folder](../../relational-databases/replication/security/secure-the-snapshot-folder.md).  
  
    2.  Clear **Put files in the default folder** unless you require snapshot files to be written to both locations.  
  
     To compress the snapshot files, select **Compress snapshot files in this location**. Compression is typically used for low bandwidth connections and alternate snapshot locations on removable media, such as a CD-ROM.  
  
2.  Select **OK**.  
  
#### Use Transact-SQL 

When [Configuring Snapshot Properties &#40;Replication Transact-SQL Programming&#41;](../../relational-databases/replication/publish/configure-snapshot-properties-replication-transact-sql-programming.md), specify the value for **snapshot_in_defaultfolder** as false. 

## Compressed snapshots
  Compressing snapshot files is appropriate when you are transferring snapshots over a slow network or you are saving them to removable media and an uncompressed snapshot is too large to fit on the media. Compressing snapshot files is useful in these situations, but compression increases the time to generate and apply the snapshot.  
  
 Compressed snapshot files are written in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] CAB file format, which can compress files of 2 GB or less (if the snapshot files are larger than 2GB, they cannot be compressed). To compress files, they must be written to an alternate snapshot folder (files written to the default snapshot folder cannot be compressed). 
  
 Files are uncompressed at the location where the Distribution Agent or Merge Agent runs; pull subscriptions are typically used with compressed snapshots so that files are uncompressed at the Subscriber. When the Subscriber receives a compressed file, the file is written initially to a temporary location. After the compressed file is copied to the Subscriber, the snapshot files in the compressed file are decompressed, in order, one file at a time by the CAB utility. Space required at the Subscriber is the size of the compressed file plus the largest uncompressed file.  
  
> [!NOTE]  
>  Compressed snapshots can, in some cases, improve the performance of transferring snapshot files across the network. However, compressing the snapshot requires additional processing by the Snapshot Agent when generating the snapshot files, and by the Distribution Agent or Merge Agent when applying the snapshot files. This may slow down snapshot generation and increase the time it takes to apply a snapshot in some cases. Additionally, compressed snapshots cannot be resumed if a network failure occurs; therefore they are not suitable for unreliable networks. Consider these tradeoffs carefully when using compressed snapshots across a network.  
  
### Use SQL Server Management Studio
1.  On the **Snapshot** page of the **Publication Properties - \<Publication>** dialog box:  
  
    1.  Select **Put files in the following folder**, and then click **Browse** to navigate to a directory, or enter the path to the directory in which the snapshot files should be stored.  
  
        > [!NOTE]  
        >  The Snapshot Agent must have write permissions for the directory you specify, and the Distribution Agent or Merge Agent must have read permissions. If pull subscriptions are used, you must specify a shared directory as a universal naming convention (UNC) path, such as \\\computername\snapshot. For more information, see [Secure the Snapshot Folder](security/secure-the-snapshot-folder.md)  
  
    2.  Clear **Put files in the default folder** unless you require snapshot files to be written to both locations.  
  
        > [!NOTE]  
        >  If this check box is selected, the files stored in the default folder are not compressed. Compressed files can only be stored in the alternate location specified in the previous step.  
  
2.  Select **Compress snapshot files in this folder**.    
3.  Select **OK**.   

### Use Transact-SQL

When [Configuring Snapshot Properties](../../relational-databases/replication/publish/configure-snapshot-properties-replication-transact-sql-programming.md), specify the value **compress_snapshot**  to be **True**. 

## Execute scripts before and after snapshot is applied
You can specify scripts to execute at the Subscriber before or after the snapshot is applied. Scripts can be used for a variety of reasons, such as creating logins and schemas (object owners) at each Subscriber.  
  
 You specify a file location for each script, and the Snapshot Agent copies the script files to the current snapshot folder each time snapshot processing occurs. The Distribution Agent or the Merge Agent runs the pre-snapshot script before any of the replicated object scripts when applying a snapshot. The Distribution Agent or the Merge Agent runs the post-snapshot script after all the other replicated object scripts and data have been applied. After the snapshot application is complete and script files run successfully, the script files are removed from the working directory on the Subscriber.  
  
 The script is run by launching the **sqlcmd** utility. Before deploying a script, run it with **sqlcmd** to ensure it executes as expected. The contents of scripts that are executed before and after the snapshot is applied must be repeatable. For example, if you create a table in the script, you should first check for its existence and take appropriate action if it exists. The script must be repeatable because if you need to reinitialize a subscription for which the script has already been applied, the script will be applied again when the new snapshot is applied during reinitialization.  
  
 If you are compressing the snapshot file (by putting it in [!INCLUDE[msCoName](../../includes/msconame-md.md)] CAB file format), the scripts are also compressed and placed in the CAB file. After the compressed snapshot file is transferred to the Subscriber and decompressed to a working directory on the Subscriber, any script indicated as a pre-snapshot script is executed. Likewise, any post-snapshot script is decompressed and executed at the Subscriber as the last step in applying the snapshot.  

### Execute a script 

1.  On the **Snapshot** page of the **Publication Properties - \<Publication>** dialog box:    
    -   To specify a script to execute before the snapshot is applied, click **Browse** to navigate to the script, or enter a path to the script in the **Before applying the snapshot, execute this script** text box.  
  
        > [!NOTE]  
        >  The Distribution Agent or Merge Agent must have read permissions for the directory you specify. If pull subscriptions are used, you must specify a shared directory as a universal naming convention (UNC) path, such as \\\computername\scripts\myscript.sql.  
  
    -   To specify a script to execute after the snapshot is applied, click **Browse** to navigate to the script, or enter a UNC path to the script in the **After applying the snapshot, execute this script** text box.   
2.  Select **OK**.
  


## See Also  
 [Initialize a Subscription with a Snapshot](../../relational-databases/replication/initialize-a-subscription-with-a-snapshot.md)   
 [Transfer snapshot through FTP](../../relational-databases/replication/publish/deliver-a-snapshot-through-ftp.md)   
 [Configure Snapshot Properties &#40;Replication Transact-SQL Programming&#41;](../../relational-databases/replication/publish/configure-snapshot-properties-replication-transact-sql-programming.md)     
  
  
