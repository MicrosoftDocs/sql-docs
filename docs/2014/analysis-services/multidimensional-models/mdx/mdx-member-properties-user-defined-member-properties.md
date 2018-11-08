---
title: "User-Defined Member Properties (MDX) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "custom member properties [MDX]"
ms.assetid: b64cc581-e784-42c4-bec8-932abd687423
author: minewiskan
ms.author: owend
manager: craigg
---
# User-Defined Member Properties (MDX)
  User-defined member properties can be added to a specific named level in a dimension as attribute relationships. User-defined member properties cannot be added to the `(All)` level of a hierarchy, or to the hierarchy itself.  
  
## Creating User-Defined Member Properties  
 User-defined member properties can be added to server-based dimensions or cubes either through the user interface or programmatically:  
  
-   To add user-defined member properties through the user interface, you use Dimension Designer in [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)]. For more information, see [Define Attribute Relationships](../attribute-relationships-define.md).  
  
-   To add user-defined member properties programmatically, your application can use either Analysis Manager Objects (AMO) or a combination of XML for Analysis (XMLA) and Analysis Services Scripting Language (ASSL). For more information, see [Attribute Relationships](../../multidimensional-models-olap-logical-dimension-objects/attribute-relationships.md).  
  
## Retrieving User-Defined Member Properties  
 You can retrieve user-defined member properties using either the `PROPERTIES` keyword or the [Properties](/sql/mdx/properties-mdx) function.  
  
### Using the PROPERTIES Keyword to Retrieve User-Defined Member Properties  
 The syntax that retrieves user-defined member properties is similar to that used to retrieve intrinsic level member properties, as shown in the following syntax:  
  
 `DIMENSION PROPERTIES [Dimension.]Level.<Custom_Member_Property>`  
  
 The `PROPERTIES` keyword appears after the set expression of the axis specification. For example, the following MDX query the `PROPERTIES` keyword retrieves the `List Price` and `Dealer Price` user-defined member properties and appears after the set expression that identifies the products sold in January:  
  
```  
SELECT   
   CROSSJOIN([Ship Date].[Calendar].[Calendar Year].Members,   
             [Measures].[Sales Amount]) ON COLUMNS,  
   NON EMPTY Product.Product.MEMBERS  
   DIMENSION PROPERTIES   
              Product.Product.[List Price],  
              Product.Product.[Dealer Price]  ON ROWS  
FROM [Adventure Works]  
WHERE ([Date].[Month of Year].[January])   
```  
  
### Using the Properties Function to Retrieve User-Defined Member Properties  
 Alternatively, you can access custom member properties with the `Properties` function. For example, the following MDX query uses the `WITH` keyword to create a calculated member consisting of the `List Price` member property:  
  
```  
WITH   
   MEMBER [Measures].[Product List Price] AS  
   [Product].[Product].CurrentMember.Properties("List Price")  
SELECT   
   [Measures].[Product List Price] on COLUMNS,  
   [Product].[Product].MEMBERS  ON Rows  
FROM [Adventure Works]  
```  
  
 For more information about building calculated members, see [Building Calculated Members in MDX &#40;MDX&#41;](mdx-calculated-members-building-calculated-members.md).  
  
## See Also  
 [Using Member Properties &#40;MDX&#41;](mdx-member-properties.md)   
 [Properties &#40;MDX&#41;](/sql/mdx/properties-mdx)  
  
  
