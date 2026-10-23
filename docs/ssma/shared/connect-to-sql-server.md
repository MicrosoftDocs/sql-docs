---
title: Connect to SQL Server Dialog Box
description: Learn about the Connect to SQL Server dialog box options in SQL Server Migration Assistant (SSMA).
author: cwebb-msft
ms.author: chweb
ms.date: 04/16/2026
ms.service: sql
ms.subservice: ssma
ms.topic: concept-article
ms.collection:
  - sql-migration-content
ai-usage: ai-assisted
---
# Connect to SQL Server dialog box

Use the **Connect to SQL Server** dialog box in SQL Server Migration Assistant (SSMA) to connect to the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that you want to migrate to. To access the **Connect to SQL Server** dialog box, on the **File** menu, select **Connect to SQL Server**.

## Options

#### Server name

Enter or select the instance of SQL Server to connect to. By default, the instance that you connected to most recently is displayed.

- If you're connecting to the default instance on the local computer, you can enter either `localhost` or a dot (`.`).

- If you're connecting to the default instance on another computer, enter the name of the computer.

- If you're connecting to a named instance on another computer, enter the computer name, a backslash, and the instance name, such as `<MyServer>\<MyInstance>`.

#### Server port

If your instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] isn't configured to accept connections on the default port (1433), enter the port number. Otherwise, leave this value blank.

#### Database

Specify the database to migrate objects and data to. This option isn't available when reconnecting to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

#### Authentication

Select the authentication method to use for the connection to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. To use your current Windows account, select **Windows Authentication**. To specify a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login and password, select **SQL Server Authentication**.

#### User name

If you're using [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] authentication, enter the login for that instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. If you're using Windows authentication, this option isn't available.

#### Password

If you're using [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] authentication, enter the password for the login on that instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. If you're using Windows authentication, this option isn't available.

#### Encrypt connection

If you want to securely connect to SQL Server, select the **Encrypt connection** checkbox.

#### Trust Server Certificate

If you want to use this option, select the **Trust Server Certificate** checkbox.

> [!NOTE]  
> To enable **Trust Server Certificate**, **Encrypt connection** must be set to **True**.

## Related content

- [SQL Server Migration Assistant](../sql-server-migration-assistant.md)
