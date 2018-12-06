---
title: "Define Semiadditive Behavior (Business Intelligence Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.biwizard.semiadditivememberdetection.f1"
ms.assetid: e57080ba-ce96-40f8-bca7-6701d1725b3c
author: minewiskan
ms.author: owend
manager: craigg
---
# Define Semiadditive Behavior (Business Intelligence Wizard)
  Use the **Define Semiadditive Behavior** page to enable or disable semi-additive behavior on measures. Semi-additive behavior determines how measures that are contained by a cube are aggregated over a time dimension.  
  
> [!NOTE]  
>  With the exception of LastChild which is available in the standard edition, semi-additive behaviors are only available in the business intelligence or enterprise editions. Furthermore, because semiadditive behavior is defined only on measures and not dimensions, you will not find this page in the Business Intelligence Wizard if it was started from Dimension Designer or by right-clicking a dimension in Solution Explorer in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)].  
  
## Options  
 **Turn off semiadditive behavior**  
 Disables semi-additive behavior in all measures contained by the cube.  
  
 **The wizard has detected the \<dimension name> account dimension, which contains semiadditive members. The server will aggregate members of this dimension according to the semiadditive behavior specified for each account type.**  
 Enables semi-additive behavior for account dimensions that contain semi-additive members. Selecting this option sets the aggregation function of all measures in measure groups that reference the account dimension to `ByAccount`.  
  
 For more information about account dimensions, see [Create a Finance Account of parent-child type Dimension](multidimensional-models/database-dimensions-finance-account-of-parent-child-type.md).  
  
 **Define semiadditive behavior for individual members**  
 Enables semi-additive behavior and specifies the semi-additive aggregation function for specific measures. The aggregation function applies to all dimensions that are referenced by the measure group that contains the measure.  
  
 **Measures**  
 Displays the name of a measure contained by the cube.  
  
 **Semiadditive Function**  
 Select the aggregation function for the selected measure. The following table lists the aggregation functions that are available.  
  
|Value|Description|  
|-----------|-----------------|  
|**AverageOfChildren**|Aggregated by returning the average of the measure's children.|  
|`ByAccount`|Aggregated by the aggregation function associated with the specified account type of an attribute in an account dimension.|  
|`Count`|Aggregated using the `Count` function.|  
|`DistinctCount`|Aggregated using the `DistinctCount` function.|  
|**FirstChild**|Aggregated by returning the measure's first child member over a time dimension.|  
|**FirstNonEmpty**|Aggregated by returning the measure's first nonempty member over a time dimension.|  
|**LastChild**|Aggregated by returning the measure's last child member over a time dimension.|  
|**LastNonEmpty**|Aggregated by returning the measure's last nonempty member over a time dimension.|  
|`Max`|Aggregated using the `Max` function.|  
|`Min`|Aggregated using the `Min` function.|  
|**None**|No aggregation performed.|  
|`Sum`|Aggregated using the `Sum` function.|  
  
> [!NOTE]  
>  Selections made for this option apply only if **Define semiadditive behavior for individual members** is selected.  
  
## See Also  
 [Business Intelligence Wizard F1 Help](business-intelligence-wizard-f1-help.md)   
 [Cube Designer &#40;Analysis Services - Multidimensional Data&#41;](cube-designer-analysis-services-multidimensional-data.md)   
 [Dimension Designer &#40;Analysis Services - Multidimensional Data&#41;](dimension-designer-analysis-services-multidimensional-data.md)  
  
  
