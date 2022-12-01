---
title: "Text-based Query Designer User Interface (Report Builder) | Microsoft Docs"
description: Use the text-based query designer to specify a query using the query language supported by the data source, run the query, and view the results at design time. 
ms.date: 03/14/2017
ms.service: reporting-services
ms.subservice: report-data


ms.topic: conceptual
f1_keywords: 
  - "10010"
helpviewer_keywords: 
  - "query designers, text-based"
ms.assetid: 89fddca5-bd96-4128-9072-5348d1b6e02c
author: maggiesMSFT
ms.author: maggies
---
# Text-based Query Designer User Interface (Report Builder)
  Use the text-based query designer to specify a query using the query language supported by the data source, run the query, and view the results at design time. You can specify multiple [!INCLUDE[tsql](../../includes/tsql-md.md)] statements, query or command syntax for custom data processing extensions, and queries that are specified as expressions. Because the text-based query designer does not preprocess the query and can accommodate any kind of query syntax, this is the default query designer tool for many data source types.  
  
> [!IMPORTANT]  
>  Users access data sources when they create and run queries. You should grant minimal permissions on the data sources, such as read-only permissions.  
  
 The text-based query designer displays a toolbar and the following two panes:  
  
-   **Query** Shows the query text, table name, or stored procedure name depending on the query type. Not all query types are available for all data source types. For example, table name is supported only for the data source type OLE DB.  
  
-   **Result** Shows the results of running the query at design time.  
  
## Text-based Query Designer Toolbar  
 The text-based query designer provides a single toolbar for all the command types. The following table lists each button on the toolbar and its function.  
  
|Button|Description|  
|------------|-----------------|  
|**Edit As Text**|Toggle between the text-based query designer and the graphical query designer. Not all data source types support graphical query designers.|  
|**Import**|Import an existing query from a file or report. Only file types sql and rdl are supported|  
|![Run the query](../../reporting-services/report-data/media/rsqdicon-run.gif "Run the query")|Run the query and display the result set in the Result pane.|  
|**Command Type**|Select **Text**, **StoredProcedure**, or **TableDirect**. If a stored procedure has parameters, the **Define Query Parameters** dialog box appears when you click **Run** on the toolbar, and you can fill in values as needed. Support for command type varies by data source type. For example, only OLE DB and ODBC support **TableDirect**.<br /><br /> Note: If a stored procedure returns more than one result set, only the first result set is used to populate the dataset.|  
  
### Command Type Text  
 When you create a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] dataset, the relational query designer opens by default. To switch to the text-based query designer, click the **Edit As Text** toggle button on the toolbar. The text-based query designer presents two panes: the Query pane and the Result pane. The following figure labels each pane.  
  
 ![Generic query designer, for relational data query](../../reporting-services/report-data/media/rsqd-dsaw-sql-generic.gif "Generic query designer, for relational data query")  
  
 The following table describes the function of each pane.  
  
|Pane|Function|  
|----------|--------------|  
|Query|Displays the [!INCLUDE[tsql](../../includes/tsql-md.md)] query text. Use this pane to write or edit a [!INCLUDE[tsql](../../includes/tsql-md.md)] query.|  
|Result|Displays the results of the query. To run the query, right-click in any pane and click **Run**, or click the **Run** button on the toolbar.|  
  
#### Example  
 The following query returns the list of names from the  AdventureWorks2014 database **ContactType** table for the **Person** schema.  
  
```  
SELECT Name FROM Person.ContactType  
```  
  
 When you click **Run** on the toolbar, the command in the **Query** pane runs and the results are displayed in the **Result** pane. The resultset displays a list of 20 types of contacts, for example, Owner or Sales Agent.  
  
### Command Type StoredProcedure  
 When you select **Command typeStoredProcedure**, the text-based query designer presents two panes: the Query pane and the Result pane. Enter the stored procedure name in the Query pane and click **Run** on the toolbar. If the stored procedures uses parameters, the **Define Query Parameters** dialog box opens. Enter the parameter values for the stored procedure. A report parameter is created for every stored procedure input parameter.  
  
 The following figure shows the Query and Results panes when you run a stored procedure. In this case, the input parameters are constants.  
  
 ![Stored procedure in text-based query designer](../../reporting-services/report-data/media/rs-relational-text-sp.gif "Stored procedure in text-based query designer")  
  
 The following table describes the function of each pane.  
  
|Pane|Function|  
|----------|--------------|  
|Query|Displays the name of the stored procedure and any input parameters.|  
|Result|Displays the results of the query. To run the query, right-click in any pane and click **Run**, or click the **Run** button on the toolbar.|  
  
#### Example  
 The following query calls the AdventureWorks2014 stored procedure **uspGetWhereUsedProductID**. You must enter a value for the product identification number parameter when you run the query.  
  
```  
uspGetWhereUsedProductID  
```  
  
 Click the **Run** (**!**) button. When prompted for the query parameters, use the following table to enter values.  
  
|Parameter|Value|  
|-|-|  
|*\@StartProductID*|820|  
|*\@CheckDate*|20010115|  
  
 For the specified date, the result set displays a list of 13 product identifiers that used the specified component number.  
  
### Command Type TableDirect  
 When you select **Command typeTableDirect**, the text-based query designer presents two panes: the Query pane and the Result pane. When you enter a table and click the **Run** button, all the columns for that table are returned.  
  
#### Example  
 For a data source type OLE DB, the following dataset query returns a result set for all contact types in the AdventureWorks2014 database.  
  
 `Person.ContactType`  
  
 When you enter the table name Person.ContactType, it is the equivalent of creating the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement `SELECT * FROM Person.ContactType`.  
  
## See Also  
 [Relational Query Designer User Interface &#40;Report Builder&#41;](../../reporting-services/report-data/relational-query-designer-user-interface-report-builder.md)   
 [Query Design Tools &#40;SSRS&#41;](query-design-tools-ssrs.md)  
  
  
