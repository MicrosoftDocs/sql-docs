---
description: "MDX Data Definition - ALTER CUBE"
title: "ALTER CUBE Statement (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# MDX Data Definition - ALTER CUBE


  Alters the structure of a specified cube, typically used to support dimension writeback. For more information about using writeback in an application, see this blog post: [Building a Writeback Application with Analysis Services (blog)](/archive/blogs/data_otaku/building-a-writeback-application-with-analysis-services)  
  
 Note that concurrent dimension writebacks can result in a deadlock, where the first writeback is blocked from a commit because of the shared lock held by the second writeback. There is no error that is generated in this situation, but neither operation is able to progress. Eventually both time out and the changes are rolled back.  
  
## Syntax  
  
```  
  
ALTER CUBE  
      Cube_Name | CURRENTCUBE  
      <alter clause>   
            [ < alter clause> ...n]  
  
< alter clause> ::=   
   <create dimension member clause>   
  | <remove dimension member clause>  
  | <move dimension member clause>   
    | <update clause>   
    | <create cell calculation clause>  
  
<create dimension member clause> ::=  
CREATE DIMENSION MEMBER [ParentName.]MemberName  
    , [[KEY = Key_Value]   
    | [Property_Name = Property_Value[, ...n]]  
  
<dropping clause>::=  
DROP   
      DIMENSION MEMBER Member_Name   
            Member_Name ...n ]   
      [WITH DESCENDANTS]  
      | [ SESSION ] [ CALCULATED ] MEMBER Member_Name   
                  [ ,Member_Name,...n ]   
    | SET Set_Name  
                  [ ,Set_Name,...n ]   
    | [ SESSION ] CELL CALCULATION CellCalc_Name  
                  [ ,CellCalc_Name,...n ]   
    | ACTION Action_Name  
  
<move dimension member clause> ::=  
MOVE DIMENSION MEMBER MemberName  
        [, SKIPPED_LEVELS = Unsigned_Integer]   
      [WITH DESCENDANTS]  
    UNDER ParentName      
  
<update clause> ::=  
UPDATE   
    CUSTOM ROLLUP FOR MEMBER MemberName  
      [,MemberName, ...n] AS MDX_Expression  
   | DIMENSION Dimension_Name | Hierarchy_Name  
      , DEFAULT_MEMBER = MDX_Expression  
   | DIMENSION MEMBER MemberName AS  
   [MDX_Expression]  
   [Property_Name = Property_Value[, ...n]]  
  
<create cell calculation clause>::=  
CELL CALCULATION Calculation_Name   
   FOR Set_Expression AS MDX_Expression   
            [ [ CONDITION = 'Logical_Expression' ]   
    | [ DISABLED = { TRUE | FALSE } ]   
    | [ DESCRIPTION =String ]   
    | [ CALCULATION_PASS_NUMBER = Integer]   
    | [ CALCULATION_PASS_DEPTH = Integer]   
    | [ SOLVE_ORDER = Integer]   
    | [ Calculation_Name= Scalar_Expression ], ...n]  
```  
  
## Creating a Dimension Member  
 A new row is added to the underlying dimension table.  
  
### Arguments  
 *ParentName*  
 A valid string expression that provides the name of the parent of the new dimension member, unless the dimension member is being created at the root.  
  
 *MemberName*  
 A valid string expression that provides a member name.  
  
 *Key_Value*  
 A valid scalar expression that defines the new dimension member's key value.  
  
 *Property_Name*  
 A valid Multidimensional Expressions (MDX) identifier that represents a member property.  
  
 *Property_Value*  
 A valid Multidimensional Expressions (MDX) scalar expression that defines the calculated member property's value.  
  
## Dropping a Dimension Member  
 Dropping a dimension member from a write-enabled dimension deletes the member and its corresponding row from the underlying dimension table.  
  
### Arguments  
 *Cube_Name*  
 A valid string expression providing a cube name.  
  
 *Member_Name*  
 A valid string expression providing a member name or member key.  
  
### Remarks  
 If the WITH DESCENDANTS clause is not used, children of a dropped member become children of the dropped member's parent. If the WITH DESCENDANTS clause is used, all descendants and their rows in the dimension table are also dropped.  
  
> [!NOTE]  
>  For information about dropping calculated members, named sets, actions, and cell calculations, see [DROP MEMBER Statement &#40;MDX&#41;](../mdx/mdx-data-definition-drop-member.md), [DROP SET Statement &#40;MDX&#41;](../mdx/mdx-data-definition-drop-set.md), [DROP ACTION Statement &#40;MDX&#41;](../mdx/mdx-data-definition-drop-action.md), and [DROP CELL CALCULATION Statement &#40;MDX&#41;](../mdx/mdx-data-definition-drop-cell-calculation.md).  
  
## Updating the Default Dimension Member  
 This clause updates the default member of a cube and is used in the MDX calculation script to define a default member. The default member can be specified for the database dimension, a cube dimension, or for a user's login. The default member can also be changed during a session.  
  
### Arguments  
 *Dimension_Name*  
 A valid string that provides the name of a dimension.  
  
 *MDX_Expression*  
 A valid MDX expression that returns a single member.  
  
### Remarks  
 The specified MDX expression can be static or dynamic.  
  
## Moving a Dimension Member  
 A row is modified in the underlying dimension table.  
  
### Arguments  
 *ParentName*  
 A valid string expression that provides the name of the new parent for the dimension member being moved.  
  
 *MemberName*  
 A valid string expression that provides a member name.  
  
 Unsigned_*Integer*  
 A valid number specifying the number of levels to skip.  
  
 If the WITH DESCENDANTS clause is specified, the entire tree is moved. If the WITH DESCENDANTS clause is not specified, the children of a moved parent become the children of the moved member's parent. The effect of a move is simply to update the values for the parent key column in the underlying dimension table.  
  
## Updating a Dimension Member  
 The UPDATE DIMENSION MEMBER clause allows you to modify properties of a member as well as the custom member formula associated with a member.  
  
### Arguments  
 *MemberName*  
 A valid string expression that provides a member name.  
  
 *MDX_Expression*  
 A valid MDX expression that returns a single member.  
  
 *Property_Value*  
 A valid MDX scalar expression that defines the calculated member property's value.  
  
## Creating a Cell Calculation  
 For more information about creating a cell calculation using the ALTER CUBE statement, see [DROP CELL CALCULATION Statement &#40;MDX&#41;](../mdx/mdx-data-definition-drop-cell-calculation.md).  
  
## See Also  
 [MDX Data Definition Statements &#40;MDX&#41;](../mdx/mdx-data-definition-statements-mdx.md)  
  
