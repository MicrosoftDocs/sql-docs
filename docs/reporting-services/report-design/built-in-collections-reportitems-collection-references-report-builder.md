---
title: "ReportItems Collection References (Report Builder and SSRS) | Microsoft Docs"
ms.date: 03/01/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: edc0c75f-0530-4e6d-85aa-3385301bfd00
author: markingmyname
ms.author: maghan
---
# Built-in Collections - ReportItems Collection References (Report Builder)
  The **ReportItems** built-in collection is the set of text boxes from report items such as rows of a data region or text boxes on the report design surface. The **ReportItems** collection includes text boxes that are in the current scope of a page header, page footer, or report body. This collection is determined at run time by the report processor and the report renderer. The current scope changes as the report processor successively combines report data and the report item layout elements as the user views pages of a report. You can use the **ReportItems** built-in collection to produce dictionary-style page headers that show the first and last items on each page.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Using the ReportItems Value Property  
 Items within the **ReportItems** collection have only one property: Value. The value for a **ReportItems** item can be used to display or calculate data from another field in the report. To access the value of the current text box, you can use the [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] built-in global Me.Value or simply Value. In report functions such as First and aggregate functions, use the fully qualified syntax.  
  
 For example:  
  
-   This expression, placed in a text box, displays the value of a **ReportItem** text box named `Textbox1`:  
  
     `=ReportItems!Textbox1.Value`  
  
-   This expression, placed in a **ReportItem** text box Color property, displays the text in black when the value is > 0; otherwise, the value is displayed in red:  
  
     `=IIF(Me.Value > 0,"Black","Red")`  
  
-   This expression, placed in a text box in the page header or page footer, displays the first value per page of the rendered report, for a text box named `LastName`:  
  
     `=First(ReportItems("LastName").Value)`  
  
## Dictionary-Style Page Header Expressions  
 You can create a page header to display the first customer on the page and the last customer on the page. Because a text box in the page header can only refer to the **ReportItems** built-in collection once in an expression, you need to add two text boxes to the page header: one for the first customer name (`=First(ReportItems!textboxLastName.Value`) and one for the last customer name (`=Last(ReportItems!textboxLastName.Value`).  
  
 In a page header or page footer section, only text boxes on the current page are available as a member of the **ReportItems** collection. For example, if `ReportItems!textboxLastName.Value` refers to a text box that only appears on the first page for a multipage data region, you see a value for the first page, but all other pages display **#Error** to show the expression could not be evaluated as written.  
  
## Scope for the ReportItems Collection  
 As the report is processed, each text box in the report body or in a data region is evaluated in the context of its dataset, data region, and group associations. The scope for a reference to the **ReportItems** collection is the current scope or any point higher than the current scope.  
  
 For example, a text box in a row that is in a parent group must not contain an expression that refers to the name of a text box in a child group row. Such an expression does not resolve to a value in the report because the child row text box is out of scope. For more information, see [Aggregate Functions Reference &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/report-builder-functions-aggregate-functions-reference.md).  
  
## See Also  
 [Built-in Collections in Expressions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/built-in-collections-in-expressions-report-builder.md)   
 [Expression Examples &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expression-examples-report-builder-and-ssrs.md)   
 [Pagination in Reporting Services &#40;Report Builder  and SSRS&#41;](../../reporting-services/report-design/pagination-in-reporting-services-report-builder-and-ssrs.md)   
 [Filter, Group, and Sort Data &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/filter-group-and-sort-data-report-builder-and-ssrs.md)  
  
  
