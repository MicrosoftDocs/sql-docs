---
title: "Configure SQL Server Error Logs"
description: Learn about error log recycling. See how to set a maximum log file size and how to set the number of previous log files that SQL Server backs up and archives.
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
f1_keywords:
  - "sql13.swb.configurelogs.configureerrorlogs.f1"
---
# SCM Services - Configure SQL Server Error Logs

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic describes how to view or modify the way [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error logs are recycled.  

## To open the Configure SQL Server Error Logs dialog box  

1. In Object Explorer, expand the instance of SQL Server, expand **Management**, right-click **SQL Server Logs**, and then click **Configure**.

2. In the **Configure SQL Server Error Logs** dialog box, choose from the following options.

    a. Log files count

      **Limit the number of the error log files before they are recycled**

      Check to limit the number of error logs created before they are recycled. A new error log is created each time an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is started. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] retains backups of the previous six logs, unless you check this option, and specify a different maximum number of error log files below.  
  
      **Maximum number of error log files**

      Specify the maximum number of archived error log files created before they are recycled. The default is 6, not including the current one. This value determines the number of previous backup logs that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] retains before recycling them.

    b. Log file size

      **Maximum size for error log file in KB**

      You can set the size amount of each file in KB. If you leave it at 0 the log size is unlimited.
