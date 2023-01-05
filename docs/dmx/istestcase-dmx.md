---
title: "IsTestCase (DMX)"
description: "IsTestCase (DMX)"
author: minewiskan
ms.author: owend
ms.reviewer: owend
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# IsTestCase (DMX)
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  Indicates whether a case is used as a test case for the specified data mining model or mining structure.  
  
## Syntax  
  
```  
  
IsTestCase()  
```  
  
## Result Type  
 Returns **true** if the case is a part of the test data set; otherwise **false**.  
  
## Remarks  
 If you use the Data Mining Wizard to create a mining structure and related mining model, by default, 30 percent of the cases are set aside for use as a test data set. The remaining cases are used for training the data mining model. The same test data set can be used with all models that are based on that structure. However, if you use DMX to create the mining model, by default, all data is used to train the model, and no test set is created. To enable creation of a test data set, you must set the parameters of the WITH HOLDOUT clause.  
  
 You can determine whether a test set has been created on a particular mining structure by viewing the value of the <xref:Microsoft.AnalysisServices.MiningStructure.HoldoutMaxCases%2A> and <xref:Microsoft.AnalysisServices.MiningStructure.HoldoutMaxPercent%2A> properties.  
  
> [!NOTE]  
>  Drillthrough must be enabled on the model if you want to use the IsTrainingCase or IsTestCase functions to return details about the cases in a particular model. For more information, see [Enable Drillthrough for a Mining Model](/analysis-services/data-mining/enable-drillthrough-for-a-mining-model).  
  
 To return cases that are part of the training data set, use the function [IsTrainingCase &#40;DMX&#41;](../dmx/istrainingcase-dmx.md).  
  
## Examples  
 The following example uses the `Targeted Mailing` mining structure that is created in the [Basic Data Mining Tutorial](/previous-versions/sql/sql-server-2016/ms167167(v=sql.130)). The query returns all the cases in the structure that are used for testing.  
  
```  
SELECT *  
FROM [Targeted Mailing].CASES  
WHERE IsTestCase()  
```  
  
 For more information about how to query cases used in data mining, see [SELECT FROM &#60;model&#62;.CASES &#40;DMX&#41;](../dmx/select-from-model-cases-dmx.md) and [SELECT FROM &#60;structure&#62;.CASES](../dmx/select-from-structure-cases.md).  
  
## See Also  
 [Functions &#40;DMX&#41;](../dmx/functions-dmx.md)   
 [Data Mining Queries](/analysis-services/data-mining/data-mining-queries)   
 [Training and Testing Data Sets](/analysis-services/data-mining/training-and-testing-data-sets)  
  
