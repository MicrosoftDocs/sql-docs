---
title: "Create a Content Query on a Mining Model | Microsoft Docs"
ms.date: 05/01/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: data-mining
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Create a Content Query on a Mining Model
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  You can query the mining model content programmatically by using AMO or XML/A, but it is easier to create queries by using DMX. You can also create queries against the data mining schema rowsets by establishing a connection to the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance and creating a query using the DMVs provided by [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
 The following procedures demonstrate how to create queries against a mining model by using DMX, and how to query the data mining schema rowsets.  
  
 For an example of how to create a similar query by using XML/A, see [Create a Data Mining Query by Using XMLA](../../analysis-services/data-mining/create-a-data-mining-query-by-using-xmla.md).  
  
## Querying Data Mining Model Content by Using DMX  
  
#### To create a DMX model content query  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], on the **View** menu, click **Template Explorer**.  
  
2.  In the **Template Explorer** pane, click the cube icon to change the list and display Analysis Services templates.  
  
3.  In the list of template categories, expand **DMX**, expand **Model Content**, and double-click **Content Query**.  
  
4.  In the **Connect to Analysis Services** dialog box, select the instance that contains the mining model you want to query, and click **Connect**.  
  
     The **Content Query** template opens in the appropriate code editor. The metadata pane lists the models that are available in the current database. To change the database, select a different database from the **Available Databases** list.  
  
5.  Enter the name of a mining model in the line, `FROM` [*\<mining model, name, MyModel>*]`.CONTENT`. If the mining model name contains spaces, you must enclose the name in brackets.  
  
     If you don't want to type the name, you can select a mining model in **Object Explorer** and drag it into the template.  
  
6.  In the line, `SELECT`*\<select list, expr list, \*>*, type the names of columns in the mining model content schema rowset.  
  
     To view a list of columns that you can return in mining model content queries, see [Mining Model Content &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-analysis-services-data-mining.md).  
  
7.  Optionally, type a condition in the WHERE clause of the template to restrict the rows returned to specific nodes or values.  
  
8.  Click **Execute**.  
  
## Querying the Data Mining Schema Rowsets  
  
#### To create a query against the data mining schema rowset  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], on the **New Query** toolbar, click **Analysis Services DMX Query**, or **Analysis Services MDX query**.  
  
2.  In the **Connect to Analysis Services** dialog box, select the instance that contains the objects you want to query, and click **Connect**.  
  
     The **Content Query** template opens in the appropriate code editor. The metadata pane lists the objects that are available in the current database. To change the database, select a different database from the **Available Databases** list.  
  
3.  In the query editor, type the following:  
  
     `SELECT *`  
  
     `FROM $system.DMSCHEMA_MINING_MODEL_CONTENT`  
  
     `WHERE MODEL_NAME = '<model name>'`  
  
4.  Click **Execute**.  
  
     The Results pane displays the contents of the model.  
  
    > [!NOTE]  
    >  To view a list of all the schema rowsets that you can query on the current instance, use this query: `SELECT * FROM $system.`DISCOVER_SCHEMA_ROWSETS. Or, for a list of schema rowsets specific to data mining, see [Data Mining Schema Rowsets](https://docs.microsoft.com/bi-reference/schema-rowsets/data-mining/data-mining-schema-rowsets).  
  
## See Also  
 [Mining Model Content &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-analysis-services-data-mining.md)   
 [Data Mining Schema Rowsets](https://docs.microsoft.com/bi-reference/schema-rowsets/data-mining/data-mining-schema-rowsets)  
  
  
