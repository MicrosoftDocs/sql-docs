---
title: "Retain date formatting for Analysis Services in mobile reports | Reporting Services | Microsoft Docs"
description: In Mobile Report Publisher, add a measure to a shared dataset in Report Builder so that dates in Analysis Services data sources retain their data type.
ms.date: 07/21/2022
ms.service: reporting-services
ms.subservice: mobile-reports

ms.topic: conceptual
ms.assetid: e9a9a199-40e3-4381-b250-1b99fb83aa62
author: maggiesMSFT
ms.author: maggies
---
# Retain date formatting for Analysis Services in mobile reports

[!INCLUDE [ssrs-mobile-report-deprecated](../../includes/ssrs-mobile-report-deprecated.md)]

Add a measure to a shared dataset in Report Builder so dates in [!INCLUDE[ssASnoversion_md](../../includes/ssasnoversion-md.md)] data sources retain their data type in [!INCLUDE[SS_MobileReptPub_Long](../../includes/ss-mobilereptpub-short.md)].

The default return type for [!INCLUDE[ssASnoversion_md](../../includes/ssasnoversion-md.md)] queries is a string.  When you build a dataset in [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] Report Builder, the string type is respected and gets saved to the server. 

However, when the JSON table renderer processes the dataset, it reads the value of the column as a string and renders strings.  Then when [!INCLUDE[SS_MobileReptPub_Long](../../includes/ss-mobilereptpub-long.md)] fetches the table, it also only sees strings.

The workaround for this is to add a calculated member when you're creating a shared dataset in Report Builder. It works for both [!INCLUDE[ssASnoversion_md](../../includes/ssasnoversion-md.md)] multidimensional and tabular models.

## Create a measure to retain a date field data type

1. Create a measure to hold the value of the date field in question, and in the expression field, choose the hierarchy/level of the date and append **.CurrentMember.MemberValue**. For example:
 
   [Internet Sales].[Ship Date].CurrentMember.MemberValue
   
   ![Screenshot of the Calculated Member Builder dialog box with the Expression text box called out.](../../reporting-services/mobile-reports/media/ssas-calculated-member-report-builder.png)
   
2. Now you can append this calculated member to the set of columns by dragging it from the Calculated Members list in the bottom left and dropping it in the column grid on the right.  

   ![Screenshot of the Query Designer with the Calculated Members section called out.](../../reporting-services/mobile-reports/media/ssas-query-designer-calculated-member-report-builder.png) 
   
### See also

-  [Data for Reporting Services mobile reports](../../reporting-services/mobile-reports/data-for-reporting-services-mobile-reports.md)
-  [Prepare data for Reporting Services mobile reports](../../reporting-services/mobile-reports/prepare-data-for-reporting-services-mobile-reports.md)
