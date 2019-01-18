---
title: "Backup Database Dialog Box (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.sqlserverstudio.Backup.f1"
ms.assetid: 7811ce7d-6c37-4189-bfa6-ef36fb4932db
author: minewiskan
ms.author: owend
manager: craigg
---
# Backup Database Dialog Box (Analysis Services - Multidimensional Data)
  Use the **Backup Database** dialog box in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] to back up an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database to a backup file using the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Backup File (.abf) format.  
  
> [!IMPORTANT]  
>  For each backup file, the user who runs the backup command must have permission to write to the backup location specified for each file. Also, the user must have one of the following roles: a member of a server role for the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance, or a member of a database role with Full Control (Administrator) permissions on the database to be backed up.  
  
 **To display the Backup Database dialog box**  
  
-   In [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], right-click either the **Databases** folder of an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance or a database in **Object Explorer**, and then click **Backup**.  
  
## Options  
 **Script**  
 Creates a backup script that is based on the options selected in the dialog box. The restore script is written in the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Scripting Language (ASSL).  
  
 Clicking the **Script** icon sends the backup script into a new query window, by default.  
  
 Clicking the **Script** arrow displays a menu that allows you to choose where to send the backup script:  
  
-   To a new query window (default).  
  
-   To a file.  
  
-   To the clipboard.  
  
-   To a job.  
  
 **Database**  
 Displays the name of the currently selected [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database.  
  
 **Backup file**  
 Type the full path and file name of the backup file to use.  
  
 **Browse**  
 Click to display the **Save File As** dialog box and select the path and file name of the backup file to use. For more information about the **Save File As** dialog box, see [Save File As Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](save-file-as-dialog-box-analysis-services-multidimensional-data.md).  
  
 **Allow file overwrite**  
 Select to overwrite an existing backup file or remote backup file, if one exists.  
  
> [!NOTE]  
>  If this option is not selected and the backup file specified in **Backup file** or a remote backup file specified in **Remote backup file** exists, an error occurs.  
  
 **Apply compression**  
 Select to compress the contents of the backup file and, if specified, remote backup files.  
  
 **Encrypt backup file**  
 Select to encrypt the backup file using the password supplied in **Password**.  
  
 **Password**  
 Type the password to use when encrypting the backup file and, if specified, remote backup files.  
  
> [!NOTE]  
>  This option is enabled only if **Encrypt backup file** is selected.  
  
 **Confirm Password**  
 Type the password entered in **Password** to confirm the password for the backup file and, if specified, remote backup files.  
  
> [!NOTE]  
>  This option is enabled only if **Encrypt backup file** is selected.  
  
 **Backup remote partition(s)**  
 Select to include location information and data for remote partitions in the backup file.  
  
> [!NOTE]  
>  This option is enabled only if the currently selected [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database uses remote partitions.  
  
 **Remote partition backup location**  
 Displays the location of remote partitions associated with the selected database, as well as the remote backup file used to back up the data and metadata for the remote partitions. The following columns are available:  
  
|Column|Description|  
|------------|-----------------|  
|**Server**|Displays the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance that manages the remote partitions.|  
|**Database**|Displays the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database that contains the remote partitions.|  
|**Partition List**|Displays the list of remote partitions contained by the database displayed in **Database**.|  
|**Remote backup file**|Type the full path and file name of the remote backup file to use, or click the ellipsis button (**...**) to display the **Save File As** dialog box and select the path and file name of the remote backup file to use. For more information about the **Save File As** dialog box, see [Save File As Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](save-file-as-dialog-box-analysis-services-multidimensional-data.md).|  
  
## See Also  
 [Analysis Services Designers and Dialog Boxes &#40;Multidimensional Data&#41;](analysis-services-designers-and-dialog-boxes-multidimensional-data.md)   
 [Backup and Restore of Analysis Services Databases](multidimensional-models/backup-and-restore-of-analysis-services-databases.md)  
  
  
