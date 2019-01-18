---
title: "Create Statistics | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.stat.properties.f1"
  - "sql13.swb.statistics.filter.f1"
  - "sql13.swb.stat.columns.f1"
  - "sql13.swb.statistics.propertis.f1"
helpviewer_keywords: 
  - "creating statistics"
  - "statistics [SQL Server], creating"
ms.assetid: 95a455fb-664d-4c95-851e-c6b62d7ebe04
author: julieMSFT
ms.author: jrasnick
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Create Statistics
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
  You can create query optimization statistics on one or more columns of a table or indexed view in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. For most queries, the query optimizer already generates the necessary statistics for a high-quality query plan; in a few cases, you need to create additional statistics.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To create statistics, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   Before creating statistics with the CREATE STATISTICS statement, verify that the AUTO_CREATE_STATISTICS option is set at the database level. This will ensure that the query optimizer continues to routinely create single-column statistics for query predicate columns.  
  
-   You can list up to 32 columns per statistics object.  
  
-   You cannot drop, rename, or alter the definition of a table column that is defined in a filtered statistics predicate.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires that the user be the table or indexed view owner, or a member of one of the following roles: **sysadmin** fixed server role, **db_owner** fixed database role, or the **db_ddladmin** fixed database role.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To create statistics  
  
1.  In **Object Explorer**, click the plus sign to expand the database in which you want to create a new statistic.  
  
2.  Click the plus sign to expand the **Tables** folder.  
  
3.  Click the plus sign to expand the table in which you want to create a new statistic.  
  
4.  Right-click the **Statistics** folder and select **New Statistics...**.  
  
     The following properties show on the **General** page in the **New Statistics on Table**_table\_name_ dialog box.  
  
     **Table Name**  
     Displays the name of the table described by the statistics.  
  
     **Statistics Name**  
     Displays the name of the database object where the statistics are stored.  
  
     **Statistics Columns**  
     This grid shows the columns described by this set of statistics. All values in the grid are read-only.  
  
     **Name**  
     Displays the name of the column described by the statistics. This can be a single column or a combination of columns in a single table.  
  
     **Data Type**  
     Indicates the data type of the columns described by the statistics.  
  
     **Size**  
     Displays the size of the data type for each column.  
  
     **Identity**  
     Indicates an identity column when it is checked.  
  
     **Allow Nulls**  
     Indicates whether the column accepts NULL values.  
  
     **Add**  
     Add additional columns from the table to the statistics grid.  
  
     **Remove**  
     Remove the selected column from the statistics grid.  
  
     **Move Up**  
     Move the selected column to an earlier location in the statistics grid. The location in the grid can substantially impact the usefulness of the statistics.  
  
     **Move Down**  
     Move the selected column to a later location in the statistics grid.  
  
     **Statistics for these columns were last updated:**  
     Indicates how old the statistics are. Statistics are more valuable when they are current. Update statistics after large changes to the data or after adding atypical data. Statistics for tables that have a consistent distribution of data need to be updated less frequently.  
  
     **Update statistics for these columns**  
     Check to update the statistics when the dialog box is closed.  
  
     The following property shows on the **Filter** page in the **New Statistics on Table**_table\_name_ dialog box.  
  
     **Filter Expression**  
     Defines which data rows to include in the filtered statistics. For example, `Production.ProductSubcategoryID IN ( 1,2,3 )`  
  
5.  In the **New Statistics on Table**_table\_name_ dialog box, on the **General** page, click **Add**.  
  
     The following properties show in the **Select Columns** dialog box. This information is read-only.  
  
     **Name**  
     Displays the name of the column described by the statistics. This can be a single column or a combination of columns in a single table.  
  
     **Data Type**  
     Indicates the data type of the columns described by the statistics.  
  
     **Size**  
     Displays the size of the data type for each column.  
  
     **Identity**  
     Indicates an identity column when checked.  
  
     **Allow NULLs**  
     Indicates whether the column accepts NULL values.  
  
6.  In the **Select Columns** dialog box, select the check box or check boxes of each column for which you want to create a statistic and then click **OK**.  
  
7.  In the **New Statistics on Table**_table\_name_ dialog box, click **OK**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To create statistics  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    USE AdventureWorks2012;   
    GO  
    -- Create new statistic object called ContactMail1  
    -- on the BusinessEntityID and EmailPromotion columns in the Person.Person table.   
  
    CREATE STATISTICS ContactMail1  
        ON Person.Person (BusinessEntityID, EmailPromotion);   
    GO  
    ```  
  
4.  The statistic created above potentially improves the results for the following query.  
  
    ```  
    USE AdventureWorks2012;   
    GO  
    SELECT LastName, FirstName  
    FROM Person.Person  
    WHERE EmailPromotion = 2  
    ORDER BY LastName, FirstName;   
    GO  
    ```  
  
 For more information, see [CREATE STATISTICS &#40;Transact-SQL&#41;](../../t-sql/statements/create-statistics-transact-sql.md).  
  
  
