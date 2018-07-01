---
title: "Use Aggregate Functions | Microsoft Docs"
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
# Use Aggregate Functions
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  When a dimension is used to slice a measure, the measure is summarized along the hierarchies contained in that dimension. The summation behavior depends on the aggregate function specified for the measure. For most measures containing numeric data, the aggregate function is **Sum**. The value of the measure will sum to different amounts depending on which level of the hierarchy is active.  
  
 In Analysis Services, every measure that you create is backed by an aggregation function that determines the measure's operation. Predefined aggregation types include **Sum**, **Min**, **Max**, **Count**, **Distinct Count**, and several other more specialized functions. Alternatively, if you require aggregations based on complex or custom formulas, you can build an MDX calculation in lieu of using a prebuilt aggregation function. For example, if you want to define a measure for a percentage value, you would do that in MDX, using a calculated measure. See [CREATE MEMBER Statement &#40;MDX&#41;](../../mdx/mdx-data-definition-create-member.md).  
  
 Measures that are created via the Cube Wizard are assigned an aggregation type as part of the measure definition. The aggregation type is always **Sum**, assuming the source column contains numeric data. **Sum** is assigned regardless of the source column's data type. For example, if you used the Cube Wizard to create measures, and you pulled in all columns from a fact table, you will notice that all of the resulting measures have an aggregation of **Sum**, even if the source is a date time column. Always review the pre-assigned aggregation methods for measures created via the wizard to make sure the aggregation function is suitable.  
  
 You can assign or change the aggregation method in the either the cube definition, via [!INCLUDE[ss_dtbi](../../includes/ss-dtbi-md.md)], or via MDX. See [Create Measures and Measure Groups in Multidimensional Models](../../analysis-services/multidimensional-models/create-measures-and-measure-groups-in-multidimensional-models.md) or [Aggregate &#40;MDX&#41;](../../mdx/aggregate-mdx.md) for further instructions.  
  
##  <a name="AggFunction"></a> Aggregate Functions  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] provides functions to aggregate measures along the dimensions that are contained in measure groups. The *additivity* of an aggregation function determines how the measure is aggregated across all the dimensions in the cube. Aggregation functions fall into three levels of additivity:  
  
 Additive  
 An additive measure, also called a fully additive measure, can be aggregated along all the dimensions that are included in the measure group that contains the measure, without restriction.  
  
 Semiadditive  
 A semiadditive measure can be aggregated along some, but not all, dimensions that are included in the measure group that contains the measure. For example, a measure that represents the quantity available for inventory can be aggregated along a geography dimension to produce a total quantity available for all warehouses, but the measure cannot be aggregated along a time dimension because the measure represents a periodic snapshot of quantities available. Aggregating such a measure along a time dimension would produce incorrect results. See [Define Semiadditive Behavior](../../analysis-services/multidimensional-models/define-semiadditive-behavior.md) for details.  
  
 Nonadditive  
 A nonadditive measure cannot be aggregated along any dimension in the measure group that contains the measure. Instead, the measure must be individually calculated for each cell in the cube that represents the measure. For example, a calculated measure that returns a percentage, such as profit margin, cannot be aggregated from the percentage values of child members in any dimension.  
  
 The following table lists the aggregation functions in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], and describes both the additivity and expected output of the function.  
  
|Aggregation function|Additivity|Returned value|  
|--------------------------|----------------|--------------------|  
|**Sum**|Additive|Calculates the sum of values for all child members. This is the default aggregation function.|  
|**Count**|Additive|Retrieves the count of all child members.|  
|**Min**|Semiadditive|Retrieves the lowest value for all child members.|  
|**Max**|Semiadditive|Retrieves the highest value for all child members.|  
|**DistinctCount**|Nonadditive|Retrieves the count of all unique child members. For more details, see [About Distinct Count Measures](../../analysis-services/multidimensional-models/use-aggregate-functions.md#bkmk_distinct) in the next section.|  
|**None**|Nonadditive|No aggregation is performed, and all values for leaf and nonleaf members in a dimension are supplied directly from the fact table for the measure group that contains the measure. If no value can be read from the fact table for a member, the value for that member is set to null.|  
|**ByAccount**|Semiadditive|Calculates the aggregation according to the aggregation function assigned to the account type for a member in an account dimension. If no account type dimension exists in the measure group, treated as the **None** aggregation function.<br /><br /> For more information about account dimensions, see [Create a Finance Account of parent-child type Dimension](../../analysis-services/multidimensional-models/database-dimensions-finance-account-of-parent-child-type.md).|  
|**AverageOfChildren**|Semiadditive|Calculates the average of values for all non-empty child members.|  
|**FirstChild**|Semiadditive|Retrieves the value of the first child member.|  
|**LastChild**|Semiadditive|Retrieves the value of the last child member.|  
|**FirstNonEmpty**|Semiadditive|Retrieves the value of the first non-empty child member.|  
|**LastNonEmpty**|Semiadditive|Retrieves the value of the last non-empty child member.|  
  
##  <a name="bkmk_distinct"></a> About Distinct Count Measures  
 A measure with an **Aggregate Function** property value of **Distinct Count** is called a distinct count measure. A distinct count measure can be used to count occurrences of a dimension's lowest-level members in the fact table. Because the count is distinct, if a member occurs multiple times, it is counted only once. A distinct count measure is always placed in a dedicated measure group. Putting a distinct count measure into its own measure group is a best practice that has been built into the designer as a performance optimization technique.  
  
 Distinct count measures are commonly used to determine for each member of a dimension how many distinct, lowest-level members of another dimension share rows in the fact table. For example, in a Sales cube, for each customer and customer group, how many distinct products were purchased? (That is, for each member of the Customers dimension, how many distinct, lowest-level members of the Products dimension share rows in the fact table?) Or, for example, in an Internet Site Visits cube, for each site visitor and site visitor group, how many distinct pages on the Internet site were visited? (That is, for each member of the Site Visitors dimension, how many distinct, lowest-level members of the Pages dimension share rows in the fact table?) In each of these examples, the second dimension's lowest-level members are counted by a distinct count measure.  
  
 This kind of analysis need not be limited to two dimensions. In fact, a distinct count measure can be separated and sliced by any combination of dimensions in the cube, including the dimension that contains the counted members.  
  
 A distinct count measure that counts members is based on a foreign key column in the fact table. (That is, the measure's **Source Column** property identifies this column.) This column joins the dimension table column that identifies the members counted by the distinct count measure.  
  
## See Also  
 [Measures and Measure Groups](../../analysis-services/multidimensional-models/measures-and-measure-groups.md)   
 [MDX Function Reference &#40;MDX&#41;](../../mdx/mdx-function-reference-mdx.md)   
 [Define Semiadditive Behavior](../../analysis-services/multidimensional-models/define-semiadditive-behavior.md)  
  
  
