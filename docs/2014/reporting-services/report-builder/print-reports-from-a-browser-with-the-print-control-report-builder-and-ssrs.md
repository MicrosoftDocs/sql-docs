---
title: "Print Reports from a Browser with the Print Control (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 10054250-d915-4bcb-8a1d-26837db4e932
author: maggiesMSFT
ms.author: maggies
manager: craigg
---
# Print Reports from a Browser with the Print Control (Report Builder and SSRS)
  Although a browser is the most common client application used to view a report, browser print functionality is not ideal for printing reports. Print functionality in a browser is designed for printing Web pages. Typically, pages that you print from a browser include all of the visual elements that are on a Web page, plus header and footer information that identifies the page or Web site. Printing from a browser prints the contents of the current window. For a multipage report, the browser prints the first page at most, and possibly less if the report page extends beyond the dimensions of a printed page.  
  
 To improve the print quality of reports that you view in a browser and to print multiple pages, you can use the client-side print functionality provided in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. Client-side printing provides a standard **Print** dialog box that can be used to select a printer, specify pages and margins, and preview the report before you print. Client-side printing is intended to be used in place of the **Print** command on the browser's **File** menu. When you use client-side printing, the report is printed as it was designed, without the extra elements you see in a Web page print out.  
  
 To use client-side printing, you need to install a [!INCLUDE[msCoName](../../includes/msconame-md.md)] ActiveX control. For more information, see [Enable and Disable Client-Side Printing for Reporting Services](../report-server/enable-and-disable-client-side-printing-for-reporting-services.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Print Options  
 To configure print properties for your report, in the **Print** dialog box, click the **Properties** button. **Paper size** is determined by the default height and width of the report page size as defined in the report definition. The available values are dependent on the printer type and its capabilities. Width and height display default values as determined by the print drivers that are configured on the computer. Changing these values causes the report to print using the new dimensions. Page width and height are each determined by **Orientation**, which is set to either **Portrait** or **Landscape**. The default orientation displayed is dependent on the page width and page height of the report.  
  
> [!NOTE]  
>  The **Print** dialog box and the default printer settings for width, height, and page orientation are determined by the report definition.  
  
### Print Preview  
 To preview a report, in the **Print** dialog box, click the **Preview** button. Clicking preview opens the first page of the report in a separate preview window. Additional pages are made available as the report is rendered on the report server. A previewed report is rendered in EMF format. You can navigate to the previous or next page until the last page is reached, and the **Next** button is disabled.  
  
### Adjusting Print Margins  
 You can modify the print margins in the rendered EMF report prior to printing the report. To do this, in the **Print** dialog box, click the **Preview** button. At the top of the preview page, click the **Margins** button. The Margins dialog box is displayed. Configure the top, bottom, right, and left margins as desired. [!INCLUDE[clickOK](../../includes/clickok-md.md)] The dialog box closes and the settings are stored for rendering preview and printing.  
  
## See Also  
 [Print Reports &#40;Report Builder and SSRS&#41;](print-reports-report-builder-and-ssrs.md)   
 [Print a Report &#40;Report Builder and SSRS&#41;](print-a-report-report-builder-and-ssrs.md)  
  
  
