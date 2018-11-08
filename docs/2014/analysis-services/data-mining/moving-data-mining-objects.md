---
title: "Moving Data Mining Objects | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "data mining [Analysis Services], models"
  - "data mining editor [Analysis Services]"
  - "mining models [Analysis Services], managing"
  - "Data Mining Designer"
  - "mining models [Analysis Services], modifying"
ms.assetid: bc108407-2603-4387-b930-b5bb9df78069
author: minewiskan
ms.author: owend
manager: craigg
---
# Moving Data Mining Objects
  The most common scenarios for moving data mining objects are to deploy a model from a testing or analysis environment to a production environment, or to share models with other users.  
  
 This topic describes how you can use the tools and scripting languages provided by [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], for moving data mining objects.  
  
## Moving Data Mining Objects between Databases or Servers  
 You can move data mining objects between [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] databases or between instances of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] in the following ways:  
  
-   Re-deploying the solution to a different database.  
  
-   Scripting individual objects.  
  
-   Backing up and then restoring a copy of the database.  
  
-   Exporting and importing structures and models.  
  
 The following section explains these options in more detail.  
  
### Deploying  
 Deploying the solution to a different server or database requires that you have the solution file that was created by using [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
 For more information about deploying Analysis Services solutions, see [Deploy Analysis Services Projects &#40;SSDT&#41;](../multidimensional-models/deploy-analysis-services-projects-ssdt.md).  
  
### Scripting  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] provides several languages that you can use to script objects.  
  
-   **XMLA**: You can script objects using XMLA by right-clicking objects in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. To execute the script, open it in an **XMLA Query** window on the target server.  
  
-   **DMX**: You can create scripts by using templates or one of the query builders provided in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] and [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
 Note, however, that there are differences in the tasks that you can perform with each scripting language:  
  
-   Properties such as the object description and data bindings can only by created or changed by using [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] DDL languages, not by using DMX.  
  
-   Only DMX supports the import and export of mining objects.  
  
-   Only DMX supports generating PMML or importing model definitions from PMML.  
  
-   Only DMX supports training a model with application data. Moreover, the DMX INSERT INTO statement supports training a model without providing values for a key column.  
  
 For more information, see [Developing with Analysis Services Scripting Language &#40;ASSL&#41;](../multidimensional-models/scripting-language-assl/developing-with-analysis-services-scripting-language-assl.md).  
  
### Backup and Restore  
 Backup and restore of an entire Analysis Services database is the method of choice if your data mining solution relies on OLAP objects. [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] provides backup and restore functionality that makes database backups faster and easier.  
  
 For more information about backup, see [Backup and Restore of Analysis Services Databases](../multidimensional-models/backup-and-restore-of-analysis-services-databases.md).  
  
### Exporting and Importing  
 Exporting and then re-importing mining models and structures by using DMX statements is the easiest way to move or back up individual relational data mining objects. For more information about the DMX syntax for these operations, see the following topics:  
  
-   [EXPORT &#40;DMX&#41;](/sql/dmx/export-dmx)  
  
-   [IMPORT &#40;DMX&#41;](/sql/dmx/import-dmx)  
  
 If you specify the INCLUDE DEPENDENCIES option, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] will also export the definition of any required data source views, and when you import the model or structure, it will re-create the data source view on the target server. After you have finished importing the model, make sure to set the necessary mining permissions on the object.  
  
> [!NOTE]  
>  You cannot export and import OLAP models by using DMX. If your mining model is based on an OLAP cube, you must use the functionality provided by [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] for backing up and restoring an entire database, or redeploy the cube and its models.  
  
## See Also  
 [Management of Data Mining Solutions and Objects](management-of-data-mining-solutions-and-objects.md)  
  
  
