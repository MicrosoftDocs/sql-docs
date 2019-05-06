---
title: "Release notes for (SSRS) 2017 and later | Microsoft Docs"
ms.date: 09/01/2018
ms.prod: reporting-services
ms.prod_service: reporting-services-native
ms.technology: reporting-services

ms.topic: conceptual
ms.reviewer: maghan
author: casualoak
ms.author: RhysSchmidtke
monikerRange: ">=sql-server-2017||=sqlallproducts-allversions"
---
# Release notes for SQL Server Reporting Services (SSRS) 2017 and later

[!INCLUDE [ssrs-appliesto](../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-2017-and-later](../includes/ssrs-appliesto-2017-and-later.md)]

This article describes changes in [!INCLUDE[ssnoversion](../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] (SSRS), for versions 2017 and later.

For the release notes for Report Viewer controls, see [Release Notes for the Report Viewer controls for WebForms and WinForms of SSRS](application-integration/release-notes-ssrs-application-integration.md).

<!--
We are "standardizing" all our 'Release Notes' style articles:

  - File names:   'release-notes-[TechArea-Name].md'


  - Content format:   2-column tables.  No longer using bullet lists.

    - Ideally, the 'Details' column should contain a link to another SSRS documentation article wherein the particular issue fix is discussed in any way.  Or if there is more to say beyond one sentence, the other sentences of elaboration would go into the 'Details' column.


  - H2 header names are kept short, for better display.


  - Try to keep all Release Notes in one .md file.  Avoid having multiple R.N. files whose names differ only by version or date.

    - Seriously consider erasing info about ancient releases that are so old that nobody cares about them anymore.  If you really want to retain ancient info, consider doing so in an HTML comment at the end of the .md file, just in case a Microsoft employee needs to re-examine ancient info.  In any case, this file cannot get ever longer, and some deletion or hiding of oldest info must eventually occur.


  - Do use '::: moniker range=' zones when version 2017 is no longer the only version represented inside this .md file.

    - Use the '=' operator on the moniker, not the '>=' operator.
    - In contrast, in our 'Whats New' articles we do use the '>=', rather than '='.

GeneMi, DevOps = 1467988 (MsEng > TechnicalContent) , 2019/03/19
-->

## 14.0.600.1109, 2019/02/12

| Fixed issue | Details |
| :---------- | :------ |
| Cache report snapshot schedules changes to "Report-specific schedule" after modifying subscription. | &nbsp; |
| rc:Toolbar=false is not working in Express edition. | &nbsp; |
| Certain Thai characters render incorrectly when exporting paginated reports to PDF. | &nbsp; |
| Deadlock during notification of completed data-driven subscriptions. | &nbsp; |
| Embedded images are not displayed in certain circumstances when the rc:Toolbar=False parameter used. | &nbsp; |
| Unable to create data driven subscriptions for reports that use cascading parameters. | &nbsp; |
| Unable to edit subscriptions that are configured with an invalid interval. | &nbsp; |
| Security updates. | &nbsp; |
| Linked reports UI is not showing. | &nbsp; |
| Certain paginated reports with nested tablix controls have incorrect fonts. | &nbsp; |
| Whitespace is incorrectly added to certain paginated reports that contain tablix data regions. | &nbsp; |
| Header rows disappear when expanding the simple data grids of a mobile report. | &nbsp; |
| &nbsp; | &nbsp; |

## 14.0.600.906, 2018/09/12

The following issue has been fixed:

| Fixed issue | Details |
| :---------- | :------ |
| Custom Authentication is not returning correct cookie information. | &nbsp; |
| &nbsp; | &nbsp; |

## 14.0.600.892, 2018/08/31

| Fixed issue | Details |
| :---------- | :------ |
| Textbox inside Rectangle causes the rectangle to not grow vertically when rc:Toolbar=False and it has long text. | &nbsp; |
| Text size is not scaling if pageHeight is less than 0.5 inches. | &nbsp; |
| Deadlock occurs in the SSRS catalog database when it is used with CRM. | &nbsp; |
| Vertically aligned column headers displayed incorrectly when scrolling down in report. | &nbsp; |
| Users added to SCOM Reporting Role have access blocked to SSRS web portal. | &nbsp; |
| Thai character is not exported correctly into the PDF. | &nbsp; |
| Browser Role Behavior Change. | &nbsp; |
| rc:Toolbar=false does not work in Express edition. | &nbsp; |
| Missing the vertical scrollbar in the parameter prompt area. | &nbsp; |
| Updated Mobile Report Runtime. | &nbsp; |
| &nbsp; | &nbsp; |

## 14.0.600.744, 2018/04/25

| Fixed issue | Details |
| :---------- | :------ |
| Data Driven Subscription page does not show the Delivery Option once it is created. | &nbsp; |
| Upgrading SSRS 2012 to SSRS 2017 results in RSManagement throwing an exception every few seconds. | &nbsp; |
| Cannot change defaults values for multi-value parameters in IE11. | &nbsp; |
| Schedules are empty whenever shared schedule is executed. | &nbsp; |
| &nbsp; | &nbsp; |

## 14.0.600.689, 2018/02/28

| Fixed issue | Details |
| :---------- | :------ |
| Report Parameter visibility in a linked report is reverted after editing its properties. | &nbsp; |
| URL Parameter rc:Toolbar=false does not work in Express edition. | &nbsp; |
| Having expressions in a Textbox with the CanGrow property set to false results in values not displaying. | &nbsp; |
| Added _Learn more_ link for product key in setup. | &nbsp; |
| Web portal with custom forms authentication ignores sliding expiration cookie. | &nbsp; |
| Export to Word creates unequal row height if row content is empty. | &nbsp; |
| &nbsp; | &nbsp; |

## 14.0.600.594, 2018/01/09

Some security updates were implemented.

### 14.0.600.490, 2017/11/01

Resolved the issues with SKU upgrade.

## 14.0.600.451, 2017/09/30

Initial release.

## Next steps

[What's New in Reporting Services (SSRS)?](what-s-new-in-sql-server-reporting-services-ssrs.md)

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231).
