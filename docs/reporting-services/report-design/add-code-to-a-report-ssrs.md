---
title: "Add Code to a Report (SSRS) | Microsoft Docs"
ms.date: 03/14/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
helpviewer_keywords: 
  - "code [Reporting Services]"
  - "custom code [Reporting Services]"
  - "expressions [Reporting Services], code"
  - "adding code"
  - "reports [Reporting Services], code"
ms.assetid: 00ef8fc6-99fe-49b2-8a22-7eb475881dc4
author: maggiesMSFT
ms.author: maggies
---
# Add Code to a Report (SSRS)
  In any expression, you can call your own custom code. You can provide code in the following two ways:  
  
-   Embed code written in [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] directly in your report. If your code refers to a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] that is not <xref:System.Math> or <xref:System.Convert>, you must add the reference to the report. For more information, see [Add an Assembly Reference to a Report &#40;SSRS&#41;](../../reporting-services/report-design/add-an-assembly-reference-to-a-report-ssrs.md). For more information about other references you can make from your code, see [Custom Code and Assembly References in Expressions in Report Designer &#40;SSRS&#41;](../../reporting-services/report-design/custom-code-and-assembly-references-in-expressions-in-report-designer-ssrs.md).  
  
-   Provide a custom code assembly by using the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)]. If you provide a custom assembly, you must install it on both the computer where you author the report and the report server where you view the report. For more information, see [Using Custom Assemblies with Reports](../../reporting-services/custom-assemblies/using-custom-assemblies-with-reports.md).  
  
### To add embedded code to a report  
  
1.  In **Design** view, right-click the design surface outside the border of the report and click **Report Properties**.  
  
2.  Click **Code**.  
  
3.  In **Custom code**, type the code. Errors in the code produce warnings when the report runs. The following example creates a custom function named `ChangeWord` that replaces the word "`Bike`" with "`Bicycle`".  
  
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
  
4.  The following example shows how to pass a dataset field named Category to this function in an expression:  
  
    ```  
    =Code.ChangeWord(Fields!Category.Value)  
    ```  
  
     If you add this expression to a table cell that displays category values, whenever the word "Bike" is in the dataset field for that row, the table cell value displays the word "Bicycle" instead.  
  
## See Also  
 [Report Properties Dialog Box, Code](https://msdn.microsoft.com/library/955d4b11-17b4-4f1c-9690-6e7af54caea7)   
 [Expression Examples &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expression-examples-report-builder-and-ssrs.md)   
 [Parameters Collection References &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/built-in-collections-parameters-collection-references-report-builder.md)  
  
  
