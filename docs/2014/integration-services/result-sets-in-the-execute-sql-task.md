---
title: "Result Sets in the Execute SQL Task | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "result sets [Integration Services]"
  - "Execute SQL task [Integration Services]"
ms.assetid: 62605b63-d43b-49e8-a863-e154011e6109
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Result Sets in the Execute SQL Task
  In an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] package, whether a result set is returned to the Execute SQL task depends on the type of SQL command that the task uses. For example, a SELECT statement typically returns a result set, but an INSERT statement does not.  
  
 What the result set contains also varies by SQL command. For example, the result set from a SELECT statement can contain zero rows, one row, or many rows. However, the result set from a SELECT statement that returns a count or a sum contains only a single row.  
  
 Working with result sets in an Execute SQL task is more than just knowing whether the SQL command returns a result set and what that result set contains. There are additional usage requirements and guidelines to successfully use result sets in the Execute SQL task. The remainder of this topic covers these usage requirements and guidelines:  
  
-   [Specifying a Result Set Type](#Result_set_type)  
  
-   [Populating a variable with a result set](#Populate_variable_with_result_set)  
  
-   [Configuring results sets in the Execute SQL Task Editor](#Configure_result_sets)  
  
##  <a name="Result_set_type"></a> Specifying a Result Set Type  
 The Execute SQL task supports the following types of result sets:  
  
-   The **None** result set is used when the query returns no results. For example, this result set is used for queries that add, change, and delete records in a table.  
  
-   The **Single row** result set is used when the query returns only one row. For example, this result set is used for a SELECT statement that returns a count or a sum.  
  
-   The **Full result set** result set is used when the query returns multiple rows. For example, this result set is used for a SELECT statement that retrieves all the rows in a table.  
  
-   The **XML** result set is used when the query returns a result set in an XML format. For example, this result set is used for a SELECT statement that includes a FOR XML clause.  
  
 If the Execute SQL task uses the **Full result set** result set and the query returns multiple rowsets, the task returns only the first rowset. If this rowset generates an error, the task reports the error. If other rowsets generate errors, the task does not report them.  
  
##  <a name="Populate_variable_with_result_set"></a> Populating a Variable with a Result Set  
 You can bind the result set that a query returns to a user-defined variable, if the result set type is a single row, a rowset, or XML.  
  
 If the result set type is **Single row**, you can bind a column in the return result to a variable by using the column name as the result set name, or you can use the ordinal position of the column in the column list as the result set name. For example, the result set name for the query `SELECT Color FROM Production.Product WHERE ProductID = ?` could be **Color** or **0**. If the query returns multiple columns and you want to access the values in all columns, you must bind each column to a different variable. If you map columns to variables using numbers as result set names, the numbers reflect the order in which the columns appear in the column list of the query. For example, in the query `SELECT Color, ListPrice, FROM Production.Product WHERE ProductID = ?`, you use 0 for the **Color** column and 1 for the **ListPrice** column. The ability to use a column name as the name of a result set depends on the provider that the task is configured to use. Not all providers make column names available.  
  
 Some queries that return a single value may not include column names. For example, the statement `SELECT COUNT (*) FROM Production.Product` returns no column name. You can access the return result using the ordinal position, 0, as the result name. To access the return result by column name, the query must include an AS \<alias name> clause to provide a column name. The statement `SELECT COUNT (*)AS CountOfProduct FROM Production.Product`, provides the **CountOfProduct** column. You can then access the return result column using the **CountOfProduct** column name or the ordinal position, 0.  
  
 If the result set type is **Full result set** or **XML**, you must use 0 as the result set name.  
  
 When you map a variable to a result set with the **Single row** result set type, the variable must have a data type that is compatible with the data type of the column that the result set contains. For example, a result set that contains a column with a `String` data type cannot map to a variable with a numeric data type. When you set the **TypeConversionMode** property to `Allowed`, the Execute SQL Task will attempt to convert output parameter and query results to the data type of the variable the results are assigned to.  
  
 An XML result set can map only to a variable with the `String` or `Object` data type. If the variable has the `String` data type, the Execute SQL task returns a string and the XML source can consume the XML data. If the variable has the `Object` data type, the Execute SQL task returns a Document Object Model (DOM) object.  
  
 A **Full result set** must map to a variable of the `Object` data type. The return result is a rowset object. You can use a Foreach Loop container to extract the table row values that are stored in the Object variable into package variables, and then use a Script Task to write the data stored in packages variables to a file. For a demonstration on how to do this using a Foreach Loop container and a Script Task, see the CodePlex sample, [Execute SQL Parameters and Result Sets](https://go.microsoft.com/fwlink/?LinkId=157863), on msftisprodsamples.codeplex.com.  
  
 The following table summarizes the data types of variables that can be mapped to result sets.  
  
|Result set type|Data type of variable|Type of object|  
|---------------------|---------------------------|--------------------|  
|Single row|Any type that is compatible with the type column in the result set.|Not applicable|  
|Full result set|`Object`|If the task uses a native connection manager, including the ADO, OLE DB, Excel, and ODBC connection managers, the returned object is an ADO `Recordset`.<br /><br /> If the task uses a managed connection manager, such as the [!INCLUDE[vstecado](../includes/vstecado-md.md)] connection manager, then the returned object is a `System.Data.DataSet`.<br /><br /> You can use a Script task to access the `System.Data.DataSet` object, as shown in the following example.<br /><br /> `Dim dt As Data.DataTable` <br /> `Dim ds As Data.DataSet = CType(Dts.Variables("Recordset").Value, DataSet)` <br /> `dt = ds.Tables(0)`|  
|XML|`String`|`String`|  
|XML|`Object`|If the task uses a native connection manager, including the ADO, OLE DB, Excel, and ODBC connection managers, the returned object is an `MSXML6.IXMLDOMDocument`.<br /><br /> If the task uses a managed connection manager, such as the [!INCLUDE[vstecado](../includes/vstecado-md.md)] connection manager, the returned object is a `System.Xml.XmlDocument`.|  
  
 The variable can be defined in the scope of the Execute SQL task or the package. If the variable has package scope, the result set is available to other tasks and containers within the package, and is available to any packages run by the Execute Package or Execute DTS 2000 Package tasks.  
  
 When you map a variable to a **Single row** result set, non-string values that the SQL statement returns are converted to strings when the following conditions are met:  
  
-   The **TypeConversionMode** property is set to true. You set the property value in the Properties window or by using the **Execute SQL Task Editor**.  
  
-   The conversion will not result in data truncation.  
  
 For information about loading a result set into a variable, see [Map Result Sets to Variables in an Execute SQL Task](control-flow/execute-sql-task.md).  
  
##  <a name="Configure_result_sets"></a> Configuring Result Sets in the Execute SQL Task  
 For more information about the properties of result sets that you can set in [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Execute SQL Task Editor &#40;Result Set Page&#41;](../../2014/integration-services/execute-sql-task-editor-result-set-page.md)  
  
 For more information about how to set these properties in [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Set the Properties of a Task or Container](../../2014/integration-services/set-the-properties-of-a-task-or-container.md)  
  
## Related Tasks  
 [Map Result Sets to Variables in an Execute SQL Task](control-flow/execute-sql-task.md)  
  
## Related Content  
  
-   CodePlex sample, [Execute SQL Parameters and Result Sets](https://go.microsoft.com/fwlink/?LinkId=157863), on msftisprodsamples.codeplex.com  
  
  
