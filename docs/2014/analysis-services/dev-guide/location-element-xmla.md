---
title: "Location Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "Location Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "microsoft.xml.analysis.location"
  - "urn:schemas-microsoft-com:xml-analysis#Location"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Location"
helpviewer_keywords: 
  - "Location element"
ms.assetid: cea5e776-f435-425a-9bce-812d727a2b71
caps.latest.revision: 12
author: "mgblythe"
ms.author: "mblythe"
manager: "mblythe"
---
# Location Element (XMLA)
  Contains information about a remote server for the parent [Backup](../../../2014/analysis-services/dev-guide/backup-element-xmla.md), [Restore](../../../2014/analysis-services/dev-guide/restore-element-xmla.md), or [Synchronize](../../../2014/analysis-services/dev-guide/synchronize-element-xmla.md) command.  
  
## Syntax  
  
```xml  
  
<Backup> <!-- or one of the elements listed below in the Element Relationships table -->  
   ...  
   <Location>  
```  
  
```  
  
<File>...</File> <!-- for Backup and Restore only -->  
<DataSourceID>...</DataSourceID>  
<DataSourceType>...</DataSourceType> <!-- for Restore and Synchronize only -->  
<ConnectionString>...</ConnectionString> <!-- for Restore and Synchronize only -->  
<Folders>...</Folders> <!-- for Restore and Synchronize only -->  
```  
  
```  
  
   </Location>  
   ...  
</Backup>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Backup](../../../2014/analysis-services/dev-guide/backup-element-xmla.md), [Restore](../../../2014/analysis-services/dev-guide/restore-element-xmla.md), [Synchronize](../../../2014/analysis-services/dev-guide/synchronize-element-xmla.md)|  
  
## Child elements  
  
|Ancestor or Parent|Child Element|  
|------------------------|-------------------|  
|[Backup](../../../2014/analysis-services/dev-guide/backup-element-xmla.md)|[DataSourceID](../../../2014/analysis-services/dev-guide/datasourceid-element-xmla.md), [File](../../../2014/analysis-services/dev-guide/file-element-xmla.md)|  
|[Restore](../../../2014/analysis-services/dev-guide/restore-element-xmla.md)|[ConnectionString](../../../2014/analysis-services/dev-guide/connectionstring-element-xmla.md), [DataSourceID](../../../2014/analysis-services/dev-guide/datasourceid-element-xmla.md), [DataSourceType](../../../2014/analysis-services/dev-guide/datasourcetype-element-xmla.md), [File](../../../2014/analysis-services/dev-guide/file-element-xmla.md), [Folders](../../../2014/analysis-services/dev-guide/folders-element-xmla.md)|  
|[Synchronize](../../../2014/analysis-services/dev-guide/synchronize-element-xmla.md)|[ConnectionString](../../../2014/analysis-services/dev-guide/connectionstring-element-xmla.md), [DataSourceID](../../../2014/analysis-services/dev-guide/datasourceid-element-xmla.md), [DataSourceType](../../../2014/analysis-services/dev-guide/datasourcetype-element-xmla.md), [Folders](../../../2014/analysis-services/dev-guide/folders-element-xmla.md)|  
  
## Remarks  
 For `Backup` commands, the `Location` element provides information about creating a remote backup file for a remote instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].  
  
 For `Restore` commands, the `Location` element provides information about identifying and connecting to a remote [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance, as well as the remote backup file used to restore remote partitions on that remote instance.  
  
 For `Synchronize` commands, the `Location` element describes either a data source to be used by the target instance or a remote instance defined on the source instance that must be synchronized with the target instance, depending on the value of the `DataSourceType` element for the parent `Synchronize` command.  
  
 For more information about backing up and restoring remote instances, see [Backing Up and Restoring Objects (XMLA)](../../../2014/analysis-services/dev-guide/backing-up-restoring-and-synchronizing-databases-xmla.md).  
  
## See Also  
 [BackupRemotePartitions Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/backupremotepartitions-element-xmla.md)   
 [Properties &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/properties-xmla.md)  
  
  