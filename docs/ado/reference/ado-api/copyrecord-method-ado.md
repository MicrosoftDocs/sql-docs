---
title: "CopyRecord Method (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_Record::raw_CopyRecord"
  - "_Record::CopyRecord"
helpviewer_keywords: 
  - "CopyRecord method [ADO]"
ms.assetid: b9bcf272-3c74-479f-95dd-0229a32e98fc
author: MightyPen
ms.author: genemi
manager: craigg
---
# CopyRecord Method (ADO)
Copies an entity represented by a [record](../../../ado/reference/ado-api/record-object-ado.md) to another location.  
  
## Syntax  
  
```  
  
Record.CopyRecord (Source, Destination, UserName, Password, Options, Async)  
```  
  
#### Parameters  
 *Source*  
 Optional. A **String** value that contains a URL specifying the entity to be copied (for example, a file or directory). If *Source* is omitted or specifies an empty string, the file or directory represented by the current [Record](../../../ado/reference/ado-api/record-object-ado.md) will be copied.  
  
 *Destination*  
 Optional. A **String** value that contains a URL specifying the location where *Source* will be copied.  
  
 *UserName*  
 Optional. A **String** value that contains the user ID that, if needed, authorizes access to *Destination*.  
  
 *Password*  
 Optional. A **String** value that contains the password that, if needed, verifies *UserName*.  
  
 *Options*  
 Optional. A [CopyRecordOptionsEnum](../../../ado/reference/ado-api/copyrecordoptionsenum.md) value that has a default value of **adCopyUnspecified**. Specifies the behavior of this method.  
  
 *Async*  
 Optional. A **Boolean** value that, when **True**, specifies that this operation should be asynchronous.  
  
## Return Value  
 A **String** value that typically returns the value of *Destination*. However, the exact value returned is provider-dependent.  
  
## Remarks  
 The values of *Source* and *Destination* must not be identical; otherwise, a run-time error occurs. At least one of the server, path, or resource names must differ.  
  
 All children (for example, subdirectories) of *Source* are copied recursively, unless **adCopyNonRecursive** is specified. In a recursive operation, *Destination* must not be a subdirectory of *Source*; otherwise, the operation will not complete.  
  
 This method fails if *Destination* identifies an existing entity (for example, a file or directory), unless **adCopyOverWrite** is specified.  
  
> [!IMPORTANT]
>  Use the **adCopyOverWrite** option judiciously. For example, specifying this option when copying a file to a directory will *delete* the directory and replace it with the file.  
  
> [!NOTE]
>  URLs using the http scheme will automatically invoke the [Microsoft OLE DB Provider for Internet Publishing](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-internet-publishing.md). For more information, see [Absolute and Relative URLs](../../../ado/guide/data/absolute-and-relative-urls.md).  
  
## Applies To  
 [Record Object (ADO)](../../../ado/reference/ado-api/record-object-ado.md)
