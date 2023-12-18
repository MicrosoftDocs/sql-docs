---
title: "HTML Viewer and the report toolbar"
description: Learn about the HTML Viewer and the report toolbar and how you can view reports on demand as they're requested from the report server.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/14/2017
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "HTML Viewer [Reporting Services]"
  - "report toolbar [Reporting Services]"
---
# HTML Viewer and the report toolbar
  [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] provides an HTML Viewer that is used to display reports on demand as they're requested from the report server. HTML Viewer provides a framework for viewing reports in HTML. It includes a report toolbar, a parameter section, a credentials section, and a document map. The report toolbar in HTML Viewer includes features you can use to work with your report, including export options so that you can view your report in formats other than HTML. The parameter section and document map appear only when you open reports that are configured to use parameters and a document map control.  
  
 Although you can't modify the report toolbar, you can configure parameters on a report URL to hide it on a report. For more information about hiding the report toolbar, see [URL access parameter reference](../reporting-services/url-access-parameter-reference.md).  
  
## Report toolbar  
 The report toolbar provides page navigation, zoom, refresh, search, export, print, and data feed functionality for reports that are rendered in the HTML rendering extension.  
  
 Print functionality is optional. When it's available, a Printer icon appears on the report toolbar. On first use, selecting the Printer icon downloads an ActiveX control that you must install. Once the control is installed, selecting the Printer icon opens a Print dialog box so that you can select from the printers that are configured for your computer. Server settings and browser settings determine print availability. For more information, see [Print reports from a browser with the Print Control &#40;Report Builder and SSRS&#41;](../reporting-services/report-builder/print-reports-from-a-browser-with-the-print-control-report-builder-and-ssrs.md) and [Enable and disable client-side printing for Reporting Services](../reporting-services/report-server/enable-and-disable-client-side-printing-for-reporting-services.md).  
  
 The report toolbar is similar to the one shown in the following illustration. The report toolbar that you see might differ from the illustration based on report features or the rendering options that are available.  
  
 :::image type="content" source="../reporting-services/media/ssrs-htmlviewer-toolbar.png" alt-text="Screenshot of the Reporting Services report toolbar.":::
  
 The following table describes commonly used features of the report toolbar. The control that you use to access each feature identifies it.  
  
|Icon or control|Name|Description|  
|------------------------------|-|--------|  
|:::image type="icon" source="../reporting-services/media/htmlviewer-pagenav.gif":::|**Page navigation controls**|Open the first or last page of a report, scroll through a report page by page, and open a specific page in a report. To view a specific page, type the page number and press ENTER.|  
|:::image type="icon" source="../reporting-services/media/htmlviewer-pagesize.gif":::|**Page display controls**|Enlarge or reduce the size of the report page. In addition to percentage-based changes, you can select **Page Width** to fit the horizontal length of a report page in the browser window, or **Whole Page** to fit the vertical length of a report in the browser window. A **Zoom** option is supported by [!INCLUDE[msCoName](../includes/msconame-md.md)] Internet Explorer 5.5 and later.|  
|:::image type="icon" source="../reporting-services/media/htmlviewer-search.gif":::|**Search field**|Search for content in the report by typing a word or phrase that you want to find (the maximum value length is 256 characters). The search is case-insensitive and starts at the page or section that is currently selected. Only visible content is included in a search operation. To search for subsequent occurrences of the same value, select **Next**.|  
|:::image type="icon" source="../reporting-services/media/htmlviewer-export.gif":::|**Export formats**|Open a new browser window and render the report in the selected format. The rendering extensions that are installed on the report server determine the available formats. TIFF is recommended for printing. Select **Export** to view the report in the selected format.|  
|:::image type="icon" source="../reporting-services/media/htmlviewer-docmap.gif":::|**Document map icon**|Show or hide the document map pane in a report that includes a document map. A document map is a report navigation control similar to the navigation pane on a Web site. You can select items in the document map to navigate to a specific group, page, or subreport.|  
|:::image type="icon" source="../reporting-services/media/printer-icon.gif":::|**Printer icon**|Open a Print dialog box so that you can specify print options and print a report. On first use, selecting this icon prompts you to download the print control.|  
||**Show and hide icons**|Show or hide parameter value fields and the **View Report** button in a report that includes parameters.|  
|:::image type="icon" source="../reporting-services/media/htmlviewer-refresh.gif":::|**Report refresh icon**|Refresh the report. Data for live reports refreshes. Cached reports reload from where they're stored.|  
|:::image type="icon" source="../reporting-services/media/htmlviewer-datafeed.gif":::|**Data feed icon**|Generated data feeds from reports.|  
|:::image type="icon" source="../reporting-services/media/ssrs-powerbi-button-reportwviewer.png":::|**Pin to Power BI Dashboard**|Pin support report items to a [!INCLUDE[sspowerbi](../includes/sspowerbi-md.md)]. If the button isn't visible, the report server isn't integrated with [!INCLUDE[sspowerbi](../includes/sspowerbi-md.md)]. For more information, see [Power BI Report Server integration &#40;Configuration Manager&#41;](../reporting-services/install-windows/power-bi-report-server-integration-configuration-manager.md).|  
  
### About export formats  
 From the report toolbar, you can select to view your report in various formats. The rendering extensions that are installed on the report server determine the available formats. When you select another format, a second browser window is used to display the report, using a viewer associated with the export format you selected. If a viewer isn't available for the format you select, you can select a different format.  
  
 The following export formats are included in a default report server installation. The list of export formats available to you might vary from the formats listed here.  
  
|Export format|Description|  
|-------------------|-----------------|  
|XML|View a report in XML syntax. Reports viewed in XML open in a new browser window.|  
|CSV|View a report in a comma-delimited format. The report opens in an application that is associated with the CSV file type.|  
|PDF|View a report using a client-side PDF viewer. You must have a PDF viewer (for example, Adobe Acrobat Reader) to use this format.|  
|MHTML|View the report in an MIME-encoded HTML format that keeps images and linked content together with a report.|  
|Excel|View the report in [!INCLUDE[msCoName](../includes/msconame-md.md)] Excel, an .xlsx file.|  
|PowerPoint|View the report in [!INCLUDE[msCoName](../includes/msconame-md.md)] PowerPoint, a .pptx file.|  
|TIFF file|View the report in the default TIFF viewer. For some [!INCLUDE[msCoName](../includes/msconame-md.md)] Windows clients, this viewer is the Windows Picture and Fax Viewer. Select this format to a view a report in a page-oriented layout.|  
|Word|View the report in [!INCLUDE[msCoName](../includes/msconame-md.md)] Word, a .docx file.|  
  
## Parameters  
 Parameters are values that are used to select specific data. Specifically, they're used to complete a query that selects the data for your report, or to filter the result set. Parameters that are commonly used in reports include dates, names, and IDs. When you specify a value for a parameter, the report contains only the data that matches the value; for example, employee data based on an Employee ID parameter. Parameters correspond to fields on the report. After you specify a parameter, select **View Report** to get the data.  
  
 The report author defines the parameter values that are valid for each report. A report administrator can also set parameter values. To find out which parameter values are valid for your report, ask your report designer or administrator.  
  
## Credentials  
 Credentials are user name and password values that grant access to a data source. After you specify your credentials, select **View Report** to get the data. If a report requires you to sign in, the data that you're authorized to see might differ from the data that another user sees. So, two users can run the same report and get different results. In addition, some reports contain hidden areas that are revealed based on user sign-in credentials or selections made in the report itself. Hidden areas in the report are excluded from search operations, producing different search results than when all parts of the report are visible.  
  
## Related content

- [Specify credential and connection information for report data sources](../reporting-services/report-data/specify-credential-and-connection-information-for-report-data-sources.md)   
- [Find, view, and manage reports &#40;Report Builder and SSRS &#41;](../reporting-services/report-builder/finding-viewing-and-managing-reports-report-builder-and-ssrs.md)   
- [Export reports &#40;Report Builder and SSRS&#41;](../reporting-services/report-builder/export-reports-report-builder-and-ssrs.md)  
  
  
