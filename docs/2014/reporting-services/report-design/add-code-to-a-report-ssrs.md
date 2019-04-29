---
title: "Add Code to a Report (SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
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
manager: kfile
---
# Add Code to a Report (SSRS)
  In any expression, you can call your own custom code. You can provide code in the following two ways:  
  
-   Embed code written in [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] directly in your report. If your code refers to a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] that is not <xref:System.Math> or <xref:System.Convert>, you must add the reference to the report. For more information, see [Add an Assembly Reference to a Report &#40;SSRS&#41;](add-an-assembly-reference-to-a-report-ssrs.md). For more information about other references you can make from your code, see [Custom Code and Assembly References in Expressions in Report Designer &#40;SSRS&#41;](custom-code-and-assembly-references-in-expressions-in-report-designer-ssrs.md).  
  
-   Provide a custom code assembly by using the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)]. If you provide a custom assembly, you must install it on both the computer where you author the report and the report server where you view the report. For more information, see [Using Custom Assemblies with Reports](../custom-assemblies/using-custom-assemblies-with-reports.md).  
  
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
 [Report Properties Dialog Box, Code](../report-properties-dialog-box-code.md)   
 [Expression Examples &#40;Report Builder and SSRS&#41;](expression-examples-report-builder-and-ssrs.md)   
 [Parameters Collection References &#40;Report Builder and SSRS&#41;](built-in-collections-parameters-collection-references-report-builder.md)  
  
  
