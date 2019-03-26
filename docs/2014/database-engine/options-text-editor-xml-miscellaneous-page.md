---
title: "Options (Text Editor - XML - Miscellaneous Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 1a9509f0-c663-4b31-b396-7f5dc4371651
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Options (Text Editor - XML - Miscellaneous Page)

The **Options** dialog box lets you change the autocompletion and schema settings for the XML Editor. These settings are available when, on the **Tools** menu, you click **Options**, expand the **Text Editor** folder, click **XML** and then click **Miscellaneous** .  
  
## Auto Insert  
 **Close tags**  
 The Text Editor adds close tags when authoring XML elements. If an element start tag is selected, the editor inserts the matching close tag, including a matching namespace prefix. This check box is selected by default.  
  
 **Attribute quotes**  
 When authoring XML attributes, the editor inserts the `="``"` characters and positions the caret (**^)** inside the quotation marks. This check box is selected by default.  
  
 **Namespace declarations**  
 The editor automatically inserts namespace declarations wherever they are needed. This check box is selected by default.  
  
 **Other markup (Comments, CDATA)**  
 Comments, CDATA, DOCTYPE, processing instructions, and other markup is autocompleted. This check box is selected by default.  
  
## Network  
 **Automatically download DTDs and schemas**  
 Schemas and document type definitions (DTDs) are automatically downloaded from HTTP locations. This feature uses System.Net with autoproxy server detection enabled. This check box is selected by default.  
  
## Outlining  
 **Enter outlining mode when files open**  
 Turns on the outlining feature when a file is opened. This check box is selected by default.  
  
## Caching  
 **Schemas**  
 Specifies the location of the schema cache. The Browse button (...) opens the current schema cache location in a new window. The default location is *\<Management Studio install directory>*\Xml\Schemas.  
