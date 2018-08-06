---
title: "Properties for Mining Structure and Structure Columns | Microsoft Docs"
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
# Properties for Mining Structure and Structure Columns
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  You can set or change the properties for a mining structure and for its associated columns and nested tables by using the **Mining Structure** tab of Data Mining Designer. Properties that you set in this tab are propagated to each mining model that is associated with the structure.  
  
> [!NOTE]  
>  If you change the value of any property in the mining structure, even metadata such as a name or description, the mining structure and its models must be reprocessed before you can view or query the model.  
  
## Properties of Mining Structures and Mining Structure Columns  
 The following table describes the properties for the mining structure and the mining structure columns that are specific to data mining, and that you can view or configure in the **Mining Structure** tab. To view or configure these properties, right-click an element in the tree view, and then click **Properties**.  
  
-   To view the properties of the structure, click the mining structure heading.  
  
-   To view the properties of a column or a nested table, click the column name.  
  
### Properties of the Mining Structure  
  
|Property|Description|  
|--------------|-----------------|  
|**CacheMode**|Specifies whether cases used in training should be cached or discarded after training is completed. **Note:**  This property must be set to **KeepTrainingCases** to enable drillthrough and holdout.|  
|**Collation**|Specifies the default collation for the column. If a collation is not specified, the collation of the server is used.|  
|**Description**|Describes the mining structure. As a best practice, the description should state the purpose and composition of the data in the structure.|  
|**ErrorConfiguration (default)**|Specifies options for special handling of errors, if any.|  
|**HoldoutMaxCases**|Specifies the maximum number of structure cases that can be reserved as a test data set.  If values are specified for both **HoldoutMaxCases** and **HoldoutPercent**, the conditions are combined. **Note:**  To set this property, <xref:Microsoft.AnalysisServices.MiningStructure.CacheMode%2A> must be set to **KeepTrainingCases**.|  
|**HoldoutPercent**|Specifies the percentage of the structure cases to reserve as a test data set. If values are specified for both **HoldoutMaxCases** and **HoldoutPercent**, the conditions are combined. **Note:**  To set this property, <xref:Microsoft.AnalysisServices.MiningStructure.CacheMode%2A> must be set to **KeepTrainingCases**.|  
|**HoldoutSeed**|Specifies a seed to initialize partitioning of the holdout test set, to ensure that the test data set can be re-created. **Note:**  To set this property, <xref:Microsoft.AnalysisServices.MiningStructure.CacheMode%2A> must be set to **KeepTrainingCases**.|  
|**ID**|Displays the unique identifier of the mining structure.<br /><br /> The name that you assigned to the mining structure when you created the structure is used as the ID. If you later change the name by typing a new value for the **Name** property, the new name is used as an alias only; the ID does not change.|  
|**Language**|Specifies the language for the captions in the mining structure.|  
|**Name**|Specifies the name or alias of the mining structure.<br /><br /> If you change the value for the Name property, the new name is used as a caption or alias only; the identifier for the mining structure does not change.|  
|**Source**|Displays the name of the data source, and the type of data source.|  
  
### Properties of the Mining Structure Columns  
  
|Property|Description|  
|--------------|-----------------|  
|**ClassifiedColumns**|Identifies the column that a classified column describes.|  
|**Content**|The content type of the column.|  
|**Description**|Describes the column. As a best practice, the description of the column should provide information about how the data in the column has been derived or altered for data mining.|  
|**DiscretizationBucketCount**|Displays the number of buckets in the discretized column.<br /><br /> Enabled only if the content type is set to **Discretized**.<br /><br /> This property is read-only.|  
|**DiscretizationMethod**|Displays the method that was used to discretize the column.<br /><br /> Enabled only if the content type is set to **Discretized**.<br /><br /> This property is read-only.|  
|**Distribution**|Specifies the distribution of content in the column.|  
|**ID**|Displays the identifier of the column.<br /><br /> If you change the value of the Name property of the column, it does not affect the value of the ID property.|  
|**IsKey**|Indicates whether the column is a key column.|  
|**KeyColumns**|Contains the definition of a column that is the key or is part of the key for an attribute.|  
|**ModelingFlags**|Sets additional parameters that are made available by the algorithm.|  
|**Name**|The name of the column.|  
|**NameColumn**|Identifies the column that provides the name of the parent element.|  
|**Source**|Displays the source of the column.<br /><br /> For relational data sources, the value is always **(none)**.<br /><br /> For structures based on an OLAP cube, the value is the MDX statement that defines the slice used as the source for the nested table.|  
|**SourceMeasureGroup**|Displays the source of the measure group.<br /><br /> For relational data sources, the value is always **(none)**.<br /><br /> For structures based on an OLAP cube, the value is the MDX statement that defines the slice used as the source for the nested table.|  
|**Type**|The data type for the content in the column.|  
  
 For more information about setting or changing properties, see [Mining Structure Tasks and How-tos](../../analysis-services/data-mining/mining-structure-tasks-and-how-tos.md).  
  
## See Also  
 [Create a Relational Mining Structure](../../analysis-services/data-mining/create-a-relational-mining-structure.md)   
 [Mining Structure Columns](../../analysis-services/data-mining/mining-structure-columns.md)  
  
  
