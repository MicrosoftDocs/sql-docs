---
title: "Configure In-Memory or DirectQuery Access for a Tabular Model Database | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: a5d439a9-5be1-4145-90e8-90777d80e98b
author: minewiskan
ms.author: owend
manager: craigg
---
# Configure In-Memory or DirectQuery Access for a Tabular Model Database
  This topic describes how to change the connection properties of a tabular model that has already been deployed, to enable use of the model in Direct Query mode.  
  
 For more information about these properties, and configuration for the most common scenarios, see [DirectQuery Deployment Scenarios &#40;SSAS Tabular&#41;](../directquery-deployment-scenarios-ssas-tabular.md).  
  
## Requirements  
 Enabling the use of Direct Query mode on a tabular model is a multistep process. You must:  
  
1.  Ensure that the model does not have features which might cause validation errors in Direct Query mode.  
  
2.  Change the storage mode on the model to support Direct Query.  
  
3.  Deploy the model in a mode that supports queries in either a hybrid or pure Direct Query mode.  
  
4.  Edit the connection string on the deployed database to support Direct Query mode.  
  
 This topic assumes that you have created and validated your model, and only need to enable Direct Query access from a client such as [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)].  
  
## Procedure  
  
#### Change the Connection String Properties of the Model  
  
1.  In SQL Server Management Studio, open the instance to which you deployed the model.  
  
2.  In Object Explorer, right-click the name of the model database, and select **Properties**.  
  
3.  Locate the property, **DirectQueryMode**. To enable use of the relational data source, this property must be set to one of these values:  
  
    -   **DirectQuery**  
  
    -   **InMemoryWithDirectQuery**  
  
    -   **DirectQueryWithInMemory**  
  
  
