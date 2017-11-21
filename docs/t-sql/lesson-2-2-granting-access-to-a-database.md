---
title: "Granting Access to a Database | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-non-specified"
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.service: ""
ms.component: "t-sql"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "SQL Server 2016"
helpviewer_keywords: 
  - "database access"
ms.assetid: 686edfe2-3650-48a6-a2da-9d46fa211ad8
caps.latest.revision: 13
author: "edmacauley"
ms.author: "edmaca"
manager: "craigg"
ms.workload: "On Demand"
---
# Lesson 2-2 - Granting Access to a Database
[!INCLUDE[tsql-appliesto-ss2008-all-md](../includes/tsql-appliesto-ss2008-all-md.md)]
Mary now has access to this instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], but does not have permission to access the databases. She does not even have access to her default database **TestData** until you authorize her as a database user.  
  
To grant Mary access, switch to the **TestData** database, and then use the CREATE USER statement to map her login to a user named Mary.  
  
### To create a user in a database  
  
1.  Type and execute the following statements (replacing `computer_name` with the name of your computer) to grant `Mary` access to the `TestData` database.  
  
    ```  
    USE [TestData];  
    GO  
  
    CREATE USER [Mary] FOR LOGIN [computer_name\Mary];  
    GO  
  
    ```  
  
    Now, Mary has access to both [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] and the `TestData` database.  
  
## Next Task in Lesson  
[Creating Views and Stored Procedures](../t-sql/lesson-2-3-creating-views-and-stored-procedures.md)  
  
  
  
