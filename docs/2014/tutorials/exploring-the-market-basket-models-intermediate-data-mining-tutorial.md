---
title: "Exploring the Market Basket Models (Intermediate Data Mining Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: analysis-services
ms.topic: conceptual
ms.assetid: da1c9cb7-6c32-4b9b-96ec-ecea772aeb77
author: minewiskan
ms.author: owend
manager: kfile
---
# Exploring the Market Basket Models (Intermediate Data Mining Tutorial)
  Now that you have built the `Association` model, you can explore it by using the [!INCLUDE[msCoName](../includes/msconame-md.md)] Association Viewer in the **Mining Model Viewer** tab of Data Mining Designer. This tutorial walks you through using the viewer to explore relationships between items. The viewer helps you see at a glance which products tend to appear together, and get a general idea of the emerging patterns.  
  
 The [!INCLUDE[msCoName](../includes/msconame-md.md)] Association Viewer contains three tabs: **Rules**, **Itemsets**, and **Dependency Network**. Because each tab reveals a slightly different view of the data, when you are exploring a model, you will typically switch back and forth between the different panes several times as you pursue insights.  
  
-   [Dependency Network tab](#bkmk_DepNet)  
  
-   [Itemsets tab](#bkmk_Itemsets)  
  
-   [Rules tab](#bkmk_Rules)  
  
-   [Generic Content View](#bkmk_ContentViewer)  
  
 For this tutorial, you will start on the **Dependency Network** tab, and then use the **Rules** tab and **Itemsets** tab to deepen your understanding of the relationships revealed in the viewer. You will also use the **Microsoft Generic Content Tree Viewer** to retrieve detailed statistics for individual rules or itemsets.  
  
##  <a name="bkmk_DepNet"></a> Dependency Network Tab  
 With the **Dependency Network** tab, you can investigate the interaction of the different items in the model. Each node in the viewer represents an item, while the lines between them represent rules. By selecting a node, you can see which other nodes predict the selected item, or which items the current item predicts. In some cases, there is a two-way association between items, meaning that they often appear in the same transaction. You can refer to the color legend at the bottom of the tab to determine the direction of the association.  
  
 A line connecting two items means that these items are likely to appear in a transaction together. In other words, customers are likely to buy these items together. The slider is associated with the probability of the rule. Move the slider up or down to filter out weak associations, meaning rules with low probability.  
  
 The dependency network graph shows pairwise rules, which can be represented logically as A->B, meaning if Product A is purchased, then Product B is likely. The graph cannot show rules of the type AB->C. If you move the slider to show all rules but still do not see any lines in the graph, it means that there were no pairwise rules that met the criteria of the algorithm parameters.  
  
 You can also find nodes by name, by typing the first letters of the attribute name. For more information, see [Find Node Dialog Box &#40;Mining Model Viewer&#41;](../../2014/analysis-services/find-node-dialog-box-mining-model-viewer.md).  
  
#### To open the Association mode in the Microsoft Assocaition Rules Viewer  
  
1.  In **Solution Explorer**, double-click the Association structure.  
  
2.  In Data Mining Designer, click the **Mining Model Viewer** tab.  
  
3.  Select Association from the list of mining models in the **Mining Model** dropdown list.  
  
#### To navigate the dependency graph and locate specific nodes  
  
1.  In the **Mining Model Viewer** tab, click the **Dependency Network** tab.  
  
2.  Click **Zoom In** several times, until you can easily view the labels for each node.  
  
     By default, the graph displays with all nodes visible. In a complex model, there may be many nodes, making each node quite small.  
  
3.  Click the **+** sign in the lower right-hand corner of the viewer and hold down the mouse button to pan around the graph.  
  
4.  On the left side of the viewer, drag the slider down, moving it from **All Links** (the default) to the bottom of the slider control.  
  
5.  The viewer updates the graph to now show only the strongest association, between the Touring Tire and Touring Tire Tube items.  
  
6.  Click the node labeled **Touring Tire Tube = Existing**.  
  
     The graph is updated to highlight only items that are strongly related to this item. Note the direction of the arrow between the two items.  
  
7.  On the left side of the viewer, drag the slider up again, moving it from the bottom to around the middle.  
  
     Note the changes in the arrow that connects the two items.  
  
8.  Select **Show attribute name only** from the dropdown list at the top of the Dependency Network pane.  
  
     The text labels in the graph are updated to show only the model name.  
  
 [Back to Top](#bkmk_DepNet)  
  
##  <a name="bkmk_Itemsets"></a> Itemsets Tab  
 Next, you will learn more about the rules and itemsets generated by the model for the Touring Tire and Touring Tire Tube products. The **Itemsets** tab displays three important pieces of information that relate to the itemsets that the [!INCLUDE[msCoName](../includes/msconame-md.md)] Association algorithm discovers:  
  
-   **Support:** The number of transactions in which the itemset occurs.  
  
-   **Size:** The number of items in the itemset.  
  
-   **Items:** A list of the items included in each itemset.  
  
 Depending on how the algorithm parameters are set, the algorithm might generate many itemsets. Each itemset that is returned in the viewer represents transactions in which the item was sold. By using the controls at the top of the **Itemsets** tab, you can filter the viewer to show only the itemsets that contain a specified minimum support and itemset size.  
  
 If you are working with a different mining model and no itemsets are listed, it is because no itemsets met the criteria of the algorithm parameters. In such a scenario, you can change the algorithm parameters to allow itemsets that have lower support.  
  
#### To filter the itemsets that are shown in the viewer by name  
  
1.  Click the **Itemsets** tab of the viewer.  
  
2.  In the **Filter Itemset** box, type `Touring Tire`, and then click outside the box.  
  
     The filter returns all items that contain this string.  
  
3.  In the **Show** list, select **Show attribute name only**.  
  
4.  Select the **Show long name** check box.  
  
     The list of itemsets is updated to show only the itemsets that contain the string Touring Tire. The long name of the itemset includes the name of the table that contains the attribute and value for each item.  
  
5.  Clear the **Show long name** check box.  
  
     The list of itemsets is updated to show only the short name.  
  
 The values in the **Support** column indicate the number of transactions for each itemset. A transaction for an itemset means a purchase that included all the items in the itemset.  
  
 By default, the viewer lists the itemsets in descending order by support. You can click on the column headers to sort by a different column, such as the itemset size or name. If you are interested in learning more about the individual transactions that are included in an itemset, you can drill through from the itemsets to the individual cases. The structure columns in the drillthrough results are the customer's income level and customer ID, which were not used in the model.  
  
#### To view details for an itemset  
  
1.  In the list of itemsets, click the **Itemset** column heading to sort by name.  
  
2.  Locate the item, `Touring Tire` (with no second item).  
  
3.  Right-click the item, `Touring Tire`, select **Drill Through**, and then select **Model and Structure Columns**.  
  
     The **Drill Through** dialog box displays the individual transactions used as support for this itemset.  
  
4.  Expand the nested table, vAssocSeqLineItems, to view the actual list of purchases in the transaction.  
  
#### To filter itemsets by support or size  
  
1.  Clear any text that might be in the **Filter Itemset** box. You cannot use a text filter together with a numeric filter.  
  
2.  In the **Minimum support** box, type 100, and then click the background of the viewer.  
  
     The list of itemsets is updated to show only itemsets with support of at least 100.  
  
 [Back to Top](#bkmk_DepNet)  
  
##  <a name="bkmk_Rules"></a> Rules Tab  
 The **Rules** tab displays the following information that is related to the rules that the algorithm finds.  
  
-   **Probability:** The *likelihood* of a rule, defined as the probability of the right-hand item given the left-hand side item.  
  
-   **Importance:** A measure of the usefulness of a rule. A greater value means a better rule.  
  
     Importance is provided to help you gauge the usefulness of a rule, because probability alone can be misleading. For example, if every transaction contains a water bottle--perhaps the water bottle is added to each customer's cart automatically as part of a promotion--the model would create a rule predicting that water bottle has a probability of 1. Based on probability alone, this rule is very accurate, but it does not provide useful information.  
  
-   **Rule:** The definition of the rule. For a market basket model, a rule describes a specific combination of items.  
  
 Each rule can be used to predict the presence of an item in a transaction based on the presence of other items. Just like in the **Itemsets** tab, you can filter the rules so that only the most interesting rules are shown. If you are working with a mining model that does not have any rules, you might want to change the algorithm parameters to lower the probability threshold for rules.  
  
#### To see only rules that include the Mountain-200 bicycle  
  
1.  In the **Mining Model Viewer** tab, click the **Rules** tab.  
  
2.  In the **Filter Rule** box, enter `Mountain-200`.  
  
     Clear the **Show long name** check box.  
  
3.  From the **Show** list, select **Show attribute name only**.  
  
     The viewer will then display only the rules that contain the words "`Mountain-200`". The probability of the rule tells you how likely it is that when someone buys a `Mountain-200` bicycle, that person will also buy the other listed product.  
  
 The rules are ordered by probability in descending order, but you can click the column headings to change the sort order. If you are interested in finding out more details about a particular rule, you can use drillthrough to view the supporting cases.  
  
#### To view cases that support a particular rule  
  
1.  In the **Rules** tab, right-click the rule that you want to view.  
  
2.  Select **Drill Through**, and then select **Model Columns Only**, or **Model and Structure Columns**.  
  
     The **Drill Through** dialog box provides a summary of the rule at the top of the pane, and a list of all cases that were used as supporting data for the rule.  
  
 [Back to Top](#bkmk_DepNet)  
  
##  <a name="bkmk_ContentViewer"></a> Generic Content Tree Viewer  
 This viewer can be used for all models, regardless of the algorithm or model type. The **Microsoft Generic Content Tree Viewer** is available from the **Viewer** drop-down list.  
  
 A content tree is a representation of a mining model as a series of nodes, where each node represents learned knowledge about some subset of the data. The node can contain a pattern, a set of rules, a cluster, or the definition of a range of dates that share some characteristics. The exact content of the node differs depending on the algorithm and the type of the predictable attribute, but the general representation of the content is the same. You can expand each node to see increasing levels of detail, and copy the content of any node to the Clipboard.  
  
#### To view details about the rule by using the content viewer  
  
1.  In the **Mining Model Viewer** tab, select **Microsoft Generic Content Tree Viewer** from the **Viewer** list.  
  
2.  In the Node Caption pane, scroll to the bottom of the list, and click the last node.  
  
     The viewer shows itemsets first and rules next, but does not group them. The easiest way to find a specific node is to create a content query. For more information, see [Association Model Query Examples](../../2014/analysis-services/data-mining/association-model-query-examples.md).  
  
3.  In the Node Details pane, review the value for NODE_TYPE and NODE_DESCRIPTION.  
  
     A node type of 8 is a rule, and a node type of 7 is an itemset. For a rule, the value of NODE_DESCRIPTION tells you the conditions that make up the rule. For an itemset, the value of NODE_DESCRIPTION tells you the items included in the itemset.  
  
 You can also create a content query to obtain detailed statistics about the rules. For more information about mining model content and how to interpret it, see [Mining Model Content for Association Models &#40;Analysis Services - Data Mining&#41;](../../2014/analysis-services/data-mining/mining-model-content-for-association-models-analysis-services-data-mining.md).  
  
 [Back to Top](#bkmk_DepNet)  
  
## Next Task in Lesson  
 [Filtering a Nested Table in a Mining Model &#40;Intermediate Data Mining Tutorial&#41;](../../2014/tutorials/filtering-a-nested-table-in-a-mining-model-intermediate-data-mining-tutorial.md)  
  
## See Also  
 [Lesson 3: Building a Market Basket Scenario &#40;Intermediate Data Mining Tutorial&#41;](../../2014/tutorials/lesson-3-building-a-market-basket-scenario-intermediate-data-mining-tutorial.md)   
 [Lesson 4: Building a Sequence Clustering Scenario &#40;Intermediate Data Mining Tutorial&#41;](../../2014/tutorials/lesson-4-build-sequence-clustering-scenario-intermediate-data-mining.md)   
 [Microsoft Association Algorithm](../../2014/analysis-services/data-mining/microsoft-association-algorithm.md)   
 [Microsoft Association Algorithm Technical Reference](../../2014/analysis-services/data-mining/microsoft-association-algorithm-technical-reference.md)  
  
  
