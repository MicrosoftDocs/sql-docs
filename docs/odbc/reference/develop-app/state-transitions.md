---
title: "State Transitions | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "state transitions [ODBC]"
  - "unallocated state [ODBC]"
  - "handles [ODBC], state transitions"
  - "allocated state [ODBC]"
  - "connection state [ODBC]"
ms.assetid: fc741611-6535-43cc-8156-6d897d04664e
author: MightyPen
ms.author: genemi
manager: craigg
---
# State Transitions
ODBC defines discrete *states* for each environment, each connection, and each statement. For example, the environment has three possible states: Unallocated (in which no environment is allocated), Allocated (in which an environment is allocated but no connections are allocated), and Connection (in which an environment and one or more connections are allocated). Connections have seven possible states; statements have 13 possible states.  
  
 A particular item, as identified by its handle, moves from one state to another when the application calls a certain function or functions and passes the handle to that item. Such movement is called a *state transition*. For example, allocating an environment handle with **SQLAllocHandle** moves the environment from Unallocated to Allocated, and freeing that handle with **SQLFreeHandle** returns it from Allocated to Unallocated. ODBC defines a limited number of legal state transitions, which is another way of saying that functions must be called in a certain order.  
  
 Some functions, such as **SQLGetConnectAttr**, do not affect state at all. Other functions affect the state of a single item. For example, **SQLDisconnect** moves a connection from a Connection state to an Allocated state. Finally, some functions affect the state of more than one item. For example, allocating a connection handle with **SQLAllocHandle** moves a connection from an Unallocated to an Allocated state and moves the environment from an Allocated to a Connection state.  
  
 If an application calls a function out of order, the function returns a *state transition error*. For example, if an environment is in a Connection state and the application calls **SQLFreeHandle** with that environment handle, **SQLFreeHandle** returns SQLSTATE HY010 (Function sequence error), because it can be called only when the environment is in an Allocated state. By defining this as an invalid state transition, ODBC prevents the application from freeing the environment while there are active connections.  
  
 Some state transitions are inherent in the design of ODBC. For example, it is not possible to allocate a connection handle without first allocating an environment handle, because the function that allocates a connection handle requires an environment handle. Other state transitions are enforced by the Driver Manager and the drivers. For example, **SQLExecute** executes a prepared statement. If the statement handle passed to it is not in a Prepared state, **SQLExecute** returns SQLSTATE HY010 (Function sequence error).  
  
 From the application's point of view, state transitions are usually straightforward: Legal state transitions tend to go hand-in-hand with the flow of a well-written application. State transitions are more complex for the Driver Manager and the drivers because they must track the state of the environment, each connection, and each statement. Most of this work is done by the Driver Manager; most of the work that must be done by drivers occurs with statements with pending results.  
  
 Parts 1 and 2 of this manual ("Introduction to ODBC" and "Developing Applications and Drivers") tend not to explicitly mention state transitions. Instead, they describe the order in which functions must be called. For example, "Executing Statements" states that a statement must be prepared with **SQLPrepare** before it can be executed with **SQLExecute**. For a complete description of states and state transitions, including which transitions are checked by the Driver Manager and which must be checked by drivers, see [Appendix B: ODBC State Transition Tables](../../../odbc/reference/appendixes/appendix-b-odbc-state-transition-tables.md).
