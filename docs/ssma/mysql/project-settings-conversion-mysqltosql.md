---
title: "Project Settings (Conversion) (MySQLToSQL) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
ms.assetid: 7ad5fe44-6445-4ba8-a457-5af792631f11
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Project Settings (Conversion) (MySQLToSQL)
The Conversion page of the **Project Settings** dialog box contains settings that customize how SSMA converts MySQL syntax to SQL Server or SQL Azure syntax.  
  
The Conversion pane is available in the **Project Settings** and **Default Project Settings** dialog boxes.  
  
-   Use the **Default Project Settings** dialog box to set configuration options for all projects. To access the conversion settings, on the **Tools** menu, select **Default Project Settings**, select migration project type for which settings are required to be viewed /changed from **Migration Target Version** drop down, click **General** at the bottom of the left pane, and then select **Conversion**.  
  
-   To specify settings for the current project, on the **Tools** menu click **Project Settings**, then click **General** at the bottom of the left pane, and then click **Conversion**.  
  
## Options  
  
### Collate Clause  
  
|||  
|-|-|  
|**Term**|**Definition**|  
|**Explicit COLLATE clause conversion**|Explicit COLLATE clause conversion option specifies how to convert explicit COLLATE clauses in MySQL code. Possible Choices: Ignore and Mark with a Warning / Generate an Error<br /><br />**Default Mode**:  Ignore and Mark with a Warning<br /><br />**Optimistic Mode**:  Ignore and Mark with a Warning<br /><br />**Full Mode**:  Ignore and Mark with a Warning|  
  
### Column Constraints  
  
|||  
|-|-|  
|**Term**|**Definition**|  
|**Generate Constraint for columns of ENUM data type**|Generates constraint for columns of ENUM data type in the SQL Server or SQL Azure table, if it is not present in the MySQL table. If yes, all converted columns of ENUM data type will be accompanied with CHECK constraint controlling the value.<br /><br />**Default Mode**:   No<br /><br />**Optimistic Mode**:  No<br /><br />**Full Mode**:   Yes|  
|**Generate Constraint for columns of SET data type**|Generates constraint for columns of SET data type in the SQL Server or SQL Azure table, if it is not present in the MySQL table. If yes, all converted columns of SET data type will be accompanied with CHECK constraint controlling the value.<br /><br />**Default Mode**:   No<br /><br />**Optimistic Mode**:  No<br /><br />**Full Mode**:   Yes|  
|**Generate Constraint for columns of UNSIGNED numeric data type columns**|Add CHECK for non-negative value to columns of UNSIGNED numeric data types.<br /><br />**Default Mode**:   No<br /><br />**Optimistic Mode**:  No<br /><br />**Full Mode**:   Yes|  
|**Generate Constraint for YEAR data type columns**|Generates constraint for YEAR data type columns in the SQL Server or SQL Azure table, if it is not present in the MySQL table. If yes, all converted columns of YEAR data type will be accompanied with CHECK constraint controlling the value.<br /><br />**Default Mode**:   No<br /><br />**Optimistic Mode**:  No<br /><br />**Full Mode**:   Yes|  
  
### Data Types  
  
|||  
|-|-|  
|**Term**|**Definition**|  
|**ENUM data type conversion**|Specifies how MySQL ENUM data type should be converted either as Convert to NVARCHAR or Convert to Numeric<br /><br />**Default Mode**:  Convert to NVARCHAR<br /><br />**Optimistic Mode**:  Convert to NVARCHAR<br /><br />**Full Mode**:  Convert to NVARCHAR|  
|**SET data type conversion**|Specifies how MySQL SET data type should be converted, Convert to NVARCHAR(L)/Convert to BINARY(L)<br /><br />**Default Mode**: Convert to NVARCHAR(L)<br /><br />**Optimistic Mode**: Convert to NVARCHAR(L)<br /><br />**Full Mode**: Convert to NVARCHAR(L)|  
  
### Generic  
  
|||  
|-|-|  
|**Term**|**Definition**|  
|**Columns without DEFAULT value in INSERT and REPLACE**|If 'Yes', all the statements that reference tables using stored engines other than MyISAM and InnoDb should be marked with warning conversion messages.<br /><br />**Default Mode**:  Add to Column list<br /><br />**Optimistic Mode**:  Add to Column list<br /><br />**Full Mode**:   Add to Column list|  
|**Division by Zero Conversion Produces**|Specifies whether or not to emulate MySQL without ERROR_FOR_DIVISION_BY_ZERO behavior.<br /><br />**Default Mode**:   Error<br /><br />**Optimistic Mode**:  Error<br /><br />**Full Mode**:   NULL|  
|**IN operator**|Specifies how to convert MySQL IN operator.<br /><br />**Default Mode**:   Always convert to IN<br /><br />**Optimistic Mode**:  Always convert to IN<br /><br />**Full Mode**:   Expand if necessary|  
|**MySQL Function Conversion**|Specifies how to convert MySQL standard functions.<br /><br />**Default Mode**:   Optimistic<br /><br />**Optimistic Mode**:  Optimistic<br /><br />**Full Mode**:   Precise|  
|**Not supported storage engines**|If 'Yes', all the statements that reference tables using stored engines other than MyISAM and InnoDb should be marked with warning conversion messages.<br /><br />**Default Mode**:   No<br /><br />**Optimistic Mode**:  No<br /><br />**Full Mode**:   Yes|  
|**Suppress ROWID auxiliary column generation**|If Yes, prohibits creation of ROWD auxiliary column creation on target tables. May affect migration of some structures.<br /><br />**Default Mode**:   No<br /><br />**Optimistic Mode**:  No<br /><br />**Full Mode**:   No|  
|**TRUNCATE statement conversion**|Specifies how to convert TRUNCATE statements.<br /><br />**Default Mode**:   TRUNCATE<br /><br />**Optimistic Mode**:  TRUNCATE<br /><br />**Full Mode**:   TRUNCATE|  
  
### Misc  
  
|||  
|-|-|  
|**Term**|**Definition**|  
|**Default Schema Mapping**|Specifies how to map MySQL databases into SQL Server schemas.<br /><br />**Default Mode**:  Database to Database<br /><br />**Optimistic Mode**:  Database to Database<br /><br />**Full Mode**:  Database to Database|  
  
### Procedures and Functions  
  
|||  
|-|-|  
|**Term**|**Definition**|  
|**Default Function conversion**|Specifies if functions should be by default be converted to T-SQL functions or to stored procedures.<br /><br />**Default Mode**:  Convert to Function<br /><br />**Optimistic Mode**:  Convert to Function<br /><br />**Full Mode**:  Convert to Function|  
|**Generate SET XACT_ABORT ON**|Specifies whether or not SET XACT_ABORT ON needs to be added to the beginning of the converted procedure or trigger.<br /><br />**Default Mode**:  Yes<br /><br />**Optimistic Mode**:  Yes<br /><br />**Full Mode**:  Yes|  
|**Generate SET NOCOUNT ON**|Specifies whether or not SET NOCOUNT ON needs to be added to the beginning of the converted procedure or trigger.<br /><br />**Default Mode**:  Yes<br /><br />**Optimistic Mode**:  Yes<br /><br />**Full Mode**:  Yes|  
  
### Spatial Data Types  
  
|||  
|-|-|  
|**Term**|**Definition**|  
|**Default bounding box {XMAX&#124;XMIN&#124;YMAX&#124;YMIN} for spatial indexes**|Defines default value for {XMAX&#124;XMIN&#124;YMAX&#124;YMIN} parameter of bounding box used in spatial indexes.<br /><br />**Default Mode**<br /><br />XMAX: 100<br /><br />XMIN: 0<br /><br />YMAX: 100<br /><br />YMIN: 0<br /><br />**Optimistic Mode**<br /><br />XMAX: 100<br /><br />XMIN: 0<br /><br />YMAX:  100<br /><br />YMIN: 0<br /><br />**Full Mode**<br /><br />XMAX: 100<br /><br />XMIN: 0<br /><br />YMAX: 100<br /><br />YMIN: 0|  
|**Default grid density for spatial indexes**|Defines default value for LEVEL_1, LEVEL_2, LEVEL_3, and LEVEL_4 of grid density used in spatial indexes.<br /><br />**Default Mode**<br /><br />LEVEL_1: Default<br /><br />LEVEL_2: Default<br /><br />LEVEL_3: Default<br /><br />LEVEL_4: Default<br /><br />**Optimistic Mode**<br /><br />LEVEL_1: Default<br /><br />LEVEL_2: Default<br /><br />LEVEL_3: Default<br /><br />LEVEL_4: Default<br /><br />**Full Mode**<br /><br />LEVEL_1: Default<br /><br />LEVEL_2: Default<br /><br />LEVEL_3: Default<br /><br />LEVEL_4: Default|  
  
### Transactions  
  
|||  
|-|-|  
|**Term**|**Definition**|  
|**Non-transactional tables**|Specifies whether or not all references to table that do not support transactions should be marked with warning conversion messages.<br /><br />**Default Mode**: No<br /><br />**Optimistic Mode**: No<br /><br />**Full Mode**: Yes|  
|**Transaction isolation level**|Specifies what transaction isolation level should be used for new transactions.<br /><br />**Default Mode**:   Default<br /><br />**Optimistic Mode**:  Default<br /><br />**Full Mode**:   Repeatable read|  
  
### Value Control  
  
|||  
|-|-|  
|**Term**|**Definition**|  
|**Character to Numeric conversion**|Specifes how to handle implicit and explicit conversion from Character data type to numeric data types.<br /><br />**Default Mode**:   Optimistic<br /><br />**Optimistic Mode**:  Optimistic<br /><br />**Full Mode**:   Precise|  
|**Control UNSIGNED numeric values**|Control assigning values to UNSIGNED numeric variables and parameters.<br /><br />**Default Mode**:   No<br /><br />**Optimistic Mode**:  No<br /><br />**Full Mode**:   Yes|  
|**Control UNSIGNED Subtraction**|Modify negative values inserted into table columns of UNSIGNED datatype.<br /><br />**Default Mode**:   Convert 'as-is'<br /><br />**Optimistic Mode**:  Convert 'as-is'<br /><br />**Full Mode**:   Mark With a Warning|  
|**Conversion to and from Binary data type**|Specifes how to handle implicit and explicit conversion from Binary data type.<br /><br />**Default Mode**:   Optimistic<br /><br />**Optimistic Mode**:  Optimistic<br /><br />**Full Mode**:   Precise|  
|**Conversion to Date/Time data type**|Specifes how to handle implicit and explicit conversion to Date/Time data type.<br /><br />**Default Mode**:   Emulate MySQL format<br /><br />**Optimistic Mode**:  Use SQL Server format<br /><br />**Full Mode**:   Emulate MySQL format|  
|**Numeric Literals With Precision Exceeding 38**|Specifies how to convert numeric literals with precision exceeding 38.<br /><br />**Default Mode**:   Round if Possible<br /><br />**Optimistic Mode**:  Round if Possible<br /><br />**Full Mode**:   Round if Possible|  
|**Zero-date in NOT NULL columns**|Specifes how to handle assignment to NOT NULL columns of Zero-date, Zero-in-date or invalid date/time values.<br /><br />**Default Mode**:   GETDATE()<br /><br />**Optimistic Mode**:  GETDATE()<br /><br />**Full Mode**:   GETDATE()|  
  
## See Also  
[User Interface Reference &#40;MySQLToSQL&#41;](../../ssma/mysql/user-interface-reference-mysqltosql.md)  
  
