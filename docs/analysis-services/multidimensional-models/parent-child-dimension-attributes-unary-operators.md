---
title: "Unary Operators in Parent-Child Dimensions | Microsoft Docs"
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
# Parent-Child Dimension Attributes - Unary Operators
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  In a dimension that contains a parent-child relationship in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], you specify a unary (or custom rollup) operator column that determines the custom rollup for all noncalculated members of the parent attribute. The unary operator is applied to members whenever the values of the parent members are evaluated. The **UnaryOperatorColumn** on a parent attribute (**Usage**=Parent) specifies the column of a table in the data source view that contains unary operators. Values for the custom rollup operators that are stored in this column are applied to each member of the attribute.  
  
 You can create and specify a named calculation on a dimension table in the data source view as a unary operator column. The simplest expression, such as '+', returns the same operator for all members. But you can use any expression as long as it returns an operator for every member.  
  
 You can change the **UnaryOperatorColumn** property setting manually on a parent attribute or use the Define Custom Aggregation enhancement of the Business Intelligence Wizard to replace the default aggregation that is associated with members of a dimension. For more information about how to use the Business Intelligence Wizard to perform this configuration, see [Add a Custom Aggregation to a Dimension](../../analysis-services/multidimensional-models/bi-wizard-add-a-custom-aggregation-to-a-dimension.md).  
  
 The default setting for the **UnaryOperatorColumn** property on a parent attribute is (none), which disables the custom rollup operators. The following table lists the unary operators and describes how they behave when they are applied to a level.  
  
|Unary operator|Description|  
|--------------------|-----------------|  
|+ (plus sign)|The value of the member is added to the aggregate value of the sibling members that occur before the member. This operator is the default operator if no unary operator column is defined for an attribute.|  
|- (minus sign)|The value of the member is subtracted from the aggregate value of the sibling members that occur before the member.|  
|* (asterisk)|The value of the member is multiplied by the aggregate value of the sibling members that occur before the member.|  
|/ (slash mark)|The value of the member is divided by the aggregate value of the sibling members that occur before the member.|  
|~ (tilde)|The value of the member is ignored.|  
  
 Blank values and any other values not found in the table are treated the same as the plus sign (+) unary operator. There is no operator precedence, so the order of members as stored in the unary operator column determines the order of evaluation. To change the order of evaluation, create a new attribute, set its **Type** property to **Sequence**, and then assign sequence numbers that correspond to the order of evaluation in its **Source Column** property. You must also order members of the attribute by that attribute. For information about how to use the Business Intelligence Wizard to order members of an attribute, see [Define the Ordering for a Dimension](../../analysis-services/multidimensional-models/bi-wizard-define-the-ordering-for-a-dimension.md).  
  
 You can use the **UnaryOperatorColumn** property to specify a named calculation that returns a unary operator as a literal character for all members of the attribute. This could be as simple as typing a literal character such as `'*'` in the named calculation. This would replace the default operator, the plus sign (+), with the multiplication operator, the asterisk (*), for all members of the attribute. For more information, see [Define Named Calculations in a Data Source View &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/define-named-calculations-in-a-data-source-view-analysis-services.md).  
  
 In the **Browser** tab of Dimension Designer, you can view the unary operators next to each member in a hierarchy. You can also change the unary operators when you work with a write-enabled dimension. If the dimension is not write-enabled, you must use a tool to modify the data source directly.  
  
## See Also  
 [Dimension Attribute Properties Reference](../../analysis-services/multidimensional-models/dimension-attribute-properties-reference.md)   
 [Custom Rollup Operators in Parent-Child Dimensions](../../analysis-services/multidimensional-models/parent-child-dimension-attributes-custom-rollup-operators.md)   
 [Start the Business Intelligence Wizard in Dimension Designer](../../analysis-services/multidimensional-models/database-dimensions-bi-wizard-in-dimension-designer.md)  
  
  
