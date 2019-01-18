---
title: "Drillthrough on Mining Structures | Microsoft Docs"
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
# Drillthrough on Mining Structures
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  *Drillthrough* means the ability to query either a mining model or a mining structure and get detailed data that is not exposed in the model.  
  
 [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] provides two different options for drilling through into case data. You can drill through to the data that were used to build the mining model, or you can drill through to the source data in the mining structure.  
  
## Drillthrough to Model Cases vs. Drillthrough to Structure  
 Drilling through to **model cases** is useful for finding additional details about rules, patterns or clusters in a model.  
  
 In contrast, **drillthrough to structure** data is intended to provide access to information that was not made available in the model. For example, if you have the appropriate permissions, you might want to find out which rows of data were used for training the model and which were used for testing.  
  
 You can also view attributes of the data that were not used in analysis, provided they have been included in the structure definition. For example, often mining structures support many different kinds of models, and some structure columns might have been excluded from a model because the data type was incompatible or the data was not useful for analysis. For example, you would not use customer contact information in a clustering model, even if the data was included in the structure, but by enabling drillthrough you gain access to this information without running separate queries against the data source.  
  
## Enabling Drillthrough to Structure Data  
 To use drillthrough on the mining structure, the following conditions must be met:  
  
-   Drillthrough on the model must also be enabled. By default, drillthrough of both kinds is disabled. To enable drillthrough in the Data Mining Wizard, select the option to enable drillthrough to model cases on the final page of the wizard. You can also add the ability to drillthrough on a model later by changing the **AllowDrillthrough** property.  
  
-   If you create the mining structure by using DMX, use the WITH DRILLTHROUGH clause. For more information, see [CREATE MINING STRUCTURE &#40;DMX&#41;](../../dmx/create-mining-structure-dmx.md).  
  
-   Drillthrough works by retrieving information about the training cases that was cached when you processed the mining structure. Therefore, if you clear the cached data after processing the structure by changing the <xref:Microsoft.AnalysisServices.MiningStructureCacheMode> property to **ClearAfterProcessing**, drillthrough will not work. To enable drillthrough to structure columns, you must change the <xref:Microsoft.AnalysisServices.MiningStructureCacheMode> property to **KeepTrainingCases** and then reprocess the structure.  
  
-   Verify that both the mining structure and the mining model have the [AllowDrillThrough](https://docs.microsoft.com/bi-reference/assl/properties/allowdrillthrough-element-assl) property set to **True**. Moreover, you must be a member of a role that has drillthrough permissions on both the structure and the model.  
  
## Security Issues for Drillthrough  
 Drillthrough permissions are set separately on the structure and model. The model permission lets you drill through from the model, even if you do not have permissions on the structure. Drillthrough permissions on the structure provide the additional ability to include structure columns in drillthrough queries from the model, by using the [StructureColumn &#40;DMX&#41;](../../dmx/structurecolumn-dmx.md) function.  
  
 For information about how to create roles and assign permissions in Analysis Services, see [Role Designer &#40;Analysis Services - Multidimensional Data&#41;](http://msdn.microsoft.com/library/e8ba42db-0565-4d68-b3ab-0c63d8d07192).  
  
> [!NOTE]  
>  If you enable drillthrough on both the mining structure and the mining model, any user who is a member of a role that has drillthrough permissions on the mining model can also view columns in the mining structure, even if those columns are not included in the mining model. Therefore, to protect sensitive data, you should set up the data source view to mask personal information, and allow drillthrough access on the mining structure only when necessary.  
  
## Related Tasks  
 See the following topics for more information about how to use drillthrough with mining models.  
  
|||  
|-|-|  
|Use drillthrough to structure from the mining model viewers|[Use Drillthrough from the Model Viewers](../../analysis-services/data-mining/use-drillthrough-from-the-model-viewers.md)|  
|See examples of drillthrough queries for specific model types.|[Data Mining Queries](../../analysis-services/data-mining/data-mining-queries.md)|  
|Get information about permissions that apply to specific mining structures and mining models.|[Grant permissions on data mining structures and models &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/grant-permissions-on-data-mining-structures-and-models-analysis-services.md)|  
  
## See Also  
 [Drillthrough on Mining Models](../../analysis-services/data-mining/drillthrough-on-mining-models.md)  
  
  
