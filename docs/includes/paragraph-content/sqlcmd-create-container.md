---
author: dlevy-msft
ms.author: dlevy
ms.reviewer: randolphwest
ms.date: 12/07/2023
ms.service: sql
ms.topic: include
---

Create a new [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] instance in a container using the latest version of [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)]. The command also restores the `WideWorldImporters` database.

Open a new terminal window and run the following command:

```bash
sqlcmd create mssql --name sql1 --accept-eula --using https://github.com/Microsoft/sql-server-samples/releases/download/wide-world-importers-v1.0/WideWorldImporters-Full.bak
 ```
