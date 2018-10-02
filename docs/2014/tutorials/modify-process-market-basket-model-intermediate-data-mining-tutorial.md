---
title: "Modifying and Processing the Market Basket Model (Intermediate Data Mining Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: b6019413-aebd-4ff7-831a-644572ad88b1
author: minewiskan
ms.author: owend
manager: craigg
---
# Modifying and Processing the Market Basket Model (Intermediate Data Mining Tutorial)
  Before you process the association mining model that you created, you must change the default values of two of the parameters: *Support* and *Probability*.  
  
-   *Support* defines the percentage of cases in which a rule must exist before it is considered valid. You will specify that a rule must be found in at least 1 percent of cases.  
  
-   *Probability* defines how likely an association must be before it is considered valid. You will consider any association with a probability of at least 10 percent.  
  
 For more information about the effects of increasing or decreasing support and probability, see [Microsoft Association Algorithm Technical Reference](../../2014/analysis-services/data-mining/microsoft-association-algorithm-technical-reference.md).  
  
 After you have defined the structure and parameters for the **Association** mining model, you will process the model.  
  
### To adjust the parameters of the Association model  
  
1.  Open the **Mining Models** tab of Data Mining Designer.  
  
2.  Right-click the **Association** column in the grid in the designer and select **Set Algorithm Parameters to open the Algorithm Parameters** dialog box.  
  
3.  In the **Value** column of the **Algorithm Parameters** dialog box, set the following parameters:  
  
     MINIMUM_PROBABILITY = 0.1  
  
     MINIMUM_SUPPORT = 0.01  
  
4.  [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
### To process the mining model  
  
1.  On the **Mining Model** menu of [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], select **Process Mining Structure and All Models.**  
  
2.  At the warning asking whether you want to build and deploy the project, click **Yes**.  
  
     The **Process Mining Structure - Association** dialog box opens.  
  
3.  Click **Run**.  
  
     The **Process Progress** dialog box opens to display information about model processing. Processing of the new structure and model might take some time.  
  
4.  After processing is complete, click **Close** to exit the **Process Progress** dialog box.  
  
5.  Click **Close** again to exit the **Process Mining Structure - Association** dialog box.  
  
## Next Task in Lesson  
 [Exploring the Market Basket Models &#40;Intermediate Data Mining Tutorial&#41;](../../2014/tutorials/exploring-the-market-basket-models-intermediate-data-mining-tutorial.md)  
  
## See Also  
 [Processing Requirements and Considerations &#40;Data Mining&#41;](../../2014/analysis-services/data-mining/processing-requirements-and-considerations-data-mining.md)  
  
  
