---
title: "Backup Options | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: multidimensional-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Backup Options
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  There are many ways to back up your [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] databases and they all require that you have server administrator and database administrator permissions. You can open the **Backup** dialog box in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], select the appropriate options configuration, and then run the backup from the dialog box itself. Or, you can create a script using the settings already specified in the file; the script can then be saved and run as frequently as required.  
  
## Backup and Synchronize  
 If the database is located on a remote instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], you can use the synchronization feature to back up the database to the local instance. Development builds of a database can be moved into production in this manner. You can also use the conventional, file based, backup and restore to move the development build into production, but synchronization provides additional functionality. For example, you can have security settings which are different for the development and production computers; synchronization will provide you the option to maintain those settings and synchronize all objects other than roles. Also, synchronization typically does an incremental update of those objects which are different for the source and destination computers. This kind of incremental backup is not available using the backup/restore feature. For more information, see [Synchronize Analysis Services Databases](../../analysis-services/multidimensional-models/synchronize-analysis-services-databases.md).  
  
> [!IMPORTANT]  
>  The Analysis Services service account must have permission to write to the backup location specified for each file. Also, the user must have one of the following roles: administrator role on the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance, or a member of a database role with Full Control (Administrator) permissions on the database to be backed up.  
  
## See Also  
 [Backup Database Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](http://msdn.microsoft.com/library/7811ce7d-6c37-4189-bfa6-ef36fb4932db)   
 [Backup and Restore of Analysis Services Databases](../../analysis-services/multidimensional-models/backup-and-restore-of-analysis-services-databases.md)   
 [Backup Element &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/backup-element-xmla)   
 [Backing Up, Restoring, and Synchronizing Databases &#40;XMLA&#41;](../../analysis-services/multidimensional-models-scripting-language-assl-xmla/backing-up-restoring-and-synchronizing-databases-xmla.md)  
  
  
