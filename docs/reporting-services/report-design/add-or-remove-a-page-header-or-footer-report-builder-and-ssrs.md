---
title: "Add or remove a page header or footer in a paginated report"
description: Find out how you can add static text, images, lines, rectangles, and borders to paginated report page headers or footers in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Add or remove a page header or footer in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  You can add static text, images, lines, rectangles, and borders to paginated report page headers or footers. You can place expressions and data-bound images in a textbox if you want variable or computed data in a header or footer.  
  
> [!NOTE]  
>  Each rendering extension processes page headers and footers differently. For more information, see [Page headers and footers &#40;Report Builder&#41;](../../reporting-services/report-design/page-headers-and-footers-report-builder-and-ssrs.md) and [Pagination in Reporting Services &#40;Report Builder&#41;](../../reporting-services/report-design/pagination-in-reporting-services-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### Add a page header or footer  
  
1.  Open a report.  
  
1.  On the design surface, right-click the report, point to **Insert**, and then select **Header** or **Footer**.  
  
> [!NOTE]  
>  The **Header** and **Footer** options appear only when a header or footer is not already part of the report.  
  
### Configure a page header or footer  
  
1.  On the design surface, right-click the page header or footer.  
  
1.  Point to **Insert**, and then select one of the following items to add it to the header or footer area:  
  
    -   **Textbox**  
  
    -   **Line**  
  
    -   **Rectangle**  
  
    -   **Image**  
  
1.  Right-click the page header, and then select **Header Properties** to add borders, background images, or colors, or to adjust the width of the header. Then, select **OK**.  
  
1.  Right-click the page footer, and then select **Footer Properties** to add borders, background images, or colors, or to adjust the width of the footer. Then, select **OK**.  
  
### Remove a page header or footer  
  
1.  Open a report.  
  
1.  On the design surface, right-click the page header or footer, and then select **Delete**.  
  
> [!NOTE]  
>  When you remove a page header or footer, you delete it from the report. Any items that you previously added to the page header or footer don't reappear if you subsequently add the header or footer again.  
  
## Related content

- [Page headers and rooters &#40;Report Builder&#41;](../../reporting-services/report-design/page-headers-and-footers-report-builder-and-ssrs.md)
