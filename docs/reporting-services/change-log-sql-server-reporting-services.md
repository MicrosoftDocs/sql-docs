---
title: "Change log for SQL Server Reporting Services (SSRS) 2017 and later | Microsoft Docs"
ms.date: 08/31/2018
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: reporting-services

ms.topic: conceptual
author: "casualoak"
ms.author: "edugonz"
monikerRange: ">= sql-server-2017 || = sqlallproducts-allversions"
---
# Change log for SQL Server Reporting Services (SSRS) 2017 and later

[!INCLUDE [ssrs-appliesto](../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-2017-and-later](../includes/ssrs-appliesto-2017-and-later.md)] 

This article describes changes in [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)]. 

## SQL Server 2017 Reporting Services 

### Version 14.0.600.906, Released: September 12, 2018

This bug has been fixed:

- Custom Authentication not returning correct cookie information

### Version 14.0.600.892, Released: August 31, 2018

These bugs have been fixed:

- Textbox inside Rectangle causes rectangle not to grow vertically when rc:Toolbar=False and it has long text 
- Text size is not scaling if pageHeight is less than 0.5 in 
- Deadlock in SSRS catalog database when used with CRM 
- Vertically aligned column headers incorrectly displayed when scrolling down in report 
- Users added to SCOM Reporting Role will have access blocked to SSRS web portal 
- Thai character is not getting exported correctly in PDF 
- Browser Role Behavior Change 
- rc:Toolbar=false doesn't work in Express edition 
- Missing vertical scrollbar in parameter prompt area 
- Updated Mobile Report Runtime 

### Version 14.0.600.744, Released: April 25, 2018 

These bugs have been fixed:

- Data Driven Subscription page does not show the Delivery Option once it is created
- Upgrading SSRS 2012 to SSRS 2017 results in RSManagement throwing an exception every few seconds
- Cannot change defaults values for multi-value parameters in IE11
- Schedules are empty whenever shared schedule is executed

### Version 14.0.600.689, Released: February 28, 2018

These bugs have been fixed:

- Report Parameter visibility in a linked report is reverted after editing its properties
- URL Parameter rc:Toolbar=false doesn't work in Express edition
- Having expressions in Textbox with CanGrow property set to false is resulting in values not showing
- Added "Learn more" link for product key in setup
- Web portal with custom forms authentication is ignoring sliding expiration cookie
- Export to Word creates unequal row height if row content is empty

### Version 14.0.600.594, Released: January 9, 2018

Security updates

### Version 14.0.600.490, Released: November 1, 2017

This bug has been fixed:

- Resolved issues with SKU upgrade

### Version 14.0.600.451, Released: September 30, 2017 

Initial release

## Next steps

[What's New in Reporting Services (SSRS)](what-s-new-in-sql-server-reporting-services-ssrs.md)   

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
