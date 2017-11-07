---
title: "Database Mail XPs Server Configuration Option | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Database Mail XPs option"
  - "Database Mail [SQL Server], enabling"
ms.assetid: e22c4e63-1792-473b-af11-14a7931ca9ed
caps.latest.revision: 20
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Database Mail XPs Server Configuration Option
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Use the **DatabaseMail XPs** option to enable Database Mail on this server. The possible values are:  
  
-   **0** indicating Database Mail is not available (default).  
  
-   **1** indicating Database Mail is available.  
  
 The setting takes effect immediately without a server stop and restart.  
  
 After enabling Database Mail, you must configure a Database Mail host database to use Database Mail.  
  
 Configuring Database Mail using the **Database Mail Configuration Wizard** enables the Database Mail extended stored procedures in the **msdb** database. If you use the **Database Mail Configuration Wizard**, you do not have to use the **sp_configure** example below.  
  
 Setting the **Database Mail XPs** option to 0 prevents Database Mail from starting. If it is running when the option is set to 0, it continues to run and send mail until it is idle for the time configured in the **DatabaseMailExeMinimumLifeTime** option.  
  
## Examples  
 The following example enables the Database Mail extended stored procedures.  
  
```  
sp_configure 'show advanced options', 1;  
GO  
RECONFIGURE;  
GO  
sp_configure 'Database Mail XPs', 1;  
GO  
RECONFIGURE  
GO  
```  
  
## See Also  
 [Database Mail](../../relational-databases/database-mail/database-mail.md)  
  
  
