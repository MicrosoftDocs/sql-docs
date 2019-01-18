---
title: "Filter a Rule in an Association Rules Model | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: data-mining
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Filter a Rule in an Association Rules Model
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  You can use filtering with association models to restrict the results to just the associations that interest you. For example, you might filter the rules to show only those that include a specific product.  
  
 In Data Mining Designer, you use the controls on the **Rules** tab of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Association Rules Viewer to filter the rules that are displayed.  You can also create a query on the model to see only itemset that contains a particular value.  
  
> [!NOTE]  
>  This option is available only for mining models that have been created by using the Microsoft Association Algorithm.  
  
### Filter a rule in an association model  
  
1.  Open the mining model by using the **Association Rules Viewer**. To do this in SQL Server Management Studio, right click the model name and select **Browse**. To do this in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], double-click the mining structure that contains the model, and then click the **Mining Model Viewer** tab of **Data Mining Designer**.  
  
2.  Click the **Rules** tab of the **Association Rules Viewer**.  
  
3.  Type a rule condition into the **Filter Rule** box. For example, a rule condition might be "Bike Stand", which also returns "Bike Stands".  
  
     The **Filter Rule** text box supports regular expressions as defined by the .NET language. Therefore, you can use expressions such as the following: `((.Helmets.*Fenders.*)|(.*Fenders.*Helmets.*))`. This expression would return any itemsets that include attributes with the words Helmets and Fenders, in any order.  
  
4.  For **Minimum probability**, increase the value of probability to see fewer rules, or decrease the value to see more rules.  
  
5.  For **Minimum importance**, increase the value of importance to see fewer rules, or decrease the value to see more rules.  
  
6.  For **Show**, select one of the following options: **Show attribute name and value**, **Show attribute name only**, or **Show attribute value only**.  
  
7.  For **Maximum rows**, increase the value to increase the total number of rules that meet the specified conditions, or decrease the value to limit the number of rules returned. Rules are ordered by probability, so you might eliminate additional rules that meet the specified conditions for probability or importance.  
  
8.  Select or deselect the **Show long name** check box to toggle the way that the rules names are displayed.  
  
     The rules are now filtered to only display rules that contain the indicated item. The filter condition applies to attribute values either before or after the rule delimiter, "->".  
  
    > [!NOTE]  
    >  The viewer caches the initial list of rules, based on a query to the mining model, and does not refresh the list of rules unless you change the conditions of the query by setting the maximum rows, the probability, importance, or the display of long names. Therefore, if you type a condition and the display does not immediately refresh, you can force the viewer to refresh the data by selecting and then deselecting the **Show long names** check box.  
  
### Create a query on the itemsets in an association model  
  
-   [Association Model Query Examples](../../analysis-services/data-mining/association-model-query-examples.md)  
  
## See Also  
 [Mining Model Viewer Tasks and How-tos](../../analysis-services/data-mining/mining-model-viewer-tasks-and-how-tos.md)   
 [Browse a Model Using the Microsoft Association Rules Viewer](../../analysis-services/data-mining/browse-a-model-using-the-microsoft-association-rules-viewer.md)   
 [Lesson 3: Building a Market Basket Scenario &#40;Intermediate Data Mining Tutorial&#41;](http://msdn.microsoft.com/library/651eef38-772e-4d97-af51-075b1b27fc5a)  
  
  
