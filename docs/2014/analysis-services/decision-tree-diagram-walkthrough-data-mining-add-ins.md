---
title: "Decision Tree Diagram Walkthrough  (Data Mining Add-ins) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "shapes, data mining"
  - "diagram, decision tree"
  - "Visio shapes, decision tree"
  - "decision tree [data mining]"
ms.assetid: 9566f6a2-c750-4125-ba5e-42c7251a78c7
author: minewiskan
ms.author: owend
manager: craigg
---
# Decision Tree Diagram Walkthrough  (Data Mining Add-ins)
  If you have created a decision tree model, you can create a customized diagram in Visio by using either the Decision Tree shape or the Dependency Network shape. This topic describes the customizations you can perform using the **Decision Tree** shape and these controls:  
  
-   Rendering controls for the decision tree diagram  
  
     These options are part of the **Decision Tree Wizard** that is launched when you drop a shape into the Visio workspace.  
  
-   **Data Mining Layout** toolbar  
  
     These options are added to the Visio workspace to help you interact with the shape.  
  
## Build a Decision Tree Diagram  
 You drop the Decision Tree shape onto the Visio page to launch the **Decision Tree Visio Shape Wizard** and set diagram options.  
  
#### Use the Decision Tree Wizard  
  
1.  If you do not see **Microsoft Data Mining Shapes** in the **Shapes** list, click **More Shapes**, select **Open Stencil**, and open the template from the default installation location.  
  
     \<drive>:\Program files (x85)\Microsoft SQL Server 2012 DM Add-Ins  
  
2.  Drag the **Decision Tree** shape onto the page.  
  
3.  On the welcome page of the **Decision Tree Visio Shape Wizard**, click **Next**.  
  
4.  On the **Select a Data Source** page of the **Cluster Wizard**, choose a connection to an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] server that has the model you want to visualize.  
  
5.  Select an appropriate mining model, and click **Next**.  
  
     This diagram type supports models created using the Decision Trees or Linear Regression algorithm.  
  
6.  On the **Select Decision Tree** page, choose an individual tree to display.  
  
     A tree models the interactions that lead to a particular outcome you've modeled; therefore, even if your model contains multiple outcomes, you can display only one tree at a time.  
  
7.  In the **Select Decision Tree** dialog box, you can also set these rendering options:  
  
     **Maximum Rendering Depth**  
     Use this option to control how many nodes are displayed.  
  
     A decision tree might go very deep, creating a diagram that does not fit on the page. Use this control to focus on the leftmost nodes, which are more important.  
  
     The default is three levels of nodes.  
  
     **Select color and display text for values**  
     Choose the color that will represent each one of the outcomes. If you do not specify colors, they are automatically generated using the current video theme colors.  
  
8.  Click **Advanced** to customize the following options for each of the nodes in the decision tree diagram.  
  
     **Shading Variable** and **State**  
     Choose the target outcome that you want to show in the tree diagram.  
  
     **Show Histogram**  
     Adds a chart to each node that shows the rules that define that node.  
  
     **Show Label**  
     Adds column names to the histogram.  
  
     **Show Probability**  
     Displays the probability of each value.  
  
     **Show Support**  
     Displays the support for each value.  
  
     **Show Footer**  
     Adds a footer that sums all displayed values.  
  
     **Show Header**  
     Adds column headings.  
  
     **Show states with zero support**  
     Lets you display missing values.  
  
9. Click **Finish** to create the graph.  
  
    > [!WARNING]  
    >  In some environments, connectors in the chart might fail to render in Office 2013.  
  
## Explore and Modify the Finished Diagram  
 After you have completed the **Decision Tree Visio Shape Wizard**, Visio creates a tree diagram on the page that graphically displays the rules that lead to the predicted outcome.  
  
 You can continue to modify the shape by using the following controls for decision tree diagrams:  
  
#### Using the decision tree option menus  
  
1.  Click the **Add-Ins** ribbon, and then display one of the custom toolbars used for working with data mining diagrams:  
  
     **Layout**  
     Optimizes the arrangement of the tree to fit the current page.  
  
     **Resize Page**  
     This control was intended for earlier HTML versions. Use the Visio page resizing controls instead,  
  
     **Description**  
     When a node of the tree is selected, click this option to display details about the cases in the node.  
  
2.  Use the **Re-Layout Page** option in the Visio **Design** ribbon to experiment with different tree layouts.  
  
3.  Use the **Connectors** option on the **Design** tab to change the connectors used between nodes in the tree.  
  
4.  Use the **Pan and Zoom** control, in the **Task Pane** area of the Visio **View** ribbon, to focus on a particular area of the diagram.  
  
5.  Right-click any node in the tree, and choose from these shortcut menu options specific to decision tree diagrams:  
  
     **Improve Page Layout**  
     Distributes the nodes evenly on the page and adjusts the page view to see all nodes.  
  
     **Move Children to New Page**  
     Moves the children of the currently selected node to a new page.  
  
     **Collapse Child Nodes**  
     Hides the children of the currently selected node.  
  
     **Expand Child Nodes**  
     Displays the children of the currently selected node.  
  
## See Also  
 [Troubleshooting Visio Data Mining Diagrams &#40;SQL Server Data Mining Add-ins&#41;](troubleshooting-visio-data-mining-diagrams-sql-server-data-mining-add-ins.md)  
  
  
