---
title: "IsTrainingCase (DMX)"
description: "IsTrainingCase (DMX)"
author: minewiskan
ms.author: owend
ms.reviewer: owend
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# IsTrainingCase (DMX)
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  Indicates whether a case is used as a training case for the specified data mining model or mining structure.  
  
## Syntax  
  
```  
  
IsTrainingCase()  
```  
  
## Result Type  
 Returns **true** if the case is a part of the training data set; otherwise **false**.  
  
## Remarks  
 If you use the Data Mining Wizard to create a mining structure and related mining model, by default, 30 percent of the cases are set aside for use as a test data set. The remaining cases in the data source that you specify are used to train the model. However, if you use Data Mining Extensions (DMX) to create the mining model, by default, all data is used to train the model, and no test set is created. To enable the creation of a test data set, you must set the parameters of the WITH HOLDOUT clause.  
  
 You can determine whether the data in a particular data mining structure has been partitioned into testing and training sets by viewing the value of the <xref:Microsoft.AnalysisServices.MiningStructure.HoldoutMaxCases%2A> and <xref:Microsoft.AnalysisServices.MiningStructure.HoldoutMaxPercent%2A> properties.  
  
> [!NOTE]  
>  Drillthrough must be enabled on the model if you want to use the IsTrainingCase or IsTestCase functions to return details about the cases in the model. For more information, see [Enable Drillthrough for a Mining Model](/analysis-services/data-mining/enable-drillthrough-for-a-mining-model).  
  
 To return cases that are part of the test data set, use the function [IsTestCase &#40;DMX&#41;](../dmx/istestcase-dmx.md).  
  
## Examples  
 The following example uses the clustering data mining model from the targeted mailing scenario in the [Basic Data Mining Tutorial](/previous-versions/sql/sql-server-2016/ms167167(v=sql.130)). The query returns only those cases that were used for training the mining model. Moreover, the training cases are restricted to customers younger than 40.  
  
```  
SELECT *  
FROM [TM Clustering].CASES  
WHERE IsTrainingCase()  
AND [Age] <40  
```  
  
 For other examples of how to query cases used in data mining, see [SELECT FROM &#60;model&#62;.CASES &#40;DMX&#41;](../dmx/select-from-model-cases-dmx.md) and [SELECT FROM &#60;structure&#62;.CASES](../dmx/select-from-structure-cases.md).  
  
## See Also  
 [Training and Testing Data Sets](/analysis-services/data-mining/training-and-testing-data-sets)   
 [Functions &#40;DMX&#41;](../dmx/functions-dmx.md)   
 [Data Mining Queries](/analysis-services/data-mining/data-mining-queries)  
  
