---
title: "Create a Data Mining Dimension | Microsoft Docs"
ms.date: 05/01/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: data-mining
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Create a Data Mining Dimension
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  If your mining structure is based on an OLAP cube, you can create a dimension that contains the content of the mining model. You can then incorporate the dimension back into the source cube.  
  
 You can also browse the dimension, use it to explore the model results, or query the dimension using MDX.  
  
### To create a data mining dimension  
  
1.  In Data Mining Designer in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], select either the **Mining Structure** tab or the **Mining Models** tab.  
  
2.  From the **Mining Model** menu, select **Create a Data Mining Dimension**.  
  
     The **Create Data Mining Dimension** dialog box opens.  
  
3.  In the **Model name** list of the **Create Data Mining Dimension** dialog box, select an OLAP mining model.  
  
4.  In the **Dimension name** box, enter a name for the new data mining dimension.  
  
5.  If you want to create a cube that includes the new data mining dimension, select **Create cube**. After you select **Create cube**, you can enter a new name for the cube.  
  
6.  Click **OK**.  
  
     The data mining dimension is created and is added to the **Dimensions** folder in Solution Explorer. If you selected **Create cube**, a new cube is also created and is added to the **Cubes** folder.  
  
## See Also  
 [Mining Structure Tasks and How-tos](../../analysis-services/data-mining/mining-structure-tasks-and-how-tos.md)  
  
  
