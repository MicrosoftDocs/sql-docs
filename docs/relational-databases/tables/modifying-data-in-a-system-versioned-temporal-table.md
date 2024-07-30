---
title: Modify data in a system-versioned temporal table
description: Data in a system-versioned temporal table is modified using regular DML statements, excluding period column data.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/29/2024
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Modify data in a system-versioned temporal table

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

Data in a system-versioned temporal table is modified using regular data manipulation language (DML) statements, with one important difference: period column data can't be directly modified. When data is updated, it is versioned, and the previous version of each updated row is inserted into the history table. When data is deleted, the delete is logical, and the row moved into the history table from the current table; the data isn't permanently deleted.

## Insert data

When you insert new data, you need to account for the `PERIOD` columns if they aren't `HIDDEN`. You can also use partition switching with temporal tables.

### Insert new data with visible period columns

You can construct your `INSERT` statement when you have visible `PERIOD` columns as follows, to account for the `PERIOD` columns:

If you specify the column list in your `INSERT` statement, you can omit the `PERIOD` columns because the system generates values for these columns automatically.

```sql
-- Insert with column list and without period columns
INSERT INTO [dbo].[Department] (
      [DeptID],
      [DeptName],
      [ManagerID],
      [ParentDeptID]
)
VALUES (10, 'Marketing', 101, 1);
```

If you do specify the `PERIOD` columns in the column list in your `INSERT` statement, then you need to specify `DEFAULT` as their value.

```sql
INSERT INTO [dbo].[Department] (
   DeptID,
   DeptName,
   ManagerID,
   ParentDeptID,
   ValidFrom,
   ValidTo
)
VALUES (11, 'Sales', 101, 1, DEFAULT, DEFAULT);
```

If you don't specify the column list in your `INSERT` statement, specify `DEFAULT` for `PERIOD` columns.

```sql
-- Insert without a column list and DEFAULT values for period columns
INSERT INTO [dbo].[Department]
VALUES(12, 'Production', 101, 1, DEFAULT, DEFAULT);
```

### Insert data into a table with HIDDEN period columns

If `PERIOD` columns are specified as `HIDDEN`, you don't need to account for the `PERIOD` columns in your `INSERT` statement. This behavior guarantees that your legacy applications continue to work when you enable system-versioning on tables that benefit from versioning.

```sql
CREATE TABLE [dbo].[CompanyLocation] (
    [LocID] [int] IDENTITY(1, 1) NOT NULL PRIMARY KEY,
    [LocName] [varchar](50) NOT NULL,
    [City] [varchar](50) NOT NULL,
    [ValidFrom] [datetime2] GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
    [ValidTo] [datetime2] GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
    PERIOD FOR SYSTEM_TIME([ValidFrom], [ValidTo])
)
WITH (SYSTEM_VERSIONING = ON);
GO

INSERT INTO [dbo].[CompanyLocation]
VALUES ('Headquarters', 'New York');
```

### Insert data using PARTITION SWITCH

If the current table is partitioned, you can use `PARTITION SWITCH` as an efficient mechanism to load data into an empty partition, or to load into multiple partitions in parallel.

The staging table that is used in the `PARTITION SWITCH IN` statement with a temporal table must have `SYSTEM_TIME PERIOD` defined, but it doesn't need to be a temporal table. This ensures that temporal consistency checks are performed during the data insert into a staging table or when `SYSTEM_TIME` period is added to a prepopulated staging table.

```sql
/* Create staging table with period definition for SWITCH IN temporal table */
CREATE TABLE [dbo].[Staging_Department_Partition2] (
    [DeptID] [int] NOT NULL,
    [DeptName] [varchar](50) NOT NULL,
    [ManagerID] [int] NULL,
    [ParentDeptID] [int] NULL,
    [ValidFrom] [datetime2] GENERATED ALWAYS AS ROW START NOT NULL,
    [ValidTo] [datetime2] GENERATED ALWAYS AS ROW END NOT NULL,
    PERIOD FOR SYSTEM_TIME([ValidFrom], [ValidTo])
) ON [PRIMARY]

/* Create aligned primary key */
ALTER TABLE [dbo].[Staging_Department_Partition2]
ADD CONSTRAINT [Staging_Department_Partition2_PK]
PRIMARY KEY CLUSTERED ([DeptID] ASC) ON [PRIMARY];

/*
Create and enforce constraints for partition boundaries.
Partition 2 contains rows with DeptID > 100 and DeptID <=200
*/
ALTER TABLE [dbo].[Staging_Department_Partition2]
WITH CHECK ADD CONSTRAINT [chk_staging_Department_partition_2] CHECK (
   [DeptID] > N'100'
   AND [DeptID] <= N'200'
);

ALTER TABLE [dbo].[Staging_Department_Partition2]
CHECK CONSTRAINT [chk_staging_Department_partition_2];

/*Load data into staging table*/
INSERT INTO [dbo].[staging_Department] (
    [DeptID],
    [DeptName],
    [ManagerID],
    [ParentDeptID]
    )
VALUES (101, 'D101', 1, NULL);

/*Use PARTITION SWITCH IN to efficiently add data to current table */
ALTER TABLE [Staging_Department]
SWITCH TO [dbo].[Department] PARTITION 2;
```

If you try to perform `PARTITION SWITCH` from a table without a period definition, you get an error message:

```output
Msg 13577, Level 16, State 1, Line 25 ALTER TABLE SWITCH statement failed on table 'MyDB.dbo.Staging_Department_2015_09_26' because target table has SYSTEM_TIME PERIOD while source table does not have it.
```

## Update data

You update data in the current table with a regular `UPDATE` statement. You can update data in the current table from the history table for the disaster scenario. However, you can't update `PERIOD` columns and you can't directly update data in the history table while `SYSTEM_VERSIONING = ON`.

If you set `SYSTEM_VERSIONING` to `OFF` and update rows from the current and history tables, the system doesn't preserve the history of changes.

### Update the current table

In this example, the `ManagerID` column is updated for each row where the `DeptID` is `10`. The `PERIOD` columns aren't referenced in any way.

```sql
UPDATE [dbo].[Department]
SET [ManagerID] = 501
WHERE [DeptID] = 10;
```

However, you can't update a `PERIOD` column, and you can't update the history table. In this example, an attempt to update a `PERIOD` column generates an error.

```sql
UPDATE [dbo].[Department]
SET ValidFrom = '2015-09-23 23:48:31.2990175'
WHERE DeptID = 10;
```

The statement generates the following error.

```output
Msg 13537, Level 16, State 1, Line 3
Cannot update GENERATED ALWAYS columns in table 'TmpDev.dbo.Department'.
```

### Update the current table from the history table

You can use `UPDATE` on the current table to revert the actual row state to a valid state at a specific point in time in the past. Think of this as reverting to a *last good known row version*. The following example shows reverting to the values in the history table as of April 25, 2015, where the `DeptID` is `10`.

```sql
UPDATE Department
SET DeptName = History.DeptName
FROM Department
FOR SYSTEM_TIME AS OF '2015-04-25' AS History
WHERE History.DeptID = 10
    AND Department.DeptID = 10;
```

## Delete data

You delete data in the current table with a regular `DELETE` statement. The end period column for deleted rows is populated with the begin time of the underlying transaction. You can't directly delete rows from the history table while `SYSTEM_VERSIONING` is `ON`. If you set `SYSTEM_VERSIONING = OFF` and delete rows from the current and history tables, the system doesn't preserve the history of changes.

The following statements aren't supported while `SYSTEM_VERSIONING = ON`:

- `TRUNCATE`
- `SWITCH PARTITION OUT` for the current table
- `SWITCH PARTITION IN` for the history table

## Use MERGE to modify data in temporal table

The `MERGE` operation is supported with the same limitations that `INSERT` and `UPDATE` statements have, regarding `PERIOD` columns.

```sql
CREATE TABLE DepartmentStaging (
    DeptId INT,
    DeptName VARCHAR(50)
);
GO

INSERT INTO DepartmentStaging
VALUES (1, 'Company Management');

INSERT INTO DepartmentStaging
VALUES (10, 'Science & Research');

INSERT INTO DepartmentStaging
VALUES (15, 'Process Management');

MERGE dbo.Department AS target
USING (
    SELECT DeptId, DeptName
    FROM DepartmentStaging
    ) AS source(DeptId, DeptName)
    ON (target.DeptId = source.DeptId)
WHEN MATCHED
    THEN UPDATE SET DeptName = source.DeptName
WHEN NOT MATCHED
    THEN
        INSERT (DeptId, DeptName)
        VALUES (source.DeptId, source.DeptName);
```

## Related content

- [Temporal tables](temporal-tables.md)
- [Create a system-versioned temporal table](creating-a-system-versioned-temporal-table.md)
- [Query data in a system-versioned temporal table](querying-data-in-a-system-versioned-temporal-table.md)
- [Change the schema of a system-versioned temporal table](changing-the-schema-of-a-system-versioned-temporal-table.md)
- [Stop system-versioning on a system-versioned temporal table](stopping-system-versioning-on-a-system-versioned-temporal-table.md)
