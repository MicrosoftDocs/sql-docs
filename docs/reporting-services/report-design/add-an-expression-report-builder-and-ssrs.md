---
title: "Add an Expression (Report Builder and SSRS) | Microsoft Docs"
ms.date: 03/14/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: a60ee091-b4ed-41e1-9b6a-032c316cd03f
author: markingmyname
ms.author: maghan
---
# Add an Expression (Report Builder and SSRS)
  Expressions are used throughout a report for defining report item properties, filters, groups, sort order, connection strings, and parameter values. Expressions begin with an equal sign (=) and are written in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)]. They are evaluated at run time by the report processor, which combines the evaluation result with report layout elements.  
  
 Expressions can be simple or complex. Simple expression refer to a single item in a built-in collection. Complex expressions can contain constants, operators, global collection items, and function calls. For more information, see [Expressions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expressions-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To add an expression to a text box  
  
-   In **Design** view, click the text box on the design surface to which you want to add an expression.  
  
    -   For a simple expression, type the display text for the expression in the text box. For example, for the dataset field Sales, type `[Sales]`.  
  
    -   For a complex expression, right-click the text box, and select **Expression**. The **Expression** dialog box opens. Type or interactively create your expression after the '=' in the expression pane, and then click OK.  
  
         The expression appears on the design surface as `<<Expr>>`.  
  
## See Also  
 [Formatting Text and Placeholders &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/formatting-text-and-placeholders-report-builder-and-ssrs.md)   
 [Text Boxes &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/text-boxes-report-builder-and-ssrs.md)   
 [Expression Uses in Reports &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expression-uses-in-reports-report-builder-and-ssrs.md)   
 [Filter Equation Examples &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/filter-equation-examples-report-builder-and-ssrs.md)   
 [Group Expression Examples &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/group-expression-examples-report-builder-and-ssrs.md)   
 [Expression Dialog Box &#40;Report Builder&#41;](https://msdn.microsoft.com/library/e89c4d97-5d41-4b55-8695-79329edac15d)   
 [Expression Examples &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expression-examples-report-builder-and-ssrs.md)   
 [Add Code to a Report &#40;SSRS&#41;](../../reporting-services/report-design/add-code-to-a-report-ssrs.md)  
  
  
