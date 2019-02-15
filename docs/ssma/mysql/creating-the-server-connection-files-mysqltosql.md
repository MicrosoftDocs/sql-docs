---
title: "Creating the Server Connection Files (MySQLToSQL) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Server connection file validation"
  - "Server connection files"
ms.assetid: df0e970c-da0b-4118-b359-c9dcbbad16d6
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Creating the Server Connection Files (MySQLToSQL)
Server information can be specified either in the servers section of the script file or in a separate server connection file. The command line parameter for the server connection file is, `-c <serverconnectionfile>`. If the same server id is present in both the script file and server connection file, then the server definition in the script file is considered.  
  
**Example:**  
  
```xml  
<!--Sample of server connection file commands -->  
  
<mysql name ="<source-server-unique-name>">  
  
   <standard-mode>  
  
      <server value ="<server-name>"/>  
  
      <user-id value ="<user-name>"/>  
  
      <password value ="<password>"/>  
  
      <port value ="<port>"/>  
  
   </standard-mode>  
  
</mysql>  
```  
  
```xml  
<!--Connection to SQL Server-->  
  
<sql-server name="<target-server-unique-name>">  
  
   <sql-server-authentication>  
  
      <server value="<server-name>"/>  
  
      <database value="<database-name>"/>  
  
      <user-id value="<user-name>"/>  
  
      <password value="<password>"/>  
  
      <encrypt value="<true/false>"/>  
  
      <trust-server-certificate value="<true/false>"/>  
  
   </sql-server-authentication>  
  
</sql-server>  
```  
  
```xml  
<!--Connection to SQL Azure-->  
  
<sql-azure name="<target-server-unique-name>">  
  
   <server value="<server-name>"/>  
  
   <database value="<database-name>"/>  
  
   <user-id value="<user-name>"/>  
  
   <password value="<password>"/>  
  
</sql-azure>  
```  
  
## Server Connection File Validation  
The user can easily validate his/her server connection file against the schema definition file **'M2SSConsoleScriptServersSchema.xsd'** available in the 'Schemas' folder.  
  
## Next Step  
The next step in operating the console is [Executing the SSMA Console &#40;MySQLToSQL&#41;](../../ssma/mysql/executing-the-ssma-console-mysqltosql.md)  
  
## See Also  
[Executing the SSMA Console (MySQL)](https://msdn.microsoft.com/e3e9f7e4-0619-4861-a202-3d5d39953b26)  
  
