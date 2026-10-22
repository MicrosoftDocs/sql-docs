---
title: "Creating the Server Connection Files (AccessToSQL)"
description: "Creating the Server Connection Files (AccessToSQL)"
author: nilabjaball
ms.author: niball
ms.reviewer: vanto
ms.date: 12/30/2025
ms.service: sql
ms.subservice: ssma
ms.topic: concept-article
ms.collection:
  - sql-migration-content
---
# Create the server connection files (AccessToSQL)

Server information can be specified either in the servers section of the script file. Server information can also be specified in a separate server connection file. The command line parameter for the server connection file is `-c <serverconnectionfile>`. If the same server ID is present in both the script and server connection files, then the server definition in the script file is considered.

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
<!--Connection to Azure SQL-->
<sql-azure name="target_azure">
  <server value="$TargetAzureServerName$"/>
  <database value="$TargetAzureDB$"/>
  <user-id value="$TargetAzureUserName$"/>
  <password value="$TargetAzurePassword$"/>
</sql-azure>
```

## Server connection file validation

The user can easily validate their server connection file against the schema definition file **'A2SSConsoleScriptServersSchema.xsd'** available in the 'Schemas' folder.

## Related content

- [Execute the SSMA Console](executing-the-ssma-console-accesstosql.md)
