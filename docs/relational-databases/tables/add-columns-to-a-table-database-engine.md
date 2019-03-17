---
title: "Add Columns to a Table (Database Engine) | Microsoft Docs"
ms.custom: ""
ms.date: "10/27/2016"
ms.prod: sql
ms.prod_service: "table-view-index, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "inserting columns"
  - "columns [SQL Server], adding"
  - "adding columns"
ms.assetid: abeb8d52-d562-4e29-9e1e-2923ae874859
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Add Columns to a Table (Database Engine)
[!INCLUDE[tsql-appliesto-ss2016-all-md](../../includes/tsql-appliesto-ss2016-all-md.md)]

  This topic describes how to add new columns to a table in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  

  ##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
 Using the ALTER TABLE statement to add columns to a table automatically adds those columns to the end of the table. If you want the columns in a specific order in the table, use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. However, note that this is not a database design best practice. Best practice is to specify the order in which the columns are returned at the application and query level. You should not rely on the use of SELECT * to return all columns in an expected order based on the order in which they are defined in the table. Always specify the columns by name in your queries and applications in the order in which you would like them to appear.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the table.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To insert columns into a table with Table Designer  
  
1.  In **Object Explorer**, right-click the table to which you want to add columns and choose **Design**.  
  
2.  Click in the first blank cell in the **Column Name** column.  
  
3.  Type the column name in the cell. The column name is a required value.  
  
4.  Press the TAB key to go to the **Data Type** cell and select a data type from the dropdown. This is a required value, and will be assigned the default value if you don't choose one.  
  
    > [!NOTE]  
    >  You can change the default value in the **Options** dialog box under **Database Tools**.  
  
5.  Continue to define any other column properties in the **Column Properties** tab.  
  
    > [!NOTE]  
    >  The default values for your column properties are added when you create a new column, but you can change them in the **Column Properties** tab.  
  
6.  When you are finished adding columns, from the **File** menu, choose **Save** _table name_.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To insert columns into a table  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  The following example adds two columns to the table `dbo.doc_exa`. Copy and paste the following example into the query window and click **Execute**.  
  
```  
ALTER TABLE dbo.doc_exa ADD column_b VARCHAR(20) NULL, column_c INT NULL ;  
```  
  
####  <a name="FollowUp"></a> For more information, see [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)  
  
  
