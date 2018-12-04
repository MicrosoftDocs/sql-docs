---
title: "View or Change the Default Locations for Data and Log Files | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: sql
ms.prod_service: high-availability
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
# View or Change the Default Locations for Data and Log Files
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
 The best practice for protecting your data files and log files is to ensure that they are protected by access control lists (ACLs). Set the ACLs on the directory root under which the files are created.  
 
  
## View or change the default locations for database files  
  
1.  In Object Explorer, right-click on your server and click **Properties**.  
  
2.  In the left panel on that Properties page, click the **Database settings** tab.  
  
3.  In **Database default locations**, view the current default locations for new data files and new log files. To change a default location, enter a new default pathname in the **Data** or **Log** field, or click the browse button to find and select a pathname.  
  
>**NOTE:** After changing the default locations, you must stop and start the SQL Server service to complete the change.  
  
## See also  
 [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](../../t-sql/statements/create-database-sql-server-transact-sql.md)   
 [Create a Database](../../relational-databases/databases/create-a-database.md)  
  
  
