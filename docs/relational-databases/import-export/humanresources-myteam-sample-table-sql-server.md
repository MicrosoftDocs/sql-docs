---
title: "HumanResources.myTeam Sample Table (SQL Server)"
description: To run code examples for importing and exporting bulk data in SQL Server, you need to create a test table named myTeam in the HumanResources schema.
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: data-movement
ms.topic: conceptual
helpviewer_keywords:
  - "myTeam sample table [SQL Server]"
  - "bulk importing [SQL Server], examples"
  - "bulk exporting [SQL Server], examples"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# HumanResources.myTeam Sample Table (SQL Server)
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]
  Many of the code examples in [Importing and Exporting Bulk Data](../../relational-databases/import-export/bulk-import-and-export-of-data-sql-server.md) require a special-purpose test table named **myTeam**. Before you can run the examples, you must create the **myTeam** table in the **HumanResources** schema of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.  
  
> [!NOTE]  
>  [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] is one of the sample databases in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)].  
  
 The **myTeam** table contains the following columns.  
  
|Column|Data type|Nullability|Description|  
|------------|---------------|-----------------|-----------------|  
|**EmployeeID**|**smallint**|Not null|Primary key for the rows. Employee ID of a member of my team.|  
|**Name**|**nvarchar(50)**|Not null|Name of a member of my team.|  
|**Title**|**nvarchar(50)**|Nullable|Title the employee performs on my team.|  
|**Background**|**nvarchar(50)**|Not null|Date and time the row was last updated. (Default)|  
  
**To create HumanResources.myTeam**  
  
-   Use the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statements:  
  
    ```sql
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
  
    ```sql
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
 [Bulk Import and Export of Data &#40;SQL Server&#41;](../../relational-databases/import-export/bulk-import-and-export-of-data-sql-server.md)  
  
  
