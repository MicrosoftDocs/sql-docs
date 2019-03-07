---
title: "Export and Import Data Mining Objects | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "backing up databases [Analysis Services]"
  - "exporting mining models"
  - "exporting mining structures"
  - "mining structures [Analysis Services], creating"
  - "mining structures [DMX], exporting"
  - "mining models [Analysis Services], migration"
ms.assetid: 10a83b13-2640-4ff5-80c8-a35e1d692908
author: minewiskan
ms.author: owend
manager: craigg
---
# Export and Import Data Mining Objects
  In addition to the functionality provided in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] for backing up, restoring, and migrating solutions, SQL Server Data Mining provides the ability to quickly transfer data mining structures and models between different servers by using Data Mining Extensions (DMX).  
  
 If your data mining solution uses relational data instead of a multidimensional database, transferring models by using `EXPORT` and `IMPORT` is much faster and easier than either using database restore or deploying an entire solution.  
  
 This section provides an overview of how to transfer data mining structures and models by using DMX statements. For details of the syntax, together with examples, see [EXPORT &#40;DMX&#41;](/sql/dmx/export-dmx) and [IMPORT &#40;DMX&#41;](/sql/dmx/import-dmx).  
  
> [!NOTE]  
>  You must be a database or server administrator to export or import objects from a Microsoft SQL Server Analysis Services database.  
  
## Exporting Data Mining Structures  
 When you export a mining structure, the EXPORT statement automatically exports all associated models. If you want to control the objects that are exported, you must specify each object by name.  
  
 If the mining structure has been processed and the results have been cached, which is the default behavior, when you export the mining structure, the definition contains a summary of the data on which the structure is based. To remove this summary, you must clear the cache associated with the mining structure by performing a `Process Clear Structure` operation. For more information, see [Process a Mining Structure](process-a-mining-structure.md).  
  
### Exporting Data Mining Models  
 You can use the `WITH DEPENDENCIES` keyword to export the data source and data source view definition together with the mining model and its structure.  
  
 When you export a mining model without exporting its dependencies, the EXPORT statement will export the definition of the mining model and its mining structure, but does not export the definition of the data sources. As a consequence, after you import the model you will be able to browse the model immediately, but if you want to reprocess the mining model on the target server, or run queries against the underlying data, you must create a corresponding data source on the destination server.  
  
## Importing Data Mining Structures and Models  
 When you import a data mining object, the object is imported to the server and database to which you are connected when you execute the IMPORT statement. If the import file includes a database that does not exist on the server, the database will be created.  
  
 You can also import a mining structure or mining model by using the `Restore` command. Your models or structures will be restored into the database that has the same name as the database from which they were exported. For more information, see [Restore Options](../multidimensional-models/restore-options.md).  
  
## Remarks  
 You cannot import a model or structure to a server if a model or structure of the same name already exists on that server. Also, you cannot export a data mining object and then modify the name of that object in the export file. Therefore, if you anticipate naming conflicts, you should either delete the data mining object on the target server, or rename the data mining object before you export the definition.  
  
## See Also  
 [Management of Data Mining Solutions and Objects](management-of-data-mining-solutions-and-objects.md)  
  
  
