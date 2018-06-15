---
title: "Restore Element (XMLA) | Microsoft Docs"
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
# Restore Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Restores a Analysis Services database from a backup file.  
  
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
  
## Element characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Command](../../../analysis-services/xmla/xml-elements-properties/command-element-xmla.md)|  
|Child elements|[AllowOverwrite](../../../analysis-services/xmla/xml-elements-properties/allowoverwrite-element-xmla.md), [DatabaseName](../../../analysis-services/xmla/xml-elements-properties/databasename-element-xmla.md), [DatabaseID](../../../analysis-services/xmla/xml-elements-properties/databaseid-element-xmla.md), [File](../../../analysis-services/xmla/xml-elements-properties/file-element-xmla.md), [Locations](../../../analysis-services/xmla/xml-elements-properties/locations-element-xmla.md), [Password](../../../analysis-services/xmla/xml-elements-properties/password-element-xmla.md), [Security](../../../analysis-services/xmla/xml-elements-properties/security-element-xmla.md), [DbStorageLocation](../../../analysis-services/xmla/xml-elements-properties/dbstoragelocation-element.md)|  
  
## Remarks  
 The **Restore** command restores an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database specified in the **DatabaseName** element from a backup file and optionally restores remote partitions from remote backup files.  
  
 Depending on the storage mode used by objects stored in the backup file, the **Restore** command restores information as listed in the following table.  
  
|Storage mode|Information|  
|------------------|-----------------|  
|Multidimensional OLAP (MOLAP)|Source data, aggregations, and metadata|  
|Hybrid OLAP (HOLAP)|Aggregations and metadata|  
|Relational OLAP (ROLAP)|Metadata|  
  
 During a **Restore** command, an exclusive lock is placed on the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database specified in the **DatabaseName** element. The lock is released after the **Restore** command has completed.  
  
 For more information about backing up and restoring databases, see [Backing Up, Restoring, and Synchronizing Databases &#40;XMLA&#41;](../../../analysis-services/multidimensional-models-scripting-language-assl-xmla/backing-up-restoring-and-synchronizing-databases-xmla.md).  
  
> [!IMPORTANT]  
>  For each backup file, the user who runs the restore command must have permission to read from the backup location specified for each file. To restore an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database that is not installed on the server, the user must also be a member of the server role for that [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance. To overwrite an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database, the user must have one of the following roles: a member of the server role for the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance, or a member of a database role with Full Control (Administrator) permissions on the database to be restored.  
  
> [!NOTE]  
>  After restoring an existing database, the user who restored the database might lose access to the restored database. This loss of access can occur if, at the time that the backup was performed, the user was not a member of the server role or was not a member of the database role with Full Control (Administrator) permissions.  
  
## See also
 [Backup Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/backup-element-xmla.md)   
 [Batch Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/batch-element-xmla.md)   
 [Parallel Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/parallel-element-xmla.md)   
 [Synchronize Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/synchronize-element-xmla.md)   
 [Commands &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/xml-elements-commands.md)  
  
  
