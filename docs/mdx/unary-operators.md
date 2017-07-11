---
title: "Unary Operators | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "unary operators"
ms.assetid: bf1b5518-6040-4484-9ce8-79c0eb4373a9
caps.latest.revision: 27
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Unary Operators
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  In Multidimensional Expressions (MDX), unary operators perform an operation on a single operand, such as returning the negative or positive value of a numeric expression.  
  
 MDX supports the unary operators listed in the following table.  
  
|Operator|Description|  
|--------------|-----------------|  
|[- (Negative)](../mdx/negative-mdx.md)|Returns the negative value of a numeric expression.|  
|[+ (Positive)](../mdx/positive-mdx.md)|Returns the positive value of a numeric expression.|  
  
 The following example demonstrates the use of a unary operator to return the negative value of a measure:  
  
```  
WITH   
   MEMBER [Measures].[NegDiscountAmount] AS  
   -[Measures].[Discount Amount]  
SELECT   
   {[Measures].[Discount Amount],[Measures].[NegDiscountAmount]} on COLUMNS,  
   NON EMPTY [Product].[Product].MEMBERS  ON Rows  
FROM [Adventure Works]  
WHERE [Product].[Category].[Bikes]  
```  
  
 In addition, MDX uses special unary operators to determine the aggregation operation performed by the [RollupChildren](../mdx/rollupchildren-mdx.md) function. For more information on these special unary operators, see [Add a Custom Aggregation to a Dimension](../analysis-services/multidimensional-models/bi-wizard-add-a-custom-aggregation-to-a-dimension.md).  
  
## See Also  
 [Operators &#40;MDX Syntax&#41;](../mdx/operators-mdx-syntax.md)  
  
  
