---
title: "Drillthrough, drilldown, subreports, and nested data regions in a paginated report | Microsoft Docs"
description: Organize data in a paginated report to show the relationship of the general to the detailed and then display the data in a subreport or a separate drillthrough report.
ms.date: 03/07/2017
ms.service: reporting-services
ms.subservice: report-design


ms.topic: conceptual
ms.assetid: 4791a157-b028-4698-905d-f1dd0887aa0d
author: maggiesMSFT
ms.author: maggies
---
# Drillthrough, drilldown, subreports, and nested data regions in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-not-pbi-rb](../../includes/ssrs-appliesto-not-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  You can organize data in a paginated report in a variety of ways to show the relationship of the general to the detailed.  You can put all the data in the report, but set it to be hidden until a user clicks to reveal details; this is a *drilldown* action. You can display the data in a data region, such as a table or chart, which is *nested* inside another data region, such as a table or matrix. You can display the data in a *subreport* that is completely contained within a main report. Or, you can put the detail data in *drillthrough* reports, separate reports that are displayed when a user clicks a link.  
  
 ![rs_DrillThruDrilldownEtc](../../reporting-services/report-design/media/rs-drillthrudrilldownetc.gif "rs_DrillThruDrilldownEtc")  
  
 A. Drillthrough report  
  
 B. Subreport  
  
 C. Nested data regions  
  
 D. Drilldown action  
  
 All of these have commonalities, but they serve different purposes and have different features. Two of them, drillthrough reports and subreports, are actually separate reports. Nesting is a means of putting one data region inside another data region. Drilldown is an action you can apply to any report item to hide and show other report items. They all are ways that you can organize and display data to help your users understand your report better.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
##  <a name="SummaryCharacteristics"></a> Summary of Characteristics  
 This table summarizes these different traits. Details are in separate sections later in this topic. Drilldown isn't included in these comparisons because you can apply its showing and hiding action to any report item.  
  
|Trait|Subreport|Drillthrough|Nested|  
|-----------|---------------|------------------|------------|  
|Uses dataset of main report|Same or different|Same or different|Same|  
|Retrieves data|Data retrieved at the same time as main report|Data retrieved one drillthrough report at a time|Data retrieved all at the same time as main report|  
|Is processed and rendered|With the main report|When link is clicked|With the main report.|  
|Performs|Slower (but retrieves all data with main report)|Faster (but does not retrieve all data with main report)|Faster (and retrieves all data with main report)|  
|Uses parameters|Yes|Yes|No|  
|Can be reused|As report, or subreport or drillthrough report in other reports|As report, or subreport or drillthrough report in other reports|Cannot be reused.|  
|Is located|External to main report, same or different report server|External to main report, same report server|Internal to main report|  
|Is displayed|In the main report|In a different report|In the main report|  
  
  
##  <a name="Details"></a> Details of Characteristics  
  
###  <a name="Datasets"></a> Datasets They Use  
 Subreports and drillthrough reports can use the same dataset at the main report, or they can use a different one. Nested data regions use the same dataset.  
  
###  <a name="RetrieveData"></a> Retrieving Data  
 Subreports and nested data regions retrieve data at the same time as the main report. Drillthrough reports do not. Each drillthrough report retrieves data when a user clicks each link. This is significant if the data for the main report and the subordinate report must be retrieved at the same time.  
  
###  <a name="ProcessRender"></a> Processing and Rendering  
 A subreport is processed as part of the main report. For example, if a subreport that displays order detail information is added to a table cell in the detail row, the subreport is processed once per row of the table and rendered as part of the main report. A drillthrough report is only processed and rendered when the user clicks the drillthrough link in the summary main report.  
  
###  <a name="Performance"></a> Performance  
 When deciding which to use, consider using a data region instead a subreport, particularly if the subreport is not used by multiple reports. Because the report server processes each instance of a subreport as a separate report, performance can be impacted. Data regions provide much of the same functionality and flexibility as subreports, but with better performance. Drillthrough reports have better performance than subreports, too, because they don't retrieve all the data at the same time as the main report.  
  
###  <a name="Parameters"></a> Use of Parameters  
 Drillthrough reports and subreports typically have report parameters that specify which report data to display. For example, when you click a sales order number in a main report, a drillthrough report opens, which accepts the sales order number as a parameter, and then displays all the data for that sales order. When you create the link in the main report, you specify values to pass as parameters to the drillthrough report.  
  
 To create a drillthrough report or subreport, you must design the target drillthrough report or subreport first and then create a drillthrough action or add the reference to the main report.  
  
###  <a name="Reusability"></a> Reusability  
 Subreports and drillthrough reports are separate reports. Thus, they can be used in a number of reports, or displayed as standalone reports. Nested data regions are not reusable. You cannot save them as report parts because they are nested in a data region. You can save the data region that contains them as a report part, but not the nested data region.  

[!INCLUDE [ssrs-report-parts-deprecated](../../includes/ssrs-report-parts-deprecated.md)]
  
###  <a name="Location"></a> Location  
 Subreports and drillthrough reports are both separate reports, so they're stored external to the main report. Subreports can be on the same or a different report server, but drillthrough reports must be on the same report server. Nested data regions are part of the main report.  
  
###  <a name="Display"></a> Display  
 Subreports and nested data regions are displayed in the main report. Drillthrough reports are displayed on their own.  
  
  
##  <a name="InThisSection"></a> In This Section  
 [Drillthrough Reports &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/drillthrough-reports-report-builder-and-ssrs.md)  
 Explains reports that open when a user clicks a link in a main report.  
  
 [Subreports &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/subreports-report-builder-and-ssrs.md)  
 Explains these reports that are displayed inside the body of a main report.  
  
 [Nested Data Regions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/nested-data-regions-report-builder-and-ssrs.md)  
 Explains nesting one data region inside another, such as a chart nested inside a matrix.  
  
 [Drilldown Action &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/drilldown-action-report-builder-and-ssrs.md)  
 Explains using the drilldown action to hide and show report items.  
  
 [Specifying Paths to External Items &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/specifying-paths-to-external-items-report-builder-and-ssrs.md)  
 Explains how to refer to items that are external to the report definition file.  
  
## See Also  
 [Report Parameters &#40;Report Builder and Report Designer&#41;](../../reporting-services/report-design/report-parameters-report-builder-and-report-designer.md)  
  
  
