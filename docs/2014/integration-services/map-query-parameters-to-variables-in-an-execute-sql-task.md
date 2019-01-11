---
title: "Map Query Parameters to Variables in an Execute SQL Task | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "queries [Integration Services], parameter mapping"
  - "parameterized SQL commands [Integration Services]"
  - "parameters [Integration Services]"
  - "mapping query parameters to variables [Integration Services]"
  - "Execute SQL task [Integration Services]"
  - "variables [Integration Services], mapping parameters to"
ms.assetid: 6a164349-dfcf-4995-80bc-d4e7aee52a83
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Map Query Parameters to Variables in an Execute SQL Task

  > [!div class="nextstepaction"]
  > [Please share your feedback about the SQL Docs Table of Contents!](https://aka.ms/sqldocsurvey)

  This topic describes how to use a parameterized SQL statement in the Execute SQL task and create mappings between variables and the parameters in the SQL statement.  
  
 To learn more about the Execute SQL task, the parameter markers, and parameter names you use with different connection types, see [Execute SQL Task](control-flow/execute-sql-task.md) and [Parameters and Return Codes in the Execute SQL Task](../../2014/integration-services/parameters-and-return-codes-in-the-execute-sql-task.md).  
  
### To map a query parameter to a variable  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] package you want to work with.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Control Flow** tab.  
  
4.  If the package does not already include an Execute SQL task, add one to the control flow of the package. For more information, see [Add or Delete a Task or a Container in a Control Flow](control-flow/add-or-delete-a-task-or-a-container-in-a-control-flow.md)  
  .  
  
5.  Double-click the Execute SQL task.  
  
6.  Provide a parameterized SQL command in one of the following ways:  
  
    -   Use direct input and type the SQL command in the SQLStatement property.  
  
    -   Use direct input, click **Build Query**, and then create an SQL command using the graphical tools that the Query Builder provides.  
  
    -   Use a file connection and then reference the file that contains the SQL command.  
  
    -   Use a variable and then reference the variable that contains the SQL command.  
  
     The parameter markers that you use in parameterized SQL statements depend on the connection type that the Execute SQL task uses.  
  
    |Connection type|Parameter marker|  
    |---------------------|----------------------|  
    |ADO|?|  
    |ADO.NET and SQLMOBILE|@\<parameter name>|  
    |ODBC|?|  
    |EXCEL and OLE DB|?|  
  
     The following table lists examples of the SELECT command by connection manager type. Parameters provide the filter values in the WHERE clauses. The examples use SELECT to return products from the **Product** table in [!INCLUDE[ssSampleDBUserInputNonLocal](../includes/sssampledbuserinputnonlocal-md.md)] that have a **ProductID** greater than and less than the values specified by two parameters.  
  
    |Connection type|SELECT syntax|  
    |---------------------|-------------------|  
    |EXCEL, ODBC, and OLEDB|`SELECT* FROM Production.Product WHERE ProductId > ? AND ProductID < ?`|  
    |ADO|`SELECT* FROM Production.Product WHERE ProductId > ? AND ProductID < ?`|  
    |[!INCLUDE[vstecado](../includes/vstecado-md.md)]|`SELECT* FROM Production.Product WHERE ProductId > @parmMinProductID AND ProductID < @parmMaxProductID`|  
  
     For examples of using parameters with stored procedures, see [Parameters and Return Codes in the Execute SQL Task](../../2014/integration-services/parameters-and-return-codes-in-the-execute-sql-task.md).  
  
7.  Click **Parameter Mapping**.  
  
8.  To add a parameter mapping, click **Add**.  
  
9. Provide a name in the **Parameter Name** box.  
  
     The parameter names that you use depend on the connection type that the Execute SQL task uses.  
  
    |Connection type|Parameter name|  
    |---------------------|--------------------|  
    |ADO|Param1, Param2, ...|  
    |ADO.NET and SQLMOBILE|@\<parameter name>|  
    |ODBC|1, 2, 3, ...|  
    |EXCEL and OLE DB|0, 1, 2, 3, ...|  
  
10. From the **Variable Name** list, select a variable. For more information, see [Add, Delete, Change Scope of User-Defined Variable in a Package](../../2014/integration-services/add-delete-change-scope-of-user-defined-variable-in-a-package.md).  
  
11. In the **Direction** list, specify if the parameter is an input, an output, or a return value.  
  
12. In the **Data Type** list, set the data type of the parameter.  
  
    > [!IMPORTANT]  
    >  The data type of the parameter must be compatible with the data type of the variable.  
  
13. Repeat steps 8 through 11 for each parameter in the SQL statement.  
  
    > [!IMPORTANT]  
    >  The order of parameter mappings must be the same as the order in which the parameters appear in the SQL statement.  
  
14. Click **OK**.  
  
## See Also  
 [Execute SQL Task](control-flow/execute-sql-task.md)   
 [Parameters and Return Codes in the Execute SQL Task](../../2014/integration-services/parameters-and-return-codes-in-the-execute-sql-task.md)   
 [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md)  
  
  
