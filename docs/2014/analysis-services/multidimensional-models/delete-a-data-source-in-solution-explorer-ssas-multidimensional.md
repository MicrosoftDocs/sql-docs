---
title: "Delete a Data Source in Solution Explorer (SSAS Multidimensional) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.deleteobjects.f1"
helpviewer_keywords: 
  - "data sources [Analysis Services], deleting"
  - "deleting data sources"
  - "removing data sources"
ms.assetid: b45441ef-f909-4736-98b9-cc80d0acac99
author: minewiskan
ms.author: owend
manager: craigg
---
# Delete a Data Source in Solution Explorer (SSAS Multidimensional)
  You can delete a data source object to permanently remove it from an Analysis Services multidimensional model project.  
  
 In [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], data sources provide the basis on which data source views are constructed, and data source views are in turn used to define dimensions, cubes, and mining structures in an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project or database. Therefore, deleting a data source may invalidate other [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] objects in an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project. You should always review the list of dependent objects that is provided before deleting the object.  
  
> [!IMPORTANT]  
>  Data sources on which other objects depend cannot be deleted from an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database opened by [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] in online mode. You must delete all objects in the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database that depend on that data source before deleting the data source. For more information about online mode, see [Connect in Online Mode to an Analysis Services Database](connect-in-online-mode-to-an-analysis-services-database.md).  
  
### To delete a data source  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the project or connect to the database from which you want to delete a data source.  
  
2.  In **Solution Explorer**, expand the **Data Sources** folder.  
  
3.  Right-click the data source, and then click **Delete**. The **Delete Objects**  dialog box appears, showing the objects that will be invalidated if you delete the data source. Review this list carefully before clicking **OK** to delete the data source.  
  
4.  Save the project.  
  
     After deleting a data source from an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project, you must save the modified project or you will receive an error the next time you open the project because the underlying XML file for the deleted data source will be missing when the project attempts to load the deleted data source.  
  
## See Also  
 [Data Sources in Multidimensional Models](data-sources-in-multidimensional-models.md)   
 [Data Sources Supported &#40;SSAS Multidimensional&#41;](supported-data-sources-ssas-multidimensional.md)  
  
  
