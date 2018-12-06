---
title: "Configure Measure Properties | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: multidimensional-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Configure Measure Properties
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Measures have properties that enable you to define how the measures function and to control how the measures appear to users.  
  
 You can set properties in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] when creating or editing a cube or measure. You can also set them programmatically, using MDX or AMO. See [Create Measures and Measure Groups in Multidimensional Models](../../analysis-services/multidimensional-models/create-measures-and-measure-groups-in-multidimensional-models.md) or [CREATE MEMBER Statement &#40;MDX&#41;](../../mdx/mdx-data-definition-create-member.md) or [Programming AMO OLAP Basic Objects](https://docs.microsoft.com/bi-reference/amo/programming-amo-olap-basic-objects) for details.  
  
## Measure Properties  
 Measures inherit certain properties from the measure group of which they are a member, unless those properties are overridden at the measure level. Measure properties determine how a measure is aggregated, its data type, the name that is displayed to the user, the display folder in which the measure will appear, its format string, any measure expression, the underlying source column, and its visibility to users.  
  
|Property|Definition|  
|--------------|----------------|  
|**AggregateFunction**|Required. Determines how measures are aggregated. **Sum** is the default aggregation. For more information, see [Use Aggregate Functions](../../analysis-services/multidimensional-models/use-aggregate-functions.md) for a description of each function.|  
|**DataType**|Required. Specifies the data type of the column in the underlying fact table to which the measure is bound. This value is inherited from the source column by default.|  
|**Description**|Provides a description of the measure, which may be exposed in client applications.|  
|**DisplayFolder**|Specifies the folder in which the measure will appear when users connect to the cube. When a cube has many measures, you can use display folders to categorize the measures and improve the user browsing experience.|  
|**FormatString**|You can select the format that is used to display measure values to users by using the **FormatString** property of the measure.<br /><br /> Although a list of display formats is provided in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], you can specify many additional formats that are not in the list. You can specify any named or user-defined format that is valid in Microsoft Visual Basic.|  
|**ID**|Required. Displays the unique identifier (ID) of the measure. This property is read-only.|  
|**MeasureExpression**|Specifies a constrained MDX expression defining the value of the measure. The expression is evaluated at the leaf level before being aggregated, and allows for weighting of a value. For example, in currency conversion where a sales amount is weighted by the exchange rate.|  
|**Name**|Required. Specifies the name of the measure.|  
|**Source**|Required. Specifies the column in the data source view to which the measure is bound. See [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../../analysis-services/multidimensional-models/data-sources-and-bindings-ssas-multidimensional.md).|  
|**Visible**|Determines the visibility of the measure in client applications.|  
  
## See Also  
 [Configure Measure Group Properties](../../analysis-services/multidimensional-models/configure-measure-group-properties.md)   
 [Modifying Measures](../../analysis-services/lesson-3-1-modifying-measures.md)  
  
  
