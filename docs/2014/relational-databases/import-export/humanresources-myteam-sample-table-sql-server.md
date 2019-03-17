---
title: "HumanResources.myTeam Sample Table (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: data-movement
ms.topic: conceptual
helpviewer_keywords: 
  - "myTeam sample table [SQL Server]"
  - "bulk importing [SQL Server], examples"
  - "bulk exporting [SQL Server], examples"
ms.assetid: 27da45a0-c1f4-4bf4-ab24-6196e80d3834
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# HumanResources.myTeam Sample Table (SQL Server)
  Many of the code examples in [Importing and Exporting Bulk Data](bulk-import-and-export-of-data-sql-server.md) require a special-purpose test table named **myTeam**. Before you can run the examples, you must create the **myTeam** table in the **HumanResources** schema of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.  
  
> [!NOTE]  
>  [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] is one of the sample databases in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 The **myTeam** table is contains the following columns.  
  
|Column|Data type|Nullability|Description|  
|------------|---------------|-----------------|-----------------|  
|**EmployeeID**|`smallint`|Not null|Primary key for the rows. Employee ID of a member of my team.|  
|**Name**|`nvarchar(50)`|Not null|Name of a member of my team.|  
|**Title**|`nvarchar(50)`|Nullable|Title the employee performs on my team.|  
|**Background**|`nvarchar(50)`|Not null|Date and time the row was last updated. (Default)|  
  
 **To create HumanResources.myTeam**  
  
-   Use the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statements:  
  
    ```  
    --Create HumanResources.MyTeam:   
    USE AdventureWorks;  
    GO  
    CREATE TABLE HumanResources.myTeam   
    (EmployeeID smallint NOT NULL,  
    Name nvarchar(50) NOT NULL,  
    Title nvarchar(50) NULL,  
    Background nvarchar(50) NOT NULL DEFAULT ''  
    );  
    GO  
    ```  
  
 **To populate HumanResources.myTeam**  
  
-   Execute following `INSERT` statements to populate the table with two rows:  
  
    ```  
    USE AdventureWorks;  
    GO  
    INSERT INTO HumanResources.myTeam(EmployeeID,Name,Title,Background)  
       VALUES(77,'Mia Doppleganger','Administrative Assistant','Microsoft Office');  
    GO  
    INSERT INTO HumanResources.myTeam(EmployeeID,Name,Title,Background)  
       VALUES(49,'Hirum Mollicat','I.T. Specialist','Report Writing and Data Mining');  
    GO  
    ```  
  
    > [!NOTE]  
    >  These statements skip the fourth column, `Background`. This has a default value. Skipping this column causes this `INSERT` statement to leave this column blank.  
  
## See Also  
 [Bulk Import and Export of Data &#40;SQL Server&#41;](bulk-import-and-export-of-data-sql-server.md)  
  
  
