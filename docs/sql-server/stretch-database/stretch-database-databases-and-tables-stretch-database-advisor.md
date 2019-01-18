---
title: "Identify databases and tables for Stretch Database | Microsoft Docs"
ms.custom: ""
ms.date: "10/30/2017"
ms.prod: sql
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "Stretch Database, identifying databases"
  - "Stretch Database, identifying tables"
  - "identifying databases for Stretch Database"
  - "identifying tables for Stretch Database"
ms.assetid: 81bd93d8-eef8-4572-88d7-5c37ab5ac2bf
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Identify databases and tables for Stretch Database with Data Migration Assistant
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md-winonly](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md-winonly.md)]


  To identify databases and tables that are candidates for Stretch Database, along with potential blocking issues, download and run Microsoft Data Migration Assistant.
  
## Get Data Migration Assistant
 Download and install Data Migration Assistant from [here](https://www.microsoft.com/download/details.aspx?id=53595). This tool is not included on the SQL Server installation media.  
  
## Run Data Migration Assistant  
  
1.  Run Microsoft Data Migration Assistant.  

2.  Create a new project of type **Assessment** and give it a name.

3.  Select **SQL Server** as both the **Source server type** and the **Target server type**.

4.  Select **Create**. 

5. On the **Options** page (step 1), select **New features recommendation**. Optionally, clear the selection for **Compatibility issues**.

6.  On the **Select sources** page (step 2), connect to a server, select a database, and then select **Add**.

7.  Select **Start Assessment**.

## Review the results  
  
1.  When the analysis is finished, on the **Review results** page (step 3), select the **Feature recommendations** option, and then select the **Storage** tab.

2.  Review the recommendations related to Stretch Database. Each recommendation lists the tables for which Stretch Database may be appropriate, along with any potential blocking issues.

## Historical note
Stretch Database Advisor was previously a component of SQL Server 2016 Upgrade Advisor. At that time, you had to select and run Stretch Database Advisor as a separate action.

With the release of Data Migration Assistant, which replaces and extends Upgrade Advisor, the functionality of Stretch Database Advisor is incorporated into this new tool. You don't have to select any options to get recommendations related to Stretch Database. When you run an Assessment in Data Migration Assistant, the results related to Stretch Database appear on the **Storage** tab of the **Feature recommendations**.
  
## Next step  
 Enable Stretch Database.  
  
-   To enable Stretch Database on a **database**, see [Enable Stretch Database for a database](../../sql-server/stretch-database/enable-stretch-database-for-a-database.md).  
  
-   To enable Stretch Database on another **table**, when Stretch is already enabled on the database, see [Enable Stretch Database for a table](../../sql-server/stretch-database/enable-stretch-database-for-a-table.md). 
  
## See Also  
 [Limitations for Stretch Database](../../sql-server/stretch-database/limitations-for-stretch-database.md)   
 [Enable Stretch Database for a database](../../sql-server/stretch-database/enable-stretch-database-for-a-database.md)   
 [Enable Stretch Database for a table](../../sql-server/stretch-database/enable-stretch-database-for-a-table.md)  
  
  
