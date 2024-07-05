---
title: Format numbers and dates in Report Builder paginated reports
description: Learn how to format numbers and dates in Report Builder data regions in a paginated report by selecting a format from the Properties dialog box.
author: maggiesMSFT
ms.author: maggies
ms.date: 07/05/2024
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom: updatefrequency5
f1_keywords:
  - "sql13.rtp.rptdesigner.placeholderproperties.number.f1"
  - "10127"
  - "sql13.rtp.rptdesigner.textboxproperties.number.f1"
  - "10130"
  - "10286"
  - "sql13.rtp.rptdesigner.serieslabelproperties.number.f1"
  - "10285"
  - "sql13.rtp.rptdesigner.axisproperties.number.f1"

#customer intent: As a Report Builder user, I want to learn how to format numbers and dates in my reports so that I can improve their presentation.
---
# Format numbers and dates in Report Builder paginated reports

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

You can format numbers and dates in data regions in a paginated report by selecting a format from the **Number** page of the corresponding data region's **Properties** dialog box.  
  
To specify format strings within a text box report item, you need to select the item that you want to format, right-click, select **Text Box Properties**, and then select **Number**. You can format individual cells in a table or matrix data region in the same manner, because cells in a table or matrix are individual text boxes.  
  
A chart data region commonly shows dates along the category (x) axis and values along the value (y) axis. To specify formatting in a chart, right-click an axis and select **Axis Properties**. On the value axis, you can specify formats only for numbers. For more information, see [Format axis labels on a paginated report chart (Report Builder)](../../reporting-services/report-design/formatting-axis-labels-on-a-chart-report-builder-and-ssrs.md).  
  
To specify formatting in a Gauge data region, right-click the scale of the gauge and select **Radial Scale Properties** or **Linear Scale Properties**.  
  
> [!NOTE]  
> If some formatting options you want to use are grayed out, it means that those formatting options aren't compatible with the field's data type, which is set in the data source. For example, if the field contains number values but the field's data type is String, you can't apply numerical data formatting options such as currency or decimals.

> [!NOTE]  
> [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Considerations for Formatting Numbers and Dates  

Before you format numbers and dates in your report, consider the following details:  
  
- By default, numbers are formatted to reflect the cultural settings on the client computer. Use formatting strings to specify how numbers display so that formatting is consistent regardless of where the person who is viewing the report is located.  
  
- The formats provided on the **Number** page are a subset of the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] standard numeric format strings. To format a number or date using a custom format that isn't shown in the dialog box, you can use any [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] format strings for numbers or dates.
  
- If a custom format string is specified, it has a higher priority over default settings that are culture-specific. For example, suppose you set a custom format string of "#,###" to show the number 1234 as 1,234. This format might have different meaning to users in the United States than it does to users in Europe. Before you specify a custom format, consider how the format you choose affects users of different cultures who might view the report.  
  
- If you specify an invalid format string, the formatted text is interpreted as a literal string, which overrides the formatting.  
  
- If you format a mix of numbers and characters in the same text box, consider the use of a placeholder to format the number separately from the rest of the text. For more information, see [Format text and placeholders in paginated reports (Report Builder)](../../reporting-services/report-design/formatting-text-and-placeholders-report-builder-and-ssrs.md). If an invalid format string is specified for the `Format` property on the text box, the format string is ignored. If you specify an invalid format string for the `Format` property on the chart or gauge, the format string that you specify is interpreted as a string. Then formatting isn't applied.  
  
- If you select **Currency** under **Category**, and you check **Show values in**, you can select **Thousands**, **Millions**, or **Billions** to display numbers that use financial formats. For example, if the field value is 1,789,905,394 and you select **Billions** and specify 2 decimal places, the value displayed in the report is 1.78.  
  
## Related content  

- [Format text and placeholders in paginated reports (Report Builder)](../../reporting-services/report-design/formatting-text-and-placeholders-report-builder-and-ssrs.md)
- [Format lines, colors, and images in a paginated report (Report Builder)](../../reporting-services/report-design/formatting-lines-colors-and-images-report-builder-and-ssrs.md)
- [Format a chart in a paginated report (Report Builder)](../../reporting-services/report-design/formatting-a-chart-report-builder-and-ssrs.md)
  