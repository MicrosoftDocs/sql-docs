---
title: "Database Properties (ChangeTracking Page) | Microsoft Docs"
ms.custom: ""
ms.date: "01/07/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.databaseproperties.changetracking.f1"
ms.assetid: 9b929640-bc62-449b-9b06-b5a77b8cf372
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Database Properties (ChangeTracking Page)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  Use this page to view or modify change tracking settings for the selected database. For more information about the options available on this page, see [Enable and Disable Change Tracking &#40;SQL Server&#41;](../../relational-databases/track-changes/enable-and-disable-change-tracking-sql-server.md).  
  
## Options  
 **Change Tracking**  
 Use to enable or disable change tracking for the database.  
  
 To enable change tracking, you must have permission to modify the database.  
  
 Setting the value to **True** sets a database option that allows change tracking to be enabled on individual tables.  
  
 You can also configure change tracking by using [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md).  
  
 **Retention Period**  
 Specifies the minimum period for keeping change track information in the database. Data is removed only if the **Auto Clean-Up** value is **True**.  
  
 The default value is 2.  
  
 **Retention Period Units**  
 Specifies the units for the Retention Period value. You can select **Days**, **Hours**, or **Minutes**. The default value is **Days**.  
  
 The minimum retention period is 1 minute. There is no maximum retention period.  
  
 **Auto Clean-Up**  
 Indicates whether change tracking information is automatically removed after the specified retention period.  
  
 Enabling **Auto Clean-Up** resets any previous custom retention period to the default retention period of 2 days.  
  
## See Also  
 [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)   
 [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](../../t-sql/statements/create-database-sql-server-transact-sql.md)  
  
  
