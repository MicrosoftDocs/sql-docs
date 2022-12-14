---
title: "Built-in Globals and User references in a paginated report | Microsoft Docs"

description: Find out about the values that can be used in expressions to filter results in a paginated report in Report Builder.
ms.date: 08/17/2018
ms.service: reporting-services
ms.subservice: report-design


ms.topic: conceptual
ms.assetid: 5f5e1149-c967-454d-9a63-18ec4a33d985
author: maggiesMSFT
ms.author: maggies
---
# Built-in collections - Globals and User references in a paginated report (Report Builder)


[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  The built-in fields collection, which includes both the **Globals** and the **User** collections, represent global values provided by Reporting Services when a paginated report is processed. The **Globals** collection provides values such as the name of the report, the time when report processing began, and current page numbers for the report header or footer. The **User** collection provides the user identifier and language settings. These values can be used in expressions to filter results in a report.  

  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Using the Globals Collection  
 The **Globals** collection contains the global variables for the report. On the design surface, these variables appear prefixed by an & (ampersand), for example, `[&ReportName]`. The following table describes the members of the **Globals** collection.  
  
|**Member**|**Type**|**Description**|  
|----------------|--------------|---------------------|  
|ExecutionTime|**DateTime**|The date and time that the report began to run.|  
|PageNumber|**Integer**|The current page number relative to page breaks that reset the page number. At the beginning of report processing, the initial value is set to 1. The page number increments for each rendered page.<br /><br /> To number pages within page breaks for a rectangle, a data region, a data region group, or a map, on the PageBreak property, set the ResetPageNumber property to **True**. Not supported on tablix column hierarchy groups.<br /><br /> PageNumber can only be used in an expression in a page header or page footer.|  
|ReportFolder|**String**|The full path to the folder containing the report. This does not include the report server URL.|  
|ReportName|**String**|The name of the report as it is stored in the report server database.|  
|ReportServerUrl|**String**|The URL of the report server on which the report is being run.|  
|TotalPages|**Integer**|The total number of pages relative to page breaks that reset PageNumber. If no page breaks are set, this value is the same as OverallTotalPages.<br /><br /> TotalPages can only be used in an expression in a page header or page footer.|  
|PageName|**String**|The name of the page. At the beginning of report processing, the initial value is set from InitialPageName, a report property. As each report item is processed, this value is replaced by the corresponding value of PageName from a rectangle, a data region, a data region group, or a map. Not supported on tablix column hierarchy groups.<br /><br /> PageName can only be used in an expression in a page header or page footer.|  
|OverallPageNumber|**Integer**|The page number of the current page for the entire report. This value is not affected by ResetPageNumber.<br /><br /> OverallPageNumber can only be used in an expression in a page header or page footer.|  
|OverallTotalPages|**Integer**|The total number pages for the entire report. This value is not affected by ResetPageNumber.<br /><br /> OverallTotalPages can only be used in an expression in a page header or page footer.|  
|RenderFormat|**RenderFormat**|Information about the current rendering request.<br /><br /> For more information, see "RenderFormat" in the next section.|  
  
 Members of the **Globals** collection return a variant. If you want to use a member of this collection in an expression that requires a specific data type, you must first cast the variable. For example, to convert the execution time variant into a Date format, use `=CDate(Globals!ExecutionTime)`. For more information, see [Data Types in Expressions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/data-types-in-expressions-report-builder-and-ssrs.md).  
  
### RenderFormat  
 The following table describes the members for **RenderFormat**.  
  
|Member|Type|Description|  
|------------|----------|-----------------|  
|Name|**String**|The name of the renderer as registered in the RSReportServer configuration file.<br /><br /> Available during specific parts of the report processing/rendering cycle.|  
|IsInteractive|**Boolean**|Whether the current rendering request uses an interactive rendering format.|  
|DeviceInfo|Read-only name/value collection|Key/value pairs for deviceinfo parameters for the current rendering request.<br /><br /> String values can be specified by using either the key or an index into the collection.|  
  
### Examples  
 The following examples show how to use a reference to the **Globals** collection in an expression:  
  
-   This expression, placed in a text box in the footer of a report, provides the page number and total pages in the report:  
  
     `=Globals.PageNumber & " of " & Globals.TotalPages`  
  
-   This expression provides the name of the report and the time it was run. The time is formatted with the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] formatting string for short date:  
  
     `=Globals.ReportName & ", dated " & Format(Globals.ExecutionTime, "d")`  
  
-   This expression, placed in the **Column Visibility** dialog box for a selected column, displays the column only when the report is exported to Excel. Otherwise, the column is hidden.  
  
     `EXCELOPENXML` refers to the format of Excel that is included in Office 2007. `EXCEL` refers to the format of Excel that is included in Office 2003.  
  
     `=IIF(Globals!RenderFormat.Name = "EXCELOPENXML" OR Globals!RenderFormat.Name = "EXCEL", false, true)`  
  
## Using the User Collection  
 The **User** collection contains data about the user who is running the report. You can use this collection to filter the data that appears in a report, for example, showing only the data of the current user, or to display the UserID, for example, in a report title. On the design surface, these variables appear prefixed by an & (ampersand), for example, `[&UserID]`.  
  
 The following table describes the members of the **User** collection.  
  
|**Member**|**Type**|**Description**|  
|----------------|--------------|---------------------|  
|**Language**|**String**|The language of the user running the report. For example, `en-US`.|  
|**UserID**|**String**|The ID of the user running the report. If you are using Windows Authentication, this value is the domain account of the current user. The value is determined by the Report Builder security extension, which can use Windows Authentication or custom authentication.|  
 
### Using Locale Settings  
 You can use expressions to refer to the locale settings on a client computer through the **User.Language** value to determine how a report appears to the user. For example, you can create a report that uses a different query expression based on the locale value. The query may change to retrieve localized information from a different column depending on the language returned. You can also use an expression in the language settings of the report or report items based on this variable.  
  
> [!NOTE]  
>  While you can change the language settings of a report, you must be careful about any display issues this may cause. For example, changing the locale setting of the report can change the date format in the report, but it can also change the currency format. Unless there is a conversion process for the currency, this may cause the incorrect currency symbol to be displayed in the report. To avoid this, set the language information about the individual items that you want to change, or set the item with the currency data to a specific language.  
  
### Identifying UserID for Snapshot or History Reports  
 In some cases, reports that include the *User!UserID* variable will fail to show report data that is specific to the current user who is viewing the report.  
  
## See Also  
 [Expressions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expressions-report-builder-and-ssrs.md)   
 [Expression Dialog Box &#40;Report Builder&#41;](./expressions-report-builder-and-ssrs.md)   
 [Data Types in Expressions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/data-types-in-expressions-report-builder-and-ssrs.md)   
 [Formatting Numbers and Dates &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/formatting-numbers-and-dates-report-builder-and-ssrs.md)   
 [Expression Examples &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expression-examples-report-builder-and-ssrs.md)  
  
