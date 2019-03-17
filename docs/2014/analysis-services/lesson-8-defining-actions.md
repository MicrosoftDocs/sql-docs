---
title: "Lesson 8: Defining Actions | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 15459396-83c9-48a0-b10a-99ae38768c79
author: minewiskan
ms.author: owend
manager: craigg
---
# Lesson 8: Defining Actions
  In this lesson, you will learn to define actions in your [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] project. An action is just a Multidimensional Expressions (MDX) statement that is stored in [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] and which can be incorporated into client applications and started by a user.  
  
> [!NOTE]  
>  Completed projects for all of the lessons in this tutorial are available online. You can jump ahead to any lesson by using the completed project from the previous lesson as a starting point. [Click here](https://go.microsoft.com/fwlink/?LinkID=221866) to download the sample projects that go with this tutorial.  
  
 [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] supports the types of actions that are described in the following table.  
  
|||  
|-|-|  
|CommandLine|Executes a command at the command prompt|  
|Dataset|Returns a dataset to a client application.|  
|Drillthrough|Returns a drillthrough statement as an expression, which the client executes to return a rowset|  
|Html|Executes an HTML script in an Internet browser|  
|Proprietary|Performs an operation by using an interface other than those listed in this table.|  
|Report|Submits a parameterized URL-based request to a report server and returns a report to a client application.|  
|Rowset|Returns a rowset to a client application.|  
|Statement|Runs an OLE DB command.|  
|URL|Displays a dynamic Web page in an Internet browser.|  
  
 Actions let users start an application or perform other steps within the context of a selected item. For more information, see [Actions &#40;Analysis Services - Multidimensional Data&#41;](multidimensional-models/actions-analysis-services-multidimensional-data.md), [Actions in Multidimensional Models](multidimensional-models/actions-in-multidimensional-models.md)  
  
> [!NOTE]  
>  For examples of actions, see the action examples on the Templates tab in the Calculation Tools pane or in the examples in the [!INCLUDE[ssSampleDBCoShort](../includes/sssampledbcoshort-md.md)] DW sample data warehouse. For more information about installing this database, see [Install Sample Data and Projects for the Analysis Services Multidimensional Modeling Tutorial](install-sample-data-and-projects.md).  
  
 This lesson includes the following task:  
  
 [Defining and Using a Drillthrough Action](lesson-8-1-defining-and-using-a-drillthrough-action.md)  
 In this task, you define, use, and then modify a drillthrough action through the fact dimension relationship that you defined earlier in this tutorial.  
  
## Next Lesson  
 [Lesson 9: Defining Perspectives and Translations](lesson-9-defining-perspectives-and-translations.md)  
  
## See Also  
 [Analysis Services Tutorial Scenario](analysis-services-tutorial-scenario.md)   
 [Multidimensional Modeling &#40;Adventure Works Tutorial&#41;](multidimensional-modeling-adventure-works-tutorial.md)   
 [Actions &#40;Analysis Services - Multidimensional Data&#41;](multidimensional-models/actions-analysis-services-multidimensional-data.md)   
 [Actions in Multidimensional Models](multidimensional-models/actions-in-multidimensional-models.md)  
  
  
