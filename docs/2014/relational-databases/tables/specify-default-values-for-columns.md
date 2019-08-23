---
title: "Specify Default Values for Columns | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "columns [SQL Server], defaults"
  - "default values"
ms.assetid: 64514aed-b846-407b-992e-cf813f9a1a91
author: stevestein
ms.author: sstein
manager: craigg
---
# Specify Default Values for Columns
  You can specify a default value that will be entered in the column in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. If you do not assign a default value and the user leaves the column blank, then:  
  
-   If you set the option to allow null values, NULL will be inserted into the column.  
  
-   If you do not set the option to allow null values, the column will remain blank, but the user will not be able to save the row until they supply a value for the column.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To specify a default value, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   If your entry in the **Default Value** field replaces a bound default (which is shown without parentheses), you will be prompted to unbind the default and replace it with your new default.  
  
-   To enter a text string, enclose the value in single quotation marks ('); do not use double quotation marks (") because they are reserved for quoted identifiers.  
  
-   To enter a numeric default, enter the number without quotation marks around it.  
  
-   To enter an object/function, enter the name of the object/function without quotation marks around it.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the table.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To specify a default value for a column  
  
1.  In **Object Explorer**, right-click the table with columns for which you want to change the scale and click **Design**.  
  
2.  Select the column for which you want to specify a default value.  
  
3.  In the **Column Properties** tab, enter the new default value in the **Default Value or Binding** property.  
  
    > [!NOTE]  
    >  To enter a numeric default value, enter the number. For an object or function enter its name. For an alphanumeric default enter the value inside single quotes.  
  
4.  On the **File** menu, click **Save**_table name_.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To specify a default value for a column  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    CREATE TABLE dbo.doc_exz ( column_a INT, column_b INT) ;  
    GO  
    INSERT INTO dbo.doc_exz (column_a)VALUES ( 7 ) ;  
    GO  
    ALTER TABLE dbo.doc_exz  
    ADD CONSTRAINT col_b_def  
    DEFAULT 50 FOR column_b ;  
    GO  
  
    ```  
  
 For more information, see [ALTER TABLE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-table-transact-sql).  
  
###  <a name="TsqlExample"></a>  
