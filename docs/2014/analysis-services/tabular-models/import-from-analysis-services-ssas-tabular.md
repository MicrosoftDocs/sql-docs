---
title: "Import from Analysis Services (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: b9a21b23-3a06-4ef8-bc06-9c79cdc54870
author: minewiskan
ms.author: owend
manager: craigg
---
# Import from Analysis Services (SSAS Tabular)
  This topic describes how to create a new tabular model project by importing the metadata from an existing tabular model by using the Import from Server project template in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
## Create a new model by importing metadata from an existing model in Analysis Services  
 You can use the Import from Server project template to create a new tabular model project by copying the metadata from an existing tabular model on an Analysis Services server. The new project will be created with the same data source connections, tables, relationships, measures, KPIs, roles, hierarchies, perspectives, and partitions as the model it was imported from. The data, however, is not copied from the existing model to the new model workspace. Once the import process has completed, and the new model project created, you must run a Process All to load the data from the data sources into the new model project workspace database.  
  
#### To create a new model by importing metadata from an existing model  
  
1.  In [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], on the **File** menu, click **New**, and then click **Project**.  
  
2.  In the **New Project** dialog box, under **Installed Templates**, click **Business Intelligence**, and then click **Import from Server**.  
  
3.  In **Name**, type a name for the project, then specify a location and solution name, and then click **OK**.  
  
4.  In the **Import from Analysis Services** dialog box, in **Server Name**, specify the name of the Analysis Services server that contains the model metadata you want to import.  
  
5.  In **Database Name**, select the tabular model database that contains the model metadata you want to import, and then click **OK**.  
  
## See Also  
 [Project Properties &#40;SSAS Tabular&#41;](properties-ssas-tabular.md)  
  
  
