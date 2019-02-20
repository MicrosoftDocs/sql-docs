---
title: "Options (Text Editor - XML - Formatting Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 97373178-d288-4127-af37-d9f5fe1b8607
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Options (Text Editor - XML - Formatting Page)

This dialog box allows you to specify the formatting settings for the XML Editor. You can access the **Options** dialog box from the **Tools** menu.  
  
> [!NOTE]  
> These settings are available when you select the **Text Editor** folder, the **XML** folder, and then the **Formatting** option from the **Options** dialog box.  
  
## Attributes  
 **Preserve manual attribute formatting**  
 Do not reformat attributes. This is the default.  
  
> [!NOTE]  
>  If the attributes are on multiple lines, the editor indents each line of attributes to match the indentation of the parent element.  
  
 **Align attributes each on a separate line**  
 Align the second and subsequent attributes vertically to match the indentation of the first attribute. The following XML text is an example of how the attributes would be aligned.  
  
```  
<item id = "123-A"  
      name = "hammer"  
      price = "9.95"  
</item>  
```  
  
## Auto Reformat  
 **On paste from clipboard.**  
 Reformat XML text pasted from the clipboard.  
  
 **On completion of end tag**  
 Reformat the element when the end tag is completed.  
  
## Mixed Content  
 **Format mixed content by default.**  
 Attempt to reformat mixed content, except when the content is found in an `xml:space="preserve"` scope. This is the default.  
  
 If an element contains a mix of text and markup, the contents are considered to be mixed content. Following is an example of an element with mixed content.  
  
```  
<dir>c:\data\AlphaProject\  
  <file readOnly="false">test1.txt</file>  
  <file readOnly="false">test2.txt</file>  
```  
  
 \</dir>  
  
## See Also  
 [XML Editor &#40;SQL Server Management Studio&#41;](../ssms/sql-server-management-studio-ssms.md)  
