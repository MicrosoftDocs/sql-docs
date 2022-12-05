---
title: "rsProcessingError - Reporting Services Error | Microsoft Docs"
description: "In this error reference page, learn about event ID 'rsProcessingError': Errors have occurred in SQL Server Reporting Services report processing."
ms.date: 03/15/2017
ms.service: reporting-services
ms.subservice: troubleshooting


ms.topic: conceptual
helpviewer_keywords: 
  - "rsProcessingError"
ms.assetid: 414ee58a-8251-4367-9a8e-10c068d17280
author: maggiesMSFT
ms.author: maggies
---
# rsProcessingError - Reporting Services Error
    
## Details  
  
|Category|Value|  
|-|-|  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|rsProcessingError|  
|Event Source|Microsoft.ReportingServices.Diagnostics.Utilities.ErrorStrings.resources|  
|Component|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|  
|Message Text|Errors have occurred in report processing.|  
  
## Explanation  
 One or more errors were encountered while publishing, processing, previewing locally, viewing from the report server, or creating a subscription for a report. This error message indicates at least one error was detected.  
  
### Possible Causes  
 Possible causes include:  
  
-   A processing error occurred on the report server.  
  
-   A processing error occurred during local report processing when previewing a report.  
  
-   A group expression evaluated to an incorrect data type.  
  
-   A filter definition specified two expressions that evaluated to data types that could not be compared.  
  
-   An expression referenced a non-existing field in the Fields collection.  
  
-   An expression included an aggregate function call with an invalid or conflicting scope.  
  
-   An expression referenced a non-existing parameter in the Report Parameters collection.  
  
-   A custom assembly or a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] assembly that was incorrectly deployed failed to load.  
  
-   A parameter that has the Nullable property set to **False** has detected a null value in the parameter.  
  
-   An expression for the Hidden property of a data region contains an error: Object reference not set to an instance of an object.  
  
-   An expression included an invalid function call or syntax error.  
  
## User Action  
  
### Finding More Information  
 Do one or more of the following actions:  
  
-   If you are viewing the report from the report server or if you are viewing the report as a subscription, look at the entire text of the error message. Additional information is provided in the expanded text.  
  
-   If you are authoring a report in Report Designer and see this error when you preview or publish the report, additional information is provided in the Error List window.  
  
-   If you are authoring a report in Report Designer Preview, look at the entire text of the error message. Additional information is provided in the expanded text.  
  
-   If you are viewing a report on the report server, and if you are running as local administrator on the report server, you can view the call stack if you right-click the page and select **View Source**. Additional information is provided in the call stack.  
  
-   If you are running as local administrator on the report server, search the log file for `ReportProcessingException`. Log entries contain more information. The report server log file is typically located at \<*drive*>:\Program Files\Microsoft SQL Server\MSRS12.MSSQLSERVER\Reporting Services\LogFiles\ReportServerService__*datetimestamp*.log. For more information, see [Reporting Services Log Files and Sources](../../reporting-services/report-server/reporting-services-log-files-and-sources.md).  
  
### Failed to Load Expression Host Assembly  
 Custom assemblies must have strong name signing and the attribute AllowPartiallyTrustedCallers set. For more information, see [Using Custom Assemblies with Reports](../../reporting-services/custom-assemblies/using-custom-assemblies-with-reports.md) and [Understanding Security Policies](../../reporting-services/extensions/secure-development/understanding-security-policies.md).  
  
### A Built-in Global Name Does Not Exist  
 Check the spelling in expressions. Built-in globals, parameters, and field names are case-sensitive. In the expression causing the error, check that the name actually exists in the report and that it is spelled correctly. For more information, see [Built-in Collections in Expressions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/built-in-collections-in-expressions-report-builder.md).  
  
### Parameter Properties and Null  
 A multivalue parameter cannot be Null. For more information, see [Report Parameters &#40;Report Builder and Report Designer&#41;](../../reporting-services/report-design/report-parameters-report-builder-and-report-designer.md).  
  
### Main Report with Subreport Could Not Be Processed  
 A report with subreports must be processed by the same version of the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report processor. When upgrading reports to the current version of the report definition schema, the main report and the subreports may or may not be updated at the same time. If the version is not compatible between a report and its subreports, the following message is displayed: "Subreport could not be processed."  
  
 You must change either the main report or the subreports so that all the reports can be processed by the same version of the report processor. For information about why a report fails to upgrade, see [Upgrade Reports](../../reporting-services/install-windows/upgrade-reports.md).  
  
### Verify Function Calls are Visual Basic and Not SQL  
 You can use SQL functions in query text on a relational database. You cannot use [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] functions in query text.  
  
 In [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], expressions can use [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] functions, System.Math or System.String functions, fully qualified [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] functions, or custom functions that you provide in custom code or a custom assembly. You cannot use SQL functions in an expression.  
  
 Verify that the function calls made in the query and in the expressions are valid.  
  
### Cannot Compare Data Types for a Filter  
 In a filter equation, the filter expression that defines what to filter on and the filter value must be the same data type in order to be compared. If you see one of the following errors, modify the field expression or the filter value so that the data types match:  
  
-   The processing of *\<report item type>* for the *\<report item name>* cannot be performed. Cannot compare data of types *\<type>* and *\<type>*. Please check the data type returned by the *\<report item name>*.  
  
-   Failed to evaluate the *\<property name>*.  
  
-   Failed to evaluate the *\<property name>*. It references a dataset field which has an error: *\<error string>*.  
  
 For more information, see [Filter, Group, and Sort Data &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/filter-group-and-sort-data-report-builder-and-ssrs.md).  
  
### Invalid or Conflicting Scope Specification in an Aggregate Function Call  
 When you include aggregate function calls to an expression in a Tablix cell, the report processor evaluates the expression in the scope of the innermost groups to which the cell belongs.  
  
 You can also pass the name of a specific scope to an aggregate function. Scope can refer to the name of a dataset, a data region, or the name of a scope higher on the data hierarchy. This applies to the following messages:  
  
-   The *\<report item type>* '*\<report item name>*' has an invalid scope "*\<scope name>*". The scope must be the current scope, or contained within the current scope.  
  
-   The *\<property name>* expression for the *\<report item type>* '*\<report item name>*' has a scope parameter that is not valid for an aggregate function. The scope parameter must be set to a string constant that is equal to either the name of a containing group, the name of a containing data region, or the name of a dataset.  
  
 For aggregate functions that calculate running totals (**Previous**, **RunningValue**, or **RowNumber**), you can specify a scope parameter that is either a row group name or a column group name, but not both. This applies to the following error message:  
  
-   **Previous**, **RunningValue** or **RowNumber** aggregate functions used in the data cells of the *\<report item type>* '*\<report item name>*' refer to grouping scopes in both the columns and rows of the *\<report item type>*. The scope parameters of all **Previous**, **RunningValue** and **RowNumber** aggregate functions within a *\<report item type>* can refer to row groupings or data column groupings, but not both.  
  
 For more information, see [Expression Scope for Totals, Aggregates, and Built-in Collections &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expression-scope-for-totals-aggregates-and-built-in-collections.md) and [Built-in Collections in Expressions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/built-in-collections-in-expressions-report-builder.md).  
  
### Default Dataset Scope for a Top Level Text Box  
 Do not use a default scope for a text box added to the report design surface when the report has more than one dataset. Use an expression that includes the name of the dataset as the scope, and an aggregate function. For example, `=First(Fields!FieldName.Value, "DataSet2")`.  
  
## See Also  
 [Expressions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expressions-report-builder-and-ssrs.md)   
 [Aggregate Functions Reference &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/report-builder-functions-aggregate-functions-reference.md)   
 [Expression Examples &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expression-examples-report-builder-and-ssrs.md)   
 [Report Datasets &#40;SSRS&#41;](../../reporting-services/report-data/report-datasets-ssrs.md)   
 [Commonly Used Filters &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/commonly-used-filters-report-builder-and-ssrs.md)   
 [Dataset Fields Collection &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/dataset-fields-collection-report-builder-and-ssrs.md)   
 [Custom Code and Assembly References in Expressions in Report Designer &#40;SSRS&#41;](../../reporting-services/report-design/custom-code-and-assembly-references-in-expressions-in-report-designer-ssrs.md)   
 [Parameters Collection References &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/built-in-collections-parameters-collection-references-report-builder.md)  
  
  
