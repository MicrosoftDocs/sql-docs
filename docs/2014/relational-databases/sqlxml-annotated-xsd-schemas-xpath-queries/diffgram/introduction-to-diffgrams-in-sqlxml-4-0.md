---
title: "Introduction to DiffGrams in SQLXML 4.0 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "annotations [SQLXML]"
  - "DiffGrams [SQLXML], about DiffGrams"
ms.assetid: 1902d67f-baf3-46e6-a36c-b24b5ba6f8ea
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Introduction to DiffGrams in SQLXML 4.0
  This topic provides a brief introduction to DiffGrams.  
  
## DiffGram Format  
 This is the general DiffGram format:  
  
```  
<?xml version="1.0"?>  
<diffgr:diffgram   
         xmlns:msdata="urn:schemas-microsoft-com:xml-msdata"  
         xmlns:diffgr="urn:schemas-microsoft-com:xml-diffgram-v1"  
         xmlns:xsd="http://www.w3.org/2001/XMLSchema">  
   <DataInstance>  
      ...  
   </DataInstance>  
   [<diffgr:before>  
        ...  
   </diffgr:before>]  
  
   [<diffgr:errors>  
        ...  
   </diffgr:errors>]  
</diffgr:diffgram>  
```  
  
 The DiffGram format consists of these blocks:  
  
 **\<DataInstance>**  
 The name of this element, **DataInstance**, is used for explanation purposes in this documentation. For example, if the DiffGram were generated from a dataset in the .NET Framework, the value of the **Name** property of the dataset would be used as the name of this element. This block contains all relevant data after the change, possibly including data that has not been modified. The DiffGram processing logic ignores the elements in this block for which the **diffgr:hasChanges** attribute is not specified.  
  
 **\<diffgr:before>**  
 This optional block contains the original record instances (elements) that must be updated or deleted. All the database tables being modified (updated or deleted) by the DiffGram must appear as top-level elements in the **\<before>** block.  
  
 **\<diffgr:errors>**  
 This optional block is ignored by the DiffGram processing logic.  
  
## DiffGram Annotations  
 These annotations are defined in the DiffGram namespace **"urn:schemas-microsoft-com:xml-diffgram-01"**:  
  
 **id**  
 This attribute is used to pair the elements in the **\<before>** and the **\<DataInstance>** blocks.  
  
 **hasChanges**  
 For an insert or an update operation, the DiffGram must specify this attribute with the value **inserted** or **modified**. If this attribute is not present, the corresponding element in the **\<DataInstance>** is ignored by the processing logic and no updates are performed. For working samples, see [DiffGram Examples &#40;SQLXML 4.0&#41;](diffgram-examples-sqlxml-4-0.md).  
  
 **parentID**  
 This attribute is used to specify parent-child relationships among the elements in the DiffGram. This attribute appears only in the \<before> block. It is used by SQLXML when applying updates. The parent-child relationship is used in determining the order in which the elements in the DiffGram are processed.  
  
## Understanding the DiffGram Processing Logic  
 The DiffGram processing logic uses certain rules to determine whether an operation is an insert, update, or delete operation. These rules are described in the following table.  
  
|Operation|Description|  
|---------------|-----------------|  
|Insert|A DiffGram indicates an insert operation when an element appears in the **\<DataInstance>** block but not in the corresponding **\<before>** block, and the **diffgr:hasChanges** attribute is specified (**diffgr:hasChanges=inserted**) on the element. In this case, the DiffGram inserts the record instance that is specified in the **\<DataInstance>** block into the database.<br /><br /> If the **diffgr:hasChanges** attribute is not specified, the element is ignored by the processing logic and no insert is performed. For working samples, see [DiffGram Examples &#40;SQLXML 4.0&#41;](diffgram-examples-sqlxml-4-0.md).|  
|Update|The DiffGram indicates an update operation when there is an element in the \<before> block for which there is a corresponding element in the **\<DataInstance>** block (that is, both elements have a **diffgr:id** attribute with same value) and the **diffgr:hasChanges** attribute is specified with the value **modified** on the element in the **\<DataInstance>** block.<br /><br /> If the **diffgr:hasChanges** attribute is not specified on the element in the **\<DataInstance>** block, an error is returned by the processing logic. For working samples, see [DiffGram Examples &#40;SQLXML 4.0&#41;](diffgram-examples-sqlxml-4-0.md).<br /><br /> If **diffgr:parentID** is specified in the **\<before>** block, the parent-child relationship of elements that are specified by **parentID** are used in determining the order in which records are updated.|  
|Delete|A DiffGram indicates a delete operation when an element appears in the **\<before>** block but not in the corresponding **\<DataInstance>** block. In this case, the DiffGram deletes the record instance that is specified in the **\<before>** block from the database. For working samples, see [DiffGram Examples &#40;SQLXML 4.0&#41;](diffgram-examples-sqlxml-4-0.md).<br /><br /> If **diffgr:parentID** is specified in the **\<before>** block, the parent-child relationship of elements that are specified by **parentID** are used in determining the order in which records are deleted.|  
  
> [!NOTE]  
>  Parameters cannot be passed to DiffGrams.  
  
  
