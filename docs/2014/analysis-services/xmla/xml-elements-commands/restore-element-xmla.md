---
title: "Restore Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Restore Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Restore"
  - "urn:schemas-microsoft-com:xml-analysis#Restore"
  - "microsoft.xml.analysis.restore"
helpviewer_keywords: 
  - "Restore command"
ms.assetid: bb5a0c92-3927-4fa4-975b-6e4d79e0a912
author: minewiskan
ms.author: owend
manager: craigg
---
# Restore Element (XMLA)
  Restores a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database from a backup file.  
  
## Syntax  
  
```xml  
  
<Command>  
   <Restore>  
      <DatabaseName>...</DatabaseName>  
      <DatabaseID>...</DatabaseID>  
      <File>...</File>  
      <Security>...</Security>  
      <AllowOverwrite>...</AllowOverwrite>  
      <Password>...</Password>  
      <Locations>...</Locations>  
      <DbStorageLocation>...</DbStorageLocation>  
   </Restore>  
</Command>  
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
|Parent elements|[Command](../xml-elements-properties/command-element-xmla.md)|  
|Child elements|[AllowOverwrite](../xml-elements-properties/allowoverwrite-element-xmla.md), [DatabaseName](../xml-elements-properties/name-element-xmla.md), [DatabaseID](../xml-elements-properties/id-element-xmla.md), [File](../xml-elements-properties/file-element-xmla.md), [Locations](../xml-elements-properties/locations-element-xmla.md), [Password](../xml-elements-properties/password-element-xmla.md), [Security](../xml-elements-properties/security-element-xmla.md), [DbStorageLocation](../xml-elements-properties/dbstoragelocation-element.md)|  
  
## Remarks  
 The `Restore` command restores an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database specified in the `DatabaseName` element from a backup file and optionally restores remote partitions from remote backup files.  
  
 Depending on the storage mode used by objects stored in the backup file, the `Restore` command restores information as listed in the following table.  
  
|Storage mode|Information|  
|------------------|-----------------|  
|Multidimensional OLAP (MOLAP)|Source data, aggregations, and metadata|  
|Hybrid OLAP (HOLAP)|Aggregations and metadata|  
|Relational OLAP (ROLAP)|Metadata|  
  
 During a `Restore` command, an exclusive lock is placed on the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database specified in the `DatabaseName` element. The lock is released after the `Restore` command has completed.  
  
 For more information about backing up and restoring databases, see [Backing Up, Restoring, and Synchronizing Databases &#40;XMLA&#41;](../../multidimensional-models-scripting-language-assl-xmla/backing-up-restoring-and-synchronizing-databases-xmla.md).  
  
> [!IMPORTANT]  
>  For each backup file, the user who runs the restore command must have permission to read from the backup location specified for each file. To restore an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database that is not installed on the server, the user must also be a member of the server role for that [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance. To overwrite an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database, the user must have one of the following roles: a member of the server role for the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance, or a member of a database role with Full Control (Administrator) permissions on the database to be restored.  
  
> [!NOTE]  
>  After restoring an existing database, the user who restored the database might lose access to the restored database. This loss of access can occur if, at the time that the backup was performed, the user was not a member of the server role or was not a member of the database role with Full Control (Administrator) permissions.  
  
## See Also  
 [Backup Element &#40;XMLA&#41;](backup-element-xmla.md)   
 [Batch Element &#40;XMLA&#41;](batch-element-xmla.md)   
 [Parallel Element &#40;XMLA&#41;](../xml-elements-properties/parallel-element-xmla.md)   
 [Synchronize Element &#40;XMLA&#41;](synchronize-element-xmla.md)   
 [Commands &#40;XMLA&#41;](xml-elements-commands.md)  
  
  
