---
title: "Actions (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "actions [Analysis Services]"
  - "actions [Analysis Services], about actions"
  - "MDX [Analysis Services], actions"
  - "cubes [Analysis Services], actions"
  - "OLAP objects [Analysis Services], actions"
ms.assetid: 07229bb2-805c-427e-8455-69c9ca5d01e0
author: minewiskan
ms.author: owend
manager: craigg
---
# Actions (Analysis Services - Multidimensional Data)
  Actions can be of different types and they have to be created accordingly. Actions can be:  
  
-   Drillthrough actions, which return the set of rows that represents the underlying data of the selected cells of the cube where the action occurs.  
  
-   Reporting actions, which return a report from [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] that is associated with the selected section of the cube where the action occurs.  
  
-   Standard actions, which return the action element (URL, HTML, DataSet, RowSet, and other elements) that is associated with the selected section of the cube where the action occurs.  
  
 A query interface, such as ADOMD.NET, is used by the client application to retrieve and expose the actions to the end user. For more information see [Developing with ADOMD.NET](https://docs.microsoft.com/bi-reference/adomd/developing-with-adomd-net).  
  
 A simple <xref:Microsoft.AnalysisServices.Action> object is composed of: basic information, the target where the action is to occur, a condition to limit the action scope, and the type. Basic information includes the name of the action, the description of the action, the caption suggested for the action, and others.  
  
 The target is the actual location in the cube where the action is to occur. The target is composed of a target type and a target object. Target type represents the kind of object, in the cube, where the action is to be enabled. Target type could be level members, cells, hierarchy, hierarchy members, or others. The target object is a specific object of the target type; if the target type is hierarchy, then the target object is any one of the defined hierarchies in the cube.  
  
 The condition is a `Boolean` MDX expression that is evaluated at the action event. If the condition evaluates to `true`, then the action is executed. Otherwise, the action is not executed.  
  
 The type is the kind of action to be executed. <xref:Microsoft.AnalysisServices.Action> is an abstract class, therefore, to use it you have to use any one of the derived classes. Two kinds of actions are predefined: drillthrough and reporting. These have corresponding derived classes: <xref:Microsoft.AnalysisServices.DrillThroughAction> and <xref:Microsoft.AnalysisServices.ReportAction>. Other actions are covered in the <xref:Microsoft.AnalysisServices.StandardAction> class.  
  
 In [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], an action is a stored MDX statement that can be presented to and employed by client applications. In other words, an action is a client command that is defined and stored on the server. An action also contains information that specifies when and how the MDX statement should be displayed and handled by the client application. The operation that is specified by the action can start an application, using the information in the action as a parameter, or can retrieve information based on criteria supplied by the action.  
  
 Actions enable business users to act upon the outcomes of their analyses. By saving and reusing actions, end users can go beyond traditional analysis, which typically ends with presentation of data, and initiate solutions to discovered problems and deficiencies, thereby extending the business intelligence application beyond the cube. Actions can transform the client application from a sophisticated data rendering tool into an integral part of the enterprise's operational system. Instead of focusing on  sending data as input to operational applications, end users can "close the loop" on the decision-making process. This ability to transform analytical data into decisions is crucial to the successful business intelligence application.  
  
 For example, a business user browsing a cube notices that the current stock of a certain product is low. The client application provides to the business user a list of actions, all related to low product stock value, that are retrieved from the Analysis Services database, The business user selects the Order action for the member of the cube that represents the product. The Order action initiates a new order by calling a stored procedure in the operational database. This stored procedure generates the appropriate information to send to the order entry system.  
  
 You can exercise flexibility when you create actions: for example, an action can launch an application, or retrieve information from a database. You can configure an action to be triggered from almost any part of a cube, including dimensions, levels, members, and cells, or create multiple actions for the same portion of a cube. You can also pass string parameters to the launched applications and specify the captions displayed to end users as the action runs.  
  
> [!IMPORTANT]  
>  In order for a business user to use actions, the client application employed by the business user must support actions.  
  
## Types of Actions  
 The following table lists the types of actions that are included in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]:  
  
|Action Type|Description|  
|-----------------|-----------------|  
|CommandLine|Executes a command at the command prompt|  
|Dataset|Returns a dataset to a client application.|  
|Drillthrough|Returns a drillthrough statement as an expression, which the client executes to return a rowset|  
|Html|Executes an HTML script in an Internet browser|  
|Proprietary|Performs an operation by using an interface other than those listed in this table.|  
|Report|Submits a parameterized URL-based request to a report server and returns a report to a client application.|  
|Rowset|Returns a rowset to a client application.|  
|Statement|Runs an OLE DB command.|  
|URL|Displays a dynamic Web page in an Internet browser.|  
  
## Resolving and Executing Actions  
 When a business user accesses the object for which the command object is defined, the statement associated with the action is automatically resolved, which makes it available to the client application, but the action is not automatically executed. The action is executed only when the business user performs the client-specific operation that initiates the action. For example, a client applications might display a list of actions as a pop-up menu when the business user right-clicks on a particular member or cell.  
  
## See Also  
 [Actions in Multidimensional Models](actions-in-multidimensional-models.md)  
  
  
