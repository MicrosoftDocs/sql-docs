---
title: "Create the server connection files (Db2ToSQL)"
description: Specify server information in the servers section of the script file, or in a separate server connection file, in SSMA for Db2.
author: cpichuka
ms.author: cpichuka
ms.reviewer: randolphwest
ms.date: 09/24/2024
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
---

# Create the server connection files (Db2ToSQL)

Server information can be specified either in the servers section of the script file or in a separate server connection file. The command line parameter for the server connection file is, `-c <serverconnectionfile>`. If the same server ID is present in both the script file and server connection file, then the server definition in the script file is considered.

## Examples

This is a sample of server connection file commands.

The `database-manager` can be one of the following attribute values:

- `zOs` - Db2 for z/OS
- `LUW` - Db2 for Linux, Unix, and Windows
- `DBi` - Db2 for i

```xml
<sql-server name="<target-server-unique-name>">
  <sql-server-authentication>
    <server value="<server-name>" />
    <database value="<database-name>" />
    <user-id value="<user-name>"/>
    <password value="<password>"/>
  </sql-server-authentication>
</sql-server>
```

For example:

```xml
<db2 name="<source-server-unique-name>">
   <standard-mode>
      <connection-provider value="OleDB Provider" />
      <database-manager value="$Db2Manager$" />
      <server-name value="$Db2HostName$" />
      <port value="$Db2Port$" />
      <initial-catalog value="$Db2Instance$" />
      <user-id value="$Db2UserName$" />
      <password value="$Db2Password$" />
   </standard-mode>
</db2>
```

## Next step

> [!div class="nextstepaction"]
> [Execute the SSMA console (Db2ToSQL)](executing-the-ssma-console-db2tosql.md)
