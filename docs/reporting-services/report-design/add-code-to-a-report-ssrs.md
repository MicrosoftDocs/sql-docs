---
title: "Add code to a paginated report"
description: Find out how to call your own custom code for any expression you have in your paginated report in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/14/2017
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "code [Reporting Services]"
  - "custom code [Reporting Services]"
  - "expressions [Reporting Services], code"
  - "adding code"
  - "reports [Reporting Services], code"
---
# Add code to a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  In any expression, you can call your own custom code in a paginated report. You can provide code in the following two ways:  
  
-   Embed code written in [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)] directly in your report. If your code refers to a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] that isn't <xref:System.Math> or <xref:System.Convert>, you must add the reference to the report. For more information, see [Add an assembly reference to a report &#40;SSRS&#41;](../../reporting-services/report-design/add-an-assembly-reference-to-a-report-ssrs.md). For more information about other references you can make from your code, see [Custom code and assembly references in Expressions in Report Designer](../../reporting-services/report-design/custom-code-and-assembly-references-in-expressions-in-report-designer-ssrs.md).  
  
-   Provide a custom code assembly by using the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)]. If you provide a custom assembly, you must install it on both the computer where you author the report and the report server where you view the report. For more information, see [Use custom assemblies with reports](../../reporting-services/custom-assemblies/using-custom-assemblies-with-reports.md).  
  
### Add embedded code to a report  
  
1.  In **Design** view, right-click the design surface outside the border of the report and select **Report Properties**.  
  
1.  Select **Code**.  
  
1.  In **Custom code**, enter the code. Errors in the code produce warnings when the report runs. The following example creates a custom function named `ChangeWord` that replaces the word `Bike` with `Bicycle`.  
  
    ```  
    Public Function ChangeWord(ByVal s As String) As String  
       Dim strBuilder As New System.Text.StringBuilder(s)  
       If s.Contains("Bike") Then  
          strBuilder.Replace("Bike", "Bicycle")  
          Return strBuilder.ToString()  
          Else : Return s  
       End If  
    End Function  
    ```  
  
1.  The following example shows how to pass a dataset field named Category to this function in an expression:  
  
    ```  
    =Code.ChangeWord(Fields!Category.Value)  
    ```  
  
     If you add this expression to a table cell that displays category values, whenever the word `Bike` is in the dataset field for that row, the table cell value displays the word `Bicycle` instead.  
  
## Related content 
 [Report properties dialog, code](./expressions-report-builder-and-ssrs.md)   
 [Expression examples &#40;Report Builder&#41;](../../reporting-services/report-design/expression-examples-report-builder-and-ssrs.md)   
 [Parameters collection references &#40;Report Builder&#41;](../../reporting-services/report-design/built-in-collections-parameters-collection-references-report-builder.md)  
  
