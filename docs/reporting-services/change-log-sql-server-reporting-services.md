---
title: "Change log for SQL Server Reporting Services (2017 and later) | Microsoft Docs"
ms.custom: ""
ms.date: "04/25/2018"
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.service: ""
ms.component: "reporting-services"
ms.reviewer: ""
ms.suite: ""
ms.technology: 

ms.tgt_pltfrm: ""
ms.topic: "article"
author: "casualoak"
ms.author: "edugonz"
manager: "kfile"
ms.workload: "Active"
---

# Change log for SQL Server Reporting Services

[!INCLUDE [ssrs-appliesto](../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-2017-and-later](../includes/ssrs-appliesto-2017-and-later.md)] 

This article describes changes in [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)]. 

## SQL Server 2017 Reporting Services 

- *Version 14.0.600.744, Released: April 25, 2018* 
    - Bug Fixes:
        - Data Driven Subscription page does not show the Delivery Option once it is created
        - Upgrading SSRS 2012 to SSRS 2017 results in RSManagement throwing an exception every few seconds
        - Cannot change defaults values for multi-value parameters in IE11
        - Schedules are empty whenever shared schedule is executed

- *Version 14.0.600.689, Released: February 28, 2018* 
    - Bug Fixes:
        - Report Parameter visibility in a linked report is reverted after editing its properties
        - URL Parameter rc:Toolbar=false doesn't work in Express edition
        - Having expressions in Textbox with CanGrow property set to false is resulting in values not showing
        - Added "Learn more" link for product key in setup
        - Web portal with custom forms authentication is ignoring sliding expiration cookie
        - Export to Word creates unequal row height if row content is empty

- *Version 14.0.600.594, Released: January 9, 2018*
    - Security Updates

- *Version 14.0.600.490, Released: November 1, 2017* 
    - Bug Fixes:
        - Resolved issues with SKU upgrade

- *Version 14.0.600.451, Released: September 30, 2017* 
    - Initial release

## Next steps

[What's New in Reporting Services (SSRS)](what-s-new-in-sql-server-reporting-services-ssrs.md)   

More questions? [Try asking the Reporting Services forum](http://go.microsoft.com/fwlink/?LinkId=620231)
