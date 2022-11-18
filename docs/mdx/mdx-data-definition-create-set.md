---
description: "MDX Data Definition - CREATE SET"
title: "CREATE SET Statement (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# MDX Data Definition - CREATE SET


  Creates a named set with session scope for the current cube.  
  
## Syntax  
  
```  
  
CREATE [SESSION] [ STATIC | DYNAMIC ] [HIDDEN] SET   
   CURRENTCUBE | Cube_Name  
      .Set_Name AS 'Set_Expression'  
      [,Property_Name = Property_Value, ...n]  
```  
  
## Arguments  
 *Cube_Name*  
 A valid string expression that provides the name of the cube.  
  
 *Set_Name*  
 A valid string expression that provides the name for the named set being created.  
  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Property_Name*  
 A valid string that provides the name of a set property.  
  
 *Property_Value*  
 A valid scalar expression that defines the set property's value.  
  
## Remarks  
 A named set is a set of dimension members (or an expression that defines a set) that you create to use again. For example, a named set makes it possible to define a set of dimension members that consists of the set of the top ten stores by sales. This set can be defined statically, or by means of a function like [TopCount](../mdx/topcount-mdx.md). This named set can then be used wherever the set of the top 10 stores is needed.  
  
 The CREATE SET statement creates a named set that remains available throughout the session, and therefore, can be used in multiple queries in a session. For more information, see [Creating Session-Scoped Calculated Members &#40;MDX&#41;](/analysis-services/multidimensional-models/mdx/mdx-calculated-members-session-scoped-calculated-members).  
  
 You can also define a named set for use by a single query. To define such a set, you use the WITH clause in the SELECT statement. For more information about the WITH clause, see [Creating Query-Scoped Named Sets &#40;MDX&#41;](/analysis-services/multidimensional-models/mdx/mdx-named-sets-creating-query-scoped-named-sets).  
  
 The *Set_Expression* clause can contain any function that supports MDX syntax. Sets created with the CREATE SET statement that do not specify the SESSION clause have session scope. Use the WITH clause to create a set with query scope.  
  
 Specifying a cube other than the cube that is currently connected causes an error. Therefore, you should use CURRENTCUBE in place of a cube name to denote the current cube.  
  
## Scope  
 A user-defined set can occur within one of the scopes listed in the following table.  
  
 Query scope  
 The visibility and lifetime of the set is limited to the query. The set is defined in an individual query. Query scope overrides session scope. For more information, see [Creating Query-Scoped Named Sets &#40;MDX&#41;](/analysis-services/multidimensional-models/mdx/mdx-named-sets-creating-query-scoped-named-sets).  
  
 Session scope  
 The visibility and lifetime of the set is limited to the session in which it is created. (The lifetime is less than the session duration if a DROP SET statement is issued on the set.) The CREATE SET statement creates a set with session scope. Use the WITH clause to create a set with query scope.  
  
### Example  
 The following example creates a set called Core Products. The SELECT query then demonstrates calling the newly created set. The CREATE SET statement must be executed before the SELECT query can be executed - they cannot be executed in the same batch.  
  
```  
CREATE SET [Adventure Works].[Core Products] AS '{[Product].[Category].[Bikes]}'  
  
SELECT [Core Products] ON 0  
  FROM [Adventure Works]  
```  
  
## Set Evaluation  
 Set evaluation can be defined to occur differently; it can be defined to occur only once at set creation or can be defined to occur every time the set is used.  
  
 STATIC  
 Indicates that the set is evaluated only once at the time the CREATE SET statement is evaluated.  
  
 DYNAMIC  
 Indicates that the set is to be evaluated every time it is used in a query.  
  
## Set Visibility  
 The set can be either visible or not to other users who query the cube.  
  
 HIDDEN  
 Specifies that the set is not visible to users who query the cube.  
  
## Standard Properties  
 Each set has a set of default properties. When a client application is connected to [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], the default properties are either supported, or available to be supported, as the administrator chooses.  
  
|Property identifier|Meaning|  
|-------------------------|-------------|  
|CAPTION|A string that the client application uses as the caption for the set.|  
|DISPLAY_FOLDER|A string that identifies the path of the display folder that the client application uses to show the set. The folder level separator is defined by the client application. For the tools and clients supplied by [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], the backslash (\\) is the level separator. To provide multiple display folders for a defined set, use a semicolon (;) to separate the folders.|  
  
## See Also  
 [DROP SET Statement &#40;MDX&#41;](../mdx/mdx-data-definition-drop-set.md)   
 [MDX Data Definition Statements &#40;MDX&#41;](../mdx/mdx-data-definition-statements-mdx.md)  
  
