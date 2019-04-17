---
title: "The Significance of Cursor Location | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "server-side cursors [ADO]"
  - "cursors [ADO], client-side"
  - "client-side cursors [ADO]"
  - "cursors [ADO], server-side"
ms.assetid: 70ef5b1c-0459-41a1-b796-031f61a29a8a
author: MightyPen
ms.author: genemi
manager: craigg
---
# The Significance of Cursor Location
Every cursor uses temporary resources to hold its data. These resources can be memory, a disk paging file, temporary disk files, or even temporary storage in the database. The cursor is called a *client-side* cursor when these resources are located on the client computer. The cursor is called a *server-side* cursor when these resources are located on the server.  
  
## Client-Side Cursors  
 In ADO, call for a client-side cursor by using the **adUseClient CursorLocationEnum.** With a non-keyset client-side cursor, the server sends the entire result set across the network to the client computer. The client computer provides and manages the temporary resources needed by the cursor and result set. The client-side application can browse through the entire result set to determine which rows it requires.  
  
 Static and keyset-driven client-side cursors may place a significant load on your workstation if they include too many rows. While all of the cursor libraries are capable of building cursors with thousands of rows, applications designed to fetch such large rowsets may perform poorly. There are exceptions, of course. For some applications, a large client-side cursor might be perfectly appropriate and performance might not be an issue.  
  
 One obvious benefit of the client-side cursor is quick response. After the result set has been downloaded to the client computer, browsing through the rows is very fast. Your application is generally more scalable with client-side cursors because the cursor's resource requirements are placed on each separate client and not on the server.  
  
## Server-Side Cursors  
 In ADO, call for a server-side cursor by using the **adUseServer CursorLocationEnum.** With a server-side cursor, the server manages the result set using resources provided by the server computer. The server-side cursor returns only the requested data over the network. This type of cursor can sometimes provide better performance than the client-side cursor, especially in situations where excessive network traffic is a problem.  
  
 However, it is important to point out that a server-side cursor is - at least temporarily - consuming precious server resources for every active client. You must plan accordingly to ensure that your server hardware is capable of managing all of the server-side cursors requested by active clients. Also, a server-side cursor can be slow because it provides only single row access - there is no batch cursor available.  
  
 Server-side cursors are useful when inserting, updating, or deleting records. With server-side cursors, you can have multiple active statements on the same connection.
