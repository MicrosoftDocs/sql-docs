---
title: "Granting Access to a Database | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "database access"
ms.assetid: 686edfe2-3650-48a6-a2da-9d46fa211ad8
author: VanMSFT
ms.author: vanto
manager: craigg
---
# Granting Access to a Database
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
 [Creating Views and Stored Procedures](lesson-2-3-creating-views-and-stored-procedures.md)  
  
  
