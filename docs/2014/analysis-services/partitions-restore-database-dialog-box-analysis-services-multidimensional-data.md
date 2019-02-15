---
title: "Partitions (Restore Database Dialog Box) (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.restoredbdialog.partitions.f1"
ms.assetid: 1ad4dde5-4651-4069-875c-7ab73cd8b4f4
author: minewiskan
ms.author: owend
manager: craigg
---
# Partitions (Restore Database Dialog Box) (Analysis Services - Multidimensional Data)
  Use the **Partitions** page of the **Restore Database** dialog box in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] to specify the location to restore local partitions, and to specify whether to restore remote partitions and the remote backup files to use when restoring remote partitions.  
  
> [!IMPORTANT]  
>  For each backup file, the user who runs the restore command must have permission to read from the backup location specified for each file. To restore an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database that is not installed on the server, the user must also be a member of the server role for that [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance. To overwrite an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database, the user must have one of the following roles: a member of the server role for the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance, or a member of a database role with Full Control (Administrator) permissions on the database to be restored.  
  
> [!NOTE]  
>  After restoring an existing database, the user who restored the database might lose access to the restored database. This loss of access can occur if, at the time that the backup was performed, the user was not a member of the server role or was not a member of the database role with Full Control (Administrator) permissions.  
  
 **To display the Partitions page in the Restore Database dialog box**  
  
-   In [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], right-click either the **Databases** folder of an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance or a database in **Object Explorer**, click **Restore**, and then under **Select a page**, click **Partitions**.  
  
## Options  
 **Script**  
 Creates a restore script that is based on the options selected in the dialog box. The restore script is written in the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Scripting Language (ASSL).  
  
 Clicking the **Script** icon sends the restore script into a new query window, by default.  
  
 Clicking the **Script** arrow displays a menu that allows you to choose where to send the restore script:  
  
-   To a new query window (default).  
  
-   To a file.  
  
-   To the clipboard.  
  
-   To a job.  
  
 **Restore to original locations**  
 Select to restore local partitions contained in the backup file to their original locations.  
  
 **Select different locations**  
 Select to specify different locations in which to restore local partitions.  
  
> [!NOTE]  
>  You can only change the restoration folder of a local partition if a location other than the default location was specified for that partition in the cube.  
  
 The following grid, enabled when this option is selected, is used to specify a restoration folder for each local partition:  
  
|Column|Description|  
|------------|-----------------|  
|**Cube**|Displays the name of the cube that contains the local partition.|  
|**MeasureGroup**|Displays the name of the measure group that contains the local partition.|  
|**Partition**|Displays the name of the local partition.|  
|**Size (MB)**|Displays the size, in megabytes, of the local partition.|  
|**Original Folder**|Displays the name of the original folder in which the local partition was stored.|  
|**Restoration Folder**|Type the name of the restoration folder for the local partition, or click the ellipsis button (**...**) to display the **Browse for Remote Folder** dialog box and select the path of the folder to use. For more information about the **Browse for Remote Folder** dialog box, see [Browse for Remote Folder Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](browse-for-remote-folder-dialog-box-analysis-services-multidimensional-data.md).|  
  
 **Restore remote partitions**  
 Select to restore remote partitions stored in remote backup files.  
  
> [!NOTE]  
>  This option is enabled only if the backup file contains references to remote partitions.  
  
 The following grid, enabled when this option is selected, is used to specify a restoration folder for each local partition:  
  
|Column|Description|  
|------------|-----------------|  
|**Server**|Displays the name of the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance that manages the remote partition.|  
|**Data Source**|Displays the name of the data source in the backup file that represents the database that contains the remote partition.|  
|**Backup File**|Type the full path and file name of the remote backup file to use, or click the ellipsis button (**...**) to display the **Locate Database Files** dialog box and select the path and file name of the remote backup file to use. For more information about the **Locate Database Files** dialog box, see [Locate Database Files Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](locate-database-files-dialog-box-analysis-services-multidimensional-data.md).|  
|**...**|Click to display the **Remote Partitions - Advanced Settings** dialog box and modify advanced options, such as the connection string for the data source, for restoring the remote partition. For more information about the **Remote Partitions - Advanced Settings** dialog box, see [Remote Partitions - Advanced Settings Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](remote-partitions-advanced-settings-dialog-analysis-services-multidimensional-data.md).|  
  
## See Also  
 [Restore Database Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](restore-database-dialog-box-analysis-services-multidimensional-data.md)   
 [General &#40;Restore Database Dialog Box&#41; &#40;Analysis Services - Multidimensional Data&#41;](general-restore-database-dialog-box-analysis-services-multidimensional-data.md)   
 [Backup and Restore of Analysis Services Databases](multidimensional-models/backup-and-restore-of-analysis-services-databases.md)  
  
  
