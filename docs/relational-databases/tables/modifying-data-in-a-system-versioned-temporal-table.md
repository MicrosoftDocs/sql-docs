---
title: "Modifying Data in a System-Versioned Temporal Table | Microsoft Docs"
ms.custom: ""
ms.date: "03/28/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
ms.assetid: 5f398470-c531-47b5-84d5-7c67c27df6e5
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Modifying Data in a System-Versioned Temporal Table
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

  Data in a system-versioned temporal table is modified using regular DML statements with one important difference: period column data cannot be directly modified. When data is updated, it is versioned, with the previous version of each updated row is inserted into the history table. When data is deleted, the delete is logical, with the row moved into the history table from the current table - it is not permanently deleted.  
  
## Inserting data  
 When you insert new data, you need to account for the **PERIOD** columns if they are not **HIDDEN**. You can also use partition switching with system-versioned temporal tables.  
  
### Insert new data with visible period columns  
 You can construct your **INSERT** statement when you have visible **PERIOD** columns as follows to account for the new **PERIOD** columns:  
  
-   If you specify the column list in your **INSERT** statement, you can omit the **PERIOD** columns because system will generate values for these columns automatically.  
  
    ```  
    --Insert with column list and without period columns   
    INSERT INTO [dbo].[Department] ([DeptID] ,[DeptName] ,[ManagerID] ,[ParentDeptID])   
    VALUES(10, 'Marketing', 101, 1);  
  
    ```  
  
-   If you do specify the**PERIOD** columns in the column list in your **INSERT** statement, then you need to specify **DEFAULT** as their value.  
  
    ```  
    INSERT INTO [dbo].[Department] ([DeptID] ,[DeptName] ,[ManagerID] ,[ParentDeptID], SysStartTime, SysEndTime)   
    VALUES(11, 'Sales', 101, 1, default, default);  
  
    ```  
  
-   If you do not specify the column list in your **INSERT** statement, specify **DEFAULT** for **PERIOD** columns.  
  
    ```  
    --Insert without  column list and DEFAULT values for period columns   
    INSERT INTO [dbo].[Department]    
    VALUES(12, 'Production', 101, 1, default, default);  
  
    ```  
  
### Insert data into a table with HIDDEN period columns  
 If **PERIOD** columns are specified as HIDDEN, then you need only to specify the values for the visible columns when you use INSERT without specifying the column list. You do not need to account for the new **PERIOD** columns in your **INSERT** statement. This behavior guarantees that your legacy applications will continue to work when you enable system-versioning on tables that will benefit from versioning.  
  
```  
CREATE TABLE [dbo].[CompanyLocation]  
(   
     [LocID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY  
   , [LocName] [varchar](50) NOT NULL  
   , [City] [varchar](50) NOT NULL  
   , [SysStartTime] [datetime2](0) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL   
   , [SysEndTime] [datetime2](0) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL   
   , PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime])   
)    
WITH ( SYSTEM_VERSIONING = ON );   
GO   
INSERT INTO [dbo].[CompanyLocation]   
VALUES  ('Headquarters', 'New York');  
  
```  
  
### Inserting data using PARTITION SWITCH  
 If the current table is partitioned, you can use partition switch as an efficient mechanism to load data into an empty partition or to load into multiple partitions in parallel.   
The staging table that is used in the **PARTITION SWITCH IN** statement with a system-versioned temporal table must have **SYSTEM_TIME PERIOD** defined, but it does not need to be a system-versioned temporal table.    
This ensures that temporal consistency checks are performed during the data insert into a staging table or when SYSTEM_TIME period is added to a pre-populated staging table.  
  
```  
/*Create staging table with period definition for SWITCH IN temporal table*/   
CREATE TABLE [dbo].[Staging_Department_Partition2]  
(   
     [DeptID] [int] NOT NULL  
   , [DeptName] [varchar](50)  NOT NULL  
   , [ManagerID] [int] NULL  
   , [ParentDeptID] [int] NULL  
   , [SysStartTime] [datetime2](7) GENERATED ALWAYS AS ROW START NOT NULL  
   , [SysEndTime] [datetime2](7) GENERATED ALWAYS AS ROW END NOT NULL  
   , PERIOD FOR SYSTEM_TIME ( [SysStartTime], [SysEndTime] )   
) ON [PRIMARY]   
  
/*Create aligned primary key*/   
ALTER TABLE [dbo].[Staging_Department_Partition2]    
ADD CONSTRAINT [Staging_Department_Partition2_PK]  
   PRIMARY KEY CLUSTERED  
   (  [DeptID] ASC )     
   ON [PRIMARY]   
  
/*   
Create and enforce constraints for partition boundaries.   
Partition 2 contains rows with DeptID > 100 and DeptID <=200   
*/   
ALTER TABLE [dbo].[Staging_Department_Partition2]      
   WITH CHECK ADD  CONSTRAINT [chk_staging_Department_partition_2]     
   CHECK  ([DeptID]>N'100' AND [DeptID]<=N'200')   
ALTER TABLE [dbo].[Staging_Department_Partition2]    
   CHECK CONSTRAINT [chk_staging_Department_partition_2]   
  
/*Load data into staging table*/   
INSERT INTO [dbo].[staging_Department] ([DeptID],[DeptName],[ManagerID],[ParentDeptID])   
VALUES (101,'D101',1,NULL)  
  
/*Use PARTITION SWITCH IN to efficiently add data to current table */    
ALTER TABLE [Staging_Department]    
SWITCH TO [dbo].[Department] PARTITION 2;  
  
```  
  
 If you try to perform PARTITION SWITCH from a table without period definition you'll get error message: `Msg 13577, Level 16, State 1, Line 25    ALTER TABLE SWITCH statement failed on table 'MyDB.dbo.Staging_Department_2015_09_26' because target table has SYSTEM_TIME PERIOD while source table does not have it.`  
  
## Updating data  
 You update data in the current table with a regular **UPDATE** statement. You can update data in the current table from the history table to for the "oops" scenario. However, you cannot update **PERIOD** columns and you cannot directly updated data in the history table while **SYSTEM_VERSIONING = ON**.   
Set **SYSTEM_VERSIONING = OFF** and update rows from current and history table but keep in mind that way system will not preserve history of changes.  
  
### Updating the current table  
 In this example, the ManagerID column is updated for each row where the DeptID = 10. The **PERIOD** columns are not referenced in any way.  
  
```  
UPDATE [dbo].[Department] SET [ManagerID] = 501 WHERE [DeptID] = 10  
```  
  
 However, you cannot update a **PERIOD** column and you cannot update the history table.  In this example, an attempt to update a **PERIOD** column generates an error.  
  
```  
UPDATE [dbo].[Department]    
SET SysStartTime = '2015-09-23 23:48:31.2990175'    
WHERE DeptID = 10 ;  
  
Msg 13537, Level 16, State 1, Line 3   
Cannot update GENERATED ALWAYS columns in table 'TmpDev.dbo.Department'.  
  
```  
  
### Updating the current table from the history table  
 You can use **UPDATE** on the current table to revert the actual row state to valid state at a specific point in time in the past (reverting to a "last good known row version"). The following example shows reverting to the values in the history table as of 2015-04-25 where the DeptID = 10.  
  
```  
UPDATE Department   
SET DeptName = History.DeptName   
FROM Department    
FOR SYSTEM_TIME AS OF '2015-04-25' AS History   
WHERE History.DeptID  = 10   
AND Department.DeptID = 10 ;  
  
```  
  
## Deleting data  
 You delete data in the current table with a regular **DELETE** statement. The end period column for deleted rows will be populated with the begin time of underlying transaction.   
You cannot directly delete rows from history table while **SYSTEM_VERSIONING = ON**.   
Set **SYSTEM_VERSIONING = OFF** and delete rows from current and history table but keep in mind that way system will not preserve history of changes.   
**TRUNCATE**, **SWITCH PARTITION OUT** of current table and **SWITCH PARTITION IN** history table are not supported while **SYSTEM_VERSIONING = ON**.  
  
## Using MERGE to modify data in temporal table  
 **MERGE** operation is supported with the same limitations that **INSERT** and **UPDATE** statements have regarding **PERIOD** columns.  
  
```  
CREATE TABLE DepartmentStaging (DeptId INT, DeptName varchar(50));   
GO   
INSERT INTO DepartmentStaging VALUES (1, 'Company Management');   
INSERT INTO DepartmentStaging VALUES (10, 'Science & Research');   
INSERT INTO DepartmentStaging VALUES (15, 'Process Management');   
  
MERGE dbo.Department AS target   
USING (SELECT DeptId, DeptName FROM DepartmentStaging) AS source (DeptId, DeptName)   
ON (target.DeptId = source.DeptId)   
WHEN MATCHED THEN    
    UPDATE   
   SET DeptName = source.DeptName   
WHEN NOT MATCHED THEN   
   INSERT (DeptName)   
   VALUES (source.DeptName);  
  
```  
  
## See Also  
 [Temporal Tables](../../relational-databases/tables/temporal-tables.md)   
 [Creating a System-Versioned Temporal Table](../../relational-databases/tables/creating-a-system-versioned-temporal-table.md)   
 [Querying Data in a System-Versioned Temporal Table](../../relational-databases/tables/querying-data-in-a-system-versioned-temporal-table.md)   
 [Changing the Schema of a System-Versioned Temporal Table](../../relational-databases/tables/changing-the-schema-of-a-system-versioned-temporal-table.md)   
 [Stopping System-Versioning on a System-Versioned Temporal Table](../../relational-databases/tables/stopping-system-versioning-on-a-system-versioned-temporal-table.md)  
  
  
