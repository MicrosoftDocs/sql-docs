---
title: "Create a Singleton Prediction Query from a Template | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "singleton query predictions [DMX]"
ms.assetid: e0a68ab0-bece-4d25-b464-47f1719302e6
author: minewiskan
ms.author: owend
manager: craigg
---
# Create a Singleton Prediction Query from a Template
  A singleton query is useful when you have a model that you want to use for prediction, but don't want to map it to an external input data set or make bulk predictions. With a singleton query, you can provide a value or values to the model and instantly see the predicted value.  
  
 For example, the following DMX query represents a singleton query against the targeted mailing model, TM_Decision_Tree.  
  
```  
SELECT * FROM [TM_Decision_tree] ;  
NATURAL PREDICTION JOIN  
(SELECT '2' AS [Number Children At Home], '45' as [Age])  
AS [t]  
```  
  
 The procedure that follows describes how to use the Template Explorer in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to quickly create this query.  
  
### To open the Analysis Services templates in SQL Server Management Studio  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], on the **View** menu, click **Template Explorer**.  
  
2.  Click the cube icon to open the **Analysis Server**templates.  
  
### To open a prediction query template  
  
1.  In **Template Explorer**, in the list of Analysis Server templates, expand **DMX**, and thenexpand **Prediction Queries**.  
  
2.  Double-click **Singleton Prediction**.  
  
3.  In the **Connect to Analysis Services** dialog box, type the name of the server that has the instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] that contains the mining model to be queried.  
  
4.  Click **Connect**.  
  
5.  The template opens in the specified database, together with a mining model Object Browser that contains data mining functions and a list of data mining structures and related models.  
  
### To customize the singleton query template  
  
1.  In the template, click the **Available Databases** drop-down list, and then select an instance of Analysis Service from the list.  
  
2.  In the **Mining Model** list, select the mining model that you want to query.  
  
     The list of columns in the mining model appears in the **Metadata** pane of the object browser.  
  
3.  On the **Query** menu, select **Specify Values for Template Parameters**.  
  
4.  In the **select list** row, type * to return all columns, or type a comma-delimited list of columns and expressions to return specific columns.  
  
     If you type *, the predictable column is returned, together with any columns for which you provide new values for in step 6.  
  
     For the sample code shown at the start of this topic, the **select list** row was set to *.  
  
5.  In the **mining model** row, type the name of the mining model from among the list of mining models that appear in **Object Explorer**.  
  
     For the sample code shown at the start of this topic, the **mining model** row was set to the name, `TM_Decision_Tree`.  
  
6.  In the **value** row, type the new data value for which you want to make a prediction.  
  
     For the sample code shown at the start of this topic, the **value** row was set to `2` to predict bike buying behavior based on the number of children at home.  
  
7.  In the **column** row, type the name of the column in the mining model to which the new data should be mapped.  
  
     For the sample code shown at the start of this topic, the **column** row was set to `Number Children at Home`.  
  
    > [!NOTE]  
    >  When you use the **Specify Values for Template Parameters** dialog box, you do not have to add square brackets around the column name. The brackets will automatically be added for you.  
  
8.  Leave the **input alias** as `t`.  
  
9. [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
10. In the query text pane, find the red squiggle under the comma and ellipsis that indicates a syntax error. Delete the ellipsis, and add any additional query condition that you want. If you do not add any other conditions, delete the comma.  
  
     For the sample code shown at the start of this topic, the additional query condition was set to `'45' as [Age]`.  
  
11. Click **Execute**.  
  
## See Also  
 [Creating Predictions &#40;Basic Data Mining Tutorial&#41;](../../tutorials/creating-predictions-basic-data-mining-tutorial.md)  
  
  
