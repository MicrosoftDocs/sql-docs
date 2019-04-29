---
title: "Page Headers and Footers (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
f1_keywords: 
  - "sql12.rtp.rptdesigner.pagefooter.fill.f1"
  - "10125"
  - "10121"
  - "10120"
  - "10122"
  - "10123"
  - "sql12.rtp.rptdesigner.pageheader.border.f1"
  - "sql12.rtp.rptdesigner.pageheader.general.f1"
  - "sql12.rtp.rptdesigner.pagefooter.border.f1"
  - "sql12.rtp.rptdesigner.pageheader.fill.f1"
  - "sql12.rtp.rptdesigner.pagefooter.general.f1"
  - "10124"
ms.assetid: 4fb9faac-511e-404a-b8d7-1f2e3cb47b11
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Page Headers and Footers (Report Builder and SSRS)
  A report can contain a header and footer that run along the top and bottom of each page, respectively. Headers and footers can contain static text, images, lines, rectangles, borders, background color, background images, and expressions. Expressions include dataset field references for reports with exactly one dataset and aggregate function calls that include the dataset as a scope.  
  
> [!NOTE]  
>  Each rendering extension processes pages differently. For more information about report pagination and rendering extensions, see [Pagination in Reporting Services &#40;Report Builder  and SSRS&#41;](pagination-in-reporting-services-report-builder-and-ssrs.md).  
  
 By default, reports have page footers, but not page headers. For more information about how to add or remove them, see [Add or Remove a Page Header or Footer &#40;Report Builder and SSRS&#41;](add-or-remove-a-page-header-or-footer-report-builder-and-ssrs.md).  
  
 Headers and footers commonly contain page numbers, report titles, and other report properties. For more information about how to add these items to your report header or footer, see [Display Page Numbers or Other Report Properties &#40;Report Builder and SSRS&#41;](display-page-numbers-or-other-report-properties-report-builder-and-ssrs.md).  
  
 After you create a page header or footer, it is displayed on each report page. For more information about how to suppress page headers and footers on the first and last pages, see [Hide a Page Header or Footer on the First or Last Page &#40;Report Builder and SSRS&#41;](hide-a-page-header-or-footer-on-the-first-or-last-page-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Report Headers and Footers  
 Page headers and footers are not the same as report headers and footers. Reports do not have a special report header or report footer area. A report header consists of the report items that are placed at the top of the report body on the report design surface. They appear only once as the first content in the report. A report footer consists of report items that are placed at the bottom of the report body. They appear only once as the last content in the report.  
  
## Displaying Variable Data in a Page Header or Footer  
 Page headers and footers can contain static content, but they are more commonly used to display varying content like page numbers or information about the contents of a page. To display variable data that is different on each page, you must use an expression.  
  
 If there is only one dataset defined in the report, you can add simple expressions such as `[FieldName]` to a page header or footer. Drag the field from the Report Data pane dataset field collection or the Built-in Fields collection to the page header or page footer. A text box with the appropriate expression is automatically added for you.  
  
 To calculate sums or other aggregates for values on the page, you can use aggregate expressions that specify ReportItems or the name of a dataset. The ReportItems collection is the collection of text boxes on each page after report rendering occurs. The dataset name must exist in the report definition. The following table displays which items are supported in each type of aggregate expression:  
  
|Supported in expression|ReportItems aggregates|Dataset aggregates (scope must be name of dataset)|  
|-----------------------------|----------------------------|----------------------------------------------------------|  
|Text boxes in body of report|Yes|No|  
|&PageNumber|Yes|No|  
|&TotalPages|Yes|No|  
|Aggregate function|Yes. For example,<br /><br /> `=First(ReportItems!TXT_LastName.Value)`|Yes. For example,<br /><br /> `=Max(Quantity.Value,"DataSet1")`|  
|Fields collection for items on the page|Indirectly. For example,<br /><br /> `=Sum(ReportItems!Textbox1.Value)`|Yes. For example,<br /><br /> `=Sum(Fields!Quantity.Value,"DataSet1")`|  
|Data-bound image|Indirectly. For example, `=ReportItems!TXT_Photo.Value`|Yes. For example,<br /><br /> `=First(Fields!Photo.Value,"DataSet1")`|  
  
 The following sections in this topic show ready-to-use expressions that get variable data commonly used in headers and footers. There is also a section on how the Excel rendering extension processes headers and footers. For more information about expressions, see [Expressions &#40;Report Builder and SSRS&#41;](expressions-report-builder-and-ssrs.md).  
  
### Adding Calculated Page Totals to a Header or Footer  
 For some reports, it is useful to include a calculated value in the header or footer of each report; for example, a per-page sum total if the page includes numeric values. Because you cannot reference the fields directly, the expression that you put in the header or footer must reference the name of the report item (for example, a text box) rather than the data field:  
  
 `=Sum(ReportItems!Textbox1.Value)`  
  
 If the text box is in a table or list that contains repeated rows of data, the value that appears in the header or footer at run time is a sum of all values of all `TextBox1` instance data in the table or list for the current page.  
  
 When calculating page totals, you can expect to see differences in the totals when you use different rendering extensions to view the report. Paginated output is calculated differently for each rendering extension. The same page that you view in HTML might show different totals when viewed in PDF if the amount of data on the PDF page is different. For more information, see [Rendering Behaviors &#40;Report Builder  and SSRS&#41;](rendering-behaviors-report-builder-and-ssrs.md).  
  
### For Reports with Multiple Datasets  
 For reports with more than one dataset, you cannot add fields or data-bound images directly to a header or footer. However, you can write an expression that indirectly references a field or data-bound image that you want to use in a header or footer.  
  
 To put variable data in a header or footer:  
  
-   Add a text box to the header or footer.  
  
-   In the text box, write an expression that produces the variable data that you want to appear.  
  
-   In the expression, include references to report items on the page; for example, you can reference a text box that contains data from a particular field. Do not include a direct reference to fields in a dataset. For example, you cannot use the expression `[LastName]`. You can use the following expression to display the contents of the first instance of a text box named `TXT_LastName`:  
  
     `=First(ReportItems!TXT_LastName.Value)`  
  
 You cannot use aggregate functions on fields in the page header or footer. You can only use an aggregate function on report items in the report body. For common expressions in page headers and footers, see [Expression Examples &#40;Report Builder and SSRS&#41;](expression-examples-report-builder-and-ssrs.md).  
  
#### Adding a Data-Bound Image to a Header or Footer  
 You can use image data stored in a database in a header or footer. However, you cannot reference database fields from the Image report item directly. Instead, you must add a text box in the body of the report and then set the text box to the data field that contains the image (note that the value must be base64 encoded). You can hide the text box in the body of the report to avoid showing the base64-encoded image. Then, you can reference the value of the hidden text box from the Image report item in the page header or footer.  
  
 For example, suppose you have a report that consists of product information pages. In the header of each page, you want to display a photograph of the product. To print a stored image in the report header, define a hidden text box named `TXT_Photo` in the body of the report that retrieves the image from the database and use an expression to give it a value:  
  
 `=Convert.ToBase64String(Fields!Photo.Value)`  
  
 In the header, add an Image report item which uses the `TXT_Photo` text box, decoded to show the image:  
  
 `=Convert.FromBase64String(ReportItems!TXT_Photo.Value)`  
  
## Using Headers and Footers to Position Text  
 You can use headers and footers to position text on a page. For example, suppose you are creating a report that you want to mail out to customers. You can use a header or footer to position the customer address so that it appears in an envelope window when folded.  
  
 If you are only using the text box to populate a header or footer, you can hide the text box in the report body. Placement of the text box in the report body can have an effect on whether the value appears on the header or footer of the first or last page of a report. For example, if you have tables, matrices, or lists that cause the report to span multiple pages, the hidden text box value appears on the last page. If you want it to appear on the first page, place the hidden text box at the top of the report body.  
  
## Designing Reports with Page Headers and Footers for Specific Renderers  
 When a report is processed, data and layout information are combined. When you view a report, the combined information is passed to a renderer that determines how much report data fits on each report page.  
  
 If you view a report on the report server using a browser, the HTML renderer controls the content on the report pages that you see. If you plan to deliver reports in a different format than you use for viewing, or if you plan to print reports in a specific format, you may want to optimize the report layout for the renderer you plan to use for the final report format. For more information about report pagination, see [Pagination in Reporting Services &#40;Report Builder  and SSRS&#41;](pagination-in-reporting-services-report-builder-and-ssrs.md).  
  
### Working with Page Headers and Footers in Excel  
 When defining page headers and footers for reports that target the Excel rendering extension, follow these guidelines to achieve best results:  
  
-   Use page footers to display page numbers.  
  
-   Use page headers to display images, titles, or other text. Do not put page numbers in the header.  
  
 In Excel, page footers have a limited layout. If you define a report that includes complex report items in the page footer, the page footer won't process as you expect when the report is viewed in Excel.  
  
 The Excel rendering extension can accommodate images and absolute positioning of simple or complex report items in the page header. A side effect of supporting a richer page header layout is reduced support for calculating page numbers in the header. In the Excel rendering extension, default settings cause page numbers to be calculated based on the number of worksheets. Depending on how you define the report, this might produce erroneous page numbers. For example, suppose you have a report that renders as a single large worksheet that prints on four pages. If you include page number information in the header, each printed page will show "Page 1 of 1" in the header.  
  
 A more accurate page count is based on logical pages that correlate to the dimensions of a printed page. In Excel, the page footer uses logical page numbers automatically. To put the logical page count in the page header, you must configure the device information settings to use simple headers. Be aware that when you use simple headers, you remove the capability of handling complex report layout in the header region.  
  
 For more information, see [Exporting to Microsoft Excel &#40;Report Builder and SSRS&#41;](../report-builder/exporting-to-microsoft-excel-report-builder-and-ssrs.md).  
  
## See Also  
 [Embed an Image in a Report &#40;Report Builder and SSRS&#41;](embed-an-image-in-a-report-report-builder-and-ssrs.md)   
 [Rectangles and Lines &#40;Report Builder and SSRS&#41;](rectangles-and-lines-report-builder-and-ssrs.md)  
  
  
