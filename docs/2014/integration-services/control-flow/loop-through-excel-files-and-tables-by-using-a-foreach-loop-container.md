---
title: "Loop through Excel Files and Tables by Using a Foreach Loop Container | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "connections [Integration Services], Excel"
  - "Excel [Integration Services]"
  - "connection managers [Integration Services], Excel"
ms.assetid: a5393c1a-cc37-491a-a260-7aad84dbff68
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Loop through Excel Files and Tables by Using a Foreach Loop Container
  The procedures in this topic describe how to loop through the Excel workbooks in a folder, or through the tables in an Excel workbook, by using the Foreach Loop container with the appropriate enumerator.  
  
### To loop through Excel files by using the Foreach File enumerator  
  
1.  Create a string variable that will receive the current Excel path and file name on each iteration of the loop. To avoid validation issues, assign a valid Excel path and file name as the initial value of the variable. (The sample expression shown later in this procedure uses the variable name, `ExcelFile`.)  
  
2.  Optionally, create another string variable that will hold the value for the Extended Properties argument of the Excel connection string. This argument contains a series of values that specify the Excel version and determine whether the first row contains column names, and whether import mode is used. (The sample expression shown later in this procedure uses the variable name `ExtProperties`, with an initial value of "`Excel 8.0;HDR=Yes`".)  
  
     If you do not use a variable for the Extended Properties argument, then you must add it manually to the expression that contains the connection string.  
  
3.  Add a Foreach Loop container to the **Control Flow** tab. For information about how to configure the Foreach Loop Container, see [Configure a Foreach Loop Container](foreach-loop-container.md).  
  
4.  On the **Collection** page of the **Foreach Loop Editor**, select the Foreach File enumerator, specify the folder in which the Excel workbooks are located, and specify the file filter (ordinarily *.xls).  
  
5.  On the **Variable Mapping** page, map Index 0 to a user-defined string variable that will receive the current Excel path and file name on each iteration of the loop. (The sample expression shown later in this procedure uses the variable name `ExcelFile`.)  
  
6.  Close the **Foreach Loop Editor**.  
  
7.  Add an Excel connection manager to the package as described in [Add, Delete, or Share a Connection Manager in a Package](../add-delete-or-share-a-connection-manager-in-a-package.md). Select an existing Excel workbook file for the connection to avoid validation errors.  
  
    > [!IMPORTANT]  
    >  To avoid validation errors as you configure tasks and data flow components that use this Excel connection manager, select an existing Excel workbook in the **Excel Connection Manager Editor**. The connection manager will not use this workbook at run time after you configure an expression for the `ConnectionString` property as described in the following steps. After you create and configure the package, you can clear the value of the `ConnectionString` property in the Properties window. However, if you clear this value, the connection string property of the Excel connection manager is no longer valid until the Foreach Loop runs. Therefore you must set the `DelayValidation` property to `True` on the tasks in which the connection manager is used, or on the package, to avoid validation errors.  
    >   
    >  You must also use the default value of `False` for the `RetainSameConnection` property of the Excel connection manager. If you change this value to `True`, each iteration of the loop will continue to open the first Excel workbook.  
  
8.  Select the new Excel connection manager, click the **Expressions** property in the Properties window, and then click the ellipsis.  
  
9. In the **Property Expressions Editor**, select the `ConnectionString` property, and then click the ellipsis.  
  
10. In the Expression Builder, enter the following expression:  
  
    ```  
    "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +  @[User::ExcelFile] + ";Extended Properties=\"" + @[User::ExtProperties] + "\""  
    ```  
  
     Note the use of the escape character "\\" to escape the inner quotation marks required around the value of the Extended Properties argument.  
  
     The Extended Properties argument is not optional. If you do not use a variable to contain its value, then you must add it manually to the expression, as in the following example for an Excel 2003 file:  
  
    ```  
    "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +  @[User::ExcelFile] + ";Extended Properties=Excel 8.0"  
    ```  
  
11. Create tasks in the Foreach Loop container that use the Excel connection manager to perform the same operations on each Excel workbook that matches the specified file location and pattern.  
  
### To loop through Excel tables by using the Foreach ADO.NET Schema Rowset enumerator  
  
1.  Create an ADO.NET connection manager that uses the Microsoft Jet OLE DB Provider to connect to an Excel workbook. On the All page of the **Connection Manager** dialog box, make sure that you enter Excel 8.0 as the value of the Extended Properties property. For more information, see [Add, Delete, or Share a Connection Manager in a Package](../add-delete-or-share-a-connection-manager-in-a-package.md).  
  
2.  Create a string variable that will receive the name of the current table on each iteration of the loop.  
  
3.  Add a Foreach Loop container to the **Control Flow** tab. For information about how to configure the Foreach Loop container, see [Configure a Foreach Loop Container](foreach-loop-container.md).  
  
4.  On the **Collection** page of the **Foreach Loop Editor**, select the Foreach ADO.NET Schema Rowset enumerator.  
  
5.  As the value of **Connection**, select the ADO.NET connection manager that you created previously.  
  
6.  As the value of **Schema**, select Tables.  
  
    > [!NOTE]  
    >  The list of tables in an Excel workbook includes both worksheets (which have the $ suffix) and named ranges. If you have to filter the list for only worksheets or only named ranges, you may have to write custom code in a Script task for this purpose. For more information, see [Working with Excel Files with the Script Task](script-task.md).  
  
7.  On the **Variable Mappings** page, map Index 2 to the string variable created earlier to hold the name of the current table.  
  
8.  Close the **Foreach Loop Editor**.  
  
9. Create tasks in the Foreach Loop container that use the Excel connection manager to perform the same operations on each Excel table in the specified workbook. If you use a Script Task to examine the enumerated table name or to work with each table, remember to add the string variable to the ReadOnlyVariables property of the Script task.  
  
## See Also  
 [Import data from Excel or export data to Excel with SQL Server Integration Services (SSIS)](../load-data-to-from-excel-with-ssis.md)
 [Configure a Foreach Loop Container](foreach-loop-container.md)   
 [Add or Change a Property Expression](../expressions/add-or-change-a-property-expression.md)   
 [Excel Connection Manager](../connection-manager/excel-connection-manager.md)   
 [Excel Source](../data-flow/excel-source.md)   
 [Excel Destination](../data-flow/excel-destination.md)   
 [Working with Excel Files with the Script Task](script-task.md)  
  
  
