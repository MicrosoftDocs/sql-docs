---
title: "Add a Mining Model to an Existing Mining Structure | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "mining models [Analysis Services], adding"
  - "mining structures [Analysis Services], mining models"
  - "adding mining models"
ms.assetid: fcf72300-0674-4e73-a826-9b8eeffefbb5
author: minewiskan
ms.author: owend
manager: craigg
---
# Add a Mining Model to an Existing Mining Structure
  You can add more mining models to a mining structure, after you add the initial model. Each model must contain columns that exist in the structure, but you can define the usage of the columns differently for each mining model. For more information about how to define mining models columns, see [Mining Model Columns](mining-model-columns.md).  
  
### To add a mining model to the structure  
  
1.  From the **Mining Model** menu item in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], select **New Mining Model**.  
  
     The **New Mining Model** dialog box opens.  
  
2.  Under **Model name**, enter a name for the new mining model.  
  
3.  Under **Algorithm name**, select the algorithm that the mining model will be built from.  
  
4.  Click **OK**.  
  
 A new mining model appears in the **Mining Models** tab. The model uses the default columns that exist in the structure. For information about how to modify the columns, see [Change the Properties of a Mining Model](change-the-properties-of-a-mining-model.md).  
  
## See Also  
 [Mining Model Tasks and How-tos](mining-model-tasks-and-how-tos.md)  
  
  
