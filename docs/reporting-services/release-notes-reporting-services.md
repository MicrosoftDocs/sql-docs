---
title: "Release notes for Reporting Services 2017 and later"
description: Learn details about the changes in SQL Server Reporting Services (SSRS), for versions 2017 and later.
ms.reviewer: petebro
ms.date: 05/28/2024
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: release-notes
ms.custom:
  - updatefrequency5
monikerRange: ">=sql-server-2017"
---

# Release notes for SQL Server Reporting Services (SSRS) 2017 and later

[!INCLUDE [ssrs-appliesto](../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-2017-and-later](../includes/ssrs-appliesto-2017-and-later.md)]

This article describes changes in [!INCLUDE[ssnoversion](../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] (SSRS), for versions 2017 and later.

For the release notes for Report Viewer controls, see [Release notes for the Report Viewer controls for WebForms and WinForms of SSRS](application-integration/release-notes-ssrs-application-integration.md).

## SQL Server 2022 Reporting Services

## 16.0.9388.19190, 2025/09/22
*(Product Version: 16.0.1118.33)*

- Support for default fallback to SimSun-ExtG font
- Fixed issue expanding document maps for some reports
- Fixed issue where custom authentication didn't re-authenticate upon cookie expiration 
- Security improvements
- Accessibility improvements

## 16.0.9276.19198, 2025/05/27
*(Product Version: 16.0.1117.43)*

- Add new advanced server property EnableCommentsOnReports and set default value to false to control comments on reports. This will disable comments on upgrade and must be enabled if you wish to have the ability to comment on reports in the future. This feature is now considered deprecated and will be removed in 2026.
- Updated underlying version of jQuery to 3.7.1

## 16.0.9218.4306, 2025/03/31
*(Product Version: 16.0.1117.32)*

- Apply button on subscription page disabled until parameters load 
- Upgraded version of Angular to 17.0 
- Security improvements
- Accessibility improvements

## 16.0.9101.19239, 2025/01/06
*(Product Version: 16.0.1116.38)*

- Changed default SupportedHyperlinkSchemes advanced server property value to disallow JavaScript
- Changed default TrustedFileFormat advanced server property value to disallow pdf content viewing
- Fixed performance issue with email subscriptions
- Fixed issue with image attachments when posting comments
- Fixed issue changing service account password in RSConfig tool.

## 16.0.8969.32906, 2024/07/23
*(Product Version: 16.0.1116.12)*

- Fixed error faced when editing data driven subscriptions
- Fixed verbose log causing RDL reports to not render
- Fixed repeated messages in RsPortal log file
- Fixed error when overwriting RDL reports
- Fixed error editing monthly schedules
- Fixed error with "Show Hidden Items" checkbox disappearing
- Fixed error with font "Lao UI" when exporting to PDF

## 16.0.8898.18912, 2024/05/20
*(Product Version: 16.0.1115.96)*

- Fixed issue with WMI provider on some calls.

## 16.0.8886.1775, 2024/05/01
*(Product Version: 16.0.1115.92)*

- Improved support for GB18030-2022 Chinese government standard.
- Added TLS 1.3 support.
- Fixed issue with subscription properties in some scenarios.

## 16.0.8784.14010, 2024/01/23
*(Product Version: 16.0.1115.61)*

-  Full-screen view for RDL reports.
-  Responsive navigation adapted to a small view port.
-  Added support for GB18030-2022 Chinese government standard.
-  Added an ability to reorder tiles sections in Portal, using new config property "DefaultTilesSectionsOrder". 
-  Enabled email comment field for individual subscriptions which was previously disabled.
-  Enabled downloading Excel files from Web Service Portal URL.
-  Fixed appearance of links in Document Map of RDL report.  
-  Fixed issue with accessing some URLs using custom authentication.  
-  Fixed vertical textbox rendering in RDL.  
-  Fixed an issue with NULL value in CC field of a data-driven email subscription.  
-  Fixed exporting to Excel/Word when using Virtual Service Account & Execution Account.  
-  Fixed displaying time in schedules in Portal (now all times are displayed in Client time zone).

## 16.0.8564.33454, 2023/06/13
*(Product Version: 16.0.1114.11)*

-  Introduced new Accessibility property that lets report authors to add accessible headers to tables and Heading levels to text boxes.
-  Fixed setting calendar days to a single day for monthly schedules.
-  Fixed an issue in Report Server (Web Service) page localization, and localization of exported Excel files 
Fixed slowness of Large MDX Query as an Expression.

## 16.0.8361.39598, 2022/11/23
*(Product Version: 16.0.1113.11)*

-  Fixed issue where some SQL Server 2022 product keys weren't working with SQL Server 2022 Reporting Services.

## 16.0.8353.8096, 2022/11/16
*(Product Version: 16.0.1112.48)*

Initial release.

- Major changes from SQL Server 2019 Reporting Services:
    - New portal experience with performance improvements using Angular
    - Accessibility fixes to many parts of the portal
    - New [Power BI Migration experience](/power-bi/guidance/migrate-ssrs-reports-to-power-bi#migration-tool-for-sql-server-2022) in the portal
    - Ability to turn off size calculation for snapshots via configuration property **EnableListHistorySnapshotsSize** if History Snapshots page loads slow due to a large number of snapshots
    - Security enhancements
    - Bug fixes
- [Deprecated features](./deprecated-features-in-sql-server-reporting-services-ssrs.md): 
    - Report Parts support 
- [Discontinued features](./discontinued-functionality-to-sql-server-reporting-services-in-sql-server.md): 
    - Pin to Power BI functionality
    - Mobile Reports functionality (`.rsmobile`) and Mobile Report Publisher

## 15.0.8264.8408, 2022/08/19
*(Product Version: 15.0.1111.106)*

Release Candidate 0 (RC 0)

- New portal experience with performance improvements using Angular.
- Accessibility fixes to many parts of the Portal.
- Security enhancements.
- Bug fixes.
- Discontinuation of several features: Pin to Power BI, Mobile Reports, and Mobile Report Publisher.


## SQL Server 2019 Reporting Services

## 15.0.9395.19445, 2025/09/24
*(Product Version: 15.0.1105.34)*
- Support for default fallback to SimSun-ExtG font
- Fixed issue expanding document maps for some reports
- Resolved several accessibility issues
- Security updates

## 15.0.9276.19365, 2025/05/27
*(Product Version: 15.0.1104.39)*
- Add new advanced server property EnableCommentsOnReports and set default value to false to control comments on reports. This will disable comments on upgrade and must be enabled if you wish to have the ability to comment on reports in the future. This feature is now considered deprecated and will be removed in 2026.

## 15.0.9218.715, 2025/03/31
*(Product Version: 15.0.1104.29)*

- Fixed issue with custom branding package 
- Fixed issue with exporting to Excel file format
- Apply button on subscription page disabled until parameters load 
- Upgraded version of Angular to 17.0 
- Security improvements
- Accessibility improvements

## 15.0.9098.6826, 2025/01/06
*(Product Version: 15.0.1103.41)*

- Added Mobile Report support back to SSRS 2019
- Updated SSRS to use Microsoft.Data.SqlClient in place of (now deprecated) System.Data.SqlClient
- Changed default SupportedHyperlinkSchemes advanced server property value to disallow JavaScript
- Changed default TrustedFileFormat advanced server property value to disallow pdf content viewing
- Fixed performance issue with email subscriptions
- Fixed issue with image attachments when posting comments
- Fixed issue changing service account password in RSConfig tool.

## 15.0.8969.33375, 2024/07/23
*(Product Version: 15.0.1103.12)*

- Fixed error faced when editing data driven subscriptions
- Fixed verbose log causing RDL reports to not render
- Fixed repeated messages in RsPortal log file
- Fixed error when overwriting RDL reports
- Fixed error editing monthly schedules
- Fixed error with font "Lao UI" when exporting to PDF

## 15.0.8863.19101, 2024/04/10
*(Product Version: 15.0.1102.1167)*
- Fixed issue where some reports will fail to render after enable the verbose log
- Fixed an error some users experienced when editing subscriptions 
- Fixed and issue where some links were missing when exporting to PDF
- Added support for server property 'RestrictedResourceMimeTypeForUpload'

## 15.0.8760.20928, 2023/12/26
*(Product Version: 15.0.1102.1140)*
- Fixed permission issue that affected some users in the new Portal experience.

## 15.0.8738.29460, 2023/12/04
*(Product Version: 15.0.1102.1129)*
- New default Portal experience (can be reverted via setting UsePortalV2 to false in the config table of catalog database) 
  - Enhanced accessibility
  - Supports smaller viewport
- New config setting for tiles sections order
- New config setting for Tile or List View by default 
- Enabled email comment field for individual subscriptions which was previously disabled
- Enabled downloading excel files from Web Service Portal URL 
- Fixed downloading content through API with Path specified  
- Fixed issue with accessing some URLs using custom authentication

## 15.0.8599.29221, 2023/07/20
*(Product Version: 15.0.1102.1084)*
- Added support for GB18030-2022 Chinese government standard

## 15.0.8563.17333, 2023/06/20
*(Product Version: 15.0.1102.1075)*
- Fixed issue with date type parameters and Oracle data source
- Fixed slowness of Large MDX Query as an Expression 
- Fixed issue with export to Excel / Word when using Virtual Service Account and Execution Account 

## 15.0.8434.2956, 2023/02/06
*(Product Version: 15.0.1102.1047)*
- New [Power BI Migration experience](/power-bi/guidance/migrate-ssrs-reports-to-power-bi#migration-tool-for-sql-server-2022) in the portal.
- Using of single quote in item names is enabled.
- Fixed issue with exporting Arabic characters with Calibri font to PDF.
- Fixed layout misalignment in vertical writing mode.
- Fixed formatting loss of empty cells exported to Word.
- Fixed problem with row height in matrix after exporting to PDF
- Changes to telemetry configuration and logging
- Security fixes

## 15.0.8270.42049, 2022/08/31
*(Product Version: 15.0.1102.1002)*

- Fixed issue with certain parameters causing connection error to certain data sources.
- Fixed issue with date time in some locales.
- Fixed issue with spacing in PDF exports.
- Fixed issue with email priority in a subscription set incorrectly.
- Updated versions of some utilized open-source software.
- Security fixes

## 15.0.8115.18148, 2022/04/04 
*(Product Version: 15.0.1102.962)*

| Fixed issue | Details |
| :---------- | :------ |
| Fixed datetime parameter issue affecting specific locales  | &nbsp; |
| Fixed System.FormatException when retrieving data from ESSBASE  | &nbsp; |
| Fixed issue where editing security groups on an item could remove all previous settings  | &nbsp; |
| Fixed PDF search issue in right-to-left languages  | &nbsp; |

## 15.0.7961.31630, 2021/10/20 
*(Product Version: 15.0.1102.932)*

| Fixed issue | Details |
| :---------- | :------ |
| Added `<LINK>` tag in PDFs exported with accessible PDF enabled on textboxes with 'Go to URL' actions.  | &nbsp; |
| Added `<UL>` and `<LI>` tags in PDFs exported with accessible PDF enabled for textbox Lists | &nbsp; |
| Fixed an issue with date parameters in Oracle based reports failing with "ORA-01008: not all variables bound"  | &nbsp; |

## 15.0.7842.32355, 2021/06/24 
*(Product Version: 15.0.1102.911)*

| Fixed issue | Details |
| :---------- | :------ |
| Fixed an issue for registering Report Server instance to enable Pin to Power BI feature  | &nbsp; |
| Fixed an issue screen readers reading extra rows and columns for a tablix when exported to MHTML using new HTML DOCTYPE  | &nbsp; |

## 15.0.7765.17516, 2021/04/7 
*(Product Version: 15.0.1102.896)*

| Fixed issue | Details |
| :---------- | :------ |
| Fixed an issue screen readers reading extra rows and columns for a tablix when exported to MHTML  | &nbsp; |
| Fixed an issue with Teradata based datasources have NULL value  | &nbsp; |
| Fixed an issue with SSRS MHTML renderer using an older HTML DOCTYPE | &nbsp; |

## 15.0.7545.4810, 2020/08/31 
*(Product Version: 15.0.1102.861)*

| Fixed issue | Details |
| :---------- | :------ |
| Security updates  | &nbsp; |
| Constrained Comment attachment support to no longer allow PDF documents  | &nbsp; |
| Fixed filename truncation when exporting reports containing a period in the name  | &nbsp; |
| Fixed an issue related to Subscriptions and the zh-TW culture that resulted in invalid date format errors  | &nbsp; |
| Fixed an issue with certain reports where accessing the parameters option leads to indefinite spinny  | &nbsp; |
| Fixed issues relating to single quotes in report names  | &nbsp; |
| Fixed an issue in URL Access causing FindString to not locate matches  | &nbsp; |
| Fixed an issue where alt text for PDF export weren't correctly encoded for multi-byte characters  | &nbsp; |
| Fixed unwanted appearance of an empty image under a linear element  | &nbsp; |
| Fixed erroneous Unsupported error for Custom Authentication in Web Edition  | &nbsp; |
| Fixed an issue where a screen reader was reading an extra row and extra column in a Tablix  | &nbsp; |
| Fixed an image truncation issue with fit to size when zoomed to whole page  | &nbsp; |
| Command line upgrade no longer requires EULA flag  | &nbsp; |

## 15.0.7243.37714, 2019/11/01
*(Product Version: 15.0.1102.675)*

Initial release.


## SQL Server 2017 Reporting Services

## 14.0.601.20, 2023/02/14
*(Product Version: 14.0.601.20)*

- New [Power BI migration experience](/power-bi/guidance/migrate-ssrs-reports-to-power-bi#migration-tool-for-sql-server-2022) in the portal.

| Fixed issue | Details |
| :---------- | :------ |
| Fixed issue with RTL alignment of toggles in RDL reports.  | &nbsp; |
| Fixed formatting loss of empty cells exported to Word.  | &nbsp; |
| Changes to telemetry configuration and logging.  | &nbsp; |
| Security fixes.  | &nbsp; |

## 14.0.600.1860, 2022/04/26
*(Product Version: 14.0.600.1860)*

| Fixed issue | Details |
| :---------- | :------ |
| Fixed an issue rendering some reports in Microsoft Edge browser.  | &nbsp; |
| Fixed an issue for some locales when working with data from an Essbase datasource.  | &nbsp; |

## 14.0.600.1763, 2021/06/28 
*(Product Version: 14.0.600.1763)*

| Fixed issue | Details |
| :---------- | :------ |
| Fixed an issue for registering Report Server instance to enable Pin to Power BI feature  | &nbsp; |

## 14.0.600.1669, 2020/08/31 

| Fixed issue | Details |
| :---------- | :------ |
| Security updates  | &nbsp; |
| Constrained Comment attachment support to no longer allow PDF documents  | &nbsp; |
| Fixed filename truncation when exporting reports containing a period in the name  | &nbsp; |
| Fixed an issue related to Subscriptions and the zh-TW culture that resulted in invalid date format errors  | &nbsp; |

## 14.0.600.1572, 2020/04/06 

| Fixed issue | Details |
| :---------- | :------ |
| Updated JQuery UI to 1.12  | &nbsp; |
| Fixed URL case-sensitivity  | &nbsp; |
| Fixed Parameter placement when there are many parameters  | &nbsp; |
| Fixed FindString not working in HTML5 rendering  | &nbsp; |
| Analysis Services Client Libraries updated to address TLS 1.0/1.1 deprecation | &nbsp; |

## 14.0.600.1451, 2019/11/13 

| Fixed issue | Details |
| :---------- | :------ |
| Security updates | &nbsp; |
| Paginated reports didn't work properly with filter parameters when snapshot is enabled  | &nbsp; |
| Users with Browser role and default settings didn't have permissions to download Excel files  | &nbsp; |
| Upgrading to Power BI Report Server from SQL Server 2016 Reporting Services failed during upgrade | &nbsp; |
| After you upgrade from SQL Server 2012 Reporting Services, subscriptions failed with "An invalid character was found in the mail header: ','" message | &nbsp; |
| Configuration tool: canceling modal windows in Database section would restart the Reporting Services service | &nbsp; |
| BorderStyle property expression of Textbox controls weren't rendered to Excel format  | &nbsp; |
| Pagination issue that could get certain reports stuck with rendering the same page without ever reaching the last page of the report | &nbsp; |

## 14.0.600.1274, 2019/07/01

| Fixed issue | Details |
| :---------- | :------ |
| Security updates | &nbsp; |
| Unable to select weekdays when creating a shared weekly schedule | &nbsp; |
| Report doesn't display carriage returns properly in Word format | &nbsp; |
| System Center Operations Manager(SCOM) 2019 no longer works with recent SSRS 2017 upgrades | &nbsp; |
| An error occurred when invoking the authorization extension for shared Dataset | &nbsp; |
| Logic changed on stored procedure GetAllProperties in SSRS 2017 and PBIRS, which causes the Web service endpoint ReportingService2010.GetProperties method unable to get any data for linked report | &nbsp; |
| Simple Grid Row Header in Mobile Report disappears when an Item within the Grid is selected | &nbsp; |
| Unable to use date field in Data Driven Subscription parameter | &nbsp; |
| Nested tablix shows small font or partial font in SSRS 2016 and later | &nbsp; |
| Subscriptions with DateTime Parameter error out after user with different Locale edits the Subscription | &nbsp; |
| Creating a Data-driven Subscription with Null Delivery Extension is failing with "A delivery error has occurred" | &nbsp; |
| URL encoding is incorrect when setting the value in Excel\Word format | &nbsp; |

## 14.0.600.1109, 2019/02/12

| Fixed issue | Details |
| :---------- | :------ |
| Cache report snapshot schedules changes to "Report-specific schedule" after modifying subscription. | &nbsp; |
| rc:Toolbar=false isn't working in Express edition. | &nbsp; |
| Certain Thai characters render incorrectly when exporting paginated reports to PDF. | &nbsp; |
| Deadlock during notification of completed data-driven subscriptions. | &nbsp; |
| Embedded images aren't displayed in certain circumstances when the rc:Toolbar=False parameter used. | &nbsp; |
| Unable to create data driven subscriptions for reports that use cascading parameters. | &nbsp; |
| Unable to edit subscriptions that are configured with an invalid interval. | &nbsp; |
| Security updates. | &nbsp; |
| Linked reports UI isn't showing. | &nbsp; |
| Certain paginated reports with nested tablix controls have incorrect fonts. | &nbsp; |
| Whitespace is incorrectly added to certain paginated reports that contain tablix data regions. | &nbsp; |
| Header rows disappear when expanding the simple data grids of a mobile report. | &nbsp; |

## 14.0.600.906, 2018/09/12

The following issue has been fixed:

| Fixed issue | Details |
| :---------- | :------ |
| Custom Authentication isn't returning correct cookie information. | &nbsp; |

## 14.0.600.892, 2018/08/31

| Fixed issue | Details |
| :---------- | :------ |
| Textbox inside Rectangle causes the rectangle to not grow vertically when rc:Toolbar=False and it has long text. | &nbsp; |
| Text size isn't scaling if pageHeight is less than 0.5 inches. | &nbsp; |
| Deadlock occurs in the SSRS catalog database when it's used with CRM. | &nbsp; |
| Vertically aligned column headers displayed incorrectly when scrolling down in report. | &nbsp; |
| Users added to System Center Operations Manager Reporting Role have access blocked to SSRS web portal. | &nbsp; |
| Thai character isn't exported correctly into the PDF. | &nbsp; |
| Browser Role Behavior Change. | &nbsp; |
| rc:Toolbar=false doesn't work in Express edition. | &nbsp; |
| Missing the vertical scrollbar in the parameter prompt area. | &nbsp; |
| Updated Mobile Report Runtime. | &nbsp; |

## 14.0.600.744, 2018/04/25

| Fixed issue | Details |
| :---------- | :------ |
| Data Driven Subscription page doesn't show the Delivery Option once it's created. | &nbsp; |
| Upgrading SSRS 2012 to SSRS 2017, results in RSManagement throwing an exception every few seconds. | &nbsp; |
| Can't change defaults values for multi-value parameters in IE11. | &nbsp; |
| Schedules are empty whenever shared schedule is executed. | &nbsp; |

## 14.0.600.689, 2018/02/28

| Fixed issue | Details |
| :---------- | :------ |
| Report Parameter visibility in a linked report is reverted after editing its properties. | &nbsp; |
| URL Parameter rc:Toolbar=false doesn't work in Express edition. | &nbsp; |
| Having expressions in a Textbox with the CanGrow property set to false results in values not displaying. | &nbsp; |
| Added _Learn more_ link for product key in setup. | &nbsp; |
| Web portal with custom forms authentication ignores sliding expiration cookie. | &nbsp; |
| Export to Word creates unequal row height if row content is empty. | &nbsp; |

## 14.0.600.594, 2018/01/09

Some security updates were implemented.

### 14.0.600.490, 2017/11/01

Resolved the issues with SKU upgrade.

## 14.0.600.451, 2017/09/30

Initial release.

## Related content

- [What's new in SQL Server Reporting Services (SSRS)](what-s-new-in-sql-server-reporting-services-ssrs.md)
- [Try asking the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)
