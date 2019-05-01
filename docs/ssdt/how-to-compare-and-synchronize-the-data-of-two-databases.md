---
title: "How to: Compare and Synchronize the Data of Two Databases | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
f1_keywords: 
  - "sql.data.tools.datacompare.connection.datasources.f1"
  - "sql.data.tools.datacompare.watermark.f1"
  - "sql.data.tools.datacompare.connection.objectselection.f1"
ms.assetid: 2148e517-ed42-41c6-b753-1ac625f594c8
author: "markingmyname"
ms.author: "maghan"
manager: "craigg"
---
# How to: Compare and Synchronize the Data of Two Databases
You can compare the data that is contained in two databases. The databases that you compare are known as the *source* and the *target*.  
  
> [!NOTE]  
> *Database projects* and .dacpac or .bacpac packages cannot be the source or target in a data comparison.  
  
As the data is compared, a *Data Manipulation Language* (DML) script is generated, which you can use to synchronize the differing databases by updating some or all of the data on the target database. When the data comparison finishes, its results appear in the Data Compare window of Visual Studio.  
  
After the comparison finishes, you can take other steps:  
  
-   You can view the differences between the two databases. For more information, see [Viewing Data Differences](#ViewDifferences).  
  
-   You can update all or part of the target to match the source. For more information, see [Synchronizing Database Data](#Synchronize).  
  
For more information, see [Compare and Synchronize Data in One or More Tables with Data in a Reference Database](../ssdt/compare-and-synchronize-data-in-tables-with-data-in-reference-database.md).  
  
> [!NOTE]  
> You can also compare the *schema* of two databases or of two versions of the same database. For more information, see [How to: Use Schema Compare to Compare Different Database Definitions](../ssdt/how-to-use-schema-compare-to-compare-different-database-definitions.md).  
  
## <a name="CompareDatabaseData"></a>Comparing Database Data  
  
#### To compare data by using the New Data Comparison Wizard  
  
1.  On the **SQL** menu, point to **Data Compare**, and then click **New Data Comparison**.  
  
    The New Data Comparison wizard appears. Also, the Data Compare window opens, and Visual Studio automatically assigns it a name such as DataCompare1.  
  
2.  Identify the source and target databases.  
  
    If the **Source Database** list or the **Target Database** list is empty, click **New Connection**. In the **Connection Properties** dialog box, identify the server on which the database resides and the type of authentication to use when connecting to the database. Then, click **OK** to close the **Connection Properties** dialog box and return to the Data Compare wizard.  
  
    On the first page of the Data Compare wizard, verify that the information for each database is correct, specify which records you want to include in the results, and then click **Next**. The second page of the Data Compare wizard appears and shows a hierarchical listing of the tables and views in the database.  
  
3.  Select the check boxes for the tables and views that you want to compare. Optionally, expand the nodes for database objects, and then select the check boxes for columns within those objects that you want to compare.  
  
    > [!NOTE]  
    > Tables and views must meet two criteria to appear in the listing. First, the schemas of the objects must match between the source and target databases. Second, only tables and views that have a primary key, a unique key, a unique index, or a unique constraint appear in the list. If no tables or views meet both criteria, the list will be empty.  
  
4.  If more than one key is present, you can use the **Comparison Key** column to specify the key on which to base the data comparison. For example, you can specify whether to base the comparison on the primary key column or on another (uniquely identifiable) key column.  
  
5.  Click **Finish**.  
  
    The comparison starts.  
  
    > [!NOTE]  
    > You can stop a data comparison operation that is in progress by opening the **SQL** menu, clicking **Data Compare**, and then clicking **Stop Data Comparison**.  
  
    When the comparison is finished, you can view the data differences between the two databases. You can also update part or all the data in the target database to match the data in the source database.  
  
#### To compare data by using the Visual Studio automation model  
  
1.  Open the **View** menu, point to **Other Windows**, and click **Command Window**.  
  
2.  In the Command Window, type the following command:  
  
    ```  
    Sql.NewDataComparison /SrcServerName sServerName /SrcDatabaseName sDatabaseName /SrcUserName sUserName /SrcPassword sPassword /SrcDisplayName sDisplayName /TargetServerName tServerName /TargetDatabaseName tDatabaseName /TargeUserName tUserName /TargetPassword tPassword /TargetDisplayName tDisplayName  
    ```  
  
    Replace the placeholders (*sServerName*, *sDatabaseName*, *sUserName*, *sPassword*, *sDisplayName*, *tServerName*, *tDatabaseName*, *tUserName*, *tPassword*, and *tDisplayName*) with the values for your source and target databases.  
  
    If you do not specify a source and a target, the **New Data Comparison** dialog box appears. For more information about the parameters for the Sql.NewDataComparison command, see [Automation Command Reference for Database Features of Visual Studio Team System](https://msdn.microsoft.com/library/dd470565.aspx).  
  
    The data in the specified source and target databases are compared. The results appear in a Data Compare session. For more information about how to view results or synchronize the data, see [Viewing Data Differences](#ViewDifferences) and [Synchronizing Database Data](#Synchronize).  
  
## <a name="ViewDifferences"></a>Viewing Data Differences  
After you compare the data in two databases, Data Compare lists each *database object* that you compared and its status. You can also view results for the records within each object, grouped by status. For more information about the status designations, see [Compare and Synchronize Data in One or More Tables with Data in a Reference Database](../ssdt/compare-and-synchronize-data-in-tables-with-data-in-reference-database.md).  
  
After you view the differences, you can update the target to match the source for some or all of the objects or records that are different, missing, or new. For more information, see [Synchronizing Database Data](#Synchronize).  
  
#### To view data differences  
  
1.  Compare the data in a source and a target database. For more information, see [Compare Database Data](#CompareDatabaseData).  
  
2.  (Optional) Do one or both of the following:  
  
    -   By default, the results for all objects appear, regardless of their status. To display only those objects that have a particular status, click an option in the **Filter** list.  
  
    -   To view results for records within a particular object, click the object in the main results pane, and then click a tab in the records view pane. Each tab displays all records within that object that have a particular status: different, only in source, only in target, and identical. Data appears by record and column.  
  
## <a name="Synchronize"></a>Synchronizing Database Data  
After you compare the data in two databases, you can synchronize them by updating all or part of the target to match the source. You can compare the data in two kinds of database objects: tables and views.  
  
#### To update target data by using the Write Updates command  
  
1.  Compare the data in a source and a target database. For more information, see [Compare Database Data](#CompareDatabaseData).  
  
    After the comparison finishes, the Data Compare window lists results for the objects that were compared. Four columns (named Different Records, Only in Source, Only in Target, and Identical Records) display information about objects that were not identical. For each such object, these columns display how many records were found to be different, and how many records an update operation would change. Those two numbers match at first, but in step 4 you can change which objects to update.  
  
    For more information, see [Compare and Synchronize Data in One or More Tables with Data in a Reference Database](../ssdt/compare-and-synchronize-data-in-tables-with-data-in-reference-database.md).  
  
2.  In the table of the Data Compare window, click a row.  
  
    The details pane shows results for the records in the database object that you clicked. Records are grouped by status onto tabs, which you can use to specify the data that will be propagated from the source to the target.  
  
3.  In the details pane, click a tab whose name contains a number other than zero (0).  
  
    The **Update** column of the **Only in Target** table contains check boxes that you can use to select rows to be updated. By default, each check box is selected.  
  
4.  Clear check boxes for records in the target that you do not want to update with data from the source.  
  
    When you clear a check box, you reduce the number of records to update, and the display changes to reflect your actions. This number appears in the status line of the details pane and in the corresponding column in the main results pane, as described in step 1.  
  
5.  (Optional) Click **Generate Script**.  
  
    A Transact\-SQL editor window opens and shows the *Data Manipulation Language* (DML) script that would be used to update the target.  
  
6.  To synchronize records that are different, missing, or new, click **Update Target**.  
  
    > [!NOTE]  
    > While the target database is being updated, you can cancel the operation by clicking **Stop Writing to Target**.  
  
    The data of the selected records in the target is updated with the data from the corresponding records in the source.  
  
    > [!NOTE]  
    > If you opt to update indexed views, the **Update Target** operation might fail if this action causes duplicate keys to be inserted into the same table.  
  
#### To update target data by using a Transact\-SQL script  
  
1.  Compare the data in a source and a target database. For more information, see [Compare Database Data](#CompareDatabaseData).  
  
    After the comparison finishes, the Data Compare window lists the objects that were compared. For more information, see [Compare and Synchronize Data in One or More Tables with Data in a Reference Database](../ssdt/compare-and-synchronize-data-in-tables-with-data-in-reference-database.md).  
  
2.  (Optional) In the details pane, clear the check boxes for records in the target that you do not want to update, as described in the previous procedure.  
  
3.  Click **Generate Script**.  
  
    A new window shows the Transact\-SQL script that would propagate the changes necessary to make the data in the target match the data in the source. The new window is given a name such as **DataUpdate_Database_1.sql**.  
  
    This script reflects changes that you have made in the details pane. For example, you might have cleared a check box for a given row in the Only in Target page for the [dbo].[Shippers] table. In that case, the script would not update that row.  
  
4.  (Optional) Edit this script in the **DataUpdate_Database_1.sql** window.  
  
5.  (Optional but recommended) Back up the target database.  
  
6.  Click **Execute** to update the target database.  
  
    Specify a connection to the target database that you want to update.  
  
    > [!IMPORTANT]  
    > By default, the updates occur within the scope of a transaction. If errors occur, you can roll back the whole update. You can change this behavior.  
  
    The data of the selected records in the target is updated with the data from the corresponding records in the source.  
  
## See Also  
[Compare and Synchronize Data in One or More Tables with Data in a Reference Database](../ssdt/compare-and-synchronize-data-in-tables-with-data-in-reference-database.md)  
  
