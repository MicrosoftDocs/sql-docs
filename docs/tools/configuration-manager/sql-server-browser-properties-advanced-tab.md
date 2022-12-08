---
title: "SQL Server Browser Properties (Advanced Tab)"
description: Learn about the options on the Advanced tab in the SQL Server Browser Properties dialog box, such as the dump directory and the instance ID.
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: tools-other
ms.topic: conceptual
ms.assetid: ba79137a-cb72-4bf3-a650-e11d02cfce10
author: markingmyname
ms.author: maghan
monikerRange: ">=sql-server-2016"
---
# SQL Server Browser Properties (Advanced Tab)
[!INCLUDE [SQL Server Windows Only](../../includes/applies-to-version/sql-windows-only.md)]
  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser program runs as a service on the server. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser listens for incoming requests for [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resources and provides information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances installed on the computer.  
  
## Options  
 **Clustered**  
 Indicates if this service is installed as a resource of a clustered server.  
  
 **Customer Feedback Reporting**  
 Indicates whether Service Quality Monitoring has been enabled on this service. For more information on Customer Feedback Reporting, search Books Online for the topic, "Error and Usage Report Settings."  
  
 **Dump Directory**  
 The location where memory dumps are placed in case of an error.  
  
 **Error Reporting**  
 When set to **Yes**, the Dr. Watson program forwards information to either [!INCLUDE[msCoName](../../includes/msconame-md.md)] or your error server if a serious failure occurs. For more information on Error Reporting, search Books Online for the topic, "Error and Usage Report Settings."  
  
 **Instance ID**  
 Indicates the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance that used this [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent instance. The default instance is **MSSQL10_50.MSSQLSERVER**.  
  
## See Also  
 [SQL Server Browser Service](../../tools/configuration-manager/sql-server-browser-service.md)  
  
  
