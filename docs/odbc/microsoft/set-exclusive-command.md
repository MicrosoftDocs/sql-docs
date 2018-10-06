---
title: "SET EXCLUSIVE Command | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SET EXCLUSIVE command [ODBC]"
ms.assetid: d4fe12c5-7e8b-4d20-9ea4-2bcaffb271f2
author: MightyPen
ms.author: genemi
manager: craigg
---
# SET EXCLUSIVE Command
Specifies whether table files are opened for exclusive or shared use on a network.  
  
## Syntax  
  
```  
  
SET EXCLUSIVE ON | OFF  
```  
  
## Arguments  
 ON  
 Limits accessibility of a table opened on a network to the user who opened it. The table isn't accessible to other users on the network. SET EXCLUSIVE ON also prevents all other users from having read-only access.  
  
 OFF  
 (Default for the driver; the defaults for Visual FoxPro are ON for the global data session and OFF for a private data session.) Allows a table opened on a network to be shared and modified by any user on the network.  
  
## Remarks  
 Changing the setting of SET EXCLUSIVE doesn't change the status of previously opened tables. For example, if a table is opened with SET EXCLUSIVE set to ON and SET EXCLUSIVE is later changed to OFF, the table retains its exclusive-use status.  
  
## See Also  
 [ODBC Visual FoxPro Setup Dialog Box](../../odbc/microsoft/odbc-visual-foxpro-setup-dialog-box.md)
