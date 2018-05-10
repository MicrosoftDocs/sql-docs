---
title: "Process a Mining Model | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: data-mining
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Process a Mining Model
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  In the Mining Models tab of Data Mining Designer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], you can either process a specific mining model that is associated with a mining structure or you can process all the models that are associated with the structure.  
  
 You can process a mining model by using the following tools:  
  
-   [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]  
  
-   [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]  
  
 You can also use an XMLA Process command. For more information, see  [Tools and Approaches for Processing &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/tools-and-approaches-for-processing-analysis-services.md).  
  
### Process a single mining model using SQL Server Data Tools  
  
1.  On the **Mining Models** tab of Data Mining Designer, select a mining model from the one or more columns of models in the grid.  
  
2.  On the **Mining Model** menu, select **Process Model**.  
  
     If you made changes to the mining structure, you will be prompted to redeploy the structure before processing the model. Click **Yes**.  
  
3.  In the **Processing Mining Model - \<model>** dialog box, click **Run**.  
  
     The **Process Progress** dialog box opens to show the details of model processing.  
  
4.  After the model has successfully completed processing, click **Close** in the **Process Progress** dialog box.  
  
5.  Click **Close** in the **Processing Mining Model - \<model>** dialog box.  
  
 Only the mining structure and the selected mining model have been processed.  
  
### Process all mining models that are associated with a mining structure  
  
1.  On the **Mining Models** tab of Data Mining Designer, select **Process Mining Structure and All Models** from the **Mining Model** menu.  
  
2.  If you made changes to the mining structure, you will be prompted to redeploy the structure before processing the models. Click **Yes**.  
  
3.  In the **Processing Mining Structure - \<structure>** dialog box, click **Run**.  
  
4.  The **Process Progress** dialog box opens to show the details of model processing.  
  
5.  After the models have successfully completed processing, click **Close** in the **Process Progress** dialog box.  
  
6.  Click **Close** in the **Processing \<model>** dialog box.  
  
 The mining structure and all the associated mining models have been processed.  
  
## See Also  
 [Mining Model Tasks and How-tos](../../analysis-services/data-mining/mining-model-tasks-and-how-tos.md)  
  
  
