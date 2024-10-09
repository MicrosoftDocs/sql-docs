---
title: "Set the locale for a paginated report or text box"
description: Use the Language property on a text box to provide the locale setting for formats in a paginated report that display data that differ by language and region in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "locales [Reporting Services]"
---
# Set the locale for a paginated report or text box (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  The **Language** property on a paginated report or a text box contains the locale setting, which determines the default formats for displaying report data that differ by language and region, for example, date, currency, or number values. The **Language** property on a text box overrides the **Language** property on the report. If no value is specified for **Language**, Reporting Services uses the locale of the operating system on the report server for published reports or of the report authoring computer for report preview.  
  
 For HTML reports, you can override the default **Language** value and use the language specified by the HTTP header of the browser client by using the built-in field User!Language in an expression for the **Language** property of a report or a text box.  
  
 You can also specify the **Language** property for a report in a URL. For more information, see [Set the Language for Report Parameters in a URL](../../reporting-services/set-the-language-for-report-parameters-in-a-url.md).  
  
### To set the locale for a report  
  
1.  In Design view, click outside the report design surface to select the report.  
  
2.  In the Properties pane, for the **Language** property, type or select the language that you want to use for the report.  
  
### To set the locale for a text box  
  
1.  In Design view, select the text box to which you want to apply the locale settings.  
  
2.  In the Properties pane, do the following:  
  
    -   For the **Calendar** property, type or select the calendar that you want to use for dates.  
  
    -   For the **Direction** property, type or select the direction in which the text is written.  
  
    -   For the **Language** property, type or select the language that you want to use for the text box. This value overrides the **Language** property for the Report.  
  
    -   For the **NumeralLanguage** property, type or select the format to use for numbers in the text box.  
  
    -   For the **NumeralVariant** property, type or select the variant of the format to use for numbers in the text box.  
  
    -   For the **UnicodeBiDi** property, select the level of bidirectional embedding to use in the text box.  
  
## Related content

- [Expression Uses in Reports &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expression-uses-in-reports-report-builder-and-ssrs.md)
- [Solution Design Considerations for Multi-Lingual or Global Deployments (Reporting Services)](/previous-versions/sql/)
