---
title: "DMX Templates | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 2a577e52-821d-4bd3-ba35-075a6be285c9
author: minewiskan
ms.author: owend
manager: craigg
---
# DMX Templates
  The data mining templates help you quickly build sophisticated queries. Although the general syntax for DMX queries is well documented, using the templates makes it easier to build queries by clicking and pointing to arguments and data sources.  
  
## Using the Templates  
  
1.  In the Data Mining Client for Excel, click **Query**.  
  
2.  On the wizard **Start** page, click **Next**.  
  
3.  On the page, **Select Model**, click **Advanced**.  
  
     **Tip:** If you are going to create a prediction query on a model, you can select the model first, and then click **Advanced**, to prepopulate the template with the model name.  
  
4.  In the **Data Mining Advanced Query Editor**, click **DMX Templates**, and select a template.  
  
5.  Press ENTER to load the template into the DMX Query pane.  
  
6.  Continue clicking the links in the template, and as the dialog box appears, select an appropriate output, model, or parameter.  
  
     For prediction queries, choose the input dataset first, and then map the columns.  
  
7.  Click **Edit Query** to switch to text editor view and manually change the query.  
  
     Be aware, however, that if you switch views when working in the query editor, any information that you had in the previous view will be cleared. Before changing views, save your work by copying and pasting the DMX statements to a separate file.  
  
8.  Click **Finish**. In the **Choose Destination** dialog  box, specify where you want the result saved. [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
> [!NOTE]  
>  If you executed a statement successfully, the DMX statement that you sent to the server is also recorded in the **Trace** window. For more information about how to use the Trace feature, see [Trace &#40;Data Mining Client for Excel&#41;](trace-data-mining-client-for-excel.md).  
  
 For more information about how to use the Data Mining Advanced Query Editor, see [Query &#40;SQL Server Data Mining Add-ins&#41;](query-sql-server-data-mining-add-ins.md) and [Advanced Data Mining Query Editor](advanced-data-mining-query-editor.md).  
  
## List of DMX Templates  
 The following DMX templates are included in the Data Mining Client for Excel.  
  
 **Prediction**  
  
 Use these templates to create advanced prediction queries, including queries not supported by the wizards in the add-ins, such as queries that use nested tables or external data sources.  
  
-   Filtered predictions  
  
-   Filtered nested predictions  
  
-   Nested predictions  
  
-   Singleton predictions  
  
-   Standard predictions  
  
-   Time series predictions  
  
-   TOP prediction query  
  
-   TOP prediction query on nested table  
  
 **Create**  
  
 Use these templates to build custom models or data structures. You are not limited to the models supported by the wizards - you can use any data mining algorithm supported by the instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] that you are connected to, including plug-in algorithms.  
  
-   Mining model  
  
-   Mining structure  
  
-   Mining structure with holdout  
  
-   Temporary model  
  
-   Temporary structure  
  
 **Model Properties**  
  
 Use these templates to create queries that get metadata about the model and the training set. You can also retrieve details from the model content, or get a statistical profile of the training data.  
  
-   Mining model content  
  
-   Minimum and maximum column values  
  
-   Mining structure test/training cases  
  
-   Discrete column values  
  
 **Management**  
  
 Use these templates to perform any management task supported by DMX, which includes importing and exporting models, deleting models, and clearing models or data structures.  
  
-   Clear mining model  
  
-   Clear structure and models  
  
-   Clear mining structure  
  
-   Delete mining model  
  
-   Delete mining structure  
  
-   Rename mining model  
  
-   Rename mining structure  
  
-   Train mining model  
  
-   Train nested mining structure  
  
-   Train mining structure  
  
### Requirements  
 Depending on which template you are using, you might need administrative permissions to access the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] server and execute the query.  
  
## See Also  
 [Creating a Data Mining Model](creating-a-data-mining-model.md)  
  
  
