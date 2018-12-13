---
title: "Customize the Behavior of Word Breakers with a Custom Dictionary | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: search
ms.topic: conceptual
ms.assetid: a8e278d1-aeaa-48f1-a0c6-5de232c983e4
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Customize the Behavior of Word Breakers with a Custom Dictionary
  You can customize the behavior of the word breaker for a particular language by creating a language-specific custom dictionary file. For example, you can prevent the word breaker from breaking certain terms or patterns.  
  
 For more information, see the following SharePoint article:  
  
 [Create a custom dictionary (SharePoint Server 2010)](https://go.microsoft.com/fwlink/?LinkId=215011)  
  
 For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], place custom dictionary files in the following folder:  
  
 `C:\Program Files\Microsoft SQL Server\<instance name>\MSSQL\Binn`  
  
 After creating or changing custom dictionary files, restart the SQL Full-text Daemon Launcher with the following command:  
  
 `exec sp_fulltext_service 'restart_all_fdhosts'`  
  
  
