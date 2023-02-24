---
description: "DELETE TAG Command"
title: "DELETE TAG Command | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "DELETE TAG command [ODBC]"
ms.assetid: 4f4e1362-a5f3-4b15-8a3c-d4e96605f221
author: David-Engel
ms.author: v-davidengel
---
# DELETE TAG Command
Removes a tag or tags from a compound index (.cdx) file.  
  
## Syntax  
  
```  
  
DELETE TAG TagName1 [OF CDXFileName1]  
   [, TagName2 [OF CDXFileName2]] ...  
  Or   
DELETE TAG ALL [OF CDXFileName]  
```  
  
## Arguments  
 *TagName1*OF *CDXFileName1*[, *TagName2*[OF *CDXFileName2*]] ...  
 Specifies a tag to remove from a compound index file. You can delete multiple tags with one DELETE TAG by including a list of tag names separated by commas. If two or more tags with the same name exist in the open index files, you can remove a tag from a specific index file by including OF *CDXFileName*.  
  
 ALL [OF *CDXFileName*]  
 Removes every tag from a compound index file. If the current table has a structural compound index file, all tags are removed from the index file, the index file is deleted from the disk, and the flag in the table's header indicating the presence of an associated structural compound index file is removed. Use ALL with OF *CDXFileName* to remove all tags from an open compound index file other than the structural compound index file.  
  
## Remarks  
 Compound index files, created with INDEX, contain tags corresponding to index entries. DELETE TAG is used to remove a tag or tags from open compound index files. You can delete only tags from compound index files open in the current work area. If you remove all the tags from a compound index file, the file is deleted from the disk.  
  
 Visual FoxPro looks first for a tag in the structural compound index file (if one is open). If the tag isn't in the structural compound index file, Visual FoxPro then looks for the tag in the other open compound index files.  
  
## See Also  
 [INDEX Command](../../odbc/microsoft/index-command.md)
