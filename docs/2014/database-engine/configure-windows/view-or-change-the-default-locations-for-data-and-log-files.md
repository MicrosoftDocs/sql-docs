---
title: "View or Change the Default Locations for Data and Log Files (SQL Server Management Studio) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "log files [SQL Server], changing default location"
  - "data files [SQL Server], changing default location"
ms.assetid: 70a57fda-fcfe-490f-9cf6-5df620e32b2a
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# View or Change the Default Locations for Data and Log Files (SQL Server Management Studio)
  This topic describes how to view and change the default locations of new data and log files in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. The default path is obtained from the registry. After you change the location all new databases created in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will use that location if a different location is not specified.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Recommendations](#Recommendations)  
  
-   **To view or change the data and log file default locations, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
-   **Follow Up:**  [Changing the Default Locations](#FollowUp)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Recommendations"></a> Recommendations  
 The best practice for protecting your data files and log files is to ensure that they are protected by access control lists (ACLs). The ACLs should be set on the directory root under which the files are created.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To view or change the default locations for database files  
  
1.  In Object Explorer, right-click a server and click **Properties**.  
  
2.  In the left panel, click the **Database settings** page.  
  
3.  In **Database default locations**, view the current default locations for new data files and new log files. To change a default location, enter a new default pathname in the **Data** or **Log** field, or click the browse button to find and select a pathname.  
  
##  <a name="FollowUp"></a> Follow Up: After Changing the Default Locations  
 You must stop and start the SQL Server service to complete the change.  
  
## See Also  
 [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](/sql/t-sql/statements/create-database-sql-server-transact-sql)   
 [Create a Database](../../relational-databases/databases/create-a-database.md)  
  
  
