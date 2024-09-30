---
title: "Allow a text box to grow or shrink in a paginated report"
description: Find out how to set property options in Report Builder paginated reports that let a text box expand or shrink based on its contents.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Allow a text box to grow or shrink in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  In a paginated report, text boxes aren't just the stand-alone boxes on the report design surface. Every cell in a table or matrix (a tablix data region) also contains a text box, which can be formatted in the same way as stand-alone text boxes. By default, text boxes are a fixed size. You can set options that let a text box expand or shrink based on its contents. These options correspond to the **CanGrow** or **CanShrink** properties in the **Properties** pane.  
  
## Allow a text box to grow or shrink  
  
1.  Right-click the text box, and select **Text Box Properties**.  
  
1.  Select the **General** tab.  
  
    -   To allow the text box to expand vertically based on its contents, select **Allow height to increase**.  
  
    -   To allow the text box to shrink based on its contents, select **Allow height to decrease**.  
  
## Related content

- [Text boxes &#40;Report Builder&#41;](../../reporting-services/report-design/text-boxes-report-builder-and-ssrs.md)
