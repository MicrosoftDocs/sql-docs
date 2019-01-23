---
title: "Define Named Queries in a Data Source View (Analysis Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "named queries [Analysis Services], creating"
  - "modifying named queries"
  - "data source views [Analysis Services], named queries"
ms.assetid: f09ba8aa-950e-4c0d-961e-970de13200be
author: minewiskan
ms.author: owend
manager: craigg
---
# Define Named Queries in a Data Source View (Analysis Services)
  A named query is a SQL expression represented as a table. In a named query, you can specify an SQL expression to select rows and columns returned from one or more tables in one or more data sources. A named query is like any other table in a data source view (DSV) with rows and relationships, except that the named query is based on an expression.  
  
 A named query lets you extend the relational schema of existing tables in DSV without modifying the underlying data source. For example, a series of named queries can be used to split up a complex dimension table into smaller, simpler dimension tables for use in database dimensions. A named query can also be used to join multiple database tables from one or more data sources into a single data source view table.  
  
## Creating a Named Query  
  
> [!NOTE]  
>  You cannot add a named calculation to a named query, nor can you base a named query on a table that contains a named calculation.  
  
 When you create a named query, you specify a name, the SQL query returning the columns and data for the table, and optionally, a description of the named query. The SQL expression can refer to other tables in the data source view. After the named query is defined, the SQL query in a named query is sent to the provider for the data source and validated as a whole. If the provider does not find any errors in the SQL query, the column is added to the table.  
  
 Tables and columns referenced in the SQL query should not be qualified or should be qualified by the table name only. For example, to refer to the SaleAmount column in a table, `SaleAmount` or `Sales.SaleAmount` is valid, but `dbo.Sales.SaleAmount` generates an error.  
  
 **Note** When defining a named query that queries a [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 7.0 data source, a named query that contains a correlated subquery and a GROUP BY clause will fail. For more information, see [Internal Error with SELECT Statement Containing Correlated Subquery and GROUP BY](https://support.microsoft.com/kb/274729) in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Knowledge Base.  
  
## Add or Edit a Named Query  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the project or connect to the database that contains the data source view in which you want to add a named query.  
  
2.  In Solution Explorer, expand the **Data Source Views** folder, then double-click the data source view.  
  
3.  In the **Tables** or **Diagram** pane, right-click an open area and then click **New Named Query**.  
  
4.  In the **Create Named Query** dialog box, do the following:  
  
    1.  In the **Name** text box, type a query name.  
  
    2.  Optionally, in the **Description** text box, type a description for the query.  
  
    3.  In the **Data Source** list box, select the data source against which the named query will execute.  
  
    4.  Type the query in the bottom pane, or use the graphical query building tools to create a query.  
  
    > [!NOTE]  
    >  The query-building user interface (UI) depends on the data source. Instead of getting a graphical UI, you can get a generic UI, which is text-based. You can accomplish the same things with these different UIs, but you must do so in different ways. For more information, see [Create or Edit Named Query Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](../create-or-edit-named-query-dialog-box-analysis-services-multidimensional-data.md).  
  
5.  Click **OK**. An icon showing two overlapping tables appears in the table header to indicate that the table has been replaced by a named query.  
  
## See Also  
 [Data Source Views in Multidimensional Models](data-source-views-in-multidimensional-models.md)   
 [Define Named Calculations in a Data Source View &#40;Analysis Services&#41;](define-named-calculations-in-a-data-source-view-analysis-services.md)  
  
  
