---
title: "Backup Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Backup Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#Backup"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Backup"
  - "microsoft.xml.analysis.backup"
helpviewer_keywords: 
  - "Backup command"
ms.assetid: 5bcbc14c-9db9-45b2-99de-f3a265bcb0c4
author: minewiskan
ms.author: owend
manager: craigg
---
# Backup Element (XMLA)
  Backs up a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database to a backup file.  
  
## Syntax  
  
```xml  
  
<Command>  
   <Backup>  
      <Object>...</Object>  
      <File>...</File>  
      <Security>...</Security>  
      <ApplyCompression>...</ApplyCompression>  
      <AllowOverwrite>...</AllowOverwrite>  
      <Password>...</Password>  
      <BackupRemotePartitions>...</BackupRemotePartitions>  
      <Locations>...</Locations>  
   </Backup>  
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
|Child elements|[AllowOverwrite](../xml-elements-properties/allowoverwrite-element-xmla.md), [ApplyCompression](../xml-elements-properties/applycompression-element-xmla.md), [BackupRemotePartitions](../xml-elements-properties/backupremotepartitions-element-xmla.md), [File](../xml-elements-properties/file-element-xmla.md), [Locations](../xml-elements-properties/locations-element-xmla.md), [Object](../xml-elements-properties/object-element-xmla.md), [Password](../xml-elements-properties/password-element-xmla.md), [Security](../xml-elements-properties/security-element-xmla.md)|  
  
## Remarks  
 The `Backup` command backs up the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database specified in the [Object](../xml-elements-properties/object-element-xmla.md) element to a backup file, and optionally backs up remote partitions to remote backup files. If the `Object` element refers to an object other than an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database, an error occurs.  
  
 Which information the `Backup` command backs up depends on the storage mode used by objects in the database. The following table identifies the information that is backed up based upon the storage mode used.  
  
|Storage mode|Information that is backed up|  
|------------------|-----------------------------------|  
|Multidimensional OLAP (MOLAP)|Source data, aggregations, and metadata|  
|Hybrid OLAP (HOLAP)|Aggregations and metadata|  
|Relational OLAP (ROLAP)|Metadata|  
  
 During a `Backup` command, a shared lock is placed on the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database specified in the `Object` element. The shared lock releases after the `Backup` command has completed.  
  
 Multiple `Backup` commands can be run in parallel, if the commands are included in the [Parallel](../xml-elements-properties/parallel-element-xmla.md) collection of a [Batch](batch-element-xmla.md) command. The `Parallel` collection enables a database to be backed up into multiple backup files at the same time.  
  
 For more information about backing up and restoring databases, see [Backing Up, Restoring, and Synchronizing Databases &#40;XMLA&#41;](../../multidimensional-models-scripting-language-assl-xmla/backing-up-restoring-and-synchronizing-databases-xmla.md).  
  
> [!IMPORTANT]  
>  For each backup file, the user who runs the backup command must have permission to write to the backup location specified for each file. Also, the user must have one of the following roles: a member of a server role for the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance, or a member of a database role with Full Control (Administrator) permissions on the database to be backed up.  
  
## See Also  
 [Restore Element &#40;XMLA&#41;](restore-element-xmla.md)   
 [Synchronize Element &#40;XMLA&#41;](synchronize-element-xmla.md)   
 [Commands &#40;XMLA&#41;](xml-elements-commands.md)  
  
  
