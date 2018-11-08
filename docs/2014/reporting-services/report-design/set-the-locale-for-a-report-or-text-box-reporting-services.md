---
title: "Set the Locale for a Report or Text Box (Reporting Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "locales [Reporting Services]"
ms.assetid: df115b01-184b-47f0-b5ec-0ad965ff9bee
author: maggiesMSFT
ms.author: maggies
manager: craigg
---
# Set the Locale for a Report or Text Box (Reporting Services)
  The **Language** property on a report or a text box contains the locale setting, which determines the default formats for displaying report data that differ by language and region, for example, date, currency, or number values. The **Language** property on a text box overrides the **Language** property on the report. If no value is specified for **Language**, Reporting Services uses the locale of the operating system on the report server for published reports or of the report authoring computer for report preview.  
  
 For HTML reports, you can override the default **Language** value and use the language specified by the HTTP header of the browser client by using the built-in field User!Language in an expression for the **Language** property of a report or a text box.  
  
 You can also specify the **Language** property for a report in a URL. For more information, see [Set the Language for Report Parameters in a URL](../set-the-language-for-report-parameters-in-a-url.md).  
  
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
  
## See Also  
 [Expression Uses in Reports &#40;Report Builder and SSRS&#41;](expression-uses-in-reports-report-builder-and-ssrs.md)  
  
  
