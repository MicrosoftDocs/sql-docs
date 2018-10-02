---
title: "DbStorageLocation Element | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
helpviewer_keywords: 
  - "DbStorageLocation element"
ms.assetid: 1f448249-103a-479f-ae86-b0017acd0436
author: minewiskan
ms.author: owend
manager: craigg
---
# DbStorageLocation Element
  Specifies the folder where [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] creates and manages all the database data and metadata files.  
  
## Syntax  
  
```xml  
  
<Database>  
...  
   <ddl100_100:DbStorageLocation>...</ddl100_100:DbStorageLocation>  
...  
</Database>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|""|  
|Cardinality|0-1: Optional element that can occur one time only.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Database](database-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The `DbStorageLocation` database property must be set to an existing UNC folder path or an empty string. An empty string is the default server data folder. If the folder doesn't exist, an error will be raised when executing a `Create`, `Attach`, or `Alter` command.  
  
 In addition, the `DbStorageLocation` database property cannot be set to point to the server data folder or any of its subfolders. If the location points to the server data folder or any of its subfolders, an error will be raised when executing a `Create`, `Attach`, or `Alter` command.  
  
## See Also  
 <xref:Microsoft.AnalysisServices.Database.DbStorageLocation%2A>   
 [Attach and Detach Analysis Services Databases](../../multidimensional-models/attach-and-detach-analysis-services-databases.md)  
  
  
