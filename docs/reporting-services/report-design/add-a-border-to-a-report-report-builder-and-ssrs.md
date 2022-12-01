---
title: "Add a border to a paginated report | Microsoft Docs"
description: Define areas of a paginated report by adding borders to the headers, footers, and report body in Report Builder. 
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: report-design


ms.topic: conceptual
ms.assetid: 81412f94-2991-4e58-bc05-5ccc0cbf2a75
author: maggiesMSFT
ms.author: maggies
---
# Add a border to a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

You can add a border to a paginated report by adding borders to the headers, footers, and report body themselves, without adding lines or rectangles.
    
 If you add a report border that appears on the page header and footer, do not suppress the header and footer on the first and last pages of the report. If you do, the border might appear cut off at the top or bottom of the first and last pages of the report. For more information, see [Page Headers and Footers &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/page-headers-and-footers-report-builder-and-ssrs.md).    
    
## To add a border to a report    
    
1.  Right-click in the header outside any items in the header, and click **Header Properties**. On the **Border** tab, add a left, top, and right border with the style you want.    
    
    > [!NOTE]    
    >  If your report doesn't have headers, you can place borders around just the report body, or you can add headers from the **Insert** tab.    
    
2.  Right-click in the body outside any items on the design surface, and click **Body Properties**. On the **Border** tab, add a left and right border with the style you want.    
    
3.  Right-click in the footer outside any items in the footer, and click **Footer Properties**. On the **Border** tab, add a left, bottom, and right border with the style you want.    
    
## See Also    
 [Rectangles and Lines &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/rectangles-and-lines-report-builder-and-ssrs.md)    
    
  
