---
title: "Add a border to a paginated report"
description: Define areas of a paginated report by adding borders to the headers, footers, and report body in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Add a border to a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

You can add a border to a paginated report by adding borders to the headers, footers, and report body themselves, without adding lines or rectangles.
    
 If you add a report border that appears on the page header and footer, don't suppress the header and footer on the first and last pages of the report. If you do, the border might appear cut off at the top or bottom of the first and last pages of the report. For more information, see [Page headers and footers &#40;Report Builder&#41;](../../reporting-services/report-design/page-headers-and-footers-report-builder-and-ssrs.md).    
    
## Add a border to a report    
    
1.  Right-click in the header outside any items in the header, and select **Header Properties**. On the **Border** tab, add a left, top, and right border with the style you want.    
    
    > [!NOTE]    
    >  If your report doesn't have headers, you can place borders around just the report body, or you can add headers from the **Insert** tab.    
    
1.  Right-click in the body outside any items on the design surface, and select **Body Properties**. On the **Border** tab, add a left and right border with the style you want.    
    
1.  Right-click in the footer outside any items in the footer, and select **Footer Properties**. On the **Border** tab, add a left, bottom, and right border with the style you want.    
    
## Related content   
 [Rectangles and lines &#40;Report Builder&#41;](../../reporting-services/report-design/rectangles-and-lines-report-builder-and-ssrs.md)    
    
  
