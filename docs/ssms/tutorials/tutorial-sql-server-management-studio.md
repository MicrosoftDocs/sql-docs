---
title: "Tutorial: SQL Server Management Studio (SSMS) | Microsoft Docs"
ms.custom: ""
ms.date: "08/30/2017"
ms.prod: "sql-non-specified"
ms.prod_service: "sql-tools"
ms.service: ""
ms.component: "ssms-tutorial"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "tools-ssms"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.tutorialstart.ssms.f1"
helpviewer_keywords: 
  - "projects [SQL Server Management Studio], tutorials"
  - "templates [SQL Server], SQL Server Management Studio"
  - "source controls [SQL Server Management Studio], tutorials"
  - "Help [SQL Server], SQL Server Management Studio"
  - "tutorials [SQL Server Management Studio]"
  - "Transact-SQL tutorials"
  - "solutions [SQL Server Management Studio], tutorials"
  - "SQL Server Management Studio [SQL Server], tutorials"
  - "scripts [SQL Server], SQL Server Management Studio"
ms.assetid: d2bade70-07cf-4d94-b5d2-88aecb538ed1
caps.latest.revision: 22
author: "MashaMSFT"
ms.author: "mathoma"
manager: "craigg"
ms.reviewer: "sstein"
ms.workload: "Active"
---
# Tutorial: SQL Server Management Studio
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

The SQL Server Management Studio (SSMS) tutorial introduces you to the integrated environment for managing your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] infrastructure. [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] presents a graphical interface for configuring, monitoring, and administering instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. It also allows you to deploy, monitor, and upgrade the data-tier components used by your applications, such as databases. [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] also provides [!INCLUDE[tsql](../../includes/tsql-md.md)], MDX, DMX, and XML language editors for editing and debugging scripts.  
  
## What You Will Learn  
This tutorial will help you understand the presentation of information in SSMS and how to take advantage of its features.
  
The best way to get acquainted with SSMS is through hands-on practice. This tutorial will teach you how to manage the components of SSMS and how to find the features that you use regularly.  

  
This tutorial is divided into several sections:  
  
- [Connect & Query SQL Server using SSMS](connect-query-sql-server.md)

    In this section, you will learn how to connect to your SQL Server instance. You will also learn some basic Transact-SQL (T-SQL) commands to create and then query a new database. 

- [Scripting Objects in SSMS](scripting-ssms.md)

    In this section, you will learn how to script out various objects in SSMS, including databases and queries. 

- [Using Templates in SSMS](templates-ssms.md)
   
    In this section, you will learn how to work with the pre-built Templates within SSMS. 

- [SSMS Configuration](ssms-configuration.md)

    In this section, you will learn the basics of configuring your SSMS environment. 
  

- [Additional Tips and Tricks for using SSMS](ssms-tricks.md)

    In this section, you will learn additional tips and tricks for using SSMS. The tutorial includes the following:
    - Commenting and uncommenting text
    - Indenting text
    - Filtering Objects in Object Explorer
    - Accessing your SQL Server error log
    - Finding the name of your instance 
 
  
## Requirements  
This tutorial is intended for experienced database administrators and database developers who are not familiar with [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)], but who are familiar with database concepts and [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
You must have the following installed to use this tutorial:  

  -   Install the latest version of [SQL Server Management Studio (SSMS)](../download-sql-server-management-studio-ssms.md).  

The first section walks you through creating a database but other sample databases can be found here: [AdventureWorks Sample Databases](https://github.com/Microsoft/sql-server-samples/releases). Instructions for restoring databases in SSMS can be found here: [Restoring a Database](https://docs.microsoft.com/en-us/sql/relational-databases/backup-restore/restore-a-database-backup-using-ssms). 


  
## See Also  
[Database Engine Tutorials](../../relational-databases/database-engine-tutorials.md)  
  
  
  

