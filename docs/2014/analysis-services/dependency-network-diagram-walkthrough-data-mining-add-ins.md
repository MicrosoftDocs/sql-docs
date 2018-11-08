---
title: "Dependency Network Diagram Walkthrough (Data Mining Add-ins) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "Visio shapes, dependency network"
  - "shapes, data mining"
  - "shapes, network"
  - "dependencies"
  - "diagram, dependency network"
  - "data mining layout toolbar"
  - "dependency network"
ms.assetid: aac732a8-5262-4649-b7d7-3ccf6f9cfa8b
author: minewiskan
ms.author: owend
manager: craigg
---
# Dependency Network Diagram Walkthrough (Data Mining Add-ins)
  Several different types of data mining models use a network graph as a way of exploring relationships in the data. You can import these models into Visio using the **Dependency Network** shape and then continue to customize and enhance the layout. The **Data Mining Shapes for Visio** include the following custom controls for working with dependency network diagrams:  
  
-   Rendering controls for the network graph  
  
     These options are part of the wizard that is launched when you drop a shape into the Visio workspace.  
  
-   **Data Mining Layout** toolbar  
  
     These options are added to the Visio workspace to help you interact with the dependency network graph.  
  
## Build a Dependency Network Graph  
 You drop a shape onto the Visio page to launch the **Dependency Net Visio Shape Wizard** and set diagram options.  
  
#### Use the Dependency Net Visio Shape Wizard  
  
1.  If you do not see **Microsoft Data Mining Shapes** in the **Shapes** list, click **More Shapes**, select **Open Stencil**, and open the template from the default installation location.  
  
     \<drive>:\Program files (x85)\Microsoft SQL Server 2012 DM Add-Ins  
  
2.  Drag the **Dependency Network** shape onto the page to start the wizard. Click **Next**.  
  
3.  On the welcome page of the **Dependency Network Visio Shape Wizard**, click **Next**.  
  
4.  On the **Select a Data Source** page of the **Dependency Network Visio Shape Wizard**, choose a connection to an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] server that has the model you want to visualize.  
  
5.  Select an appropriate mining model, and click **Next**.  
  
     To select an appropriate model, you can review the name, description, columns, and type of data in the **Properties** pane.  
  
     This shape supports models created by using these algorithms:  
  
    -   Naïve Bayes  
  
    -   Decision Trees  
  
    -   Association Rules  
  
6.  On the page, **Select Nodes to Render**, customize the size of the graph, and optionally filter to only include certain nodes.  
  
     With a large dataset, a dependency graph can become huge, and contain many related objects, represented as *nodes*. By using this dialog box, you can create a custom network graph that includes only the nodes of interest.  
  
     [art placeholder]  
  
     **Number of nodes to fetch**  
     If the model contains fewer than the specified number of nodes, this setting has no effect. If the model contains more nodes than the specified number, only the strongest nodes are displayed.  
  
     **Name contains**  
     Leave this box blank and click the green arrow to retrieve all nodes in the model.  
  
     Or, type a string and click the green arrow to return only those nodes that contain the specified string.  
  
     Most of the models that you can create with the sample data are not big, so for this example, increase the number of nodes to 10, and then click the green arrow to run a query and get the list of all nodes.  
  
7.  Click **Advanced** to set shading and shape options for the graph.  
  
8.  Click **Close** to exit the **Advanced** options dialog box, and then click **Finish** to create the graph.  
  
## Explore and Modify the Finished Diagram  
 After Visio has created the network dependency graph, you can continue to modify and enhance the graph by using the features in Visio.  
  
 The following graphic shows the default layout of a dependency network graph.  
  
 [art placeholder]  
  
1.  Use the **Pan and Zoom** control, in the **Task Pane** area of the Visio **View** ribbon, to focus on a section of the graph and move around the diagram.  
  
2.  Experiment with different graph layouts provided by the Visio **Re-Layout Page** option.  
  
3.  Click the **Add-Ins** ribbon, and then display one of the custom toolbars used for working with data mining diagrams:  
  
     **Layout**  
     Optimizes the layout of nodes on the page, and changes the view so that all nodes are visible.  
  
     **Resize Page**  
     Changes the size of the page so that all nodes are visible.  
  
     **Edge Strength**  
     Toggles display of edge strength for the entire graph. An edge is a connection between nodes. You can use the slider control to filter out connections that are not strong.  
  
     **Slider**  
     The **Slider** helps you control the strength of the relationships that are displayed in the dependency network diagram.  
  
     Each node in the graph represents a state. An arrow represents a transition between two states and the probability that is associated with the transition. To reduce the number of nodes in the graph, move the slider bar up.  
  
     To increase the number of nodes in the graph, move the slider bar down.  
  
     **Add Items**  
     Opens the **Select Nodes to Render** dialog box so that you can select new nodes to add to the graph.  
  
4.  You can make the graphs as simple or elaborate as you like, and add annotations, while staying connected to the model.  
  
     [ art placeholder]  
  
> [!WARNING]  
>  Highlighting of dependent nodes and other shortcut menu options that were available in previous versions of the Add-Ins do not work in Office 2013.  
  
## See Also  
 [Troubleshooting Visio Data Mining Diagrams &#40;SQL Server Data Mining Add-ins&#41;](troubleshooting-visio-data-mining-diagrams-sql-server-data-mining-add-ins.md)  
  
  
