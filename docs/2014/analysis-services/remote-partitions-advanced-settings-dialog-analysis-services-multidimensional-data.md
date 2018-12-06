---
title: "Remote Partitions - Advanced Settings Dialog Box (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.sqlserverstudio.advancedrestoresettings.f1"
ms.assetid: a03bb7e1-efaf-47c8-b0ee-f3e4438311cb
author: minewiskan
ms.author: owend
manager: craigg
---
# Remote Partitions - Advanced Settings Dialog Box (Analysis Services - Multidimensional Data)
  Use the **Remote Partitions - Advanced Settings** dialog box in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] to edit advanced settings, such as the connection string of the data source representing the remote [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database maintaining remote partitions, while restoring remote partitions from a remote backup file to an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database by using the **Restore Database** dialog box. You can display the **Remote Partitions - Advanced Settings** dialog box from the **Partitions** page of the **Restore Database** dialog box by clicking the ellipsis button (**...**) for a remote partition after selecting the **Restore remote partitions** option.  
  
## Options  
  
|Term|Definition|  
|----------|----------------|  
|**Data source name**|Displays the name of the data source that represents the remote [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database that manages the listed remote partitions.|  
|**Backup file**|Displays the name of the remote backup file that contains the data to be restored for the listed remote partitions.<br /><br /> Note: No backup file is displayed if a remote backup file was specified in the **Backup file** column on the **Partitions** page of the **Restore Database** dialog box.|  
|**Connection string**|Displays the connection string for the data source displayed in **Data source name**.|  
|**Edit**|Click to display the **Data Link Properties** dialog box and edit the properties contained in the connection string.|  
|**Partitions list**|Select to specify different locations in which to restore remote partitions. Note that you can only change the restoration folder of a remote partition if a location other than the default location was specified for that remote partition in the cube. The following grid, enabled when this option is selected, is used to specify a restoration folder for each remote partition:<br /><br /> **Cube**: Displays the name of the cube that contains the remote partition.<br /><br /> **MeasureGroup**: Displays the name of the measure group that contains the remote partition.<br /><br /> **Partition**: Displays the name of the remote partition.<br /><br /> **Size (MB)**: Displays the size, in megabytes, of the remote partition.<br /><br /> **Original Folder**: Displays the name of the original folder in which the remote partition was stored.<br /><br /> **Restoration Folder**: Type the name of the restoration folder for the remote partition, or click the ellipsis button (**...**) to display the **Browse for Remote Folder** dialog box and select the path of the folder to use. For more information about the **Browse for Remote Folder** dialog box, see [Browse for Remote Folder Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](browse-for-remote-folder-dialog-box-analysis-services-multidimensional-data.md).|  
  
## See Also  
 [Analysis Services Designers and Dialog Boxes &#40;Multidimensional Data&#41;](analysis-services-designers-and-dialog-boxes-multidimensional-data.md)   
 [Partitions &#40;Restore Database Dialog Box&#41; &#40;Analysis Services - Multidimensional Data&#41;](partitions-restore-database-dialog-box-analysis-services-multidimensional-data.md)   
 [Backup and Restore of Analysis Services Databases](multidimensional-models/backup-and-restore-of-analysis-services-databases.md)  
  
  
