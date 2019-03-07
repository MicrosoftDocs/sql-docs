---
title: "Modify Snapshot Initialization Options for SQL Replication | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "snapshot replication [SQL Server], options"
  - "snapshots [SQL Server replication], options"
ms.assetid: 759fab42-66c7-4541-a7a3-bb6fb868493c
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Modify Snapshot Initialization Options for SQL Replication

This article discusses how to modify a number of options when [initializing a subscription with a snapshot](initialize-a-subscription-with-a-snapshot.md).

## Snapshot Format
  Specify snapshot format on the **Snapshot** page of the **Publication Properties - \<Publication>** dialog box. For more information about accessing this dialog box, see [View and Modify Publication Properties](publish/view-and-modify-publication-properties.md).  

1.  On the **Snapshot** page of the **Publication Properties - \<Publication>** dialog box, select **Native SQL Server - all Subscribers must be servers running SQL Server** or **Character - required if a Publisher or Subscriber is not running SQL Server**. 

    > [!NOTE]  
    >  It is recommended to select the native format unless this publication must support subscriptions to a [!INCLUDE[ssEW](../../../includes/ssew-md.md)] database or a non-[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database.    
1.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  

## Snapshot folder locations

### Default snapshot location
Specify the Default Snapshot Location (SQL Server Management Studio)
  Specify the default snapshot location on the **Snapshot Folder** page of the Configure Distribution Wizard. For more information about using this wizard, see [Configure Publishing and Distribution](configure-publishing-and-distribution.md). If you create a publication on a server that is not configured as a Distributor, specify a default snapshot location on the **Snapshot Folder** page of the New Publication Wizard. For more information about using this wizard, see [Create a Publication](publish/create-a-publication.md).  
  
 Modify the default snapshot location on the **Publishers** page of the **Distributor Properties - \<Distributor>** dialog box. For more information, see [View and Modify Distributor and Publisher Properties](view-and-modify-distributor-and-publisher-properties.md). Set the snapshot folder for each publication in the **Publication Properties - \<Publication>** dialog box. For more information, see [View and Modify Publication Properties](publish/view-and-modify-publication-properties.md).  
  
#### Modify the default snapshot location  
  
1.  On the **Publishers** page of the **Distributor Properties - \<Distributor>** dialog box, click the properties button (**...**) for the Publisher for which you want to change the default snapshot location.    
2.  In the **Publisher Properties - \<Publisher>** dialog box, enter a value for the **Default Snapshot Folder** property.

    > [!NOTE]  
    >  The Snapshot Agent must have write permissions for the directory you specify, and the Distribution Agent or Merge Agent must have read permissions. If pull subscriptions are used, you must specify a shared directory as a universal naming convention (UNC) path, such as \\\computername\snapshot. For more information, see [Secure the Snapshot Folder](security/secure-the-snapshot-folder.md).    
1.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  

### Alternate snapshot location
  Specify an alternate snapshot location on the **Snapshot** page of the **Publication Properties - \<Publication>** dialog box. For more information about accessing this dialog box, see [View and Modify Publication Properties](publish/view-and-modify-publication-properties.md). 

  
#### Specify an alternate snapshot location  
  
1.  On the **Snapshot** page of the **Publication Properties - \<Publication>** dialog box:    
    1.  Select **Put files in the following folder**, and then click **Browse** to navigate to a directory, or enter the path to the directory in which the snapshot files should be stored.    

        > [!NOTE]  
        >  The Snapshot Agent must have write permissions for the directory you specify, and the Distribution Agent or Merge Agent must have read permissions. If pull subscriptions are used, you must specify a shared directory as a universal naming convention (UNC) path, such as \\\computername\snapshot. For more information, see [Secure the Snapshot Folder](security/secure-the-snapshot-folder.md).    
    a.  Clear **Put files in the default folder** unless you require snapshot files to be written to both locations.    
     To compress the snapshot files, select **Compress snapshot files in this location**. Compression is typically used for low bandwidth connections and alternate snapshot locations on removable media, such as a CD-ROM.    
1.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]   


## Compress snapshot files
Specify that files should be compressed on the **Snapshot** page of the **Publication Properties - \<Publication>** dialog box. For more information about accessing this dialog box, see [View and Modify Publication Properties](publish/view-and-modify-publication-properties.md).  
  
1.  On the **Snapshot** page of the **Publication Properties - \<Publication>** dialog box:  
  
    1.  Select **Put files in the following folder**, and then click **Browse** to navigate to a directory, or enter the path to the directory in which the snapshot files should be stored.    
        > [!NOTE]  
        >  The Snapshot Agent must have write permissions for the directory you specify, and the Distribution Agent or Merge Agent must have read permissions. If pull subscriptions are used, you must specify a shared directory as a universal naming convention (UNC) path, such as \\\computername\snapshot. For more information, see [Secure the Snapshot Folder](security/secure-the-snapshot-folder.md)  
  
    2.  Clear **Put files in the default folder** unless you require snapshot files to be written to both locations.    
        > [!NOTE]  
        >  If this check box is selected, the files stored in the default folder are not compressed. Compressed files can only be stored in the alternate location specified in the previous step.    
2.  Select **Compress snapshot files in this folder**.    
3.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  

## Execute scripts before and after snapshot is applied

 You can specify scripts to execute at the Subscriber before or after the snapshot is applied. Scripts can be used for a variety of reasons, such as creating logins and schemas (object owners) at each Subscriber.  
  
 You specify a file location for each script, and the Snapshot Agent copies the script files to the current snapshot folder each time snapshot processing occurs. The Distribution Agent or the Merge Agent runs the pre-snapshot script before any of the replicated object scripts when applying a snapshot. The Distribution Agent or the Merge Agent runs the post-snapshot script after all the other replicated object scripts and data have been applied. After the snapshot application is complete and script files run successfully, the script files are removed from the working directory on the Subscriber.  
  
 The script is run by launching the **sqlcmd** utility. Before deploying a script, run it with **sqlcmd** to ensure it executes as expected. The contents of scripts that are executed before and after the snapshot is applied must be repeatable. For example, if you create a table in the script, you should first check for its existence and take appropriate action if it exists. The script must be repeatable because if you need to reinitialize a subscription for which the script has already been applied, the script will be applied again when the new snapshot is applied during reinitialization.  
  
 If you are compressing the snapshot file (by putting it in [!INCLUDE[msCoName](../../includes/msconame-md.md)] CAB file format), the scripts are also compressed and placed in the CAB file. After the compressed snapshot file is transferred to the Subscriber and decompressed to a working directory on the Subscriber, any script indicated as a pre-snapshot script is executed. Likewise, any post-snapshot script is decompressed and executed at the Subscriber as the last step in applying the snapshot.  

### Execute a script before or after a snapshot is applied  
 Specify an optional script to execute before or after the snapshot is applied on the **Snapshot** page of the **Publication Properties - \<Publication>** dialog box. For more information about accessing this dialog box, see [View and Modify Publication Properties](publish/view-and-modify-publication-properties.md).  


1.  On the **Snapshot** page of the **Publication Properties - \<Publication>** dialog box:    
    -   To specify a script to execute before the snapshot is applied, click **Browse** to navigate to the script, or enter a path to the script in the **Before applying the snapshot, execute this script** text box. 
   
        > [!NOTE]  
        >  The Distribution Agent or Merge Agent must have read permissions for the directory you specify. If pull subscriptions are used, you must specify a shared directory as a universal naming convention (UNC) path, such as \\\computername\scripts\myscript.sql.    
    -   To specify a script to execute after the snapshot is applied, click **Browse** to navigate to the script, or enter a UNC path to the script in the **After applying the snapshot, execute this script** text box.    
2.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
  
  
