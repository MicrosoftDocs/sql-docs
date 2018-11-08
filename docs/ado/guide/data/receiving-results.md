---
title: "Receiving Results | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "receiving results [ADO]"
  - "Recordset object [ADO], receiving results"
ms.assetid: 791aa26e-7aae-477e-9f05-5cd46e1de095
author: MightyPen
ms.author: genemi
manager: craigg
---
# Receiving Results
In ADO most commands result in some information returned to the caller. For commands returning rowset, the results are received in a **Recordset** object, which is probably the most used of the ADO objects.  
  
 There are several ways to receive data in a **Recordset** object from a data source, including calling the following:  
  
-   [Connection.Execute method](../../../ado/guide/data/creating-and-executing-a-simple-command.md)  
  
-   [Command.Execute method](../../../ado/guide/data/creating-and-executing-a-simple-command.md)  
  
-   [Recordset.Open method](../../../ado/guide/data/creating-and-executing-a-simple-command.md)  
  
-   [Connection.NamedCommand](../../../ado/guide/data/named-commands.md)  
  
-   [Connection.StoredProcedure](../../../ado/guide/data/calling-a-stored-procedure-as-a-method-on-a-connection-object.md)  
  
 Receiving data in a **Recordset** object concludes the process of getting data, with the participation of a **Connection** object and a **Command** object, implicitly or explicitly. In a typical client/server application system, the whole process of getting data requires a round trip over the network for each filled **Recordset**.  
  
 To receive more than one result sets means you would need to make several round trips over the network, one for each data set encapsulated in a **Recordset** object. For slow or congested networks, reducing the number of round trips may help improve the application's performances. Therefore, some providers offer support to receive multiple **Recordset**s in a single round trip. This is discussed in the following topic:  
  
-   [Receiving Multiple Recordsets](../../../ado/guide/data/receiving-multiple-recordsets.md)
