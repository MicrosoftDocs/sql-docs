---
title: "Managing Scope and Context (MDX) | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: mdx
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Managing Scope and Context (MDX)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  In [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)], a Multidimensional Expressions (MDX) script can apply to the entire cube, or to specific portions of the cube, at specific points within the execution of the script. The MDX script can take a layered approach to calculations within a cube through the use of calculation passes.  
  
> [!NOTE]  
>  For more information on how calculation passes can affect calculations, see [Understanding Pass Order and Solve Order &#40;MDX&#41;](../../../analysis-services/multidimensional-models/mdx/mdx-data-manipulation-understanding-pass-order-and-solve-order.md).  
  
 To control the calculation pass, scope, and context within an MDX script, you specifically use the CACULATE statement, the **This** function, and the SCOPE statement.  
  
## Using the CALCULATE Statement  
 The CALCULATE statement populates each cell in the cube with aggregated data. For example, the default MDX script has a single CALCULATE statement at the beginning of the script.  
  
 For more information on the syntax of the CALCULATE statement, see [CALCULATE Statement &#40;MDX&#41;](../../../mdx/mdx-scripting-calculate.md).  
  
> [!NOTE]  
>  If the script contains a SCOPE statement that contains a CALCULATE statement, MDX evaluates the CALCULATE statement within the context of the subcube defined by the SCOPE statement, not against the whole cube.  
  
## Using the This Function  
 The **This** function lets you retrieve the current subcube within an MDX script. You can use the **This** function to quickly set the value of cells within the current subcube to an MDX expression. You often use the **This** function in conjunction with the SCOPE statement to change the contents of a specific subcube during a specific calculation pass.  
  
> [!NOTE]  
>  If the script contains a SCOPE statement that contains a **This** function, MDX evaluates the **This** function within the context of the subcube defined by the SCOPE statement, not against the whole cube.  
  
### This Function Example  
 The following MDX script command example uses the **This** function to increase the value of the Amount measure, in the Finance measure group of the [!INCLUDE[ssAWDWsp](../../../includes/ssawdwsp-md.md)] sample cube, to 10% higher for the children of the Redmond member in the Customer dimension:  
  
```  
/* This SCOPE statement defines the current subcube */  
SCOPE([Customer].&[Redmond].MEMBERS,   
    [Measures].[Amount], *);  
        /* This expression sets the value of the Amount measure */  
        THIS = [Measures].[Amount] * 1.1;  
END SCOPE;  
```  
  
 For more information on the syntax of the **This** function, see [This &#40;MDX&#41;](../../../mdx/this-mdx.md).  
  
## Using the SCOPE Statement  
 The SCOPE statement defines the current subcube that contains, and specifies the scope of, other MDX expressions and statements within an MDX script. MDX evaluates this other MDX expressions and statements, including the **This** function and the CALCULATE statement, within the context of the subcube.  
  
 A SCOPE statement is dynamic, but not iterative in nature. The statements contained within a SCOPE statement run once, but the subcube itself can be dynamically determined. For example, you have a cube named SampleCube. Against the SampleCube cube, you apply the following SCOPE statement to define a subcube the defines the context as the ALLMEMBERS within the Measures dimension:  
  
 `SCOPE([Measures].ALLMEMBERS);`  
  
 `THIS = [Measures].ALLMEMBERS.COUNT;`  
  
 `END SCOPE;`  
  
 The statements and expressions within this SCOPE statement run once.  
  
 Now, a business user runs the following MDX query that contains one measure, named ExistingMeasure, against the SampleCube cube:  
  
 `WITH MEMBER [Measures].[NewMeasure] AS '1'`  
  
 `SELECT`  
  
 `[Measures].ALLMEMBERS ON COLUMNS,`  
  
 `[Customer].DEFAULTMEMBER ON ROWS`  
  
 `FROM`  
  
 `[SampleCube]`  
  
 The cellset returned from the query resembles the output shown in the following table.  
  
||[ExistingMeasure]|[NewMeasure]|  
|-|-------------------------|--------------------|  
|[Customer].[All]|2|2|  
  
 Looking at the returned cellset, notice how the ExistingMeasure value, included in the SCOPE statement within the MDX script, is dynamically updated after the measure NewMeasure was defined.  
  
 A SCOPE statement can be nested within another SCOPE statement. However, as the SCOPE statement is not iterative, the primary purpose for nesting SCOPE statements is to further subdivide a subcube for special treatment.  
  
### SCOPE Statement Example  
 The following MDX script example uses a SCOPE statement to sets the value of the Amount measure, in the Finance measure group of the [!INCLUDE[ssAWDWsp](../../../includes/ssawdwsp-md.md)] sample cube, to 10% higher for the children of the Redmond member in the Customer dimension. However, another SCOPE statement changes the subcube to include the Amount measure for the children of the 2002 calendar year. Finally, the Amount measure is then aggregated only for that subcube, leaving the aggregated values for the Amount measure in other calendar years unchanged.  
  
```  
/* Calculate the entire cube first. */  
CALCULATE;  
/* This SCOPE statement defines the current subcube */  
SCOPE([Customer].&[Redmond].MEMBERS,   
    [Measures].[Amount], *);  
        /* This expression sets the value of the Amount measure */  
        THIS = [Measures].[Amount] * 1.1;  
END SCOPE;  
```  
  
 For more information on the syntax of the SCOPE statement, see [SCOPE Statement &#40;MDX&#41;](../../../mdx/mdx-scripting-scope.md).  
  
## See Also  
 [MDX Language Reference &#40;MDX&#41;](../../../mdx/mdx-language-reference-mdx.md)   
 [The Basic MDX Script &#40;MDX&#41;](../../../analysis-services/multidimensional-models/mdx/the-basic-mdx-script-mdx.md)   
 [MDX Query Fundamentals &#40;Analysis Services&#41;](../../../analysis-services/multidimensional-models/mdx/mdx-query-fundamentals-analysis-services.md)  
  
  
