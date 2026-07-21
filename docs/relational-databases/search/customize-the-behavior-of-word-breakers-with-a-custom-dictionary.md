---
title: Customize Behavior of Word Breakers with a Dictionary File
description: Customize behavior of word breakers with a dictionary file
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/20/2026
ms.service: sql
ms.subservice: search
ms.topic: concept-article
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017"
---
# Customize behavior of word breakers with a dictionary file

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

You can customize the behavior of the word breaker by creating a language-specific dictionary file. For example, you can prevent the word breaker from breaking certain terms or patterns.

For more information, see the following SharePoint article:

[Create a custom dictionary (SharePoint Server 2010)](/previous-versions/office/sharepoint-server-2010/cc263242(v=office.14))

For [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], place custom dictionary files in the following folder:

`C:\Program Files\Microsoft SQL Server\<instance name>\MSSQL\Binn`

After creating or changing custom dictionary files, restart the SQL Full-text Daemon Launcher with the following command:

```sql
EXECUTE sp_fulltext_service 'restart_all_fdhosts';
```
