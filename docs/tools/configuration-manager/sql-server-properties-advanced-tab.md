---
title: "SQL Server Properties (Advanced Tab)"
description: Learn about the options on the Advanced tab in the SQL Server Properties dialog box, such as the data path, the instance ID, and custom properties.
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: tools-other
ms.topic: conceptual
ms.assetid: 2ffd10fd-bac1-478f-9cff-96ed6c8b787f
author: markingmyname
ms.author: maghan
monikerRange: ">=sql-server-2016"
---
# SQL Server Properties (Advanced Tab)
[!INCLUDE [SQL Server Windows Only](../../includes/applies-to-version/sql-windows-only.md)]
  The following properties appear on the **Advanced** tab by default. If custom properties are defined, they also appear on this tab, with their values.  
  
## Options  
 **Clustered**  
 Indicates if this service is installed as a resource of a clustered server.  
  
 **Customer Feedback Reporting**  
 Indicates whether Service Quality Monitoring has been enabled on this service. For more information on Customer Feedback Reporting, search Books Online for the topic, "Error and Usage Report Settings."  
  
 **Data Path**  
 Displays the path to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] binaries for this installation of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 **Dump Directory**  
 Displays the location where memory dumps are placed in case of an error.  
  
 **Error Reporting**  
 When set to **Yes**, the Dr. Watson program forwards information to either [!INCLUDE[msCoName](../../includes/msconame-md.md)] or your error server if a serious failure occurs. For more information on Error Reporting, search Books Online for the topic, "Error and Usage Report Settings." To change this value, in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] Object Explorer, right-click your server, click **Properties**, and then click the **Misc. Server Settings** page. The options are presented in the **Information Reporting** area.  
  
 **File Version**  
 Displays the version of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] executable.  
  
 **Install Path**  
 Displays the path to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] binaries for this installation of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 **Instance ID**  
 Indicates the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance that used this service.  
  
 **Language**  
 Displays the default language for server messages.  
  
 **Registry Root**  
 Displays the location of the registry keys used by this application.  
  
 **Service Pack Level**  
 Displays the service pack level of this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 **SKU Name**  
 Displays the product stock keeping unit (SKU), sometimes called the product edition.  
  
 **Startup Parameters**  
 Lists any startup parameters that are used by this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Parameters are separated by semi-colons. The default parameters include the paths to the data file for the master database (`master.mdf`), the log file for the master database (`mastlog.ldf`), and the error log file. For the syntax of startup parameters, search Books Online for the topic **Using the SQL Server Service Startup Options.**  
  
 **Stock Keeping Unit**  
 Displays the product stock keeping unit (SKU) number.  
  
 **Version**  
 Displays the version number of this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 **Virtual Server Name**  
 **Virtual Server Name** when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed on a clustered server.  
  
  
