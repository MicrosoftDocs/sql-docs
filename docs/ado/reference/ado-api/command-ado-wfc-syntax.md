---
title: "Command (ADO - WFC Syntax) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
helpviewer_keywords: 
  - "Command collection [ADO], ADO/WFC syntax"
ms.assetid: 39d0aa06-03ac-4c9a-8400-83947756ef99
author: MightyPen
ms.author: genemi
manager: craigg
---
# Command (ADO - WFC Syntax)
## package com.ms.wfc.data  
  
### Constructor  
  
```  
public Command()  
public Command(String commandtext)  
```  
  
### Methods  
  
```  
public void cancel()  
public com.ms.wfc.data.Parameter createParameter(String  
    Name, int Type, int Direction, int Size, Object Value)  
public Recordset execute()  
public Recordset execute(Object[] parameters)  
public Recordset execute(Object[] parameters, int options)  
public int executeUpdate(Object[] parameters)  
public int executeUpdate(Object[] parameters, int options)  
public int executeUpdate()  
```  
  
 The **executeUpdate** method is a special case method that calls the underlying ADO **execute** method with certain parameters. The **executeUpdate** method does not support the return of a **Recordset** object, so the **execute** method's *options* parameter is modified with **AdoEnums.ExecuteOptions.NORECORDS**. After the **execute** method completes, its updated *RecordsAffected* parameter is passed back to the **executeUpdate** method, which is finally returned as an **int**.  
  
### Properties  
  
```  
public com.ms.wfc.data.Connection getActiveConnection()  
public void setActiveConnection(com.ms.wfc.data.Connection con)  
public void setActiveConnection(String conString)  
public String getCommandText()  
public void setCommandText(String command)  
public int getCommandTimeout()  
public void setCommandTimeout(int timeout)  
public int getCommandType()  
public void setCommandType(int type)  
public String getName()  
public void setName(String name)  
public boolean getPrepared()  
public void setPrepared(boolean prepared)  
public int getState()  
public com.ms.wfc.data.Parameter getParameter(int n)  
public com.ms.wfc.data.Parameter getParameter(String n)  
public com.ms.wfc.data.Parameters getParameters()  
public AdoProperties getProperties()  
```  
  
## See Also  
 [Command Object (ADO)](../../../ado/reference/ado-api/command-object-ado.md)
