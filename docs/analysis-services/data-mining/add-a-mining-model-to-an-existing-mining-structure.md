---
title: "Add a Mining Model to an Existing Mining Structure | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "mining models [Analysis Services], adding"
  - "mining structures [Analysis Services], mining models"
  - "adding mining models"
ms.assetid: fcf72300-0674-4e73-a826-9b8eeffefbb5
caps.latest.revision: 24
author: "Minewiskan"
ms.author: "owend"
manager: "jhubbard"
---
# Add a Mining Model to an Existing Mining Structure
  You can add more mining models to a mining structure, after you add the initial model. Each model must contain columns that exist in the structure, but you can define the usage of the columns differently for each mining model. For more information about how to define mining models columns, see [Mining Model Columns](../../analysis-services/data-mining/mining-model-columns.md).  
  
### To add a mining model to the structure  
  
1.  From the **Mining Model** menu item in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], select **New Mining Model**.  
  
     The **New Mining Model** dialog box opens.  
  
2.  Under **Model name**, enter a name for the new mining model.  
  
3.  Under **Algorithm name**, select the algorithm that the mining model will be built from.  
  
4.  Click **OK**.  
  
 A new mining model appears in the **Mining Models** tab. The model uses the default columns that exist in the structure. For information about how to modify the columns, see [Change the Properties of a Mining Model](../../analysis-services/data-mining/change-the-properties-of-a-mining-model.md).  
  
## See Also  
 [Mining Model Tasks and How-tos](../../analysis-services/data-mining/mining-model-tasks-and-how-tos.md)  
  
  