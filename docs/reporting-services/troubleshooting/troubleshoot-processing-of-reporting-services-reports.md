---
title: "Troubleshoot Processing of Reporting Services Reports | Microsoft Docs"
ms.date: 08/26/2016
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: troubleshooting


ms.topic: conceptual
ms.assetid: bb309231-68be-4d68-a44c-c098999c67a2
author: markingmyname
ms.author: maghan
---
# Troubleshoot Processing of Reporting Services Reports
After the report data is retrieved, the report processor combines the data and layout information. Each report item property that has an expression is evaluated in the context of the combined data and layout. Use this topic to help troubleshoot these issues.   
  
## My report definition is not valid.  
At run time, the report processor combines data and layout elements in the report definition, and evaluates expressions for report item properties.   
  
The report processor checks that the report definition (.rdl file) conforms to the schema that is specified in the namespace declaration at the beginning of the .rdl file. For more information about RDL schemas, see [Find the Report Definition Schema Version (SSRS)](../../reporting-services/reports/find-the-report-definition-schema-version-ssrs.md).  
  
In addition, the report expressions that are evaluated at run time must follow a set of rules that ensure the report data and layout can be combined correctly. When the report processor detects a problem, you might see the following message: The definition of the report `<report name>` is invalid.  
  
### Report item expressions can only refer to fields within the current dataset scope or, if inside an aggregate, the specified dataset scope.  
  
Use the following list to help determine the cause of the error:  
* When a report has more than one dataset, an aggregate expression in a text box on the report body must specify a scope parameter. For example, `=First(Fields!FieldName.Value, "DataSet1")`.  
  
To specify a scope parameter, provide the name of a dataset, data region, or group that is in scope for the report item. For more information, see [Understanding Expression Scope for Totals, Aggregates, and Built-in Collections (Report Builder 3.0 and SSRS)](../../reporting-services/report-design/expression-scope-for-totals-aggregates-and-built-in-collections.md) and [Expression Reference (Report Builder 3.0 and SSRS)](../../reporting-services/report-design/expression-reference-report-builder-and-ssrs.md).  
  
### Names of objects must be greater than 0 and less than or equal to 256 characters.  
The length of object identifiers in a report definition is restricted to 256 characters. Identifiers must be case-sensitive and CLS-compliant. Names must begin with a letter, consist of letters, numbers, or an underscore (_), and have no spaces. For example, text box names or data region names must comply with these guidelines.   
  
To change the name of an object, in the toolbar of the Properties pane, select the item in the drop-down list, scroll to **Name** and enter a valid object name.   
  
## A text box displays "#Error"; how do I fix it?  
The "#Error" message occurs when the report processor evaluates expressions in report item properties at run-time and detects a data type conversion, scope, or other error.   
  
A data type error usually means the default or the specified data type is not supported. A scope error means that the specified scope was not available at the time that the expression was evaluated.   
  
To eliminate the #Error message, you must rewrite the expression that causes it. To determine more details about the issue, view the detailed error message.   
  
In preview, in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull.md)], view the Output window. On the report server, view the call stack. 
  
  
## See Also  
[Errors and events (Reporting Services)](../../reporting-services/troubleshooting/errors-and-events-reference-reporting-services.md)  
[!INCLUDE[feedback_stackoverflow_msdn_connect](../../includes/feedback-stackoverflow-msdn-connect-md.md)]

