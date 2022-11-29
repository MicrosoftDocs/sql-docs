---
description: "SCOPE Statement (MDX)"
title: "SCOPE Statement (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# MDX Scripting - SCOPE


  Limits the scope of specified Multidimensional Expressions (MDX) statements to a specified subcube.  
  
## Syntax  
  
```  
  
SCOPE(Subcube_Expression)   
   [ MDX_Statement ]  
END SCOPE  
  
Subcube_Expression ::=(Auxiliary_Subcube [, Auxiliary_Subcube,...n])  
  
Auxiliary_Subcube ::=   
        Limited_Set   
    | Root([dimension_name])   
    | Leaves([dimension_name])  
  
Limited_Set ::=   
        single_tuple   
    | member   
    | Common_Grain_Members   
    | hierarchy.members   
    | level.members   
    | {}   
    | Descendants  
            (  
                  Member  
         , [level  
         [  
            , SELF   
             | AFTER   
                          | BEFORE   
                          | SELF_AND_AFTER   
                          | SELF_AND_BEFORE   
                          | SELF_BEFORE_AFTER   
                          | LEAVES  
                  ]  
            )   
[* <limited set>]  
```  
  
## Arguments  
 *Subcube_Expression*  
 A valid MDX subcube expression.  
  
 *MDX_Statement*  
 A valid MDX statement.  
  
 *Common_Grain_Members*  
 A valid MDX statement that evaluates to members that have the same grain.  
  
 *single_tuple*  
 A single tuple.  
  
## Remarks  
 The SCOPE statement determines the subcube that will be affected by the running of one or more MDX statements. Unless an MDX statement is framed within a SCOPE statement, the implicit scope of an MDX statement is the entire cube.  
  
> [!NOTE]  
>  Hidden members are exposed in SCOPE statements.  
  
 SCOPE statements will create subcubes that expose "holes" regardless of the **MDX Compatibility** setting. For example, the statement, `Scope( Customer.State.members )`, can include the states in countries or regions that do not contain states, but for which otherwise invisible placeholder members were inserted.  
  
 Calculated members and named sets created within a SCOPE statement are unaffected by the SCOPE statement.  
  
## Example  
 The following example, from the MDX calculation script in the Adventure Works sample solution, defines the current scope as fiscal quarter in fiscal year 2005 and the sales amount quota measure, and then assigns a value to the cells in the current scope by using the **ParallelPeriod** function. The example then modifies the scope using another SCOPE statement, and then performs another assignment using the [This (MDX)](../mdx/this-mdx.md) function.  
  
```  
Scope   
 (   
    [Date].[Fiscal Year].&[2005],  
    [Date].[Fiscal].[Fiscal Quarter].Members,  
    [Measures].[Sales Amount Quota]  
 ) ;     
  
   This = ParallelPeriod                               
          (   
             [Date].[Fiscal].[Fiscal Year], 1,  
             [Date].[Fiscal].CurrentMember   
          ) * 1.35 ;  
  
/*-- Allocate equally to months in FY 2002 -----------------------------*/  
  
  Scope   
  (   
     [Date].[Fiscal Year].&[2002],  
     [Date].[Fiscal].[Month].Members   
  ) ;     
  
    This = [Date].[Fiscal].CurrentMember.Parent / 3 ;     
  
  End Scope ;     
End Scope ;     
```  
  
## See Also  
 [MDX Scripting Statements &#40;MDX&#41;](../mdx/mdx-scripting-statements-mdx.md)  
  
  
