---
title: "DbStorageLocation Element | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: xmla
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# DbStorageLocation Element
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Specifies the folder where [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] creates and manages all the database data and metadata files.  
  
## Syntax  
  
```xml  
  
<Database>  
...  
   <ddl100_100:DbStorageLocation>...</ddl100_100:DbStorageLocation>  
...  
</Database>  
```  
  
## Element characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|""|  
|Cardinality|0-1: Optional element that can occur one time only.|  
  
## Element relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Database](../../../analysis-services/xmla/xml-elements-properties/database-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The **DbStorageLocation** database property must be set to an existing UNC folder path or an empty string. An empty string is the default server data folder. If the folder doesn't exist, an error will be raised when executing a **Create**, **Attach**, or **Alter** command.  
  
 In addition, the **DbStorageLocation** database property cannot be set to point to the server data folder or any of its subfolders. If the location points to the server data folder or any of its subfolders, an error will be raised when executing a **Create**, **Attach**, or **Alter** command.  
  
## See also
 <xref:Microsoft.AnalysisServices.Database.DbStorageLocation%2A>   
 [Attach and Detach Analysis Services Databases](../../../analysis-services/multidimensional-models/attach-and-detach-analysis-services-databases.md)  
  
  
