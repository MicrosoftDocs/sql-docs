---
description: "Creating the Server Connection Files (SybaseToSQL)"
title: "Creating the Server Connection Files (SybaseToSQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Sybase Console,Creating Server Connection Files"
  - "Sybase Console,Server Connection File Validation"
ms.assetid: 35ef396f-9f98-429d-9fc5-4f413d08fb37
author: cpichuka 
ms.author: cpichuka 
---
# Creating the Server Connection Files (SybaseToSQL)
Server information can be specified either in the servers section of the script file or in a separate server connection file. The command line parameter for the server connection file is, `-c <serverconnectionfile>`. If the same server id is present in both the script file and server connection file, then the server definition in the script file is considered.  
  
**Example:**  
  
```  
1.<!--Sample of server connection file commands -->  
  
<sybase name="<source-server-unique-name>">  
  
  <standard-mode>  
  
    <provider value="Ole DB Provider"/>  
  
    <server-name value="<server-name>"/>  
  
    <server-port value="<port>"/>  
  
    <user-id value="<password>"/>  
  
  </standard-mode>  
  
</sybase>  
```  
  
```xml  
<!--Connection to SQL Server-->  
  
<sql-server name="<target-server-unique-name>">  
  
  <sql-server-authentication>  
  
    <database value="<database-name>"/>  
  
    <server value="<server-name>"/>  
  
    <user-id value="<user-name>"/>  
  
    <password value="<password>"/>  
  
    <encrypt value="<true/false>"/>  
  
    <trust-server-certificate value="<true/false>"/>  
  
  </sql-server-authentication >  
  
</sql-server>  
```  
  
```  
2.<!-Sample of server connection file commands-->  
<sybase name="<source-server-unique-name>">  
  
  <advanced-mode>  
  
    <connection-string value="User ID=<user-name>;Password=<password>;Provider=ASEOLEDB.1;Server=<server-name>;Port=<port>;OLE DB Services = -2;"/>  
  
  </advanced-mode >  
  
</sybase>  
```  
  
```xml  
<!--Connection to SQL Server-->  
  
<sql-server name="<target-server-unique-name>">  
  
  <sql-server-authentication >  
  
    <database value="<database-name>"/>  
  
    <server value="<server-name>"/>  
  
    <user-id value="<user-name>"/>  
  
    <password value="<password>"/>  
  
  </sql-server-authentication >  
  
</sql-server>  
```  
  
## Server Connection File Validation  
The user can easily validate his/her server connection file against the schema definition file **S2SSConsoleScriptServersSchema.xsd** available in the 'Schemas' folder.  
  
## Next Step  
The next step in operating the console is [Executing the SSMA Console &#40;SybaseToSQL&#41;](../../ssma/sybase/executing-the-ssma-console-sybasetosql.md)  
  
## See Also  
[Executing the SSMA Console](executing-the-ssma-console-sybasetosql.md)  
  
