---
title: "Compare and Synchronize Data in One or More Tables with Data in a Reference Database | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: 96d743b0-b69a-45bb-ae0e-62103dca76e2
author: "stevestein"
ms.author: "sstein"
manager: "craigg"
---
# Compare and Synchronize Data in One or More Tables with Data in a Reference Database
You can compare the data in a *source* database and a *target* database and specify which tables should be compared. You can review the data and decide which changes to synchronize. You can then either update the target to synchronize the databases or export the update script to the Transact\-SQL editor or to a file.  
  
For example, you might synchronize databases to update a staging server with a copy of the production data. You might also synchronize one or more tables to populate them with reference data from another database. In addition, you might compare data before and after you run tests as an additional form of verification.  
  
You can compare data in two databases, but you cannot specify a database project file or .dacpac for comparison because it does not contain data.  
  
This section contains the following topics:  
  
-   [How to: Compare and Synchronize the Data of Two Databases](../ssdt/how-to-compare-and-synchronize-the-data-of-two-databases.md)  
  
-   [How to: View Data Differences](../ssdt/how-to-view-data-differences.md)  
  
## Requirements  
When you compare data in a table or view, the table or view in the source database must share several attributes with a table or view in the target database. Tables and views that do not meet the following criteria are not compared and do not appear on the second page of the **New Data Comparison** wizard:  
  
-   Tables must have matching column names that have compatible data types.  
  
    Names of tables, views, and owners are case-sensitive.  
  
-   Tables must have the same primary key, unique index, or unique constraint.  
  
-   Views must have the same unique, clustered index.  
  
-   You can compare a table with a view only if they have the same name.  
  
Each object has a key or an index that determines the other objects to which it corresponds. However, each table or view can have more than one primary key, unique index, or unique constraint. Therefore, you might want to specify which key, index, or constraint to use.  
  
## Common Tasks  
In this section, you can find descriptions of common tasks that support this scenario.  
  
**Set options to control how the data is compared:** When you compare data, you can safely ignore identity columns, disable triggers, and disable foreign keys. You can also drop primary keys, indexes, and unique constraints from the update script.  
  
**Compare data in tables and optionally update the target to match the source:** After you specify a source and a target database to compare and run the comparison, you can view the results in the **Data Compare** window. You can view not only details of the differences but also the update script that you can use to synchronize the data. After you identify differences between the two databases, you can specify an action for each difference. You can then update the target or export the update script to the Transact\-SQL editor or to a file. You might want to export the script so that you or someone else can review it before you apply the changes.  
  
## <a name="UnderstandingDataCompareResults"></a>Understanding Comparison Results  
The following table describes the five columns in the **Data Compare** window.  
  
|Column|Notes|  
|----------|---------|  
|Object|Displays the name of the table or view and a check box that indicates whether the target should be synchronized when you write updates or export the update script. The check box is unavailable for tables or views that do not contain data.|  
|Different Records|Displays the number of records in the target that have the same key but not the same data as in the source. Parentheses enclose the number of records that are marked to be updated when you write updates or export the update script.|  
|Only in Source|Displays the number of records in the source that do not occur in the target. Parentheses enclose the number of records that are marked to be added when you write updates or export the update script.|  
|Only in Target|Displays the number of records in the target that do not occur in the source. Parentheses enclose the number of those records that are marked to be deleted when you write updates or export the update script.|  
|Identical Records|Displays the number of records in the target that have the same key and the same data as in the source. These records are not updated when you write updates or export the update script.|  
  
### Table and View Details  
When you click any table or view in the **Data Compare** window, the details pane displays all the rows that the table or view contains. Each tab in the details pane displays a different category (Different Records, Only in Source, Only in Target, Identical Records). For each row, you can select or clear the corresponding check box to indicate whether you want to include that change in the update script.  
  
## See Also  
[SQL Server Data Tools](../ssdt/sql-server-data-tools.md)  
[How to: Use Schema Compare to Compare Different Database Definitions](../ssdt/how-to-use-schema-compare-to-compare-different-database-definitions.md)  
  
