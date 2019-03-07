---
title: "Rendering Report Items (Report Builder and SSRS) | Microsoft Docs"
ms.date: 03/07/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: 99ebb4dc-41cc-42ac-82dd-a2b0e31155a0
author: markingmyname
ms.author: maghan
---
# Rendering Report Items (Report Builder and SSRS)
  The number, size, and location of report items affect how the renderers paginate the report body. Below is a description of how various report items are rendered.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Overlapping Report Items  
 Overlapping report items are not supported in HTML, MHTML, Word, Excel, in Preview, or the Report Viewer. If overlapping items exist, they are moved. The following rules are applied to overlapping report items:  
  
-   If the vertical overlap of report items is greater, one of the overlapping items is moved to the right. The left-most item remains where it is positioned.  
  
-   If the horizontal overlap of report items is greater, one of the overlapping items is moved down. The top-most item remains where it is positioned.  
  
-   If the vertical and horizontal overlap is equal, one of the overlapping items is moved to the right. The left-most item remains where it is positioned.  
  
-   If an item must be moved to correct overlapping, adjacent report items move down and/or to the right to maintain a minimum amount of spacing between the item and the report items that end above it and/or to the left of it. For example, suppose two report items overlap vertically and a third report item is 2 inches to the right of them. When the overlapping report item is moved to the right, the third report item moves to the right as well in order to maintain the 2 inches between itself and the report item to its left.  
  
 Overlapping report items are supported in hard page-break formats, including print.  
  
## Visibility and Report Items  
 Report items can be hidden or displayed by default, or hidden or displayed conditionally using expressions. Optionally, the visibility can be switched by clicking another report item.  
  
 The following visibility rules apply when rendering report items:  
  
-   If a report item and its contents are always hidden (it is not hidden based on an expression or its visibility cannot be switched by clicking another report item), then other report items to the right or below it do not move to fill the empty space. For example, if a rectangle and the image contained within it are hidden, the report item that starts to the right of the rectangle does not move to the left to fill what appears to be empty space. The space occupied by the rectangle is preserved.  
  
-   If a report item and its contents are hidden conditionally (it is hidden based on an expression or its visibility is switched by clicking another report item), then report items to the right or below it move to the left to fill in the space when the item is hidden.  
  
-   If the visibility of a report item and its contents can be switched by clicking another report item, then pagination changes to accommodate the report item and its contents only when it is initially displayed.  
  
## Keeping Report Items Together on a Single Page  
 Many report items within a report can be kept together on a single page implicitly or explicitly by setting the keep with group or keep together properties. Report items are always rendered on the same page if the report item does not have any logical page breaks and is smaller in size than the usable page area. If a report item does not fit completely on the page on which it would usually start, a hard page break is inserted before the report item, forcing it to the next page. For soft page-break renderers, the page grows to accommodate the report item.  
  
 When the report item is always hidden, the rules for keeping items together are ignored.  
  
 The following items are always kept together:  
  
-   Images.  
  
-   Lines.  
  
-   Charts, gauges, and maps.  
  
-   A single row in a data region that appears separately on another page, by selecting the keep with group option. This will implicitly keep together the single row with at least one instance of the group so that the row is not orphaned. You can set this option on a data region or a group.  
  
-   Header area of a data region.  
  
-   Header area of a data region and the first row of data.  
  
-   Report items that can be toggled in a tablix data region.  
  
### Priority Order  
 Due to page size limitations, conflicts can arise between the rules for keeping report items together. When conflicts occur, the following priority order is used to keep items together when rendering:  
  
-   Lines, charts, and images.  
  
-   Widow and orphan control.  
  
-   Repeated column headers and row headers.  
  
     Headers take precedence over footers. Inner repeated groups have priority over outer groups. Items where the **RepeatWith** property is set that are closer to the target data region have priority over items that are farther away from the data region.  
  
-   Small report items, such as text boxes or rectangles, with an explicit KeepTogether property set to **true**.  
  
-   Large report items, such as subreports or a non-innermost tablix member, with an explicit KeepTogether property set to **true**.  
  
-   Tablix data regions with an explicit KeepTogether property set to **true**.  
  
### Subreports  
 A subreport renders as a rectangle that contains another report that is defined in a separate report .rdl file. The subreport file must be published to a report server before it can be accessed by the parent report.  
  
 The following rules apply when rendering subreports:  
  
-   Subreports can grow to the size of the body defined in the .rdl file that defines the subreport. For example, if the RDL for the subreport states that the subreport body is 5 inches wide, then the subreport will be 5 inches wide within the parent report.  
  
-   Subreports inherit column settings from the parent report. Column settings that are defined in the original RDL are always ignored.  
  
-   Only the body of the subreport is rendered. Header and footer sections that are defined in the subreport's .rdl file are not rendered when the subreport is rendered in the parent report.  
  
-   Subreports have an explicit KeepTogether property. When it is set to **true**, all the items in the subreport are kept together on one page when possible.  
  
-   If a subreport cannot run, it is displayed in the report as a text box with an error message. The style properties applied to the subreport are applied to the text box instead.  
  
-   If the subreport is split by a page break, the **Omit border on page break** setting controls whether or not the borders on the subreport are closed or open.  
  
 For more information about subreports, see [Subreports &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/subreports-report-builder-and-ssrs.md).  
  
## See Also  
 [Pagination in Reporting Services &#40;Report Builder  and SSRS&#41;](../../reporting-services/report-design/pagination-in-reporting-services-report-builder-and-ssrs.md)   
 [Rendering Behaviors &#40;Report Builder  and SSRS&#41;](../../reporting-services/report-design/rendering-behaviors-report-builder-and-ssrs.md)   
 [Interactive Functionality for Different Report Rendering Extensions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-builder/interactive-functionality-different-report-rendering-extensions.md)   
 [Tables, Matrices, and Lists &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/tables-matrices-and-lists-report-builder-and-ssrs.md)  
  
  
