---
description: "Creating the Server Connection Files (AccessToSQL)"
title: "Creating the Server Connection Files (AccessToSQL) | Microsoft Docs"
ms.service: sql
ms.custom: ""
ms.date: "08/17/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
ms.assetid: 829153be-aa8e-4162-87e8-69882feecf19
author: cpichuka 
ms.author: cpichuka 
---
# Creating the server connection files (AccessToSQL)
Server information can be specified either in the servers section of the script file. Server information can also be specified in a separate server connection file. The command line parameter for the server connection file is `-c <serverconnectionfile>`. If the same server id is present in both the script and server connection files, then the server definition in the script file is considered.  
  
```xml  
<!--Sample of server connection file commands -->  
<!--Connection to SQL Server-->  
  
<sql-server name="target_3">  
  
  <sql-server-authentication>  
  
    <server value="$TargetServerName$"/>  
  
    <database value="$TargetDB$"/>  
  
    <user-id value="$TargetUserName$"/>  
  
    <password value="$TargetPassword$"/>  
  
    <encrypt value="true"/>  
  
    <trust-server-certificate value="true"/>  
  
  </sql-server-authentication>  
  
</sql-server>  
```  
  
```xml  
<!--Connection to SQL Azure-->  
  
<sql-azure name="target_azure">  
  
  <server value="$TargetAzureServerName$"/>  
  
  <database value="$TargetAzureDB$"/>  
  
  <user-id value="$TargetAzureUserName$"/>  
  
  <password value="$TargetAzurePassword$"/>  
  
</sql-azure>  
```  
  
## Server connection file validation  
The user can easily validate his/her server connection file against the schema definition file **'A2SSConsoleScriptServersSchema.xsd'** available in the 'Schemas' folder.  
  
## Next step  
The next step in operating the console is [Executing the SSMA Console &#40;AccessToSQL&#41;](../../ssma/access/executing-the-ssma-console-accesstosql.md)  
  
## See also  
[Executing the SSMA Console (Access)](./executing-the-ssma-console-accesstosql.md)  
