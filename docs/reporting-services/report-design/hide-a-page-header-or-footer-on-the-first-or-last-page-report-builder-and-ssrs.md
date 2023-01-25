---
title: "Hide page header or footer on first or last page of a paginated report | Microsoft Docs"
description: Do you want a header or footer on the first or last page of your report? If not, find out how to turn off display of the header or footer in Report Builder.
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: report-design


ms.topic: conceptual
ms.assetid: f87ce79b-00d7-4458-a17e-e253a20f720d
author: maggiesMSFT
ms.author: maggies
---
# Hide page header or footer on first or last page of a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  A paginated report can contain a page header and page footer that run along the top and bottom of each page, respectively. After you a add a header or footer, you can selectively hide it on the first and last pages of a report.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To hide a page header on the first or last page  
  
1.  Open a report in Design view.  
  
2.  Right-click the page header, and then click **Header Properties**. The **Report Header Properties** dialog box opens.  
  
3.  Verify that **Display header for this report** is not selected.  
  
4.  In the **Print options** section, clear the check box for each option to hide the display on the first or last page of the report.  
  
5.  Select **OK**.
  
### To hide a page footer on the first or last page  
  
1.  Open a report in Design view.  
  
2.  Right-click the page footer, and then click **Footer Properties**. The **Report Footer Properties** dialog box opens.  
  
3.  Verify that **Display footer for this report** is not selected.  
  
4.  In the **Print options** section, clear the check box for each option to hide the display on the first or last page of the report.  
  
5.  Select **OK**.
  
## See Also  
 [Page Headers and Footers &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/page-headers-and-footers-report-builder-and-ssrs.md)   
 [Pagination in Reporting Services &#40;Report Builder  and SSRS&#41;](../../reporting-services/report-design/pagination-in-reporting-services-report-builder-and-ssrs.md)   
 [Add or Remove a Page Header or Footer &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/add-or-remove-a-page-header-or-footer-report-builder-and-ssrs.md)  
  
  
