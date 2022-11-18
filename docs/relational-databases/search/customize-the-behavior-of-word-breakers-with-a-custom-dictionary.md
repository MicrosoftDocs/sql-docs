---
description: "Customize behavior of word breakers with a custom dictionary (SQL Server Search)"
title: "Customize behavior of word breakers with a custom dictionary"
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: search
ms.topic: conceptual
ms.assetid: a8e278d1-aeaa-48f1-a0c6-5de232c983e4
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
ms.custom: "seo-lt-2019"
---
# Customize behavior of word breakers with a custom dictionary (SQL Server Search)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  You can customize the behavior of the word breaker for a particular language by creating a language-specific custom dictionary file. For example, you can prevent the word breaker from breaking certain terms or patterns.  
  
 For more information, see the following SharePoint article:  
  
 [Create a custom dictionary (SharePoint Server 2010)](/previous-versions/office/sharepoint-server-2010/cc263242(v=office.14))  
  
 For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], place custom dictionary files in the following folder:  
  
 `C:\Program Files\Microsoft SQL Server\<instance name>\MSSQL\Binn`  
  
 After creating or changing custom dictionary files, restart the SQL Full-text Daemon Launcher with the following command:  
  
 `exec sp_fulltext_service 'restart_all_fdhosts'`  
  
