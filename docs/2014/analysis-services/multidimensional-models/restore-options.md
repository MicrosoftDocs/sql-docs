---
title: "Restore Options | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "databases [Analysis Services], restoring"
  - "restoring databases [Analysis Services]"
ms.assetid: 75c73802-f321-4671-afc7-54505d62c013
author: minewiskan
ms.author: owend
manager: craigg
---
# Restore Options
  There are many ways to restore your [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] databases, all of which require that you have administrator permissions for both the server computer and the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database. To restore an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database, you can open the **Restore Database** dialog box in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], select the appropriate options configuration and then run the restore from the dialog box. Or, you can create a script using the settings already specified in the file; the script can then be saved and run as often as needed. In this way, the restore is completed by using XMLA, as described in the next section.  
  
## Restoring Databases Using XMLA  
 The XMLA Restore command is a way to automate the restore process by running a restore based on an .abf file. The Restore command has a number of properties that can be set to specify security definitions, where remote partitions should be stored, and the relocation of relational OLAP (ROLAP) objects. For more information, see [Restore Element &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/restore-element-xmla).  
  
> [!IMPORTANT]  
>  For each backup file, the user who runs the restore command must have permission to read from the backup location specified for each file. To restore an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database that is not installed on the server, the user must also be a member of the server role for that [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance. To overwrite an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database, the user must have one of the following roles: a member of the server role for the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance, or a member of a database role with Full Control (Administrator) permissions on the database to be restored.  
  
> [!NOTE]  
>  After restoring an existing database, the user who restored the database might lose access to the restored database. This loss of access can occur if, at the time that the backup was performed, the user was not a member of the server role or was not a member of the database role with Full Control (Administrator) permissions.  
  
## See Also  
 [Restore Database Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](../restore-database-dialog-box-analysis-services-multidimensional-data.md)   
 [Backup and Restore of Analysis Services Databases](backup-and-restore-of-analysis-services-databases.md)   
 [Restore Element &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/restore-element-xmla)   
 [Backing Up, Restoring, and Synchronizing Databases &#40;XMLA&#41;](../multidimensional-models-scripting-language-assl-xmla/backing-up-restoring-and-synchronizing-databases-xmla.md)  
  
  
