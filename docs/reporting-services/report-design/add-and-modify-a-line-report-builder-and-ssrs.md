---
title: "Add and modify a line in a paginated report | Microsoft Docs"
description: Customize the appearance of reports by adding a graphical element to separate sections or by editing line properties to change color or style in Report Builder.
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: report-design


ms.topic: conceptual
ms.assetid: 8dc4998b-a214-49b6-96e7-fbc179015209
author: maggiesMSFT
ms.author: maggies
---
# Add and modify a line in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  You can add a line to a paginated report when you want a graphical element to separate sections of the report. You can customize the appearance of the line by editing line properties such as color or style. For example, you might want to incorporate company colors into the report.    
    
> [!NOTE]    
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]    
    
## To add a line    
    
1.  On the **Insert** tab, click **Line**.    
    
2.  On the design surface, click where you want one end of the line, and then click where you want the other end.    
    
     Note that as you move the end of the line, "snap lines" appear as the end lines up with other objects on the design surface. These help you if you want objects to be aligned.    
    
3.  To change the line properties, select the line on the design surface and then edit its properties in the **Border** section of the **Home** tab.    
    
    > [!NOTE]    
    >  If you set the line style to **Double** and the line width is 1 1/2 pt or narrower, the line may not appear double when you run the report in Report Builder, Report Designer, or a [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] web portal. It appears double when you export the report to other formats such as Microsoft Word and Acrobat PDF.    
    
## See Also    
 [Rectangles and Lines &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/rectangles-and-lines-report-builder-and-ssrs.md)    
    
  
