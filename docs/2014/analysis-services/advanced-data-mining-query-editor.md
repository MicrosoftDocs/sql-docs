---
title: "Advanced Data Mining Query Editor | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 27e7fc46-689d-43a4-9647-1c27d182bdd6
author: minewiskan
ms.author: owend
manager: craigg
---
# Advanced Data Mining Query Editor
  The **Data Mining Query Advanced Editor** is a tool to help you build custom models and queries.  
  
 The editor provides a set of templates with clickable links. Just click each link, and use the dialog boxes to select objects or values and build complex Data Mining Extensions (DMX) statements. You can switch the view to text editing model to modify the DMX statement manually.  
  
 To get to the **Data Mining Query Advanced Editor**, click **Query** and then click **Advanced**.  
  
## UIElement List  
 **DMX Query pane**  
 Here you can see the current DMX statement.  
  
 Right-click in the pane to copy the current DMX statement.  
  
 You can click any highlighted part of the statement to get options specific to that clause. For example, to delete, add, or edit an output, right-click the **\<Output>** link.  
  
 **Edit Query/Query Builder**  
 Use this button to switch the editor between a text editor, where you can write DMX statements directly; and the **Query Builder**, which helps you build a DMX statement.  
  
> [!NOTE]  
>  **Warning:** If you switch views before the query has been run, a message appears stating that you might lose some changes. If the DMX statement is valid, in many cases the **Query Builder** might successfully convert these changes. However, if you have built a particularly complex DMX statement, you should definitely save your work before switching views.  
  
 **DMX Templates**  
 Click and select from a list of templates that contain DMX examples. The templates provide just about every type of model or prediction query that you might need, including queries that use nested tables, and DMX statements to manage models. Even if you know some DMX, the templates can save you time by getting the syntax right.  
  
 **Choose Model**  
 Click to view a list of data mining models available on the current connection.  
  
 You can also display a list of available models by clicking on the model name in the DMX statement in the **DMX Query** pane. The model name is typically highlighted in red.  
  
 **Select Input**  
 Click to choose the data to use as input to the mining model. If no data source has been specified, you can also click the **\<Input>** link, which is highlighted in red in the **DMX Query** pane.  
  
 Select **@InputRowset** from the dropdown list to open the **Replace InputRowset** dialog box and modify an existing input.  
  
 Select **Add Input** to open the **Add Input** dialog box and specify a new data source.  
  
 You can also modify an existing input by clicking the **@InputRowset** link, which is highlighted in red in the DMX Query pane.  
  
 **Map Columns**  
 Select columns from the mining model and then map them to columns in the external data source.  
  
 You can also click the highlighted **\<Mapping>** link in the DMX Query pane.  
  
 **Add Output**  
 Click to choose the columns that should be output as part of a prediction query.  
  
 You can also click the highlighted **\<Add Output>** link in the DMX Query pane.  
  
 **Model Columns**  
 Lists the columns in the selected mining model. A diamond mark next to the column name indicates that the column is a predictable column.  
  
 **Input Columns**  
 Lists the columns from the external data source that have been added as inputs.  
  
## See Also  
 [Query &#40;SQL Server Data Mining Add-ins&#41;](query-sql-server-data-mining-add-ins.md)  
  
  
