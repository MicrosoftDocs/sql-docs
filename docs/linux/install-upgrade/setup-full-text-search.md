---
title: Install SQL Server Full-Text Search on Linux
description: Learn how to install SQL Server Full-Text Search on Linux. Full-Text Search enables you to run full-text queries against character-based data in SQL Server tables.
author: rwestMSFT
ms.author: randolphwest
ms.date: 01/08/2026
ms.service: sql
ms.subservice: linux
ms.topic: install-set-up-deploy
ms.custom:
  - intro-installation
  - linux-related-content
---
# Install SQL Server Full-Text Search on Linux

[!INCLUDE [SQL Server - Linux](../../includes/applies-to-version/sql-linux.md)]

The following steps install [Full-Text Search](../../relational-databases/search/full-text-search.md) (`mssql-server-fts`) on Linux. You can use Full-Text Search to run full-text queries against character-based data in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] tables.

::: moniker range=">=sql-server-linux-ver17 || >=sql-server-ver17"

> [!IMPORTANT]  
> The supported Full-Text languages and document types have changed in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] on Linux. You must rebuild any existing indexes upgraded from [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. For more information, see [Breaking changes to Database Engine features in SQL Server 2025](../../database-engine/breaking-changes-to-database-engine-features-in-sql-server-2025.md#full-text-queries-and-populations-fail-after-upgrade).

::: moniker-end

[!INCLUDE [editions-supported-features-linux](../../includes/editions-supported-features-linux.md)]

> [!NOTE]  
> Before you install [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Full-Text Search, first [install SQL Server](setup.md#platforms). This step configures the keys and repositories that you use when installing the `mssql-server-fts` package.

Install [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Full-Text Search for your platform:

## [Red Hat Enterprise Linux](#tab/rhel)

Use the following command to install `mssql-server-fts` on Red Hat Enterprise Linux.

```bash
sudo yum install -y mssql-server-fts
```

If you already have `mssql-server-fts` installed, update to the latest version using the following commands:

```bash
sudo yum check-update
sudo yum update mssql-server-fts
```

If you need an offline installation, locate the Full-Text Search package download in the [Release notes for SQL Server 2022 on Linux](../sql-server-linux-release-notes-2022.md). Then use the same offline installation steps described in the article [Install SQL Server](setup.md#offline).

## [SUSE Linux Enterprise Server](#tab/sles)

> [!NOTE]  
> Starting in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], SUSE Linux Enterprise Server (SLES) isn't supported.

Use the following command to install `mssql-server-fts` on SUSE Linux Enterprise Server.

```bash
sudo zypper install mssql-server-fts
```

If you already have `mssql-server-fts` installed, update to the latest version using the following commands:

```bash
sudo zypper refresh
sudo zypper update mssql-server-fts
```

If you need an offline installation, locate the Full-Text Search package download in the [Release notes for SQL Server 2022 on Linux](../sql-server-linux-release-notes-2022.md). Then use the same offline installation steps described in the article [Install SQL Server](setup.md#offline).

## [Ubuntu](#tab/ubuntu)

Use the following commands to install `mssql-server-fts` on Ubuntu.

```bash
sudo apt-get update
sudo apt-get install -y mssql-server-fts
```

If you already have `mssql-server-fts` installed, update to the latest version using the following commands:

```bash
sudo apt-get update
sudo apt-get install -y mssql-server-fts
```

If you need an offline installation, locate the Full-Text Search package download in the [Release notes for SQL Server 2025 on Linux](../sql-server-linux-release-notes-2025.md). Then use the same offline installation steps described in the article [Install SQL Server](setup.md#offline).

---

## Supported languages

Full-Text Search uses [word breakers](../../relational-databases/search/configure-and-manage-word-breakers-and-stemmers-for-search.md) that determine how to identify individual words based on language. You can get a list of registered word breakers by querying the [sys.fulltext_languages](../../relational-databases/system-catalog-views/sys-fulltext-languages-transact-sql.md) catalog view.

::: moniker range="<=sql-server-linux-ver16 || <=sql-server-ver16"

The following word breakers are installed with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and earlier versions:

| Language | Language ID |
| --- | --- |
| Neutral | 0 |
| Arabic | 1025 |
| Bengali (India) | 1093 |
| Bokmål | 1044 |
| Portuguese (Brazil) | 1046 |
| British English | 2057 |
| Bulgarian | 1026 |
| Catalan | 1027 |
| Chinese (Hong Kong SAR, PRC) | 3076 |
| Chinese (Macao SAR) | 5124 |
| Chinese (Singapore) | 4100 |
| Croatian | 1050 |
| Czech | 1029 |
| Danish | 1030 |
| Dutch | 1043 |
| English | 1033 |
| French | 1036 |
| German | 1031 |
| Greek | 1032 |
| Gujarati | 1095 |
| Hebrew | 1037 |
| Hindi | 1081 |
| Icelandic | 1039 |
| Indonesian | 1057 |
| Italian | 1040 |
| Japanese | 1041 |
| Kannada | 1099 |
| Korean | 1042 |
| Latvian | 1062 |
| Lithuanian | 1063 |
| Malay - Malaysia | 1086 |
| Malayalam | 1100 |
| Marathi | 1102 |
| Polish | 1045 |
| Portuguese | 2070 |
| Punjabi | 1094 |
| Romanian | 1048 |
| Russian | 1049 |
| Serbian (Cyrillic) | 3098 |
| Serbian (Latin) | 2074 |
| Simplified Chinese | 2052 |
| Slovak | 1051 |
| Slovenian | 1060 |
| Spanish | 3082 |
| Swedish | 1053 |
| Tamil | 1097 |
| Telugu | 1098 |
| Thai | 1054 |
| Traditional Chinese | 1028 |
| Turkish | 1055 |
| Ukrainian | 1058 |
| Urdu | 1056 |
| Vietnamese | 1066 |

::: moniker-end
::: moniker range=">=sql-server-linux-ver17 || >=sql-server-ver17"

The following word breakers are installed with [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions:

| Language | Language ID |
| --- | --- |
| Neutral | 0 |
| Arabic | 1025 |
| Bangla | 2117 |
| Bengali (India) | 1093 |
| Bokmål | 1044 |
| Brazilian | 1046 |
| British English | 2057 |
| Bulgarian | 1026 |
| Catalan | 1027 |
| Chinese (Hong Kong SAR, PRC) | 3076 |
| Chinese (Macao SAR) | 5124 |
| Chinese (Singapore) | 4100 |
| Croatian | 1050 |
| Czech | 1029 |
| Danish | 1030 |
| Dutch | 1043 |
| English | 1033 |
| Estonian | 1061 |
| Finnish | 1035 |
| French | 1036 |
| German | 1031 |
| Greek | 1032 |
| Gujarati | 1095 |
| Hebrew | 1037 |
| Hindi | 1081 |
| Hungarian | 1038 |
| Icelandic | 1039 |
| Indonesian | 1057 |
| Italian | 1040 |
| Japanese | 1041 |
| Kannada | 1099 |
| Korean | 1042 |
| Latvian | 1062 |
| Lithuanian | 1063 |
| Malay - Malaysia | 1086 |
| Malayalam | 1100 |
| Marathi | 1102 |
| Norwegian | 2068 |
| Polish | 1045 |
| Portuguese | 2070 |
| Punjabi | 1094 |
| Romanian | 1048 |
| Russian | 1049 |
| Serbian (Cyrillic) | 3098 |
| Serbian (Latin) | 2074 |
| Serbian (Sr-Latin) | 9242 |
| Simplified Chinese | 2052 |
| Slovak | 1051 |
| Slovenian | 1060 |
| Spanish | 3082 |
| Swedish | 1053 |
| Tamil | 1097 |
| Telugu | 1098 |
| Thai | 1054 |
| Traditional Chinese | 1028 |
| Turkish | 1055 |
| Ukrainian | 1058 |
| Urdu | 1056 |
| Vietnamese | 1066 |

::: moniker-end

## Filters

Full-Text Search also works with text stored in binary files. But in this case, an installed filter is required to process the file. For more information about filters, see [Configure and Manage Filters for Search](../../relational-databases/search/configure-and-manage-filters-for-search.md).

You can see a list of installed filters with the following Transact-SQL (T-SQL) query:

```sql
EXECUTE sp_help_fulltext_system_components 'filter';
```

::: moniker range="<=sql-server-linux-ver16 || <=sql-server-ver16"

The following filters are installed for [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and earlier versions:

| Component Name | Class ID | Version |
| --- | --- | --- |
| `.a` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.ans` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.asc` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.ascx` | E0CA5340-4534-11CF-B952-00AA0051FE20 | 12.0.6828.0 |
| `.asm` | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
| `.asp` | E0CA5340-4534-11CF-B952-00AA0051FE20 | 12.0.6828.0 |
| `.aspx` | E0CA5340-4534-11CF-B952-00AA0051FE20 | 12.0.6828.0 |
| `.asx` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.bas` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.bat` | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
| `.bcp` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.c` | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
| `.cc` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.cls` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.cmd` | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
| `.cpp` | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
| `.cs` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.csa` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.css` | E0CA5340-4534-11CF-B952-00AA0051FE20 | 12.0.6828.0 |
| `.csv` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.cxx` | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
| `.dbs` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.def` | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
| `.dic` | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
| `.dos` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.dsp` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.dsw` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.ext` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.faq` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.fky` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.h` | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
| `.hhc` | E0CA5340-4534-11CF-B952-00AA0051FE20 | 12.0.6828.0 |
| `.hpp` | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
| `.hta` | E0CA5340-4534-11CF-B952-00AA0051FE20 | 12.0.6828.0 |
| `.htm` | E0CA5340-4534-11CF-B952-00AA0051FE20 | 12.0.6828.0 |
| `.html` | E0CA5340-4534-11CF-B952-00AA0051FE20 | 12.0.6828.0 |
| `.htt` | E0CA5340-4534-11CF-B952-00AA0051FE20 | 12.0.6828.0 |
| `.htw` | E0CA5340-4534-11CF-B952-00AA0051FE20 | 12.0.6828.0 |
| `.htx` | E0CA5340-4534-11CF-B952-00AA0051FE20 | 12.0.6828.0 |
| `.hxx` | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
| `.i` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.ibq` | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
| `.ics` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.idl` | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
| `.idq` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.inc` | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
| `.inf` | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
| `.ini` | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
| `.inl` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.inx` | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
| `.jav` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.java` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.js` | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
| `.kci` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.lgn` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.log` | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
| `.lst` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.m3u` | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
| `.mak` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.mk` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.odc` | E0CA5340-4534-11CF-B952-00AA0051FE20 | 12.0.6828.0 |
| `.odh` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.odl` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.pkgdef` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.pkgundef` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.pl` | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
| `.prc` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.rc` | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
| `.rc2` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.rct` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.reg` | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
| `.rgs` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.rtf` | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
| `.rul` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.s` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.scc` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.shtm` | E0CA5340-4534-11CF-B952-00AA0051FE20 | 12.0.6828.0 |
| `.shtml` | E0CA5340-4534-11CF-B952-00AA0051FE20 | 12.0.6828.0 |
| `.snippet` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.sol` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.sor` | E0CA5340-4534-11CF-B952-00AA0051FE20 | 12.0.6828.0 |
| `.srf` | E0CA5340-4534-11CF-B952-00AA0051FE20 | 12.0.6828.0 |
| `.stm` | E0CA5340-4534-11CF-B952-00AA0051FE20 | 12.0.6828.0 |
| `.tab` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.tdl` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.tlh` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.tli` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.trg` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.txt` | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
| `.udf` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.udt` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.url` | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
| `.usr` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.vbs` | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
| `.viw` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.vsct` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.vsixlangpack` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.vsixmanifest` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.vspscc` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.vsscc` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.vssscc` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.wri` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
| `.wtx` | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
| `.xml` | 41B9BE05-B3AF-460C-BF0B-2CDD44A093B1 | 12.0.9735.0 |

::: moniker-end
::: moniker range=">=sql-server-linux-ver17 || >=sql-server-ver17"

The following filters are installed for [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions:

| Component Name | Class ID | Version |
| --- | --- | --- |
| `.a` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.ans` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.asc` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.ascx` | 56BD18AD-CF9C-4110-AAAA-B2F96887D123 | 16.0.18816.44375 |
| `.asm` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.asp` | 56BD18AD-CF9C-4110-AAAA-B2F96887D123 | 16.0.18816.44375 |
| `.aspx` | 56BD18AD-CF9C-4110-AAAA-B2F96887D123 | 16.0.18816.44375 |
| `.asx` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.bas` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.bat` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.bcp` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.c` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.cc` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.cls` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.cmd` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.cpp` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.cs` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.csa` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.css` | 56BD18AD-CF9C-4110-AAAA-B2F96887D123 | 16.0.18816.44375 |
| `.csv` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.cxx` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.dbs` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.def` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.dic` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.dos` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.dsp` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.dsw` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.ext` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.faq` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.fky` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.h` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.hhc` | 56BD18AD-CF9C-4110-AAAA-B2F96887D123 | 16.0.18816.44375 |
| `.hpp` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.hta` | 56BD18AD-CF9C-4110-AAAA-B2F96887D123 | 16.0.18816.44375 |
| `.htm` | 56BD18AD-CF9C-4110-AAAA-B2F96887D123 | 16.0.18816.44375 |
| `.html` | 56BD18AD-CF9C-4110-AAAA-B2F96887D123 | 16.0.18816.44375 |
| `.htt` | 56BD18AD-CF9C-4110-AAAA-B2F96887D123 | 16.0.18816.44375 |
| `.htw` | 56BD18AD-CF9C-4110-AAAA-B2F96887D123 | 16.0.18816.44375 |
| `.htx` | 56BD18AD-CF9C-4110-AAAA-B2F96887D123 | 16.0.18816.44375 |
| `.hxx` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.i` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.ibq` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.ics` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.idl` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.idq` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.inc` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.inf` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.ini` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.inl` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.inx` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.jav` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.java` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.js` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.kci` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.lgn` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.log` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.lst` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.m3u` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.mak` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.mk` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.odc` | 56BD18AD-CF9C-4110-AAAA-B2F96887D123 | 16.0.18816.44375 |
| `.odh` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.odl` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.odp` | 4693FF15-B962-420A-9E5D-176F7D4B8321 | 16.0.18816.44375 |
| `.ods` | E2F5480E-ED5A-4DDE-B8A8-F9F297479F62 | 16.0.18816.44375 |
| `.odt` | 9FBC2D8F-6F52-4CFA-A86F-096F3E9EB4B2 | 16.0.18816.44375 |
| `.pkgdef` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.pkgundef` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.pl` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.prc` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.rc` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.rc2` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.rct` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.reg` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.rgs` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.rul` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.s` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.scc` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.shtm` | 56BD18AD-CF9C-4110-AAAA-B2F96887D123 | 16.0.18816.44375 |
| `.shtml` | 56BD18AD-CF9C-4110-AAAA-B2F96887D123 | 16.0.18816.44375 |
| `.snippet` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.sol` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.sor` | 56BD18AD-CF9C-4110-AAAA-B2F96887D123 | 16.0.18816.44375 |
| `.srf` | 56BD18AD-CF9C-4110-AAAA-B2F96887D123 | 16.0.18816.44375 |
| `.stm` | 56BD18AD-CF9C-4110-AAAA-B2F96887D123 | 16.0.18816.44375 |
| `.tab` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.tdl` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.tlh` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.tli` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.trg` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.txt` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.udf` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.udt` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.url` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.usr` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.vbs` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.viw` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.vsct` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.vsixlangpack` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.vsixmanifest` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.vspscc` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.vsscc` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.vssscc` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.wri` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.wtx` | C1243CA0-BF96-11CD-B579-08002B30BFEB | 10.0.20348.3453 |
| `.xlsb` | 312AB530-ECC9-496E-AE0E-C9E6C5392499 | 16.0.18816.44375 |
| `.xlsm` | F90DFE0C-CBDF-41FF-8598-EDD8F222A2C8 | 16.0.18816.44375 |
| `.xlsx` | F90DFE0C-CBDF-41FF-8598-EDD8F222A2C8 | 16.0.18816.44375 |
| `.xml` | 41B9BE05-B3AF-460C-BF0B-2CDD44A093B1 | 2008.0.20348.3453 |
| `.zip` | 20E823C2-62F3-4638-96BD-90F4F6784EBC | 16.0.18816.44375 |

::: moniker-end

## Semantic search

[Semantic Search](../../relational-databases/search/semantic-search-sql-server.md) builds on the Full-Text Search feature to extract and index statistically relevant *key phrases*. Use this feature to query the meaning within documents in your database, and identify documents that are similar.

To use Semantic Search, first restore the Semantic Language Statistics database to your machine.

1. Use a tool, such as [sqlcmd](setup-tools.md), to run the following T-SQL command on your Linux [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance. This command restores the Language Statistics database. If necessary, update the paths to match your configuration.

   ```sql
   RESTORE DATABASE [semanticsdb]
   FROM DISK = N'/opt/mssql/misc/semanticsdb.bak'
   WITH FILE = 1,
       MOVE N'semanticsdb' TO N'/var/opt/mssql/data/semanticsDB.mdf',
       MOVE N'semanticsdb_log' TO N'/var/opt/mssql/data/semanticsdb_log.ldf',
       NOUNLOAD, STATS = 5;
   GO
   ```

1. Run the following T-SQL command to register the semantic language statistics database.

   ```sql
   EXECUTE sp_fulltext_semantic_register_language_statistics_db @dbname = N'semanticsdb';
   GO
   ```

## Related content

- [Full-Text Search](../../relational-databases/search/full-text-search.md)
