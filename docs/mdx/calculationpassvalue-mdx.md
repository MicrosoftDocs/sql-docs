---
title: "CalculationPassValue (MDX) | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "CALCULATIONPASSVALUE"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "CalculationPassValue function"
ms.assetid: 1b4012cb-c8c7-441a-bb9c-59430703b189
caps.latest.revision: 45
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# CalculationPassValue (MDX)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns either the numeric or the string value of a Multidimensional Expressions (MDX) expression evaluated over the specified calculation pass of a cube.  
  
## Syntax  
  
```  
  
      Numeric syntax  
CalculationPassValue(Numeric_Expression,Pass_Value [, ABSOLUTE | RELATIVE [,ALL]])  
String syntax  
CalculationPassValue(String_Expression ,Pass_Value [, ABSOLUTE | RELATIVE [,ALL]])  
```  
  
## Arguments  
 *Numeric_Expression*  
 A valid numeric expression that is typically a Multidimensional Expressions (MDX) expression of cell coordinates that return a number.  
  
 *String_Expression*  
 A valid string expression that is typically a valid Multidimensional Expressions (MDX) expression of cell coordinates that return a number expressed as a string.  
  
 *Pass_Value*  
 A valid numeric expression that specifies the calculation pass number.  
  
 ABSOLUTE  
 An access flag value that specifies that the *Pass_Value* parameter contains the zero-based index of the calculation pass. ABSOLUTE is the default access flag value if no access flag value is specified.  
  
 RELATIVE  
 An access flag value that specifies that the *Pass_Value* parameter contains a relative offset from the calculation pass of the triggering calculation. If the offset resolves into a calculation pass index less than 0, calculation pass 0 is used and no error occurs.  
  
 ALL  
 When this flag is set, all values are null except for those loaded by the storage engine. When not set, the values are aggregated without any calculations applied.  
  
## Remarks  
 If a numeric expression is provided, the function returns a numeric value by evaluating the specified MDX numeric expression in the specified calculation pass, and optionally modified by an access flag and an access flag modifier.  
  
 If a string expression is provided, the function returns a string value by evaluating the specified MDX string expression in the specified calculation pass, and optionally modified by an access flag and an access flag modifier*.*  
  
 With automatic recursion resolution in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], this function has little practical use.  
  
> [!NOTE]  
>  Only administrators can use the **CalculationPassValue** function within an MDX script. An error occurs if an MDX script that contains this function is run in the context of a role that does not have administrator privileges.  
  
## See Also  
 [CalculationCurrentPass &#40;MDX&#41;](../mdx/calculationcurrentpass-mdx.md)   
 [IIf &#40;MDX&#41;](../mdx/iif-mdx.md)   
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
