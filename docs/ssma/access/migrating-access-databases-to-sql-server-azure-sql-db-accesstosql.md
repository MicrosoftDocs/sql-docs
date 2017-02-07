---
title: "Migrate Access Databases to SQL Server - Azure SQL DB | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "sql-ssma"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "Azure SQL Database"
  - "SQL Server"
helpviewer_keywords: 
  - "instructions, migration"
  - "migrating databases, overview"
  - "overview, migration process"
  - "procedure, migration"
  - "recommended migration process"
ms.assetid: 76a3abcf-2998-4712-9490-fe8d872c89ca
caps.latest.revision: 23
author: "sabotta"
ms.author: "carlasab"
manager: "lonnyb"
---
# Migrating Access Databases to SQL Server - Azure SQL DB (AccessToSQL)
[!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Migration Assistant (SSMA) is a comprehensive environment that helps you quickly migrate Access databases to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure. By using SSMA, you can review Access and [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure database objects, assess the Access database for migration, convert Access database objects, load them into [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure, and then migrate data.  
  
## Recommended Migration Process  
To successfully migrate objects and data from Access to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure, use the following process:  
  
1.  [Create a new SSMA Project](http://msdn.microsoft.com/en-us/f2d1f0b0-5394-4adb-b3f3-abd71eb68ca7). After you create the project, you can [set project options](http://msdn.microsoft.com/en-us/0a7304df-2f35-4453-96ef-7ac83dea1167). These include conversion options, migration options, and data type mappings.  
  
2.  [Add Access database files](http://msdn.microsoft.com/en-us/e944c740-4c8a-4bc1-b0ed-be57bc06dced) to the project.  
  
    You can add individual files, or you can add files that you find on the computer or network.  
  
3.  [Connect to the target instance of SQL Server](http://msdn.microsoft.com/en-us/f84cf007-ddf1-4396-a07c-3e0729abc769) or [Connect to the target instance of SQL Azure](http://msdn.microsoft.com/en-us/1ba0d113-dc05-4431-8689-e14a8821bafd).  
  
    You can either connect to SQL Server or SQL Azure.  
  
4.  To customize the mapping between one or more Access databases and [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure schemas,  [map the source and target databases](http://msdn.microsoft.com/en-us/69bee937-7b2c-49ee-8866-7518c683fad4).  
  
5.  Optionally, you can [create an assessment report](http://msdn.microsoft.com/en-us/8b9e23d6-da62-437a-8c05-8ad2628b9441)to determine whether the Access database objects can be successfully converted to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure.  
  
6.  [Convert Access database objects](http://msdn.microsoft.com/en-us/e0ef67bf-80a6-4e6c-a82d-5d46e0623c6c) to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure object definitions.  
  
7.  [Load the converted database objects into SQL Server](http://msdn.microsoft.com/en-us/4e854eee-b10c-4f0b-9d9e-d92416e6f2ba).  
  
    You can either load the database objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure by using SSMA, or you can save [!INCLUDE[tsql](../../includes/tsql_md.md)] scripts.  
  
8.  [Migrate Access data into SQL Server](http://msdn.microsoft.com/en-us/f3b18af7-1af0-499d-a00d-a0af94895625).  
  
    > [!NOTE]  
    > You can convert, load, and migrate schemas and data in one step. To perform one-click migration, click the **Convert, Load, and Migrate** button.  
  
9. If you want your Access applications to use the data in [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure, use [link the Access tables to the SQL Server tables](http://msdn.microsoft.com/en-us/82374ad2-7737-4164-a489-13261ba393d4).  
  
You can also use the Migration Wizard to guide you through this process. For more information, see [Migration Wizard](http://msdn.microsoft.com/en-us/5bab5914-b2ae-4795-8cf5-83e42d64bef2).  
  
## See Also  
[Getting Started with SQL Server Migration Assistant for Access](http://msdn.microsoft.com/en-us/462a731f-08f1-44e1-9eeb-4deac6d2f6c5)  
[Preparing Access Databases for Migration](http://msdn.microsoft.com/en-us/9b80a9e0-08e7-4b4d-b5ec-cc998d3f5114)  
  
