---
title: "Defining and Identifying Objects (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "objects [XML for Analysis]"
  - "identifying objects [XML for Analysis]"
  - "XML for Analysis, objects"
  - "object references [XML for Analysis]"
  - "object identifiers [XML for Analysis]"
  - "object definitions [XML for Analysis]"
  - "XMLA, objects"
ms.assetid: 43b65f6d-0123-4556-81f0-c7a0b84361e5
caps.latest.revision: 13
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Defining and Identifying Objects (XMLA)
  Objects are identified in XML for Analysis (XMLA) commands by using object identifiers and object references, and are defined by using Analysis Services Scripting Language (ASSL) elements XMLA commands.  
  
## Object Identifiers  
 An object is identified by using the unique identifier of the object as defined on an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]. Object identifiers can either be explicitly specified or determined by the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance when [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] creates the object. You can use the [Discover](../../../2014/analysis-services/dev-guide/discover-method-xmla.md) method to retrieve object identifiers for subsequent `Discover` or [Execute](../../../2014/analysis-services/dev-guide/execute-method-xmla.md) method calls.  
  
## Object References  
 Several XMLA commands, such as [Delete](../../../2014/analysis-services/dev-guide/delete-element-xmla.md) or [Process](../../../2014/analysis-services/dev-guide/process-element-xmla.md), use an object reference to refer to an object in an unambiguous manner. An object reference contains the object identifier of the object on which a command is executed and the object identifiers of the ancestors for that object. For example, the object reference for a partition contains the object identifier of the partition, as well as the object identifiers of that partition's parent measure group, cube, and database.  
  
## Object Definitions  
 The [Create](../../../2014/analysis-services/dev-guide/create-element-xmla.md) and [Alter](../../../2014/analysis-services/dev-guide/alter-element-xmla.md) commands in XMLA create or alter, respectively, objects on an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance. The definitions for those objects are represented by an [ObjectDefinition](../../../2014/analysis-services/dev-guide/objectdefinition-element-xmla.md) element that contains elements from ASSL. Object identifiers can be explicitly specified for all major and many minor objects by using the [ID](../../../2014/analysis-services/dev-guide/id-element-xmla.md) element. If the `ID` element is not used, the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance provides a unique identifier, with a naming convention that depends on the object to be identified. For more information about how to use the `Create` and `Alter` commands to define objects, see [Creating and Altering Objects &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/creating-and-altering-objects-xmla.md).  
  
## See Also  
 [Object Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/object-element-xmla.md)   
 [ParentObject Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/parentobject-element-xmla.md)   
 [Source Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/source-element-xmla.md)   
 [Target Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/target-element-xmla.md)   
 [Developing with XMLA in Analysis Services](../../../2014/analysis-services/dev-guide/developing-with-xmla-in-analysis-services.md)  
  
  