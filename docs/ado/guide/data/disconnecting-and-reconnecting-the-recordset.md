---
title: "Disconnecting and Reconnecting the Recordset"
description: "Disconnecting and Reconnecting the Recordset"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "Recordset object [ADO], disconnecting and reconnecting"
---
# Disconnecting and Reconnecting the Recordset
One of the most powerful features found in ADO is the capability to open a client-side Recordset from a data source and then disconnect the Recordset from the data source. Once the Recordset has been disconnected, the connection to the data source can be closed, thereby releasing the resources on the server used to maintain it. You can continue to view and edit the data in the Recordset while it is disconnected and later reconnect to the data source and send your updates in batch mode.  
  
 To disconnect a Recordset, open it with a cursor location of adUseClient, and then set the ActiveConnection property equal to Nothing. (C++ users should set the ActiveConnection equal to NULL to disconnect.)  
  
 We will use a disconnected Recordset later in this section when we discuss Recordset persistence to address a scenario in which we need to have the data in a Recordset available to an application while the client computer is not connected to a network.  
  
## See Also  
 [Batch Mode](./batch-mode.md)