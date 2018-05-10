---
title: "Accessing Query Context in Stored Procedures | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: olap
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Accessing Query Context in Stored Procedures
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  The execution context of a stored procedure is available within the code of the stored procedure as the **Context** object of the ADOMD.NET server object model. This is a read-only context and cannot be modified by the stored procedure. The following properties are available on this object.  
  
|Property|Type|Description|  
|--------------|----------|-----------------|  
|**CurrentCube**|Cube|The cube for the current query context.|  
|**CurrentDatabaseName**|String|The identifier of the current database.|  
|**CurrentConnection**|Connection|A reference to the connection object in the current context.|  
|**Pass**|Integer|The pass number for the current context.|  
  
 The **Context** object exists when the Multidimensional Expressions (MDX) object model is used in a stored procedure. It is not available when the MDX object model is used on a client. The **Context** object is not explicitly passed to or returned by the stored procedure. It is available during the execution of the stored procedure.  
  
## See Also  
 [Multidimensional Model Assemblies Management](../../analysis-services/multidimensional-models/multidimensional-model-assemblies-management.md)   
 [Defining Stored Procedures](../../analysis-services/multidimensional-models-extending-olap-stored-procedures/defining-stored-procedures.md)  
  
  
