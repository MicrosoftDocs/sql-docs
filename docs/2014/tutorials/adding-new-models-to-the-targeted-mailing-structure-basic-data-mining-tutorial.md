---
title: "Adding New Models to the Targeted Mailing Structure (Basic Data Mining Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 512c6888-60f1-46e4-9639-bc448395b8d7
author: minewiskan
ms.author: owend
manager: craigg
---
# Adding New Models to the Targeted Mailing Structure (Basic Data Mining Tutorial)
  In this task, you will define two additional models by using the **Mining Models** tab of Data Mining Designer. You will use the Microsoft Clustering and Microsoft Naive Bayes algorithms to create the models. These two algorithms are selected because of their ability to predict a discrete value (i.e., bike purchase). For more information about these algorithms, see [Microsoft Clustering Algorithm](../../2014/analysis-services/data-mining/microsoft-clustering-algorithm.md) and [Microsoft Naive Bayes Algorithm](../../2014/analysis-services/data-mining/microsoft-naive-bayes-algorithm.md)  
  
### To create a clustering mining model  
  
1.  Switch to the **Mining Models** tab in Data Mining Designer in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)].  
  
     Notice that the designer displays two columns, one for the mining structure and one for the `TM_Decision_Tree` mining model, which you created in the previous lesson.  
  
2.  Right-click the **Structure** column and select **New Mining Model**.  
  
3.  In the **New Mining Model** dialog box, in **Model name**, type `TM_Clustering`.  
  
4.  In **Algorithm name**, select **Microsoft Clustering**.  
  
5.  [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
 The new model now appears in the **Mining Models** tab of Data Mining Designer. This model, built with the [!INCLUDE[msCoName](../includes/msconame-md.md)] Clustering algorithm, groups customers with similar characteristics into clusters and predicts bike buying for each cluster. Although you can modify the column usage and properties for the new model, no changes to the `TM_Clustering` model are necessary for this tutorial.  
  
### To create a Naive Bayes mining model  
  
1.  In the **Mining Models** tab of Data Mining Designer, right-clickthe **Structure** column, and select **New Mining Model**.  
  
2.  In the **New Mining Model** dialog box, under **Model name**, type `TM_NaiveBayes`.  
  
3.  In **Algorithm name**, select **Microsoft Naive Bayes**, then click **OK**.  
  
     A message appears stating that the [!INCLUDE[msCoName](../includes/msconame-md.md)] Naive Bayes algorithm does not support the **Age** and **Yearly Income** columns, which are continuous.  
  
4.  Click **Yes** to acknowledge the message and continue.  
  
 A new model appears in the **Mining Models** tab of Data Mining Designer. Although you can modify the column usage and properties for all the models in this tab, no changes to the `TM_NaiveBayes` model are necessary for this tutorial.  
  
## Next Task in Lesson  
 [Processing Models in the Targeted Mailing Structure &#40;Basic Data Mining Tutorial&#41;](../../2014/tutorials/processing-models-in-the-targeted-mailing-structure-basic-data-mining-tutorial.md)  
  
## See Also  
 [Add Mining Models to a Structure &#40;Analysis Services - Data Mining&#41;](../../2014/analysis-services/data-mining/add-mining-models-to-a-structure-analysis-services-data-mining.md)   
 [Data Mining Designer](../../2014/analysis-services/data-mining/data-mining-designer.md)   
 [Moving Data Mining Objects](../../2014/analysis-services/data-mining/moving-data-mining-objects.md)  
  
  
