---
title: "SET EXCLUSIVE Command"
description: "SET EXCLUSIVE Command"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SET EXCLUSIVE command [ODBC]"
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
