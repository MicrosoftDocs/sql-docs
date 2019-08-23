---
title: "Accessing Query Context in Stored Procedures | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: "analysis-services"
ms.topic: "reference"
helpviewer_keywords: 
  - "execution context [Analysis Services]"
  - "stored procedures [Analysis Services], query context"
  - "Context object"
  - "query context [Analysis Services]"
ms.assetid: bdc7dad8-2f22-4265-aba4-a3a451527840
author: minewiskan
ms.author: owend
manager: craigg
---
# Accessing Query Context in Stored Procedures
  The execution context of a stored procedure is available within the code of the stored procedure as the `Context` object of the ADOMD.NET server object model. This is a read-only context and cannot be modified by the stored procedure. The following properties are available on this object.  
  
|Property|Type|Description|  
|--------------|----------|-----------------|  
|**CurrentCube**|Cube|The cube for the current query context.|  
|**CurrentDatabaseName**|String|The identifier of the current database.|  
|**CurrentConnection**|Connection|A reference to the connection object in the current context.|  
|**Pass**|Integer|The pass number for the current context.|  
  
 The `Context` object exists when the Multidimensional Expressions (MDX) object model is used in a stored procedure. It is not available when the MDX object model is used on a client. The `Context` object is not explicitly passed to or returned by the stored procedure. It is available during the execution of the stored procedure.  
  
## See Also  
 [Multidimensional Model Assemblies Management](../multidimensional-models/multidimensional-model-assemblies-management.md)   
 [Defining Stored Procedures](../multidimensional-models-extending-olap-stored-procedures/defining-stored-procedures.md)  
  
  
