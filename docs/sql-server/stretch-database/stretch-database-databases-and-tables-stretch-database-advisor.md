---
title: "Stretch Database databases and tables - Stretch Database Advisor | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "06/14/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-stretch"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Stretch Database, identifying databases"
  - "Stretch Database, identifying tables"
  - "identifying databases for Stretch Database"
  - "identifying tables for Stretch Database"
ms.assetid: 81bd93d8-eef8-4572-88d7-5c37ab5ac2bf
caps.latest.revision: 29
author: "douglaslMS"
ms.author: "douglasl"
manager: "craigg"
---
# Stretch Database databases and tables - Stretch Database Advisor
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

  To identify databases and tables that are candidates for Stretch Database, download SQL Server 2016 Upgrade Advisor and run the Stretch Database Advisor. Stretch Database Advisor also identifies blocking issues.  
  
## Download and install Upgrade Advisor  
 Download and install Upgrade Advisor from [here](https://www.microsoft.com/en-us/download/details.aspx?id=53595). This tool is not included on the [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] installation media.  
  
## Run the Stretch Database Advisor  
  
1.  Run Upgrade Advisor.  
  
2.  Select **Scenarios**, and then select **RUN STRETCH DATABASE ADVISOR**.  
  
3.  On the **Run Stretch Database Advisor** blade, click **SELECT DATABASES TO ANALYZE**.  
  
4.  On the **Select databases** blade, enter or select the server name and the authentication info. Click **Connect**.

5.  A list of databases on the selected server appears. Select the databases that you want to analyze. Click **Select**.  
  
6.  On the **Run Stretch Database Advisor** blade, click **Run**.  The analysis runs.  
  
## Review the results  
  
1.  When the analysis is finished, on the **Analyzed databases** blade, select one of the databases that you analyzed to display the **Analysis results** blade.  
  
     The **Analysis results** blade lists recommended tables in the selected database that match the default recommendation criteria. 
  
2.  In the list of tables on the **Analysis results** blade, select one of the recommended tables to display the **Table results** blade.  
  
     If there are blocking issues, the **Table results** blade lists the blocking issues for the selected table. For information about blocking issues detected by Stretch Database Advisor, see [Limitations for Stretch Database](../../sql-server/stretch-database/limitations-for-stretch-database.md).  
  
3.  In the list of blocking issues on the **Table results** blade, select one of the issues to display more info about the selected issue and proposes mitigation steps. Implement the suggested mitigation steps if you want to configure the selected table for Stretch Database.  
  
## Next step  
 Enable Stretch Database.  
  
-   To enable Stretch Database on a **database**, see [Enable Stretch Database for a database](../../sql-server/stretch-database/enable-stretch-database-for-a-database.md).  
  
-   To enable Stretch Database on another **table**, when Stretch is already enabled on the database, see [Enable Stretch Database for a table](../../sql-server/stretch-database/enable-stretch-database-for-a-table.md). 
  
## See Also  
 [Limitations for Stretch Database](../../sql-server/stretch-database/limitations-for-stretch-database.md)   
 [Enable Stretch Database for a database](../../sql-server/stretch-database/enable-stretch-database-for-a-database.md)   
 [Enable Stretch Database for a table](../../sql-server/stretch-database/enable-stretch-database-for-a-table.md)  
  
  
