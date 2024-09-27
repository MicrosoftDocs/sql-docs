---
title: "Retain date formatting for Analysis Services in mobile reports"
description: In Mobile Report Publisher, add a measure to a shared dataset in Report Builder so that dates in Analysis Services data sources retain their data type.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: mobile-reports
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Retain date formatting for Analysis Services in mobile reports

[!INCLUDE [ssrs-mobile-report-deprecated](../../includes/ssrs-mobile-report-deprecated.md)]

Add a measure to a shared dataset in Report Builder so dates in [!INCLUDE[ssASnoversion_md](../../includes/ssasnoversion-md.md)] data sources retain their data type in [!INCLUDE[SS_MobileReptPub_Long](../../includes/ss-mobilereptpub-short.md)].

The default return type for [!INCLUDE[ssASnoversion_md](../../includes/ssasnoversion-md.md)] queries is a string.  When you build a dataset in [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] Report Builder, the string type is respected and gets saved to the server. 

However, when the JSON table renderer processes the dataset, it reads the value of the column as a string and renders strings.  Then when [!INCLUDE[SS_MobileReptPub_Long](../../includes/ss-mobilereptpub-long.md)] fetches the table, it also only sees strings.

The workaround for this constraint is to add a calculated member when you're creating a shared dataset in Report Builder. It works for both [!INCLUDE[ssASnoversion_md](../../includes/ssasnoversion-md.md)] multidimensional and tabular models.

## Create a measure to retain a date field data type

1. Create a measure to hold the value of the date field in question, and in the expression field, choose the hierarchy/level of the date and append **.CurrentMember.MemberValue**. For example:
 
   `[Internet Sales].[Ship Date].CurrentMember.MemberValue`
   
   :::image type="content" source="../../reporting-services/mobile-reports/media/ssas-calculated-member-report-builder.png" alt-text="Screenshot of the Calculated Member Builder box with the Expression text box called out.":::

   
2. Now you can append this calculated member to the set of columns by dragging it from the Calculated Members list in the bottom left and dropping it in the column grid on the right.  

   :::image type="content" source="../../reporting-services/mobile-reports/media/ssas-query-designer-calculated-member-report-builder.png" alt-text="Screenshot of the Query Designer with the Calculated Members section called out.":::

   
### Related content

-  [Data for Reporting Services mobile reports](../../reporting-services/mobile-reports/data-for-reporting-services-mobile-reports.md)
-  [Prepare data for Reporting Services mobile reports](../../reporting-services/mobile-reports/prepare-data-for-reporting-services-mobile-reports.md)
