---
title: "General (Restore Database Dialog Box) (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.restoredbdialog.f1"
ms.assetid: 319e7ef5-c9c7-4e50-8690-02a90aed006f
author: minewiskan
ms.author: owend
manager: craigg
---
# General (Restore Database Dialog Box) (Analysis Services - Multidimensional Data)
  Use the **General** page of the **Restore Database** dialog box in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] to specify the backup file and general settings to use when restoring an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database.  
  
> [!IMPORTANT]  
>  For each backup file, the user who runs the restore command must have permission to read from the backup location specified for each file. To restore an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database that is not installed on the server, the user must also be a member of the server role for that [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance. To overwrite an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database, the user must have one of the following roles: a member of the server role for the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance, or a member of a database role with Full Control (Administrator) permissions on the database to be restored.  
  
> [!NOTE]  
>  After restoring an existing database, the user who restored the database might lose access to the restored database. This loss of access can occur if, at the time that the backup was performed, the user was not a member of the server role or was not a member of the database role with Full Control (Administrator) permissions.  
  
 **To display the General page in the Restore Database dialog box**  
  
-   In [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], right-click either the **Databases** folder of an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance or a database in **Object Explorer**, click **Restore**, and then under **Select a page**, click **General**.  
  
## Options  
 **Script**  
 Creates a restore script that is based on the options selected in the dialog box. The restore script is written in the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Scripting Language (ASSL).  
  
 Clicking the **Script** icon sends the restore script into a new query window, by default.  
  
 Clicking the **Script** arrow displays a menu that allows you to choose where to send the restore script:  
  
-   To a new query window (default).  
  
-   To a file.  
  
-   To the clipboard.  
  
-   To a job.  
  
 **Restore database**  
 Select the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database to restore.  
  
 **From backup file**  
 Select the backup file from which to restore the selected [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database.  
  
 **Browse**  
 Click to display the **Locate Database Files** dialog box and select the path and file name of the backup file to use. For more information about the **Locate Database Files** dialog box, see [Locate Database Files Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](locate-database-files-dialog-box-analysis-services-multidimensional-data.md).  
  
 **Allow database overwrite**  
 Select to allow [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] to restore the contents of the selected backup file over any existing objects in the selected [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database.  
  
 **Include security information**  
 Select to copy any security information stored in the backup file to the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database.  
  
 If this option is selected, you can choose the amount of security information from the drop-down list enabled by selecting this option. The following options are available:  
  
|Option|Description|  
|------------|-----------------|  
|**Copy All**|Restores the database roles contained in the backup file, as well as the user accounts associated with the roles.|  
|**Skip Membership**|Restores the database roles contained in the backup file, but does not restore the user accounts associated with the roles.|  
  
 **Password**  
 If the backup file is encrypted, type the password used to encrypt the backup file.  
  
## See Also  
 [Restore Database Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](restore-database-dialog-box-analysis-services-multidimensional-data.md)   
 [Partitions &#40;Restore Database Dialog Box&#41; &#40;Analysis Services - Multidimensional Data&#41;](partitions-restore-database-dialog-box-analysis-services-multidimensional-data.md)   
 [Backup and Restore of Analysis Services Databases](multidimensional-models/backup-and-restore-of-analysis-services-databases.md)  
  
  
